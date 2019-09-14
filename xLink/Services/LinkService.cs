using NewLife.Log;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Serialization;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using XCode;
using xLink.Models;

namespace xLink.Services
{
    /// <summary>物联服务接口</summary>
    public interface ILinkService
    {
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        Int32 ClearExpire(Int32 secTimeout);
    }

    /// <summary>物联服务</summary>
    public abstract class LinkService : ILinkService, IActionFilter, IApi
    {
        #region 属性
        /// <summary>接口会话</summary>
        public IApiSession Session { get; set; }

        /// <summary>当前用户</summary>
        public IManageUser Current { get; set; }

        /// <summary>在线对象</summary>
        public IOnline Online { get; set; }

        /// <summary>请求参数</summary>
        private IDictionary<String, Object> Parameters { get; set; }
        #endregion

        #region 执行拦截
        void IActionFilter.OnActionExecuting(ControllerContext filterContext)
        {
            Parameters = filterContext.Parameters;

            var act = filterContext.ActionName;
            if (act == nameof(Login) || act.EndsWith("/" + nameof(Login))) return;

            var ns = Session as INetSession;
            if (Session["Current"] is IManageUser user)
                Current = user;
            else
                throw new ApiException(401, "{0}未登录！不能执行{1}".F(ns.Remote, act));

            Online = Session["Online"] as IOnline;
        }

        void IActionFilter.OnActionExecuted(ControllerContext filterContext)
        {
            Session["Current"] = Current;
            Session["Online"] = Online;

            if (filterContext.Exception != null && !filterContext.ExceptionHandled)
            {
                // 显示错误
                var ex = filterContext.Exception;
                if (ex != null)
                {
                    if (ex is ApiException)
                        XTrace.Log.Error(ex.Message);
                    else
                        XTrace.WriteException(ex);
                }
            }
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

            var ps = Parameters;

            // 在线记录
            CheckOnline(user, ps);

            // 登录
            var msg = "登录 {0}/{1}".F(user, pass);

            var flag = true;
            try
            {
                // 查找并登录，找不到用户是返回空，登录失败则抛出异常
                var u = CheckUser(user, pass, ps);
                if (u == null) throw Error(3, user + " 不存在");
                if (!u.Enable) throw Error(4, user + " 已被禁用");

                var rs = SaveLogin(u, ps);

                // 当前用户
                Current = u;

                return rs;
            }
            catch (Exception ex)
            {
                msg += " " + ex?.GetTrue()?.Message;
                flag = false;
                throw;
            }
            finally
            {
                SaveHistory("Login", flag, msg + " " + ps.ToJson());
            }
        }

        /// <summary>查找用户并登录，找不到用户是返回空，登录失败则抛出异常</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="ps">附加参数</param>
        /// <returns></returns>
        protected abstract IManageUser CheckUser(String user, String pass, IDictionary<String, Object> ps);

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="user"></param>
        /// <param name="ps">附加参数</param>
        protected virtual Object SaveLogin(IManageUser user, IDictionary<String, Object> ps)
        {
            var ns = Session as NetSession;
            if (user is IAuthUser au) au.SaveLogin(ns);

            return new { Name = user + "" };
        }
        #endregion

        #region 心跳
        /// <summary>心跳</summary>
        /// <returns></returns>
        [Api(nameof(Ping))]
        public virtual Object Ping()
        {
            var ps = Parameters;

            CheckOnline(Current + "", ps);

            var now = DateTime.Now;

            var dic = ControllerContext.Current.Parameters;
            // 返回服务器时间
            dic["ServerTime"] = now;
            dic["ServerSeconds"] = now.ToInt();

            return dic;
        }

        /// <summary>更新在线信息，登录前、心跳时 调用</summary>
        /// <param name="name"></param>
        /// <param name="ps">附加参数</param>
        protected virtual void CheckOnline(String name, IDictionary<String, Object> ps)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = Online ?? CreateOnline(sid);
            olt.Name = name;
            olt.SessionID = sid;
            olt.UpdateTime = DateTime.Now;

            var user = Current;
            if (user != null)
            {
                olt.UserID = user.ID;
                if (olt.Name.IsNullOrEmpty()) olt.Name = user + "";
            }
            olt.SaveAsync();

            Online = olt;
        }

        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected abstract IOnline CreateOnline(String sessionid);

        /// <summary>保存令牌操作历史</summary>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="content"></param>
        protected virtual void SaveHistory(String action, Boolean success, String content) { }
        #endregion

        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public virtual Int32 ClearExpire(Int32 secTimeout) => 0;
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
            var ns = Session as NetSession;
            ns?.WriteLog(format, args);
        }
        #endregion
    }
}