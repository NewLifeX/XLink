using System;
using xLink.Entity;

namespace xLink.Services
{
    /// <summary>设备服务</summary>
    public class DeviceService : LinkService<DeviceSession>
    {
        ///// <summary>心跳</summary>
        ///// <returns></returns>
        //public override Object Ping()
        //{
        //    var ss = Session;

        //    // 检查下发指令
        //    TimerX.Delay(ss.CheckCommand, 100);

        //    // 保存数据区
        //    var dic = ControllerContext.Current?.Parameters?.ToNullable();
        //    if (dic != null)
        //    {
        //        var data = dic["data"] + "";
        //        if (!data.IsNullOrEmpty())
        //        {
        //            var buf = data.ToHex();

        //            var dv = ss.Device;
        //            dv.Data = buf.ToHex();
        //            dv.SaveAsync();
        //        }
        //    }

        //    return base.Ping();
        //}

        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public override Int32 ClearExpire(Int32 secTimeout) => DeviceOnline.ClearExpire(secTimeout).Count;
        #endregion
    }
}