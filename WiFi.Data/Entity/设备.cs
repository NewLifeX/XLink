using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace WiFi.Entity
{
    /// <summary>设备</summary>
    [Serializable]
    [DataObject]
    [Description("设备")]
    [BindIndex("IU_Device_Code", true, "Code")]
    [BindTable("Device", Description = "设备", ConnName = "WiFi", DbType = DatabaseType.None)]
    public partial class Device : IDevice
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private String _Code;
        /// <summary>编码。MAC</summary>
        [DisplayName("编码")]
        [Description("编码。MAC")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "编码。MAC", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private DeviceKinds _Kind;
        /// <summary>类型。1设备，2路由，3主机</summary>
        [DisplayName("类型")]
        [Description("类型。1设备，2路由，3主机")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Kind", "类型。1设备，2路由，3主机", "")]
        public DeviceKinds Kind { get { return _Kind; } set { if (OnPropertyChanging(__.Kind, value)) { _Kind = value; OnPropertyChanged(__.Kind); } } }

        private Boolean _Enable;
        /// <summary>启用</summary>
        [DisplayName("启用")]
        [Description("启用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "启用", "")]
        public Boolean Enable { get { return _Enable; } set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } } }

        private Int32 _Logins;
        /// <summary>登录次数</summary>
        [DisplayName("登录次数")]
        [Description("登录次数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Logins", "登录次数", "")]
        public Int32 Logins { get { return _Logins; } set { if (OnPropertyChanging(__.Logins, value)) { _Logins = value; OnPropertyChanged(__.Logins); } } }

        private DateTime _LastLogin;
        /// <summary>最后登录</summary>
        [DisplayName("最后登录")]
        [Description("最后登录")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastLogin", "最后登录", "")]
        public DateTime LastLogin { get { return _LastLogin; } set { if (OnPropertyChanging(__.LastLogin, value)) { _LastLogin = value; OnPropertyChanged(__.LastLogin); } } }

        private String _LastLoginIP;
        /// <summary>最后IP。最后的公网IP地址</summary>
        [DisplayName("最后IP")]
        [Description("最后IP。最后的公网IP地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("LastLoginIP", "最后IP。最后的公网IP地址", "")]
        public String LastLoginIP { get { return _LastLoginIP; } set { if (OnPropertyChanging(__.LastLoginIP, value)) { _LastLoginIP = value; OnPropertyChanged(__.LastLoginIP); } } }

        private Int32 _LastHostID;
        /// <summary>最后主机</summary>
        [DisplayName("最后主机")]
        [Description("最后主机")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastHostID", "最后主机", "")]
        public Int32 LastHostID { get { return _LastHostID; } set { if (OnPropertyChanging(__.LastHostID, value)) { _LastHostID = value; OnPropertyChanged(__.LastHostID); } } }

        private Int32 _LastRouteID;
        /// <summary>最后路由</summary>
        [DisplayName("最后路由")]
        [Description("最后路由")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastRouteID", "最后路由", "")]
        public Int32 LastRouteID { get { return _LastRouteID; } set { if (OnPropertyChanging(__.LastRouteID, value)) { _LastRouteID = value; OnPropertyChanged(__.LastRouteID); } } }

        private Int32 _LastRSSI;
        /// <summary>信号强度</summary>
        [DisplayName("信号强度")]
        [Description("信号强度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastRSSI", "信号强度", "")]
        public Int32 LastRSSI { get { return _LastRSSI; } set { if (OnPropertyChanging(__.LastRSSI, value)) { _LastRSSI = value; OnPropertyChanged(__.LastRSSI); } } }

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

        private String _Description;
        /// <summary>描述</summary>
        [DisplayName("描述")]
        [Description("描述")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Description", "描述", "")]
        public String Description { get { return _Description; } set { if (OnPropertyChanging(__.Description, value)) { _Description = value; OnPropertyChanged(__.Description); } } }
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
                    case __.Name : return _Name;
                    case __.Code : return _Code;
                    case __.Kind : return _Kind;
                    case __.Enable : return _Enable;
                    case __.Logins : return _Logins;
                    case __.LastLogin : return _LastLogin;
                    case __.LastLoginIP : return _LastLoginIP;
                    case __.LastHostID : return _LastHostID;
                    case __.LastRouteID : return _LastRouteID;
                    case __.LastRSSI : return _LastRSSI;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.UpdateUserID : return _UpdateUserID;
                    case __.UpdateTime : return _UpdateTime;
                    case __.UpdateIP : return _UpdateIP;
                    case __.Description : return _Description;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Kind : _Kind = (DeviceKinds)value.ToInt(); break;
                    case __.Enable : _Enable = value.ToBoolean(); break;
                    case __.Logins : _Logins = value.ToInt(); break;
                    case __.LastLogin : _LastLogin = value.ToDateTime(); break;
                    case __.LastLoginIP : _LastLoginIP = Convert.ToString(value); break;
                    case __.LastHostID : _LastHostID = value.ToInt(); break;
                    case __.LastRouteID : _LastRouteID = value.ToInt(); break;
                    case __.LastRSSI : _LastRSSI = value.ToInt(); break;
                    case __.CreateUserID : _CreateUserID = value.ToInt(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateUserID : _UpdateUserID = value.ToInt(); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
                    case __.UpdateIP : _UpdateIP = Convert.ToString(value); break;
                    case __.Description : _Description = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设备字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>编码。MAC</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>类型。1设备，2路由，3主机</summary>
            public static readonly Field Kind = FindByName(__.Kind);

            /// <summary>启用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            /// <summary>登录次数</summary>
            public static readonly Field Logins = FindByName(__.Logins);

            /// <summary>最后登录</summary>
            public static readonly Field LastLogin = FindByName(__.LastLogin);

            /// <summary>最后IP。最后的公网IP地址</summary>
            public static readonly Field LastLoginIP = FindByName(__.LastLoginIP);

            /// <summary>最后主机</summary>
            public static readonly Field LastHostID = FindByName(__.LastHostID);

            /// <summary>最后路由</summary>
            public static readonly Field LastRouteID = FindByName(__.LastRouteID);

            /// <summary>信号强度</summary>
            public static readonly Field LastRSSI = FindByName(__.LastRSSI);

            /// <summary>创建者</summary>
            public static readonly Field CreateUserID = FindByName(__.CreateUserID);

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            /// <summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            /// <summary>更新者</summary>
            public static readonly Field UpdateUserID = FindByName(__.UpdateUserID);

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            /// <summary>更新地址</summary>
            public static readonly Field UpdateIP = FindByName(__.UpdateIP);

            /// <summary>描述</summary>
            public static readonly Field Description = FindByName(__.Description);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得设备字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>编码。MAC</summary>
            public const String Code = "Code";

            /// <summary>类型。1设备，2路由，3主机</summary>
            public const String Kind = "Kind";

            /// <summary>启用</summary>
            public const String Enable = "Enable";

            /// <summary>登录次数</summary>
            public const String Logins = "Logins";

            /// <summary>最后登录</summary>
            public const String LastLogin = "LastLogin";

            /// <summary>最后IP。最后的公网IP地址</summary>
            public const String LastLoginIP = "LastLoginIP";

            /// <summary>最后主机</summary>
            public const String LastHostID = "LastHostID";

            /// <summary>最后路由</summary>
            public const String LastRouteID = "LastRouteID";

            /// <summary>信号强度</summary>
            public const String LastRSSI = "LastRSSI";

            /// <summary>创建者</summary>
            public const String CreateUserID = "CreateUserID";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            /// <summary>更新者</summary>
            public const String UpdateUserID = "UpdateUserID";

            /// <summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            /// <summary>更新地址</summary>
            public const String UpdateIP = "UpdateIP";

            /// <summary>描述</summary>
            public const String Description = "Description";
        }
        #endregion
    }

    /// <summary>设备接口</summary>
    public partial interface IDevice
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>编码。MAC</summary>
        String Code { get; set; }

        /// <summary>类型。1设备，2路由，3主机</summary>
        DeviceKinds Kind { get; set; }

        /// <summary>启用</summary>
        Boolean Enable { get; set; }

        /// <summary>登录次数</summary>
        Int32 Logins { get; set; }

        /// <summary>最后登录</summary>
        DateTime LastLogin { get; set; }

        /// <summary>最后IP。最后的公网IP地址</summary>
        String LastLoginIP { get; set; }

        /// <summary>最后主机</summary>
        Int32 LastHostID { get; set; }

        /// <summary>最后路由</summary>
        Int32 LastRouteID { get; set; }

        /// <summary>信号强度</summary>
        Int32 LastRSSI { get; set; }

        /// <summary>创建者</summary>
        Int32 CreateUserID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>更新者</summary>
        Int32 UpdateUserID { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>更新地址</summary>
        String UpdateIP { get; set; }

        /// <summary>描述</summary>
        String Description { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}