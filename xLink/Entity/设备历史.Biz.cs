/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:14:32
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Cache;
using xLink.Models;

namespace xLink.Entity
{
    /// <summary>设备历史</summary>
    public partial class DeviceHistory : Entity<DeviceHistory>, IHistory
    {
        #region 对象操作
        static DeviceHistory()
        {
            Meta.Table.DataTable.InsertOnly = true;
        }

        ///// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        ///// <param name="isNew"></param>
        //public override void Valid(Boolean isNew)
        //{
        //    // 如果没有脏数据，则不需要进行任何处理
        //    if (!HasDirty) return;

        //    // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
        //    base.Valid(isNew);
        //}
        #endregion

        #region 扩展属性
        /// <summary>设备</summary>
        public Device Device { get { return Extends.Get(nameof(Device), k => Device.FindByID(DeviceID)); } }

        /// <summary>设备名</summary>
        [Map(__.DeviceID, typeof(Device), "ID")]
        public String DeviceName { get { return Device + ""; } }

        /// <summary>地址。IP=>Address</summary>
        [DisplayName("创建地址")]
        public String CreateAddress { get { return CreateIP.IPToAddress(); } }

        Int32 IHistory.UserID { get => DeviceID; set => DeviceID = value; }
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

            if (deviceId >= 0) exp &= _.DeviceID == deviceId;
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
        #endregion

        #region 扩展操作
        /// <summary>类别名实体缓存，异步，缓存10分钟</summary>
        static FieldCache<DeviceHistory> ActionCache = new FieldCache<DeviceHistory>(__.Action);

        /// <summary>获取所有类别名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllActionName()
        {
            return ActionCache.FindAllName();
        }
        #endregion

        #region 业务
        #endregion
    }
}