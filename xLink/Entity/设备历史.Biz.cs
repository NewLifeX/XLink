/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:14:32
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.Linq;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Membership;
using xLink.Models;

namespace xLink.Entity
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

        /// <summary>插入或修改时</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 截断日志
            var len = _.Remark.Length;
            if (!Remark.IsNullOrEmpty() && len > 0 && Remark.Length > len) Remark = Remark.Substring(0, len);
        }
        #endregion

        #region 扩展属性
        /// <summary>设备</summary>
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceId));

        /// <summary>设备名</summary>
        [Map(__.DeviceId, typeof(Device), "ID")]
        public String DeviceName => Device + "";

        /// <summary>城市</summary>
        public Area City => Extends.Get(nameof(City), k => Area.FindByID(AreaId));

        /// <summary>城市名</summary>
        [Map(__.AreaId)]
        public String CityName => City + "";
        #endregion

        #region 扩展查询
        #endregion

        #region 高级查询
        /// <summary>高级搜索</summary>
        /// <param name="deviceId"></param>
        /// <param name="action"></param>
        /// <param name="result"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<DeviceHistory> Search(Int32 deviceId, String action, Int32 result, DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = new WhereExpression();

            if (deviceId >= 0) exp &= _.DeviceId == deviceId;
            //if (!type.IsNullOrEmpty()) exp &= _.Type == type;
            if (!action.IsNullOrEmpty()) exp &= _.Action == action;
            if (result == 0)
                exp &= _.Success == false;
            else if (result == 1)
                exp &= _.Success == true;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
                exp &= (_.Name.Contains(key) | _.Remark.Contains(key) | _.CreateIP.Contains(key));

            return FindAll(exp, param);
        }

        /// <summary>高级搜索</summary>
        /// <param name="areaId"></param>
        /// <param name="deviceId"></param>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IList<DeviceHistory> Search(Int32 areaId, Int32 deviceId, String action, Boolean? success, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (areaId > 0) exp &= _.AreaId == areaId;
            if (deviceId >= 0) exp &= _.DeviceId == deviceId;
            if (!action.IsNullOrEmpty()) exp &= _.Action.In(action.Split(","));
            if (success != null) exp &= _.Success == success;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
            {
                exp &= (_.Name.Contains(key) | _.Remark.Contains(key) | _.CreateIP.Contains(key));
            }

            return FindAll(exp, page);
        }

        /// <summary>根据设备，分组统计</summary>
        /// <returns></returns>
        public static IDictionary<Int32, Int32> SearchGroupByDevice(String action, DateTime start, DateTime end)
        {
            var exp = new WhereExpression();
            if (!action.IsNullOrEmpty()) exp &= _.Action == action;
            exp &= _.CreateTime.Between(start, end);

            var list = FindAll(exp.GroupBy(_.DeviceId), null, _.ID.Count() & _.DeviceId);
            return list.ToDictionary(e => e.DeviceId, e => e.ID);
        }
        #endregion

        #region 扩展操作
        /// <summary>操作名实体缓存，异步，缓存10分钟</summary>
        static Lazy<FieldCache<DeviceHistory>> ActionCache = new Lazy<FieldCache<DeviceHistory>>(() => new FieldCache<DeviceHistory>(__.Action)
        {
            Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty,
            MaxRows = 50
        });

        /// <summary>获取所有操作名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllAction() => ActionCache.Value.FindAllName();
        #endregion

        #region 业务
        /// <summary>创建日志</summary>
        /// <param name="dv"></param>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="remark"></param>
        /// <param name="creator"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static DeviceHistory Create(IDevice dv, String action, Boolean success, String remark, String creator, String ip)
        {
            if (dv == null) dv = new Device();

            var hi = new DeviceHistory
            {
                DeviceId = dv.ID,
                Name = dv.Name,
                Action = action,
                Success = success,
                Creator = creator,

                AreaId = dv.CityId,
                Version = dv.Version,

                Remark = remark,

                CreateTime = DateTime.Now,
                CreateIP = ip,
            };

            hi.SaveAsync();

            return hi;
        }
        #endregion
    }
}