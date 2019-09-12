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
    [BindIndex("IX_DeviceOnline_DeviceID", false, "DeviceID")]
    [BindTable("DeviceOnline", Description = "设备在线", ConnName = "xLink", DbType = DatabaseType.SqlServer)]
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

        private Int32 _SessionID;
        /// <summary>会话</summary>
        [DisplayName("会话")]
        [Description("会话")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SessionID", "会话", "")]
        public Int32 SessionID { get { return _SessionID; } set { if (OnPropertyChanging(__.SessionID, value)) { _SessionID = value; OnPropertyChanged(__.SessionID); } } }

        private Int32 _ProductID;
        /// <summary>产品</summary>
        [DisplayName("产品")]
        [Description("产品")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ProductID", "产品", "")]
        public Int32 ProductID { get { return _ProductID; } set { if (OnPropertyChanging(__.ProductID, value)) { _ProductID = value; OnPropertyChanged(__.ProductID); } } }

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

        private DateTime _CompileTime;
        /// <summary>编译时间</summary>
        [DisplayName("编译时间")]
        [Description("编译时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CompileTime", "编译时间", "")]
        public DateTime CompileTime { get { return _CompileTime; } set { if (OnPropertyChanging(__.CompileTime, value)) { _CompileTime = value; OnPropertyChanged(__.CompileTime); } } }

        private String _NetType;
        /// <summary>网络</summary>
        [DisplayName("网络")]
        [Description("网络")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("NetType", "网络", "")]
        public String NetType { get { return _NetType; } set { if (OnPropertyChanging(__.NetType, value)) { _NetType = value; OnPropertyChanged(__.NetType); } } }

        private Int32 _Memory;
        /// <summary>内存。单位M</summary>
        [DisplayName("内存")]
        [Description("内存。单位M")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Memory", "内存。单位M", "")]
        public Int32 Memory { get { return _Memory; } set { if (OnPropertyChanging(__.Memory, value)) { _Memory = value; OnPropertyChanged(__.Memory); } } }

        private Int32 _AvailableMemory;
        /// <summary>可用内存。单位M</summary>
        [DisplayName("可用内存")]
        [Description("可用内存。单位M")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("AvailableMemory", "可用内存。单位M", "")]
        public Int32 AvailableMemory { get { return _AvailableMemory; } set { if (OnPropertyChanging(__.AvailableMemory, value)) { _AvailableMemory = value; OnPropertyChanged(__.AvailableMemory); } } }

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
        /// <summary>偏移。客户端时间减服务端时间，单位ms</summary>
        [DisplayName("偏移")]
        [Description("偏移。客户端时间减服务端时间，单位ms")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Offset", "偏移。客户端时间减服务端时间，单位ms", "")]
        public Int32 Offset { get { return _Offset; } set { if (OnPropertyChanging(__.Offset, value)) { _Offset = value; OnPropertyChanged(__.Offset); } } }

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

        private Int32 _LoginCount;
        /// <summary>登录</summary>
        [DisplayName("登录")]
        [Description("登录")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LoginCount", "登录", "")]
        public Int32 LoginCount { get { return _LoginCount; } set { if (OnPropertyChanging(__.LoginCount, value)) { _LoginCount = value; OnPropertyChanged(__.LoginCount); } } }

        private Int32 _PingCount;
        /// <summary>心跳</summary>
        [DisplayName("心跳")]
        [Description("心跳")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("PingCount", "心跳", "")]
        public Int32 PingCount { get { return _PingCount; } set { if (OnPropertyChanging(__.PingCount, value)) { _PingCount = value; OnPropertyChanged(__.PingCount); } } }

        private DateTime _LoginTime;
        /// <summary>登录时间</summary>
        [DisplayName("登录时间")]
        [Description("登录时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("LoginTime", "登录时间", "")]
        public DateTime LoginTime { get { return _LoginTime; } set { if (OnPropertyChanging(__.LoginTime, value)) { _LoginTime = value; OnPropertyChanged(__.LoginTime); } } }

        private Int32 _ErrorCount;
        /// <summary>错误</summary>
        [DisplayName("错误")]
        [Description("错误")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ErrorCount", "错误", "")]
        public Int32 ErrorCount { get { return _ErrorCount; } set { if (OnPropertyChanging(__.ErrorCount, value)) { _ErrorCount = value; OnPropertyChanged(__.ErrorCount); } } }

        private String _LastError;
        /// <summary>最后错误</summary>
        [DisplayName("最后错误")]
        [Description("最后错误")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("LastError", "最后错误", "")]
        public String LastError { get { return _LastError; } set { if (OnPropertyChanging(__.LastError, value)) { _LastError = value; OnPropertyChanged(__.LastError); } } }

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
                    case __.ProductID : return _ProductID;
                    case __.DeviceID : return _DeviceID;
                    case __.Name : return _Name;
                    case __.Version : return _Version;
                    case __.CompileTime : return _CompileTime;
                    case __.NetType : return _NetType;
                    case __.Memory : return _Memory;
                    case __.AvailableMemory : return _AvailableMemory;
                    case __.CpuRate : return _CpuRate;
                    case __.Delay : return _Delay;
                    case __.Offset : return _Offset;
                    case __.InternalUri : return _InternalUri;
                    case __.ExternalUri : return _ExternalUri;
                    case __.LoginCount : return _LoginCount;
                    case __.PingCount : return _PingCount;
                    case __.LoginTime : return _LoginTime;
                    case __.ErrorCount : return _ErrorCount;
                    case __.LastError : return _LastError;
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
                    case __.SessionID : _SessionID = value.ToInt(); break;
                    case __.ProductID : _ProductID = value.ToInt(); break;
                    case __.DeviceID : _DeviceID = value.ToInt(); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Version : _Version = Convert.ToString(value); break;
                    case __.CompileTime : _CompileTime = value.ToDateTime(); break;
                    case __.NetType : _NetType = Convert.ToString(value); break;
                    case __.Memory : _Memory = value.ToInt(); break;
                    case __.AvailableMemory : _AvailableMemory = value.ToInt(); break;
                    case __.CpuRate : _CpuRate = value.ToDouble(); break;
                    case __.Delay : _Delay = value.ToInt(); break;
                    case __.Offset : _Offset = value.ToInt(); break;
                    case __.InternalUri : _InternalUri = Convert.ToString(value); break;
                    case __.ExternalUri : _ExternalUri = Convert.ToString(value); break;
                    case __.LoginCount : _LoginCount = value.ToInt(); break;
                    case __.PingCount : _PingCount = value.ToInt(); break;
                    case __.LoginTime : _LoginTime = value.ToDateTime(); break;
                    case __.ErrorCount : _ErrorCount = value.ToInt(); break;
                    case __.LastError : _LastError = Convert.ToString(value); break;
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
            public static readonly Field ProductID = FindByName(__.ProductID);

            /// <summary>编码</summary>
            public static readonly Field DeviceID = FindByName(__.DeviceID);

            /// <summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            /// <summary>版本</summary>
            public static readonly Field Version = FindByName(__.Version);

            /// <summary>编译时间</summary>
            public static readonly Field CompileTime = FindByName(__.CompileTime);

            /// <summary>网络</summary>
            public static readonly Field NetType = FindByName(__.NetType);

            /// <summary>内存。单位M</summary>
            public static readonly Field Memory = FindByName(__.Memory);

            /// <summary>可用内存。单位M</summary>
            public static readonly Field AvailableMemory = FindByName(__.AvailableMemory);

            /// <summary>CPU率。占用率</summary>
            public static readonly Field CpuRate = FindByName(__.CpuRate);

            /// <summary>延迟。网络延迟，单位ms</summary>
            public static readonly Field Delay = FindByName(__.Delay);

            /// <summary>偏移。客户端时间减服务端时间，单位ms</summary>
            public static readonly Field Offset = FindByName(__.Offset);

            /// <summary>内网</summary>
            public static readonly Field InternalUri = FindByName(__.InternalUri);

            /// <summary>外网</summary>
            public static readonly Field ExternalUri = FindByName(__.ExternalUri);

            /// <summary>登录</summary>
            public static readonly Field LoginCount = FindByName(__.LoginCount);

            /// <summary>心跳</summary>
            public static readonly Field PingCount = FindByName(__.PingCount);

            /// <summary>登录时间</summary>
            public static readonly Field LoginTime = FindByName(__.LoginTime);

            /// <summary>错误</summary>
            public static readonly Field ErrorCount = FindByName(__.ErrorCount);

            /// <summary>最后错误</summary>
            public static readonly Field LastError = FindByName(__.LastError);

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
            public const String ProductID = "ProductID";

            /// <summary>编码</summary>
            public const String DeviceID = "DeviceID";

            /// <summary>名称</summary>
            public const String Name = "Name";

            /// <summary>版本</summary>
            public const String Version = "Version";

            /// <summary>编译时间</summary>
            public const String CompileTime = "CompileTime";

            /// <summary>网络</summary>
            public const String NetType = "NetType";

            /// <summary>内存。单位M</summary>
            public const String Memory = "Memory";

            /// <summary>可用内存。单位M</summary>
            public const String AvailableMemory = "AvailableMemory";

            /// <summary>CPU率。占用率</summary>
            public const String CpuRate = "CpuRate";

            /// <summary>延迟。网络延迟，单位ms</summary>
            public const String Delay = "Delay";

            /// <summary>偏移。客户端时间减服务端时间，单位ms</summary>
            public const String Offset = "Offset";

            /// <summary>内网</summary>
            public const String InternalUri = "InternalUri";

            /// <summary>外网</summary>
            public const String ExternalUri = "ExternalUri";

            /// <summary>登录</summary>
            public const String LoginCount = "LoginCount";

            /// <summary>心跳</summary>
            public const String PingCount = "PingCount";

            /// <summary>登录时间</summary>
            public const String LoginTime = "LoginTime";

            /// <summary>错误</summary>
            public const String ErrorCount = "ErrorCount";

            /// <summary>最后错误</summary>
            public const String LastError = "LastError";

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
        Int32 SessionID { get; set; }

        /// <summary>产品</summary>
        Int32 ProductID { get; set; }

        /// <summary>编码</summary>
        Int32 DeviceID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>版本</summary>
        String Version { get; set; }

        /// <summary>编译时间</summary>
        DateTime CompileTime { get; set; }

        /// <summary>网络</summary>
        String NetType { get; set; }

        /// <summary>内存。单位M</summary>
        Int32 Memory { get; set; }

        /// <summary>可用内存。单位M</summary>
        Int32 AvailableMemory { get; set; }

        /// <summary>CPU率。占用率</summary>
        Double CpuRate { get; set; }

        /// <summary>延迟。网络延迟，单位ms</summary>
        Int32 Delay { get; set; }

        /// <summary>偏移。客户端时间减服务端时间，单位ms</summary>
        Int32 Offset { get; set; }

        /// <summary>内网</summary>
        String InternalUri { get; set; }

        /// <summary>外网</summary>
        String ExternalUri { get; set; }

        /// <summary>登录</summary>
        Int32 LoginCount { get; set; }

        /// <summary>心跳</summary>
        Int32 PingCount { get; set; }

        /// <summary>登录时间</summary>
        DateTime LoginTime { get; set; }

        /// <summary>错误</summary>
        Int32 ErrorCount { get; set; }

        /// <summary>最后错误</summary>
        String LastError { get; set; }

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