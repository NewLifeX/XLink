using System;
using System.ComponentModel;
using NewLife.Xml;

namespace xLink
{
    /// <summary>配置</summary>
    [XmlConfigFile("Config/Server.config", 15000)]
    public class Setting : XmlConfig<Setting>
    {
        #region 属性
        /// <summary>调试开关。默认true</summary>
        [Description("调试开关。默认true")]
        public Boolean Debug { get; set; } = true;

        ///// <summary>服务名称</summary>
        //[Description("服务名称")]
        //public String ServiceName { get; set; } = "LinkServer";

        /// <summary>端口。默认2233</summary>
        [Description("端口。默认2233")]
        public Int32 Port { get; set; } = 2233;

        /// <summary>会话超时时间。默认360秒</summary>
        [Description("会话超时时间。默认360秒")]
        public Int32 SessionTimeout { get; set; } = 360;

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