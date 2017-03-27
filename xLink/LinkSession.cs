using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;

namespace xLink
{
    /// <summary>物联会话</summary>
    [Api(null)]
    public class LinkSession : DisposeBase, IApi
    {
        #region 属性
        /// <summary>Api会话</summary>
        public IApiSession Session { get; set; }

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
        public LinkSession()
        {
        }

        /// <summary>销毁时，从集合里面删除令牌</summary>
        /// <param name="disposing"></param>
        protected override void OnDispose(Boolean disposing)
        {
            base.OnDispose(disposing);

            Session.TryDispose();
        }
        #endregion

        #region 主要方法
        /// <summary>为加解密过滤器提供会话密钥</summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static Byte[] GetKey(FilterContext context)
        {
            var ctx = context as ApiFilterContext;
            var ss = ctx?.Session;
            if (ss == null) return null;

            return ss["Key"] as Byte[];
        }

        /// <summary>设置活跃时间</summary>
        public virtual Boolean SetActive(Byte seq)
        {
            LastActive = DateTime.Now;

            return true;
        }
        #endregion

        #region 异常处理
        /// <summary>抛出异常</summary>
        /// <param name="errCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ApiException Error(Int32 errCode, String msg)
        {
            return new ApiException(errCode, msg);
        }
        #endregion

        #region 登录
        /// <summary>收到登录请求</summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        [Api("Login")]
        protected virtual Object OnLogin(String user, String pass)
        {
            //if (user.IsNullOrEmpty() || pass.IsNullOrEmpty()) throw Error(3, "用户名或密码不能为空");
            if (user.IsNullOrEmpty()) throw Error(3, "用户名不能为空");

            // 账号真实密码
            var truepass = user;

            // 注册与登录
            if (pass.IsNullOrEmpty())
            {
                var old = user;
                user = user.GetBytes().Crc().GetBytes().ToHex();
                truepass = Rand.NextString(8);

                Name = user;
                WriteLog("注册 {0} => {1}/{2}", old, user, truepass);

                var f = "Pass\\{0}.key".F(user).GetFullPath();
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
                var f = "Pass\\{0}.key".F(user).GetFullPath();
                truepass = File.ReadAllText(f);
                if (salt.RC4(truepass.GetBytes()).ToHex() != pass) throw Error(4, "密码错误");
            }

            // 随机密钥
            Key = Rand.NextBytes(8);
            Session["Key"] = Key;

            var rs = new
            {
                // 加密返回的密钥
                Key = Key.RC4(truepass.GetBytes()).ToHex()
            };

            var dic = rs.ToDictionary();
            // 注册需要返回用户名密码
            if (pass.IsNullOrEmpty())
            {
                dic["User"] = user;
                dic["Pass"] = truepass;
            }

            return dic;
        }
        #endregion

        #region 心跳
        /// <summary>心跳</summary>
        /// <returns></returns>
        [Api("Ping")]
        internal protected virtual Object OnPing()
        {
            WriteLog("心跳 ");

            var dic = ControllerContext.Current.Parameters;
            // 返回服务器时间
            dic["ServerTime"] = DateTime.Now;

            return dic;
        }
        #endregion

        #region 远程调用
        /// <summary>远程调用</summary>
        /// <example>
        /// <code>
        /// client.InvokeAsync("GetDeviceCount");
        /// var rs = client.InvokeAsync("GetDeviceInfo", 2, 5, 9);
        /// var di = rs.Result[0].Value;
        /// </code>
        /// </example>
        /// <param name="action"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual async Task<TResult> InvokeAsync<TResult>(String action, Object args = null)
        {
            return await Session.InvokeAsync<TResult>(action, args);
        }
        #endregion

        #region 辅助
        ///// <summary>日志</summary>
        //public ILog Log { get; set; } = Logger.Null;

        /// <summary>写日志</summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WriteLog(String format, params Object[] args)
        {
            //Log?.Info(Name + " " + format, args);
            var ns = Session as NetSession;
            ns.WriteLog(Name + " " + format, args);
        }
        #endregion
    }
}