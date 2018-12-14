using System;
using System.Collections.Generic;
using NewLife.Remoting;
using NewLife.Threading;
using xLink.Server.Models;

namespace xLink
{
    class DeviceController : LinkController<DeviceSession>
    {
        /// <summary>心跳</summary>
        /// <returns></returns>
        public override Object Ping()
        {
            var ss = Session;

            // 检查下发指令
            TimerX.Delay(ss.CheckCommand, 100);

            // 保存数据区
            var dic = ControllerContext.Current?.Parameters?.ToNullable();
            if (dic != null)
            {
                var data = dic["data"] + "";
                if (!data.IsNullOrEmpty())
                {
                    var buf = data.ToHex();

                    var dv = ss.Device;
                    dv.Data = buf.ToHex();
                    dv.SaveAsync();
                }
            }

            return base.Ping();
        }
    }
}