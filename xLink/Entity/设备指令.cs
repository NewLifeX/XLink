using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>设备指令</summary>
    [Serializable]
    [DataObject]
    [Description("设备指令")]
    [BindIndex("IX_DeviceCommand_DeviceId_Command", false, "DeviceId,Command")]
    [BindIndex("IX_DeviceCommand_UpdateTime_AreaId", false, "UpdateTime,AreaId")]
    [BindTable("DeviceCommand", Description = "设备指令", ConnName = "xLink", DbType = DatabaseType.None)]
    public partial class DeviceCommand : IDeviceCommand
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private Int32 _DeviceId;
        /// <summary>设备</summary>
        [DisplayName("设备")]
        [Description("设备")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeviceId", "设备", "")]
        public Int32 DeviceId { get { return _DeviceId; } set { if (OnPropertyChanging(__.DeviceId, value)) { _DeviceId = value; OnPropertyChanged(__.DeviceId); } } }

        private Int32 _AreaId;
        /// <summary>地区</summary>
        [DisplayName("地区")]
        [Description("地区")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AreaId", "地区", "")]
        public Int32 AreaId { get { return _AreaId; } set { if (OnPropertyChanging(__.AreaId, value)) { _AreaId = value; OnPropertyChanged(__.AreaId); } } }

        private String _Command;
        /// <summary>命令</summary>
        [DisplayName("命令")]
        [Description("命令")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Command", "命令", "", Master = true)]
        public String Command { get { return _Command; } set { if (OnPropertyChanging(__.Command, value)) { _Command = value; OnPropertyChanged(__.Command); } } }

        private String _Argument;
        /// <summary>参数</summary>
        [DisplayName("参数")]
        [Description("参数")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Argument", "参数", "")]
        public String Argument { get { return _Argument; } set { if (OnPropertyChanging(__.Argument, value)) { _Argument = value; OnPropertyChanged(__.Argument); } } }

        private Boolean _Finished;
        /// <summary>完成。客户端是否已执行</summary>
        [DisplayName("完成")]
        [Description("完成。客户端是否已执行")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Finished", "完成。客户端是否已执行", "")]
        public Boolean Finished { get { return _Finished; } set { if (OnPropertyChanging(__.Finished, value)) { _Finished = value; OnPropertyChanged(__.Finished); } } }

        private String _Result;
        /// <summary>结果</summary>
        [DisplayName("结果")]
        [Description("结果")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Result", "结果", "")]
        public String Result { get { return _Result; } set { if (OnPropertyChanging(__.Result, value)) { _Result = value; OnPropertyChanged(__.Result); } } }

        private String _CreateUser;
        /// <summary>创建人</summary>
        [DisplayName("创建人")]
        [Description("创建人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CreateUser", "创建人", "")]
        public String CreateUser { get { return _CreateUser; } set { if (OnPropertyChanging(__.CreateUser, value)) { _CreateUser = value; OnPropertyChanged(__.CreateUser); } } }

        private Int32 _CreateUserID;
        /// <summary>创建者</summary>
        [DisplayName("创建者")]
        [Description("创建者")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CreateUserID", "创建者", "")]
        public Int32 CreateUserID { get { return _CreateUserID; } set { if (OnPropertyChanging(__.CreateUserID, value)) { _CreateUserID = value; OnPropertyChanged(__.CreateUserID); } } }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CreateTime", "创建时间", "")]
        public DateTime CreateTime { get { return _CreateTime; } set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } } }

        private String _CreateIP;
        /// <summary>创建地址</summary>
        [DisplayName("创建地址")]
        [Description("创建地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("CreateIP", "创建地址", "")]
        public String CreateIP { get { return _CreateIP; } set { if (OnPropertyChanging(__.CreateIP, value)) { _CreateIP = value; OnPropertyChanged(__.CreateIP); } } }

        private String _UpdateUser;
        /// <summary>更新人</summary>
        [DisplayName("更新人")]
        [Description("更新人")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UpdateUser", "更新人", "")]
        public String UpdateUser { get { return _UpdateUser; } set { if (OnPropertyChanging(__.UpdateUser, value)) { _UpdateUser = value; OnPropertyChanged(__.UpdateUser); } } }

        private Int32 _UpdateUserID;
        /// <summary>更新者</summary>
        [DisplayName("更新者")]
        [Description("更新者")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UpdateUserID", "更新者", "")]
        public Int32 UpdateUserID { get { return _UpdateUserID; } set { if (OnPropertyChanging(__.UpdateUserID, value)) { _UpdateUserID = value; OnPropertyChanged(__.UpdateUserID); } } }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "更新时间", "")]
        public DateTime UpdateTime { get { return _UpdateTime; } set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }

        private String _UpdateIP;
        /// <summary>更新地址</summary>
        [DisplayName("更新地址")]
        [Description("更新地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UpdateIP", "更新地址", "")]
        public String UpdateIP { get { return _UpdateIP; } set { if (OnPropertyChanging(__.UpdateIP, value)) { _UpdateIP = value; OnPropertyChanged(__.UpdateIP); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.DeviceId : return _DeviceId;
                    case __.AreaId : return _AreaId;
                    case __.Command : return _Command;
                    case __.Argument : return _Argument;
                    case __.Finished : return _Finished;
                    case __.Result : return _Result;
                    case __.CreateUser : return _CreateUser;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.UpdateUser : return _UpdateUser;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateTime : return _UpdateTime;
                    case __.UpdateIP : return _UpdateIP;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.DeviceId : _DeviceId = value.ToInt(); break;
                    case __.AreaId : _AreaId = value.ToInt(); break;
                    case __.Command : _Command = Convert.ToString(value); break;
                    case __.Argument : _Argument = Convert.ToString(value); break;
                    case __.Finished : _Finished = value.ToBoolean(); break;
                    case __.Result : _Result = Convert.ToString(value); break;
                    case __.CreateUser : _CreateUser = Convert.ToString(value); break;
                    case __.CreateUserID : _CreateUserID = value.ToInt(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateUser : _UpdateUser = Convert.ToString(value); break;
                    case __.UpdateUserID : _UpdateUserID = value.ToInt(); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
                    case __.UpdateIP : _UpdateIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设备指令字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>设备</summary>
            public static readonly Field DeviceId = FindByName(__.DeviceId);

            /// <summary>地区</summary>
            public static readonly Field AreaId = FindByName(__.AreaId);

            /// <summary>命令</summary>
            public static readonly Field Command = FindByName(__.Command);

            /// <summary>参数</summary>
            public static readonly Field Argument = FindByName(__.Argument);

            /// <summary>完成。客户端是否已执行</summary>
            public static readonly Field Finished = FindByName(__.Finished);

            /// <summary>结果</summary>
            public static readonly Field Result = FindByName(__.Result);

            /// <summary>创建人</summary>
            public static readonly Field CreateUser = FindByName(__.CreateUser);

            /// <summary>创建者</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            /// <summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            /// <summary>更新人</summary>
            public static readonly Field UpdateUser = FindByName(__.UpdateUser);

            /// <summary>更新者</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            /// <summary>更新地址</summary>
            public static readonly Field UpdateIP = FindByName(__.UpdateIP);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得设备指令字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>设备</summary>
            public const String DeviceId = "DeviceId";

            /// <summary>地区</summary>
            public const String AreaId = "AreaId";

            /// <summary>命令</summary>
            public const String Command = "Command";

            /// <summary>参数</summary>
            public const String Argument = "Argument";

            /// <summary>完成。客户端是否已执行</summary>
            public const String Finished = "Finished";

            /// <summary>结果</summary>
            public const String Result = "Result";

            /// <summary>创建人</summary>
            public const String CreateUser = "CreateUser";

            /// <summary>创建者</summary>
            public const String CreateUserID = "CreateUserID";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            /// <summary>更新人</summary>
            public const String UpdateUser = "UpdateUser";

            /// <summary>更新者</summary>
            public const String UpdateUserID = "UpdateUserID";

            /// <summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            /// <summary>更新地址</summary>
            public const String UpdateIP = "UpdateIP";
        }
        #endregion
    }

    /// <summary>设备指令接口</summary>
    public partial interface IDeviceCommand
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>设备</summary>
        Int32 DeviceId { get; set; }

        /// <summary>地区</summary>
        Int32 AreaId { get; set; }

        /// <summary>命令</summary>
        String Command { get; set; }

        /// <summary>参数</summary>
        String Argument { get; set; }

        /// <summary>完成。客户端是否已执行</summary>
        Boolean Finished { get; set; }

        /// <summary>结果</summary>
        String Result { get; set; }

        /// <summary>创建人</summary>
        String CreateUser { get; set; }

        /// <summary>创建者</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>更新人</summary>
        String UpdateUser { get; set; }

        /// <summary>更新者</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>更新地址</summary>
        String UpdateIP { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}