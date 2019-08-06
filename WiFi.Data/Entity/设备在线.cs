using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace WiFi.Entity
{
    /// <summary>设备在线</summary>
    [Serializable]
    [DataObject]
    [Description("设备在线")]
    [BindIndex("IU_DeviceOnline_SessionID", true, "SessionID")]
    [BindIndex("IX_DeviceOnline_DeviceID", false, "DeviceID")]
    [BindTable("DeviceOnline", Description = "设备在线", ConnName = "WiFi", DbType = DatabaseType.None)]
    public partial class DeviceOnline : IDeviceOnline
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _SessionID;
        /// <summary>会话</summary>
        [DisplayName("会话")]
        [Description("会话")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("SessionID", "会话", "")]
        public String SessionID { get { return _SessionID; } set { if (OnPropertyChanging(__.SessionID, value)) { _SessionID = value; OnPropertyChanged(__.SessionID); } } }

        private Int32 _DeviceID;
        /// <summary>设备</summary>
        [DisplayName("设备")]
        [Description("设备")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeviceID", "设备", "")]
        public Int32 DeviceID { get { return _DeviceID; } set { if (OnPropertyChanging(__.DeviceID, value)) { _DeviceID = value; OnPropertyChanged(__.DeviceID); } } }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private Int32 _HostID;
        /// <summary>主机</summary>
        [DisplayName("主机")]
        [Description("主机")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("HostID", "主机", "")]
        public Int32 HostID { get { return _HostID; } set { if (OnPropertyChanging(__.HostID, value)) { _HostID = value; OnPropertyChanged(__.HostID); } } }

        private Int32 _RouteID;
        /// <summary>路由</summary>
        [DisplayName("路由")]
        [Description("路由")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RouteID", "路由", "")]
        public Int32 RouteID { get { return _RouteID; } set { if (OnPropertyChanging(__.RouteID, value)) { _RouteID = value; OnPropertyChanged(__.RouteID); } } }

        private Int32 _Kind;
        /// <summary>类型。1路由，2设备</summary>
        [DisplayName("类型")]
        [Description("类型。1路由，2设备")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Kind", "类型。1路由，2设备", "")]
        public Int32 Kind { get { return _Kind; } set { if (OnPropertyChanging(__.Kind, value)) { _Kind = value; OnPropertyChanged(__.Kind); } } }

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

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "更新时间", "")]
        public DateTime UpdateTime { get { return _UpdateTime; } set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }
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
                    case __.SessionID : return _SessionID;
                    case __.DeviceID : return _DeviceID;
                    case __.Name : return _Name;
                    case __.HostID : return _HostID;
                    case __.RouteID : return _RouteID;
                    case __.Kind : return _Kind;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.UpdateTime : return _UpdateTime;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.SessionID : _SessionID = Convert.ToString(value); break;
                    case __.DeviceID : _DeviceID = value.ToInt(); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.HostID : _HostID = value.ToInt(); break;
                    case __.RouteID : _RouteID = value.ToInt(); break;
                    case __.Kind : _Kind = value.ToInt(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设备在线字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>会话</summary>
            public static readonly Field SessionID = FindByName(__.SessionID);

            /// <summary>设备</summary>
            public static readonly Field DeviceID = FindByName(__.DeviceID);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>主机</summary>
            public static readonly Field HostID = FindByName(__.HostID);

            /// <summary>路由</summary>
            public static readonly Field RouteID = FindByName(__.RouteID);

            /// <summary>类型。1路由，2设备</summary>
            public static readonly Field Kind = FindByName(__.Kind);

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            /// <summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得设备在线字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>会话</summary>
            public const String SessionID = "SessionID";

            /// <summary>设备</summary>
            public const String DeviceID = "DeviceID";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>主机</summary>
            public const String HostID = "HostID";

            /// <summary>路由</summary>
            public const String RouteID = "RouteID";

            /// <summary>类型。1路由，2设备</summary>
            public const String Kind = "Kind";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            /// <summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";
        }
        #endregion
    }

    /// <summary>设备在线接口</summary>
    public partial interface IDeviceOnline
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>会话</summary>
        String SessionID { get; set; }

        /// <summary>设备</summary>
        Int32 DeviceID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>主机</summary>
        Int32 HostID { get; set; }

        /// <summary>路由</summary>
        Int32 RouteID { get; set; }

        /// <summary>类型。1路由，2设备</summary>
        Int32 Kind { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}