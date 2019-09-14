using NewLife;
using NewLife.Model;
using NewLife.Net;
using NewLife.Security;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using xLink.Entity;
using xLink.Models;

namespace xLink.Services
{
    /// <summary>设备服务</summary>
    public class DeviceService : LinkService
    {
        #region 属性
        #endregion

        #region 构造
        static DeviceService()
        {
            // 异步初始化数据
            ThreadPoolX.QueueUserWorkItem(() =>
            {
                var n = 0;
                n = Device.Meta.Count;
                n = DeviceOnline.Meta.Count;
                n = DeviceHistory.Meta.Count;
            });
        }

        /// <summary>实例化</summary>
        public DeviceService()
        {
        }
        #endregion

        #region 登录注册
        /// <summary>查找用户并登录，找不到用户是返回空，登录失败则抛出异常</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="ps">附加参数</param>
        /// <returns></returns>
        protected override IManageUser CheckUser(String user, String pass, IDictionary<String, Object> ps)
        {
            var u = Device.FindByCode(user);
            if (u == null)
            {
                var ns = Session as INetSession;
                u = new Device
                {
                    Name = user,
                    Code = Rand.NextString(8),
                    Secret = Rand.NextString(16),
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
            //u.CheckEqual(pass);
            if (u.Secret.MD5() != pass) throw new XException("密码错误！");

            return u;
        }

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="user"></param>
        /// <param name="ps">附加参数</param>
        protected override Object SaveLogin(IManageUser user, IDictionary<String, Object> ps)
        {
            if (!(user is Device dv)) return base.SaveLogin(user, ps);

            Fill(dv, ps);

            if (Online is DeviceOnline olt) olt.ProductID = dv.ProductID;

            var ns = Session as INetSession;

            dv.Logins++;
            dv.LastLoginIP = ns.Remote.Address + "";
            dv.LastLogin = DateTime.Now;

            dv.Save();

            // 一分钟之类注册，返回编码
            if (dv.CreateTime.AddSeconds(60) > DateTime.Now)
            {
                return new
                {
                    Name = user + "",
                    dv.Code,
                    dv.Secret,
                };
            }

            return new { Name = user + "" };
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
        }
        #endregion

        #region 心跳历史
        /// <summary>更新在线信息，登录前、心跳时 调用</summary>
        /// <param name="name"></param>
        /// <param name="ps">附加参数</param>
        protected override void CheckOnline(String name, IDictionary<String, Object> ps)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = Online ?? CreateOnline(sid);
            if (olt is DeviceOnline dolt) Fill(dolt, ps);

            Online = olt;

            base.CheckOnline(name, ps);
        }

        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected override IOnline CreateOnline(String sessionid)
        {
            var ns = Session as NetSession;
            var sid = ns.Remote.EndPoint + "";

            var olt = DeviceOnline.GetOrAdd(sid);
            olt.ExternalUri = ns.Remote + "";

            if (Current is Device dv)
            {
                olt.DeviceID = dv.ID;
                olt.ProductID = dv.ProductID;
                olt.Name = dv.Name;

                olt.Version = dv.Version;
                olt.CompileTime = dv.CompileTime;
                olt.Memory = dv.Memory;
                olt.MACs = dv.MACs;
                olt.COMs = dv.COMs;

                olt.CreateIP = ns.Remote.Address + "";
            }

            olt.SaveAsync();

            return olt;
        }

        /// <summary>保存操作历史</summary>
        /// <param name="action"></param>
        /// <param name="success"></param>
        /// <param name="content"></param>
        protected override void SaveHistory(String action, Boolean success, String content)
        {
            var hi = new DeviceHistory();

            if (Current is Device dv)
            {
                if (hi.DeviceID == 0) hi.DeviceID = dv.ID;
                if (hi.Name.IsNullOrEmpty()) hi.Name = dv + "";

                hi.Version = dv.Version;
                hi.CompileTime = dv.CompileTime;
            }
            else if (Online is DeviceOnline olt)
            {
                if (hi.DeviceID == 0) hi.DeviceID = olt.DeviceID;
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