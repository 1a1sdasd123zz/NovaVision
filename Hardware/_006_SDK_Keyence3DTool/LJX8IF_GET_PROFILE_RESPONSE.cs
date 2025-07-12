using System.Runtime.InteropServices;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public struct LJX8IF_GET_PROFILE_RESPONSE
    {
        public uint dwCurrentProfileNo;

        public uint dwOldestProfileNo;

        public uint dwGetTopProfileNo;

        public byte byGetProfileCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] reserve;
    }
}
