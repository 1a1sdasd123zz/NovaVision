using System.Runtime.InteropServices;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public struct LJX8IF_PROFILE_INFO
    {
        public byte byProfileCount;

        public byte reserve1;

        public byte byLuminanceOutput;

        public byte reserve2;

        public short nProfileDataCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] reserve3;

        public int lXStart;

        public int lXPitch;
    }
}
