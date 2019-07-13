using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using xLink.Entity;
using xLink.Models;

namespace xLink
{
    /// <summary>设备会话</summary>
    [Api("Device")]
    [DisplayName("设备")]
    public class DeviceSession : LinkSession
    {
        #region 属性
        #endregion

        #region 构造
        static DeviceSession()
        {
            // 异步初始化数据
            ThreadPoolX.QueueUserWorkItem(() =>
            {
                var n = 0;
                n = Device.Meta.Count;
                n = DeviceOnline.Meta.Count;
                n = DeviceHistory.Meta.Count;
            });
        }

        /// <summary>实例化</summary>
        public DeviceSession()
        {
            //GetData = OnGetData;
            //SetData = OnSetData;
        }
        #endregion

        #region 登录
        /// <summary>登录</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="session"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected override IManageUser OnLogin(String user, String pass, INetSession session, IDictionary<String, Object> parameters)
        {
            if (user.IsNullOrEmpty()) throw new ApiException(3, "用户名不能为空");

            var u = Device.FindByName(user);
            //if (u == null) throw new ApiException(4, $"找不到设备[{user}]");
            if (u == null)
            {
                u = new Device
                {
                    Name = user,
                    Password = Rand.NextString(8),
                };
            }

            // 验证密码
            u.CheckRC4(pass);

            return u;
        }
        #endregion

        #region 在线历史
        /// <summary>获取 或 添加 在线记录</summary>
        /// <param name="user"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected override IOnline GetOrAddOnline(IManageUser user, INetSession ns)
        {
            var ip = ns.Remote.Host;
            var sessionid = ns.Remote.EndPoint + "";
            var online = DeviceOnline.FindBySessionID(sessionid) ?? new DeviceOnline { CreateIP = ip };
            online.Version = Version;
            online.ExternalUri = ns.Remote + "";

            return online;
        }

        /// <summary>创建历史</summary>
        /// <returns></returns>
        protected override IHistory CreateHistory() => new DeviceHistory();
        #endregion
    }
}