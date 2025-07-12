using System.Runtime.InteropServices;

namespace NovaVision.Hardware._011_SDK_SSZN3DTool
{
    public struct SR7IF_ETHERNET_CONFIG
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] abyIpAddress;
    }
}
