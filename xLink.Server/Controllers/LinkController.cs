using System;
using System.Collections.Generic;
using System.Linq;
using NewLife.Remoting;
using xLink.Models;
using xLink.Server.Models;

namespace xLink
{
    class LinkController<TSession> : IActionFilter where TSession : LinkSession, new()
    {
        #region 属性
        /// <summary>会话</summary>
        public TSession Session { get; set; }
        #endregion

        #region 执行拦截
        void IActionFilter.OnActionExecuting(ControllerContext filterContext)
        {
            var ss = filterContext.Session;
            Session = ss["Current"] as TSession ?? new TSession();
            Session.Session = ss;
        }

        void IActionFilter.OnActionExecuted(ControllerContext filterContext)
        {
            var ss = filterContext.Session;
            ss["Current"] = Session;
        }
        #endregion

        #region 登录
        /// <summary>收到登录请求</summary>
        /// <param name="user">用户名</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        [Api(nameof(Login))]
        public virtual Object Login(String user, String pass)
        {
            if (user.IsNullOrEmpty()) throw Error(3, "用户名不能为空");

            var ss = Session;
            WriteLog("登录 {0}/{1}", user, pass);

            // 注册与登录
            var rs = ss.CheckLogin(user, pass);

            // 可能是注册
            var dic = rs.ToDictionary();
            if (dic.ContainsKey(nameof(user))) user = dic[nameof(user)] + "";
            //if (dic.ContainsKey(nameof(pass))) pass = dic[nameof(pass)] + "";

            // 登录会话
            ss.Logined = true;

            return dic;
        }
        #endregion

        #region 心跳
        /// <summary>心跳</summary>
        /// <returns></returns>
        [Api(nameof(Ping))]
        public virtual Object Ping()
        {
            var ss = Session;
            WriteLog("心跳 ");

            ss.CheckOnline(ss.Current + "");

            var now = DateTime.Now;

            var dic = ControllerContext.Current.Parameters;
            // 返回服务器时间
            dic["ServerTime"] = now;
            dic["ServerSeconds"] = now.ToInt();

            return dic;
        }
        #endregion

        #region 读写
        public Func<String, Byte[]> GetData;
        public Action<String, Byte[]> SetData;

        /// <summary>收到写入请求</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="data"></param>
        [Api("Write")]
        public virtual DataModel OnWrite(String id, Int32 start, String data)
        {
            var buf = GetData?.Invoke(id);
            if (buf == null) throw new ApiException(405, "找不到设备！");

            var ds = data.ToHex();

            // 检查扩容
            if (start + ds.Length > buf.Length)
            {
                var buf2 = new Byte[start + ds.Length];
                buf2.Write(0, buf);
                buf = buf2;
            }
            buf.Write(start, ds);
            buf[0] = (Byte)buf.Length;

            // 保存回去
            SetData?.Invoke(id, buf);

            return new DataModel { ID = id, Start = 0, Data = buf.ToHex() };
        }

        /// <summary>收到读取请求</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        [Api("Read")]
        public virtual DataModel OnRead(String id, Int32 start, Int32 count)
        {
            var buf = GetData?.Invoke(id);
            if (buf == null) throw new ApiException(405, "找不到设备！");

            return new DataModel { ID = id, Start = start, Data = buf.ReadBytes(start, count).ToHex() };
        }
        #endregion

        #region 异常处理
        /// <summary>抛出异常</summary>
        /// <param name="errCode"></param>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ApiException Error(Int32 errCode, String msg, Object result = null)
        {
            var ex = new ApiException(errCode, msg);
            if (result != null)
            {
                // 支持自定义类型
                foreach (var item in result.ToDictionary())
                {
                    ex.Data[item.Key] = item.Value;
                }
            }

            return ex;
        }
        #endregion

        #region 辅助
        /// <summary>写日志</summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void WriteLog(String format, params Object[] args)
        {
            var ns = Session as LinkSession;
            ns?.WriteLog(format, args);
        }
        #endregion
    }
}