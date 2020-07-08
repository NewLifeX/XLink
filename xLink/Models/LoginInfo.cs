using System;

namespace xLink.Models
{
    /// <summary>设备登录信息</summary>
    public class LoginInfo
    {
        #region 属性
        /// <summary>设备编码</summary>
        public String Code { get; set; }

        /// <summary>设备密钥</summary>
        public String Secret { get; set; }

        /// <summary>设备信息</summary>
        public DeviceInfo Device { get; set; }
        #endregion
    }

    /// <summary>设备登录响应</summary>
    public class LoginResponse
    {
        #region 属性
        /// <summary>设备编码</summary>
        public String Code { get; set; }

        /// <summary>设备密钥</summary>
        public String Secret { get; set; }

        /// <summary>名称</summary>
        public String Name { get; set; }

        /// <summary>令牌</summary>
        public String Token { get; set; }

        /// <summary>服务端版本</summary>
        public String Version { get; set; }
        #endregion
    }
}