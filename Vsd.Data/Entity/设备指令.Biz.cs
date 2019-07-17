using NewLife.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Membership;

namespace Vsd.Entity
{
    /// <summary>指令状态</summary>
    public enum CommandStatus
    {
        /// <summary>就绪</summary>
        就绪 = 0,

        /// <summary>已完成</summary>
        完成 = 1,

        /// <summary>错误</summary>
        错误 = 2,

        /// <summary>取消</summary>
        取消 = 3
    }

    /// <summary>设备指令</summary>
    public partial class DeviceCommand : Entity<DeviceCommand>
    {
        #region 对象操作
        static DeviceCommand()
        {
            // 累加字段
            //Meta.Factory.AdditionalFields.Add(__.Logins);

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
                if (isNew && !Dirtys[nameof(CreateUserID)) nameof(CreateUserID) = user.ID;
                if (!Dirtys[nameof(UpdateUserID)]) nameof(UpdateUserID) = user.ID;
            }*/
            //if (isNew && !Dirtys[nameof(CreateTime)]) nameof(CreateTime) = DateTime.Now;
            //if (!Dirtys[nameof(UpdateTime)]) nameof(UpdateTime) = DateTime.Now;
            //if (isNew && !Dirtys[nameof(CreateIP)]) nameof(CreateIP) = WebHelper.UserHost;
            //if (!Dirtys[nameof(UpdateIP)]) nameof(UpdateIP) = WebHelper.UserHost;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化DeviceCommand[设备指令]数据……");

        //    var entity = new DeviceCommand();
        //    entity.ID = 0;
        //    entity.DeviceID = 0;
        //    entity.Command = "abc";
        //    entity.Argument = "abc";
        //    entity.StartTime = DateTime.Now;
        //    entity.EndTime = DateTime.Now;
        //    entity.Finished = true;
        //    entity.FinishTime = DateTime.Now;
        //    entity.CreateUserID = 0;
        //    entity.CreateTime = DateTime.Now;
        //    entity.CreateIP = "abc";
        //    entity.UpdateUserID = 0;
        //    entity.UpdateTime = DateTime.Now;
        //    entity.UpdateIP = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化DeviceCommand[设备指令]数据！"
        //}

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性
        /// <summary>设备</summary>
        [XmlIgnore, ScriptIgnore]
        public Device Device => Extends.Get(nameof(Device), k => Device.FindByID(DeviceID));

        /// <summary>设备</summary>
        [Map(__.DeviceID)]
        public String DeviceName => Device + "";
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static DeviceCommand FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.FirstOrDefault(e => e.ID == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.ID == id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="command">名称</param>
        /// <returns>实体列表</returns>
        public static IList<DeviceCommand> FindByCommand(String command)
        {
            // 实体缓存
            if (Meta.Count < 1000) return Meta.Cache.Entities.Where(e => e.Command == command).ToList();

            return FindAll(_.Command == command);
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="deviceid"></param>
        /// <param name="cmd"></param>
        /// <param name="finished"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="key"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IList<DeviceCommand> Search(Int32 deviceid, String cmd, Boolean? finished, DateTime start, DateTime end, String key, PageParameter param)
        {
            var exp = SearchWhereByKeys(key, null, null);

            if (deviceid > 0) exp &= _.DeviceID == deviceid;
            if (!cmd.IsNullOrEmpty()) exp &= _.Command == cmd;
            if (finished != null) exp &= _.Status == (finished.Value ? CommandStatus.完成 : CommandStatus.就绪);

            exp &= _.UpdateTime.Between(start, end);

            return FindAll(exp, param);
        }
        #endregion

        #region 业务操作
        /// <summary>获取该设备未完成指令</summary>
        /// <param name="deviceid"></param>
        /// <param name="start"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IList<DeviceCommand> GetCommands(Int32 deviceid, Int32 start, Int32 max)
        {
            return FindAll(_.DeviceID == deviceid & _.Status == CommandStatus.就绪, _.ID.Asc(), null, start, max);
        }
        #endregion
    }
}