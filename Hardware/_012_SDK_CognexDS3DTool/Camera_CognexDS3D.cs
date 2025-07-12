using Cognex.VisionPro;
using NovaVision.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovaVision.Hardware._012_SDK_CognexDS3DTool
{
    public class Camera_CognexDS3D : Camera3DBase
    {
        private Cognex.VisionPro.ICogAcqFifo _camera;

        private Cognex.VisionPro.ICogFrameGrabber cameraFrame;

        private static bool EnumCamerasState = false;

        public bool CCDGrabState = false;

        private int mYscale = 0;

        private int mXscale = 0;

        private int ROIX = 0;

        private int ROIY = 0;

        private int roiWidth = 0;

        public static Dictionary<string, Cognex.VisionPro.ICogFrameGrabber> D_devices = new Dictionary<string, Cognex.VisionPro.ICogFrameGrabber>();

        public static Dictionary<string, string> CCD_SNIP = new Dictionary<string, string>();

        public int numAcqs;

        private int trigNums = 0;

        public bool SyncronousAcquire = false;

        public Camera_CognexDS3D(string ip)
        {
            if (!EnumCamerasState)
            {
                EnumCameras();
            }
            _cameraVendor = CameraBase.Cam3DVendor[4];
            if (CCD_SNIP.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in CCD_SNIP)
                {
                    if (item.Value == ip)
                    {
                        _cameraSn = item.Key;
                    }
                }
            }
            if (_cameraSn == null || _cameraSn == "")
            {
                LogUtil.LogError("创建CognexDS相机失败！");
            }
        }

        public static void EnumCameras()
        {
            D_devices.Clear();
            try
            {
                CogFrameGrabbers fs = new CogFrameGrabbers();
                for (int i = 0; i < fs.Count; i++)
                {
                    Cognex.VisionPro.ICogFrameGrabber cogFrameGrabber = fs[i];
                    if (cogFrameGrabber.Name.ToLower().Contains("cognex") && cogFrameGrabber.Name.Contains("DS"))
                    {
                        D_devices.Add(cogFrameGrabber.SerialNumber, cogFrameGrabber);
                        CCD_SNIP.Add(cogFrameGrabber.SerialNumber, cogFrameGrabber.OwnedGigEAccess.CurrentIPAddress);
                    }
                }
                if (fs.Count > 0)
                {
                    EnumCamerasState = true;
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("cognexDS相机枚举失败:" + ex.Message);
            }
        }

        public void Acq_Completed(object sender, CogCompleteEventArgs e)
        {
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
                    image = (CogImage16Range)_camera.CompleteAcquireEx(info);
                }
                else
                {
                    LogUtil.LogError("CognexDS (" + _cameraSn + "): Ready count is not greater than 0.");
                }
                ImageReady_Handler(image);
            }
            catch (Exception ce)
            {
                LogUtil.LogError("CognexDS (" + _cameraSn + "): The following error has occured\n" + ce.Message);
                GC.Collect();
            }
        }

        private unsafe void ImageReady_Handler(Cognex.VisionPro.ICogImage img)
        {
            acqOk = true;
            CogImage16Range image = (CogImage16Range)img;
            if (image != null)
            {
                int width = image.Width;
                int height = image.Height;
                CogTransform2DLinear cogTransform2DLinear = (CogTransform2DLinear)image.PixelFromRootTransform;
                mXscale = (int)cogTransform2DLinear.ScalingX;
                mYscale = (int)cogTransform2DLinear.ScalingY;
                CogImage16Grey mGrey = image.GetPixelData();
                IntPtr mptr = mGrey.Get16GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height).Scan0;
                int Stride = mGrey.Get16GreyPixelMemory(CogImageDataModeConstants.ReadWrite, 0, 0, width, height).Stride;
                double[,] PixelValueData = new double[height, Stride];
                ushort* p = (ushort*)mptr.ToPointer();
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < Stride; j++)
                    {
                        ushort PixelValue = p[i * Stride + j];
                        if (PixelValue == 0)
                        {
                            PixelValueData[i, j] = double.NaN;
                        }
                        else
                        {
                            PixelValueData[i, j] = (double)(int)PixelValue * dataContext.zResolution + dataContext.zOffset;
                        }
                    }
                }
                if (ShowPointCloudDelegate != null)
                {
                    ShowPointCloudDelegate(PixelValueData, mXscale, mYscale);
                }
            }
            if (image != null)
            {
                ImageData imaData = new ImageData(image);
                if (UpdateImage != null)
                {
                    UpdateImage(imaData);
                }
                GC.Collect();
            }
        }

        public override int Open_Sensor()
        {
            try
            {
                cameraFrame = D_devices[_cameraSn];
                _camera = cameraFrame.CreateAcqFifo(cameraFrame.AvailableVideoFormats[0], CogAcqFifoPixelFormatConstants.Format16Grey, 0, autoPrepare: true);
                _camera.Complete += Acq_Completed;
                GetCameraInfo();
                CCD = _camera;
                triggerMode3D = TriggerMode3D.Time_Software;
                _camera.Flush();
                if (!CameraOperator.camera3DCollection._3DCameras.ContainsKey(_cameraSn))
                {
                    CameraOperator.camera3DCollection.Add(_cameraSn, this);
                }
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                LogUtil.Log("CognexDS (" + _cameraSn + "):相机开启成功！");
                return 0;
            }
            catch
            {
                LogUtil.LogError("CognexDS (" + _cameraSn + "):相机开启失败！");
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
        }

        public override void Close_Sensor()
        {
            if (_camera == null)
            {
                return;
            }
            try
            {
                _camera.FrameGrabber.Disconnect(AllowRecovery: true);
                laserState = false;
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                CameraOperator.camera3DCollection.Remove(_cameraSn);
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(_cameraSn, false);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                LogUtil.Log("CognexDS (" + _cameraSn + "):相机关闭成功！");
            }
            catch
            {
                LogUtil.LogError("CognexDS (" + _cameraSn + "):相机关闭失败！");
            }
        }

        public override int Start_Grab(bool state)
        {
            SyncronousAcquire = false;
            try
            {
                _camera.OwnedLineScanParams.ResetCounter();
            }
            catch (Exception)
            {
            }
            if (triggerMode3D == TriggerMode3D.Encoder_Software || triggerMode3D == TriggerMode3D.Time_Software)
            {
                return 0;
            }
            SetHardwareTriggerMode();
            return 0;
        }

        public override int Stop_Grab(bool state)
        {
            try
            {
                SetSoftwareTriggerMode();
                bStopFlag = true;
                return 0;
            }
            catch
            {
                LogUtil.LogError("CognexDS (" + _cameraSn + "):停止采集失败！");
                return -1;
            }
        }

        public override int SoftTriggerOnce()
        {
            SetSoftwareTriggerMode();
            Task.Factory.StartNew(AcquireImageMethod);
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
                Cognex.VisionPro.ICogAcqLineScan mLineScan = _camera.OwnedLineScanParams;
                int start = 0;
                if (mLineScan != null)
                {
                    start = mLineScan.CurrentEncoderCount;
                }
                else
                {
                    LogUtil.LogError("CognexDS (" + _cameraSn + "):编码器起始计数获取异常！");
                }
                Task.Run(delegate
                {
                    while (true)
                    {
                        timeSpan = DateTime.Now - now;
                        if (acqOk || timeSpan.TotalMilliseconds > _camera.Timeout)
                        {
                            break;
                        }
                        Thread.Sleep(3);
                    }
                    if (!bStopFlag)
                    {
                        Stop_Grab(state: true);
                    }
                    if (timeSpan.TotalMilliseconds > _camera.Timeout)
                    {
                        int num = 0;
                        double num2 = 0.0;
                        if (mLineScan != null)
                        {
                            num = mLineScan.CurrentEncoderCount;
                            num2 = SettingParams.EncoderResolution;
                        }
                        else
                        {
                            LogUtil.LogError("CognexDS (" + _cameraSn + "):编码器结束计数获取异常！");
                        }
                        LogUtil.LogError($"CognexDS ({_cameraSn}): 采集超时 ，起始：{start}，结束：{num}，接收脉冲数：{num - start}，行程：{(double)(num - start) * num2}mm");
                    }
                });
                Cognex.VisionPro.ICogImage retImg;
                try
                {
                    retImg = _camera.Acquire(out var _);
                    int end2 = 0;
                    double EncoderResolution2 = 0.0;
                    if (mLineScan != null)
                    {
                        end2 = mLineScan.CurrentEncoderCount;
                        EncoderResolution2 = SettingParams.EncoderResolution;
                    }
                    else
                    {
                        LogUtil.LogError("CognexDS (" + _cameraSn + "):编码器结束计数获取异常！");
                    }
                    LogUtil.Log($"CognexDS ({_cameraSn}): 采集正常，起始：{start}，结束：{end2}，接收脉冲数：{end2 - start}，行程：{(double)(end2 - start) * EncoderResolution2}mm");
                }
                catch (Exception ex)
                {
                    retImg = null;
                    int end = 0;
                    double EncoderResolution = 0.0;
                    if (mLineScan != null)
                    {
                        end = mLineScan.CurrentEncoderCount;
                        EncoderResolution = SettingParams.EncoderResolution;
                    }
                    else
                    {
                        LogUtil.LogError("CognexDS (" + _cameraSn + "):编码器结束计数获取异常！");
                    }
                    LogUtil.Log($"CognexDS ({_cameraSn}): 采集异常[{ex.Message}]，起始：{start}，结束：{end}，接收脉冲数：{end - start}，行程：{(double)(end - start) * EncoderResolution}mm");
                }
                ImageReady_Handler(retImg);
            }
            finally
            {
                SyncronousAcquire = false;
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
                mTrigger.TriggerModel = CogAcqTriggerModelConstants.Semi;
                mTrigger.TriggerLowToHigh = true;
            }
            _camera.Flush();
        }

        public void SetEncoderMode()
        {
            if (_camera != null)
            {
                Cognex.VisionPro.ICogAcqLineScan mLineScan = _camera.OwnedLineScanParams;
                if (mLineScan != null)
                {
                    mLineScan.MotionInput = CogMotionInputConstants.Encoder;
                    return;
                }
                _camera.FrameGrabber.OwnedGigEAccess.SetFeature("MotionInput", CogMotionInputConstants.Encoder.ToString());
                _camera.Flush();
            }
        }

        public void SetSimulatedEncoderMode()
        {
            if (_camera != null)
            {
                Cognex.VisionPro.ICogAcqLineScan mLineScan = _camera.OwnedLineScanParams;
                if (mLineScan != null)
                {
                    mLineScan.MotionInput = CogMotionInputConstants.SimulatedEncoder;
                    return;
                }
                _camera.FrameGrabber.OwnedGigEAccess.SetFeature("MotionInput", CogMotionInputConstants.SimulatedEncoder.ToString());
                _camera.Flush();
            }
        }

        public override void SetCameraInfo(CamSettingParams CamParams)
        {
            if (_camera == null)
            {
                return;
            }
            Cognex.VisionPro.ICogAcqRangeImage acqRangeImage = _camera.OwnedRangeImageParams;
            if (acqRangeImage != null)
            {
                acqRangeImage.ZScale = CamParams.ZScale;
                acqRangeImage.ZOffset = CamParams.ZOffset;
                acqRangeImage.XScale = CamParams.XScale;
            }
            _camera.OwnedROIParams?.SetROIXYWidthHeight(CamParams.ROIX, CamParams.ROIY, CamParams.ROIWidth, CamParams.ROIHeight);
            Cognex.VisionPro.ICogAcqTrigger mTrigger = _camera.OwnedTriggerParams;
            if (mTrigger != null)
            {
                mTrigger.TriggerEnabled = CamParams.TriggerEnabled;
                mTrigger.TriggerModel = (CogAcqTriggerModelConstants)CamParams.TriggerModel;
                mTrigger.TriggerLowToHigh = CamParams.TriggerLowToHigh;
            }
            Cognex.VisionPro.ICogAcqExposure acqExposure = _camera.OwnedExposureParams;
            if (acqExposure != null)
            {
                acqExposure.Exposure = CamParams.Exposure;
            }
            _camera.TimeoutEnabled = CamParams.TimeoutEnabled;
            _camera.Timeout = CamParams.Timeout;
            LogUtil.Log("CognexDS (" + _cameraSn + "):设置相机参数！");
            Cognex.VisionPro.ICogAcqLineScan mLineScan = _camera.OwnedLineScanParams;
            if (mLineScan != null)
            {
                mLineScan.DistancePerCycle = CamParams.DistancePerCycle;
                mLineScan.AcquireDirectionPositive = CamParams.AcquireDirectionPositive;
                mLineScan.ResetCounterOnHardwareTrigger = CamParams.ResetCounterOnHardwareTrigger;
                mLineScan.TriggerFromEncoder = CamParams.TriggerFromEncoder;
                mLineScan.EncoderOffset = CamParams.EncoderOffset;
                mLineScan.UseSingleChannel = CamParams.UseSingleChannel;
                mLineScan.StartAcqOnEncoderCount = CamParams.StartAcqOnEncoderCount;
                mLineScan.IgnoreBackwardsMotionBetweenAcquires = CamParams.IgnoreBackwardsMotionBetweenAcquires;
                mLineScan.EncoderResolution = (CogEncoderResolutionConstants)CamParams.EncoderResolution;
                mLineScan.IgnoreTooFastEncoder = CamParams.IgnoreTooFastEncoder;
                mLineScan.TestEncoderDirectionPositive = CamParams.TestEncoderDirectionPositive;
                mLineScan.TestEncoderEnabled = CamParams.TestEncoderEnabled;
                mLineScan.ProfileCameraPositiveEncoderDirection = (CogProfileCameraDirectionConstants)CamParams.ProfileCameraPositiveEncoderDirection;
                mLineScan.ProfileCameraAcquireDirection = (CogProfileCameraDirectionConstants)CamParams.ProfileCameraAcquireDirection;
                mLineScan.MotionInput = (CogMotionInputConstants)CamParams.MotionInput;
                mLineScan.ExpectedMotionSpeed = CamParams.ExpectedMotionSpeed;
                mLineScan.ExpectedDistancePerLine = CamParams.ExpectedDistancePerLine;
                if (CCDSettingParams.StepsPerLine != 0)
                {
                    mLineScan.SetStepsPerLine((int)CCDSettingParams.StepsPerLine, CCDSettingParams.Step16thsPerLine);
                }
            }
            Cognex.VisionPro.ICogAcqProfileCamera AcqProfileCamera = _camera.OwnedProfileCameraParams;
            if (AcqProfileCamera != null)
            {
                AcqProfileCamera.CameraMode = (CogAcqCameraModeConstants)CamParams.CameraMode;
                AcqProfileCamera.ZDetectionEnable = CamParams.ZDetectionEnable;
                AcqProfileCamera.ZDetectionEnable2 = CamParams.ZDetectionEnable2;
                if (AcqProfileCamera.ZDetectionEnable)
                {
                    AcqProfileCamera.ZDetectionHeight = CamParams.ZDetectionHeight;
                    AcqProfileCamera.ZDetectionSampling = CamParams.ZDetectionSampling;
                    AcqProfileCamera.ZDetectionBase = CamParams.ZDetectionBase;
                }
                if (AcqProfileCamera.ZDetectionEnable2)
                {
                    AcqProfileCamera.ZDetectionHeight2 = CamParams.ZDetectionHeight2;
                    AcqProfileCamera.ZDetectionSampling2 = CamParams.ZDetectionSampling2;
                    AcqProfileCamera.ZDetectionBase2 = CamParams.ZDetectionBase2;
                }
                AcqProfileCamera.DetectionSensitivity = CamParams.DetectionSensitivity;
                AcqProfileCamera.LaserMode = (CogAcqLaserModeConstants)CamParams.LaserMode;
                AcqProfileCamera.HighDynamicRange = CamParams.HighDynamicRange;
                AcqProfileCamera.BridgeDetectionZones = CamParams.BridgeDetectionZones;
                AcqProfileCamera.LinkDetectionZones = CamParams.LinkDetectionZones;
                AcqProfileCamera.TriggerType = (CogProfileCameraTriggerTypeConstants)CamParams.TriggerType;
                AcqProfileCamera.LaserDetectionMode = (CogProfileCameraLaserDetectionModeConstants)CamParams.LaserDetectionMode;
                AcqProfileCamera.PeakDetectionMode = (CogProfileCameraPeakDetectionModeConstants)CamParams.PeakDetectionMode;
                AcqProfileCamera.MinimumBinarizationLineWidth = CamParams.MinimumBinarizationLineWidth;
                AcqProfileCamera.BinarizationThreshold = CamParams.BinarizationThreshold;
                AcqProfileCamera.TriggerSignal = (CogProfileCameraTriggerSignalConstants)CamParams.TriggerSignal;
            }
            if (mLineScan != null)
            {
                CCDSettingParams = CamParams;
                _camera.Flush();
                return;
            }
            _camera.FrameGrabber.OwnedGigEAccess.SetDoubleFeature("ExpectedMotionSpeed", CamParams.ExpectedMotionSpeed);
            _camera.FrameGrabber.OwnedGigEAccess.SetFeature("MotionInput", ((CogMotionInputConstants)CamParams.MotionInput).ToString());
            _camera.FrameGrabber.OwnedGigEAccess.SetDoubleFeature("DistancePerCycle", CamParams.DistancePerCycle);
            _camera.FrameGrabber.OwnedGigEAccess.SetIntegerFeature("StepsPerLine", CamParams.StepsPerLine);
            _camera.FrameGrabber.OwnedGigEAccess.SetDoubleFeature("XScale", CamParams.XScale);
            CCDSettingParams = CamParams;
            CCD = _camera;
        }

        public override void GetCameraInfo()
        {
            CCDSettingParams = new CamSettingParams();
            if (cameraFrame == null)
            {
                return;
            }
            _cameraSn = cameraFrame.SerialNumber;
            _cameraModelName = cameraFrame.Name.Split(':')[2];
            _cameraIp = cameraFrame.OwnedGigEAccess.CurrentIPAddress;
            if (_camera != null)
            {
                Cognex.VisionPro.ICogAcqRangeImage acqRangeImage = _camera.OwnedRangeImageParams;
                if (acqRangeImage != null)
                {
                    dataContext.zResolution = acqRangeImage.ZScale;
                    dataContext.zOffset = acqRangeImage.ZOffset;
                    CCDSettingParams.ZScale = acqRangeImage.ZScale;
                    CCDSettingParams.ZOffset = acqRangeImage.ZOffset;
                    CCDSettingParams.XScale = acqRangeImage.XScale;
                }
                Cognex.VisionPro.ICogAcqROI cogAcqROI = _camera.OwnedROIParams;
                if (cogAcqROI != null)
                {
                    cogAcqROI.GetROIXYWidthHeight(out var ROIX, out var ROIY, out var ROIW, out var ROIH);
                    CCDSettingParams.ROIX = ROIX;
                    CCDSettingParams.ROIY = ROIY;
                    CCDSettingParams.ROIWidth = ROIW;
                    CCDSettingParams.ROIHeight = ROIH;
                }
                Cognex.VisionPro.ICogAcqTrigger mTrigger = _camera.OwnedTriggerParams;
                if (mTrigger != null)
                {
                    CCDSettingParams.TriggerEnabled = mTrigger.TriggerEnabled;
                    CCDSettingParams.TriggerModel = (int)mTrigger.TriggerModel;
                    CCDSettingParams.TriggerLowToHigh = mTrigger.TriggerLowToHigh;
                }
                Cognex.VisionPro.ICogAcqExposure acqExposure = _camera.OwnedExposureParams;
                if (acqExposure != null)
                {
                    CCDSettingParams.Exposure = acqExposure.Exposure;
                }
                CCDSettingParams.TimeoutEnabled = _camera.TimeoutEnabled;
                CCDSettingParams.Timeout = _camera.Timeout;
                LogUtil.Log("CognexDS (" + _cameraSn + "):获取相机参数！");
                Cognex.VisionPro.ICogAcqLineScan mLineScan = _camera.OwnedLineScanParams;
                if (mLineScan != null)
                {
                    CCDSettingParams.DistancePerCycle = mLineScan.DistancePerCycle;
                    CCDSettingParams.AcquireDirectionPositive = mLineScan.AcquireDirectionPositive;
                    CCDSettingParams.ResetCounterOnHardwareTrigger = mLineScan.ResetCounterOnHardwareTrigger;
                    CCDSettingParams.TriggerFromEncoder = mLineScan.TriggerFromEncoder;
                    CCDSettingParams.EncoderOffset = mLineScan.EncoderOffset;
                    CCDSettingParams.UseSingleChannel = mLineScan.UseSingleChannel;
                    CCDSettingParams.StartAcqOnEncoderCount = mLineScan.StartAcqOnEncoderCount;
                    CCDSettingParams.IgnoreBackwardsMotionBetweenAcquires = mLineScan.IgnoreBackwardsMotionBetweenAcquires;
                    CCDSettingParams.EncoderResolution = (int)mLineScan.EncoderResolution;
                    CCDSettingParams.IgnoreTooFastEncoder = mLineScan.IgnoreTooFastEncoder;
                    CCDSettingParams.TestEncoderDirectionPositive = mLineScan.TestEncoderDirectionPositive;
                    CCDSettingParams.TestEncoderEnabled = mLineScan.TestEncoderEnabled;
                    CCDSettingParams.ProfileCameraPositiveEncoderDirection = (int)mLineScan.ProfileCameraPositiveEncoderDirection;
                    CCDSettingParams.ProfileCameraAcquireDirection = (int)mLineScan.ProfileCameraAcquireDirection;
                    CCDSettingParams.MotionInput = (int)mLineScan.MotionInput;
                    CCDSettingParams.ExpectedMotionSpeed = mLineScan.ExpectedMotionSpeed;
                    CCDSettingParams.ExpectedDistancePerLine = mLineScan.ExpectedDistancePerLine;
                    mLineScan.GetStepsPerLine(out var stepsPerLine, out var step16thsPerLine);
                    CCDSettingParams.StepsPerLine = (uint)stepsPerLine;
                    CCDSettingParams.Step16thsPerLine = step16thsPerLine;
                }
                Cognex.VisionPro.ICogAcqProfileCamera AcqProfileCamera = _camera.OwnedProfileCameraParams;
                if (AcqProfileCamera != null)
                {
                    CCDSettingParams.CameraMode = (int)AcqProfileCamera.CameraMode;
                    CCDSettingParams.ZDetectionHeight = AcqProfileCamera.ZDetectionHeight;
                    CCDSettingParams.ZDetectionHeight2 = AcqProfileCamera.ZDetectionHeight2;
                    CCDSettingParams.ZDetectionEnable = AcqProfileCamera.ZDetectionEnable;
                    CCDSettingParams.ZDetectionEnable2 = AcqProfileCamera.ZDetectionEnable2;
                    CCDSettingParams.ZDetectionSampling = AcqProfileCamera.ZDetectionSampling;
                    CCDSettingParams.ZDetectionSampling2 = AcqProfileCamera.ZDetectionSampling2;
                    CCDSettingParams.DetectionSensitivity = AcqProfileCamera.DetectionSensitivity;
                    CCDSettingParams.ZDetectionBase = AcqProfileCamera.ZDetectionBase;
                    CCDSettingParams.LaserMode = (int)AcqProfileCamera.LaserMode;
                    CCDSettingParams.HighDynamicRange = AcqProfileCamera.HighDynamicRange;
                    CCDSettingParams.BridgeDetectionZones = AcqProfileCamera.BridgeDetectionZones;
                    CCDSettingParams.LinkDetectionZones = AcqProfileCamera.LinkDetectionZones;
                    CCDSettingParams.TriggerType = (int)AcqProfileCamera.TriggerType;
                    CCDSettingParams.LaserDetectionMode = (int)AcqProfileCamera.LaserDetectionMode;
                    CCDSettingParams.PeakDetectionMode = (int)AcqProfileCamera.PeakDetectionMode;
                    CCDSettingParams.MinimumBinarizationLineWidth = AcqProfileCamera.MinimumBinarizationLineWidth;
                    CCDSettingParams.BinarizationThreshold = AcqProfileCamera.BinarizationThreshold;
                    CCDSettingParams.TriggerSignal = (int)AcqProfileCamera.TriggerSignal;
                    CCDSettingParams.ZDetectionBase2 = AcqProfileCamera.ZDetectionBase2;
                }
                if (mLineScan == null)
                {
                    CCDSettingParams.ExpectedMotionSpeed = _camera.FrameGrabber.OwnedGigEAccess.GetDoubleFeature("ExpectedMotionSpeed");
                    string MotionInput = "";
                    MotionInput = _camera.FrameGrabber.OwnedGigEAccess.GetFeature("MotionInput");
                    CCDSettingParams.MotionInput = (int)(CogMotionInputConstants)Enum.Parse(typeof(CogMotionInputConstants), MotionInput);
                    CCDSettingParams.DistancePerCycle = _camera.FrameGrabber.OwnedGigEAccess.GetDoubleFeature("DistancePerCycle");
                    CCDSettingParams.StepsPerLine = _camera.FrameGrabber.OwnedGigEAccess.GetIntegerFeature("StepsPerLine");
                    CCDSettingParams.XScale = _camera.FrameGrabber.OwnedGigEAccess.GetDoubleFeature("XScale");
                }
            }
        }
    }
}
