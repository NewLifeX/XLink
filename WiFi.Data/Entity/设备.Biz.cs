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

            if (ParameterA <= 0) ParameterA = 60;
            if (ParameterN < 0.01) ParameterN = 3.3;
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
        /// <param name="kind"></param>
        /// <param name="enable"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<Device> Search(DeviceKinds kind, Boolean? enable, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (kind > 0) exp &= _.Kind == kind;
            if (enable != null) exp &= _.Enable == enable.Value;

            //exp &= _.CreateTime.Between(start, end);
            exp &= _.LastLogin.Between(start, end);

            if (!key.IsNullOrEmpty()) exp &= SearchWhereByKeys(key);

            return FindAll(exp, page);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        #endregion

        #region 辅助
        /// <summary>显示友好名称</summary>
        /// <returns></returns>
        public override String ToString() => Name.IsNullOrEmpty() ? Code : Name;
        #endregion
    }
}