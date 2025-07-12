using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using NovaVision.BaseClass;

namespace NovaVision.Hardware._007_SDK_LMI3DTool
{
    public class Camera_LMI3D : Camera3DBase
    {
        public GoSensor _sensor;

        public static GoSystem system;

        public double xResolution;

        public double yResolution;

        public static Dictionary<string, GoSensor> D_sensors;

        public static Dictionary<string, string> D_ips;

        public static List<string> cameraSerialNums;

        public List<string> sensorJobs;

        public string sensorDefaultJob;

        public string sensorState;

        public double z_offset_mm;

        public GoTrigger triggerSource;

        [NonSerialized]
        private SafeBufferExt _bufHeight;

        private SafeBufferExt _bufLuminance;

        public Camera_LMI3D(string externSn)
        {
            _cameraSn = externSn;
            _cameraVendor = CameraBase.Cam3DVendor[0];
        }

        public Camera_LMI3D()
        {
            _cameraVendor = CameraBase.Cam3DVendor[0];
        }

        static Camera_LMI3D()
        {
            D_ips = new Dictionary<string, string>();
            cameraSerialNums = new List<string>();
            D_sensors = new Dictionary<string, GoSensor>();
            KApiLib.Construct();
            GoSdkLib.Construct();
            system = new GoSystem();
        }

        public static void EnumSensors()
        {
            D_sensors.Clear();
            D_ips.Clear();
            cameraSerialNums.Clear();
            try
            {
                for (int i = 0; i < system.SensorCount; i++)
                {
                    GoSensor goSensor = system.GetSensor(i);
                    string serialNumber = goSensor.Id.ToString();
                    string ipStr = goSensor.Address().Address.ToString();
                    D_sensors.Add(serialNumber, goSensor);
                    D_ips.Add(serialNumber, ipStr);
                    cameraSerialNums.Add(serialNumber);
                }
            }
            catch
            {
            }
        }

        public void OpenSensor()
        {
            if (D_sensors.Count > 0)
            {
                try
                {
                    _sensor = D_sensors[_cameraSn];
                    _sensor.Connect();
                    system.EnableData(enable: true);
                    _sensor.SetDataHandler(OnData);
                    _cameraModelName = _sensor.Model;
                    _cameraIp = _sensor.Address().Address.ToString();
                    GoSetup setup = _sensor.Setup;
                    xResolution = SensorInformation.XInterval(_sensor);
                    yResolution = SensorInformation.YInterval(_sensor);
                    y_pitch_mm = yResolution;
                    sensorJobs = SensorInformation.Get_SeneorJob(_sensor);
                    sensorDefaultJob = SensorInformation.Get_SensorDefaultJob(_sensor);
                    sensorState = SensorInformation.Get_SensorState(_sensor);
                    _version = SensorInformation.Get_SensorVersions(system);
                    _acqLineRate = SensorOperation.Get_MaxFrameRate(_sensor);
                    triggerSource = SensorOperation.Get_TriggerSource(_sensor);
                    exposure = SensorOperation.Get_Exposure(_sensor);
                    _scanLength = SensorOperation.GetScanLength(_sensor);
                    speed = SensorOperation.GetTravelSpeed(_sensor);
                    encoderResolution = SensorOperation.GetEncoderResolution(_sensor);
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
                    return;
                }
                catch
                {
                    camErrCode = CamErrCode.ConnectFailed;
                    isConnected = false;
                    LogUtil.LogError(CameraBase.Cam3DVendor[0] + " Gocator连接失败");
                    return;
                }
            }
            LogUtil.LogError(CameraBase.Cam3DVendor[0] + " 未找到LMI相机");
        }

