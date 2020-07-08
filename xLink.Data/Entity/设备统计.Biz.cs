using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using XCode;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>设备统计。每日按产品统计</summary>
    public partial class DeviceStat : Entity<DeviceStat>
    {
        #region 对象操作
        static DeviceStat()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.ProductId);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<TimeModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
            //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
            //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;

            // 检查唯一索引
            // CheckExist(isNew, __.StatDate, __.ProductId);
        }
        #endregion

        #region 扩展属性
        /// <summary>产品</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public Product Product => Extends.Get(nameof(Product), k => Product.FindByID(ProductId));

        /// <summary>产品</summary>
        [Map(__.ProductId, typeof(Product), "ID")]
        public String ProductName => Product?.Name;
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static DeviceStat FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据日期查找</summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static IList<DeviceStat> FindAllByDate(DateTime date) => FindAll(_.StatDate == date);
        #endregion

        #region 高级查询
        /// <summary>查询指定产品的统计数据</summary>
        /// <param name="productId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<DeviceStat> Search(Int32 productId, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            exp &= _.StatDate.Between(start, end);
            if (productId >= 0) exp &= _.ProductId == productId;

            if (!key.IsNullOrEmpty()) exp &= _.Remark.Contains(key);

            return FindAll(exp, page);
        }
        #endregion

        #region 业务操作
        /// <summary>统计指定日期的数据，每小时，耗时较大</summary>
        /// <param name="date"></param>
        public static void ProcessDate(DateTime date)
        {
            // 这一天的所有统计数据
            var sts = FindAllByDate(date);

            // 活跃数
            {
                var dic = Device.SearchGroupByLastLogin(date, date.AddDays(1));
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.Actives = item.Value;
                }
            }
            // 7天活跃数
            {
                var dic = Device.SearchGroupByLastLogin(date.AddDays(-7 + 1), date.AddDays(1));
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.T7Actives = item.Value;
                }
            }
            // 30天活跃数
            {
                var dic = Device.SearchGroupByLastLogin(date.AddDays(-30 + 1), date.AddDays(1));
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.T30Actives = item.Value;
                }
            }

            // 新增数
            {
                var dic = Device.SearchGroupByCreateTime(date, date.AddDays(1));
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.News = item.Value;
                }
            }
            // 7天新增
            {
                var dic = Device.SearchGroupByCreateTime(date.AddDays(-7 + 1), date.AddDays(1));
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.T7News = item.Value;
                }
            }
            // 30天新增
            {
                var dic = Device.SearchGroupByCreateTime(date.AddDays(-30 + 1), date.AddDays(1));
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.T30News = item.Value;
                }
            }

            // 注册数
            {
                //var his = DeviceHistory.Search(-1, -1, -1, "动态注册", null, date, date, null, null);
                var his = DeviceHistory.SearchGroupByDevice("动态注册", date, date);
                var dvs = his.Select(e => e.Key).Select(Device.FindByID).ToList();
                var dic = dvs.Where(e => e != null).GroupBy(e => e.ProductId).ToDictionary(e => e.Key, e => e.ToList());
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.Registers = item.Value.Count;
                }
            }

            // 总数
            {
                var dic = Device.SearchCountByCreateDate(date);
                foreach (var item in dic)
                {
                    var st = GetStat(sts, item.Key, date);
                    st.Total = item.Value;
                }
            }

            // 最高在线
            if (date == DateTime.Today)
            {
                var dic = DeviceOnline.SearchGroupByProduct();
                foreach (var item in dic)
                {
                    if (item.Key == 0) continue;

                    var st = GetStat(sts, item.Key, date);
                    if (item.Value > st.MaxOnline)
                    {
                        st.MaxOnline = item.Value;
                        st.MaxOnlineTime = DateTime.Now;
                    }
                }
            }

            // 计算所有产品
            {
                var st = GetStat(sts, 0, date);

                var sts2 = sts.Where(e => e.ProductId > 0).ToList();
                //st.Total = sts2.Sum(e => e.Total);
                //st.Actives = sts2.Sum(e => e.Actives);
                //st.News = sts2.Sum(e => e.News);
                //st.T7Actives = sts2.Sum(e => e.T7Actives);
                //st.T7News = sts2.Sum(e => e.T7News);
                //st.T30Actives = sts2.Sum(e => e.T30Actives);
                //st.T30News = sts2.Sum(e => e.T30News);
                //st.Registers = sts2.Sum(e => e.Registers);

                var max = st.MaxOnline;
                st.Merge(sts2);

                //var max = sts2.Sum(e => e.MaxOnline);
                if (max < st.MaxOnline)
                {
                    //st.MaxOnline = max;
                    st.MaxOnlineTime = DateTime.Now;
                }
            }

            // 保存统计数据
            sts.Save(true);
        }

        /// <summary>统计指定日期的数据，每分钟，耗时较小</summary>
        /// <param name="date"></param>
        public static void ProcessDate2(DateTime date)
        {
            // 这一天的所有统计数据
            var sts = FindAllByDate(date);

            // 最高在线
            if (date == DateTime.Today)
            {
                var dic = DeviceOnline.SearchGroupByProduct();
                foreach (var item in dic)
                {
                    if (item.Key == 0) continue;

                    var st = GetStat(sts, item.Key, date);
                    if (item.Value > st.MaxOnline)
                    {
                        st.MaxOnline = item.Value;
                        st.MaxOnlineTime = DateTime.Now;
                    }
                }
            }

            // 计算所有产品
            {
                var st = GetStat(sts, 0, date);

                var sts2 = sts.Where(e => e.ProductId > 0).ToList();
                st.Total = sts2.Sum(e => e.Total);
                st.Actives = sts2.Sum(e => e.Actives);
                st.News = sts2.Sum(e => e.News);
                st.T7Actives = sts2.Sum(e => e.T7Actives);
                st.T7News = sts2.Sum(e => e.T7News);
                st.T30Actives = sts2.Sum(e => e.T30Actives);
                st.T30News = sts2.Sum(e => e.T30News);
                st.Registers = sts2.Sum(e => e.Registers);

                var max = sts2.Sum(e => e.MaxOnline);
                if (max > st.MaxOnline)
                {
                    st.MaxOnline = max;
                    st.MaxOnlineTime = DateTime.Now;
                }
            }

            // 保存统计数据
            sts.Save(true);
        }

        private static DeviceStat GetStat(IList<DeviceStat> sts, Int32 productId, DateTime date)
        {
            var st = sts.FirstOrDefault(e => e.ProductId == productId);
            if (st == null)
            {
                st = new DeviceStat { StatDate = date, ProductId = productId };
                sts.Add(st);
            }

            return st;
        }

        /// <summary>合并多方数据到当前统计行</summary>
        /// <param name="stats"></param>
        public void Merge(IList<DeviceStat> stats)
        {
            var st = this;
            st.Total = stats.Sum(e => e.Total);
            st.Actives = stats.Sum(e => e.Actives);
            st.News = stats.Sum(e => e.News);
            st.T7Actives = stats.Sum(e => e.T7Actives);
            st.T7News = stats.Sum(e => e.T7News);
            st.T30Actives = stats.Sum(e => e.T30Actives);
            st.T30News = stats.Sum(e => e.T30News);
            st.Registers = stats.Sum(e => e.Registers);
            st.MaxOnline = stats.Sum(e => e.MaxOnline);
        }
        #endregion
    }
}