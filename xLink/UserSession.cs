using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
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

        /// <summary>在线对象</summary>
        public UserOnline Online { get; private set; }
        #endregion

        #region 构造
        static UserSession()
        {
            // 异步初始化数据
            Task.Run(() =>
            {
                var set = XCode.Setting.Current;
                if (set.IsNew)
                {
                    set.Debug = false;
                    set.SQLiteDbPath = "../Data";
                    set.Save();
                }

                var n = 0;
                n = User.Meta.Count;
                n = UserOnline.Meta.Count;
                n = UserHistory.Meta.Count;
            });
        }
        #endregion

        #region 登录注册
        /// <summary>检查登录，默认检查密码MD5散列，可继承修改</summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <returns>返回要发给客户端的对象</returns>
        protected override Object CheckLogin(String user, String pass)
        {
            if (user.IsNullOrEmpty()) throw Error(3, "用户名不能为空");

            var u = User.FindByName(user);

            // 注册
            if (u == null) return Register(user, pass);

            // 登录
            Name = user;

            // 密码有盐值和密文两部分组成
            var salt = pass.Substring(0, pass.Length / 2).ToHex();
            pass = pass.Substring(pass.Length / 2);

            WriteLog("登录 {0} => {1}/{2}", user, salt.ToHex(), pass);

            // 验证密码
            var tpass = u.Password.GetBytes();
            if (salt.RC4(tpass).ToHex() != pass) throw Error(4, "密码错误");

            var ns = Session as NetSession;
            u.Logins++;
            u.LastLogin = DateTime.Now;
            u.LastLoginIP = ns.Remote.EndPoint.Address + "";

            u.Save();

            return OnLogin(u, true);
        }

        /// <summary>注册</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected virtual Object Register(String user, String pass)
        {
            var u = User.FindByName(user);
            if (u == null) u = new User { Name = user };

            //u.Name = user.GetBytes().Crc().GetBytes().ToHex();
            u.Password = Rand.NextString(8);
            u.Enable = true;

            Name = u.Name;
            WriteLog("注册 {0} => {1}/{2}", user, u.Name, u.Password);

            var ns = Session as NetSession;

            u.Registers++;
            u.RegisterTime = DateTime.Now;
            u.RegisterIP = ns.Remote.EndPoint.Address + "";

            u.Online = true;

            u.Save();

            return OnLogin(u, false);
        }

        private Object OnLogin(User dv, Boolean islogin)
        {
            // 当前用户
            User = dv;

            var ns = Session as NetSession;

            var olt = Online ?? UserOnline.FindBySessionID(ns.ID) ?? new UserOnline();
            olt.UserID = dv.ID;
            olt.SessionID = ns.ID;
            olt.ExternalUri = ns.Remote + "";
            olt.LoginCount++;
            olt.LoginTime = DateTime.Now;

            olt.Save();
            Online = olt;

            // 加密返回的密钥
            var key = Key.RC4(dv.Password.GetBytes()).ToHex();

            if (islogin)
                return new { Name = dv.Name };
            else
                return new { user = dv.Name, pass = dv.Password };
        }
        #endregion
    }
}