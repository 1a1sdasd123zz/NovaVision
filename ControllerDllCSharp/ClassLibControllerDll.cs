using System;
using System.Runtime.InteropServices;

namespace NovaVision.ControllerDllCSharp;

public class ClassLibControllerDll
{
	private const string CommonToolDll = "CommonToolDll.dll";
	private const string ControllerDll = "ControllerDll.dll";
	private const string sControllerDll = "sControllerDll.dll";
	public const int EthernetMode = 0;
	public const int Rs232Mode = 1;
	public const int SUCCESS = 10000;
	public const int ERROR_INIT = 10001;
	public const int ERROR_CLOSE = 10002;
	public const int ERROR_CFG = 10003;
	public const int ERROR_CONNECT = 10004;
	public const int ERROR_RX = 10005;
	public const int ERROR_TX = 10006;
	public const int ERROR_DATA = 10007;
	public const int ERROR_OUTRANGE = 10008;
	public const int ERROR_GET_DIG_VAl = 10009;
	public const int ERROR_GET_STB_VAl = 10010;
	public const int ERROR_GET_LIG_DEL_VAl = 10011;
	public const int ERROR_GET_CAM_DEL_VAl = 10012;
	public const int ERROR_GET_INT_CYC_VAl = 10013;
	public const int ERROR_GET_LIG_TRI_MODE = 10014;
	public const int ERROR_GET_CAM_TRI_EDGE = 10015;
	public const int ERROR_GET_LIG_STA = 10016;
	public const int ERROR_SET_DIG_VAl = 10017;
	public const int ERROR_SET_STB_VAl = 10018;
	public const int ERROR_SET_LIG_DEL_VAl = 10019;
	public const int ERROR_SET_CAM_DEL_VAl = 10020;
	public const int ERROR_SET_INT_CYC_VAl = 10021;
	public const int ERROR_SET_LIG_TRI_MODE = 10022;
	public const int ERROR_SET_CAM_TRI_EDGE = 10023;
	public const int ERROR_SET_LIG_STA = 10024;
	public const int ERROR_DOWNLINE = 10025;
	public const int ERROR_SEND_HEARTBEAT = 10026;
	public const int ERROR_GET_ADAPTER = 10027;
	public const int ERROR_SET_MUL_DIG_VAl = 10028;
	public const int ERROR_SET_MUL_STB_VAl = 10029;
	public const int ERROR_SET_MUL_LIG_DEL_VAl = 10030;
	public const int ERROR_SET_MUL_CAM_DEL_VAl = 10031;
	public const int ERROR_IP_ADDRESS = 10032;
	public const int ERROR_SM_ADDRESS = 10033;
	public const int ERROR_GW_ADDRESS = 10034;
	public const int ERROR_SET_PGM_PAR = 10035;
	public const int ERROR_COLLISION = 10036;
	public const int ERROR_GET_PGM_PAR = 10037;
	public const int ERROR_GET_PULSE_UNIT = 10038;
	public const int ERROR_SET_PULSE_UNIT = 10039;
	public const int ERROR_GET_TRI_PTY = 10040;
	public const int ERROR_SET_TRI_PTY = 10041;
	public const int ERROR_CLR_PGM_PAR = 10042;
	public const int ERROR_SET_CURRENT_STEP = 10043;
	public const int ERROR_GET_CURRENT_STEP = 10044;
	public const int ERROR_GET_TOTAL_STEP = 10045;
	public const int ERROR_RESET_STEP = 10046;
	public const int ERROR_SetON_OFF = 10047;
	public const int ERROR_GetON_OFF = 10048;
	public const int ERROR_SetMulON_OFF = 10049;
	public const int ERROR_GetSoftwareVersion = 10050;
	public const int ERROR_SetTriggerPolarity = 10051;
	public const int ERROR_GetTriggerPolarity = 10052;
	public const int ERROR_GetChannelNumberSummary = 10053;
	public const int ERROR_GeControllerModel = 10054;
	public const int ERROR_SetColorTemperature = 10055;
	public const int ERROR_SetMulColorTemperature = 10056;
	public const int ERROR_GetColorTemperature = 10057;

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetAdapter(ref int AdatterCnt, IntPtr mAdapterPrm);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetHost(ref int controllerCnt, IntPtr mHostPrm, string AdapterIP);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetConfigure(byte[] mMAC, IntPtr mConPrm, string AdapterIP);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetConfigure(
	  byte[] mMAC,
	  ref ClassLibControllerDll.Controller_prm mConPrm,
	  string AdapterIP);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int ConnectIP(string ipAddress, int mTimeOut, ref long controllerHandle);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int DestroyIpConnection(long controllerHandle);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int CreateSerialPort(int serialPortIndex, ref long controllerHandle);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int CreateSerialPort_Baud(
	  int serialPortIndex,
	  int baud,
	  ref long controllerHandle);

