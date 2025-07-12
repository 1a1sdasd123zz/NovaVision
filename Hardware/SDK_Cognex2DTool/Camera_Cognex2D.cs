using Cognex.VisionPro;
using NovaVision.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaVision.Hardware.SDK_Cognex2DTool
{
    public class Camera_Cognex2D : Camera2DBase
    {
        private Cognex.VisionPro.ICogAcqFifo _camera;

        public static Dictionary<string, Cognex.VisionPro.ICogAcqFifo> D_cameras = new Dictionary<string, Cognex.VisionPro.ICogAcqFifo>();

        public static Dictionary<string, Cognex.VisionPro.ICogFrameGrabber> D_devices = new Dictionary<string, Cognex.VisionPro.ICogFrameGrabber>();

        public static List<Camera_Cognex2D> L_devices = new List<Camera_Cognex2D>();

        public int numAcqs;

        private int trigNums = 0;

        public bool SyncronousAcquire = false;

        public int acquiredNum;

        public override double Exposure
        {
            get
            {
                try
                {
                    return _camera.OwnedExposureParams.Exposure * 1000.0;
                }
                catch
                {
                    return 0.0;
                }
            }
            set
            {
                try
                {
                    if (_exposure != value)
                    {
                        _camera.OwnedExposureParams.Exposure = value / 1000.0;
                        _exposure = value;
                        _camera.Flush();
                    }
                }
                catch
                {
                }
            }
        }

        public Camera_Cognex2D(string externSN)
        {
            SN = externSN;
        }

        public static void EnumCameras()
        {
            D_devices.Clear();
            CogFrameGrabbers fs = new CogFrameGrabbers();
            for (int i = 0; i < fs.Count; i++)
            {
                Cognex.VisionPro.ICogFrameGrabber cogFrameGrabber = fs[i];
                if (cogFrameGrabber.Name.ToLower().Contains("cognex") && !cogFrameGrabber.Name.Contains("DS"))
                {
                    D_devices.Add(cogFrameGrabber.SerialNumber, cogFrameGrabber);
                }
            }
        }

        public static Camera_Cognex2D FindCamera(string deviceSN)
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
            catch
            {
                return null;
            }
        }

        public override int OpenCamera()
        {
            try
            {
                Cognex.VisionPro.ICogFrameGrabber cogFrameGrabber = D_devices[SN];
                _camera = cogFrameGrabber.CreateAcqFifo(cogFrameGrabber.AvailableVideoFormats[0], CogAcqFifoPixelFormatConstants.Format8Grey, 0, autoPrepare: true);
                SetSoftwareTriggerMode();
                _camera.Complete += Acq_Completed;
                string sn = cogFrameGrabber.SerialNumber;
                string ipAddress = cogFrameGrabber.OwnedGigEAccess.CurrentIPAddress;
                string macAddress = cogFrameGrabber.OwnedGigEAccess.MACAddress;
                string modelName = cogFrameGrabber.Name.Split(':')[2];
                string vendorName = "Cognex";
                string userDefinedName = "XXX";
                _camera.Flush();
                DeviceIP = ipAddress;
                base.vendorName = vendorName;
                base.modelName = modelName;
                _userDefinedName = userDefinedName;
                friendlyName = $"{userDefinedName} ({sn})";
                triggerMode = TriggerMode2D.Software;
                DeviceInfoStr = $"{ipAddress} | {macAddress} | {vendorName} | {modelName}";
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
                LogUtil.LogError("Cognex(" + SN + "开启失败！)");
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
        }

        public void Acq_Completed(object sender, CogCompleteEventArgs e)
        {
            acqOk = true;
            if (SyncronousAcquire)
            {
                SyncronousAcquire = false;
                return;
            }
            CogAcqInfo info = new CogAcqInfo();
            Cognex.VisionPro.ICogImage image = null;
            try
            {
                _camera.GetFifoState(out var _, out var numReadyVal, out var _);
                if (numReadyVal > 0)
                {
                    image = (CogImage8Grey)_camera.CompleteAcquireEx(info);
                }
                else
                {
                    LogUtil.LogError("Cognex(" + SN + ") ready count is not greater than 0.");
                }
                if (image != null)
                {
                    if (UpdateImage != null)
                    {
                        UpdateImage(new ImageData(image));
                    }
                    numAcqs++;
                    if (numAcqs == 10)
                    {
                        numAcqs = 0;
                        GC.Collect();
                    }
                }
            }
            catch (Exception ce)
            {
                LogUtil.LogError("Cognex(" + SN + ")：The following error has occured\n" + ce.Message);
                GC.Collect();
            }
        }

        public void SetSoftwareTriggerMode()
        {
            Cognex.VisionPro.ICogAcqTrigger mTrigger = _camera.OwnedTriggerParams;
            if (mTrigger != null)
            {
                mTrigger.TriggerEnabled = true;
                mTrigger.TriggerModel = CogAcqTriggerModelConstants.Manual;
            }
            _camera.Flush();
        }

        public void SetHardwareTriggerMode()
        {
            Cognex.VisionPro.ICogAcqTrigger mTrigger = _camera.OwnedTriggerParams;
            if (mTrigger != null)
            {
                mTrigger.TriggerEnabled = true;
                mTrigger.TriggerModel = CogAcqTriggerModelConstants.Auto;
                mTrigger.TriggerLowToHigh = true;
            }
            _camera.Flush();
        }

        public void SetContinousTriggerMode()
        {
            Cognex.VisionPro.ICogAcqTrigger mTrigger = _camera.OwnedTriggerParams;
            if (mTrigger != null)
            {
                mTrigger.TriggerEnabled = true;
                mTrigger.TriggerModel = CogAcqTriggerModelConstants.FreeRun;
            }
            _camera.Flush();
        }

        public override void SetExposure(double exposure)
        {
            Exposure = exposure;
            if (FindCamera(SN) != null)
            {
                FindCamera(SN).Exposure = exposure;
            }
            SettingParams.ExposureTime = (int)exposure;
        }

        public override void SetTriggerMode(TriggerMode2D triggerMode)
        {
            switch (triggerMode)
            {
                case TriggerMode2D.Software:
                    SetSoftwareTriggerMode();
                    base.triggerMode = TriggerMode2D.Software;
                    break;
                case TriggerMode2D.Hardware:
                    SetHardwareTriggerMode();
                    base.triggerMode = TriggerMode2D.Hardware;
                    break;
                case TriggerMode2D.Continous:
                    SetContinousTriggerMode();
                    base.triggerMode = TriggerMode2D.Continous;
                    break;
            }
            SettingParams.TriggerMode = (int)triggerMode;
        }

        public override void SetCamName(string name)
        {
            ModifyCamName(name);
        }

        public override int SoftwareTriggerOnce()
        {
            SetSoftwareTriggerMode();
            Thread t = new Thread(AcquireImageMethod);
            t.Start();
            return 0;
        }

        public void AcquireImageMethod()
        {
            SyncronousAcquire = true;
            try
            {
                acqOk = false;
                bStopFlag = false;
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = default(TimeSpan);
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
                        LogUtil.LogError("Cognex(" + SN + ")采集时间超时！");
                    }
                });
                Cognex.VisionPro.ICogImage img = _camera.Acquire(out trigNums);
                UpdateImage(new ImageData(img));
                GC.Collect();
            }
            catch (Exception ex)
            {
                LogUtil.LogError("cognex" + SN + ":" + ex.Message);
                SyncronousAcquire = false;
            }
        }

        public override void ContinousGrab()
        {
            SyncronousAcquire = false;
            SetTriggerMode(TriggerMode2D.Continous);
        }

        public override void HardwareGrab()
        {
            SyncronousAcquire = false;
            SetTriggerMode(TriggerMode2D.Hardware);
        }

        public override int StopGrab()
        {
            try
            {
                SetSoftwareTriggerMode();
                bStopFlag = true;
                return 0;
            }
            catch
            {
                LogUtil.LogError("Cognex(" + SN + ")停止采集失败！");
                return -1;
            }
        }

        public void ModifyCamName(string name)
        {
            _userDefinedName = name;
        }

        public override int CloseCamera()
        {
            if (_camera != null)
            {
                try
                {
                    SetSoftwareTriggerMode();
                    CameraOperator.camera2DCollection.Remove(SN);
                    _camera.FrameGrabber.Disconnect(AllowRecovery: true);
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
                }
            }
            return -1;
        }

        public Cognex.VisionPro.ICogImage GrabOneImage()
        {
            try
            {
                int trigNums = 0;
                Cognex.VisionPro.ICogImage cogImage = _camera.Acquire(out trigNums);
                acquiredNum++;
                if (acquiredNum > 4)
                {
                    GC.Collect();
                    acquiredNum = 0;
                }
                return cogImage;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("非正常取相"))
                {
                }
                return null;
            }
        }

        public static void CloseAllCameras()
        {
            for (int i = 0; i < L_devices.Count; i++)
            {
                try
                {
                    L_devices[i]._camera.FrameGrabber.Disconnect(AllowRecovery: true);
                }
                catch
                {
                }
            }
        }
    }
}
