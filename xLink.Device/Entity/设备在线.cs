﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Device.Entity
{
    /// <summary>设备在线</summary>
    [Serializable]
    [DataObject]
    [Description("设备在线")]
    [BindIndex("IX_DeviceOnline_SessionID", false, "SessionID")]
    [BindIndex("IX_DeviceOnline_DeviceID", false, "DeviceID")]
    [BindIndex("IX_DeviceOnline_Name", false, "Name")]
    [BindIndex("IX_DeviceOnline_Type", false, "Type")]
    [BindTable("DeviceOnline", Description = "设备在线", ConnName = "Device", DbType = DatabaseType.SqlServer)]
    public partial class DeviceOnline : IDeviceOnline
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 10)]
        [BindColumn(1, "ID", "编号", null, "int", 10, 0, false)]
        public virtual Int32 ID
        {
            get { return _ID; }
            set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } }
        }

        private Int32 _DeviceID;
        /// <summary>编码</summary>
        [DisplayName("编码")]
        [Description("编码")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "DeviceID", "编码", null, "int", 10, 0, false)]
        public virtual Int32 DeviceID
        {
            get { return _DeviceID; }
            set { if (OnPropertyChanging(__.DeviceID, value)) { _DeviceID = value; OnPropertyChanged(__.DeviceID); } }
        }

        private String _Name;
        /// <summary>名称</summary>
        [DisplayName("名称")]
        [Description("名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(3, "Name", "名称", null, "nvarchar(50)", 0, 0, true, Master=true)]
        public virtual String Name
        {
            get { return _Name; }
            set { if (OnPropertyChanging(__.Name, value)) { _Name = value; OnPropertyChanged(__.Name); } }
        }

        private String _Ver;
        /// <summary>版本</summary>
        [DisplayName("版本")]
        [Description("版本")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(4, "Ver", "版本", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Ver
        {
            get { return _Ver; }
            set { if (OnPropertyChanging(__.Ver, value)) { _Ver = value; OnPropertyChanged(__.Ver); } }
        }

        private String _Type;
        /// <summary>类型</summary>
        [DisplayName("类型")]
        [Description("类型")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(5, "Type", "类型", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Type
        {
            get { return _Type; }
            set { if (OnPropertyChanging(__.Type, value)) { _Type = value; OnPropertyChanged(__.Type); } }
        }

        private Int32 _SessionID;
        /// <summary>会话</summary>
        [DisplayName("会话")]
        [Description("会话")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(6, "SessionID", "会话", null, "int", 10, 0, false)]
        public virtual Int32 SessionID
        {
            get { return _SessionID; }
            set { if (OnPropertyChanging(__.SessionID, value)) { _SessionID = value; OnPropertyChanged(__.SessionID); } }
        }

        private String _InternalUri;
        /// <summary>内网</summary>
        [DisplayName("内网")]
        [Description("内网")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(7, "InternalUri", "内网", null, "nvarchar(50)", 0, 0, true)]
        public virtual String InternalUri
        {
            get { return _InternalUri; }
            set { if (OnPropertyChanging(__.InternalUri, value)) { _InternalUri = value; OnPropertyChanged(__.InternalUri); } }
        }

        private String _ExternalUri;
        /// <summary>外网</summary>
        [DisplayName("外网")]
        [Description("外网")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(8, "ExternalUri", "外网", null, "nvarchar(50)", 0, 0, true)]
        public virtual String ExternalUri
        {
            get { return _ExternalUri; }
            set { if (OnPropertyChanging(__.ExternalUri, value)) { _ExternalUri = value; OnPropertyChanged(__.ExternalUri); } }
        }

        private Int32 _LoginCount;
        /// <summary>登录</summary>
        [DisplayName("登录")]
        [Description("登录")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(9, "LoginCount", "登录", null, "int", 10, 0, false)]
        public virtual Int32 LoginCount
        {
            get { return _LoginCount; }
            set { if (OnPropertyChanging(__.LoginCount, value)) { _LoginCount = value; OnPropertyChanged(__.LoginCount); } }
        }

        private Int32 _PingCount;
        /// <summary>心跳</summary>
        [DisplayName("心跳")]
        [Description("心跳")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(10, "PingCount", "心跳", null, "int", 10, 0, false)]
        public virtual Int32 PingCount
        {
            get { return _PingCount; }
            set { if (OnPropertyChanging(__.PingCount, value)) { _PingCount = value; OnPropertyChanged(__.PingCount); } }
        }

        private DateTime _LoginTime;
        /// <summary>登录时间</summary>
        [DisplayName("登录时间")]
        [Description("登录时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(11, "LoginTime", "登录时间", null, "datetime", 3, 0, false)]
        public virtual DateTime LoginTime
        {
            get { return _LoginTime; }
            set { if (OnPropertyChanging(__.LoginTime, value)) { _LoginTime = value; OnPropertyChanged(__.LoginTime); } }
        }

        private DateTime _LastActive;
        /// <summary>最后活跃</summary>
        [DisplayName("最后活跃")]
        [Description("最后活跃")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(12, "LastActive", "最后活跃", null, "datetime", 3, 0, false)]
        public virtual DateTime LastActive
        {
            get { return _LastActive; }
            set { if (OnPropertyChanging(__.LastActive, value)) { _LastActive = value; OnPropertyChanged(__.LastActive); } }
        }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(13, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private Int32 _ErrorCount;
        /// <summary>错误</summary>
        [DisplayName("错误")]
        [Description("错误")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(14, "ErrorCount", "错误", null, "int", 10, 0, false)]
        public virtual Int32 ErrorCount
        {
            get { return _ErrorCount; }
            set { if (OnPropertyChanging(__.ErrorCount, value)) { _ErrorCount = value; OnPropertyChanged(__.ErrorCount); } }
        }

        private String _LastError;
        /// <summary>最后错误</summary>
        [DisplayName("最后错误")]
        [Description("最后错误")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(15, "LastError", "最后错误", null, "nvarchar(50)", 0, 0, true)]
        public virtual String LastError
        {
            get { return _LastError; }
            set { if (OnPropertyChanging(__.LastError, value)) { _LastError = value; OnPropertyChanged(__.LastError); } }
        }
        #endregion

        #region 获取/设置 字段值
        /// <summary>
        /// 获取/设置 字段值。
        /// 一个索引，基类使用反射实现。
        /// 派生实体类可重写该索引，以避免反射带来的性能损耗
        /// </summary>
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
                    case __.Ver : return _Ver;
                    case __.Type : return _Type;
                    case __.SessionID : return _SessionID;
                    case __.InternalUri : return _InternalUri;
                    case __.ExternalUri : return _ExternalUri;
                    case __.LoginCount : return _LoginCount;
                    case __.PingCount : return _PingCount;
                    case __.LoginTime : return _LoginTime;
                    case __.LastActive : return _LastActive;
                    case __.CreateTime : return _CreateTime;
                    case __.ErrorCount : return _ErrorCount;
                    case __.LastError : return _LastError;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.DeviceID : _DeviceID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Ver : _Ver = Convert.ToString(value); break;
                    case __.Type : _Type = Convert.ToString(value); break;
                    case __.SessionID : _SessionID = Convert.ToInt32(value); break;
                    case __.InternalUri : _InternalUri = Convert.ToString(value); break;
                    case __.ExternalUri : _ExternalUri = Convert.ToString(value); break;
                    case __.LoginCount : _LoginCount = Convert.ToInt32(value); break;
                    case __.PingCount : _PingCount = Convert.ToInt32(value); break;
                    case __.LoginTime : _LoginTime = Convert.ToDateTime(value); break;
                    case __.LastActive : _LastActive = Convert.ToDateTime(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.ErrorCount : _ErrorCount = Convert.ToInt32(value); break;
                    case __.LastError : _LastError = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设备在线字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>编码</summary>
            public static readonly Field DeviceID = FindByName(__.DeviceID);

            ///<summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>版本</summary>
            public static readonly Field Ver = FindByName(__.Ver);

            ///<summary>类型</summary>
            public static readonly Field Type = FindByName(__.Type);

            ///<summary>会话</summary>
            public static readonly Field SessionID = FindByName(__.SessionID);

            ///<summary>内网</summary>
            public static readonly Field InternalUri = FindByName(__.InternalUri);

            ///<summary>外网</summary>
            public static readonly Field ExternalUri = FindByName(__.ExternalUri);

            ///<summary>登录</summary>
            public static readonly Field LoginCount = FindByName(__.LoginCount);

            ///<summary>心跳</summary>
            public static readonly Field PingCount = FindByName(__.PingCount);

            ///<summary>登录时间</summary>
            public static readonly Field LoginTime = FindByName(__.LoginTime);

            ///<summary>最后活跃</summary>
            public static readonly Field LastActive = FindByName(__.LastActive);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>错误</summary>
            public static readonly Field ErrorCount = FindByName(__.ErrorCount);

            ///<summary>最后错误</summary>
            public static readonly Field LastError = FindByName(__.LastError);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得设备在线字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>编码</summary>
            public const String DeviceID = "DeviceID";

            ///<summary>名称</summary>
            public const String Name = "Name";

            ///<summary>版本</summary>
            public const String Ver = "Ver";

            ///<summary>类型</summary>
            public const String Type = "Type";

            ///<summary>会话</summary>
            public const String SessionID = "SessionID";

            ///<summary>内网</summary>
            public const String InternalUri = "InternalUri";

            ///<summary>外网</summary>
            public const String ExternalUri = "ExternalUri";

            ///<summary>登录</summary>
            public const String LoginCount = "LoginCount";

            ///<summary>心跳</summary>
            public const String PingCount = "PingCount";

            ///<summary>登录时间</summary>
            public const String LoginTime = "LoginTime";

            ///<summary>最后活跃</summary>
            public const String LastActive = "LastActive";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>错误</summary>
            public const String ErrorCount = "ErrorCount";

            ///<summary>最后错误</summary>
            public const String LastError = "LastError";

        }
        #endregion
    }

    /// <summary>设备在线接口</summary>
    public partial interface IDeviceOnline
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>编码</summary>
        Int32 DeviceID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>版本</summary>
        String Ver { get; set; }

        /// <summary>类型</summary>
        String Type { get; set; }

        /// <summary>会话</summary>
        Int32 SessionID { get; set; }

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

        /// <summary>最后活跃</summary>
        DateTime LastActive { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>错误</summary>
        Int32 ErrorCount { get; set; }

        /// <summary>最后错误</summary>
        String LastError { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}