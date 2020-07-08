using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace xLink.Entity
{
    /// <summary>设备统计。每日按产品统计</summary>
    [Serializable]
    [DataObject]
    [Description("设备统计。每日按产品统计")]
    [BindIndex("IU_DeviceStat_StatDate_ProductId", true, "StatDate,ProductId")]
    [BindIndex("IX_DeviceStat_UpdateTime_ProductId", false, "UpdateTime,ProductId")]
    [BindTable("DeviceStat", Description = "设备统计。每日按产品统计", ConnName = "xLink", DbType = DatabaseType.None)]
    public partial class DeviceStat : IDeviceStat
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private DateTime _StatDate;
        /// <summary>统计日期</summary>
        [DisplayName("统计日期")]
        [Description("统计日期")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("StatDate", "统计日期", "")]
        public DateTime StatDate { get { return _StatDate; } set { if (OnPropertyChanging(__.StatDate, value)) { _StatDate = value; OnPropertyChanged(__.StatDate); } } }

        private Int32 _ProductId;
        /// <summary>产品。0表示全部</summary>
        [DisplayName("产品")]
        [Description("产品。0表示全部")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ProductId", "产品。0表示全部", "")]
        public Int32 ProductId { get { return _ProductId; } set { if (OnPropertyChanging(__.ProductId, value)) { _ProductId = value; OnPropertyChanged(__.ProductId); } } }

        private Int32 _Total;
        /// <summary>总数。截止今天的全部设备数</summary>
        [DisplayName("总数")]
        [Description("总数。截止今天的全部设备数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Total", "总数。截止今天的全部设备数", "")]
        public Int32 Total { get { return _Total; } set { if (OnPropertyChanging(__.Total, value)) { _Total = value; OnPropertyChanged(__.Total); } } }

        private Int32 _Actives;
        /// <summary>活跃数。最后登录位于今天</summary>
        [DisplayName("活跃数")]
        [Description("活跃数。最后登录位于今天")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Actives", "活跃数。最后登录位于今天", "")]
        public Int32 Actives { get { return _Actives; } set { if (OnPropertyChanging(__.Actives, value)) { _Actives = value; OnPropertyChanged(__.Actives); } } }

        private Int32 _T7Actives;
        /// <summary>7天活跃数。最后登录位于7天内</summary>
        [DisplayName("7天活跃数")]
        [Description("7天活跃数。最后登录位于7天内")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("T7Actives", "7天活跃数。最后登录位于7天内", "")]
        public Int32 T7Actives { get { return _T7Actives; } set { if (OnPropertyChanging(__.T7Actives, value)) { _T7Actives = value; OnPropertyChanged(__.T7Actives); } } }

        private Int32 _T30Actives;
        /// <summary>30天活跃数。最后登录位于30天内</summary>
        [DisplayName("30天活跃数")]
        [Description("30天活跃数。最后登录位于30天内")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("T30Actives", "30天活跃数。最后登录位于30天内", "")]
        public Int32 T30Actives { get { return _T30Actives; } set { if (OnPropertyChanging(__.T30Actives, value)) { _T30Actives = value; OnPropertyChanged(__.T30Actives); } } }

        private Int32 _News;
        /// <summary>新增数。今天创建</summary>
        [DisplayName("新增数")]
        [Description("新增数。今天创建")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("News", "新增数。今天创建", "")]
        public Int32 News { get { return _News; } set { if (OnPropertyChanging(__.News, value)) { _News = value; OnPropertyChanged(__.News); } } }

        private Int32 _T7News;
        /// <summary>7天新增数。7天创建</summary>
        [DisplayName("7天新增数")]
        [Description("7天新增数。7天创建")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("T7News", "7天新增数。7天创建", "")]
        public Int32 T7News { get { return _T7News; } set { if (OnPropertyChanging(__.T7News, value)) { _T7News = value; OnPropertyChanged(__.T7News); } } }

        private Int32 _T30News;
        /// <summary>30天新增数。30天创建</summary>
        [DisplayName("30天新增数")]
        [Description("30天新增数。30天创建")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("T30News", "30天新增数。30天创建", "")]
        public Int32 T30News { get { return _T30News; } set { if (OnPropertyChanging(__.T30News, value)) { _T30News = value; OnPropertyChanged(__.T30News); } } }

        private Int32 _Registers;
        /// <summary>注册数。今天激活或重新激活</summary>
        [DisplayName("注册数")]
        [Description("注册数。今天激活或重新激活")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Registers", "注册数。今天激活或重新激活", "")]
        public Int32 Registers { get { return _Registers; } set { if (OnPropertyChanging(__.Registers, value)) { _Registers = value; OnPropertyChanged(__.Registers); } } }

        private Int32 _MaxOnline;
        /// <summary>最高在线。今天最高在线数</summary>
        [DisplayName("最高在线")]
        [Description("最高在线。今天最高在线数")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("MaxOnline", "最高在线。今天最高在线数", "")]
        public Int32 MaxOnline { get { return _MaxOnline; } set { if (OnPropertyChanging(__.MaxOnline, value)) { _MaxOnline = value; OnPropertyChanged(__.MaxOnline); } } }

        private DateTime _MaxOnlineTime;
        /// <summary>最高在线时间</summary>
        [DisplayName("最高在线时间")]
        [Description("最高在线时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("MaxOnlineTime", "最高在线时间", "")]
        public DateTime MaxOnlineTime { get { return _MaxOnlineTime; } set { if (OnPropertyChanging(__.MaxOnlineTime, value)) { _MaxOnlineTime = value; OnPropertyChanged(__.MaxOnlineTime); } } }

        private DateTime _CreateTime;
        /// <summary>创建时间</summary>
        [DisplayName("创建时间")]
        [Description("创建时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("CreateTime", "创建时间", "")]
        public DateTime CreateTime { get { return _CreateTime; } set { if (OnPropertyChanging(__.CreateTime, value)) { _CreateTime = value; OnPropertyChanged(__.CreateTime); } } }

        private DateTime _UpdateTime;
        /// <summary>更新时间</summary>
        [DisplayName("更新时间")]
        [Description("更新时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("UpdateTime", "更新时间", "")]
        public DateTime UpdateTime { get { return _UpdateTime; } set { if (OnPropertyChanging(__.UpdateTime, value)) { _UpdateTime = value; OnPropertyChanged(__.UpdateTime); } } }

        private String _Remark;
        /// <summary>备注</summary>
        [DisplayName("备注")]
        [Description("备注")]
        [DataObjectField(false, false, true, 500)]
        [BindColumn("Remark", "备注", "")]
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
                    case __.StatDate : return _StatDate;
                    case __.ProductId : return _ProductId;
                    case __.Total : return _Total;
                    case __.Actives : return _Actives;
                    case __.T7Actives : return _T7Actives;
                    case __.T30Actives : return _T30Actives;
                    case __.News : return _News;
                    case __.T7News : return _T7News;
                    case __.T30News : return _T30News;
                    case __.Registers : return _Registers;
                    case __.MaxOnline : return _MaxOnline;
                    case __.MaxOnlineTime : return _MaxOnlineTime;
                    case __.CreateTime : return _CreateTime;
                    case __.UpdateTime : return _UpdateTime;
                    case __.Remark : return _Remark;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = value.ToInt(); break;
                    case __.StatDate : _StatDate = value.ToDateTime(); break;
                    case __.ProductId : _ProductId = value.ToInt(); break;
                    case __.Total : _Total = value.ToInt(); break;
                    case __.Actives : _Actives = value.ToInt(); break;
                    case __.T7Actives : _T7Actives = value.ToInt(); break;
                    case __.T30Actives : _T30Actives = value.ToInt(); break;
                    case __.News : _News = value.ToInt(); break;
                    case __.T7News : _T7News = value.ToInt(); break;
                    case __.T30News : _T30News = value.ToInt(); break;
                    case __.Registers : _Registers = value.ToInt(); break;
                    case __.MaxOnline : _MaxOnline = value.ToInt(); break;
                    case __.MaxOnlineTime : _MaxOnlineTime = value.ToDateTime(); break;
                    case __.CreateTime : _CreateTime = value.ToDateTime(); break;
                    case __.UpdateTime : _UpdateTime = value.ToDateTime(); break;
                    case __.Remark : _Remark = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设备统计字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>统计日期</summary>
            public static readonly Field StatDate = FindByName(__.StatDate);

            /// <summary>产品。0表示全部</summary>
            public static readonly Field ProductId = FindByName(__.ProductId);

            /// <summary>总数。截止今天的全部设备数</summary>
            public static readonly Field Total = FindByName(__.Total);

            /// <summary>活跃数。最后登录位于今天</summary>
            public static readonly Field Actives = FindByName(__.Actives);

            /// <summary>7天活跃数。最后登录位于7天内</summary>
            public static readonly Field T7Actives = FindByName(__.T7Actives);

            /// <summary>30天活跃数。最后登录位于30天内</summary>
            public static readonly Field T30Actives = FindByName(__.T30Actives);

            /// <summary>新增数。今天创建</summary>
            public static readonly Field News = FindByName(__.News);

            /// <summary>7天新增数。7天创建</summary>
            public static readonly Field T7News = FindByName(__.T7News);

            /// <summary>30天新增数。30天创建</summary>
            public static readonly Field T30News = FindByName(__.T30News);

            /// <summary>注册数。今天激活或重新激活</summary>
            public static readonly Field Registers = FindByName(__.Registers);

            /// <summary>最高在线。今天最高在线数</summary>
            public static readonly Field MaxOnline = FindByName(__.MaxOnline);

            /// <summary>最高在线时间</summary>
            public static readonly Field MaxOnlineTime = FindByName(__.MaxOnlineTime);

            /// <summary>创建时间</summary>
            public static readonly Field CreateTime = FindByName(__.CreateTime);

            /// <summary>更新时间</summary>
            public static readonly Field UpdateTime = FindByName(__.UpdateTime);

            /// <summary>备注</summary>
            public static readonly Field Remark = FindByName(__.Remark);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得设备统计字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>统计日期</summary>
            public const String StatDate = "StatDate";

            /// <summary>产品。0表示全部</summary>
            public const String ProductId = "ProductId";

            /// <summary>总数。截止今天的全部设备数</summary>
            public const String Total = "Total";

            /// <summary>活跃数。最后登录位于今天</summary>
            public const String Actives = "Actives";

            /// <summary>7天活跃数。最后登录位于7天内</summary>
            public const String T7Actives = "T7Actives";

            /// <summary>30天活跃数。最后登录位于30天内</summary>
            public const String T30Actives = "T30Actives";

            /// <summary>新增数。今天创建</summary>
            public const String News = "News";

            /// <summary>7天新增数。7天创建</summary>
            public const String T7News = "T7News";

            /// <summary>30天新增数。30天创建</summary>
            public const String T30News = "T30News";

            /// <summary>注册数。今天激活或重新激活</summary>
            public const String Registers = "Registers";

            /// <summary>最高在线。今天最高在线数</summary>
            public const String MaxOnline = "MaxOnline";

            /// <summary>最高在线时间</summary>
            public const String MaxOnlineTime = "MaxOnlineTime";

            /// <summary>创建时间</summary>
            public const String CreateTime = "CreateTime";

            /// <summary>更新时间</summary>
            public const String UpdateTime = "UpdateTime";

            /// <summary>备注</summary>
            public const String Remark = "Remark";
        }
        #endregion
    }

    /// <summary>设备统计。每日按产品统计接口</summary>
    public partial interface IDeviceStat
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>统计日期</summary>
        DateTime StatDate { get; set; }

        /// <summary>产品。0表示全部</summary>
        Int32 ProductId { get; set; }

        /// <summary>总数。截止今天的全部设备数</summary>
        Int32 Total { get; set; }

        /// <summary>活跃数。最后登录位于今天</summary>
        Int32 Actives { get; set; }

        /// <summary>7天活跃数。最后登录位于7天内</summary>
        Int32 T7Actives { get; set; }

        /// <summary>30天活跃数。最后登录位于30天内</summary>
        Int32 T30Actives { get; set; }

        /// <summary>新增数。今天创建</summary>
        Int32 News { get; set; }

        /// <summary>7天新增数。7天创建</summary>
        Int32 T7News { get; set; }

        /// <summary>30天新增数。30天创建</summary>
        Int32 T30News { get; set; }

        /// <summary>注册数。今天激活或重新激活</summary>
        Int32 Registers { get; set; }

        /// <summary>最高在线。今天最高在线数</summary>
        Int32 MaxOnline { get; set; }

        /// <summary>最高在线时间</summary>
        DateTime MaxOnlineTime { get; set; }

        /// <summary>创建时间</summary>
        DateTime CreateTime { get; set; }

        /// <summary>更新时间</summary>
        DateTime UpdateTime { get; set; }

        /// <summary>备注</summary>
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