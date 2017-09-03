using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NewLife;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using xLink.Models;

namespace xLink
{
    /// <summary>物联客户端</summary>
    [Api(null, false)]
    public class LinkClient : ApiClient
    {
        #region 属性
        /// <summary>远程地址</summary>
        public NetUri Remote { get; set; }

        /// <summary>附加参数</summary>
        public IDictionary<String, Object> Parameters { get; set; } = new Dictionary<String, Object>();
        #endregion

        #region 构造
        public LinkClient(String uri)
        {
            Remote = uri;

            // 初始数据
            var dic = Parameters;
            dic["OS"] = Runtime.OSName;
            dic["Agent"] = $"{Environment.UserName}_{Environment.MachineName}";

            var asmx = AssemblyX.Create(Assembly.GetCallingAssembly());
            dic["Version"] = asmx.Version;

            // 注册当前类所有接口
            Manager.Register(this, null, true);
            //Register(this, nameof(OnWrite));
        }
        #endregion

        #region 打开关闭
        /// <summary>开始处理数据</summary>
        public override Boolean Open()
        {
            if (Active) return true;

            if (Remote == null) throw new ArgumentNullException(nameof(Remote));

            SetRemote(Remote + "");

            WriteLog("连接服务器 {0}", Remote);

            if (!base.Open()) return false;


            return Active;
        }
        #endregion

        #region 登录
        /// <summary>预登录。参数准备</summary>
        /// <returns></returns>
        protected override Object OnPreLogin()
        {
            //!!! 安全起见，强烈建议不用传输明文密码
            var user = UserName;
            var pass = Password;
            if (user.IsNullOrEmpty()) throw new ArgumentNullException(nameof(user), "用户名不能为空！");
            //if (pass.IsNullOrEmpty()) throw new ArgumentNullException(nameof(pass), "密码不能为空！");

            // 加密随机数，密码为空表示注册
            var p = "";
            if (!pass.IsNullOrEmpty())
            {
                var salt = ((Int64)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds).GetBytes();
                p = salt.RC4(pass.GetBytes()).ToHex();
                p = salt.ToHex() + p;
            }

            // 克隆一份，避免修改原始数据
            var dic = new { user, pass = p }.ToDictionary();
            dic.Merge(Parameters, false);

            return dic;
        }

        protected override async Task<Object> OnLogin(Object args)
        {
            var dic = args.ToDictionary();
            var rs = dic;

            // 循环，支持重定向
            while (true)
            {
                rs = (await base.OnLogin(dic)).ToDictionary().ToNullable();

                // 注册返回
                if (rs.ContainsKey("User"))
                {
                    UserName = rs["User"] + "";
                    Password = rs["Pass"] + "";

                    // 重新构造参数
                    dic = OnPreLogin().ToDictionary();
                }

                // 检查重定向，解析目标地址，合并参数，断开当前连接，建立新连接
                var uri = rs["Location"] + "";
                if (!uri.IsNullOrEmpty())
                {
                    dic.Merge(rs, true, new String[] { "Key", "User", "Pass", "Location" });

                    Key = null;
                    SetRemote(uri);

                    // 重新登录
                    continue;
                }

                break;
            }

            return rs;
        }
        #endregion

        #region 心跳
        #endregion

        #region 读写
        /// <summary>写入数据，返回整个数据区</summary>
        /// <param name="start"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Byte[]> Write(Int32 start, params Byte[] data)
        {
            var rs = await InvokeAsync<DataModel>("Write", new { start, data = data.ToHex() });
            return rs.Data.ToHex();
        }

        /// <summary>收到写入请求</summary>
        /// <param name="start"></param>
        /// <param name="data"></param>
        [Api("Write")]
        private void OnWrite(Int32 start, String data)
        {
            WriteLog("start={0} data={1}", start, data);
        }

        /// <summary>读取对方数据</summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<Byte[]> Read(Int32 start, Int32 count)
        {
            var rs = await InvokeAsync<DataModel>("Read", new { start, count });
            return rs.Data.ToHex();
        }

        /// <summary>收到读取请求</summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        [Api("Read")]
        private void OnRead(Int32 start, Int32 count)
        {
            WriteLog("start={0} count={1}", start, count);
        }
        #endregion

        #region 业务
        #endregion
    }
}