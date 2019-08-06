/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 23:16:13
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using NewLife.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using XCode;
using XCode.Membership;

namespace WiFi.Entity
{
    /// <summary>设备种类</summary>
    public enum DeviceKinds
    {
        /// <summary>终端设备</summary>
        Device = 1,

        /// <summary>路由</summary>
        Route = 2,

        /// <summary>主机</summary>
        Host = 3,
    }

    /// <summary>设备</summary>
    public partial class Device : Entity<Device>
    {
        #region 对象操作
        static Device()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.Logins);

            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = e => Find(__.Name, e);
            sc.GetSlaveKeyMethod = e => e.Name;
            sc.SlaveKeyIgnoreCase = false;

            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;
        }
        #endregion

        #region 扩展属性
        /// <summary>最后主机</summary>
        public Device LastHost => Extends.Get(nameof(LastHost), k => Device.FindByID(LastHostID));

        /// <summary>最后主机</summary>
        [Map(__.LastHostID)]
        public String LastHostName => LastHost + "";

        /// <summary>最后路由</summary>
        public Device LastRoute => Extends.Get(nameof(LastRoute), k => Device.FindByID(LastRouteID));

        /// <summary>最后路由</summary>
        [Map(__.LastRouteID)]
        public String LastRouteName => LastRoute + "";

        /// <summary>最后地址。IP=>Address</summary>
        [DisplayName("最后地址")]
        public String LastLoginAddress => LastLoginIP.IPToAddress();
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Device FindByID(Int32 id)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];
        }

        /// <summary>根据名称。登录用户名查找</summary>
        /// <param name="name">名称。登录用户名</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static Device FindByName(String name)
        {
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.Name == name);

            return Find(__.Name, name);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="code">名称</param>
        /// <returns>实体对象</returns>
        public static Device FindByCode(String code)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Code == code);

            // 单对象缓存
            return Meta.SingleCache.GetItemWithSlaveKey(code) as Device;

            //return Find(_.Code == code);
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="type"></param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<Device> Search(String type, Boolean? enable, DateTime start, DateTime end, String key, PageParameter param)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索，第二个参数可指定要搜索的字段
            var exp = SearchWhereByKeys(key, null, null);

            //if (!type.IsNullOrEmpty()) exp &= _.Type == type;
            if (enable != null) exp &= _.Enable == enable.Value;

            //exp &= _.CreateTime.Between(start, end);
            exp &= _.LastLogin.Between(start, end);

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion

        #region 辅助
        /// <summary>显示友好名称</summary>
        /// <returns></returns>
        public override String ToString() => Name;
        #endregion
    }
}