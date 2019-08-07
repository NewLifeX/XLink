/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:14:32
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using NewLife.Data;
using System;
using System.Collections.Generic;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace WiFi.Entity
{
    /// <summary>设备历史</summary>
    public partial class DeviceHistory : Entity<DeviceHistory>
    {
        #region 对象操作
        static DeviceHistory()
        {
            Meta.Table.DataTable.InsertOnly = true;

            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }
        #endregion

        #region 扩展属性
        /// <summary>设备</summary>
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceID));

        /// <summary>设备名</summary>
        [Map(__.DeviceID)]
        public String DeviceName => Device + "";
        #endregion

        #region 扩展查询
        #endregion

        #region 高级查询
        /// <summary>高级搜索</summary>
        /// <param name="deviceid"></param>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<DeviceHistory> Search(Int32 deviceid, String action, Boolean? success, DateTime start, DateTime end, String key, PageParameter page)
        {
            var list = Search(deviceid, action, success, start, end, key, page, false);
            // 如果结果为0，并且有key，则使用扩展查询，对内网外网地址进行模糊查询
            if (list.Count == 0 && !key.IsNullOrEmpty()) list = Search(deviceid, action, success, start, end, key, page, true);

            return list;
        }

        private static IList<DeviceHistory> Search(Int32 deviceid, String action, Boolean? success, DateTime start, DateTime end, String key, PageParameter page, Boolean ext)
        {
            var exp = new WhereExpression();

            if (deviceid >= 0) exp &= _.DeviceID == deviceid;
            if (!action.IsNullOrEmpty()) exp &= _.Action == action;
            if (success != null) exp &= _.Success == success;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
            {
                if (ext)
                    exp &= (_.Name.Contains(key) | _.Remark.Contains(key) | _.CreateIP.Contains(key));
                else
                    exp &= _.Name.StartsWith(key);
            }

            return FindAll(exp, page);
        }
        #endregion

        #region 扩展操作
        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static FieldCache<DeviceHistory> ActionCache = new FieldCache<DeviceHistory>(_.Action);

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllActionName() => ActionCache.FindAllName();
        #endregion

        #region 业务
        #endregion
    }
}