using NewLife;
using NewLife.Data;
using NewLife.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using XCode;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>产品</summary>
    public partial class Product : Entity<Product>
    {
        #region 对象操作
        static Product()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.Status);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();

            // 单对象缓存
            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = k => Find(__.Code, k);
            sc.GetSlaveKeyMethod = e => e.Code;
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 自动生成产品证书密钥
            if (Code.IsNullOrEmpty()) Code = Rand.NextString(8);
            if (Secret.IsNullOrEmpty()) Secret = Rand.NextString(16);

            if (Kind.IsNullOrEmpty()) Kind = "设备";
            if (Category.IsNullOrEmpty()) Category = "边缘网关";
            if (DataFormat.IsNullOrEmpty()) DataFormat = "Json";
            if (NetworkProtocol.IsNullOrEmpty()) NetworkProtocol = "以太网";
        }

        /// <summary>初始化</summary>
        protected override void InitData()
        {
            if (Meta.Count > 0) return;

            var entity = new Product
            {
                Name = "默认",
                Enable = true,

                Code = "Apollo03",
                Secret = "Apollo03Apollo03",
                AutoRegister = true,
            };
            entity.Insert();
        }

        /// <summary>已重载</summary>
        /// <returns></returns>
        public override String ToString() => Name ?? Code;
        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Product FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="name">名称</param>
        /// <returns>实体对象</returns>
        public static Product FindByName(String name)
        {
            if (name.IsNullOrEmpty()) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name == name);

            return Find(_.Name == name);
        }

        /// <summary>根据唯一编码查找</summary>
        /// <param name="code">唯一编码</param>
        /// <returns></returns>
        public static Product FindByCode(String code)
        {
            if (code.IsNullOrEmpty()) return null;

            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Code == code);

            return Meta.SingleCache.GetItemWithSlaveKey(code) as Product;
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<Product> Search(Boolean? enable, DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = SearchWhereByKeys(key, null, null);

            if (enable != null) exp &= _.Enable == enable.Value;

            exp &= _.UpdateTime.Between(start, end);

            return FindAll(exp, param);
        }
        #endregion

        #region 业务操作
        #endregion
    }
}