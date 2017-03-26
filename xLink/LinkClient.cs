using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife;
using NewLife.Remoting;

namespace xLink
{
    /// <summary>物联客户端</summary>
    public class LinkClient : DisposeBase
    {
        #region 属性
        /// <summary>Api客户端</summary>
        public ApiClient Client { get; set; }
        #endregion

        #region 构造
        /// <summary>销毁</summary>
        /// <param name="disposing"></param>
        protected override void OnDispose(Boolean disposing)
        {
            base.OnDispose(disposing);

            var reason = "{0}{1}".F(GetType().Name, disposing ? "主动销毁" : "GC");
            Close(reason);
        }
        #endregion

        #region 打开关闭
        /// <summary>打开</summary>
        public void Open()
        {
            Client.Open();
        }

        /// <summary>关闭</summary>
        /// <param name="reason"></param>
        public void Close(String reason)
        {
            Client.Close(reason);
        }
        #endregion

        #region 握手
        public async Task<IDictionary<String, Object>> HelloAsync(IDictionary<String, Object> args)
        {
            if (args == null) args = new Dictionary<String, Object>();

            if (!args.ContainsKey("OS")) args["OS"] = Runtime.OSName;
            if (!args.ContainsKey("Agent")) args["Agent"] = $"{Environment.UserName}_{Environment.MachineName}";

            var rs = await Client.InvokeAsync<IDictionary<String, Object>>("Hello", args);

            return rs;
        }
        #endregion

        #region 登录
        #endregion

        #region 注册
        #endregion

        #region 心跳
        #endregion

        #region 业务
        #endregion
    }
}