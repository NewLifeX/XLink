using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.Xml;

namespace xLink.Client
{
    /// <summary>配置</summary>
    [XmlConfigFile("Config\\Client.config")]
    public class Setting : XmlConfig<Setting>
    {
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
        public String Address { get; set; } = "Tcp://127.0.0.1:1234";

        ///// <summary>文本编码</summary>
        //[XmlIgnore]
        //public Encoding Encoding { get; set; } = Encoding.Default;

        ///// <summary>编码</summary>
        //[Description("编码 gb2312/us-ascii/utf-8")]
        //public String WebEncoding { get { return Encoding?.WebName; } set { Encoding = Encoding.GetEncoding(value); } }

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

        /// <summary>显示网络日志</summary>
        [Description("显示网络日志")]
        public Boolean ShowSocketLog { get; set; } = true;

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

        /// <summary>是否加密，默认false</summary>
        [Description("是否加密，默认false")]
        public Boolean Encrypted { get; set; }

        /// <summary>是否压缩，默认false</summary>
        [Description("是否压缩，默认false")]
        public Boolean Compressed { get; set; }

        /// <summary>编码日志开关。默认false</summary>
        [Description("编码日志开关。默认false")]
        public Boolean EncoderDebug { get; set; }

        /// <summary>日志着色</summary>
        [Description("日志着色")]
        public Boolean ColorLog { get; set; } = true;

        /// <summary>扩展数据</summary>
        [Description("扩展数据")]
        public String Extend { get; set; } = "";

        public Setting()
        {
        }
    }
}