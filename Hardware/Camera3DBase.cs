using System;

namespace NovaVision.Hardware
{
    public class Camera3DBase : CameraBase
    {
        public Action<double[,], double, double> ShowPointCloudDelegate;

        public string CamCategory = CameraBase.CameraType["3D"];

        public string _cameraSn;

        public string _cameraIp;

        public string _cameraVendor;

        public string _cameraModelName;

        public string headModel;

        public string _version;

        public double _scanLength;

        public uint _profileCount = 1000u;

        public double _acqLineRate;

        public byte _expourse_index = 1;

        public byte _acqLineRate_index = 0;

        public double y_pitch_mm = 0.05;

        public DataContext dataContext = new DataContext();

        public double[,] laserData;

        public int expoIndex;

        public double exposure;

        public double encoderResolution;

        public double speed;

        public bool acqOk;

        public bool bStopFlag = false;

        public double timeout = 5000.0;

        public TriggerMode3D triggerMode3D = TriggerMode3D.None;

        public bool laserState;

        public Cognex.VisionPro.ICogAcqFifo CCD;

        public CamSettingParams CCDSettingParams;

        public virtual void SetExposure(ref double newExposure)
        {
        }

        public virtual void SetExpoIndex(ref int index)
        {
        }

        public virtual void SetTriggerMode(TriggerMode3D triggerMode)
        {
        }

        public virtual void SetAcqLineRate(ref double newRate)
        {
        }

        public virtual void SetAcqLineRateIndex(ref int index)
        {
        }

        public virtual void SetScanLength(ref double newLength)
        {
        }

        public virtual void SetScanLines(ref int length)
        {
        }

        public virtual void SetROI_Top_Buttom(ref int top, ref int buttom)
        {
        }

        public virtual void SetROI_Top(ref int top)
        {
        }

        public virtual void SetROI_Buttom(ref int buttom)
        {
        }

        public virtual void SetTravelSpeedWithEncoderResolution(double encoderResolution, double speed)
        {
        }

        public virtual void SetTravelSpeed(ref double speed)
        {
        }

        public virtual void SetEncoderResolution(ref double encoderResolution)
        {
        }

        public virtual void SetYPitch(ref double yPitch)
        {
        }

        public virtual double GetXPitch()
        {
            return 0.0;
        }

        public virtual void SetCameraInfo(CamSettingParams CamParams)
        {
        }

        public virtual void GetCameraInfo()
        {
        }

        public void SetTimeOut(ref double _timeout)
        {
            if (_timeout > 0.0)
            {
                timeout = _timeout;
            }
            SettingParams.Timeout = timeout;
        }

        public override void SetCameraSetting(CameraConfigData configData)
        {
            if (configData.CamVendor == CameraBase.Cam3DVendor[4])
            {
                if (configData.ACQSettingParams != null)
                {
                    SetCameraInfo(configData.ACQSettingParams);
                }
            }
            else
            {
                if (configData.SettingParams == null)
                {
                    return;
                }
                if (SettingParams.TriggerMode != configData.SettingParams.TriggerMode)
                {
                    SetTriggerMode((TriggerMode3D)configData.SettingParams.TriggerMode);
                }
                if (SettingParams.AcqLineRateIndex != configData.SettingParams.AcqLineRateIndex)
                {
                    int newRate2 = configData.SettingParams.AcqLineRateIndex;
                    SetAcqLineRateIndex(ref newRate2);
                }
                if (SettingParams.AcqLineRate != configData.SettingParams.AcqLineRate)
                {
                    double newRate3 = configData.SettingParams.AcqLineRate;
                    SetAcqLineRate(ref newRate3);
                }
                if (SettingParams.ScanLength != configData.SettingParams.ScanLength)
                {
                    double newRate8 = configData.SettingParams.ScanLength;
                    SetScanLength(ref newRate8);
                }
                if (SettingParams.ScanLines != configData.SettingParams.ScanLines)
                {
                    int newRate7 = configData.SettingParams.ScanLines;
                    SetScanLines(ref newRate7);
                }
                if (SettingParams.ExposureIndex != configData.SettingParams.ExposureIndex)
                {
                    int ExposureIndex = configData.SettingParams.ExposureIndex;
                    SetExpoIndex(ref ExposureIndex);
                }
                if (SettingParams.ExposureTime != configData.SettingParams.ExposureTime)
                {
                    double Exposure = configData.SettingParams.ExposureTime;
                    SetExposure(ref Exposure);
                }
                if (SettingParams.Timeout != configData.SettingParams.Timeout)
                {
                    double newRate6 = configData.SettingParams.Timeout;
                    SetTimeOut(ref newRate6);
                }
                if (SettingParams.EncoderResolution != configData.SettingParams.EncoderResolution)
                {
                    double newRate5 = configData.SettingParams.EncoderResolution;
                    SetEncoderResolution(ref newRate5);
                }
                if (SettingParams.Speed != configData.SettingParams.Speed)
                {
                    double newRate4 = configData.SettingParams.Speed;
                    SetTravelSpeed(ref newRate4);
                }
                if (SettingParams.y_pitch_mm != configData.SettingParams.y_pitch_mm)
                {
                    double yPitch = configData.SettingParams.y_pitch_mm;
                    SetYPitch(ref yPitch);
                }
                if (SettingParams.ROI_Top != configData.SettingParams.ROI_Top || SettingParams.ROI_Buttom != configData.SettingParams.ROI_Buttom)
                {
                    int top = configData.SettingParams.ROI_Top;
                    int buttom = configData.SettingParams.ROI_Buttom;
                    if (top != 0 && buttom != 0)
                    {
                        SetROI_Top_Buttom(ref top, ref buttom);
                    }
                }
                if (configData.CamVendor == CameraBase.Cam3DVendor[1])
                {
                    double newRate = configData.SettingParams.EncoderResolution;
                    SetEncoderResolution(ref newRate);
                }
            }
        }

        public virtual int Open_Sensor()
        {
            return -1;
        }

        public virtual void Close_Sensor()
        {
        }

    }

}
