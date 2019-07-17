using NewLife.Data;
using NewLife.Security;
using System;
using System.Collections.Generic;
using XCode;
using XCode.Membership;

namespace Vsd.Entity
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

            if (isNew)
            {
                // 自动生成产品证书密钥
                if (Code.IsNullOrEmpty()) Code = Rand.NextString(4);
                if (Secret.IsNullOrEmpty()) Secret = Rand.NextString(8);
            }
        }
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
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Name == name);

            return Find(_.Name == name);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="code">名称</param>
        /// <returns>实体对象</returns>
        public static Product FindByCode(String code)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Code == code);

            // 单对象缓存
            return Meta.SingleCache.GetItemWithSlaveKey(code) as Product;

            //return Find(_.Code == code);
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
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
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