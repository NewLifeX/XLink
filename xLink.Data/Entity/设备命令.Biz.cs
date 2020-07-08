using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using XCode;
using XCode.Membership;

namespace xLink.Entity
{
    /// <summary>设备命令</summary>
    public partial class DeviceCommand : Entity<DeviceCommand>
    {
        #region 对象操作
        static DeviceCommand()
        {
            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.DeviceId);

            // 过滤器 UserModule、TimeModule、IPModule
            Meta.Modules.Add<UserModule>();
            Meta.Modules.Add<TimeModule>();
            Meta.Modules.Add<IPModule>();
        }
        #endregion

        #region 扩展属性
        /// <summary>设备</summary>
        [XmlIgnore, IgnoreDataMember]
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceId));

        /// <summary>设备名</summary>
        [Map(__.DeviceId)]
        public String DeviceName => Device + "";

        /// <summary>城市</summary>
        [XmlIgnore, IgnoreDataMember]
        public Area City => Extends.Get(nameof(City), k => Area.FindByID(AreaId));

        /// <summary>城市名</summary>
        [Map(__.AreaId)]
        public String CityName => City + "";
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static DeviceCommand FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.ID == id);
        }

        /// <summary>根据设备、命令查找</summary>
        /// <param name="deviceid">设备</param>
        /// <param name="command">命令</param>
        /// <returns>实体列表</returns>
        public static IList<DeviceCommand> FindAllByDeviceIdAndCommand(Int32 deviceid, String command)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.DeviceId == deviceid && e.Command == command);

            return FindAll(_.DeviceId == deviceid & _.Command == command);
        }
        #endregion

        #region 高级查询
        public static IList<DeviceCommand> Search(Int32 areaId,  Int32 deviceId, String command, DateTime start, DateTime end, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (areaId > 0) exp &= _.AreaId == areaId;
            if (deviceId > 0) exp &= _.DeviceId == deviceId;
            if (!command.IsNullOrEmpty()) exp &= _.Command == command;

            exp &= _.UpdateTime.Between(start, end);

            if (!key.IsNullOrEmpty()) exp &= _.Command == key;

            return FindAll(exp, page);
        }
        #endregion

        #region 业务操作
        /// <summary>获取有效命令</summary>
        /// <param name="deviceId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<DeviceCommand> AcquireCommands(Int32 deviceId, Int32 count = 100)
        {
            var exp = new WhereExpression();
            if (deviceId >= 0) exp &= _.DeviceId == deviceId;
            exp &= _.Finished == false;

            // 先执行较新的命令
            //return FindAll(exp, _.ID.Asc(), null, 0, count);
            return FindAll(exp, null, null, 0, count);
        }

        /// <summary>创建命令</summary>
        /// <param name="dv"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DeviceCommand Add(Device dv, String command)
        {
            var cmd = new DeviceCommand
            {
                DeviceId = dv.ID,
                AreaId = dv.CityId,
                Command = command,
                CreateTime = DateTime.Now,
            };

            cmd.Insert();

            return cmd;
        }
        #endregion
    }
}