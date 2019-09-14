using System;
using xLink.Entity;

namespace xLink.Services
{
    /// <summary>设备服务</summary>
    public class DeviceService : LinkService<DeviceSession>
    {
        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public override Int32 ClearExpire(Int32 secTimeout) => DeviceOnline.ClearExpire(secTimeout).Count;
        #endregion
    }
}