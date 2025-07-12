using System;
using System.Runtime.InteropServices;

namespace NovaVision.Hardware._011_SDK_SSZN3DTool
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SR7IF_BatchOneTimeCallBack(IntPtr info, IntPtr DataObj);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HighSpeedDataCallBack(IntPtr buffer, uint size, uint count, uint notify, uint user);
    internal class SR7LinkFunc
    {
        internal static uint ProgramSettingSize => 10932u;

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_EthernetOpen(int lDeviceId, ref SR7IF_ETHERNET_CONFIG pEthernetConfig);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_CommClose(int lDeviceId);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_StartMeasure(int lDeviceId, int Timeout);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_StartIOTriggerMeasure(int lDeviceId, int Timeout, int restart);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_StopMeasure(int lDeviceId);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_ReceiveData(int lDeviceId, IntPtr DataObj);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_ProfilePointSetCount(int lDeviceId, IntPtr DataObj);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_ProfilePointCount(int lDeviceId, IntPtr DataObj);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_ProfileDataWidth(int lDeviceId, IntPtr DataObj);

        [DllImport("SR7Link.dll")]
        internal static extern double SR7IF_ProfileData_XPitch(int lDeviceId, IntPtr DataObj);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetEncoder(int lDeviceId, IntPtr DataObj, IntPtr Encoder);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetEncoderContiune(int lDeviceId, IntPtr DataObj, IntPtr Encoder, int GetCnt);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetProfileData(int lDeviceId, IntPtr DataObj, IntPtr Profile);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetProfileContiuneData(int lDeviceId, IntPtr DataObj, IntPtr Profile, int GetCnt);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetBatchRollData(int lDeviceId, IntPtr DataObj, IntPtr Profile, IntPtr Intensity, IntPtr Encoder, IntPtr FrameId, IntPtr FrameLoss, int GetCnt);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetIntensityData(int lDeviceId, IntPtr DataObj, IntPtr Intensity);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetIntensityContiuneData(int lDeviceId, IntPtr DataObj, IntPtr Intensity, int GetCnt);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetBatchRollError(int lDeviceId, IntPtr EthErrCnt, IntPtr UserErrCnt);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetError(int lDeviceId, IntPtr pbyErrCnt, IntPtr pwErrCode);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_HighSpeedDataEthernetCommunicationInitalize(int lDeviceId, ref SR7IF_ETHERNET_CONFIG pEthernetConfig, int wHighSpeedPortNo, HighSpeedDataCallBack pCallBack, uint dwProfileCnt, uint dwThreadId);

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_GetVersion();

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_GetModels(int lDeviceId);

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_GetHeaderSerial(int lDeviceId, int Head);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetOnlineCameraB(int lDeviceId);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_SwitchProgram(int lDeviceId, int No);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_SetOutputPortLevel(uint lDeviceId, uint Port, bool Level);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetInputPortLevel(uint lDeviceId, uint Port, IntPtr Level);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetSetting(uint lDeviceId, int Type, int Category, int Item, int[] Target, ref IntPtr pData, int DataSize);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_SetSetting(uint lDeviceId, int Depth, int Type, int Category, int Item, int[] Target, IntPtr pData, int DataSize);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetSingleProfile(uint lDeviceId, IntPtr pProfileData, IntPtr pEncoder);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_LoadParameters(uint lDeviceId, IntPtr pSettingdata, uint size);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_GetLicenseKey(uint lDeviceId, IntPtr RemainDay);

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_ExportParameters(int lDeviceId, IntPtr size);

        [DllImport("SR3dexe.dll")]
        internal static extern void SR_3D_EXE_Show(IntPtr _BatchData, double x_true_step, double y_true_step, int x_Point_num, int y_batchPoint_num, double z_scale, double Ho, double Lo);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_SetBatchOneTimeDataHandler(int lDeviceId, SR7IF_BatchOneTimeCallBack CallFunc);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_StartMeasureWithCallback(int lDeviceId, int ImmediateBatch);

        [DllImport("SR7Link.dll")]
        internal static extern int SR7IF_TriggerOneBatch(int lDeviceId);

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_GetBatchProfilePoint(IntPtr DataObj, int Head);

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_GetBatchIntensityPoint(IntPtr DataObj, int Head);

        [DllImport("SR7Link.dll")]
        internal static extern IntPtr SR7IF_GetBatchEncoderPoint(IntPtr DataObj, int Head);
    }
}
