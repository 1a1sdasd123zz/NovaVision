using System.Runtime.InteropServices;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public struct LJX8IF_GET_BATCH_PROFILE_RESPONSE
    {
        public uint dwCurrentBatchNo;

        public uint dwCurrentBatchProfileCount;

        public uint dwOldestBatchNo;

        public uint dwOldestBatchProfileCount;

        public uint dwGetBatchNo;

        public uint dwGetBatchProfileCount;

        public uint dwGetBatchTopProfileNo;

        public byte byGetProfileCount;

        public byte byCurrentBatchCommited;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] reserve;
    }
}
