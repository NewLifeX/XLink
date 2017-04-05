using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NewLife;
using NewLife.Data;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;

namespace xLink
{
    /// <summary>物联会话</summary>
    [Api(null)]
    public class LinkSession : ApiSession
    {
        #region 属性
        /// <summary>名称</summary>
        public String Name { get; set; }

        /// <summary>加密通信指令中负载数据的密匙</summary>
        public Byte[] Key { get; set; }

        /// <summary>附加参数</summary>
        public IDictionary<String, Object> Parameters { get; set; } = new Dictionary<String, Object>();

        /// <summary>登录时间</summary>
        public DateTime LoginTime { get; set; }

        /// <summary>最后活跃时间</summary>
        public DateTime LastActive { get; set; }

        /// <summary>指令超时时间。默认5000ms</summary>
        public Int32 Timeout { get; set; } = 5000;
        #endregion

        #region 构造
        /// <summary>构造令牌会话，指定输出日志</summary>
        public LinkSession() { }
        #endregion

        #region 主要方法
        /// <summary>设置活跃时间</summary>
        public virtual Boolean SetActive(Byte seq)
        {
            LastActive = DateTime.Now;

            return true;
        }
        #endregion

        #region 登录
        /// <summary>检查登录，默认检查密码MD5散列，可继承修改</summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <returns>返回要发给客户端的对象</returns>
        protected override Object CheckLogin(String user, String pass)
        {
            // 账号真实密码
            var truepass = user;

            // 注册与登录
            var f = "Pass\\{0}.key".F(user).GetFullPath();
            if (pass.IsNullOrEmpty() || !File.Exists(f))
            {
                var old = user;
                if (user.Length != 8) user = user.GetBytes().Crc().GetBytes().ToHex();
                truepass = Rand.NextString(8);
                pass = null;

                Name = user;
                WriteLog("注册 {0} => {1}/{2}", old, user, truepass);

                f.EnsureDirectory();
                File.WriteAllText(f, truepass);
            }
            else
            {
                Name = user;

                // 密码有盐值和密文两部分组成
                var salt = pass.Substring(0, pass.Length / 2).ToHex();
                pass = pass.Substring(pass.Length / 2);

                WriteLog("登录 {0} => {1}/{2}", user, salt.ToHex(), pass);

                // 验证密码
                truepass = File.ReadAllText(f);
                if (salt.RC4(truepass.GetBytes()).ToHex() != pass)
                {
                    File.Delete(f);
                    throw Error(4, "密码错误");
                }
            }

            Session["_TruePass"] = truepass;

            // 注册需要返回用户名密码
            if (pass.IsNullOrEmpty()) return new { user, pass = truepass };

            return new { user };
        }
        #endregion
    }
}