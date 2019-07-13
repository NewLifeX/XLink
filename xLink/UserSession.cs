using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using xLink.Entity;
using xLink.Models;

namespace xLink
{
    /// <summary>用户会话</summary>
    [Api("User")]
    [DisplayName("用户")]
    public class UserSession : LinkSession
    {
        #region 属性
        #endregion

        #region 构造
        static UserSession()
        {
            // 异步初始化数据
            ThreadPoolX.QueueUserWorkItem(() =>
            {
                var n = 0;
                n = User.Meta.Count;
                n = UserOnline.Meta.Count;
                n = UserHistory.Meta.Count;
            });
        }

        /// <summary>实例化</summary>
        public UserSession()
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

            var u = User.FindByName(user);
            if (u == null) throw new ApiException(4, $"找不到用户[{user}]");

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
            var online = /*UserOnline.FindBySessionID(sessionid) ??*/ new UserOnline { CreateIP = ip };
            online.Version = Version;
            online.ExternalUri = ns.Remote + "";

            return online;
        }

        /// <summary>创建历史</summary>
        /// <returns></returns>
        protected override IHistory CreateHistory() => new UserHistory();
        #endregion
    }
}