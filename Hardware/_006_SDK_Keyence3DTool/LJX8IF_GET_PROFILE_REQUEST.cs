using System.Runtime.InteropServices;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public struct LJX8IF_GET_PROFILE_REQUEST
    {
        public byte byTargetBank;

        public byte byPositionMode;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] reserve;

        public uint dwGetProfileNo;

        public byte byGetProfileCount;

        public byte byErase;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] reserve2;
    }
}
