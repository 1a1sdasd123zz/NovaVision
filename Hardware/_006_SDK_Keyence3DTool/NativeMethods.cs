using System;
using System.Runtime.InteropServices;
using KeyenceLib;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    internal class NativeMethods
    {
        public sealed class PinnedObject : IDisposable
        {
            private GCHandle _handle;

            public IntPtr Pointer => _handle.AddrOfPinnedObject();

            public PinnedObject(object target)
            {
                _handle = GCHandle.Alloc(target, GCHandleType.Pinned);
            }

            public void Dispose()
            {
                _handle.Free();
                _handle = default(GCHandle);
            }
        }

        internal static int DeviceCount => 6;

        internal static uint EnvironmentSettingSize => 60u;

        internal static uint CommonSettingSize => 20u;

        internal static uint ProgramSettingSize => 10980u;

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_Initialize();

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_Finalize();

        [DllImport("LJX8_IF.dll")]
        internal static extern LJX8IF_VERSION_INFO LJX8IF_GetVersion();

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_EthernetOpen(int lDeviceId, ref LJX8IF_ETHERNET_CONFIG pEthernetConfig);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_CommunicationClose(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_RebootController(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ReturnToFactorySetting(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ControlLaser(int lDeviceId, byte byState);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetError(int lDeviceId, byte byReceivedMax, ref byte pbyErrCount, IntPtr pwErrCode);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ClearError(int lDeviceId, short wErrCode);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_TrgErrorReset(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetTriggerAndPulseCount(int lDeviceId, ref uint pdwTriggerCount, ref int plEncoderCount);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_SetTimerCount(int lDeviceId, uint dwTimerCount);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetTimerCount(int lDeviceId, ref uint pdwTimerCount);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetHeadTemperature(int lDeviceId, ref short pnSensorTemperature, ref short pnProcessorTemperature, ref short pnCaseTemperature);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetHeadModel(int lDeviceId, IntPtr pHeadModel);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetSerialNumber(int lDeviceId, IntPtr pControllerSerialNo, IntPtr pHeadSerialNo);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetAttentionStatus(int lDeviceId, ref ushort pwAttentionStatus);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_Trigger(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_StartMeasure(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_StopMeasure(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ClearMemory(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_SetSetting(int lDeviceId, byte byDepth, LJX8IF_TARGET_SETTING targetSetting, IntPtr pData, uint dwDataSize, ref uint pdwError);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetSetting(int lDeviceId, byte byDepth, LJX8IF_TARGET_SETTING targetSetting, IntPtr pData, uint dwDataSize);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_InitializeSetting(int lDeviceId, byte byDepth, byte byTarget);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ReflectSetting(int lDeviceId, byte byDepth, ref uint pdwError);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_RewriteTemporarySetting(int lDeviceId, byte byDepth);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_CheckMemoryAccess(int lDeviceId, ref byte pbyBusy);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ChangeActiveProgram(int lDeviceId, byte byProgramNo);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetActiveProgram(int lDeviceId, ref byte pbyProgramNo);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_SetXpitch(int lDeviceId, uint dwXpitch);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetXpitch(int lDeviceId, ref uint pdwXpitch);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetProfile(int lDeviceId, ref LJX8IF_GET_PROFILE_REQUEST pReq, ref LJX8IF_GET_PROFILE_RESPONSE pRsp, ref LJX8IF_PROFILE_INFO pProfileInfo, IntPtr pdwProfileData, uint dwDataSize);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetBatchProfile(int lDeviceId, ref LJX8IF_GET_BATCH_PROFILE_REQUEST pReq, ref LJX8IF_GET_BATCH_PROFILE_RESPONSE pRsp, ref LJX8IF_PROFILE_INFO pProfileInfo, IntPtr pdwBatchData, uint dwDataSize);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetBatchSimpleArray(int lDeviceId, ref LJX8IF_GET_BATCH_PROFILE_REQUEST pReq, ref LJX8IF_GET_BATCH_PROFILE_RESPONSE pRsp, ref LJX8IF_PROFILE_INFO pProfileInfo, IntPtr pProfileHeaderArray, IntPtr pHeightProfileArray, IntPtr pLuminanceProfileArray);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_InitializeHighSpeedDataCommunication(int lDeviceId, ref LJX8IF_ETHERNET_CONFIG pEthernetConfig, ushort wHighSpeedPortNo, HighSpeedDataCallBack pCallBack, uint dwProfileCount, uint dwThreadId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_InitializeHighSpeedDataCommunicationSimpleArray(int lDeviceId, ref LJX8IF_ETHERNET_CONFIG pEthernetConfig, ushort wHighSpeedPortNo, HighSpeedDataCallBackForSimpleArray pCallBackSimpleArray, uint dwProfileCount, uint dwThreadId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_PreStartHighSpeedDataCommunication(int lDeviceId, ref LJX8IF_HIGH_SPEED_PRE_START_REQUEST pReq, ref LJX8IF_PROFILE_INFO pProfileInfo);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_StartHighSpeedDataCommunication(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_StopHighSpeedDataCommunication(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_FinalizeHighSpeedDataCommunication(int lDeviceId);

        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(IntPtr destination, IntPtr source, UIntPtr length);

        public static void CopyUshort(IntPtr source, ushort[] destination, int length)
        {
            using PinnedObject pin = new PinnedObject(destination);
            int copyLength = Marshal.SizeOf(typeof(ushort)) * length;
            CopyMemory(pin.Pointer, source, (UIntPtr)(ulong)copyLength);
        }
    }
}
