using NewLife.Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace xLink.Client.Services
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

            // 如果设备编码不存在，则需要提交产品证书
            var pkey = "";
            var psecret = "";
            if (UserName.IsNullOrEmpty())
            {
                var set = Setting.Current;
                pkey = set.ProductKey;
                psecret = set.ProductSecret.MD5();
            }

            var ext = new
            {
                ProductKey = pkey,
                ProductSecret = psecret,

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