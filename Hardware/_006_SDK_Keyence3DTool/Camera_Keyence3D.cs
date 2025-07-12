using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using KeyenceLib;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HighSpeedDataCallBack(IntPtr pBuffer, uint dwSize, uint dwCount, uint dwNotify, uint dwUser);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HighSpeedDataCallBackForSimpleArray(IntPtr pProfileHeaderArray, IntPtr pHeightProfileArray, IntPtr pLuminanceProfileArray, uint dwLuminanceEnable, uint dwProfileDataCount, uint dwCount, uint dwNotify, uint dwUser);
    public class Camera_Keyence3D : Camera3DBase
    {
        private int _jobId;

        public List<string> _jobs;

        private byte _trigger_index = 0;

        public bool CCDGrabState = false;

        private LJX8IF_ETHERNET_CONFIG _ethernetConfig;

        private ushort _wPort;

        private ushort _highSpeedPortNo;

        private byte _bySendPosition;

        private int _currentDeviceId;

        private int _xImageSize = 0;

        private int _yImageSize = 1000;

        private int _yImageSizeAcquired;

        private double x_pitch_mm = 1.0;

        private double z_pitch_mm = 0.0016;

        private short z_offset_pixel = short.MinValue;

        private double x_offset_mm = 0.0;

        private double y_offset_mm = 0.0;

        private double z_offset_mm = 0.0;

        private DeviceData _deviceData;

        private Hardware._006_SDK_Keyence3DTool.LJX8IF_PROFILE_INFO _profileInfo;

        private static bool _isBufferFull;

        private static bool _isStopCommunicationByError;

        private Hardware._006_SDK_Keyence3DTool.HighSpeedDataCallBackForSimpleArray _callbackSimpleArray;

        private static Dictionary<string, KeyenceConfig> KeyenceConfigData;

        private static Dictionary<int, int> DeviceIdList;

        [NonSerialized]
        private SafeBufferExt _bufHeight;

        [NonSerialized]
        private SafeBufferExt _bufLuminance;

        private uint pdwTriggerCount = 0u;

        private int plEncoderCount = 0;

        private int numCount = 0;

        public LJX8IF_ETHERNET_CONFIG EthernetConfig
        {
            get
            {
                return _ethernetConfig;
            }
            set
            {
                _ethernetConfig = value;
            }
        }

        public ushort HighSpeedPortNo
        {
            get
            {
                return _highSpeedPortNo;
            }
            set
            {
                _highSpeedPortNo = value;
            }
        }

        public uint ProfileCount => _profileCount;

        static Camera_Keyence3D()
        {
            _isBufferFull = false;
            _isStopCommunicationByError = false;
            KeyenceConfigData = new Dictionary<string, KeyenceConfig>();
            DeviceIdList = new Dictionary<int, int>();
            InitialDll();
            string path = Environment.CurrentDirectory + "\\KeyenceConfig.xml";
            List<KeyenceConfig> keyenceConfigs = new List<KeyenceConfig>();
            if (File.Exists(path))
            {
                keyenceConfigs = XmlHelper.ReadXML<List<KeyenceConfig>>(path);
            }
            if (keyenceConfigs == null)
            {
                return;
            }
            KeyenceConfigData.Clear();
            foreach (KeyenceConfig item in keyenceConfigs)
            {
                KeyenceConfigData.Add(item.LaserHead, item);
            }
        }

        public Camera_Keyence3D(LJX8IF_ETHERNET_CONFIG ethernetConfig, int currentDeviceId)
        {
            _ethernetConfig = ethernetConfig;
            _cameraIp = $"{_ethernetConfig.abyIpAddress[0].ToString()}.{_ethernetConfig.abyIpAddress[1].ToString()}.{_ethernetConfig.abyIpAddress[2].ToString()}.{_ethernetConfig.abyIpAddress[3].ToString()}";
            _wPort = _ethernetConfig.wPortNo;
            _cameraVendor = CameraBase.Cam3DVendor[1];
            _deviceData = new DeviceData();
            _currentDeviceId = currentDeviceId;
        }

        public Camera_Keyence3D(LJX8IF_ETHERNET_CONFIG ethernetConfig, int currentDeviceId, ushort highSpeedPortNo, byte bySendPosition, uint profileCount)
            : this(ethernetConfig, currentDeviceId)
        {
            _highSpeedPortNo = highSpeedPortNo;
            _bySendPosition = bySendPosition;
            _profileCount = profileCount;
        }

        public static void InitialDll()
        {
            int rc = 0;
            if (NativeMethods.LJX8IF_Initialize() != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + " DLL初始化失败");
            }
        }

        public int InitialSnap()
        {
            if (DeviceIdList.ContainsKey(_currentDeviceId))
            {
                for (int i = 0; i < 6; i++)
                {
                    if (!DeviceIdList.ContainsKey(i))
                    {
                        _currentDeviceId = i;
                        break;
                    }
                }
            }
            if (_currentDeviceId >= 6)
            {
                LogUtil.LogError("Keyence相机数量超出");
                return -1;
            }
            _callbackSimpleArray = ReceiveHighSpeedSimpleArray;
            _version = GetDllVersion();
            int rc = 0;
            rc = NativeMethods.LJX8IF_EthernetOpen(_currentDeviceId, ref _ethernetConfig);
            if (rc != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + " Ethernet通讯连接失败");
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
            }
            else
            {
                _deviceData.Status = DeviceStatus.Ethernet;
                _cameraSn = GetSerialNumber();
                headModel = GetHeadModel();
                try
                {
                    LogUtil.Log("Keyence激光头型号：" + headModel);
                    if (KeyenceConfigData.ContainsKey(headModel))
                    {
                        z_pitch_mm = KeyenceConfigData[headModel].Coefficient;
                    }
                    else
                    {
                        LogUtil.LogError("Keyence配置文件中不存在该相机型号(" + headModel + ")参数，请查询相关型号添加至配置文件");
                    }
                }
                catch
                {
                }
                SetBatchPoints((int)_profileCount);
                GetAcqLineRate();
                GetExpourse();
                GetYPitch();
                GetDeviceName();
                SetBatchPointsStatus(1);
                if (!CameraOperator.camera3DCollection._3DCameras.ContainsKey(_cameraSn))
                {
                    CameraOperator.camera3DCollection.Add(_cameraSn, this);
                }
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(_cameraSn, true);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
            }
            if (rc == 0)
            {
                DeviceIdList.Add(_currentDeviceId, _currentDeviceId);
            }
            return rc;
        }

        public override int Open_Sensor()
        {
            return InitialSnap();
        }

        public override void SetEncoderResolution(ref double newEncoderResolution)
        {
            encoderResolution = newEncoderResolution;
            SettingParams.EncoderResolution = encoderResolution;
        }

        public int StartHighSpeedDataCommunication()
        {
            ThreadSafeBuffer.ClearBuffer(0);
            _deviceData.ProfileDataHighSpeed.Clear();
            _deviceData.SimpleArrayDataHighSpeed.Clear();
            if (NativeMethods.LJX8IF_InitializeHighSpeedDataCommunicationSimpleArray(_currentDeviceId, ref _ethernetConfig, HighSpeedPortNo, _callbackSimpleArray, ProfileCount, 0u) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 初始化高速数据通讯失败");
                laserState = false;
            }
            else
            {
                _deviceData.Status = DeviceStatus.EthernetFast;
                laserState = true;
            }
            Hardware._006_SDK_Keyence3DTool.LJX8IF_HIGH_SPEED_PRE_START_REQUEST request = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_HIGH_SPEED_PRE_START_REQUEST);
            request.bySendPosition = _bySendPosition;
            Hardware._006_SDK_Keyence3DTool.LJX8IF_PROFILE_INFO profileInfo = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_PROFILE_INFO);
            if (NativeMethods.LJX8IF_PreStartHighSpeedDataCommunication(_currentDeviceId, ref request, ref profileInfo) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 请求开始准备高速数据通讯失败");
                laserState = false;
            }
            else
            {
                _xImageSize = profileInfo.nProfileDataCount;
                x_pitch_mm = (double)profileInfo.lXPitch / 100000.0;
                _deviceData.SimpleArrayDataHighSpeed.Clear();
                _deviceData.SimpleArrayDataHighSpeed.DataWidth = profileInfo.nProfileDataCount;
                _deviceData.SimpleArrayDataHighSpeed.IsLuminanceEnable = profileInfo.byLuminanceOutput == 1;
                _profileInfo = profileInfo;
                laserState = true;
            }
            ThreadSafeBuffer.ClearBuffer(_currentDeviceId);
            _deviceData.ProfileDataHighSpeed.Clear();
            _isBufferFull = false;
            _isStopCommunicationByError = false;
            return NativeMethods.LJX8IF_StartHighSpeedDataCommunication(_currentDeviceId);
        }

        public int FinalizeSnap()
        {
            if (NativeMethods.LJX8IF_StopHighSpeedDataCommunication(_currentDeviceId) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 停止高速数据通讯失败");
            }
            if (NativeMethods.LJX8IF_FinalizeHighSpeedDataCommunication(_currentDeviceId) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 结束高速数据通讯失败");
            }
            int rc = NativeMethods.LJX8IF_CommunicationClose(_currentDeviceId);
            if (rc != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 中断Ethernet通讯失败");
            }
            if (rc == 0)
            {
                laserState = false;
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                CameraOperator.camera3DCollection.Remove(_cameraSn);
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(_cameraSn, false);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
            }
            return rc;
        }

        public static void FinalizeDll()
        {
            if (NativeMethods.LJX8IF_Initialize() == 0)
            {
                LogUtil.Log(CameraBase.Cam3DVendor[1] + " DLL 释放成功");
            }
            else
            {
                LogUtil.Log(CameraBase.Cam3DVendor[1] + " DLL 释放失败");
            }
        }

        public int StartSnap()
        {
            acqOk = false;
            bStopFlag = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            int start = 0;
            int rc2 = NativeMethods.LJX8IF_GetTriggerAndPulseCount(_currentDeviceId, ref pdwTriggerCount, ref start);
            int rc = NativeMethods.LJX8IF_StartMeasure(_currentDeviceId);
            if (rc != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 开启批处理失败");
            }
            else
            {
                if (rc2 != 0)
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 编码器起始计数获取异常");
                }
                Task.Run(delegate
                {
                    while (true)
                    {
                        timeSpan = DateTime.Now - now;
                        if (acqOk || timeSpan.TotalMilliseconds > timeout)
                        {
                            break;
                        }
                        Thread.Sleep(3);
                    }
                    if (!bStopFlag)
                    {
                        StopSnap();
                    }
                    if (timeSpan.TotalMilliseconds <= timeout)
                    {
                        int num = 0;
                        rc2 = NativeMethods.LJX8IF_GetTriggerAndPulseCount(_currentDeviceId, ref pdwTriggerCount, ref num);
                        if (rc2 != 0)
                        {
                            LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 编码器结束计数获取异常");
                        }
                        LogUtil.Log($"{CameraBase.Cam3DVendor[1]}:{_cameraSn} 采集正常，起始：{start}，结束：{num}，接收脉冲数：{num - start}，编码器分辨率：{encoderResolution}，行程：{(double)(num - start) * encoderResolution}mm");
                    }
                    if (timeSpan.TotalMilliseconds > timeout)
                    {
                        int num2 = 0;
                        rc2 = NativeMethods.LJX8IF_GetTriggerAndPulseCount(_currentDeviceId, ref pdwTriggerCount, ref num2);
                        if (rc2 != 0)
                        {
                            LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 编码器结束计数获取异常");
                        }
                        LogUtil.LogError($"{CameraBase.Cam3DVendor[1]}:{_cameraSn} 采集超时 ，起始：{start}，结束：{num2}，接收脉冲数：{num2 - start}，编码器分辨率：{encoderResolution}，行程：{(double)(num2 - start) * encoderResolution}mm");
                    }
                });
            }
            return rc;
        }

        public override int Start_Grab(bool state)
        {
            int rc = StartHighSpeedDataCommunication();
            if (rc == 0)
            {
                CCDGrabState = true;
            }
            else
            {
                CCDGrabState = false;
            }
            return rc;
        }

        public override int SoftTriggerOnce()
        {
            return StartSnap();
        }

        public void StopSnap()
        {
            if (NativeMethods.LJX8IF_StopMeasure(_currentDeviceId) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 停止批处理失败");
            }
            else
            {
                bStopFlag = true;
            }
        }

        public override int Stop_Grab(bool state)
        {
            if (!CCDGrabState)
            {
                return 0;
            }
            StopSnap();
            if (NativeMethods.LJX8IF_StopHighSpeedDataCommunication(_currentDeviceId) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 停止高速数据通讯失败");
            }
            int rc = NativeMethods.LJX8IF_FinalizeHighSpeedDataCommunication(_currentDeviceId);
            if (rc != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[1] + ":" + _cameraSn + " 结束高速数据通讯失败");
            }
            return rc;
        }

        public override void Close_Sensor()
        {
            FinalizeSnap();
            DeviceIdList.Remove(_currentDeviceId);
        }

        public string GetDllVersion()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_VERSION_INFO versionInfo = NativeMethods.LJX8IF_GetVersion();
            return $"{versionInfo.nMajorNumber}.{versionInfo.nMinorNumber}.{versionInfo.nRevisionNumber}.{versionInfo.nBuildNumber}";
        }

        public string GetSerialNumber()
        {
            int rc = 0;
            byte[] controllerSerialNo = new byte[16];
            byte[] headSerialNo = new byte[16];
            IntPtr pControllerSerialNo = Marshal.AllocHGlobal(16);
            IntPtr pHeadSerialNo = Marshal.AllocHGlobal(16);
            if (NativeMethods.LJX8IF_GetSerialNumber(_currentDeviceId, pControllerSerialNo, pHeadSerialNo) != 0)
            {
                Marshal.FreeHGlobal(pControllerSerialNo);
                Marshal.FreeHGlobal(pHeadSerialNo);
                return string.Empty;
            }
            Marshal.Copy(pControllerSerialNo, controllerSerialNo, 0, controllerSerialNo.Length);
            Marshal.Copy(pHeadSerialNo, headSerialNo, 0, headSerialNo.Length);
            Marshal.FreeHGlobal(pControllerSerialNo);
            Marshal.FreeHGlobal(pHeadSerialNo);
            return $"{Encoding.UTF8.GetString(controllerSerialNo).TrimEnd(default(char))}({Encoding.UTF8.GetString(headSerialNo).TrimEnd(default(char))})";
        }

        public string GetHeadModel()
        {
            int rc = 0;
            byte[] headModel = new byte[32];
            IntPtr pHeadModel = Marshal.AllocHGlobal(32);
            if (NativeMethods.LJX8IF_GetHeadModel(_currentDeviceId, pHeadModel) != 0)
            {
                Marshal.FreeHGlobal(pHeadModel);
                return string.Empty;
            }
            Marshal.Copy(pHeadModel, headModel, 0, headModel.Length);
            Marshal.FreeHGlobal(pHeadModel);
            return Encoding.UTF8.GetString(headModel).TrimEnd(default(char));
        }

        public bool SetTriggerMode_k(int triggerId)
        {
            if (triggerId < 0 || triggerId > 2)
            {
                return false;
            }
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 1;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4]
            {
            (byte)triggerId,
            0,
            0,
            0
            };
            if (SetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _trigger_index = (byte)triggerId;
                return true;
            }
            return false;
        }

        public override void SetTriggerMode(TriggerMode3D triggerMode)
        {
            triggerMode3D = triggerMode;
            switch (triggerMode)
            {
                case TriggerMode3D.Time_ExternTrigger:
                    SetTriggerMode_k(0);
                    break;
                case TriggerMode3D.Time_Software:
                    SetTriggerMode_k(0);
                    break;
                case TriggerMode3D.Encoder_ExternTrigger:
                    SetTriggerMode_k(2);
                    break;
                case TriggerMode3D.Encoder_Software:
                    SetTriggerMode_k(2);
                    break;
            }
            SettingParams.TriggerMode = (int)triggerMode;
        }

        public bool GetTriggerMode()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 1;
            targetSetting.byItem = 6;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            if (GetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _trigger_index = data[0];
                return true;
            }
            return false;
        }

        public bool GetDeviceName()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 1;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 0;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[32];
            if (GetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _cameraModelName = Encoding.UTF8.GetString(data).TrimEnd(default(char));
                return true;
            }
            return false;
        }

        public bool SetAcqLineRate_k(int acqLineRate_index)
        {
            if (acqLineRate_index < 0 || acqLineRate_index > 13)
            {
                return false;
            }
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 2;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4]
            {
            (byte)acqLineRate_index,
            0,
            0,
            0
            };
            if (SetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _acqLineRate_index = (byte)acqLineRate_index;
                SettingParams.AcqLineRateIndex = _acqLineRate_index;
                return true;
            }
            return false;
        }

        public override void SetAcqLineRateIndex(ref int index)
        {
            SetAcqLineRate_k(index);
        }

        public bool GetAcqLineRate()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 2;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            if (GetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _acqLineRate_index = data[0];
                return true;
            }
            return false;
        }

        public bool SetExposure_k(int exposureIndex)
        {
            if (exposureIndex < 0 || exposureIndex > 9)
            {
                return false;
            }
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 1;
            targetSetting.byItem = 6;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4]
            {
            (byte)exposureIndex,
            0,
            0,
            0
            };
            if (SetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _expourse_index = (byte)exposureIndex;
                SettingParams.ExposureIndex = _expourse_index;
                return true;
            }
            return false;
        }

        public override void SetExpoIndex(ref int index)
        {
            SetExposure_k(index);
        }

        private bool GetExpourse()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 1;
            targetSetting.byItem = 6;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            if (GetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _expourse_index = data[0];
                return true;
            }
            return false;
        }

        public bool SetBatchPointsStatus(int status)
        {
            if (status < 0 || status > 1)
            {
                return false;
            }
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 3;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            if (SetSetting(data: new byte[4]
            {
            (byte)status,
            0,
            0,
            0
            }, DeviceId: _currentDeviceId, depth: 1, targetSetting: targetSetting))
            {
                return true;
            }
            return false;
        }

        public bool SetBatchPoints(int yImageSize)
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 10;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            data[1] = (byte)(yImageSize >> 8);
            data[0] = (byte)((uint)yImageSize & 0xFFu);
            if (SetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _yImageSize = yImageSize;
                _profileCount = (uint)yImageSize;
                return true;
            }
            return false;
        }

        public override void SetScanLines(ref int length)
        {
            SetBatchPoints(length);
            SettingParams.ScanLines = length;
        }

        public bool GetBatchPoints()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byCategory = 0;
            targetSetting.byItem = 10;
            targetSetting.byType = 16;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            if (GetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                _yImageSize = data[0];
                return true;
            }
            return false;
        }

        public bool SetYPitchEnable(int isEnabled)
        {
            if (isEnabled < 0 || isEnabled > 1)
            {
                return false;
            }
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 4;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4]
            {
            (byte)isEnabled,
            0,
            0,
            0
            };
            return SetSetting(_currentDeviceId, 1, targetSetting, data);
        }

        public bool SetYPitch_k(int y_pitch)
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 5;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            data[3] = (byte)(y_pitch >> 24);
            data[2] = (byte)((uint)(y_pitch >> 16) & 0xFFu);
            data[1] = (byte)((uint)(y_pitch >> 8) & 0xFFu);
            data[0] = (byte)((uint)y_pitch & 0xFFu);
            if (SetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                return true;
            }
            return false;
        }

        public override void SetYPitch(ref double yPitch)
        {
            int y_pitch_temp = (int)yPitch * 10000;
            bool flag = SetYPitch_k(y_pitch_temp);
            if (triggerMode3D == TriggerMode3D.Encoder_Software || triggerMode3D == TriggerMode3D.Encoder_ExternTrigger || flag)
            {
                y_pitch_mm = yPitch;
            }
            SettingParams.y_pitch_mm = y_pitch_mm;
            SetYPitchEnable(1);
        }

        public bool GetYPitch()
        {
            Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting = default(Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING);
            targetSetting.byType = 16;
            targetSetting.byCategory = 0;
            targetSetting.byItem = 5;
            targetSetting.byTarget1 = 0;
            targetSetting.byTarget2 = 0;
            targetSetting.byTarget3 = 0;
            targetSetting.byTarget4 = 0;
            byte[] data = new byte[4];
            if (GetSetting(_currentDeviceId, 1, targetSetting, data))
            {
                int value = (data[0] & 0xFF) | ((data[1] & 0xFF) << 8) | ((data[2] & 0xFF) << 16) | ((data[3] & 0xFF) << 24);
                y_pitch_mm = (double)value / 10000.0;
                return true;
            }
            return false;
        }

        public override double GetXPitch()
        {
            return x_pitch_mm;
        }

        public bool SetJob(int jobId)
        {
            if (jobId < 0 || jobId > 15)
            {
                return false;
            }
            if (NativeMethods.LJX8IF_ChangeActiveProgram(_currentDeviceId, (byte)jobId) != 0)
            {
                return false;
            }
            _jobId = jobId;
            return true;
        }

        public bool GetJob()
        {
            byte jobIdByte = 0;
            if (NativeMethods.LJX8IF_GetActiveProgram(_currentDeviceId, ref jobIdByte) != 0)
            {
                return false;
            }
            _jobId = jobIdByte;
            return true;
        }

        private bool SetSetting(int DeviceId, byte depth, Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting, byte[] data)
        {
            using NativeMethods.PinnedObject pin = new NativeMethods.PinnedObject(data);
            uint error = 0u;
            if (NativeMethods.LJX8IF_SetSetting(DeviceId, depth, targetSetting, pin.Pointer, (uint)data.Length, ref error) == 0 && error == 0)
            {
                return true;
            }
            return false;
        }

        private bool GetSetting(int DeviceId, byte depth, Hardware._006_SDK_Keyence3DTool.LJX8IF_TARGET_SETTING targetSetting, byte[] data)
        {
            using NativeMethods.PinnedObject pin = new NativeMethods.PinnedObject(data);
            if (NativeMethods.LJX8IF_GetSetting(DeviceId, depth, targetSetting, pin.Pointer, (uint)data.Length) != 0)
            {
                return false;
            }
            return true;
        }

        private void ReceiveHighSpeedSimpleArray(IntPtr headBuffer, IntPtr profileBuffer, IntPtr luminanceBuffer, uint isLuminanceEnable, uint profileSize, uint count, uint notify, uint user)
        {
            acqOk = true;
            _isBufferFull = _deviceData.SimpleArrayDataHighSpeed.AddReceivedData(profileBuffer, luminanceBuffer, count);
            _deviceData.SimpleArrayDataHighSpeed.Notify = notify;
            LogUtil.Log($"Keyence[{_cameraSn}]取像反馈代码Notify={notify},采集行数Count={count}");
            if ((notify != 0 && notify != 65536) || count == 0)
            {
                return;
            }
            if (numCount == 0 || notify == 0)
            {
                _yImageSizeAcquired = (int)count;
                long copySize = Marshal.SizeOf(typeof(ushort)) * (count * _xImageSize);
                int copyLength = Marshal.SizeOf(typeof(ushort)) * _xImageSize * _yImageSize;
                _bufHeight = new SafeBufferExt(copyLength);
                _bufLuminance = new SafeBufferExt(copyLength);
                NativeMethods.CopyMemory(_bufHeight, profileBuffer, (UIntPtr)(ulong)copySize);
                NativeMethods.CopyMemory(_bufLuminance, luminanceBuffer, (UIntPtr)(ulong)copySize);
                dataContext.xResolution = x_pitch_mm;
                dataContext.yResolution = y_pitch_mm;
                dataContext.zResolution = z_pitch_mm;
                dataContext.xOffset = x_offset_mm;
                dataContext.yOffset = y_offset_mm;
                dataContext.zOffset = z_offset_mm;
                laserData = new double[_yImageSize, _xImageSize];
                laserData = CSVOperator.GetPointCloudDataKeyence(_bufHeight, _xImageSize, _yImageSize, dataContext, z_offset_pixel);
                if (ShowPointCloudDelegate != null)
                {
                    ShowPointCloudDelegate(laserData, x_pitch_mm, y_pitch_mm);
                }
                Task.Run(delegate
                {
                    CogImage16Range externCogImage = ImageData.Keyence3DTransformToRange(dataContext, _xImageSize, _yImageSize, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeHL);
                    ImageData obj = new ImageData(externCogImage);
                    if (UpdateImage != null)
                    {
                        UpdateImage(obj);
                    }
                });
            }
            if (notify == 0)
            {
                numCount++;
            }
            else
            {
                numCount = 0;
            }
        }
    }
}
