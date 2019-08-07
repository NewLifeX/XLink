using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace WiFi.Entity
{
    /// <summary>原始数据</summary>
    [Serializable]
    [DataObject]
    [Description("原始数据")]
    [BindIndex("IX_RawData_CreateTime", false, "CreateTime")]
    [BindIndex("IX_RawData_DeviceID_CreateTime", false, "DeviceID,CreateTime")]
    [BindTable("RawData", Description = "原始数据", ConnName = "WiFi", DbType = DatabaseType.None)]
    public partial class RawData : IRawData
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
        /// <summary>设备</summary>
        [DisplayName("设备")]
        [Description("设备")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DeviceID", "设备", "")]
        public Int32 DeviceID { get { return _DeviceID; } set { if (OnPropertyChanging(__.DeviceID, value)) { _DeviceID = value; OnPropertyChanged(__.DeviceID); } } }

        private String _DeviceMAC;
        /// <summary>设备MAC</summary>
        [DisplayName("设备MAC")]
        [Description("设备MAC")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DeviceMAC", "设备MAC", "")]
        public String DeviceMAC { get { return _DeviceMAC; } set { if (OnPropertyChanging(__.DeviceMAC, value)) { _DeviceMAC = value; OnPropertyChanged(__.DeviceMAC); } } }

        private String _RouteMAC;
        /// <summary>路由MAC</summary>
        [DisplayName("路由MAC")]
        [Description("路由MAC")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("RouteMAC", "路由MAC", "")]
        public String RouteMAC { get { return _RouteMAC; } set { if (OnPropertyChanging(__.RouteMAC, value)) { _RouteMAC = value; OnPropertyChanged(__.RouteMAC); } } }

        private String _HostMAC;
        /// <summary>主机MAC</summary>
        [DisplayName("主机MAC")]
        [Description("主机MAC")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("HostMAC", "主机MAC", "")]
        public String HostMAC { get { return _HostMAC; } set { if (OnPropertyChanging(__.HostMAC, value)) { _HostMAC = value; OnPropertyChanged(__.HostMAC); } } }

        private Int32 _FrameType;
        /// <summary>帧类型</summary>
        [DisplayName("帧类型")]
        [Description("帧类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("FrameType", "帧类型", "")]
        public Int32 FrameType { get { return _FrameType; } set { if (OnPropertyChanging(__.FrameType, value)) { _FrameType = value; OnPropertyChanged(__.FrameType); } } }

        private Int32 _FrameType2;
        /// <summary>子类型</summary>
        [DisplayName("子类型")]
        [Description("子类型")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("FrameType2", "子类型", "")]
        public Int32 FrameType2 { get { return _FrameType2; } set { if (OnPropertyChanging(__.FrameType2, value)) { _FrameType2 = value; OnPropertyChanged(__.FrameType2); } } }

        private Int32 _Channel;
        /// <summary>信道</summary>
        [DisplayName("信道")]
        [Description("信道")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Channel", "信道", "")]
        public Int32 Channel { get { return _Channel; } set { if (OnPropertyChanging(__.Channel, value)) { _Channel = value; OnPropertyChanged(__.Channel); } } }

        private Int32 _Rssi;
        /// <summary>信号强度</summary>
        [DisplayName("信号强度")]
        [Description("信号强度")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rssi", "信号强度", "")]
        public Int32 Rssi { get { return _Rssi; } set { if (OnPropertyChanging(__.Rssi, value)) { _Rssi = value; OnPropertyChanged(__.Rssi); } } }

        private Double _Distance;
        /// <summary>距离。设备到主机的距离，单位米</summary>
        [DisplayName("距离")]
        [Description("距离。设备到主机的距离，单位米")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Distance", "距离。设备到主机的距离，单位米", "")]
        public Double Distance { get { return _Distance; } set { if (OnPropertyChanging(__.Distance, value)) { _Distance = value; OnPropertyChanged(__.Distance); } } }

        private Boolean _PowerSave;
        /// <summary>省电。发射设备是否处于省电模式</summary>
        [DisplayName("省电")]
        [Description("省电。发射设备是否处于省电模式")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PowerSave", "省电。发射设备是否处于省电模式", "")]
        public Boolean PowerSave { get { return _PowerSave; } set { if (OnPropertyChanging(__.PowerSave, value)) { _PowerSave = value; OnPropertyChanged(__.PowerSave); } } }

        private Boolean _IsRoute;
        /// <summary>路由。是否由路由发出的信号</summary>
        [DisplayName("路由")]
        [Description("路由。是否由路由发出的信号")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRoute", "路由。是否由路由发出的信号", "")]
        public Boolean IsRoute { get { return _IsRoute; } set { if (OnPropertyChanging(__.IsRoute, value)) { _IsRoute = value; OnPropertyChanged(__.IsRoute); } } }

        private String _Remark;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Remark", "内容", "")]
        public String Remark { get { return _Remark; } set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } } }

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
                    case __.DeviceMAC : return _DeviceMAC;
                    case __.RouteMAC : return _RouteMAC;
                    case __.HostMAC : return _HostMAC;
                    case __.FrameType : return _FrameType;
                    case __.FrameType2 : return _FrameType2;
                    case __.Channel : return _Channel;
                    case __.Rssi : return _Rssi;
                    case __.Distance : return _Distance;
                    case __.PowerSave : return _PowerSave;
                    case __.IsRoute : return _IsRoute;
                    case __.Remark : return _Remark;
                    case __.CreateTime : return _CreateTime;
                    case __.CreateIP : return _CreateIP;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.DeviceID : _DeviceID = value.ToInt(); break;
                    case __.DeviceMAC : _DeviceMAC = Convert.ToString(value); break;
                    case __.RouteMAC : _RouteMAC = Convert.ToString(value); break;
                    case __.HostMAC : _HostMAC = Convert.ToString(value); break;
                    case __.FrameType : _FrameType = value.ToInt(); break;
                    case __.FrameType2 : _FrameType2 = value.ToInt(); break;
                    case __.Channel : _Channel = value.ToInt(); break;
                    case __.Rssi : _Rssi = value.ToInt(); break;
                    case __.Distance : _Distance = value.ToDouble(); break;
                    case __.PowerSave : _PowerSave = value.ToBoolean(); break;
                    case __.IsRoute : _IsRoute = value.ToBoolean(); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得原始数据字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>设备</summary>
            public static readonly Field DeviceID = FindByName(__.DeviceID);

            /// <summary>设备MAC</summary>
            public static readonly Field DeviceMAC = FindByName(__.DeviceMAC);

            /// <summary>路由MAC</summary>
            public static readonly Field RouteMAC = FindByName(__.RouteMAC);

            /// <summary>主机MAC</summary>
            public static readonly Field HostMAC = FindByName(__.HostMAC);

            /// <summary>帧类型</summary>
            public static readonly Field FrameType = FindByName(__.FrameType);

            /// <summary>子类型</summary>
            public static readonly Field FrameType2 = FindByName(__.FrameType2);

            /// <summary>信道</summary>
            public static readonly Field Channel = FindByName(__.Channel);

            /// <summary>信号强度</summary>
            public static readonly Field Rssi = FindByName(__.Rssi);

            /// <summary>距离。设备到主机的距离，单位米</summary>
            public static readonly Field Distance = FindByName(__.Distance);

            /// <summary>省电。发射设备是否处于省电模式</summary>
            public static readonly Field PowerSave = FindByName(__.PowerSave);

            /// <summary>路由。是否由路由发出的信号</summary>
            public static readonly Field IsRoute = FindByName(__.IsRoute);

            /// <summary>内容</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            /// <summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得原始数据字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>设备</summary>
            public const String DeviceID = "DeviceID";

            /// <summary>设备MAC</summary>
            public const String DeviceMAC = "DeviceMAC";

            /// <summary>路由MAC</summary>
            public const String RouteMAC = "RouteMAC";

            /// <summary>主机MAC</summary>
            public const String HostMAC = "HostMAC";

            /// <summary>帧类型</summary>
            public const String FrameType = "FrameType";

            /// <summary>子类型</summary>
            public const String FrameType2 = "FrameType2";

            /// <summary>信道</summary>
            public const String Channel = "Channel";

            /// <summary>信号强度</summary>
            public const String Rssi = "Rssi";

            /// <summary>距离。设备到主机的距离，单位米</summary>
            public const String Distance = "Distance";

            /// <summary>省电。发射设备是否处于省电模式</summary>
            public const String PowerSave = "PowerSave";

            /// <summary>路由。是否由路由发出的信号</summary>
            public const String IsRoute = "IsRoute";

            /// <summary>内容</summary>
            public const String Remark = "Remark";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>创建地址</summary>
            public const String CreateIP = "CreateIP";
        }
        #endregion
    }

    /// <summary>原始数据接口</summary>
    public partial interface IRawData
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>设备</summary>
        Int32 DeviceID { get; set; }

        /// <summary>设备MAC</summary>
        String DeviceMAC { get; set; }

        /// <summary>路由MAC</summary>
        String RouteMAC { get; set; }

        /// <summary>主机MAC</summary>
        String HostMAC { get; set; }

        /// <summary>帧类型</summary>
        Int32 FrameType { get; set; }

        /// <summary>子类型</summary>
        Int32 FrameType2 { get; set; }

        /// <summary>信道</summary>
        Int32 Channel { get; set; }

        /// <summary>信号强度</summary>
        Int32 Rssi { get; set; }

        /// <summary>距离。设备到主机的距离，单位米</summary>
        Double Distance { get; set; }

        /// <summary>省电。发射设备是否处于省电模式</summary>
        Boolean PowerSave { get; set; }

        /// <summary>路由。是否由路由发出的信号</summary>
        Boolean IsRoute { get; set; }

        /// <summary>内容</summary>
        String Remark { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}