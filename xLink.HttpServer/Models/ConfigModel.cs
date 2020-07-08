using System;

namespace xLinkServer.Models
{
    /// <summary>配置模型</summary>
    public class ConfigModel
    {
        /// <summary>配置名</summary>
        public String Name { get; set; }

        /// <summary>配置字符串，json格式</summary>
        public String Config { get; set; }
    }
}