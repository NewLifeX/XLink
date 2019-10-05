using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using NewLife;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace xLink.Client
{
    /// <summary>硬件助手</summary>
    public class MachineInfo
    {
        #region 属性
        /// <summary>系统名称</summary>
        public String OSName { get; }

        /// <summary>系统版本</summary>
        public String OSVersion { get; }

        /// <summary>处理器序列号</summary>
        public String Processor { get; }

        /// <summary>处理器序列号</summary>
        public String CpuID { get; }

        /// <summary>唯一标识</summary>
        public String UUID { get; }

        /// <summary>机器标识</summary>
        public String MachineGuid { get; }

        /// <summary>内存总量</summary>
        public UInt64 Memory { get; }

        /// <summary>可用内存</summary>
        public UInt64 AvailableMemory => new ComputerInfo().AvailablePhysicalMemory;

        private PerformanceCounter _cpuCounter;
        /// <summary>CPU占用率</summary>
        public Single CpuRate => _cpuCounter == null ? 0 : (_cpuCounter.NextValue() / 100);
        #endregion

        #region 构造
        public MachineInfo()
        {
            var ci = new ComputerInfo();
            OSName = ci.OSFullName + (Environment.Is64BitOperatingSystem ? " x64" : " x86");
            OSVersion = ci.OSVersion;

            Processor = GetInfo("Win32_Processor", "Name");
            CpuID = GetInfo("Win32_Processor", "ProcessorId");
            Memory = ci.TotalPhysicalMemory;
            //AvailableMemory = ci.AvailablePhysicalMemory;

            UUID = GetInfo("Win32_ComputerSystemProduct", "UUID");

            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");
            if (reg != null)
            {
                MachineGuid = reg.GetValue("MachineGuid") + "";
            }

            // 性能计数器的初始化非常耗时
            Task.Run(() =>
            {
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total")
                {
                    MachineName = "."
                };
                _cpuCounter.NextValue();
            });
        }
        #endregion

        #region WMI辅助
        /// <summary>获取WMI信息</summary>
        /// <param name="path"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static String GetInfo(String path, String property)
        {
            // Linux Mono不支持WMI
            if (Runtime.Mono) return "";

            var bbs = new List<String>();
            try
            {
                var wql = String.Format("Select {0} From {1}", property, path);
                var cimobject = new ManagementObjectSearcher(wql);
                var moc = cimobject.Get();
                foreach (var mo in moc)
                {
                    if (mo != null &&
                        mo.Properties != null &&
                        mo.Properties[property] != null &&
                        mo.Properties[property].Value != null)
                        bbs.Add(mo.Properties[property].Value.ToString());
                }
            }
            catch
            {
                return "";
            }

            bbs.Sort();

            return bbs.Distinct().Join();
        }
        #endregion
    }
}