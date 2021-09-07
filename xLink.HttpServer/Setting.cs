using System;
using System.ComponentModel;
using NewLife;
using NewLife.Configuration;

namespace xLinkServer
{
    /// <summary>扫描服务配置</summary>
    [Config("xLinkServer.config")]
    public class Setting : Config<Setting>
    {
        /// <summary>节点名称。默认本地计算机名</summary>
        [Description("节点名称。默认本地计算机名")]
        public String NodeName { get; set; }

        /// <summary>令牌有效期。默认12*3600秒</summary>
        [Description("令牌有效期。默认12*3600秒")]
        public Int32 TokenExpire { get; set; } = 12 * 3600;

        /// <summary>会话超时。默认600秒</summary>
        [Description("会话超时。默认600秒")]
        public Int32 SessionTimeout { get; set; } = 600;

        /// <summary>自动注册。允许客户端自动注册，默认true</summary>
        [Description("自动注册。允许客户端自动注册，默认true")]
        public Boolean AutoRegister { get; set; } = true;

        protected override void OnLoaded()
        {
            if (NodeName.IsNullOrEmpty()) NodeName = Environment.MachineName;

            base.OnLoaded();
        }
    }
}