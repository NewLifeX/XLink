using System;
using System.ComponentModel;
using NewLife.Xml;

namespace WiFi.Server
{
    /// <summary>配置</summary>
    [XmlConfigFile("Config/Server.config", 15000)]
    public class Setting : XmlConfig<Setting>
    {
        #region 属性
        /// <summary>调试开关。默认true</summary>
        [Description("调试开关。默认true")]
        public Boolean Debug { get; set; } = true;

        /// <summary>端口。默认6000</summary>
        [Description("端口。默认6000")]
        public Int32 Port { get; set; } = 6000;

        /// <summary>网络日志开关。默认false</summary>
        [Description("网络日志开关。默认false")]
        public Boolean SocketDebug { get; set; }

        /// <summary>编码日志开关。默认false</summary>
        [Description("编码日志开关。默认false")]
        public Boolean EncoderDebug { get; set; }
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        public Setting()
        {
#if DEBUG
            EncoderDebug = true;
#endif
        }
        #endregion
    }
}