        public void OpenSensorByIp(string ip)
        {
            try
            {
                KIpAddress ipAddress = KIpAddress.Parse(ip);
                _sensor = system.FindSensorByIpAddress(ipAddress);
                _sensor.Connect();
                _cameraIp = ip;
                _cameraSn = _sensor.Id.ToString();
                system.EnableData(enable: true);
                _sensor.SetDataHandler(OnData);
                _cameraModelName = _sensor.Model;
                GoSetup setup = _sensor.Setup;
                xResolution = SensorInformation.XInterval(_sensor);
                yResolution = SensorInformation.YInterval(_sensor);
                y_pitch_mm = yResolution;
                sensorJobs = SensorInformation.Get_SeneorJob(_sensor);
                sensorDefaultJob = SensorInformation.Get_SensorDefaultJob(_sensor);
                sensorState = SensorInformation.Get_SensorState(_sensor);
                _version = SensorInformation.Get_SensorVersions(system);
                _acqLineRate = SensorOperation.Get_MaxFrameRate(_sensor);
                triggerSource = SensorOperation.Get_TriggerSource(_sensor);
                exposure = SensorOperation.Get_Exposure(_sensor);
                _scanLength = SensorOperation.GetScanLength(_sensor);
                speed = SensorOperation.GetTravelSpeed(_sensor);
                encoderResolution = SensorOperation.GetEncoderResolution(_sensor);
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
            catch (Exception ex)
            {
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
                LogUtil.LogError(CameraBase.Cam3DVendor[0] + " Gocator连接失败");
                throw ex;
            }
        }

        public override int Open_Sensor()
        {
            try
            {
                if (_cameraIp != null)
                {
                    OpenSensorByIp(_cameraIp);
                }
                else
                {
                    OpenSensor();
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public override void Close_Sensor()
        {
            _sensor.Disconnect();
            isConnected = false;
            laserState = false;
            camErrCode = CamErrCode.ConnectFailed;
            CameraOperator.camera3DCollection.Remove(_cameraSn);
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(_cameraSn, false);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
        }

        public void UpdateSensorParams()
        {
            _cameraModelName = _sensor.Model;
            _cameraIp = _sensor.Address().Address.ToString();
            GoSetup setup = _sensor.Setup;
            xResolution = SensorInformation.XInterval(_sensor);
            yResolution = SensorInformation.YInterval(_sensor);
            y_pitch_mm = yResolution;
            sensorJobs = SensorInformation.Get_SeneorJob(_sensor);
            sensorDefaultJob = SensorInformation.Get_SensorDefaultJob(_sensor);
            sensorState = SensorInformation.Get_SensorState(_sensor);
            _version = SensorInformation.Get_SensorVersions(system);
            _acqLineRate = SensorOperation.Get_MaxFrameRate(_sensor);
            triggerSource = SensorOperation.Get_TriggerSource(_sensor);
            exposure = SensorOperation.Get_Exposure(_sensor);
            _scanLength = SensorOperation.GetScanLength(_sensor);
            speed = SensorOperation.GetTravelSpeed(_sensor);
            encoderResolution = SensorOperation.GetEncoderResolution(_sensor);
        }

        public void ChangeJob(string newJobName)
        {
            SensorOperation.ChangeJob(_sensor, newJobName);
        }

        public int StartGrab()
        {
            if (_sensor != null)
            {
                try
                {
                    if (_sensor.State == 10)
                    {
                        _sensor.Start();
                    }
                    laserState = true;
                    return 0;
                }
                catch
                {
                    laserState = false;
                    return -1;
                }
            }
            LogUtil.LogError(CameraBase.Cam3DVendor[0] + " 相机对象未初始化");
            laserState = false;
            return -1;
        }

        public override int Start_Grab(bool state)
        {
            return StartGrab();
        }

        public int StopGrab()
        {
            try
            {
                if (_sensor != null)
                {
                    if (_sensor.State == 11)
                    {
                        _sensor.Stop();
                    }
                    bStopFlag = true;
                    laserState = false;
                    return 0;
                }
                LogUtil.LogError(CameraBase.Cam3DVendor[0] + " 相机对象未初始化");
                return -1;
            }
            catch
            {
            }
            return -1;
        }

        public void DestroyCamear()
        {
            if (_sensor != null)
            {
                _sensor.Stop();
                _sensor.Destroy();
            }
        }

        public override int Stop_Grab(bool state)
        {
            return StopGrab();
        }

        public int Trigger()
        {
            try
            {
                if (_sensor != null)
                {
                    StartGrab();
                    acqOk = false;
                    bStopFlag = false;
                    DateTime now = DateTime.Now;
                    TimeSpan timeSpan = default(TimeSpan);
                    Thread.Sleep(20);
                    int start = (int)_sensor.Encoder();
                    _sensor.Trigger();
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
                            StopGrab();
                        }
                        if (timeSpan.TotalMilliseconds <= timeout)
                        {
                            int num = (int)_sensor.Encoder();
                            LogUtil.Log($"{CameraBase.Cam3DVendor[0]}:{_cameraSn} 采集正常，起始：{start}，结束：{num}，接收脉冲数：{num - start}，行程：{(double)(num - start) * SettingParams.EncoderResolution}mm");
                        }
                        if (timeSpan.TotalMilliseconds > timeout)
                        {
                            int num2 = (int)_sensor.Encoder();
                            LogUtil.LogError($"{CameraBase.Cam3DVendor[0]}:{_cameraSn} 采集超时 ，起始：{start}，结束：{num2}，接收脉冲数：{num2 - start}，行程：{(double)(num2 - start) * SettingParams.EncoderResolution}mm");
                            Task.Run(delegate
                            {
                                Bitmap bmp = new Bitmap(100, 100);
                                CogImage16Range externCogImage = new CogImage16Range(bmp);
                                ImageData obj2 = new ImageData(externCogImage);
                                if (UpdateImage != null)
                                {
                                    UpdateImage(obj2);
                                }
                            });
                        }
                    });
                    return 0;
                }
                LogUtil.LogError(CameraBase.Cam3DVendor[0] + " 相机对象未初始化");
                return -1;
            }
            catch
            {
                LogUtil.LogError(CameraBase.Cam3DVendor[0] + ":" + _cameraSn + " 相机采集异常");
                return -1;
            }
        }

        public override int SoftTriggerOnce()
        {
            return Trigger();
        }

        public unsafe void OnData(KObject data)
        {
            LogUtil.Log("LMI(" + _cameraSn + ")进入取相回调！");
            DateTime dt = DateTime.Now;
            acqOk = true;
            GoDataSet dataSet = (GoDataSet)data;
            DataContext context = new DataContext();
            int imageWidth = 0;
            int imageHeight = 0;
            List<GoDataMessageType> goDataMessageTypes = new List<GoDataMessageType>();
            for (uint i = 0u; i < dataSet.Count; i++)
            {
                goDataMessageTypes.Add(((GoDataMsg)dataSet.Get(i)).MessageType);
            }
            for (uint j = 0u; j < dataSet.Count; j++)
            {
                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(j);
                switch ((int)dataObj.MessageType)
                {
                    case 10:
                        {
                            GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                            for (uint k3 = 0u; k3 < measurementMsg.Count; k3++)
                            {
                                Lmi3d.GoSdk.Messages.GoMeasurementData goMeasurementData = measurementMsg.Get(k3);
                            }
                            break;
                        }
                    case 8:
                        {
                            GoUniformSurfaceMsg goSurfaceMsg = (GoUniformSurfaceMsg)dataObj;
                            imageWidth = (int)goSurfaceMsg.Width;
                            imageHeight = (int)goSurfaceMsg.Length;
                            int bufferSize2 = Marshal.SizeOf(typeof(short)) * imageWidth * imageHeight;
                            _bufHeight = new SafeBufferExt(bufferSize2);
                            context.xResolution = xResolution;
                            context.yResolution = yResolution;
                            z_offset_mm = (double)goSurfaceMsg.ZOffset / 1000.0;
                            context.zOffset = 32768.0;
                            context.zResolution = (double)goSurfaceMsg.ZResolution / 1000000.0;
                            byte* pDestBufImage = (byte*)(void*)(IntPtr)_bufHeight;
                            for (int m = 0; m < imageHeight; m++)
                            {
                                for (int k4 = 0; k4 < imageWidth; k4++)
                                {
                                    short originalShort = goSurfaceMsg.Get(m, k4);
                                    ushort originalushort = (ushort)(originalShort + 32768);
                                    byte[] byteArray = BitConverter.GetBytes(originalushort);
                                    pDestBufImage[m * imageWidth * 2 + k4 * 2] = byteArray[0];
                                    pDestBufImage[m * imageWidth * 2 + k4 * 2 + 1] = byteArray[1];
                                }
                            }
                            if (ShowPointCloudDelegate == null)
                            {
                                break;
                            }
                            laserData = new double[imageHeight, imageWidth];
                            for (int l = 0; l < imageHeight; l++)
                            {
                                for (int k5 = 0; k5 < imageWidth; k5++)
                                {
                                    short value2 = goSurfaceMsg.Get(l, k5);
                                    if (value2 == short.MinValue)
                                    {
                                        laserData[l, k5] = double.NaN;
                                    }
                                    else
                                    {
                                        laserData[l, k5] = (double)value2 * context.zResolution + z_offset_mm;
                                    }
                                }
                            }
                            ShowPointCloudDelegate(laserData, xResolution, yResolution);
                            break;
                        }
                    case 9:
                        {
                            GoSurfaceIntensityMsg goSurfaceIntensityMsg = (GoSurfaceIntensityMsg)dataObj;
                            int width2 = (int)goSurfaceIntensityMsg.Width;
                            int height2 = (int)goSurfaceIntensityMsg.Length;
                            int size = width2 * height2;
                            _bufLuminance = new SafeBufferExt(size * 2);
                            byte* pBufImage = (byte*)(void*)goSurfaceIntensityMsg.Data;
                            byte* pDestBufImage2 = (byte*)(void*)(IntPtr)_bufLuminance;
                            for (int n = 0; n < size; n++)
                            {
                                pDestBufImage2[n * 2] = pBufImage[n];
                            }
                            break;
                        }
                    case 28:
                        {
                            GoSurfacePointCloudMsg goSurfacePointCloudMsg = (GoSurfacePointCloudMsg)dataObj;
                            long width = goSurfacePointCloudMsg.Width;
                            long height = goSurfacePointCloudMsg.Length;
                            long bufferSize = width * height;
                            IntPtr bufferPointer = goSurfacePointCloudMsg.Data;
                            SurfacePoints[] surfacePointsCloud = new SurfacePoints[bufferSize];
                            GoPoints[] surfacePoints = new GoPoints[bufferSize];
                            int structSize = Marshal.SizeOf(typeof(GoPoints));
                            laserData = new double[(int)checked((nint)height), (int)checked((nint)width)];
                            context.zOffset = (double)goSurfacePointCloudMsg.ZOffset / 1000.0;
                            context.zResolution = (double)goSurfacePointCloudMsg.ZResolution / 1000000.0;
                            for (int k = 0; k < height; k++)
                            {
                                for (int k2 = 0; k2 < width; k2++)
                                {
                                    IntPtr intPtr = new IntPtr(bufferPointer.ToInt64() + (k * width + k2) * structSize);
                                    surfacePoints[k * width + k2] = (GoPoints)Marshal.PtrToStructure(intPtr, typeof(GoPoints));
                                    short value = surfacePoints[k * width + k2].z;
                                    if (value == short.MinValue)
                                    {
                                        laserData[k, k2] = double.NaN;
                                    }
                                    else
                                    {
                                        laserData[k, k2] = (double)surfacePoints[k * width + k2].z * context.zResolution + context.zOffset;
                                    }
                                }
                            }
                            break;
                        }
                }
            }
            context.zOffset -= z_offset_mm / context.zResolution;
            if (goDataMessageTypes.Contains(8) && !goDataMessageTypes.Contains(9))
            {
                Task.Run(delegate
                {
                    CogImage16Range externCogImage2 = ImageData.Keyence3DTransformToRange(context, imageWidth, imageHeight, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeH);
                    ImageData obj2 = new ImageData(externCogImage2);
                    if (UpdateImage != null)
                    {
                        UpdateImage(obj2);
                    }
                });
            }
            else if (goDataMessageTypes.Contains(8) && goDataMessageTypes.Contains(9))
            {
                Task.Run(delegate
                {
                    CogImage16Range externCogImage = ImageData.Keyence3DTransformToRange(context, imageWidth, imageHeight, _bufHeight, _bufLuminance, RangeImageFormatEnum.rangeHL);
                    ImageData obj = new ImageData(externCogImage);
                    if (UpdateImage != null)
                    {
                        UpdateImage(obj);
                    }
                });
            }
            dataSet.Dispose();
            LogUtil.Log($"LMI({_cameraSn})回调结束，共耗时：{(DateTime.Now - dt).Milliseconds}ms");
        }

        public override void SetExposure(ref double newExposure)
        {
            if (exposure != newExposure)
            {
                GoSetup setup = _sensor.Setup;
                if (_sensor != null)
                {
                    SensorOperation.Set_Exposure(_sensor, newExposure);
                    exposure = newExposure;
                    SettingParams.ExposureTime = (int)exposure;
                }
            }
        }

        public void SetTriggerSource(GoTrigger newTriggerSource)
        {
            if (!(triggerSource == newTriggerSource))
            {
                GoSetup setup = _sensor.Setup;
                if (_sensor != null)
                {
                    SensorOperation.Set_TriggerSource(_sensor, triggerSource);
                }
            }
        }

        public override void SetAcqLineRate(ref double newRate)
        {
            if (_acqLineRate != newRate)
            {
                SensorOperation.Set_FrameRate(_sensor, newRate);
                _acqLineRate = newRate;
                SettingParams.AcqLineRate = (int)_acqLineRate;
            }
        }

        public override void SetScanLength(ref double newLength)
        {
            if (_scanLength != newLength)
            {
                SensorOperation.SetScanFixedLength(_sensor, newLength);
                _scanLength = newLength;
                SettingParams.ScanLength = (int)_scanLength;
            }
        }

        public override void SetTriggerMode(TriggerMode3D triggerMode)
        {
            triggerMode3D = triggerMode;
            switch (triggerMode)
            {
                case TriggerMode3D.Time_ExternTrigger:
                    SensorOperation.Set_TriggerSource(_sensor, 0);
                    SensorOperation.SetTriggerControl(_sensor, 1);
                    break;
                case TriggerMode3D.Time_Software:
                    SensorOperation.Set_TriggerSource(_sensor, 0);
                    SensorOperation.SetTriggerControl(_sensor, 2);
                    break;
                case TriggerMode3D.Encoder_ExternTrigger:
                    SensorOperation.Set_TriggerSource(_sensor, 1);
                    SensorOperation.SetTriggerControl(_sensor, 1);
                    break;
                case TriggerMode3D.Encoder_Software:
                    SensorOperation.Set_TriggerSource(_sensor, 1);
                    SensorOperation.SetTriggerControl(_sensor, 2);
                    break;
                case TriggerMode3D.Test_Time:
                    SensorOperation.Set_TriggerSource(_sensor, 0);
                    SensorOperation.SetTriggerControl(_sensor, 0);
                    break;
            }
            SettingParams.TriggerMode = (int)triggerMode;
        }

        public override void SetTravelSpeedWithEncoderResolution(double encoderResolution, double speed)
        {
            SensorOperation.SetTravelSpeedWithResolution(_sensor, encoderResolution, speed);
        }

        public override void SetTravelSpeed(ref double newSpeed)
        {
            SensorOperation.SetTravelSpeed(_sensor, newSpeed);
            speed = newSpeed;
            SettingParams.Speed = speed;
        }

        public override void SetEncoderResolution(ref double newEncoderResolution)
        {
            SensorOperation.SetEncoderResolution(_sensor, newEncoderResolution);
            encoderResolution = newEncoderResolution;
            SettingParams.EncoderResolution = encoderResolution;
        }

        public override void SetYPitch(ref double yPitch)
        {
            SensorOperation.SetYPitch(_sensor, yPitch);
            y_pitch_mm = yPitch;
            yResolution = yPitch;
            SettingParams.y_pitch_mm = y_pitch_mm;
        }

        public static void CloseSensorSystem()
        {
            if (system != null)
            {
                system.Stop();
                system.Destroy();
            }
        }

        public override void SetROI_Top(ref int top)
        {
            SetROI_Top(top);
        }

        public void SetROI_Top(int newTop)
        {
            try
            {
                double RangeMin = _sensor.Setup.GetActiveAreaHeightLimitMin(_sensor.Role);
                double ZMin = _sensor.Setup.GetActiveAreaZLimitMin(_sensor.Role);
                double ZStart = _sensor.Setup.GetActiveAreaZ(_sensor.Role);
                if (Convert.ToDouble(newTop) > 0.0 - (ZMin + ZStart) || Convert.ToDouble(newTop) < RangeMin)
                {
                    MessageBox.Show($"LMI 高度范围设置超限！上限：{0.0 - (ZMin + ZStart)}，下限：{RangeMin}");
                    LogUtil.Log($"LMI 高度范围设置超限！上限：{0.0 - (ZMin + ZStart)}，下限：{RangeMin}");
                }
                else if (_sensor.Setup.GetActiveAreaHeight(_sensor.Role) != (double)newTop)
                {
                    _sensor.Setup.SetActiveAreaHeight(_sensor.Role, newTop);
                    SettingParams.ROI_Top = newTop;
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex.Message ?? "");
            }
        }

        public override void SetROI_Buttom(ref int buttom)
        {
            SetROI_Buttom(buttom);
        }

        public void SetROI_Buttom(int newButtom)
        {
            try
            {
                double ZMax = _sensor.Setup.GetActiveAreaZLimitMax(_sensor.Role);
                double ZMin = _sensor.Setup.GetActiveAreaZLimitMin(_sensor.Role);
                double range = _sensor.Setup.GetActiveAreaHeight(_sensor.Role);
                if (Convert.ToDouble(newButtom) > ZMax - range || Convert.ToDouble(newButtom) < ZMin)
                {
                    MessageBox.Show($"LMI Z轴起点设置超限！上限：{ZMax - range}，下限：{ZMin}");
                    LogUtil.Log($"LMI Z轴起点设置超限！上限：{ZMax - range}，下限：{ZMin}");
                }
                else if (_sensor.Setup.GetActiveAreaZ(_sensor.Role) != (double)newButtom)
                {
                    _sensor.Setup.SetActiveAreaZ(_sensor.Role, newButtom);
                    SettingParams.ROI_Buttom = newButtom;
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError(ex.Message ?? "");
            }
        }

        public override void SetROI_Top_Buttom(ref int top, ref int buttom)
        {
            SetROI_Buttom(buttom);
            SetROI_Top(top);
        }
    }
}
