using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>子设备</summary>
    [Serializable]
    [DataObject]
    [Description("子设备")]
    [BindIndex("IU_SubDevice_Code", true, "Code")]
    [BindIndex("IX_SubDevice_DeviceId", false, "DeviceId")]
    [BindIndex("IX_SubDevice_Vendor_Model", false, "Vendor,Model")]
    [BindIndex("IX_SubDevice_UpdateTime", false, "UpdateTime")]
    [BindTable("SubDevice", Description = "子设备", ConnName = "xLink", DbType = DatabaseType.None)]
    public partial class SubDevice : ISubDevice
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

        private Int32 _DeviceId;
        /// <summary>设备</summary>
        [DisplayName("设备")]
        [Description("设备")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeviceId", "设备", "")]
        public Int32 DeviceId { get { return _DeviceId; } set { if (OnPropertyChanging(__.DeviceId, value)) { _DeviceId = value; OnPropertyChanged(__.DeviceId); } } }

        private String _Code;
        /// <summary>编码</summary>
        [DisplayName("编码")]
        [Description("编码")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Code", "编码", "")]
        public String Code { get { return _Code; } set { if (OnPropertyChanging(__.Code, value)) { _Code = value; OnPropertyChanged(__.Code); } } }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private String _Version;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Version", "版本", "")]
        public String Version { get { return _Version; } set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } } }

        private String _Vendor;
        /// <summary>经销商</summary>
        [DisplayName("经销商")]
        [Description("经销商")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Vendor", "经销商", "")]
        public String Vendor { get { return _Vendor; } set { if (OnPropertyChanging(__.Vendor, value)) { _Vendor = value; OnPropertyChanged(__.Vendor); } } }

        private String _Model;
        /// <summary>产品型号</summary>
        [DisplayName("产品型号")]
        [Description("产品型号")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("Model", "产品型号", "")]
        public String Model { get { return _Model; } set { if (OnPropertyChanging(__.Model, value)) { _Model = value; OnPropertyChanged(__.Model); } } }

        private Boolean _Enable;
        /// <summary>启用。启用/停用</summary>
        [DisplayName("启用")]
        [Description("启用。启用/停用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "启用。启用/停用", "")]
        public Boolean Enable { get { return _Enable; } set { if (OnPropertyChanging(__.Enable, value)) { _Enable = value; OnPropertyChanged(__.Enable); } } }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Remark", "备注", "")]
        public String Remark { get { return _Remark; } set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } } }

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
                    case __.ProductId : return _ProductId;
                    case __.DeviceId : return _DeviceId;
                    case __.Code : return _Code;
                    case __.Name : return _Name;
                    case __.Version : return _Version;
                    case __.Vendor : return _Vendor;
                    case __.Model : return _Model;
                    case __.Enable : return _Enable;
                    case __.Remark : return _Remark;
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
                    case __.ProductId : _ProductId = value.ToInt(); break;
                    case __.DeviceId : _DeviceId = value.ToInt(); break;
                    case __.Code : _Code = Convert.ToString(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToString(value); break;
                    case __.Vendor : _Vendor = Convert.ToString(value); break;
                    case __.Model : _Model = Convert.ToString(value); break;
                    case __.Enable : _Enable = value.ToBoolean(); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
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
        /// <summary>取得子设备字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>产品</summary>
            public static readonly Field ProductId = FindByName(__.ProductId);

            /// <summary>设备</summary>
            public static readonly Field DeviceId = FindByName(__.DeviceId);

            /// <summary>编码</summary>
            public static readonly Field Code = FindByName(__.Code);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            /// <summary>经销商</summary>
            public static readonly Field Vendor = FindByName(__.Vendor);

            /// <summary>产品型号</summary>
            public static readonly Field Model = FindByName(__.Model);

            /// <summary>启用。启用/停用</summary>
            public static readonly Field Enable = FindByName(__.Enable);

            /// <summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

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

        /// <summary>取得子设备字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>产品</summary>
            public const String ProductId = "ProductId";

            /// <summary>设备</summary>
            public const String DeviceId = "DeviceId";

            /// <summary>编码</summary>
            public const String Code = "Code";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>版本</summary>
            public const String Version = "Version";

            /// <summary>经销商</summary>
            public const String Vendor = "Vendor";

            /// <summary>产品型号</summary>
            public const String Model = "Model";

            /// <summary>启用。启用/停用</summary>
            public const String Enable = "Enable";

            /// <summary>备注</summary>
            public const String Remark = "Remark";

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

    /// <summary>子设备接口</summary>
    public partial interface ISubDevice
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>产品</summary>
        Int32 ProductId { get; set; }

        /// <summary>设备</summary>
        Int32 DeviceId { get; set; }

        /// <summary>编码</summary>
        String Code { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>经销商</summary>
        String Vendor { get; set; }

        /// <summary>产品型号</summary>
        String Model { get; set; }

        /// <summary>启用。启用/停用</summary>
        Boolean Enable { get; set; }

        /// <summary>备注</summary>
        String Remark { get; set; }

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