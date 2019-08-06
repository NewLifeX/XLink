using NewLife.Net;
using System;
using WiFi.Entity;

namespace WiFi.Server
{
    class WiFiServer : NetServer<WiFiSession>
    {
        /// <summary>日志命令。打开后显示收发命令详情</summary>
        public Boolean CommandLog { get; set; }
    }

    class WiFiSession : NetSession<WiFiServer>
    {
        #region 属性
        #endregion

        #region 主循环
        protected override void OnReceive(ReceivedEventArgs e)
        {
            var str = e.Packet.ToStr().Trim();
            if (str.IsNullOrEmpty()) return;

            if (Host.CommandLog) WriteLog(str);

            //Process(str);
            var ss = str.Split("\r", "\n");
            if (ss != null && ss.Length > 0)
            {
                foreach (var item in ss)
                {
                    Process(item);
                }
            }
        }

        public void Process(String data)
        {
            var rd = Parse(data);
            if (rd == null) return;

            // 入库
            rd.SaveAsync();

            // 登录在线
            var host = Check(rd.HostMAC, null, DeviceKinds.Host);
            var route = Check(rd.RouteMAC, rd.Remark, DeviceKinds.Route);
            var olt = Check(rd.DeviceMAC, null, DeviceKinds.Device);

            if (olt != null)
            {
                olt.Rssi = rd.Rssi;

                if (host != null) olt.HostID = host.DeviceID;
                if (route != null) olt.RouteID = route.DeviceID;

                // 设备属性
                var dv = olt.Device;
                if (dv != null)
                {
                    dv.LastHostID = host.DeviceID;
                    dv.LastRouteID = route.DeviceID;
                    dv.LastRSSI = rd.Rssi;
                }
            }

            // 更新路由器名称
            if (route != null)
            {
                route.Name = rd.Remark;

                var dv = route.Device;
                if (dv != null)
                {
                    dv.Name = rd.Remark;
                    dv.SaveAsync();
                }
            }
        }

        protected virtual DeviceOnline Check(String mac, String name, DeviceKinds kind)
        {
            var olt = GetOnline(mac);
            if (olt == null)
            {
                var ip = Remote?.EndPoint.Address + "";
                var dv = Login(mac, name, ip, kind);
                olt = CreateOnline(mac, dv);
            }
            olt.Kind = kind;
            olt.Total++;
            olt.SaveAsync(5_000);

            return olt;
        }

        protected override void OnDispose(Boolean disposing)
        {
            base.OnDispose(disposing);

            //Online?.Delete();
        }
        #endregion

        #region 解析
        public virtual RawData Parse(String str)
        {
            str = str.Trim();
            if (str.IsNullOrEmpty()) return null;

            var ss = str.Split('|', ' ');
            if (ss == null || ss.Length < 6) return null;

            var rd = new RawData();

            // TZ-007
            if (ss[2].Length >= 6)
            {
                // B4:E6:2D:09:62:ED|78:DA:07:85:86:7E|70:AF:6A:78:45:0A|01|09|2|-73|0|0|0|FeiFan
                rd.HostMAC = ss[0];
                rd.DeviceMAC = ss[1];
                rd.RouteMAC = ss[2];
                rd.FrameType = ss[3].ToInt();
                rd.FrameType2 = ss[4].ToInt();
                rd.Channel = ss[5].ToInt();
                rd.Rssi = ss[6].ToInt();

                //rd.Remark = ss.Last();
                if (ss.Length >= 11) rd.Remark = ss[10];
            }
            else
            {

            }

            var ip = Remote?.EndPoint.Address + "";
            rd.CreateTime = DateTime.Now;
            rd.CreateIP = ip;

            return rd;
        }
        #endregion

        #region 在线
        /// <summary>登录</summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        protected virtual Device Login(String code, String name, String ip, DeviceKinds kind)
        {
            var dv = Device.FindByCode(code);
            if (dv == null)
            {
                dv = new Device
                {
                    Name = name,
                    Code = code,
                    Enable = true,
                };
                dv.Insert();
            }

            if (!dv.Enable) throw new Exception($"[{dv.Name}/{dv.Code}]禁止登录");

            dv.Name = name;
            dv.Kind = kind;

            dv.Logins++;
            dv.LastLogin = DateTime.Now;
            dv.LastLoginIP = ip;

            dv.SaveAsync();

            //Device = dv;

            // 登录历史
            WriteHistory("登录", dv);

            return dv;
        }

        protected virtual DeviceOnline GetOnline(String mac)
        {
            var sid = $"{mac}@{Remote.EndPoint}";
            return DeviceOnline.FindBySessionID(sid);
        }

        /// <summary>检查在线</summary>
        /// <returns></returns>
        protected virtual DeviceOnline CreateOnline(String mac, IDevice dv)
        {
            var sid = $"{mac}@{Remote.EndPoint}";
            var olt = new DeviceOnline
            {
                SessionID = sid,
                DeviceID = dv.ID,
                Name = dv + "",
                Kind = dv.Kind,
            };

            olt.Insert();

            return olt;
        }
        #endregion

        #region 历史
        protected virtual DeviceHistory WriteHistory(String action, IDevice dv)
        {
            var ip = Remote?.EndPoint.Address + "";
            var hi = new DeviceHistory
            {
                DeviceID = dv.ID,
                Name = dv.Name,
                Action = action,
                Success = true,
                CreateDeviceID = dv.ID,

                CreateTime = DateTime.Now,
                CreateIP = ip,
            };

            hi.SaveAsync();

            return hi;
        }
        #endregion
    }
}