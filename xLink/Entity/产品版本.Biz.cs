using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using NewLife.Data;
using XCode;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>产品通道</summary>
    public enum ProductChannels
    {
        /// <summary>发布</summary>
        Release = 1,

        /// <summary>测试</summary>
        Beta = 2,

        /// <summary>开发</summary>
        Develop = 3,

        /// <summary>预览</summary>
        Preview = 4,
    }

    /// <summary>产品版本。产品固件更新管理</summary>
    public partial class ProductVersion : Entity<ProductVersion>
    {
        #region 对象操作
        static ProductVersion()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.ProductId);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 重新聚合规则
            if (!Dirtys[__.Strategy] && Rules != null)
            {
                var sb = new StringBuilder();
                foreach (var item in Rules)
                {
                    if (sb.Length > 0) sb.Append(";");
                    sb.AppendFormat("{0}={1}", item.Key, item.Value.Join(","));
                }
                Strategy = sb.ToString();
            }

            // 默认通道
            if (Channel < ProductChannels.Release || Channel > ProductChannels.Develop) Channel = ProductChannels.Release;
        }

        /// <summary>加载后，释放规则</summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            var dic = Strategy.SplitAsDictionary("=", ";");
            Rules = dic.ToDictionary(e => e.Key, e => e.Value.Split(","));
        }
        #endregion

        #region 扩展属性
        /// <summary>产品</summary>
        [XmlIgnore, IgnoreDataMember]
        public Product Product => Extends.Get(nameof(Product), k => Product.FindByID(ProductId));

        /// <summary>产品</summary>
        [XmlIgnore, IgnoreDataMember]
        [DisplayName("产品")]
        [Map(__.ProductId, typeof(Product), "ID")]
        public String ProductName => Product?.Name;

        /// <summary>规则集合</summary>
        [XmlIgnore, IgnoreDataMember]
        public IDictionary<String, String[]> Rules { get; set; }
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static ProductVersion FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据产品查找版本</summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static IList<ProductVersion> FindAllByProductId(Int32 productId)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.ProductId == productId);

            return FindAll(_.ProductId == productId);
        }

        /// <summary>根据产品、版本号查找</summary>
        /// <param name="productid">产品</param>
        /// <param name="version">版本号</param>
        /// <returns>实体对象</returns>
        public static ProductVersion FindByProductIDAndVersion(Int32 productid, String version)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ProductId == productid && e.Version == version);

            return Find(_.ProductId == productid & _.Version == version);
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="productId"></param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<ProductVersion> Search(Int32 productId, Boolean? enable, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (productId >= 0) exp &= _.ProductId == productId;
            if (enable != null) exp &= _.Enable == enable.Value;
            exp &= _.UpdateTime.Between(start, end);
            if (!key.IsNullOrEmpty()) exp &= _.Version.Contains(key) | _.Description.Contains(key);

            return FindAll(exp, page);
        }
        #endregion

        #region 业务操作
        /// <summary>获取有效</summary>
        /// <param name="productId"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static IList<ProductVersion> GetValids(Int32 productId, ProductChannels channel)
        {
            var list = Meta.Cache.FindAll(e => e.ProductId == productId && e.Enable);
            if (list.Count == 0) return list;

            if (channel >= ProductChannels.Release) list = list.Where(e => e.Channel == channel).ToList();

            // 按照编号降序，最大100个
            list = list.OrderByDescending(e => e.ID).Take(100).ToList();

            return list;
        }

        /// <summary>应用策略是否匹配指定设备</summary>
        /// <param name="dv"></param>
        /// <returns></returns>
        public Boolean Match(Device dv)
        {
            // 没有使用该规则，直接过
            if (Rules.TryGetValue("province", out var vs))
            {
                var prov = dv.Province?.Name;
                if (prov.IsNullOrEmpty() || !vs.Contains(prov)) return false;
            }

            if (Rules.TryGetValue("city", out vs))
            {
                var city = dv.City?.Name;
                if (city.IsNullOrEmpty() || !vs.Contains(city)) return false;
            }

            if (Rules.TryGetValue("version", out vs))
            {
                var ver = dv.Version;
                if (ver.IsNullOrEmpty() || !vs.Contains(ver)) return false;
            }

            if (Rules.TryGetValue("device", out vs))
            {
                var code = dv.Code;
                if (code.IsNullOrEmpty() || !vs.Contains(code)) return false;
            }

            return true;
        }
        #endregion
    }
}