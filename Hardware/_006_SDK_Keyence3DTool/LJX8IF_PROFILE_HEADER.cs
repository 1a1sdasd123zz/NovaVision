using System.Runtime.InteropServices;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public struct LJX8IF_PROFILE_HEADER
    {
        public uint reserve;

        public uint dwTriggerCount;

        public int lEncoderCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public uint[] reserve2;
    }
}
