using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>设备历史</summary>
    [Serializable]
    [DataObject]
    [Description("设备历史")]
    [BindIndex("IX_DeviceHistory_DeviceID", false, "DeviceID")]
    [BindIndex("IX_DeviceHistory_Name", false, "Name")]
    [BindIndex("IX_DeviceHistory_Type", false, "Type")]
    [BindTable("DeviceHistory", Description = "设备历史", ConnName = "Device", DbType = DatabaseType.SqlServer)]
    public partial class DeviceHistory : IDeviceHistory
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private Int32 _DeviceID;
        /// <summary>编码</summary>
        [DisplayName("编码")]
        [Description("编码")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeviceID", "编码", "")]
        public Int32 DeviceID { get { return _DeviceID; } set { if (OnPropertyChanging(__.DeviceID, value)) { _DeviceID = value; OnPropertyChanged(__.DeviceID); } } }

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

        private String _Type;
        /// <summary>类型</summary>
        [DisplayName("类型")]
        [Description("类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Type", "类型", "")]
        public String Type { get { return _Type; } set { if (OnPropertyChanging(__.Type, value)) { _Type = value; OnPropertyChanged(__.Type); } } }

        private String _NetType;
        /// <summary>网络</summary>
        [DisplayName("网络")]
        [Description("网络")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NetType", "网络", "")]
        public String NetType { get { return _NetType; } set { if (OnPropertyChanging(__.NetType, value)) { _NetType = value; OnPropertyChanged(__.NetType); } } }

        private String _Action;
        /// <summary>操作</summary>
        [DisplayName("操作")]
        [Description("操作")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Action", "操作", "")]
        public String Action { get { return _Action; } set { if (OnPropertyChanging(__.Action, value)) { _Action = value; OnPropertyChanged(__.Action); } } }

        private Boolean _Success;
        /// <summary>成功</summary>
        [DisplayName("成功")]
        [Description("成功")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Success", "成功", "")]
        public Boolean Success { get { return _Success; } set { if (OnPropertyChanging(__.Success, value)) { _Success = value; OnPropertyChanged(__.Success); } } }

        private Int32 _CreateDeviceID;
        /// <summary>创建者</summary>
        [DisplayName("创建者")]
        [Description("创建者")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CreateDeviceID", "创建者", "")]
        public Int32 CreateDeviceID { get { return _CreateDeviceID; } set { if (OnPropertyChanging(__.CreateDeviceID, value)) { _CreateDeviceID = value; OnPropertyChanged(__.CreateDeviceID); } } }

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

        private String _Remark;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Content", "内容", "")]
        public String Remark { get { return _Remark; } set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } } }
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
                    case __.DeviceID : return _DeviceID;
                    case __.Name : return _Name;
                    case __.Version : return _Version;
                    case __.Type : return _Type;
                    case __.NetType : return _NetType;
                    case __.Action : return _Action;
                    case __.Success : return _Success;
                    case __.CreateDeviceID : return _CreateDeviceID;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.DeviceID : _DeviceID = value.ToInt(); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToString(value); break;
                    case __.Type : _Type = Convert.ToString(value); break;
                    case __.NetType : _NetType = Convert.ToString(value); break;
                    case __.Action : _Action = Convert.ToString(value); break;
                    case __.Success : _Success = value.ToBoolean(); break;
                    case __.CreateDeviceID : _CreateDeviceID = value.ToInt(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设备历史字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>编码</summary>
            public static readonly Field DeviceID = FindByName(__.DeviceID);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            /// <summary>类型</summary>
            public static readonly Field Type = FindByName(__.Type);

            /// <summary>网络</summary>
            public static readonly Field NetType = FindByName(__.NetType);

            /// <summary>操作</summary>
            public static readonly Field Action = FindByName(__.Action);

            /// <summary>成功</summary>
            public static readonly Field Success = FindByName(__.Success);

            /// <summary>创建者</summary>
            public static readonly Field CreateDeviceID = FindByName(__.CreateDeviceID);

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            /// <summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            /// <summary>内容</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得设备历史字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>编码</summary>
            public const String DeviceID = "DeviceID";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>版本</summary>
            public const String Version = "Version";

            /// <summary>类型</summary>
            public const String Type = "Type";

            /// <summary>网络</summary>
            public const String NetType = "NetType";

            /// <summary>操作</summary>
            public const String Action = "Action";

            /// <summary>成功</summary>
            public const String Success = "Success";

            /// <summary>创建者</summary>
            public const String CreateDeviceID = "CreateDeviceID";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            /// <summary>内容</summary>
            public const String Remark = "Remark";
        }
        #endregion
    }

    /// <summary>设备历史接口</summary>
    public partial interface IDeviceHistory
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>编码</summary>
        Int32 DeviceID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>类型</summary>
        String Type { get; set; }

        /// <summary>网络</summary>
        String NetType { get; set; }

        /// <summary>操作</summary>
        String Action { get; set; }

        /// <summary>成功</summary>
        Boolean Success { get; set; }

        /// <summary>创建者</summary>
        Int32 CreateDeviceID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>内容</summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}