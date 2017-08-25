using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
using XCode.Remoting;
using xLink.Entity;

namespace xLink
{
    /// <summary>用户会话</summary>
    [Api("User")]
    [DisplayName("用户")]
    public class UserSession : LinkSession
    {
        #region 属性
        /// <summary>当前用户</summary>
        public User User { get; private set; }

        ///// <summary>在线对象</summary>
        //public UserOnline Online { get; private set; }
        #endregion

        #region 构造
        static UserSession()
        {
            // 异步初始化数据
            Task.Run(() =>
            {
                var n = 0;
                n = User.Meta.Count;
                n = UserOnline.Meta.Count;
                n = UserHistory.Meta.Count;
            });
        }
        #endregion

        #region 登录注册
        /// <summary>查找用户并登录，找不到用户是返回空，登录失败则抛出异常</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected override IManageUser CheckUser(String user, String pass)
        {
            var u = User.FindByName(user);
            if (u == null) return null;

            // 登录
            Name = user;

            WriteLog("登录 {0} => {1}", user, pass);

            // 验证密码
            u.CheckRC4(pass);

            u.Type = Type;
            u.Version = Version;

            return u;
        }

        /// <summary>注册，登录找不到用户时调用注册，返回空表示禁止注册</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected override IManageUser Register(String user, String pass)
        {
            var u = User.FindByName(user);
            if (u == null) u = new User { Name = user };

            u.Password = Rand.NextString(8);
            u.Enable = true;
            u.Registers++;

            Name = u.Name;
            WriteLog("注册 {0} => {1}/{2}", user, u.Name, u.Password);

            return u;
        }
        #endregion

        #region 操作历史
        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected override IOnline CreateOnline(Int32 sessionid)
        {
            var ns = Session as NetSession;

            var olt = UserOnline.FindBySessionID(sessionid) ?? new UserOnline();
            olt.ExternalUri = ns.Remote + "";

            return olt;
        }

        /// <summary>创建历史</summary>
        /// <returns></returns>
        protected override IHistory CreateHistory() { return new UserHistory(); }
        #endregion
    }
}