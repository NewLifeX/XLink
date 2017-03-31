﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Master.Entity
{
    /// <summary>服务器历史</summary>
    [Serializable]
    [DataObject]
    [Description("服务器历史")]
    [BindIndex("IX_ServerHistory_ServerID", false, "ServerID")]
    [BindIndex("IX_ServerHistory_Name", false, "Name")]
    [BindIndex("IX_ServerHistory_Type", false, "Type")]
    [BindTable("ServerHistory", Description = "服务器历史", ConnName = "ServerHistory", DbType = DatabaseType.SqlServer)]
    public partial class ServerHistory : IServerHistory
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

        private Int32 _ServerID;
        /// <summary>编码</summary>
        [DisplayName("编码")]
        [Description("编码")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(2, "ServerID", "编码", null, "int", 10, 0, false)]
        public virtual Int32 ServerID
        {
            get { return _ServerID; }
            set { if (OnPropertyChanging(__.ServerID, value)) { _ServerID = value; OnPropertyChanged(__.ServerID); } }
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

        private String _Action;
        /// <summary>操作</summary>
        [DisplayName("操作")]
        [Description("操作")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(6, "Action", "操作", null, "nvarchar(50)", 0, 0, true)]
        public virtual String Action
        {
            get { return _Action; }
            set { if (OnPropertyChanging(__.Action, value)) { _Action = value; OnPropertyChanged(__.Action); } }
        }

        private Boolean _Success;
        /// <summary>成功</summary>
        [DisplayName("成功")]
        [Description("成功")]
        [DataObjectField(false, false, true, 1)]
        [BindColumn(7, "Success", "成功", null, "bit", 0, 0, false)]
        public virtual Boolean Success
        {
            get { return _Success; }
            set { if (OnPropertyChanging(__.Success, value)) { _Success = value; OnPropertyChanged(__.Success); } }
        }

        private Int32 _CreateServerID;
        /// <summary>创建者</summary>
        [DisplayName("创建者")]
        [Description("创建者")]
        [DataObjectField(false, false, true, 10)]
        [BindColumn(8, "CreateServerID", "创建者", null, "int", 10, 0, false)]
        public virtual Int32 CreateServerID
        {
            get { return _CreateServerID; }
            set { if (OnPropertyChanging(__.CreateServerID, value)) { _CreateServerID = value; OnPropertyChanged(__.CreateServerID); } }
        }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 3)]
        [BindColumn(9, "CreateTime", "创建时间", null, "datetime", 3, 0, false)]
        public virtual DateTime CreateTime
        {
            get { return _CreateTime; }
            set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } }
        }

        private String _CreateIP;
        /// <summary>创建地址</summary>
        [DisplayName("创建地址")]
        [Description("创建地址")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn(10, "CreateIP", "创建地址", null, "nvarchar(50)", 0, 0, true)]
        public virtual String CreateIP
        {
            get { return _CreateIP; }
            set { if (OnPropertyChanging(__.CreateIP, value)) { _CreateIP = value; OnPropertyChanged(__.CreateIP); } }
        }

        private String _Remark;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn(11, "Content", "内容", null, "nvarchar(500)", 0, 0, true)]
        public virtual String Remark
        {
            get { return _Remark; }
            set { if (OnPropertyChanging(__.Remark, value)) { _Remark = value; OnPropertyChanged(__.Remark); } }
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
                    case __.ServerID : return _ServerID;
                    case __.Name : return _Name;
                    case __.Ver : return _Ver;
                    case __.Type : return _Type;
                    case __.Action : return _Action;
                    case __.Success : return _Success;
                    case __.CreateServerID : return _CreateServerID;
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
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.ServerID : _ServerID = Convert.ToInt32(value); break;
                    case __.Name : _Name = Convert.ToString(value); break;
                    case __.Ver : _Ver = Convert.ToString(value); break;
                    case __.Type : _Type = Convert.ToString(value); break;
                    case __.Action : _Action = Convert.ToString(value); break;
                    case __.Success : _Success = Convert.ToBoolean(value); break;
                    case __.CreateServerID : _CreateServerID = Convert.ToInt32(value); break;
                    case __.CreateTime : _CreateTime = Convert.ToDateTime(value); break;
                    case __.CreateIP : _CreateIP = Convert.ToString(value); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得服务器历史字段信息的快捷方式</summary>
        public partial class _
        {
            ///<summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            ///<summary>编码</summary>
            public static readonly Field ServerID = FindByName(__.ServerID);

            ///<summary>名称</summary>
            public static readonly Field Name = FindByName(__.Name);

            ///<summary>版本</summary>
            public static readonly Field Ver = FindByName(__.Ver);

            ///<summary>类型</summary>
            public static readonly Field Type = FindByName(__.Type);

            ///<summary>操作</summary>
            public static readonly Field Action = FindByName(__.Action);

            ///<summary>成功</summary>
            public static readonly Field Success = FindByName(__.Success);

            ///<summary>创建者</summary>
            public static readonly Field CreateServerID = FindByName(__.CreateServerID);

            ///<summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            ///<summary>创建地址</summary>
            public static readonly Field CreateIP = FindByName(__.CreateIP);

            ///<summary>内容</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得服务器历史字段名称的快捷方式</summary>
        partial class __
        {
            ///<summary>编号</summary>
            public const String ID = "ID";

            ///<summary>编码</summary>
            public const String ServerID = "ServerID";

            ///<summary>名称</summary>
            public const String Name = "Name";

            ///<summary>版本</summary>
            public const String Ver = "Ver";

            ///<summary>类型</summary>
            public const String Type = "Type";

            ///<summary>操作</summary>
            public const String Action = "Action";

            ///<summary>成功</summary>
            public const String Success = "Success";

            ///<summary>创建者</summary>
            public const String CreateServerID = "CreateServerID";

            ///<summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            ///<summary>创建地址</summary>
            public const String CreateIP = "CreateIP";

            ///<summary>内容</summary>
            public const String Remark = "Remark";

        }
        #endregion
    }

    /// <summary>服务器历史接口</summary>
    public partial interface IServerHistory
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>编码</summary>
        Int32 ServerID { get; set; }

        /// <summary>名称</summary>
        String Name { get; set; }

        /// <summary>版本</summary>
        String Ver { get; set; }

        /// <summary>类型</summary>
        String Type { get; set; }

        /// <summary>操作</summary>
        String Action { get; set; }

        /// <summary>成功</summary>
        Boolean Success { get; set; }

        /// <summary>创建者</summary>
        Int32 CreateServerID { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>创建地址</summary>
        String CreateIP { get; set; }

        /// <summary>内容</summary>
        String Remark { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值。</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}