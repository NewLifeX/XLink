using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using NewLife.Model;
using NewLife.Net;
using NewLife.Security;
using NewLife.Serialization;
using NewLife.Threading;
using xLink.Entity;
using xLink.Models;

namespace xLink.Services
{
    /// <summary>设备会话</summary>
    [DisplayName("设备")]
    public class DeviceSession : LinkSession
    {
        #region 属性
        #endregion

        #region 构造
        static DeviceSession()
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
        public DeviceSession()
        {
            //GetData = OnGetData;
            //SetData = OnSetData;
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
            var u = Device.FindByName(user);
            if (u == null)
            {
                if (pass.IsNullOrEmpty()) pass = Rand.NextString(8);

                u = new Device
                {
                    Name = user,
                    Secret = pass,
                    Enable = true
                };
                u.SaveRegister(Session as INetSession);
            }

            // 登录
            Name = user;

            // 验证密码
            //u.CheckMD5(pass);
            u.CheckEqual(pass);

            return u;
        }

        /// <summary>登录或注册完成后，保存登录信息</summary>
        /// <param name="user"></param>
        /// <param name="ps">附加参数</param>
        protected override void SaveLogin(IManageUser user, IDictionary<String, Object> ps)
        {
            if (user is Device dv) Fill(dv, ps);

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
            //// 读取信息
            //TimerX.Delay(async s =>
            //{
            //    try
            //    {
            //        var rs = await Session.InvokeAsync<Object>("GetServer");
            //        var dic = rs.ToDictionary();
            //        WriteLog("Server={0}", dic["Server"]);
            //    }
            //    catch { }
            //}, 1000);

            base.SaveLogin(user, ps);
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

        #region 操作历史
        /// <summary>更新在线信息，登录前、心跳时 调用</summary>
        /// <param name="name"></param>
        /// <param name="ps">附加参数</param>
        public override void CheckOnline(String name, IDictionary<String, Object> ps)
        {
            var ns = Session as NetSession;

            var olt = Online ?? CreateOnline(ns.ID);
            if (olt is DeviceOnline dolt) Fill(dolt, ps);

            Online = olt;

            base.CheckOnline(name, ps);
        }

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
            var hi = new DeviceHistory
            {
                Version = Version,
                Name = Current + "",
                NetType = NetType
            };

            return hi;
        }
        #endregion

        #region 下发指令
        /// <summary>下发指令</summary>
        /// <param name="state"></param>
        public void CheckCommand(Object state)
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

        //private Byte[] OnGetData(String id)
        //{
        //    var dv = Current as Device;
        //    if (!id.IsNullOrEmpty() && !id.EqualIgnoreCase(dv.Name)) dv = Device.FindByName(id);

        //    return dv?.Data.ToHex();
        //}

        //private void OnSetData(String id, Byte[] data)
        //{
        //    var dv = Current as Device;
        //    if (!id.IsNullOrEmpty() && !id.EqualIgnoreCase(dv.Name)) dv = Device.FindByName(id);

        //    dv.Data = data.ToHex();
        //    dv.SaveAsync();
        //}
        #endregion
    }
}