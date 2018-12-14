using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NewLife.Net;
using NewLife.Xml;

namespace xLink.Client
{
    /// <summary>配置</summary>
    [XmlConfigFile("Config\\Client.config")]
    public class Setting : XmlConfig<Setting>
    {
        #region 属性
        /// <summary>设备编码</summary>
        [Description("设备编码")]
        public String UserName { get; set; } = "";

        /// <summary>密码</summary>
        [Description("密码")]
        public String Password { get; set; } = "";

        /// <summary>模式</summary>
        [Description("模式")]
        public String Mode { get; set; } = "User";

        /// <summary>地址</summary>
        [Description("地址")]
        public String Address { get; set; } = "tcp://127.0.0.1:2233,tcp://feifan.link:2233";

        /// <summary>十六进制显示</summary>
        [Description("十六进制显示")]
        public Boolean HexShow { get; set; }

        /// <summary>十六进制发送</summary>
        [Description("十六进制发送")]
        public Boolean HexSend { get; set; }

        /// <summary>发送内容</summary>
        [Description("发送内容")]
        public String SendContent { get; set; } = "新生命开发团队，学无先后达者为师";

        /// <summary>发送次数</summary>
        [Description("发送次数")]
        public Int32 SendTimes { get; set; } = 1;

        /// <summary>发送间隔。毫秒</summary>
        [Description("发送间隔。毫秒")]
        public Int32 SendSleep { get; set; } = 1000;

        /// <summary>发送用户数</summary>
        [Description("发送用户数")]
        public Int32 SendUsers { get; set; } = 10000;

        /// <summary>显示应用日志</summary>
        [Description("显示应用日志")]
        public Boolean ShowLog { get; set; } = true;

        /// <summary>显示编码日志</summary>
        [Description("显示编码日志")]
        public Boolean ShowEncoderLog { get; set; } = true;

        /// <summary>显示接收字符串</summary>
        [Description("显示接收字符串")]
        public Boolean ShowReceiveString { get; set; } = true;

        /// <summary>显示发送数据</summary>
        [Description("显示发送数据")]
        public Boolean ShowSend { get; set; } = true;

        /// <summary>显示接收数据</summary>
        [Description("显示接收数据")]
        public Boolean ShowReceive { get; set; } = true;

        /// <summary>显示统计信息</summary>
        [Description("显示统计信息")]
        public Boolean ShowStat { get; set; } = true;

        ///// <summary>是否加密，默认false</summary>
        //[Description("是否加密，默认false")]
        //public Boolean Encrypted { get; set; }

        ///// <summary>是否压缩，默认false</summary>
        //[Description("是否压缩，默认false")]
        //public Boolean Compressed { get; set; }

        /// <summary>日志着色</summary>
        [Description("日志着色")]
        public Boolean ColorLog { get; set; } = true;

        /// <summary>扩展数据</summary>
        [Description("扩展数据")]
        public String Extend { get; set; } = "";
        #endregion

        #region 地址
        public List<String> GetAddresss()
        {
            return (Address + "").Split(",").Distinct().ToList();
        }

        public void AddAddresss(String addr)
        {
            if (addr.IsNullOrWhiteSpace()) return;

            //addr = addr.Trim();
            addr = new NetUri(addr).ToString();

            var list = GetAddresss();

            // 先删除再插入，保持第一位
            var addrs = new List<String>();
            addrs.Add(addr);
            // 最多10个
            addrs.AddRange(list.Where(e => !e.EqualIgnoreCase(addr)).Take(10 - 1));

            Address = addrs.Join(",");
        }
        #endregion
    }
}