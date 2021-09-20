using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using NewLife;
using NewLife.Cube;
using NewLife.Cube.Charts;
using NewLife.Log;
using NewLife.Remoting;
using NewLife.Threading;
using NewLife.Web;
using XCode.Membership;
using xLink.Entity;
using static xLink.Entity.DeviceStat;

namespace xLink.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    public class DeviceStatController : ReadOnlyEntityController<DeviceStat>
    {
        static DeviceStatController()
        {
            MenuOrder = 30;

            // 计算统计
            _timer = new TimerX(DoDeviceStat, null, DateTime.Today, 3600_000) { Async = true };
            _timer2 = new TimerX(DoDeviceStat2, null, 5_000, 60_000) { Async = true };
            _timerReport = new TimerX(DoReport, null, DateTime.Today.AddMinutes(10), 24 * 3600 * 1000) { Async = true };

            // 先来一次
            _timer.SetNext(10_000);
            //_timerReport.SetNext(10_000);
        }

        protected override IEnumerable<DeviceStat> Search(Pager p)
        {
            var productId = p["productId"].ToInt(-1);
            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            // 如果选择产品，但没有排序，默认升序
            if (productId >= 0 && p.Sort.IsNullOrEmpty())
            {
                p.Sort = __.ID;
                p.Desc = false;
                if (p.PageSize == 20 || p.PageSize == 0) p.PageSize = 1000;

                if (start.Year < 2000 && end.Year < 2000)
                {
                    start = DateTime.Today.AddDays(-30);
                    p["dtStart"] = start.ToString("yyyy-MM-dd");
                }
            }

            var list = DeviceStat.Search(productId, start, end, p["Q"], p);

            if (list.Count > 0)
            {
                var hasDate = start.Year > 2000 || end.Year > 2000;
                // 指定产品后，绘制日期曲线图
                var prd = Product.FindByID(productId);
                if (productId >= 0)
                {
                    if (productId == 99)
                    {
                        var dic = list.GroupBy(e => e.StatDate).ToDictionary(e => e.Key, e => e.ToList());
                        var list2 = new List<DeviceStat>();
                        foreach (var item in dic)
                        {
                            var st = new DeviceStat { StatDate = item.Key };
                            st.Merge(item.Value);
                            list2.Add(st);
                        }
                        list = list2;
                    }

                    var chart = new ECharts
                    {
                        Title = new ChartTitle { Text = prd + "设备统计" },
                        Height = 400,
                    };
                    chart.SetX(list, _.StatDate, e => e.StatDate.ToString("MM-dd"));
                    chart.SetY("台数");
                    var sr = chart.Add(list, _.Total, "line", null);
                    sr.Smooth = true;
                    chart.Add(list, _.Actives);
                    chart.Add(list, _.News);
                    chart.Add(list, _.T7Actives);
                    chart.Add(list, _.T7News);
                    chart.Add(list, _.T30Actives);
                    chart.Add(list, _.T30News);
                    chart.Add(list, _.Registers);
                    chart.Add(list, _.MaxOnline);
                    chart.SetTooltip();
                    ViewBag.Charts = new[] { chart };
                }
                // 指定日期后，绘制饼图
                if (hasDate && productId < 0)
                {
                    var w = 400;
                    var h = 300;

                    // 去掉所有产品
                    list = list.Where(e => e.ProductId > 0).ToList();

                    var chart0 = new ECharts { Width = w, Height = h };
                    chart0.Add(list, _.Total, "pie", e => new { name = e.ProductName, value = e.Total });

                    var chart1 = new ECharts { Width = w, Height = h };
                    chart1.Add(list, _.Actives, "pie", e => new { name = e.ProductName, value = e.Actives });

                    var chart2 = new ECharts { Width = w, Height = h };
                    chart2.Add(list, _.News, "pie", e => new { name = e.ProductName, value = e.News });

                    var chart3 = new ECharts { Width = w, Height = h };
                    chart3.Add(list, _.Registers, "pie", e => new { name = e.ProductName, value = e.Registers });

                    var chart4 = new ECharts { Width = w, Height = h };
                    chart4.Add(list, _.MaxOnline, "pie", e => new { name = e.ProductName, value = e.MaxOnline });

                    ViewBag.Charts2 = new[] { chart0, chart1, chart2, chart3, chart4 };
                }
            }

            return list;
        }

        private static TimerX _timer;
        private static TimerX _timer2;
        private static void DoDeviceStat(Object state)
        {
            // 计算统计，月初开始
            var date = DateTime.Today;
            date = new DateTime(date.Year, date.Month, 1);
            var p = Parameter.GetOrAdd(0, "统计", "设备日统计", date.ToFullString("yyyy-MM-dd"));
            date = p.GetValue().ToDateTime();

            while (date <= DateTime.Today)
            {
                DeviceStat.ProcessDate(date);

                date = date.AddDays(1);
            }

            // 保存位置
            p.SetValue(date.AddDays(-1));
            p.Save();
        }

        private static void DoDeviceStat2(Object state) => DeviceStat.ProcessDate2(DateTime.Today);

        private static TimerX _timerReport;
        //private static Int32[] _hours = new[] { 0, 12, 18 };
        private static void DoReport(Object state)
        {
            //if (_setting != null && !_setting.DeviceStatSwitch) return;

            // 从字典参数读取节点名，置空则不发送报告
            var p = Parameter.GetOrAdd(0, "统计", "节点名", Environment.MachineName);
            var nodeName = p.GetValue() + "";
            if (nodeName.IsNullOrEmpty()) return;

            var dt = DateTime.Now;
            //if (!_hours.Contains(dt.Hour)) return;

            // 昨天/今天 的所有统计数据
            if (dt.Hour < 8) dt = dt.AddDays(-1);

            var list = DeviceStat.FindAllByDate(dt.Date);
            list = list.Where(e => e.MaxOnline > 0).OrderByDescending(e => e.Total).ToList();
            if (list.Count == 0) return;

            // 按照产品节点类型汇总，例如，把海康视觉汇总到中通扫描上面
            var list2 = new List<DeviceStat>();
            foreach (var item in list)
            {
                var st = list2.FirstOrDefault(e => e.Product?.Kind == item.Product?.Kind);
                if (st == null)
                    list2.Add(item);
                else
                {
                    st.Total += item.Total;
                    st.Actives += item.Actives;
                    st.News += item.News;
                    st.T7Actives += item.T7Actives;
                    st.T7News += item.T7News;
                    st.T30Actives += item.T30Actives;
                    st.T30News += item.T30News;
                    st.MaxOnline += item.MaxOnline;
                }
            }

            var sb = new StringBuilder();
            sb.Append($"扫描云中台[{dt:MM-dd}@{nodeName}]报告（今天/7天/30天）：\n");
            foreach (var item in list2)
            {
                sb.Append($"[{item.ProductName ?? "所有产品"}] 总数{item.Total}，活跃{item.Actives}/{item.T7Actives}/{item.T30Actives}，新增{item.News}/{item.T7News}/{item.T30News}，最高在线{item.MaxOnline}");
                if (item.MaxOnlineTime.Year > 2000) sb.Append($" [{item.MaxOnlineTime:HH:mm:ss}]");
                sb.Append("\n");
            }

            var msg = sb.ToString();
            XTrace.WriteLine(msg);

            // 发钉钉
            var token = "83694ec8aa5c1b3337cbda5f576692e7f7e35343ef2e58d68ff399dd77a7017c";
            try
            {
                SendDingTalk(token, msg);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }
        }

        private static void SendDingTalk(String access_token, String content)
        {
            var action = $"robot/send?access_token={access_token}";
            var model = new { msgtype = "text", text = new { content } };

            /*
             * {"errmsg":"ok","errcode":0}
             * {"errmsg":"param error","errcode":300001}
             */

            var client = new HttpClient
            {
                BaseAddress = new Uri("https://oapi.dingtalk.com")
            };
            client.PostAsync<String>(action, model);
        }
    }
}