using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NewLife;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using xLink.Models;

namespace xLink.Client
{
    /// <summary>设备客户端</summary>
    public class DeviceClient : LinkClient
    {
        #region 属性
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        /// <param name="uri"></param>
        public DeviceClient(String uri) : base(uri) { }
        #endregion

        #region 登录
        private MachineInfo _hardInfo;
        protected override Object GetLoginInfo()
        {
            var rs = base.GetLoginInfo().ToDictionary();

            if (_hardInfo == null) _hardInfo = new MachineInfo();

            var ps = SerialTransport.GetPortNames();

            var ext = new
            {
                _hardInfo.OSName,
                _hardInfo.OSVersion,

                _hardInfo.Memory,
                _hardInfo.AvailableMemory,
                _hardInfo.Processor,
                _hardInfo.CpuID,
                _hardInfo.CpuRate,
                _hardInfo.UUID,
                _hardInfo.MachineGuid,

                COMs = ps.Join(","),
            };

            return rs.Merge(ext, true);
        }
        #endregion

        #region 心跳
        protected override Object GetPingInfo()
        {
            var rs = base.GetPingInfo().ToDictionary();

            if (_hardInfo == null) _hardInfo = new MachineInfo();

            var ps = SerialTransport.GetPortNames();

            var ext = new
            {
                _hardInfo.AvailableMemory,
                _hardInfo.CpuRate,

                COMs = ps.Join(","),

                Time = DateTime.Now,
                Delay = (Int32)Delay.TotalMilliseconds,
            };

            return rs.Merge(ext, true);
        }
        #endregion
    }
}