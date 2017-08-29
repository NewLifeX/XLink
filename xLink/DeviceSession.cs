using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
using NewLife.Serialization;
using NewLife.Threading;
using XCode.Remoting;
using xLink.Entity;

namespace xLink
{
    /// <summary>设备会话</summary>
    [Api("Device", true)]
    [DisplayName("设备")]
    public class DeviceSession : LinkSession
    {
        #region 属性
        /// <summary>当前设备</summary>
        public Device Device { get; private set; }

        ///// <summary>在线对象</summary>
        //public DeviceOnline Online { get; private set; }
        #endregion

        #region 构造
        static DeviceSession()
        {
            // 异步初始化数据
            Task.Run(() =>
            {
                var n = 0;
                n = Device.Meta.Count;
                n = DeviceOnline.Meta.Count;
                n = DeviceHistory.Meta.Count;
            });
        }
        #endregion

        #region 登录注册
        /// <summary>查找用户并登录，找不到用户是返回空，登录失败则抛出异常</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected override IManageUser CheckUser(String user, String pass)
        {
            var u = Device.FindByName(user);
            if (u == null) return null;

            // 登录
            Name = user;

            WriteLog("登录 {0} => {1}", user, pass);

            // 验证密码
            u.CheckRC4(pass);

            var olt = Online as DeviceOnline;
            olt.LoginTime = DateTime.Now;
            olt.LoginCount++;

            return u;
        }

        /// <summary>注册，登录找不到用户时调用注册，返回空表示禁止注册</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected override IManageUser Register(String user, String pass)
        {
            var u = Device.FindByCode(user);
            if (u == null) u = new Device { Code = user };

            u.Name = user.GetBytes().Crc().GetBytes().ToHex();
            u.Password = Rand.NextString(8);
            u.Enable = true;
            u.Registers++;

            Name = u.Name;
            WriteLog("注册 {0} => {1}/{2}", user, u.Name, u.Password);

            return u;
        }

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="user"></param>
        protected override void SaveLogin(IManageUser user)
        {
            var u = user as Device;
            u.Type = Type;
            u.Version = Version;
            if (u.NickName.IsNullOrEmpty()) u.NickName = Agent;

            var dic = ControllerContext.Current?.Parameters?.ToNullable();
            if (dic != null)
            {
                var olt = Online as DeviceOnline;
                // 本地地址
                olt.InternalUri = dic["ip"] + "";
            }

            // 检查下发指令
            //Task.Run(() => CheckCommandAsync());
            TimerX.Delay(CheckCommand, 100);

            base.SaveLogin(user);
        }

        /// <summary>心跳</summary>
        /// <returns></returns>
        protected override Object OnPing()
        {
            var olt = Online as DeviceOnline;
            olt.PingCount++;

            // 检查下发指令
            //Task.Run(() => CheckCommandAsync());
            TimerX.Delay(CheckCommand, 100);

            return base.OnPing();
        }
        #endregion

        #region 操作历史
        /// <summary>创建在线</summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        protected override IOnline CreateOnline(Int32 sessionid)
        {
            var ns = Session as NetSession;

            var olt = DeviceOnline.FindBySessionID(sessionid) ?? new DeviceOnline();
            olt.Version = Version;
            olt.ExternalUri = ns.Remote + "";

            return olt;
        }

        /// <summary>创建历史</summary>
        /// <returns></returns>
        protected override IHistory CreateHistory()
        {
            var hi = new DeviceHistory();
            hi.Version = Version;
            hi.Name = Current + "";

            return hi;
        }
        #endregion

        #region 下发指令
        private void CheckCommand(Object state)
        {
            var dv = Current as Device;
            if (dv == null) return;

            var list = DeviceCommand.GetCommands(dv.ID, 0, 100);
            foreach (var item in list)
            {
                // 尚未到开始时间
                if (item.StartTime > DateTime.MinValue && item.StartTime > DateTime.Now) continue;

                // 过期指令
                if (item.EndTime > DateTime.MinValue && item.EndTime < DateTime.Now)
                {
                    item.Status = CommandStatus.取消;
                }
                else
                {
                    try
                    {
                        var args = (item.Argument + "").Trim();
                        Object obj = args;
                        if (!args.IsNullOrEmpty() && args.StartsWith("{") && args.EndsWith("}"))
                        {
                            obj = new JsonParser(args).Decode();
                        }

                        var dic = Session.InvokeAsync<IDictionary<String, Object>>(item.Command, obj).Result;

                        item.Status = CommandStatus.完成;
                        item.Message = dic.ToJson();
                    }
                    catch (Exception ex)
                    {
                        item.Status = CommandStatus.错误;
                        item.Message = ex?.GetTrue()?.Message;
                    }
                }

                item.Save();
            }
        }
        #endregion
    }
}