using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
using xLink.Device.Entity;
using DeviceX = xLink.Device.Entity.Device;

namespace xLink.Device
{
    /// <summary>设备会话</summary>
    [Api("Device")]
    [DisplayName("设备")]
    public class DeviceSession : LinkSession
    {
        #region 属性
        /// <summary>当前设备</summary>
        public DeviceX Device { get; private set; }

        /// <summary>在线对象</summary>
        public DeviceOnline Online { get; private set; }
        #endregion

        #region 构造
        static DeviceSession()
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
                n = DeviceX.Meta.Count;
                n = DeviceOnline.Meta.Count;
                n = DeviceHistory.Meta.Count;
            });
        }
        #endregion

        #region 登录注册
        /// <summary>登录或注册</summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        protected override Object OnLogin(String user, String pass)
        {
            if (user.IsNullOrEmpty()) throw Error(3, "用户名不能为空");

            var dv = DeviceX.FindByName(user);

            // 注册
            if (dv == null) return Register(user, pass);

            // 登录
            Name = user;

            // 密码有盐值和密文两部分组成
            var salt = pass.Substring(0, pass.Length / 2).ToHex();
            pass = pass.Substring(pass.Length / 2);

            WriteLog("登录 {0} => {1}/{2}", user, salt.ToHex(), pass);

            // 验证密码
            var tpass = dv.Password.GetBytes();
            if (salt.RC4(tpass).ToHex() != pass) throw Error(4, "密码错误");

            var ns = Session as NetSession;
            dv.Logins++;
            dv.LastLogin = DateTime.Now;
            dv.LastLoginIP = ns.Remote.EndPoint.Address + "";

            dv.Save();

            return OnLogin(dv, true);
        }

        /// <summary>注册</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected virtual Object Register(String user, String pass)
        {
            var dv = DeviceX.FindByCode(user);
            if (dv == null) dv = new DeviceX { Code = user };

            dv.Name = user.GetBytes().Crc().GetBytes().ToHex();
            dv.Password = Rand.NextString(8);
            dv.Enable = true;

            Name = dv.Name;
            WriteLog("注册 {0} => {1}/{2}", user, dv.Name, dv.Password);

            var ns = Session as NetSession;

            dv.Registers++;
            dv.RegisterTime = DateTime.Now;
            dv.RegisterIP = ns.Remote.EndPoint.Address + "";

            dv.Online = true;

            dv.Save();

            return OnLogin(dv, false);
        }

        private Object OnLogin(DeviceX dv, Boolean islogin)
        {
            // 随机密钥
            Key = Rand.NextBytes(8);
            Session["Key"] = Key;

            // 当前设备
            Device = dv;

            var ns = Session as NetSession;

            var olt = Online ?? DeviceOnline.FindBySessionID(ns.ID) ?? new DeviceOnline();
            olt.DeviceID = dv.ID;
            olt.SessionID = ns.ID;
            olt.ExternalUri = ns.Remote + "";
            olt.LoginCount++;
            olt.LoginTime = DateTime.Now;

            olt.Save();
            Online = olt;

            // 加密返回的密钥
            var key = Key.RC4(dv.Password.GetBytes()).ToHex();

            if (islogin)
                return new { Key = key };
            else
                return new { Key = key, User = dv.Name, Pass = dv.Password };
        }
        #endregion
    }
}