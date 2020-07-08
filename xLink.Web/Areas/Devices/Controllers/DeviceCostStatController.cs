using System;
using System.Collections.Generic;
using System.Linq;
using NewLife.Cube;
using NewLife.Cube.Charts;
using NewLife.Threading;
using NewLife.Web;
using XCode.Membership;
using ZTO.Scan.Data;
using ZTO.Scan.Devices;
using static ZTO.Scan.Devices.DeviceCostStat;

namespace ZTO.Scan.Web.Areas.Devices.Controllers
{
    [DevicesArea]
    public class DeviceCostStatController : EntityController<DeviceCostStat>
    {
        static DeviceCostStatController()
        {
            MenuOrder = 20;

            // 计算统计
            _timer = new TimerX(DoDeviceStat, null, 10_000, 60_000) { Async = true };
        }

        protected override IEnumerable<DeviceCostStat> Search(Pager p)
        {
            var cityId = p["cityId"].ToInt(-1);
            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            var list = DeviceCostStat.Search(cityId, start, end, p["Q"], p);

            if (list.Count > 0)
            {
                // 指定地区后，绘制日期曲线图
                var r = Area.FindByID(cityId);
                if (cityId >= 0)
                {
                    var chart = new ECharts
                    {
                        Title = new ChartTitle { Text = r + "设备耗时统计" },
                        Height = 400,
                    };
                    chart.SetX(list, _.StatDate, e => e.StatDate.ToString("MM-dd"));
                    chart.SetY("耗时");
                    chart.Add(list, _.ACostAgv);
                    chart.Add(list, _.BCostAgv);
                    chart.Add(list, _.CCostAgv);
                    chart.Add(list, _.DCostAgv);
                    chart.Add(list, _.ECostAgv);
                    chart.Add(list, _.FCostAgv);
                    chart.SetTooltip();
                    ViewBag.Charts = new[] { chart };
                }
            }

            return list;
        }

        private static TimerX _timer;
        private static void DoDeviceStat(Object state)
        {
            var date = DateTime.Today;

            var ps = Parameter.Search(0, "统计", null, null, null);
            var p = ps.FirstOrDefault(e => e.Name == "设备耗时日统计");
            if (p != null)
            {
                if (!p.Enable) return;

                date = p.GetValue().ToDateTime();
            }
            else
            {
                // 计算统计，月初开始
                date = new DateTime(date.Year, date.Month, 1);
                p = new Parameter { Category = "统计", Name = "设备耗时日统计", Enable = true };
                p.SetValue(date);
                p.Insert();
            }

            while (date <= DateTime.Today)
            {
                DeviceCostStat.ProcessDate(date);

                date = date.AddDays(1);
            }

            // 保存位置
            p.SetValue(date.AddDays(-1));
            p.Save();
        }
    }
}