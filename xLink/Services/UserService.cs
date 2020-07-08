using NewLife;
using NewLife.Log;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Serialization;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using xLink.Entity;
using xLink.Models;

namespace xLink.Services
{
    /// <summary>用户服务</summary>
    public class UserService : LinkService, IActionFilter
    {
        #region 属性
        /// <summary>当前用户</summary>
        public IManageUser Current { get; set; }

        /// <summary>在线对象</summary>
        public IOnline Online { get; set; }
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
            //if (user.IsNullOrEmpty()) throw Error(3, "用户名不能为空");

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

                //var rs = SaveLogin(u, ps);
                var ns = Session as NetSession;
                if (u is IAuthUser au) au.SaveLogin(ns);
                var rs = new { Name = user + "" };

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
        protected virtual IManageUser CheckUser(String user, String pass, IDictionary<String, Object> ps)
        {
            var u = User.FindByName(user);
            if (u == null)
            {
                var ns = Session as INetSession;
                u = new User
                {
                    Name = user,
                    Password = pass.MD5(),
                    Enable = true,

                    CreateIP = ns?.Remote.Address + "",
                    CreateTime = DateTime.Now,
                };
                //u.SaveRegister(Session as INetSession);

                u.Insert();

                return u;
            }

            // 验证密码
            //u.CheckMD5(pass);
            if (u.Password != pass.MD5()) throw new XException("密码错误！");

            return u;
        }
        #endregion

        #region 心跳历史
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
            //if (olt is UserOnline dolt) Fill(dolt, ps);
            olt.Name = name;
            olt.SessionID = sid;
            olt.UpdateTime = DateTime.Now;

            Online = olt;
        }

        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected virtual IOnline CreateOnline(String sessionid)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = UserOnline.GetOrAdd(sid);
            olt.ExternalUri = ns.Remote + "";

            if (Current is User user)
            {
                //olt.DeviceId = user.ID;
                //olt.ProductId = user.ProductId;
                olt.Name = user.Name;

                olt.Version = user.Version;
                //olt.CompileTime = user.CompileTime;
                //olt.Memory = user.Memory;
                //olt.MACs = user.MACs;
                //olt.COMs = user.COMs;

                olt.CreateIP = ns.Remote.Address + "";
            }

            olt.SaveAsync();

            return olt;
        }

        /// <summary>保存操作历史</summary>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="content"></param>
        protected virtual void SaveHistory(String action, Boolean success, String content)
        {
            var hi = new UserHistory();

            if (Current is User user)
            {
                if (hi.UserID == 0) hi.UserID = user.ID;
                if (hi.Name.IsNullOrEmpty()) hi.Name = user + "";

                hi.Version = user.Version;
                //hi.CompileTime = user.CompileTime;
            }
            else if (Online is UserOnline olt)
            {
                if (hi.UserID == 0) hi.UserID = olt.UserID;
                if (hi.Name.IsNullOrEmpty()) hi.Name = olt.Name;
            }

            hi.Action = action;
            hi.Success = success;
            hi.Remark = content;
            hi.CreateTime = DateTime.Now;

            if (Session is INetSession ns) hi.CreateIP = ns.Remote + "";

            hi.SaveAsync();
        }
        #endregion

        #region 清理超时
        /// <summary>清理超时会话</summary>
        /// <param name="secTimeout"></param>
        /// <returns></returns>
        public override Int32 ClearExpire(Int32 secTimeout) => UserOnline.ClearExpire(secTimeout).Count;
        #endregion
    }
}