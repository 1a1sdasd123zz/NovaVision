using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using NovaVision.BaseClass;

namespace NovaVision.Hardware._011_SDK_SSZN3DTool
{
    public class Camera_SSZN : Camera3DBase
    {
        private delegate void AddTextDel(string str);

        private enum SR7IF_SETTING_ITEM
        {
            TRIG_MODE = 1,
            SAMPLED_CYCLE = 2,
            BATCH_ON_OFF = 3,
            ENCODER_TYPE = 7,
            REFINING_POINTS = 9,
            BATCH_POINT = 10,
            CYCLICAL_PATTERN = 16,
            Z_MEASURING_RANGE = 259,
            SENSITIVITY = 261,
            EXP_TIME = 262,
            LIGHT_CONTROL = 267,
            LIGHT_MAX = 268,
            LIGHT_MIN = 269,
            PEAK_SENSITIVITY = 271,
            PEAK_SELECT = 273,
            X_SAMPLING = 514,
            FILTER_X_MEDIAN = 522,
            FILTER_X_SMOOTH = 523,
            FILTER_Y_MEDIAN = 524,
            FILTER_Y_SMOOTH = 525,
            CHANGE_3D_25D = 12288,
            X_PIXEL = 12289,
            X_PITCH = 12290
        }

        private double heightRange = 8.4;

        public bool b_connect = false;

        public bool b_HighSpeedInitFail = false;

        public bool b_camBOnline = false;

        private SR7IF_BatchOneTimeCallBack batchOneTimeCallBack;

        private int m_BatchWait = 0;

        private int mBatchPoint = 0;

        private int mBatchWidth = 3200;

        private int mYscale = 0;

        private int mXscale = 0;

        private double x_pitch_mm = 0.0;

        private int[][] HeightData = new int[2][];

        private byte[][] GrayData = new byte[2][];

        private SR7IF_ETHERNET_CONFIG _ethernetConfig;

        private int _currentDeviceId = 0;

        private int _currentHead = 0;

        public bool CCDGrabState = false;

        private static Dictionary<int, int> DeviceIdList = new Dictionary<int, int>();

        [NonSerialized]
        private SafeBufferExt _bufHeight;

        [NonSerialized]
        private SafeBufferExt _bufLuminance;

        private string[] headers = new string[2] { "A", "B" };

        public Camera_SSZN(SR7IF_ETHERNET_CONFIG methernetConfig, int currentDeviceId)
        {
            _ethernetConfig = methernetConfig;
            _cameraIp = $"{_ethernetConfig.abyIpAddress[0].ToString()}.{_ethernetConfig.abyIpAddress[1].ToString()}.{_ethernetConfig.abyIpAddress[2].ToString()}.{_ethernetConfig.abyIpAddress[3].ToString()}";
            _cameraVendor = CameraBase.Cam3DVendor[3];
            _currentDeviceId = currentDeviceId;
        }

