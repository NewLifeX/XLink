using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLife.MessageQueue;
using NewLife.Model;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Security;
using NewLife.Serialization;
using NewLife.Threading;
using xLink.Entity;
using xLink.Models;

namespace xLink
{
    /// <summary>设备会话</summary>
    [Api("Device", true)]
    [DisplayName("设备")]
    public class DeviceSession : LinkSession
    {
        #region 属性
        /// <summary>当前设备</summary>
        public Device Device { get => Current as Device; }
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

        /// <summary>实例化</summary>
        public DeviceSession()
        {
            GetData = OnGetData;
            SetData = OnSetData;
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

            return u;
        }

        /// <summary>注册，登录找不到用户时调用注册，返回空表示禁止注册</summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        protected override IMyModel CreateUser(String user, String pass)
        {
            var u = Device.FindByCode(user);
            if (u == null) u = new Device { Code = user };

            var devideid = user.GetBytes().Crc().GetBytes().ToHex();
            if (!Type.IsNullOrEmpty() && Type.Length == 4) devideid = Type + devideid;

            u.Name = devideid;
            u.Password = Rand.NextString(8);

            return u;
        }

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="user"></param>
        protected override void SaveLogin(IManageUser user)
        {
            //var dv = Device;
            //if (dv != null)
            //{
            //    // 注册消息队列
            //    MQHost.Instance.Subscribe(dv.Name, dv.Name, "Device", async (sub, msg) =>
            //    {
            //    }, Session);
            //}

            // 检查下发指令
            TimerX.Delay(CheckCommand, 100);
            // 读取信息
            TimerX.Delay(async s =>
            {
                try
                {
                    var rs = await InvokeAsync<Object>("GetServer");
                    var dic = rs.ToDictionary();
                    WriteLog("Server={0}", dic["Server"]);
                }
                catch { }
            }, 1000);

            base.SaveLogin(user);
        }

        /// <summary>心跳</summary>
        /// <returns></returns>
        protected override Object OnPing()
        {
            // 检查下发指令
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

        #region 读写
        /// <summary>写入数据，返回整个数据区</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<Byte[]> Write(String id, Int32 start, params Byte[] data)
        {
            var err = "";
            try
            {
                return await base.Write(id, start, data);
            }
            catch (Exception ex)
            {
                err = ex?.GetTrue()?.Message;
                throw;
            }
            finally
            {
                SaveHistory("Write", err.IsNullOrEmpty(), "({0}, {1}, {2}) {3}".F(id, start, data.ToHex(), err));
            }
        }

        /// <summary>读取对方数据</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override async Task<Byte[]> Read(String id, Int32 start, Int32 count)
        {
            var err = "";
            try
            {
                return await base.Read(id, start, count);
            }
            catch (Exception ex)
            {
                err = ex?.GetTrue()?.Message;
                throw;
            }
            finally
            {
                SaveHistory("Read", err.IsNullOrEmpty(), "({0}, {1}, {2}) {3}".F(id, start, count, err));
            }
        }

        private Byte[] OnGetData(String id)
        {
            var dv = Device;
            if (!id.IsNullOrEmpty() && !id.EqualIgnoreCase(dv.Name)) dv = Device.FindByName(id);

            return dv?.Data.ToHex();
        }

        private void OnSetData(String id, Byte[] data)
        {
            var dv = Device;
            if (!id.IsNullOrEmpty() && !id.EqualIgnoreCase(dv.Name)) dv = Device.FindByName(id);

            dv.Data = data.ToHex();
            dv.SaveAsync();
        }
        #endregion
    }
}