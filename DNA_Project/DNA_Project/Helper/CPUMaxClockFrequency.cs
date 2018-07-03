using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace DNA_Project.Helper
{
    static class CPUMaxClockFrequency
    {
        public static uint GetCPUMaxClockFrequency() {
            var searcher = new ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                var clockSpeed = (uint)item["MaxClockSpeed"];
                Console.WriteLine(clockSpeed);
                // if search is not empt, return MaxClockSpeed.
                return clockSpeed;
            }
            // else return 0.
            return 0;
        }
    }
}
