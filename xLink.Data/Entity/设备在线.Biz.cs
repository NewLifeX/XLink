/*
 * XCoder v6.9.6298.42194
 * 作者：nnhy/X3
 * 时间：2017-03-31 22:14:32
 * 版权：版权所有 (C) 新生命开发团队 2002~2017
*/
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
    /// <summary>设备在线</summary>
    public partial class DeviceOnline : Entity<DeviceOnline>
    {
        #region 对象操作
        static DeviceOnline()
        {
            var df = Meta.Factory.AdditionalFields;
            //df.Add(__.LoginCount);
            df.Add(__.PingCount);

            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();

            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = k => Find(_.SessionID == k);
            sc.GetSlaveKeyMethod = e => e.SessionID;
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            if (SessionID.IsNullOrEmpty()) throw new ArgumentNullException(__.SessionID, _.SessionID.DisplayName + "不能为空！");

            var len = _.MACs.Length;
            if (MACs != null && len > 0 && MACs.Length > len) MACs = MACs.Substring(0, len);
            len = _.COMs.Length;
            if (COMs != null && len > 0 && COMs.Length > len) COMs = COMs.Substring(0, len);

            len = _.Processes.Length;
            if (Processes != null && len > 0 && Processes.Length > len) Processes = Processes.Substring(0, len);
        }
        #endregion

        #region 扩展属性
        /// <summary>产品</summary>
        [XmlIgnore, IgnoreDataMember]
        public Product Product => Extends.Get(nameof(Product), k => Product.FindByID(ProductId));

        /// <summary>产品名</summary>
        [Map(__.ProductId)]
        public String ProductName => Product + "";

        /// <summary>设备</summary>
        [XmlIgnore, IgnoreDataMember]
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceId));

        /// <summary>设备名</summary>
        [Map(__.DeviceId)]
        public String DeviceName => Device + "";

        /// <summary>城市</summary>
        public Area City => Extends.Get(nameof(City), k => Area.FindByID(AreaId));

        /// <summary>城市名</summary>
        [Map(__.AreaId)]
        public String CityName => City + "";
        #endregion

        #region 扩展查询
        /// <summary>根据会话查找</summary>
        /// <param name="deviceid">会话</param>
        /// <returns></returns>
        public static DeviceOnline FindByDeviceID(Int32 deviceid)
        {
            return Find(__.DeviceId, deviceid);
        }

        /// <summary>根据会话查找</summary>
        /// <param name="sessionid">会话</param>
        /// <param name="cache">是否走缓存</param>
        /// <returns></returns>
        public static DeviceOnline FindBySessionID(String sessionid, Boolean cache = true)
        {
            if (!cache) return Find(_.SessionID == sessionid);

            return Meta.SingleCache.GetItemWithSlaveKey(sessionid) as DeviceOnline;
        }
        #endregion

        #region 高级查询
        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="productId">产品</param>
        /// <param name="deviceId">设备</param>
        /// <param name="siteId">网点</param>
        /// <param name="areaId">地区</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static IList<DeviceOnline> Search(Int32 productId, Int32 deviceId, Int32 areaId, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (productId >= 0) exp &= _.ProductId == productId;
            if (deviceId >= 0) exp &= _.DeviceId == deviceId;
            if (areaId >= 0) exp &= _.AreaId == areaId;

            exp &= _.CreateTime.Between(start, end);

            if (!key.IsNullOrEmpty()) exp &= (_.Name.Contains(key) | _.SessionID.Contains(key));

            return FindAll(exp, page);
        }

        /// <summary>根据产品，分组统计在线数</summary>
        /// <returns></returns>
        public static IDictionary<Int32, Int32> SearchGroupByProduct()
        {
            var list = FindAll(_.ProductId.GroupBy(), null, _.ID.Count() & _.ProductId);
            return list.ToDictionary(e => e.ProductId, e => e.ID);
        }
        #endregion

        #region 扩展操作
        /// <summary>获取 或 添加</summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public static DeviceOnline GetOrAdd(String sessionId) => GetOrAdd(sessionId, FindBySessionID, k => new DeviceOnline { SessionID = k });
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