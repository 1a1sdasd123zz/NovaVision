using System.Runtime.InteropServices;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public struct LJX8IF_HIGH_SPEED_PRE_START_REQUEST
    {
        public byte bySendPosition;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] reserve;
    }
}
