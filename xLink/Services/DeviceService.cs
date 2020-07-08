using NewLife;
using NewLife.Log;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
using NewLife.Serialization;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using xLink.Entity;
using xLink.Models;

namespace xLink.Services
{
    /// <summary>设备服务</summary>
    public class DeviceService : LinkService, IActionFilter
    {
        #region 属性
        /// <summary>当前用户</summary>
        public Device Current { get; set; }

        /// <summary>在线对象</summary>
        public DeviceOnline Online { get; set; }
        #endregion

        #region 执行拦截
        void IActionFilter.OnActionExecuting(ControllerContext filterContext)
        {
            Parameters = filterContext.Parameters;

            var act = filterContext.ActionName;
            if (act == nameof(Login) || act.EndsWith("/" + nameof(Login))) return;

            var ns = Session as INetSession;
            if (Session["Current"] is Device user)
                Current = user;
            else
                throw new ApiException(401, "{0}未登录！不能执行{1}".F(ns.Remote, act));

            Online = Session["Online"] as DeviceOnline;
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

        #region 登录注册
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
        protected virtual Device CheckUser(String user, String pass, IDictionary<String, Object> ps)
        {
            var dv = Device.FindByCode(user);
            if (dv == null)
            {
                if (!user.IsNullOrEmpty()) throw Error(10, "找不到设备");

                // 找到产品，尝试自动注册
                var pkey = ps["ProductKey"] + "";
                var psecret = ps["ProductSecret"] + "";
                var prd = Product.FindByCode(pkey);
                if (prd == null) throw Error(11, "找不到设备和产品");
                if (prd.Secret.MD5() != psecret) throw Error(12, "产品鉴权失败");
                if (!prd.Enable || !prd.AutoRegister) throw Error(13, $"产品[{prd}]未启用动态注册");

                // 根据唯一编码找设备
                var uuid = ps["UUID"] + "";
                var guid = ps["MachineGuid"] + "";
                dv = Device.FindByUuid(uuid, prd.ID) ?? Device.FindByMachineGuid(guid, prd.ID);

                var ns = Session as INetSession;
                var name = user;
                if (name.IsNullOrEmpty()) name = ps["MachineName"] + "";
                if (name.IsNullOrEmpty()) name = ps["UserName"] + "";

                if (dv == null) dv = new Device
                {
                    Enable = true,

                    CreateIP = ns?.Remote.Address + "",
                    CreateTime = DateTime.Now,
                };

                dv.ProductId = prd.ID;
                dv.Name = name;
                dv.Code = Rand.NextString(8);
                dv.Secret = Rand.NextString(16);
                dv.UpdateIP = ns?.Remote.Address + "";
                dv.UpdateTime = DateTime.Now;

                dv.Save();

                Session["AutoRegister"] = true;

                return dv;
            }

            // 验证密码
            //u.CheckMD5(pass);
            //u.CheckEqual(pass);
            if (dv.Secret.MD5() != pass) throw Error(9, "密码错误！");

            return dv;
        }

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="dv"></param>
        /// <param name="ps">附加参数</param>
        protected virtual Object SaveLogin(Device dv, IDictionary<String, Object> ps)
        {
            Fill(dv, ps);

            if (Online is DeviceOnline olt) olt.ProductId = dv.ProductId;

            var ns = Session as INetSession;

            dv.Logins++;
            dv.LastLoginIP = ns.Remote.Address + "";
            dv.LastLogin = DateTime.Now;

            dv.Save();

            //// 一分钟之类注册，返回编码
            //if (dv.CreateTime.AddSeconds(60) > DateTime.Now)
            if (Session["AutoRegister"].ToBoolean())
            {
                return new
                {
                    Name = dv + "",
                    DeviceKey = dv.Code,
                    DeviceSecret = dv.Secret,
                };
            }

            return new { Name = dv + "" };
        }

        private void Fill(Device dv, IDictionary<String, Object> ps)
        {
            var os = ps["os"] + "";
            if (os.IsNullOrEmpty()) os = ps["osName"] + "";
            var osVersion = ps["osVersion"] + "";
            var version = ps["version"] + "";
            var compile = ps["compile"].ToDateTime();

            if (!os.IsNullOrEmpty()) dv.OS = os;
            if (!osVersion.IsNullOrEmpty()) dv.OSVersion = osVersion;
            if (!version.IsNullOrEmpty()) dv.Version = version;
            if (compile.Year > 2000) dv.CompileTime = compile;

            var str = ps["MachineName"] + "";
            if (!str.IsNullOrEmpty()) dv.MachineName = str;

            str = ps["UserName"] + "";
            if (!str.IsNullOrEmpty()) dv.UserName = str;

            str = ps["Processor"] + "";
            if (!str.IsNullOrEmpty()) dv.Processor = str;

            str = ps["CpuID"] + "";
            if (!str.IsNullOrEmpty()) dv.CpuID = str;

            str = ps["UUID"] + "";
            if (!str.IsNullOrEmpty()) dv.Uuid = str;

            str = ps["MachineGuid"] + "";
            if (!str.IsNullOrEmpty()) dv.MachineGuid = str;

            var n = ps["CPU"].ToInt();
            if (n > 0) dv.Cpu = n;

            var m = ps["Memory"].ToLong();
            if (m > 0) dv.Memory = (Int32)(m / 1024 / 1024);

            str = ps["MACs"] + "";
            if (!str.IsNullOrEmpty()) dv.MACs = str;

            str = ps["COMs"] + "";
            if (!str.IsNullOrEmpty()) dv.COMs = str;

            str = ps["InstallPath"] + "";
            if (!str.IsNullOrEmpty()) dv.InstallPath = str;

            str = ps["Runtime"] + "";
            if (!str.IsNullOrEmpty()) dv.Runtime = str;
        }

        private void Fill(DeviceOnline olt, IDictionary<String, Object> ps)
        {
            var m = ps["AvailableMemory"].ToLong();
            if (m > 0) olt.AvailableMemory = (Int32)(m / 1024 / 1024);

            var d = ps["CpuRate"].ToDouble();
            if (d > 0) olt.CpuRate = d;

            var n = ps["Delay"].ToInt();
            if (n > 0) olt.Delay = n;

            var dt = ps["Time"].ToDateTime();
            if (dt.Year > 2000)
            {
                olt.LocalTime = dt;
                olt.Offset = (Int32)Math.Round((dt - DateTime.Now).TotalSeconds);
            }

            var str = ps["Processes"] + "";
            if (!str.IsNullOrEmpty()) olt.Processes = str;

            str = ps["MACs"] + "";
            if (!str.IsNullOrEmpty()) olt.MACs = str;

            str = ps["COMs"] + "";
            if (!str.IsNullOrEmpty()) olt.COMs = str;

            str = ps["LocalIP"] + "";
            //if (!str.IsNullOrEmpty()) olt.InternalUri = str;
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

            var olt = (Online ?? CreateOnline(sid)) as DeviceOnline;

            if (Current is Device dv)
            {
                olt.DeviceId = dv.ID;
                olt.ProductId = dv.ProductId;
                olt.Name = dv.Name;

                olt.Version = dv.Version;
                olt.MACs = dv.MACs;
                olt.COMs = dv.COMs;

                olt.CreateIP = ns.Remote.Address + "";
            }

            Fill(olt, ps);

            olt.PingCount++;

            Online = olt;
        }

        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected virtual DeviceOnline CreateOnline(String sessionid)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = DeviceOnline.GetOrAdd(sid);
            olt.CreateIP = ns.Remote + "";

            olt.SaveAsync();

            return olt;
        }

        /// <summary>保存操作历史</summary>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="content"></param>
        protected virtual void SaveHistory(String action, Boolean success, String content)
        {
            var hi = new DeviceHistory();

            if (Current is Device dv)
            {
                if (hi.DeviceId == 0) hi.DeviceId = dv.ID;
                if (hi.Name.IsNullOrEmpty()) hi.Name = dv + "";

                hi.Version = dv.Version;
            }
            else if (Online is DeviceOnline olt)
            {
                if (hi.DeviceId == 0) hi.DeviceId = olt.DeviceId;
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
        public override Int32 ClearExpire(Int32 secTimeout) => DeviceOnline.ClearExpire(secTimeout).Count;
        #endregion
    }
}