using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NovaVision.BaseClass.Helper;

public class MemoryHelper
{
    [DllImport("psapi.dll", SetLastError = true)]
    private static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS_EX counters, int size);

    [StructLayout(LayoutKind.Sequential, Size = 72)]
    public struct PROCESS_MEMORY_COUNTERS_EX
    {
        public int cb;
        public int PageFaultCount;
        public long PeakWorkingSetSize;
        public long WorkingSetSize;
        public long QuotaPeakPagedPoolUsage;
        public long QuotaPagedPoolUsage;
        public long QuotaPeakNonPagedPoolUsage;
        public long QuotaNonPagedPoolUsage;
        public long PagefileUsage;
        public long PeakPagefileUsage;
        public long PrivateUsage;
    }

    public static (long WorkingSet, long PrivateWorkingSet) GetMemoryUsage()
    {
        Process process = Process.GetCurrentProcess();
        PROCESS_MEMORY_COUNTERS_EX counters = new PROCESS_MEMORY_COUNTERS_EX();
        counters.cb = Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS_EX));
        if (GetProcessMemoryInfo(process.Handle, out counters, Marshal.SizeOf(counters)))
        {
            return (counters.WorkingSetSize, counters.PrivateUsage);
        }
        return (0, 0);
    }
}