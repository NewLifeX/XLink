using NewLife.Log;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using XCode;
using xLink.Models;

namespace xLink
{
    /// <summary>物联会话</summary>
    [Api(null)]
    public abstract class LinkSession : IApi, IActionFilter
    {
        #region 属性
        /// <summary>名称</summary>
        public String Name { get; set; }

        /// <summary>类型</summary>
        public String Type { get; set; }

        /// <summary>版本</summary>
        public String Version { get; set; }

        /// <summary>当前登录</summary>
        public IManageUser Current { get; private set; }

        /// <summary>在线对象</summary>
        public IOnline Online { get; private set; }
        #endregion

        #region 登录
        /// <summary>会话</summary>
        public IApiSession Session { get; set; }

        void IActionFilter.OnActionExecuting(ControllerContext filterContext)
        {
            var act = filterContext.ActionName;
            if (act == nameof(Login)) return;

            var ns = Session as INetSession;
            if (Session["User"] is IManageUser user)
            {
                Current = user;

                var online = GetOnline(user, ns);
                online.UpdateTime = TimerX.Now;
                online.SaveAsync();

                Online = online;
            }
            else
            {
                throw new ApiException(401, "{0}未登录！不能执行{1}".F(ns.Remote, act));
            }
        }

        void IActionFilter.OnActionExecuted(ControllerContext filterContext)
        {
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

        /// <summary>登录</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [Api(nameof(Login))]
        public virtual Object Login(String user, String pass)
        {
            //if (user.IsNullOrEmpty()) throw new ApiException(3, "用户名不能为空");

            WriteLog("登录 {0}/{1}", user, pass);

            var ps = ControllerContext.Current.Parameters.ToNullable();
            var ns = Session as INetSession;

            var text = "{0} 登录".F(user);
            var success = true;
            try
            {
                //if (ps != null)
                //{
                //    Agent = ps[nameof(Agent)] + "";
                //    Type = ps[nameof(Type)] + "";
                //    Version = ps[nameof(Version)] + "";
                //}

                Name = user;

                var u = OnLogin(user, pass, ns, ps);
                if (u is IAuthUser au) au.SaveLogin(ns);

                // 保存
                if (u is IEntity entity) entity.Save();

                // 上线
                CreateOnline(u, ns, ps);

                // 记录当前用户
                Session["User"] = u;

                return u;
            }
            catch (Exception ex)
            {
                text += " " + ex?.GetTrue()?.Message;
                success = false;
                throw;
            }
            finally
            {
                SaveHistory(nameof(Login), success, text);
            }
        }

        /// <summary>登录</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="session"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected abstract IManageUser OnLogin(String user, String pass, INetSession session, IDictionary<String, Object> parameters);
        #endregion

        #region 读写
        #endregion

        #region 在线历史
        private IOnline CreateOnline(IManageUser user, INetSession ns, IDictionary<String, Object> parameters)
        {
            var ip = ns.Remote.Host;

            var ps = parameters;

            var online = GetOnline(user, ns);
            //online.Client = $"{(ip.IsNullOrEmpty() ? machine : ip)}@{pid}";
            //online.Name = machine;
            //online.UpdateIP = ip;
            //online.Version = ps["Version"] +"";
            //online.Compile = ps["Version"] + "";
            //online.ProtocolVersion = pVersion;
            //online.Server = Local + "";

            online.Save();

            // 真正的用户
            Session["Online"] = online;

            // 下线
            ns.OnDisposed += (s, e) => online.Delete();

            return online;
        }

        private IOnline GetOnline(IManageUser user, INetSession ns)
        {
            if (Session["Online"] is IOnline online) return online;

            var ip = ns.Remote.Host;
            var ins = ns.Remote.EndPoint + "";
            //online = AppOnline.FindByInstance(ins) ?? new AppOnline { CreateIP = ip };
            //online.AppID = user.ID;
            //online.Instance = ins;
            online = GetOrAddOnline(user, ns);

            return online;
        }

        /// <summary>获取 或 添加 在线记录</summary>
        /// <param name="user"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected abstract IOnline GetOrAddOnline(IManageUser user, INetSession ns);

        /// <summary>保存历史</summary>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="content"></param>
        protected virtual void SaveHistory(String action, Boolean success, String content)
        {
            var history = CreateHistory();
            history.Name = Name;
            history.Type = Type;

            var user = Session["User"] as IManageUser;
            var online = Session["Online"] as IOnline;
            if (user != null)
            {
                if (history.UserID == 0) history.UserID = user.ID;
                if (history.Name.IsNullOrEmpty()) history.Name = user + "";
            }
            else if (online != null)
            {
                if (history.UserID == 0) history.UserID = online.UserID;
            }

            history.Action = action;
            history.Success = success;
            history.Remark = content;
            history.CreateTime = DateTime.Now;

            history.CreateIP = online.CreateIP;

            history.SaveAsync();
        }

        /// <summary>创建历史对象</summary>
        /// <returns></returns>
        protected abstract IHistory CreateHistory();
        #endregion

        #region 日志
        /// <summary>日志</summary>
        public ILog Log { get; set; } = Logger.Null;

        /// <summary>写日志</summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WriteLog(String format, params Object[] args)
        {
            Log?.Info(format, args);
        }
        #endregion
    }
}