        private void BatchOneTimeCallBack(IntPtr info, IntPtr data)
        {
            SR7IF_STR_CALLBACK_INFO coninfo = default(SR7IF_STR_CALLBACK_INFO);
            coninfo = (SR7IF_STR_CALLBACK_INFO)Marshal.PtrToStructure(info, typeof(SR7IF_STR_CALLBACK_INFO));
            if (coninfo.returnStatus == -100)
            {
                return;
            }
            acqOk = true;
            mBatchPoint = coninfo.BatchPoints;
            mBatchWidth = coninfo.xPoints;
            HeightData[0] = new int[mBatchPoint * mBatchWidth];
            GrayData[0] = new byte[mBatchPoint * mBatchWidth];
            HeightData[1] = new int[mBatchPoint * mBatchWidth];
            GrayData[1] = new byte[mBatchPoint * mBatchWidth];
            for (int k = 0; k < HeightData[0].Length; k++)
            {
                HeightData[0][k] = -1000000;
                HeightData[1][k] = -1000000;
            }
            mYscale = mBatchPoint / 560;
            mXscale = mBatchWidth / 800;
            if (mBatchPoint < 560)
            {
                mYscale = 1;
            }
            if (mBatchWidth < 800)
            {
                mXscale = 1;
            }
            int mNumP = mBatchPoint * mBatchWidth;
            IntPtr[] mTmpData = new IntPtr[2];
            IntPtr[] mTmpGraydata = new IntPtr[2];
            for (int index = 0; index < coninfo.HeadNumber; index++)
            {
                mTmpData[index] = SR7LinkFunc.SR7IF_GetBatchProfilePoint(data, index);
                mTmpGraydata[index] = SR7LinkFunc.SR7IF_GetBatchIntensityPoint(data, index);
                if (mTmpData[index] != IntPtr.Zero)
                {
                    Marshal.Copy(mTmpData[index], HeightData[index], 0, mNumP);
                }
                else
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + " 内存不足,相机头" + headers[index] + "高度数据获取失败");
                }
                if (mTmpGraydata[index] != IntPtr.Zero)
                {
                    Marshal.Copy(mTmpGraydata[index], GrayData[index], 0, mNumP);
                }
                else
                {
                    LogUtil.Log(CameraBase.Cam3DVendor[3] + " ,相机头" + headers[index] + "灰度数据未输出");
                }
            }
            dataContext.xResolution = x_pitch_mm;
            dataContext.yResolution = y_pitch_mm;
            dataContext.zResolution = 1.0;
            dataContext.zOffset = 0.0;
            int[] HeightDataJoint = new int[mBatchPoint * mBatchWidth * 2];
            double[,] receiveBuffer;
            if (b_camBOnline)
            {
                receiveBuffer = new double[mBatchPoint, mBatchWidth * 2];
                for (int j = 0; j < mBatchPoint; j++)
                {
                    for (int m = 0; m < mBatchWidth; m++)
                    {
                        int value2 = HeightData[0][j * mBatchWidth + m];
                        int value3 = HeightData[1][j * mBatchWidth + m];
                        if (value2 <= -1000000)
                        {
                            receiveBuffer[j, m] = double.NaN;
                        }
                        else
                        {
                            receiveBuffer[j, m] = (double)value2 * dataContext.zResolution / 100000.0 + dataContext.zOffset;
                        }
                        if (value3 <= -1000000)
                        {
                            receiveBuffer[j, mBatchWidth + m] = double.NaN;
                        }
                        else
                        {
                            receiveBuffer[j, mBatchWidth + m] = (double)value3 * dataContext.zResolution / 100000.0 + dataContext.zOffset;
                        }
                    }
                    Array.Copy(HeightData[0], j * mBatchWidth, HeightDataJoint, j * mBatchWidth * 2, mBatchWidth);
                    Array.Copy(HeightData[1], j * mBatchWidth, HeightDataJoint, j * mBatchWidth * 2 + mBatchWidth, mBatchWidth);
                }
            }
            else
            {
                receiveBuffer = new double[mBatchPoint, mBatchWidth];
                for (int i = 0; i < mBatchPoint; i++)
                {
                    for (int l = 0; l < mBatchWidth; l++)
                    {
                        int value = HeightData[0][i * mBatchWidth + l];
                        if (value <= -1000000)
                        {
                            receiveBuffer[i, l] = double.NaN;
                        }
                        else
                        {
                            receiveBuffer[i, l] = (double)value * dataContext.zResolution / 100000.0 + dataContext.zOffset;
                        }
                    }
                }
            }
            if (ShowPointCloudDelegate != null)
            {
                ShowPointCloudDelegate(receiveBuffer, mXscale, mYscale);
            }
            Task.Run(delegate
            {
                CogImage16Range cogImage16Range = new CogImage16Range();
                if (b_camBOnline)
                {
                    cogImage16Range = SSZNImageData.LoadSSZN3DImage(HeightDataJoint, 2 * mBatchWidth, mBatchPoint, dataContext.xResolution, dataContext.yResolution);
                }
                else
                {
                    int[] array = new int[mBatchPoint * mBatchWidth * 2];
                    if (mTmpGraydata[0] != IntPtr.Zero)
                    {
                        for (int n = 0; n < mBatchWidth * 2; n++)
                        {
                            for (int num = 0; num < mBatchPoint; num++)
                            {
                                if (n < mBatchWidth)
                                {
                                    array[n + num * mBatchWidth * 2] = HeightData[0][n + num * mBatchWidth];
                                }
                                else
                                {
                                    array[n + num * mBatchWidth * 2] = GrayData[0][n - mBatchWidth + num * mBatchWidth];
                                }
                            }
                        }
                        cogImage16Range = SSZNImageData.LoadSSZN3DImage(array, mBatchWidth * 2, mBatchPoint, dataContext.xResolution, dataContext.yResolution);
                    }
                    else
                    {
                        cogImage16Range = SSZNImageData.LoadSSZN3DImage(HeightData[0], mBatchWidth, mBatchPoint, dataContext.xResolution, dataContext.yResolution);
                    }
                }
                ImageData obj = new ImageData(cogImage16Range);
                if (UpdateImage != null)
                {
                    UpdateImage(obj);
                }
            });
            GC.Collect();
        }

        public unsafe override int Open_Sensor()
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
            if (_currentDeviceId >= 4)
            {
                LogUtil.LogError("SSZN控制器数量超出");
                return -1;
            }
            if (b_connect)
            {
                return 0;
            }
            if (SR7LinkFunc.SR7IF_EthernetOpen(_currentDeviceId, ref _ethernetConfig) == 0)
            {
                b_connect = true;
                using (StreamWriter sw = new StreamWriter("IPSet.bin", append: false, Encoding.GetEncoding("utf-16")))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}\t", _ethernetConfig.abyIpAddress[0]);
                    sb.AppendFormat("{0}\t", _ethernetConfig.abyIpAddress[1]);
                    sb.AppendFormat("{0}\t", _ethernetConfig.abyIpAddress[2]);
                    sb.AppendFormat("{0}\t", _ethernetConfig.abyIpAddress[3]);
                    sw.WriteLine(sb);
                }
                IntPtr str_Model = SR7LinkFunc.SR7IF_GetModels(_currentDeviceId);
                switch (Marshal.PtrToStringAnsi(str_Model))
                {
                    case "SR7050":
                        heightRange = 2.5;
                        break;
                    case "SR7080":
                        heightRange = 6.0;
                        break;
                    case "SR7140":
                        heightRange = 12.0;
                        break;
                    case "SR7240":
                        heightRange = 20.0;
                        break;
                    case "SR7300":
                        heightRange = 144.0;
                        break;
                    case "SR7400":
                        heightRange = 100.0;
                        break;
                    case "SR8020":
                        heightRange = 3.0;
                        break;
                    case "SR8060":
                        heightRange = 9.0;
                        break;
                }
                if (SR7LinkFunc.SR7IF_GetOnlineCameraB(_currentDeviceId) == 0)
                {
                    b_camBOnline = true;
                }
                else
                {
                    b_camBOnline = false;
                    LogUtil.Log(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 相机B不在线");
                }
                IntPtr pSn = SR7LinkFunc.SR7IF_GetHeaderSerial(_currentDeviceId, _currentHead);
                byte[] str = new byte[30];
                Marshal.Copy(pSn, str, 0, 30);
                _cameraSn = Encoding.ASCII.GetString(str).Trim(default(char));
                IntPtr pModel = SR7LinkFunc.SR7IF_GetModels(_currentDeviceId);
                Marshal.Copy(pModel, str, 0, 30);
                _cameraModelName = Encoding.ASCII.GetString(str).Trim(default(char));
                IntPtr pVersion = SR7LinkFunc.SR7IF_GetVersion();
                Marshal.Copy(pVersion, str, 0, 30);
                _version = Encoding.ASCII.GetString(str).Substring(0, 10).Trim(default(char));
                GetExpoIndex();
                GetAcqLineRate();
                GetScanLines();
                GetYPitch();
                GetTriggerMode();
                x_pitch_mm = SR7LinkFunc.SR7IF_ProfileData_XPitch(_currentDeviceId, (IntPtr)(void*)null);
                int[] target = new int[1];
                if (!SetSetting(_currentDeviceId, 0, 3, 1))
                {
                    LogUtil.Log(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 批处理设置失败");
                }
                batchOneTimeCallBack = BatchOneTimeCallBack;
                if (SR7LinkFunc.SR7IF_SetBatchOneTimeDataHandler(_currentDeviceId, batchOneTimeCallBack) != 0)
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 设置回调失败");
                }
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
                DeviceIdList.Add(_currentDeviceId, _currentDeviceId);
                return 0;
            }
            b_connect = false;
            camErrCode = CamErrCode.ConnectFailed;
            isConnected = false;
            LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 相机连接失败");
            return -1;
        }

        public override void Close_Sensor()
        {
            if (SR7LinkFunc.SR7IF_StopMeasure(_currentDeviceId) != 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 停止批处理失败");
            }
            int reT = SR7LinkFunc.SR7IF_CommClose(_currentDeviceId);
            if (reT < 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 设备关闭失败！");
            }
            laserState = false;
            isConnected = false;
            camErrCode = CamErrCode.ConnectFailed;
            CameraOperator.camera3DCollection.Remove(_cameraSn);
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(_cameraSn, false);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
            DeviceIdList.Remove(_currentDeviceId);
        }

        public override int Start_Grab(bool state)
        {
            int reT = -1;
            if (!b_connect)
            {
                return -1;
            }
            if (triggerMode3D == TriggerMode3D.Encoder_Software || triggerMode3D == TriggerMode3D.Time_Software)
            {
                return 0;
            }
            m_BatchWait = 1;
            if (StartGrab(m_BatchWait))
            {
                CCDGrabState = true;
                laserState = true;
                return 0;
            }
            laserState = false;
            CCDGrabState = false;
            return -1;
        }

        public bool StartGrab(int m_BatchWait)
        {
            int reT = SR7LinkFunc.SR7IF_StartMeasureWithCallback(_currentDeviceId, m_BatchWait);
            if (reT < 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 开始批处理失败");
                return false;
            }
            return true;
        }

        public override int Stop_Grab(bool state)
        {
            if (!CCDGrabState)
            {
                return 0;
            }
            if (!b_connect)
            {
                return 0;
            }
            int Ret = SR7LinkFunc.SR7IF_StopMeasure(_currentDeviceId);
            if (Ret < 0)
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 停止批处理失败");
                return -1;
            }
            return 0;
        }

        public override int SoftTriggerOnce()
        {
            acqOk = false;
            bStopFlag = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            m_BatchWait = 0;
            if (!StartGrab(m_BatchWait))
            {
                CCDGrabState = false;
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 软件触发失败");
                return -1;
            }
            CCDGrabState = true;
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
                    Stop_Grab(state: true);
                }
                if (timeSpan.TotalMilliseconds > timeout)
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " SSZN相机采集超时");
                }
            });
            return 0;
        }

        public override void SetExpoIndex(ref int index)
        {
            if (index >= 0 && index <= 11 && _expourse_index != (byte)index)
            {
                if (SetSetting(_currentDeviceId, 0, 262, index))
                {
                    _expourse_index = (byte)index;
                    SettingParams.ExposureIndex = _expourse_index;
                }
                else
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 曝光设置失败");
                }
            }
        }

        public void GetExpoIndex()
        {
            IntPtr data = (IntPtr)0;
            int[] target = new int[2] { 0, 1 };
            if (GetSetting(_currentDeviceId, 1, 262, ref data))
            {
                _expourse_index = (byte)(int)data;
                SettingParams.ExposureIndex = _expourse_index;
            }
            else
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 曝光获取失败");
            }
        }

        public override void SetTriggerMode(TriggerMode3D triggerMode)
        {
            if (triggerMode3D != triggerMode)
            {
                int data = 0;
                triggerMode3D = triggerMode;
                switch (triggerMode)
                {
                    case TriggerMode3D.Time_ExternTrigger:
                        data = 0;
                        break;
                    case TriggerMode3D.Time_Software:
                        data = 0;
                        break;
                    case TriggerMode3D.Encoder_ExternTrigger:
                        data = 2;
                        break;
                    case TriggerMode3D.Encoder_Software:
                        data = 2;
                        break;
                }
                if (SetSetting(_currentDeviceId, 0, 1, data))
                {
                    SettingParams.TriggerMode = (int)triggerMode;
                }
                else
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 触发模式设置失败");
                }
            }
        }

        public void GetTriggerMode()
        {
            IntPtr data = (IntPtr)0;
            if (GetSetting(_currentDeviceId, 0, 1, ref data))
            {
                switch ((int)data)
                {
                    case 0:
                        triggerMode3D = TriggerMode3D.Time_Software;
                        break;
                    case 2:
                        triggerMode3D = TriggerMode3D.Encoder_Software;
                        break;
                    default:
                        triggerMode3D = TriggerMode3D.Time_Software;
                        break;
                }
                SettingParams.TriggerMode = (int)triggerMode3D;
            }
            else
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 触发模式获取失败");
            }
        }

        public override void SetAcqLineRate(ref double newRate)
        {
            if (_acqLineRate != newRate)
            {
                if (SetSetting(_currentDeviceId, 0, 2, (int)newRate))
                {
                    _acqLineRate = newRate;
                    SettingParams.AcqLineRate = (int)_acqLineRate;
                }
                else
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 行频设置失败");
                }
            }
        }

        public void GetAcqLineRate()
        {
            IntPtr data = (IntPtr)0;
            if (GetSetting(_currentDeviceId, 0, 2, ref data))
            {
                _acqLineRate = (long)data;
                SettingParams.AcqLineRate = (int)_acqLineRate;
            }
            else
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 行频获取失败");
            }
        }

        public override void SetScanLines(ref int length)
        {
            if (length >= 50 && length <= 15000 && _profileCount != (uint)length)
            {
                if (SetSetting(_currentDeviceId, 0, 10, length))
                {
                    _profileCount = (uint)length;
                    SettingParams.ScanLines = length;
                }
                else
                {
                    LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 扫描行数设置失败");
                }
            }
        }

        public void GetScanLines()
        {
            IntPtr data = (IntPtr)0;
            if (GetSetting(_currentDeviceId, 0, 10, ref data))
            {
                _profileCount = (uint)(int)data;
                SettingParams.ScanLines = (int)_profileCount;
            }
            else
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[3] + ":" + _cameraSn + " 扫描行数获取失败");
            }
        }

        public override void SetYPitch(ref double yPitch)
        {
            y_pitch_mm = yPitch;
            SettingParams.y_pitch_mm = y_pitch_mm;
        }

        public void GetYPitch()
        {
            SettingParams.y_pitch_mm = y_pitch_mm;
        }

        private bool SetSetting(int lDeviceId, int iAB, int Item, int num)
        {
            int depth = 2;
            int Type = 16;
            int DataSize = 0;
            int Category = 0;
            int[] tar = new int[4] { iAB, 0, 0, 0 };
            if (TransCategory(Item, out Category, out Item, out DataSize) != 0)
            {
                return false;
            }
            byte[] pData = null;
            if (TransNum(num, DataSize, ref pData) != 0)
            {
                return false;
            }
            using PinnedObject pin = new PinnedObject(pData);
            if (SR7LinkFunc.SR7IF_SetSetting((uint)lDeviceId, depth, Type, Category, Item, tar, pin.Pointer, DataSize) != 0)
            {
                return false;
            }
            return true;
        }

        private bool GetSetting(int lDeviceId, int iAB, int Item, ref IntPtr pData)
        {
            int Type = 16;
            int DataSize = 0;
            int Category = 0;
            int[] tar = new int[4] { iAB, 0, 0, 0 };
            int errT = TransCategory(Item, out Category, out Item, out DataSize);
            if (SR7LinkFunc.SR7IF_GetSetting((uint)lDeviceId, Type, Category, Item, tar, ref pData, DataSize) == 0)
            {
                return true;
            }
            return false;
        }

        private int TransCategory(int SupportItem, out int Category, out int Item, out int DataSize)
        {
            Category = SupportItem / 256;
            Item = SupportItem % 256;
            DataSize = 1;
            try
            {
                switch (SupportItem)
                {
                    case 1:
                        DataSize = 1;
                        break;
                    case 2:
                        DataSize = 4;
                        break;
                    case 3:
                        DataSize = 1;
                        break;
                    case 7:
                        DataSize = 1;
                        break;
                    case 9:
                        DataSize = 2;
                        break;
                    case 10:
                        DataSize = 2;
                        break;
                    case 16:
                        DataSize = 1;
                        break;
                    case 259:
                        DataSize = 1;
                        break;
                    case 261:
                        DataSize = 1;
                        break;
                    case 262:
                        DataSize = 1;
                        break;
                    case 267:
                        DataSize = 1;
                        break;
                    case 268:
                        DataSize = 1;
                        break;
                    case 269:
                        DataSize = 1;
                        break;
                    case 271:
                        DataSize = 1;
                        break;
                    case 273:
                        DataSize = 1;
                        break;
                    case 514:
                        DataSize = 1;
                        break;
                    case 522:
                        DataSize = 1;
                        break;
                    case 524:
                        DataSize = 1;
                        break;
                    case 523:
                        DataSize = 1;
                        break;
                    case 525:
                        DataSize = 2;
                        break;
                    case 12288:
                        DataSize = 1;
                        break;
                    default:
                        Item = 0;
                        DataSize = 1;
                        return -1;
                }
            }
            catch (Exception)
            {
                return -2;
            }
            return 0;
        }

        private int TransNum(int num, int DataSize, ref byte[] byteNum)
        {
            try
            {
                byteNum = new byte[DataSize];
                for (int i = 0; i < DataSize; i++)
                {
                    byteNum[i] = (byte)((uint)(num >> i * 8) & 0xFFu);
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }
    }
}
