using Basler.Pylon;
using NovaVision.BaseClass;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaVision.Hardware.SDK_BaslerTool
{
    public class Camera_Basler : Camera2DBase
    {
        private Basler.Pylon.Camera _camera;

        private ICameraInfo _cameraInfo;

        public static Dictionary<string, Basler.Pylon.Camera> D_cameras = new Dictionary<string, Basler.Pylon.Camera>();

        public static Dictionary<string, ICameraInfo> D_devices = new Dictionary<string, ICameraInfo>();

        public static List<ICameraInfo> L_camInfo = new List<ICameraInfo>();

        public static List<Camera_Basler> L_devices = new List<Camera_Basler>();

        private PixelDataConverter converter = new PixelDataConverter();

        public override double Exposure
        {
            get
            {
                try
                {
                    if (_camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                    {
                        return (int)_camera.Parameters[PLCamera.ExposureTimeAbs].GetValue();
                    }
                    return (int)_camera.Parameters[PLCamera.ExposureTime].GetValue();
                }
                catch
                {
                }
                return 0.0;
            }
            set
            {
                try
                {
                    if (_exposure != value)
                    {
                        if (_camera.Parameters.Contains(PLCamera.ExposureTimeAbs))
                        {
                            _camera.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(value);
                        }
                        else
                        {
                            _camera.Parameters[PLCamera.ExposureTime].TrySetValue(value);
                        }
                        _exposure = value;
                    }
                }
                catch
                {
                }
            }
        }

        public Camera_Basler(string externSN)
        {
            SN = externSN;
        }

        public static void EnumCameras()
        {
            D_devices.Clear();
            try
            {
                L_camInfo = CameraFinder.Enumerate();
                foreach (ICameraInfo cameraInfo in L_camInfo)
                {
                    D_devices.Add(cameraInfo[CameraInfoKey.SerialNumber], cameraInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("Basler:" + ex.Message);
            }
        }

        public static Camera_Basler FindCamera(string deviceSN)
        {
            try
            {
                for (int i = 0; i < L_devices.Count; i++)
                {
                    if (L_devices[i].SN == deviceSN)
                    {
                        return L_devices[i];
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override int OpenCamera()
        {
            _camera = new Basler.Pylon.Camera(D_devices[SN]);
            try
            {
                _camera.Parameters[PLCameraInstance.GrabCameraEvents].SetValue(value: true);
                SetSoftwareTriggerMode();
                _camera.CameraOpened += Configuration.SoftwareTrigger;
                _camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                _camera.ConnectionLost += OnConnectionLost;
                if (!_camera.IsOpen)
                {
                    _camera.Open();
                }
                string sn = D_devices[SN][CameraInfoKey.SerialNumber];
                string ipAddress = D_devices[SN][CameraInfoKey.DeviceIpAddress];
                string macAddress = D_devices[SN][CameraInfoKey.DeviceMacAddress];
                string vendorName = D_devices[SN][CameraInfoKey.VendorName];
                string deviceUserId = D_devices[SN][CameraInfoKey.UserDefinedName];
                string friendlyName = D_devices[SN][CameraInfoKey.FriendlyName];
                string modelName = D_devices[SN][CameraInfoKey.ModelName];
                _camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(15L);
                try
                {
                    _camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                }
                catch
                {
                    _camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
                }
                DeviceIP = ipAddress;
                base.vendorName = vendorName;
                base.modelName = modelName;
                _userDefinedName = deviceUserId;
                base.friendlyName = $"{deviceUserId} ({sn})";
                triggerMode = TriggerMode2D.Software;
                DeviceInfoStr = $"{vendorName} | {friendlyName} | {ipAddress} | {modelName} ";
                if (modelName.Substring(modelName.Length - 2).Equals("gc", StringComparison.OrdinalIgnoreCase))
                {
                    whiteBalance.isColorCam = true;
                    GetWhiteBalance();
                }
                if (!D_cameras.ContainsKey(sn))
                {
                    L_devices.Add(this);
                    D_cameras.Add(sn, _camera);
                }
                if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(sn))
                {
                    CameraOperator.camera2DCollection.Add(sn, this);
                }
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(sn, true);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                return 0;
            }
            catch
            {
                LogUtil.LogError("序列号为" + SN + "的相机开启失败！");
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
                return -1;
            }
        }

        public void OnConnectionLost(object sender, EventArgs e)
        {
            try
            {
                string info = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.FriendlyName];
                string sn = ((Basler.Pylon.Camera)sender).CameraInfo[CameraInfoKey.SerialNumber];
                CameraOperator.camera2DCollection.Remove(sn);
                L_camInfo = CameraFinder.Enumerate();
                Console.WriteLine("相机连接中断！相机信息：" + info);
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(sn, true);
                    cam_Handle.CamConnectedLostHandle(cameraMessage);
                }
            }
            catch (Exception)
            {
            }
        }

        public void OnImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            IGrabResult grabResult = e.GrabResult;
            bool imgType = IsMonoData(grabResult);
            if (grabResult.GrabSucceeded)
            {
                int sizeX = grabResult.Width;
                int sizeY = grabResult.Height;
                byte[] buffer = grabResult.PixelData as byte[];
                GCHandle hand = GCHandle.Alloc(buffer, GCHandleType.Pinned);
                IntPtr pr = hand.AddrOfPinnedObject();
                Cognex.VisionPro.ICogImage image;
                if (imgType)
                {
                    image = ImageData.GetMonoImage(sizeY, sizeX, pr);
                }
                else
                {
                    Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                    BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                    converter.OutputPixelFormat = PixelType.BGRA8packed;
                    IntPtr ptrBmp = bmpData.Scan0;
                    converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                    bitmap.UnlockBits(bmpData);
                    image = ImageData.GetOutputRGBImage(bitmap);
                }
                ImageData imaData = new ImageData(image);
                if (UpdateImage != null)
                {
                    UpdateImage(imaData);
                }
                if (hand.IsAllocated)
                {
                    hand.Free();
                }
                GC.Collect();
                acqOk = true;
            }
            else
            {
                LogUtil.LogError($"{grabResult.ErrorCode} {grabResult.ErrorDescription}");
            }
        }

        public void SetSoftwareTriggerMode()
        {
            try
            {
                if (_camera.StreamGrabber.IsGrabbing)
                {
                    _camera.StreamGrabber.Stop();
                }
                _camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                _camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                _camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Software);
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                _camera.StreamGrabber.Start(1L, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                if (FindCamera(SN) != null)
                {
                    FindCamera(SN).triggerMode = TriggerMode2D.Software;
                }
            }
            catch
            {
            }
        }

        public void SetHardwareTriggerMode()
        {
            try
            {
                if (_camera.StreamGrabber.IsGrabbing)
                {
                    _camera.StreamGrabber.Stop();
                }
                _camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.On);
                _camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                _camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                _camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                if (FindCamera(SN) != null)
                {
                    FindCamera(SN).triggerMode = TriggerMode2D.Hardware;
                }
            }
            catch
            {
            }
        }

        public void SetContinousTriggerMode()
        {
            try
            {
                if (_camera.StreamGrabber.IsGrabbing)
                {
                    _camera.StreamGrabber.Stop();
                }
                _camera.Parameters[PLCamera.TriggerMode].TrySetValue(PLCamera.TriggerMode.Off);
                _camera.Parameters[PLCamera.TriggerSelector].TrySetValue(PLCamera.TriggerSelector.FrameStart);
                _camera.Parameters[PLCamera.TriggerSource].TrySetValue(PLCamera.TriggerSource.Line1);
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                _camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                if (FindCamera(SN) != null)
                {
                    FindCamera(SN).triggerMode = TriggerMode2D.Continous;
                }
            }
            catch
            {
            }
        }

        public override void SetExposure(double exposure)
        {
            Exposure = exposure;
            if (FindCamera(SN) != null)
            {
                FindCamera(SN)._exposure = exposure;
            }
            SettingParams.ExposureTime = (int)exposure;
        }

        public override void SetTriggerMode(TriggerMode2D triggerMode)
        {
            if (FindCamera(SN) != null)
            {
                switch (triggerMode)
                {
                    case TriggerMode2D.Software:
                        SetSoftwareTriggerMode();
                        triggerMode = TriggerMode2D.Software;
                        break;
                    case TriggerMode2D.Hardware:
                        SetHardwareTriggerMode();
                        triggerMode = TriggerMode2D.Hardware;
                        break;
                    case TriggerMode2D.Continous:
                        SetContinousTriggerMode();
                        triggerMode = TriggerMode2D.Continous;
                        break;
                }
                SettingParams.TriggerMode = (int)triggerMode;
            }
        }

        public override void SetCamName(string name)
        {
            ModifyCamName(name);
        }

        public override int SoftwareTriggerOnce()
        {
            try
            {
                acqOk = false;
                bStopFlag = false;
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = default(TimeSpan);
                SetTriggerMode(TriggerMode2D.Software);
                _camera.ExecuteSoftwareTrigger();
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
                    if (timeSpan.TotalMilliseconds > timeout)
                    {
                        LogUtil.LogError("Basler(" + SN + ")采集时间超时！");
                    }
                });
                if (acqOk)
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                LogUtil.LogError("Basler(" + SN + ")软触发失败");
            }
            return -1;
        }

        public override void ContinousGrab()
        {
            try
            {
                SetTriggerMode(TriggerMode2D.Continous);
            }
            catch (Exception)
            {
            }
        }

        public override void HardwareGrab()
        {
            try
            {
                SetTriggerMode(TriggerMode2D.Hardware);
            }
            catch (Exception)
            {
            }
        }

        public override int StopGrab()
        {
            try
            {
                if (_camera.StreamGrabber.IsGrabbing)
                {
                    _camera.StreamGrabber.Stop();
                    bStopFlag = true;
                }
                return 0;
            }
            catch
            {
                LogUtil.LogError("Basler(" + SN + ")停止采集失败！");
                return -1;
            }
        }

        public override int CloseCamera()
        {
            try
            {
                CameraOperator.camera2DCollection.Remove(SN);
                _camera.Close();
                _camera.Dispose();
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(SN, false);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public void GetWhiteBalance()
        {
            if (!whiteBalance.isColorCam || _camera == null)
            {
                return;
            }
            _camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Off);
            try
            {
                _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Red);
                whiteBalance.RedColor = (int)_camera.Parameters[PLCamera.BalanceRatioRaw].GetValue();
                _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Blue);
                whiteBalance.BlueColor = (int)_camera.Parameters[PLCamera.BalanceRatioRaw].GetValue();
                _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Green);
                whiteBalance.GreenColor = (int)_camera.Parameters[PLCamera.BalanceRatioRaw].GetValue();
            }
            catch (Exception)
            {
                LogUtil.LogError("Basler（" + SN + "）:获取白平衡值失败");
            }
        }

        public override void AdjustWhiteBalance()
        {
            if (!whiteBalance.isColorCam || _camera == null)
            {
                return;
            }
            _camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Once);
            _camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Off);
            try
            {
                _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Red);
                whiteBalance.RedColor = (int)_camera.Parameters[PLCamera.BalanceRatioRaw].GetValue();
                _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Blue);
                whiteBalance.BlueColor = (int)_camera.Parameters[PLCamera.BalanceRatioRaw].GetValue();
                _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Green);
                whiteBalance.GreenColor = (int)_camera.Parameters[PLCamera.BalanceRatioRaw].GetValue();
            }
            catch (Exception ex)
            {
                LogUtil.LogError("Basler（" + SN + "）:调整白平衡值失败,错误日志：" + ex.Message);
            }
        }

        public override void SetWhiteBalance(CamWhiteBalance camWhiteBalance)
        {
            if (!camWhiteBalance.isColorCam)
            {
                return;
            }
            if (camWhiteBalance.RedColor == 0 || camWhiteBalance.BlueColor == 0 || camWhiteBalance.GreenColor == 0)
            {
                LogUtil.LogError($"Balser：（{SN}）设置白平衡参数有误,当前RedColor={camWhiteBalance.RedColor},BlueColor={camWhiteBalance.BlueColor},GreenColor={camWhiteBalance.GreenColor}");
            }
            else if (_camera != null)
            {
                _camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Off);
                try
                {
                    _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Red);
                    _camera.Parameters[PLCamera.BalanceRatioRaw].TrySetValue(camWhiteBalance.RedColor);
                    _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Blue);
                    _camera.Parameters[PLCamera.BalanceRatioRaw].TrySetValue(camWhiteBalance.BlueColor);
                    _camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Green);
                    _camera.Parameters[PLCamera.BalanceRatioRaw].TrySetValue(camWhiteBalance.GreenColor);
                    whiteBalance.RedColor = camWhiteBalance.RedColor;
                    whiteBalance.GreenColor = camWhiteBalance.GreenColor;
                    whiteBalance.BlueColor = camWhiteBalance.BlueColor;
                }
                catch (Exception ex)
                {
                    LogUtil.LogError("Balser：（" + SN + "）设置白平衡失败，错误日志：" + ex.Message);
                }
            }
        }

        public void ModifyCamName(string name)
        {
            _userDefinedName = name;
        }

        private bool IsMonoData(IGrabResult iGrabResult)
        {
            PixelType pixelTypeValue = iGrabResult.PixelTypeValue;
            PixelType pixelType = pixelTypeValue;
            if (pixelType == PixelType.Mono8)
            {
                return true;
            }
            return false;
        }
    }
}
