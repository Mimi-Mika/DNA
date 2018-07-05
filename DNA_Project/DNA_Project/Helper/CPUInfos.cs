using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace DNA_Project.Helper
{
    class CPUInfos
    {
        public uint maxClockFrequency { get; }
        public int maxThreads { get; } = Environment.ProcessorCount;
        public float performanceIndex { get; }

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public CPUInfos()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                maxClockFrequency = (uint)item["MaxClockSpeed"];
            }

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            performanceIndex = maxClockFrequency * maxThreads;
        }


        public int GetCpuUsage()
        {
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(500);
            return (int)cpuCounter.NextValue();
        }
    }
}
