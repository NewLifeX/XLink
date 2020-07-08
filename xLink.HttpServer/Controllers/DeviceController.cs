using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Security;
using NewLife.Serialization;
using XCode;
using xLink.Entity;
using xLink.Models;
using xLinkServer.Common;
using xLinkServer.Models;
using xLinkServer.Services;

namespace xLinkServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiFilter]
    public class DeviceController : BaseController
    {
        private static String _version;
        static DeviceController()
        {
            var asmx = AssemblyX.Entry;
            _version = asmx?.Version;
        }

        private readonly IQueueService _queue;
        public DeviceController(IQueueService queue) => _queue = queue;

        #region 登录
        [HttpPost(nameof(Login))]
        public LoginResponse Login(LoginInfo inf)
        {
            var code = inf.Code;
            var secret = inf.Secret;

            var dv = Device.FindByCode(code, true);
            var di = inf.Device;
            _deviceForHistory = dv;

            // 校验唯一编码，防止客户端拷贝配置
            if (dv != null) dv = CheckDevice(dv, di);

            var autoReg = false;
            if (dv == null)
            {
                dv = AutoRegister(null, inf, out autoReg);
            }
            else
            {
                if (!dv.Enable) throw new ApiException(99, "禁止登录");

                // 登录密码未设置或者未提交，则执行动态注册
                if (dv.Secret.IsNullOrEmpty() || secret.IsNullOrEmpty())
                    dv = AutoRegister(dv, inf, out autoReg);
                else if (dv.Secret.MD5() != secret)
                    dv = AutoRegister(dv, inf, out autoReg);
            }

            if (dv == null) throw new ApiException(12, "设备鉴权失败");
            _deviceForHistory = dv;

            var msg = "";
            var success = false;
            try
            {
                Fill(dv, di);

                //var ip = Request.Host + "";
                var ip = UserHost;

                dv.Logins++;
                dv.LastLogin = DateTime.Now;
                dv.LastLoginIP = ip;

                if (dv.CreateIP.IsNullOrEmpty()) dv.CreateIP = ip;
                dv.UpdateIP = ip;

                dv.Save();

                // 设置令牌
                CreateToken(dv.Code);

                if (Session != null) Session["Device"] = dv;

                // 在线记录
                var olt = GetOnline(code, dv);
                if (olt == null) olt = CreateOnline(code, dv);

                olt.LocalTime = di.Time;
                olt.MACs = di.Macs;
                olt.COMs = di.COMs;

                olt.Token = Token;
                olt.PingCount++;

                // 5秒内直接保存
                if (olt.CreateTime.AddSeconds(5) > DateTime.Now)
                    olt.Save();
                else
                    olt.SaveAsync();

                msg = $"[{dv.Name}/{dv.Code}]鉴权成功 ";

                success = true;
            }
            catch (Exception ex)
            {
                msg = ex.GetTrue().Message + " ";
                throw;
            }
            finally
            {
                // 登录历史
                WriteHistory("设备鉴权", success, dv, $"{msg},token:【{Token}】;{di.ToJson(false, false, false)}");
                XTrace.WriteLine("登录{0} {1}", success ? "成功" : "失败", msg);
            }

            // 推送消息，解耦
            _queue.Public("设备鉴权", dv.Code);

            var rs = new LoginResponse
            {
                Name = dv.Name,
                Token = Token,
                Version = _version,
            };

            // 动态注册，下发设备证书
            if (autoReg)
            {
                rs.Code = dv.Code;
                rs.Secret = dv.Secret;
            }

            return rs;
        }

        /// <summary>注销</summary>
        /// <param name="reason">注销原因</param>
        /// <returns></returns>
        [HttpGet(nameof(Logout))]
        [HttpPost(nameof(Logout))]
        public LoginResponse Logout(String reason)
        {
            var dv = Session["Device"] as Device;
            if (dv != null)
            {
                var olt = GetOnline(dv.Code, dv);
                if (olt != null)
                {
                    var msg = "{3} [{0}]]登录于{1}，最后活跃于{2}".F(dv, olt.CreateTime, olt.UpdateTime, reason);
                    WriteHistory(dv, "设备下线", true, msg);
                    olt.Delete();

                    // 计算在线时长
                    if (olt.CreateTime.Year > 2000)
                    {
                        dv.OnlineTime += (Int32)(DateTime.Now - olt.CreateTime).TotalSeconds;
                        dv.SaveAsync();
                    }
                }
            }

            // 销毁会话，更新令牌
            Session["Device"] = null;
            CreateToken(null);

            return new LoginResponse
            {
                Name = dv?.Name,
                Token = Token,
            };
        }

        /// <summary>
        /// 校验设备密钥
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        private Device CheckDevice(Device dv, DeviceInfo di)
        {
            // 校验唯一编码，防止客户端拷贝配置
            var uuid = di.UUID;
            var guid = di.MachineGuid;
            var diskid = di.DiskID;
            if (!uuid.IsNullOrEmpty() && uuid != dv.Uuid)
            {
                WriteHistory("登录校验", false, dv, "唯一标识不符！{0}!={1}".F(uuid, dv.Uuid));
                return null;
            }
            if (!guid.IsNullOrEmpty() && guid != dv.MachineGuid)
            {
                WriteHistory("登录校验", false, dv, "机器标识不符！{0}!={1}".F(guid, dv.MachineGuid));
                return null;
            }
            if (!diskid.IsNullOrEmpty() && diskid != dv.DiskID)
            {
                WriteHistory("登录校验", false, dv, "磁盘序列号不符！{0}!={1}".F(diskid, dv.DiskID));
                return null;
            }

            // 机器名
            if (di.MachineName != dv.MachineName)
            {
                WriteHistory("登录校验", false, dv, "机器名不符！{0}!={1}".F(di.MachineName, dv.MachineName));
            }

            // 网卡地址
            if (di.Macs != dv.MACs)
            {
                var dims = di.Macs?.Split(",") ?? new String[0];
                var dvms = dv.MACs?.Split(",") ?? new String[0];
                // 任意网卡匹配则通过
                if (!dvms.Any(e => dims.Contains(e)))
                {
                    WriteHistory("登录校验", false, dv, "网卡地址不符！{0}!={1}".F(di.Macs, dv.MACs));
                }
            }

            return dv;
        }

        private Device AutoRegister(Device dv, LoginInfo inf, out Boolean autoReg)
        {
            var set = Setting.Current;
            if (!set.AutoRegister) throw new ApiException(12, "禁止自动注册");

            var di = inf.Device;
            if (dv == null)
            {
                // 该硬件的所有设备信息
                var list = Device.Search(di.UUID, di.MachineGuid, di.Macs);

                // 当前设备信息，取较老者
                list = list.OrderBy(e => e.ID).ToList();

                // 找到设备
                if (dv == null) dv = list.FirstOrDefault();
            }

            var ip = UserHost;
            var name = "";
            if (name.IsNullOrEmpty()) name = di.MachineName;
            if (name.IsNullOrEmpty()) name = di.UserName;

            if (dv == null) dv = new Device
            {
                Enable = true,

                CreateIP = ip,
                CreateTime = DateTime.Now,
            };

            // 如果未打开动态注册，则把设备修改为禁用
            dv.Enable = set.AutoRegister;

            if (dv.Name.IsNullOrEmpty()) dv.Name = name;

            // 优先使用设备散列来生成设备证书，确保设备路由到其它接入网关时保持相同证书代码
            var code = "";
            var uid = $"{di.UUID}@{di.MachineGuid}@{di.Macs}";
            if (!uid.IsNullOrEmpty())
            {
                // 使用产品类别加密一下，确保不同类别有不同编码
                var buf = uid.GetBytes();
                code = buf.Crc().GetBytes().ToHex();

                dv.Code = code;
            }

            if (dv.Code.IsNullOrEmpty()) code = Rand.NextString(8);

            dv.Secret = Rand.NextString(16);
            dv.UpdateIP = ip;
            dv.UpdateTime = DateTime.Now;

            dv.Save();
            autoReg = true;

            WriteHistory("动态注册", true, dv, inf.ToJson(false, false, false));

            return dv;
        }

        private void Fill(Device dv, DeviceInfo di)
        {
            if (!di.OSName.IsNullOrEmpty()) dv.OS = di.OSName;
            if (!di.OSVersion.IsNullOrEmpty()) dv.OSVersion = di.OSVersion;
            if (!di.Version.IsNullOrEmpty()) dv.Version = di.Version;
            if (di.Compile.Year > 2000) dv.CompileTime = di.Compile;

            if (!di.MachineName.IsNullOrEmpty()) dv.MachineName = di.MachineName;
            if (!di.UserName.IsNullOrEmpty()) dv.UserName = di.UserName;
            if (!di.Processor.IsNullOrEmpty()) dv.Processor = di.Processor;
            if (!di.CpuID.IsNullOrEmpty()) dv.CpuID = di.CpuID;
            if (!di.UUID.IsNullOrEmpty()) dv.Uuid = di.UUID;
            if (!di.MachineGuid.IsNullOrEmpty()) dv.MachineGuid = di.MachineGuid;
            if (!di.DiskID.IsNullOrEmpty()) dv.DiskID = di.DiskID?.Trim();

            if (di.ProcessorCount > 0) dv.Cpu = di.ProcessorCount;
            if (di.Memory > 0) dv.Memory = (Int32)(di.Memory / 1024 / 1024);
            if (!di.Macs.IsNullOrEmpty()) dv.MACs = di.Macs;
            if (!di.COMs.IsNullOrEmpty()) dv.COMs = di.COMs;
            if (!di.InstallPath.IsNullOrEmpty()) dv.InstallPath = di.InstallPath;
            if (!di.Runtime.IsNullOrEmpty()) dv.Runtime = di.Runtime;
            if (!di.Dpi.IsNullOrEmpty()) dv.Dpi = di.Dpi;
            if (!di.Resolution.IsNullOrEmpty()) dv.Resolution = di.Resolution;
        }
        #endregion

        #region 心跳
        [TokenFilter]
        //[HttpGet(nameof(Ping))]
        [HttpPost(nameof(Ping))]
        public PingResponse Ping(PingInfo inf)
        {
            var rs = new PingResponse
            {
                Time = inf.Time,
                ServerTime = DateTime.Now,
            };

            var dv = Session["Device"] as Device;
            if (dv != null)
            {
                var code = dv.Code;

                var olt = GetOnline(code, dv);
                if (olt == null) olt = CreateOnline(code, dv);
                Fill(olt, inf);

                olt.Token = Token;
                olt.PingCount++;
                olt.SaveAsync();

                // 拉取命令
                rs.Commands = AcquireCommands(dv.ID);
            }

            return rs;
        }

        private static IList<DeviceCommand> _commands;
        private static DateTime _nextTime;
        private static CommandModel[] AcquireCommands(Int32 deviceId)
        {
            // 缓存最近1000个未执行命令，用于快速过滤，避免大量设备在线时频繁查询命令表
            if (_nextTime < DateTime.Now)
            {
                _commands = DeviceCommand.AcquireCommands(-1, 1000);
                _nextTime = DateTime.Now.AddMinutes(1);
            }

            // 是否有本设备
            if (!_commands.Any(e => e.DeviceId == deviceId)) return null;

            var cmds = DeviceCommand.AcquireCommands(deviceId, 100);
            if (cmds == null) return null;

            var rs = cmds.Select(e => new CommandModel
            {
                Id = e.ID,
                Command = e.Command,
                Argument = e.Argument,
            }).ToArray();

            foreach (var item in cmds)
            {
                item.Finished = true;
                item.UpdateTime = DateTime.Now;
            }
            cmds.Update(true);

            return rs;
        }

        //[TokenFilter]
        [HttpGet(nameof(Ping))]
        public PingResponse Ping()
        {
            return new PingResponse
            {
                Time = 0,
                ServerTime = DateTime.Now,
            };
        }

        /// <summary>填充在线设备信息</summary>
        /// <param name="olt"></param>
        /// <param name="inf"></param>
        private void Fill(DeviceOnline olt, PingInfo inf)
        {
            if (inf.AvailableMemory > 0) olt.AvailableMemory = (Int32)(inf.AvailableMemory / 1024 / 1024);
            if (inf.AvailableSpace > 0) olt.AvailableSpace = (Int32)(inf.AvailableSpace / 1024 / 1024);
            if (inf.CpuRate > 0) olt.CpuRate = inf.CpuRate;
            if (inf.Delay > 0) olt.Delay = inf.Delay;

            var dt = inf.Time.ToDateTime();
            if (dt.Year > 2000)
            {
                olt.LocalTime = dt;
                olt.Offset = (Int32)Math.Round((dt - DateTime.Now).TotalSeconds);
            }

            if (!inf.Processes.IsNullOrEmpty()) olt.Processes = inf.Processes;
            if (!inf.Macs.IsNullOrEmpty()) olt.MACs = inf.Macs;
            if (!inf.COMs.IsNullOrEmpty()) olt.COMs = inf.COMs;
        }

        /// <summary></summary>
        /// <param name="code"></param>
        /// <param name="dv"></param>
        /// <returns></returns>
        protected virtual DeviceOnline GetOnline(String code, IDevice dv)
        {
            var olt = Session?["Online"] as DeviceOnline;
            if (olt != null) return olt;

            var ip = UserHost;
            var port = HttpContext.Connection.RemotePort;
            var sid = $"{dv.ID}@{ip}:{port}";
            return DeviceOnline.FindBySessionID(sid);
        }

        /// <summary>检查在线</summary>
        /// <returns></returns>
        protected virtual DeviceOnline CreateOnline(String code, IDevice dv)
        {
            var ip = UserHost;
            var port = HttpContext.Connection.RemotePort;
            var sid = $"{dv.ID}@{ip}:{port}";
            var olt = DeviceOnline.GetOrAdd(sid);
            olt.DeviceId = dv.ID;
            olt.Name = dv.Name;
            olt.ProductId = dv.ProductId;

            olt.AreaId = dv.CityId;
            olt.Version = dv.Version;
            olt.MACs = dv.MACs;
            olt.COMs = dv.COMs;
            olt.Token = Token;
            olt.CreateIP = ip;

            var set = Setting.Current;
            olt.Creator = set.NodeName;

            //olt.SaveAsync();

            if (Session != null) Session["Online"] = olt;

            return olt;
        }
        #endregion

        #region 历史
        /// <summary>上报数据，针对命令</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost(nameof(Report))]
        public async Task<Object> Report(Int32 id)
        {
            var dv = Session["Device"] as Device;
            if (dv == null) throw new ApiException(402, "设备未登录");

            // 如果找不到命令，自动插入
            var cmd = DeviceCommand.FindByID(id);
            if (cmd == null) cmd = new DeviceCommand { DeviceId = dv.ID };
            //if (cmd != null && cmd.DeviceId == dv.ID)
            //{
            var ms = Request.Body;
            if (Request.ContentLength > 0)
            {
                var rs = (cmd.Command + "") switch
                {
                    "截屏" => await SaveFileAsync(cmd, ms, "png"),
                    "抓日志" => await SaveFileAsync(cmd, ms, "log"),
                    _ => await SaveFileAsync(cmd, ms, "bin"),
                };
                if (!rs.IsNullOrEmpty())
                {
                    cmd.Result = rs;
                    cmd.CreateUser = Setting.Current.NodeName;
                    cmd.Save();

                    WriteHistory(dv, cmd.Command, true, rs);
                }
            }
            //}

            return null;
        }

        private async Task<String> SaveFileAsync(DeviceCommand cmd, Stream ms, String ext)
        {
            var file = $"../{cmd.Command}/{DateTime.Today:yyyyMMdd}/{cmd.DeviceId}_{cmd.ID}.{ext}";
            file.EnsureDirectory(true);

            using var fs = file.AsFile().OpenWrite();
            await ms.CopyToAsync(fs);
            await ms.FlushAsync();

            return file;
        }

        protected virtual DeviceHistory WriteHistory(String action, Boolean success, IDevice dv, String remark = null)
        {
            var set = Setting.Current;
            var ip = UserHost;
            return DeviceHistory.Create(dv, action, success, remark, set.NodeName, ip);
        }

        /// <summary>获取命令执行结果</summary>
        /// <param name="id"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetResult))]
        public ActionResult GetResult(Int32 id, Int32 deviceId)
        {
            var cmd = DeviceCommand.FindByID(id);
            if (cmd == null || cmd.DeviceId != deviceId) throw new InvalidOperationException();

            var file = cmd.Result;
            if (file.IsNullOrEmpty()) return new EmptyResult();

            file = file.GetFullPath();
            if (!System.IO.File.Exists(file)) throw new FileNotFoundException($"找不到文件{cmd.Result}", file);

            var contentType = (Path.GetExtension(file)?.ToLower()) switch
            {
                ".png" => "image/png",
                ".log" => "text/plain",
                _ => "",
            };

            return PhysicalFile(file, contentType, Path.GetFileName(file));
        }
        #endregion

        #region 升级
        /// <summary>升级检查</summary>
        /// <param name="channel">更新通道</param>
        /// <returns></returns>
        [TokenFilter]
        [HttpGet(nameof(Upgrade))]
        public UpgradeInfo Upgrade(String channel)
        {
            var dv = Session["Device"] as Device;
            if (dv == null) throw new ApiException(402, "设备未登录");

            // 默认Release通道
            if (!Enum.TryParse<ProductChannels>(channel, true, out var ch)) ch = ProductChannels.Release;
            if (ch < ProductChannels.Release) ch = ProductChannels.Release;

            // 找到所有产品版本
            var list = ProductVersion.GetValids(dv.ProductId, ch);

            // 应用过滤规则
            var pv = list.FirstOrDefault(e => e.Match(dv));
            if (pv == null) return null;
            //if (pv == null) throw new ApiException(509, "没有升级规则");

            WriteHistory("自动更新", true, dv, $"channel={ch} => [{pv.ID}] {pv.Version} {pv.Source} {pv.Executor}");

            return new UpgradeInfo
            {
                Version = pv.Version,
                Source = pv.Source,
                Executor = pv.Executor,
                Force = pv.Force,
                Description = pv.Description,
            };
        }
        #endregion

        #region 配置
        /// <summary>获取配置</summary>
        /// <param name="name">配置名</param>
        /// <returns>返回Json字符串</returns>
        [TokenFilter]
        [HttpGet(nameof(GetConfig))]
        public String GetConfig(String name)
        {
            var dv = Session["Device"] as Device;
            if (dv == null) throw new ApiException(402, "设备未登录");
            if (name.IsNullOrEmpty()) throw new Exception($"配置名不能为空！");

            var cfg = DeviceConfig.FindByDeviceIdAndName(dv.ID, name);

            // 修正城市网点
            if (cfg != null)
            {
                cfg.AreaId = dv.CityId;
                cfg.SaveAsync();
            }

            return cfg?.Content ?? "";
        }

        /// <summary>设置配置</summary>
        /// <param name="model">配置模型</param>
        /// <returns>返回Json字符串</returns>
        [TokenFilter]
        [HttpPost(nameof(SetConfig))]
        public String SetConfig([FromBody] ConfigModel model)
        {
            var dv = Session["Device"] as Device;
            if (dv == null) throw new ApiException(402, "设备未登录");
            model = model ?? new ConfigModel();
            if (model.Name.IsNullOrEmpty()) throw new Exception($"配置名不能为空！");

            var cfg = DeviceConfig.FindByDeviceIdAndName(dv.ID, model.Name);
            if (cfg == null) cfg = new DeviceConfig { DeviceId = dv.ID, Name = model.Name };

            cfg.AreaId = dv.CityId;
            cfg.Content = model.Config;

            cfg.Save();

            return cfg.Content;
        }
        #endregion
    }
}