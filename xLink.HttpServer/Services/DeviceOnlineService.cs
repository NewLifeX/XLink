using System;
using NewLife;
using NewLife.Threading;
using xLink.Entity;

namespace xLinkServer.Services
{
    /// <summary>设备在线服务</summary>
    public class DeviceOnlineService : DisposeBase
    {
        #region 属性
        #endregion

        #region 构造
        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);

            _timer.TryDispose();
        }
        #endregion

        #region 方法
        public void Init() => _timer = new TimerX(CheckOnline, null, 5_000, 30_000) { Async = true };

        private TimerX _timer;
        private void CheckOnline(Object state)
        {
            var set = Setting.Current;
            if (set.SessionTimeout > 0)
            {
                var rs = DeviceOnline.ClearExpire(set.SessionTimeout);
                if (rs != null)
                {
                    foreach (var olt in rs)
                    {
                        var dv = olt?.Device;
                        var msg = "[{0}]]登录于{1}，最后活跃于{2}".F(dv, olt.CreateTime, olt.UpdateTime);
                        DeviceHistory.Create(dv, "超时下线", true, msg, set.NodeName, olt.CreateIP);

                        if (dv != null)
                        {
                            // 计算在线时长
                            if (olt.CreateTime.Year > 2000 && olt.UpdateTime.Year > 2000)
                            {
                                dv.OnlineTime += (Int32)(olt.UpdateTime - olt.CreateTime).TotalSeconds;
                                dv.SaveAsync();
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}