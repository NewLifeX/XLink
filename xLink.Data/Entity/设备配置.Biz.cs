using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>设备配置</summary>
    public partial class DeviceConfig : Entity<DeviceConfig>
    {
        #region 对象操作
        static DeviceConfig()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.DeviceId);

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

            // 在新插入数据或者修改了指定字段时进行修正
            // 处理当前已登录用户信息，可以由UserModule过滤器代劳
            /*var user = ManageProvider.User;
            if (user != null)
            {
                if (isNew && !Dirtys[nameof(CreateUserID)]) CreateUserID = user.ID;
                if (!Dirtys[nameof(UpdateUserID)]) UpdateUserID = user.ID;
            }*/
            //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
            //if (!Dirtys[nameof(UpdateTime)]) UpdateTime = DateTime.Now;
            //if (isNew && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
            //if (!Dirtys[nameof(UpdateIP)]) UpdateIP = ManageProvider.UserHost;

            // 检查唯一索引
            // CheckExist(isNew, __.DeviceId, __.Name);
        }
        #endregion

        #region 扩展属性
        /// <summary>设备</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceId));

        /// <summary>设备</summary>
        [Map(__.DeviceId, typeof(Device), "ID")]
        public String DeviceName => Device?.Name;

        /// <summary>城市</summary>
        public Area City => Extends.Get(nameof(City), k => Area.FindByID(AreaId));

        /// <summary>城市名</summary>
        [Map(__.AreaId)]
        public String CityName => City + "";
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static DeviceConfig FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据设备、名称查找</summary>
        /// <param name="deviceid">设备</param>
        /// <param name="name">名称</param>
        /// <returns>实体对象</returns>
        public static DeviceConfig FindByDeviceIdAndName(Int32 deviceid, String name)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.DeviceId == deviceid && e.Name == name);

            return Find(_.DeviceId == deviceid & _.Name == name);
        }
        #endregion

        #region 高级查询
        /// <summary>查询满足条件的记录集，分页、排序</summary>
        /// <param name="areaId">地区</param>
        /// <param name="deviceId">设备</param>
        /// <param name="name">名称</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页排序参数，同时返回满足条件的总记录数</param>
        /// <returns>实体集</returns>
        public static IList<DeviceConfig> Search(Int32 areaId, Int32 deviceId, String name, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (areaId > 0) exp &= _.AreaId == areaId;
            if (deviceId > 0) exp &= _.DeviceId == deviceId;
            if (!name.IsNullOrEmpty()) exp &= _.Name == name;

            exp &= _.UpdateTime.Between(start, end);

            if (!key.IsNullOrEmpty()) exp &= _.Name == key;

            return FindAll(exp, page);
        }
        #endregion

        #region 业务操作
        static Lazy<FieldCache<DeviceConfig>> NameCache = new Lazy<FieldCache<DeviceConfig>>(() => new FieldCache<DeviceConfig>(__.Name));
        /// <summary>获取所有分类名称</summary>
        /// <returns></returns>
        public static IDictionary<String, String> FindAllName() => NameCache.Value.FindAllName();
        #endregion
    }
}