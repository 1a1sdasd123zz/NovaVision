using System;

namespace NovaVision.Hardware
{
    public class CameraLine2DBase : CameraBase
    {
        public string CamCategory = CameraBase.CameraType["2D_LineScan"];

        public TriggerMode2DLinear triggerSelectorMode;

        public string DeviceIP;

        public string VendorName;

        public string ModelName;

        public string Version;

        public string cameraSN;

        protected float exposure;

        protected float gain;

        protected long scanWidth;

        protected long scanHeight;

        protected long offsetX;

        protected long timerDuration;

        protected long acqLineRate;

        protected long tapNum;

        protected long linePeriod;

        protected int rotaryDirection;

        protected int scanDirection;

        public bool acqOk;

        public bool bStopFlag = false;

        public double timeout = 5000.0;

        //public Action<ImageData> UpdateImage;

        public virtual float Exposure
        {
            get
            {
                return exposure;
            }
            set
            {
                exposure = value;
            }
        }

        public virtual float Gain
        {
            get
            {
                return gain;
            }
            set
            {
                gain = value;
            }
        }

        public virtual long ScanWidth
        {
            get
            {
                return scanWidth;
            }
            set
            {
                scanWidth = value;
            }
        }

        public virtual long ScanHeight
        {
            get
            {
                return scanHeight;
            }
            set
            {
                scanHeight = value;
            }
        }

        public virtual long OffsetX
        {
            get
            {
                return offsetX;
            }
            set
            {
                offsetX = value;
            }
        }

        public virtual long TimerDuration
        {
            get
            {
                return timerDuration;
            }
            set
            {
                timerDuration = value;
            }
        }

        public virtual long AcqLineRate
        {
            get
            {
                return acqLineRate;
            }
            set
            {
                acqLineRate = value;
            }
        }

        public virtual long TapNum
        {
            get
            {
                return tapNum;
            }
            set
            {
                tapNum = value;
            }
        }

        public virtual long LinePeriod
        {
            get
            {
                return linePeriod;
            }
            set
            {
                linePeriod = value;
            }
        }

        public virtual int RotaryDirection
        {
            get
            {
                return rotaryDirection;
            }
            set
            {
                rotaryDirection = value;
            }
        }

        public virtual int ScanDirection
        {
            get
            {
                return scanDirection;
            }
            set
            {
                scanDirection = value;
            }
        }

        public virtual void SetTriggerSelector(TriggerMode2DLinear triggerMode)
        {
        }

        public virtual void SetRotaryDirection(TriggerMode2DLinear triggerSelector, int index)
        {
        }

        public virtual void SetScanDirection(int scanDirectionIndex)
        {
        }

        public virtual int SoftwareTriggerOnce()
        {
            return -1;
        }

        //public virtual int StartGrab(bool state)
        //{
        //    return -1;
        //}

        //public virtual int StopGrab(bool state)
        //{
        //    return -1;
        //}

        public virtual void DestroyObjects()
        {
        }

        public virtual int OpenCamera()
        {
            return -1;
        }

        public void SetTimeOut(double _timeout)
        {
            if (_timeout > 0.0)
            {
                timeout = _timeout;
            }
            SettingParams.Timeout = timeout;
        }

        public override void SetCameraSetting(CameraConfigData configData)
        {
            if (configData.SettingParams != null)
            {
                if (SettingParams.ExposureTime != configData.SettingParams.ExposureTime)
                {
                    Exposure = configData.SettingParams.ExposureTime;
                }
                if (SettingParams.Gain != configData.SettingParams.Gain)
                {
                    Gain = configData.SettingParams.Gain;
                }
                if (SettingParams.OffsetX != configData.SettingParams.OffsetX)
                {
                    OffsetX = configData.SettingParams.OffsetX;
                }
                if (SettingParams.ScanWidth != configData.SettingParams.ScanWidth)
                {
                    ScanWidth = configData.SettingParams.ScanWidth;
                }
                if (SettingParams.ScanHeight != configData.SettingParams.ScanHeight)
                {
                    ScanHeight = configData.SettingParams.ScanHeight;
                }
                if (SettingParams.TimerDuration != configData.SettingParams.TimerDuration)
                {
                    TimerDuration = configData.SettingParams.TimerDuration;
                }
                if (SettingParams.AcqLineRate != configData.SettingParams.AcqLineRate)
                {
                    AcqLineRate = configData.SettingParams.AcqLineRate;
                }
                if (SettingParams.TapNum != configData.SettingParams.TapNum)
                {
                    TapNum = configData.SettingParams.TapNum;
                }
                if (SettingParams.LinePeriod != configData.SettingParams.LinePeriod)
                {
                    LinePeriod = configData.SettingParams.LinePeriod;
                }
                if (SettingParams.TriggerMode != configData.SettingParams.TriggerMode)
                {
                    SetTriggerSelector((TriggerMode2DLinear)configData.SettingParams.TriggerMode);
                }
                if (SettingParams.RotaryDirection != configData.SettingParams.RotaryDirection)
                {
                    SetRotaryDirection((TriggerMode2DLinear)configData.SettingParams.TriggerMode, configData.SettingParams.RotaryDirection);
                }
                if (SettingParams.ScanDirection != configData.SettingParams.ScanDirection)
                {
                    SetScanDirection(configData.SettingParams.ScanDirection);
                }
                if (SettingParams.Timeout != configData.SettingParams.Timeout)
                {
                    SetTimeOut(configData.SettingParams.Timeout);
                }
            }
        }
    }
}
