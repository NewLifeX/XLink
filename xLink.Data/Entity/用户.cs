using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>用户</summary>
    [Serializable]
    [DataObject]
    [Description("用户")]
    [BindIndex("IU_User_Name", true, "Name")]
    [BindIndex("IX_User_Type", false, "Type")]
    [BindTable("User", Description = "用户", ConnName = "xLink", DbType = DatabaseType.None)]
    public partial class User : IUser
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
        /// <summary>名称。登录用户名</summary>
        [DisplayName("名称")]
        [Description("名称。登录用户名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "名称。登录用户名", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private String _Password;
        /// <summary>密码</summary>
        [DisplayName("密码")]
        [Description("密码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Password", "密码", "")]
        public String Password { get { return _Password; } set { if (OnPropertyChanging(__.Password, value)) { _Password = value; OnPropertyChanged(__.Password); } } }

        private String _NickName;
        /// <summary>昵称</summary>
        [DisplayName("昵称")]
        [Description("昵称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NickName", "昵称", "", Master = true)]
        public String NickName { get { return _NickName; } set { if (OnPropertyChanging(__.NickName, value)) { _NickName = value; OnPropertyChanged(__.NickName); } } }

        private String _Location;
        /// <summary>位置</summary>
        [DisplayName("位置")]
        [Description("位置")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Location", "位置", "")]
        public String Location { get { return _Location; } set { if (OnPropertyChanging(__.Location, value)) { _Location = value; OnPropertyChanged(__.Location); } } }

        private Boolean _Enable;
        /// <summary>启用</summary>
        [DisplayName("启用")]
        [Description("启用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "启用", "")]
        public Boolean Enable { get { return _Enable; } set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } } }

        private String _Type;
        /// <summary>类型</summary>
        [DisplayName("类型")]
        [Description("类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Type", "类型", "")]
        public String Type { get { return _Type; } set { if (OnPropertyChanging(__.Type, value)) { _Type = value; OnPropertyChanged(__.Type); } } }

        private String _Version;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Version", "版本", "")]
        public String Version { get { return _Version; } set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } } }

        private String _InternalUri;
        /// <summary>内网</summary>
        [DisplayName("内网")]
        [Description("内网")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("InternalUri", "内网", "")]
        public String InternalUri { get { return _InternalUri; } set { if (OnPropertyChanging(__.InternalUri, value)) { _InternalUri = value; OnPropertyChanged(__.InternalUri); } } }

        private String _ExternalUri;
        /// <summary>外网</summary>
        [DisplayName("外网")]
        [Description("外网")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("ExternalUri", "外网", "")]
        public String ExternalUri { get { return _ExternalUri; } set { if (OnPropertyChanging(__.ExternalUri, value)) { _ExternalUri = value; OnPropertyChanged(__.ExternalUri); } } }

        private Boolean _Online;
        /// <summary>在线</summary>
        [DisplayName("在线")]
        [Description("在线")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Online", "在线", "")]
        public Boolean Online { get { return _Online; } set { if (OnPropertyChanging(__.Online, value)) { _Online = value; OnPropertyChanged(__.Online); } } }

        private Int32 _Logins;
        /// <summary>登录</summary>
        [DisplayName("登录")]
        [Description("登录")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Logins", "登录", "")]
        public Int32 Logins { get { return _Logins; } set { if (OnPropertyChanging(__.Logins, value)) { _Logins = value; OnPropertyChanged(__.Logins); } } }

        private DateTime _LastLogin;
        /// <summary>最后登录</summary>
        [DisplayName("最后登录")]
        [Description("最后登录")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LastLogin", "最后登录", "")]
        public DateTime LastLogin { get { return _LastLogin; } set { if (OnPropertyChanging(__.LastLogin, value)) { _LastLogin = value; OnPropertyChanged(__.LastLogin); } } }

        private String _LastLoginIP;
        /// <summary>最后登录IP</summary>
        [DisplayName("最后登录IP")]
        [Description("最后登录IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("LastLoginIP", "最后登录IP", "")]
        public String LastLoginIP { get { return _LastLoginIP; } set { if (OnPropertyChanging(__.LastLoginIP, value)) { _LastLoginIP = value; OnPropertyChanged(__.LastLoginIP); } } }

        private Int32 _Registers;
        /// <summary>注册次数</summary>
        [DisplayName("注册次数")]
        [Description("注册次数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Registers", "注册次数", "")]
        public Int32 Registers { get { return _Registers; } set { if (OnPropertyChanging(__.Registers, value)) { _Registers = value; OnPropertyChanged(__.Registers); } } }

        private DateTime _RegisterTime;
        /// <summary>注册时间</summary>
        [DisplayName("注册时间")]
        [Description("注册时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("RegisterTime", "注册时间", "")]
        public DateTime RegisterTime { get { return _RegisterTime; } set { if (OnPropertyChanging(__.RegisterTime, value)) { _RegisterTime = value; OnPropertyChanged(__.RegisterTime); } } }

        private String _RegisterIP;
        /// <summary>注册IP</summary>
        [DisplayName("注册IP")]
        [Description("注册IP")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RegisterIP", "注册IP", "")]
        public String RegisterIP { get { return _RegisterIP; } set { if (OnPropertyChanging(__.RegisterIP, value)) { _RegisterIP = value; OnPropertyChanged(__.RegisterIP); } } }

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
                    case __.Password : return _Password;
                    case __.NickName : return _NickName;
                    case __.Location : return _Location;
                    case __.Enable : return _Enable;
                    case __.Type : return _Type;
                    case __.Version : return _Version;
                    case __.InternalUri : return _InternalUri;
                    case __.ExternalUri : return _ExternalUri;
                    case __.Online : return _Online;
                    case __.Logins : return _Logins;
                    case __.LastLogin : return _LastLogin;
                    case __.LastLoginIP : return _LastLoginIP;
                    case __.Registers : return _Registers;
                    case __.RegisterTime : return _RegisterTime;
                    case __.RegisterIP : return _RegisterIP;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
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
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Password : _Password = Convert.ToString(value); break;
                    case __.NickName : _NickName = Convert.ToString(value); break;
                    case __.Location : _Location = Convert.ToString(value); break;
                    case __.Enable : _Enable = value.ToBoolean(); break;
                    case __.Type : _Type = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToString(value); break;
                    case __.InternalUri : _InternalUri = Convert.ToString(value); break;
                    case __.ExternalUri : _ExternalUri = Convert.ToString(value); break;
                    case __.Online : _Online = value.ToBoolean(); break;
                    case __.Logins : _Logins = value.ToInt(); break;
                    case __.LastLogin : _LastLogin = value.ToDateTime(); break;
                    case __.LastLoginIP : _LastLoginIP = Convert.ToString(value); break;
                    case __.Registers : _Registers = value.ToInt(); break;
                    case __.RegisterTime : _RegisterTime = value.ToDateTime(); break;
                    case __.RegisterIP : _RegisterIP = Convert.ToString(value); break;
                    case __.CreateUserID : _CreateUserID = value.ToInt(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateUserID : _UpdateUserID = value.ToInt(); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
                    case __.UpdateIP : _UpdateIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得用户字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>名称。登录用户名</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>密码</summary>
            public static readonly Field Password = FindByName(__.Password);

            /// <summary>昵称</summary>
            public static readonly Field NickName = FindByName(__.NickName);

            /// <summary>位置</summary>
            public static readonly Field Location = FindByName(__.Location);

            /// <summary>启用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            /// <summary>类型</summary>
            public static readonly Field Type = FindByName(__.Type);

            /// <summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            /// <summary>内网</summary>
            public static readonly Field InternalUri = FindByName(__.InternalUri);

            /// <summary>外网</summary>
            public static readonly Field ExternalUri = FindByName(__.ExternalUri);

            /// <summary>在线</summary>
            public static readonly Field Online = FindByName(__.Online);

            /// <summary>登录</summary>
            public static readonly Field Logins = FindByName(__.Logins);

            /// <summary>最后登录</summary>
            public static readonly Field LastLogin = FindByName(__.LastLogin);

            /// <summary>最后登录IP</summary>
            public static readonly Field LastLoginIP = FindByName(__.LastLoginIP);

            /// <summary>注册次数</summary>
            public static readonly Field Registers = FindByName(__.Registers);

            /// <summary>注册时间</summary>
            public static readonly Field RegisterTime = FindByName(__.RegisterTime);

            /// <summary>注册IP</summary>
            public static readonly Field RegisterIP = FindByName(__.RegisterIP);

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

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得用户字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>名称。登录用户名</summary>
            public const String Name = "Name";

            /// <summary>密码</summary>
            public const String Password = "Password";

            /// <summary>昵称</summary>
            public const String NickName = "NickName";

            /// <summary>位置</summary>
            public const String Location = "Location";

            /// <summary>启用</summary>
            public const String Enable = "Enable";

            /// <summary>类型</summary>
            public const String Type = "Type";

            /// <summary>版本</summary>
            public const String Version = "Version";

            /// <summary>内网</summary>
            public const String InternalUri = "InternalUri";

            /// <summary>外网</summary>
            public const String ExternalUri = "ExternalUri";

            /// <summary>在线</summary>
            public const String Online = "Online";

            /// <summary>登录</summary>
            public const String Logins = "Logins";

            /// <summary>最后登录</summary>
            public const String LastLogin = "LastLogin";

            /// <summary>最后登录IP</summary>
            public const String LastLoginIP = "LastLoginIP";

            /// <summary>注册次数</summary>
            public const String Registers = "Registers";

            /// <summary>注册时间</summary>
            public const String RegisterTime = "RegisterTime";

            /// <summary>注册IP</summary>
            public const String RegisterIP = "RegisterIP";

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
        }
        #endregion
    }

    /// <summary>用户接口</summary>
    public partial interface IUser
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称。登录用户名</summary>
        String Name { get; set; }

        /// <summary>密码</summary>
        String Password { get; set; }

        /// <summary>昵称</summary>
        String NickName { get; set; }

        /// <summary>位置</summary>
        String Location { get; set; }

        /// <summary>启用</summary>
        Boolean Enable { get; set; }

        /// <summary>类型</summary>
        String Type { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>内网</summary>
        String InternalUri { get; set; }

        /// <summary>外网</summary>
        String ExternalUri { get; set; }

        /// <summary>在线</summary>
        Boolean Online { get; set; }

        /// <summary>登录</summary>
        Int32 Logins { get; set; }

        /// <summary>最后登录</summary>
        DateTime LastLogin { get; set; }

        /// <summary>最后登录IP</summary>
        String LastLoginIP { get; set; }

        /// <summary>注册次数</summary>
        Int32 Registers { get; set; }

        /// <summary>注册时间</summary>
        DateTime RegisterTime { get; set; }

        /// <summary>注册IP</summary>
        String RegisterIP { get; set; }

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
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}