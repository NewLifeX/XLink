using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>产品</summary>
    [Serializable]
    [DataObject]
    [Description("产品")]
    [BindIndex("IU_Product_Code", true, "Code")]
    [BindIndex("IX_Product_Kind", false, "Kind")]
    [BindTable("Product", Description = "产品", ConnName = "xLink", DbType = DatabaseType.None)]
    public partial class Product : IProduct
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
        /// <summary>编码。ProductKey</summary>
        [DisplayName("编码")]
        [Description("编码。ProductKey")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Code", "编码。ProductKey", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private String _Secret;
        /// <summary>密钥。ProductSecret</summary>
        [DisplayName("密钥")]
        [Description("密钥。ProductSecret")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Secret", "密钥。ProductSecret", "")]
        public String Secret { get { return _Secret; } set { if (OnPropertyChanging(__.Secret, value)) { _Secret = value; OnPropertyChanged(__.Secret); } } }

        private String _Kind;
        /// <summary>节点类型。设备/网关</summary>
        [DisplayName("节点类型")]
        [Description("节点类型。设备/网关")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Kind", "节点类型。设备/网关", "")]
        public String Kind { get { return _Kind; } set { if (OnPropertyChanging(__.Kind, value)) { _Kind = value; OnPropertyChanged(__.Kind); } } }

        private String _Category;
        /// <summary>分类。边缘网关/大气监测设备</summary>
        [DisplayName("分类")]
        [Description("分类。边缘网关/大气监测设备")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Category", "分类。边缘网关/大气监测设备", "")]
        public String Category { get { return _Category; } set { if (OnPropertyChanging(__.Category, value)) { _Category = value; OnPropertyChanged(__.Category); } } }

        private String _DataFormat;
        /// <summary>数据格式。Json</summary>
        [DisplayName("数据格式")]
        [Description("数据格式。Json")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DataFormat", "数据格式。Json", "")]
        public String DataFormat { get { return _DataFormat; } set { if (OnPropertyChanging(__.DataFormat, value)) { _DataFormat = value; OnPropertyChanged(__.DataFormat); } } }

        private String _NetworkProtocol;
        /// <summary>网络协议。WiFi/蜂窝（2G/3G/4G/5G）/以太网/LoRaWAN/其它</summary>
        [DisplayName("网络协议")]
        [Description("网络协议。WiFi/蜂窝（2G/3G/4G/5G）/以太网/LoRaWAN/其它")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NetworkProtocol", "网络协议。WiFi/蜂窝（2G/3G/4G/5G）/以太网/LoRaWAN/其它", "")]
        public String NetworkProtocol { get { return _NetworkProtocol; } set { if (OnPropertyChanging(__.NetworkProtocol, value)) { _NetworkProtocol = value; OnPropertyChanged(__.NetworkProtocol); } } }

        private Boolean _Enable;
        /// <summary>启用。开发中/已发布</summary>
        [DisplayName("启用")]
        [Description("启用。开发中/已发布")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "启用。开发中/已发布", "")]
        public Boolean Enable { get { return _Enable; } set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } } }

        private Boolean _AutoRegister;
        /// <summary>动态注册。每台设备烧录相同的产品证书，即ProductKey和ProductSecret，云端鉴权通过后下发设备证书</summary>
        [DisplayName("动态注册")]
        [Description("动态注册。每台设备烧录相同的产品证书，即ProductKey和ProductSecret，云端鉴权通过后下发设备证书")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AutoRegister", "动态注册。每台设备烧录相同的产品证书，即ProductKey和ProductSecret，云端鉴权通过后下发设备证书", "")]
        public Boolean AutoRegister { get { return _AutoRegister; } set { if (OnPropertyChanging(__.AutoRegister, value)) { _AutoRegister = value; OnPropertyChanged(__.AutoRegister); } } }

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
                    case __.Name : return _Name;
                    case __.Code : return _Code;
                    case __.Secret : return _Secret;
                    case __.Kind : return _Kind;
                    case __.Category : return _Category;
                    case __.DataFormat : return _DataFormat;
                    case __.NetworkProtocol : return _NetworkProtocol;
                    case __.Enable : return _Enable;
                    case __.AutoRegister : return _AutoRegister;
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
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Secret : _Secret = Convert.ToString(value); break;
                    case __.Kind : _Kind = Convert.ToString(value); break;
                    case __.Category : _Category = Convert.ToString(value); break;
                    case __.DataFormat : _DataFormat = Convert.ToString(value); break;
                    case __.NetworkProtocol : _NetworkProtocol = Convert.ToString(value); break;
                    case __.Enable : _Enable = value.ToBoolean(); break;
                    case __.AutoRegister : _AutoRegister = value.ToBoolean(); break;
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
        /// <summary>取得产品字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>编码。ProductKey</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>密钥。ProductSecret</summary>
            public static readonly Field Secret = FindByName(__.Secret);

            /// <summary>节点类型。设备/网关</summary>
            public static readonly Field Kind = FindByName(__.Kind);

            /// <summary>分类。边缘网关/大气监测设备</summary>
            public static readonly Field Category = FindByName(__.Category);

            /// <summary>数据格式。Json</summary>
            public static readonly Field DataFormat = FindByName(__.DataFormat);

            /// <summary>网络协议。WiFi/蜂窝（2G/3G/4G/5G）/以太网/LoRaWAN/其它</summary>
            public static readonly Field NetworkProtocol = FindByName(__.NetworkProtocol);

            /// <summary>启用。开发中/已发布</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            /// <summary>动态注册。每台设备烧录相同的产品证书，即ProductKey和ProductSecret，云端鉴权通过后下发设备证书</summary>
            public static readonly Field AutoRegister = FindByName(__.AutoRegister);

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

        /// <summary>取得产品字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>编码。ProductKey</summary>
            public const String Code = "Code";

            /// <summary>密钥。ProductSecret</summary>
            public const String Secret = "Secret";

            /// <summary>节点类型。设备/网关</summary>
            public const String Kind = "Kind";

            /// <summary>分类。边缘网关/大气监测设备</summary>
            public const String Category = "Category";

            /// <summary>数据格式。Json</summary>
            public const String DataFormat = "DataFormat";

            /// <summary>网络协议。WiFi/蜂窝（2G/3G/4G/5G）/以太网/LoRaWAN/其它</summary>
            public const String NetworkProtocol = "NetworkProtocol";

            /// <summary>启用。开发中/已发布</summary>
            public const String Enable = "Enable";

            /// <summary>动态注册。每台设备烧录相同的产品证书，即ProductKey和ProductSecret，云端鉴权通过后下发设备证书</summary>
            public const String AutoRegister = "AutoRegister";

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

    /// <summary>产品接口</summary>
    public partial interface IProduct
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>编码。ProductKey</summary>
        String Code { get; set; }

        /// <summary>密钥。ProductSecret</summary>
        String Secret { get; set; }

        /// <summary>节点类型。设备/网关</summary>
        String Kind { get; set; }

        /// <summary>分类。边缘网关/大气监测设备</summary>
        String Category { get; set; }

        /// <summary>数据格式。Json</summary>
        String DataFormat { get; set; }

        /// <summary>网络协议。WiFi/蜂窝（2G/3G/4G/5G）/以太网/LoRaWAN/其它</summary>
        String NetworkProtocol { get; set; }

        /// <summary>启用。开发中/已发布</summary>
        Boolean Enable { get; set; }

        /// <summary>动态注册。每台设备烧录相同的产品证书，即ProductKey和ProductSecret，云端鉴权通过后下发设备证书</summary>
        Boolean AutoRegister { get; set; }

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