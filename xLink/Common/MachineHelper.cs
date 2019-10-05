using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace xLink.Common
{
    /// <summary>机器信息助手</summary>
    public class MachineHelper
    {
        private static String[] _Excludes = new[] { "Loopback", "VMware", "VBox", "Virtual", "Teredo", "Microsoft", "VPN", "VNIC", "IEEE" };
        private static NetworkInterface[] _ifs;
        /// <summary>获取所有网卡MAC地址</summary>
        /// <returns></returns>
        public static IEnumerable<Byte[]> GetMacs()
        {
            if (_ifs == null) _ifs = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var item in _ifs)
            {
                if (_Excludes.Any(e => item.Description.Contains(e))) continue;
                if (item.Speed < 1_000_000) continue;

                var ips = item.GetIPProperties();
                var addrs = ips.UnicastAddresses
                    .Where(e => e.Address.AddressFamily == AddressFamily.InterNetwork)
                    .Select(e => e.Address)
                    .ToArray();
                if (addrs.All(e => IPAddress.IsLoopback(e))) continue;

                var mac = item.GetPhysicalAddress()?.GetAddressBytes();
                if (mac != null && mac.Length == 6) yield return mac;
            }
        }

        /// <summary>获取网卡MAC地址（网关所在网卡）</summary>
        /// <returns></returns>
        public static Byte[] GetMac()
        {
            if (_ifs == null) _ifs = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var item in _ifs)
            {
                if (_Excludes.Any(e => item.Description.Contains(e))) continue;
                if (item.Speed < 1_000_000) continue;

                var ips = item.GetIPProperties();
                var addrs = ips.UnicastAddresses
                    .Where(e => e.Address.AddressFamily == AddressFamily.InterNetwork)
                    .Select(e => e.Address)
                    .ToArray();
                if (addrs.All(e => IPAddress.IsLoopback(e))) continue;

                // 网关
                addrs = ips.GatewayAddresses
                    .Where(e => e.Address.AddressFamily == AddressFamily.InterNetwork)
                    .Select(e => e.Address)
                    .ToArray();
                if (addrs.Length == 0) continue;

                var mac = item.GetPhysicalAddress()?.GetAddressBytes();
                if (mac != null && mac.Length == 6) return mac;
            }

            return null;
        }
    }
}