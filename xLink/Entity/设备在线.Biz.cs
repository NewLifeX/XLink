/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:14:32
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using NewLife.Data;
using NewLife.Model;
using XCode;
using XCode.Cache;
using xLink.Models;

namespace xLink.Entity
{
    /// <summary>设备在线</summary>
    public partial class DeviceOnline : Entity<DeviceOnline>, IOnline
    {
        #region 对象操作
        static DeviceOnline()
        {
            var df = Meta.Factory.AdditionalFields;
            df.Add(__.LoginCount);
            df.Add(__.PingCount);
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
        public String DeviceName { get { return Device?.Name + ""; } }

        /// <summary>地址。IP=>Address</summary>
        [DisplayName("地址")]
        public String ExternalAddress { get { return ExternalUri.IPToAddress(); } }

        Int32 IOnline.UserID { get => DeviceID; set => DeviceID = value; }
        #endregion

        #region 扩展查询
        /// <summary>根据会话查找</summary>
        /// <param name="deviceid">会话</param>
        /// <returns></returns>
        public static DeviceOnline FindByDeviceID(Int32 deviceid)
        {
            return Find(__.DeviceID, deviceid);
        }

        /// <summary>根据会话查找</summary>
        /// <param name="sessionid">会话</param>
        /// <returns></returns>
        public static DeviceOnline FindBySessionID(Int32 sessionid)
        {
            //if (Meta.Count >= 1000)
            return Find(__.SessionID, sessionid);
            //else // 实体缓存
            //    return Meta.Cache.Entities.FirstOrDefault(e => e.SessionID == sessionid);
        }
        #endregion

        #region 高级查询
        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="productId">类型</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="param">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static IList<DeviceOnline> Search(Int32 productId, DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = new WhereExpression();

            if (productId >= 0) exp &= _.ProductID == productId;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty())
                exp &= (_.Name.Contains(key) | _.InternalUri.Contains(key) | _.ExternalUri.Contains(key));

            return FindAll(exp, param);
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务
        /// <summary>删除过期，指定过期时间</summary>
        /// <param name="secTimeout">超时时间，秒</param>
        /// <returns></returns>
        public static IList<DeviceOnline> ClearExpire(Int32 secTimeout)
        {
            // 10分钟不活跃将会被删除
            var exp = _.UpdateTime < DateTime.Now.AddSeconds(-secTimeout);
            var list = FindAll(exp, null, null, 0, 0);
            list.Delete();

            return list;
        }
        #endregion
    }
}