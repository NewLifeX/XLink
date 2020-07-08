using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>设备在线</summary>
    [Serializable]
    [DataObject]
    [Description("设备在线")]
    [BindIndex("IU_DeviceOnline_SessionID", true, "SessionID")]
    [BindIndex("IX_DeviceOnline_UpdateTime", false, "UpdateTime")]
    [BindIndex("IX_DeviceOnline_DeviceId", false, "DeviceId")]
    [BindIndex("IX_DeviceOnline_AreaId", false, "AreaId")]
    [BindTable("DeviceOnline", Description = "设备在线", ConnName = "xLink", DbType = DatabaseType.None)]
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

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "名称", "", Master = true)]
        public String Name { get { return _Name; } set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } } }

        private Int32 _AreaId;
        /// <summary>地区</summary>
        [DisplayName("地区")]
        [Description("地区")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AreaId", "地区", "")]
        public Int32 AreaId { get { return _AreaId; } set { if (OnPropertyChanging(__.AreaId, value)) { _AreaId = value; OnPropertyChanged(__.AreaId); } } }

        private String _Version;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Version", "版本", "")]
        public String Version { get { return _Version; } set { if (OnPropertyChanging(__.Version, value)) { _Version = value; OnPropertyChanged(__.Version); } } }

        private String _Action;
        /// <summary>操作。客户端正在指定的功能动作</summary>
        [DisplayName("操作")]
        [Description("操作。客户端正在指定的功能动作")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Action", "操作。客户端正在指定的功能动作", "")]
        public String Action { get { return _Action; } set { if (OnPropertyChanging(__.Action, value)) { _Action = value; OnPropertyChanged(__.Action); } } }

        private Int32 _PingCount;
        /// <summary>心跳</summary>
        [DisplayName("心跳")]
        [Description("心跳")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PingCount", "心跳", "")]
        public Int32 PingCount { get { return _PingCount; } set { if (OnPropertyChanging(__.PingCount, value)) { _PingCount = value; OnPropertyChanged(__.PingCount); } } }

        private Int32 _AvailableMemory;
        /// <summary>可用内存。单位M</summary>
        [DisplayName("可用内存")]
        [Description("可用内存。单位M")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AvailableMemory", "可用内存。单位M", "")]
        public Int32 AvailableMemory { get { return _AvailableMemory; } set { if (OnPropertyChanging(__.AvailableMemory, value)) { _AvailableMemory = value; OnPropertyChanged(__.AvailableMemory); } } }

        private Int32 _AvailableSpace;
        /// <summary>可用磁盘。单位M</summary>
        [DisplayName("可用磁盘")]
        [Description("可用磁盘。单位M")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AvailableSpace", "可用磁盘。单位M", "")]
        public Int32 AvailableSpace { get { return _AvailableSpace; } set { if (OnPropertyChanging(__.AvailableSpace, value)) { _AvailableSpace = value; OnPropertyChanged(__.AvailableSpace); } } }

        private Double _CpuRate;
        /// <summary>CPU率。占用率</summary>
        [DisplayName("CPU率")]
        [Description("CPU率。占用率")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CpuRate", "CPU率。占用率", "")]
        public Double CpuRate { get { return _CpuRate; } set { if (OnPropertyChanging(__.CpuRate, value)) { _CpuRate = value; OnPropertyChanged(__.CpuRate); } } }

        private Int32 _Delay;
        /// <summary>延迟。网络延迟，单位ms</summary>
        [DisplayName("延迟")]
        [Description("延迟。网络延迟，单位ms")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Delay", "延迟。网络延迟，单位ms", "")]
        public Int32 Delay { get { return _Delay; } set { if (OnPropertyChanging(__.Delay, value)) { _Delay = value; OnPropertyChanged(__.Delay); } } }

        private Int32 _Offset;
        /// <summary>偏移。客户端时间减服务端时间，单位s</summary>
        [DisplayName("偏移")]
        [Description("偏移。客户端时间减服务端时间，单位s")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Offset", "偏移。客户端时间减服务端时间，单位s", "")]
        public Int32 Offset { get { return _Offset; } set { if (OnPropertyChanging(__.Offset, value)) { _Offset = value; OnPropertyChanged(__.Offset); } } }

        private DateTime _LocalTime;
        /// <summary>本地时间</summary>
        [DisplayName("本地时间")]
        [Description("本地时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LocalTime", "本地时间", "")]
        public DateTime LocalTime { get { return _LocalTime; } set { if (OnPropertyChanging(__.LocalTime, value)) { _LocalTime = value; OnPropertyChanged(__.LocalTime); } } }

        private String _MACs;
        /// <summary>网卡</summary>
        [DisplayName("网卡")]
        [Description("网卡")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("MACs", "网卡", "")]
        public String MACs { get { return _MACs; } set { if (OnPropertyChanging(__.MACs, value)) { _MACs = value; OnPropertyChanged(__.MACs); } } }

        private String _COMs;
        /// <summary>串口</summary>
        [DisplayName("串口")]
        [Description("串口")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("COMs", "串口", "")]
        public String COMs { get { return _COMs; } set { if (OnPropertyChanging(__.COMs, value)) { _COMs = value; OnPropertyChanged(__.COMs); } } }

        private String _Processes;
        /// <summary>进程列表</summary>
        [DisplayName("进程列表")]
        [Description("进程列表")]
        [DataObjectField(false, false, true, 2000)]
        [BindColumn("Processes", "进程列表", "")]
        public String Processes { get { return _Processes; } set { if (OnPropertyChanging(__.Processes, value)) { _Processes = value; OnPropertyChanged(__.Processes); } } }

        private String _Token;
        /// <summary>令牌</summary>
        [DisplayName("令牌")]
        [Description("令牌")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Token", "令牌", "")]
        public String Token { get { return _Token; } set { if (OnPropertyChanging(__.Token, value)) { _Token = value; OnPropertyChanged(__.Token); } } }

        private String _Creator;
        /// <summary>创建者。服务端节点</summary>
        [DisplayName("创建者")]
        [Description("创建者。服务端节点")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Creator", "创建者。服务端节点", "")]
        public String Creator { get { return _Creator; } set { if (OnPropertyChanging(__.Creator, value)) { _Creator = value; OnPropertyChanged(__.Creator); } } }

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
                    case __.ProductId : return _ProductId;
                    case __.DeviceId : return _DeviceId;
                    case __.Name : return _Name;
                    case __.AreaId : return _AreaId;
                    case __.Version : return _Version;
                    case __.Action : return _Action;
                    case __.PingCount : return _PingCount;
                    case __.AvailableMemory : return _AvailableMemory;
                    case __.AvailableSpace : return _AvailableSpace;
                    case __.CpuRate : return _CpuRate;
                    case __.Delay : return _Delay;
                    case __.Offset : return _Offset;
                    case __.LocalTime : return _LocalTime;
                    case __.MACs : return _MACs;
                    case __.COMs : return _COMs;
                    case __.Processes : return _Processes;
                    case __.Token : return _Token;
                    case __.Creator : return _Creator;
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
                    case __.ProductId : _ProductId = value.ToInt(); break;
                    case __.DeviceId : _DeviceId = value.ToInt(); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.AreaId : _AreaId = value.ToInt(); break;
                    case __.Version : _Version = Convert.ToString(value); break;
                    case __.Action : _Action = Convert.ToString(value); break;
                    case __.PingCount : _PingCount = value.ToInt(); break;
                    case __.AvailableMemory : _AvailableMemory = value.ToInt(); break;
                    case __.AvailableSpace : _AvailableSpace = value.ToInt(); break;
                    case __.CpuRate : _CpuRate = value.ToDouble(); break;
                    case __.Delay : _Delay = value.ToInt(); break;
                    case __.Offset : _Offset = value.ToInt(); break;
                    case __.LocalTime : _LocalTime = value.ToDateTime(); break;
                    case __.MACs : _MACs = Convert.ToString(value); break;
                    case __.COMs : _COMs = Convert.ToString(value); break;
                    case __.Processes : _Processes = Convert.ToString(value); break;
                    case __.Token : _Token = Convert.ToString(value); break;
                    case __.Creator : _Creator = Convert.ToString(value); break;
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

            /// <summary>产品</summary>
            public static readonly Field ProductId = FindByName(__.ProductId);

            /// <summary>设备</summary>
            public static readonly Field DeviceId = FindByName(__.DeviceId);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>地区</summary>
            public static readonly Field AreaId = FindByName(__.AreaId);

            /// <summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            /// <summary>操作。客户端正在指定的功能动作</summary>
            public static readonly Field Action = FindByName(__.Action);

            /// <summary>心跳</summary>
            public static readonly Field PingCount = FindByName(__.PingCount);

            /// <summary>可用内存。单位M</summary>
            public static readonly Field AvailableMemory = FindByName(__.AvailableMemory);

            /// <summary>可用磁盘。单位M</summary>
            public static readonly Field AvailableSpace = FindByName(__.AvailableSpace);

            /// <summary>CPU率。占用率</summary>
            public static readonly Field CpuRate = FindByName(__.CpuRate);

            /// <summary>延迟。网络延迟，单位ms</summary>
            public static readonly Field Delay = FindByName(__.Delay);

            /// <summary>偏移。客户端时间减服务端时间，单位s</summary>
            public static readonly Field Offset = FindByName(__.Offset);

            /// <summary>本地时间</summary>
            public static readonly Field LocalTime = FindByName(__.LocalTime);

            /// <summary>网卡</summary>
            public static readonly Field MACs = FindByName(__.MACs);

            /// <summary>串口</summary>
            public static readonly Field COMs = FindByName(__.COMs);

            /// <summary>进程列表</summary>
            public static readonly Field Processes = FindByName(__.Processes);

            /// <summary>令牌</summary>
            public static readonly Field Token = FindByName(__.Token);

            /// <summary>创建者。服务端节点</summary>
            public static readonly Field Creator = FindByName(__.Creator);

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

            /// <summary>产品</summary>
            public const String ProductId = "ProductId";

            /// <summary>设备</summary>
            public const String DeviceId = "DeviceId";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>地区</summary>
            public const String AreaId = "AreaId";

            /// <summary>版本</summary>
            public const String Version = "Version";

            /// <summary>操作。客户端正在指定的功能动作</summary>
            public const String Action = "Action";

            /// <summary>心跳</summary>
            public const String PingCount = "PingCount";

            /// <summary>可用内存。单位M</summary>
            public const String AvailableMemory = "AvailableMemory";

            /// <summary>可用磁盘。单位M</summary>
            public const String AvailableSpace = "AvailableSpace";

            /// <summary>CPU率。占用率</summary>
            public const String CpuRate = "CpuRate";

            /// <summary>延迟。网络延迟，单位ms</summary>
            public const String Delay = "Delay";

            /// <summary>偏移。客户端时间减服务端时间，单位s</summary>
            public const String Offset = "Offset";

            /// <summary>本地时间</summary>
            public const String LocalTime = "LocalTime";

            /// <summary>网卡</summary>
            public const String MACs = "MACs";

            /// <summary>串口</summary>
            public const String COMs = "COMs";

            /// <summary>进程列表</summary>
            public const String Processes = "Processes";

            /// <summary>令牌</summary>
            public const String Token = "Token";

            /// <summary>创建者。服务端节点</summary>
            public const String Creator = "Creator";

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

        /// <summary>产品</summary>
        Int32 ProductId { get; set; }

        /// <summary>设备</summary>
        Int32 DeviceId { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>地区</summary>
        Int32 AreaId { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>操作。客户端正在指定的功能动作</summary>
        String Action { get; set; }

        /// <summary>心跳</summary>
        Int32 PingCount { get; set; }

        /// <summary>可用内存。单位M</summary>
        Int32 AvailableMemory { get; set; }

        /// <summary>可用磁盘。单位M</summary>
        Int32 AvailableSpace { get; set; }

        /// <summary>CPU率。占用率</summary>
        Double CpuRate { get; set; }

        /// <summary>延迟。网络延迟，单位ms</summary>
        Int32 Delay { get; set; }

        /// <summary>偏移。客户端时间减服务端时间，单位s</summary>
        Int32 Offset { get; set; }

        /// <summary>本地时间</summary>
        DateTime LocalTime { get; set; }

        /// <summary>网卡</summary>
        String MACs { get; set; }

        /// <summary>串口</summary>
        String COMs { get; set; }

        /// <summary>进程列表</summary>
        String Processes { get; set; }

        /// <summary>令牌</summary>
        String Token { get; set; }

        /// <summary>创建者。服务端节点</summary>
        String Creator { get; set; }

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