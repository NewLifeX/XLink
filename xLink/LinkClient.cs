using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewLife;
using NewLife.Remoting;

namespace xLink
{
    /// <summary>物联客户端</summary>
    public class LinkClient : ApiClient
    {
        #region 属性
        /// <summary>附带登录一起送出的参数</summary>
        public IDictionary<String, Object> Parameters { get; set; } = new Dictionary<String, Object>();
        #endregion

        #region 构造
        public LinkClient(String uri) : base(uri) { }
        #endregion

        #region 打开关闭
        #endregion

        #region 握手
        //public async Task<IDictionary<String, Object>> HelloAsync(Object args = null)
        //{
        //    var dic = args.ToDictionary();

        //    if (!dic.ContainsKey("OS")) dic["OS"] = Runtime.OSName;
        //    if (!dic.ContainsKey("Agent")) dic["Agent"] = $"{Environment.UserName}_{Environment.MachineName}";

        //    try
        //    {
        //        var rs = await InvokeAsync<IDictionary<String, Object>>("Hello", dic);

        //        return rs;
        //    }
        //    catch (ApiException ex)
        //    {
        //        if (ex.Code == 404)
        //        {
        //            //OnRedirect(ex.Message);
        //            return null;
        //        }
        //        throw;
        //    }
        //}
        #endregion

        #region 登录
        /// <summary>是否已登录</summary>
        public Boolean Logined { get; protected set; }

        /// <summary>异步登录</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        public async Task<IDictionary<String, Object>> LoginAsync(String user, String pass)
        {
            if (user.IsNullOrEmpty()) throw new ArgumentNullException(nameof(user), "用户名不能为空！");
            if (pass.IsNullOrEmpty()) throw new ArgumentNullException(nameof(pass), "密码不能为空！");

            // 加密随机数
            var salt = ((Int64)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds).GetBytes();
            var p = salt.RC4(pass.GetBytes()).ToHex();
            p = salt.ToHex() + p;

            // 克隆一份，避免修改原始数据
            var dic = Parameters.ToDictionary(e => e.Key, e => e.Value);
            dic["OS"] = Runtime.OSName;
            dic["Agent"] = $"{Environment.UserName}_{Environment.MachineName}";
            dic.Merge(new { user, pass = p });

            // 声明响应
            IDictionary<String, Object> rs = null;
            var enc = Encoder;
            var code = 0;
            Object result = null;
            while (true)
            {
                //var rs = await InvokeAsync<IDictionary<String, Object>>("Login", new { user, pass = p });
                rs = await InvokeAsync<IDictionary<String, Object>>("Login", dic);

                enc.TryGet(rs, out code, out result);

                // 检查重定向
                if (code == 302)
                {
                    //OnRedirect(ex.Message);
                    // 解析目标地址，合并参数，断开当前连接，建立新连接
                    if (rs.TryGetValue("Location", out result) && result != null)
                    {
                        dic.Merge(rs);

                        var uri = result + "";
                        Client.TryDispose();
                        SetRemote(uri);

                        // 重新登录
                        continue;
                    }
                }

                break;
            }

            //Token = rs.Token;
            //Key = rs.Key.RC4(pass.GetBytes());

            //LoginTime = DateTime.Now;
            //WriteLog("密匙:{1},令牌到手 0x{0:X8}", Token, Key.ToHex());

            //// 登录完成以后，开始心跳，周期调整
            //SetTimerPeriod(20);

            //// 为自动登录做准备
            //UserName = user;
            //Password = pass;

            Logined = true;

            //// 不管登录成功还是失败，都除法OnLogined事件
            //OnLogined?.Invoke(this, EventArgs.Empty);

            return rs;
        }
        #endregion

        #region 注册
        #endregion

        #region 心跳
        #endregion

        #region 业务
        #endregion

        #region 远程调用
        /// <summary>调用</summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task<TResult> InvokeAsync<TResult>(String action, Object args = null)
        {
            //if (Info == null) return action == "Hello";
            if (!Logined)
            {
                if (action != "Login" && action != "Register") throw new Exception("未登录，无权调用" + action);
            }

            return await base.InvokeAsync<TResult>(action, args);
        }
        #endregion
    }
}