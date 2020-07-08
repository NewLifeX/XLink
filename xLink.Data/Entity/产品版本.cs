using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>产品版本。产品固件更新管理</summary>
    [Serializable]
    [DataObject]
    [Description("产品版本。产品固件更新管理")]
    [BindIndex("IU_ProductVersion_ProductId_Version", true, "ProductId,Version")]
    [BindTable("ProductVersion", Description = "产品版本。产品固件更新管理", ConnName = "xLink", DbType = DatabaseType.None)]
    public partial class ProductVersion : IProductVersion
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private Int32 _ProductId;
        /// <summary>产品</summary>
        [DisplayName("产品")]
        [Description("产品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ProductId", "产品", "")]
        public Int32 ProductId { get { return _ProductId; } set { if (OnPropertyChanging(__.ProductId, value)) { _ProductId = value; OnPropertyChanged(__.ProductId); } } }

        private String _Version;
        /// <summary>版本号</summary>
        [DisplayName("版本号")]
        [Description("版本号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Version", "版本号", "")]
        public String Version { get { return _Version; } set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } } }

        private Boolean _Enable;
        /// <summary>启用。启用/停用</summary>
        [DisplayName("启用")]
        [Description("启用。启用/停用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "启用。启用/停用", "")]
        public Boolean Enable { get { return _Enable; } set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } } }

        private Boolean _Force;
        /// <summary>强制。强制升级</summary>
        [DisplayName("强制")]
        [Description("强制。强制升级")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Force", "强制。强制升级", "")]
        public Boolean Force { get { return _Force; } set { if (OnPropertyChanging(__.Force, value)) { _Force = value; OnPropertyChanged(__.Force); } } }

        private ProductChannels _Channel;
        /// <summary>升级通道</summary>
        [DisplayName("升级通道")]
        [Description("升级通道")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Channel", "升级通道", "")]
        public ProductChannels Channel { get { return _Channel; } set { if (OnPropertyChanging(__.Channel, value)) { _Channel = value; OnPropertyChanged(__.Channel); } } }

        private String _Strategy;
        /// <summary>策略。升级策略</summary>
        [DisplayName("策略")]
        [Description("策略。升级策略")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Strategy", "策略。升级策略", "")]
        public String Strategy { get { return _Strategy; } set { if (OnPropertyChanging(__.Strategy, value)) { _Strategy = value; OnPropertyChanged(__.Strategy); } } }

        private String _Source;
        /// <summary>升级源</summary>
        [DisplayName("升级源")]
        [Description("升级源")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Source", "升级源", "")]
        public String Source { get { return _Source; } set { if (OnPropertyChanging(__.Source, value)) { _Source = value; OnPropertyChanged(__.Source); } } }

        private String _Executor;
        /// <summary>执行命令。空格前后为文件名和参数</summary>
        [DisplayName("执行命令")]
        [Description("执行命令。空格前后为文件名和参数")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Executor", "执行命令。空格前后为文件名和参数", "")]
        public String Executor { get { return _Executor; } set { if (OnPropertyChanging(__.Executor, value)) { _Executor = value; OnPropertyChanged(__.Executor); } } }

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
                    case __.ProductId : return _ProductId;
                    case __.Version : return _Version;
                    case __.Enable : return _Enable;
                    case __.Force : return _Force;
                    case __.Channel : return _Channel;
                    case __.Strategy : return _Strategy;
                    case __.Source : return _Source;
                    case __.Executor : return _Executor;
                    case __.CreateUser : return _CreateUser;
                    case __.CreateUserID : return _CreateUserID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.UpdateUser : return _UpdateUser;
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
                    case __.ProductId : _ProductId = value.ToInt(); break;
                    case __.Version : _Version = Convert.ToString(value); break;
                    case __.Enable : _Enable = value.ToBoolean(); break;
                    case __.Force : _Force = value.ToBoolean(); break;
                    case __.Channel : _Channel = (ProductChannels)value.ToInt(); break;
                    case __.Strategy : _Strategy = Convert.ToString(value); break;
                    case __.Source : _Source = Convert.ToString(value); break;
                    case __.Executor : _Executor = Convert.ToString(value); break;
                    case __.CreateUser : _CreateUser = Convert.ToString(value); break;
                    case __.CreateUserID : _CreateUserID = value.ToInt(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.UpdateUser : _UpdateUser = Convert.ToString(value); break;
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
        /// <summary>取得产品版本字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>产品</summary>
            public static readonly Field ProductId = FindByName(__.ProductId);

            /// <summary>版本号</summary>
            public static readonly Field Version = FindByName(__.Version);

            /// <summary>启用。启用/停用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            /// <summary>强制。强制升级</summary>
            public static readonly Field Force = FindByName(__.Force);

            /// <summary>升级通道</summary>
            public static readonly Field Channel = FindByName(__.Channel);

            /// <summary>策略。升级策略</summary>
            public static readonly Field Strategy = FindByName(__.Strategy);

            /// <summary>升级源</summary>
            public static readonly Field Source = FindByName(__.Source);

            /// <summary>执行命令。空格前后为文件名和参数</summary>
            public static readonly Field Executor = FindByName(__.Executor);

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

            /// <summary>描述</summary>
            public static readonly Field Description = FindByName(__.Description);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得产品版本字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>产品</summary>
            public const String ProductId = "ProductId";

            /// <summary>版本号</summary>
            public const String Version = "Version";

            /// <summary>启用。启用/停用</summary>
            public const String Enable = "Enable";

            /// <summary>强制。强制升级</summary>
            public const String Force = "Force";

            /// <summary>升级通道</summary>
            public const String Channel = "Channel";

            /// <summary>策略。升级策略</summary>
            public const String Strategy = "Strategy";

            /// <summary>升级源</summary>
            public const String Source = "Source";

            /// <summary>执行命令。空格前后为文件名和参数</summary>
            public const String Executor = "Executor";

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

            /// <summary>描述</summary>
            public const String Description = "Description";
        }
        #endregion
    }

    /// <summary>产品版本。产品固件更新管理接口</summary>
    public partial interface IProductVersion
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>产品</summary>
        Int32 ProductId { get; set; }

        /// <summary>版本号</summary>
        String Version { get; set; }

        /// <summary>启用。启用/停用</summary>
        Boolean Enable { get; set; }

        /// <summary>强制。强制升级</summary>
        Boolean Force { get; set; }

        /// <summary>升级通道</summary>
        ProductChannels Channel { get; set; }

        /// <summary>策略。升级策略</summary>
        String Strategy { get; set; }

        /// <summary>升级源</summary>
        String Source { get; set; }

        /// <summary>执行命令。空格前后为文件名和参数</summary>
        String Executor { get; set; }

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