	[DllImport("CommonToolDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int ReleaseSerialPort(long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetDigitalValue(
	  int connectType,
	  ref int intensity,
	  int ChannelIndex,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetDigitalValue(
	  int connectType,
	  int ChannelIndex,
	  int intensity,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetMulDigitalValue(
	  int connectType,
	  ClassLibControllerDll.MulDigitalValue[] MulDigValArray,
	  int length,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetStrobeValue(
	  int connectType,
	  ref int strobeValue,
	  int ChannelIndex,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetStrobeValue(
	  int connectType,
	  int ChannelIndex,
	  int strobeValue,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetLightDelayValue(
	  int connectType,
	  ref int lightDelayValue,
	  int ChannelIndex,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetLightDelayValue(
	  int connectType,
	  int ChannelIndex,
	  int lightDelayValue,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetCameraDelayValue(
	  int connectType,
	  ref int cameraDelayValue,
	  int ChannelIndex,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetCameraDelayValue(
	  int connectType,
	  int ChannelIndex,
	  int cameraDelayValue,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetIntCycleValue(
	  int connectType,
	  ref int intCycleValue,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetIntCycleValue(
	  int connectType,
	  int intCycleValue,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetLightTriMode(
	  int connectType,
	  ref int lightTriMode,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetLightTriMode(
	  int connectType,
	  int LightTriMode,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetCamTriEdge(
	  int connectType,
	  ref int camTriEdge,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetCamTriEdge(int connectType, int camTriEdge, long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetLightState(
	  int connectType,
	  ref int lightState,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetLightState(int connectType, int lightState, long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int KeepAlive(int connectType, long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetErrMsg(int errCode, byte[] ErrMsg);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetProgramParameter(
	  int connectType,
	  int triggerSourceIndex,
	  int stepIndex,
	  int length,
	  ClassLibControllerDll.ProgramConfiguration[] ProgramConfigurationArray,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetProgramParameter(
	  int connectType,
	  int triggerSourceIndex,
	  int stepIndex,
	  ref int length,
	  ClassLibControllerDll.ProgramConfiguration[] ProgramConfigurationArray,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int ClearProgramParameter(
	  int connectType,
	  int triggerSourceIndex,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetPulseUnit(
	  int connectType,
	  int ChannelIndex,
	  ref int pulseUnit,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetPulseUnit(
	  int connectType,
	  int ChannelIndex,
	  int pulseUnit,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetLightTriPty(
	  int connectType,
	  ref int triggerPolarity,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetLightTriPty(
	  int connectType,
	  int triggerPolarity,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetCurrentStep(
	  int connectType,
	  int triggerSourceIndex,
	  int currentStep,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetCurrentStep(
	  int connectType,
	  int triggerSourceIndex,
	  ref int currentStep,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int ResetCurrentStep(
	  int connectType,
	  int triggerSourceIndex,
	  long controllerHandle);

	[DllImport("ControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetTotalStep(
	  int connectType,
	  int triggerSourceIndex,
	  ref int totalStep,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetDigitalValue_s(
	  int connectType,
	  int ChannelIndex,
	  int intensity,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetMulDigitalValue_s(
	  int connectType,
	  ClassLibControllerDll.MulDigitalValue_s[] MulDigValArray,
	  int length,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetDigitalValue_s(
	  int connectType,
	  ref int intensity,
	  int ChannelIndex,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetON_OFF_s(
	  int connectType,
	  int ChannelIndex,
	  int on_off,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetMulON_OFF_s(
	  int connectType,
	  ClassLibControllerDll.MulONOFF_s[] MulONOFFArray,
	  int length,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetON_OFF_s(
	  int connectType,
	  int ChannelIndex,
	  ref int on_off,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetTriggerPolarity_s(
	  int connectType,
	  int triggerPolarity,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetTriggerPolarity_s(
	  int connectType,
	  ref int triggerPolarity,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetChannelNumberSummary_s(
	  int connectType,
	  ref int channelNumberSummary,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetSoftwareVersion_s(
	  int connectType,
	  ref int softwareVersion,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetControllerModel_s(
	  int connectType,
	  byte[] controllerModel,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetColorTemperature_s(
	  int connectType,
	  int ChannelIndex,
	  int colorTemperature,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int SetMulColorTemperature_s(
	  int connectType,
	  ClassLibControllerDll.MulColorTemperature_s[] MulColorTemperatureArray,
	  int length,
	  long controllerHandle);

	[DllImport("sControllerDll.dll", CallingConvention = CallingConvention.Cdecl)]
	public static extern int GetColorTemperature_s(
	  int connectType,
	  ref int colorTemperature,
	  int ChannelIndex,
	  long controllerHandle);

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Adapter_prm
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 132)]
		public char[] cSn;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] cIp;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Host_prm
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
		public char[] cSn;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] cIp;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] cMac;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Controller_prm
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
		public char[] cSn;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] cIp;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] cSm;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public char[] cGw;
		public char DHCP;
	}

	public struct MulDigitalValue_s
	{
		public int channelIndex;
		public int DigitalValue;
	}

	public struct MulDigitalValue
	{
		public int channelIndex;
		public int DigitalValue;
	}

	public struct MulONOFF_s
	{
		public int channelIndex;
		public int onoffState;
	}

	public struct ProgramConfiguration
	{
		public int channelIndex;
		public int intensity;
		public int pulseWidth;
	}

	public struct MulColorTemperature_s
	{
		public int channelIndex;
		public int colorTemperature;
	}
}
