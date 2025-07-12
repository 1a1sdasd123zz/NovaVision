using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware.Frame_Grabber_CameraLink_;

namespace NovaVision.Hardware.C_2DGigeLineScan.iRAYPLE
{
    public class Bv_DaHuaGigeLineScan : Bv_Camera, IAcquisition2DLineScan3D, IDisposable
    {
        private string serialNum = "";

        private readonly string VENDORNAME = "Dahua2DLineGige";

        private readonly CameraCategory CATEGORY = CameraCategory.C_2DLineGige;

        private bool cameraOpened = false;

        private bool stopGrabFlag = true;

        private bool acqOK = false;

        private Cognex.VisionPro.ICogImage cogImage;

        private int acqCount = 0;

        private FrameGrabberConfigData configDatas;

        private DaHua_GigeLineScan camera;

        public static List<string> SerialNumList;

        public static Dictionary<string, Bv_DaHuaGigeLineScan> CamreasDic;

        public long FrameNum = 0L;

        public override string VendorName => VENDORNAME;

        public override CameraCategory Category => CATEGORY;

        public override string SerialNum => serialNum;

        public override FrameGrabberConfigData ConfigDatas => configDatas;

        public bool ObjectCreated
        {
            get
            {
                return cameraOpened;
            }
            set
            {
                cameraOpened = value;
            }
        }

        public event HardwareErrorEventHandler errorOccured;

        public event Action<bool> UpdateStartStopStatus;

        static Bv_DaHuaGigeLineScan()
        {
            SerialNumList = new List<string>();
            CamreasDic = new Dictionary<string, Bv_DaHuaGigeLineScan>();
        }

        public Bv_DaHuaGigeLineScan(string SN, FrameGrabberConfigData paramValues)
        {
            serialNum = SN;
            camera = new DaHua_GigeLineScan(serialNum);
            InitiallParams();
            if (paramValues != null && paramValues.CameraOrGrabberParams.Count == configDatas.CameraOrGrabberParams.Count)
            {
                SetParams(paramValues);
            }
        }

        public static void EnumDevices()
        {
            DaHua_GigeLineScan.EnumDevices(null);
            SerialNumList = DaHua_GigeLineScan.SerialNums;
        }

        public override bool OpenDevice()
        {
            bool bRet = false;
            if (cameraOpened)
            {
                return true;
            }
            camera.ImageArrivedEvent += Camera_ImageArrived;
            camera.LogPushEvent += Camera_ExceptionEvnet;
            configDatas.CameraParamChanged += Bv_DahuaGigeLineScan_CameraParamChanged;
            bRet = camera.Open();
            if (bRet)
            {
                cameraOpened = true;
                UpdateNowConfigData();
                GetCameraParam();
                if (!CamreasDic.ContainsKey(serialNum))
                {
                    CamreasDic.Add(serialNum, this);
                }
                else
                {
                    CamreasDic[serialNum] = this;
                }
            }
            else
            {
                bRet = false;
                cameraOpened = false;
                ErrorAck(1);
            }
            return bRet;
        }

        public override void CloseDevice()
        {
            camera.ImageArrivedEvent -= Camera_ImageArrived;
            camera.LogPushEvent -= Camera_ExceptionEvnet;
            configDatas.CameraParamChanged -= Bv_DahuaGigeLineScan_CameraParamChanged;
            camera.Close();
            cameraOpened = false;
            if (CamreasDic.ContainsKey(serialNum))
            {
                CamreasDic.Remove(serialNum);
            }
            Dispose();
        }

        public override bool SetParams(FrameGrabberConfigData paramCollection)
        {
            try
            {
                for (int i = 0; i < configDatas.CameraOrGrabberParams.Count; i++)
                {
                    configDatas[i] = paramCollection[i];
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override void SetWorkMode(int workMode)
        {
            camera.WorkMode = workMode;
        }

        protected override void InitiallParams()
        {
            configDatas = new FrameGrabberConfigData();
            ParamElement serial = new ParamElement
            {
                Name = "Serial",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = serialNum
                }
            };
            ParamElement vendorName = new ParamElement
            {
                Name = "VendorName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = VENDORNAME
                }
            };
            ParamElement category = new ParamElement
            {
                Name = "Category",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = CATEGORY.ToString()
                }
            };
            ParamElement cameraVendorName = new ParamElement
            {
                Name = "CameraVendorName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = "Dahua"
                }
            };
            ParamElement workMode = new ParamElement
            {
                Name = "WorkMode",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 2
                }
            };
            ParamElement cameraExposureTime = new ParamElement
            {
                Name = "Exposure",
                Type = "Double",
                Value = new XmlObject
                {
                    mValue = 32.0
                }
            };
            ParamElement cameraGain = new ParamElement
            {
                Name = "Gain",
                Type = "Double",
                Value = new XmlObject
                {
                    mValue = "1.0"
                }
            };
            ParamElement timeout = new ParamElement
            {
                Name = "Timeout",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 5000
                }
            };
            ParamElement offsetX = new ParamElement
            {
                Name = "OffsetX",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement width = new ParamElement
            {
                Name = "Width",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1024
                }
            };
            ParamElement height = new ParamElement
            {
                Name = "Height",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 500
                }
            };
            ParamElement frameCount = new ParamElement
            {
                Name = "TriggerFrameCount",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1
                }
            };
            ParamElement acqLineRate = new ParamElement
            {
                Name = "AcquisitionLineRate",
                Type = "Double",
                Value = new XmlObject
                {
                    mValue = 10000
                }
            };
            ParamElement acqLineRateEnable = new ParamElement
            {
                Name = "AcquisitionLineRateEnable",
                Type = "Boolean",
                Value = new XmlObject
                {
                    mValue = false
                }
            };
            ParamElement resultLineRate = new ParamElement
            {
                Name = "ResultLineRate",
                Type = "Double",
                Value = new XmlObject
                {
                    mValue = 10000
                }
            };
            ParamElement recMode = new ParamElement
            {
                Name = "RotaryEncoderMode",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement divider = new ParamElement
            {
                Name = "Divider",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1
                }
            };
            ParamElement multiplier = new ParamElement
            {
                Name = "Multiplier",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1
                }
            };
            ParamElement lineDebouncerTime = new ParamElement
            {
                Name = "LineDebouncerTime",
                Type = "Double",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement ip = new ParamElement
            {
                Name = "IP",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = ""
                }
            };
            ParamElement reverseScanDirection = new ParamElement
            {
                Name = "ReverseScanDirection",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = "Off"
                }
            };
            configDatas.CameraOrGrabberParams.AddRange(new ParamElement[21]
            {
            serial, vendorName, category, cameraVendorName, workMode, cameraExposureTime, cameraGain, timeout, offsetX, width,
            height, frameCount, acqLineRate, acqLineRateEnable, resultLineRate, recMode, divider, multiplier, lineDebouncerTime, ip,
            reverseScanDirection
            });
        }

        public void UpdateNowConfigData()
        {
            if (cameraOpened)
            {
                byte workMode = Convert.ToByte(configDatas["WorkMode"].Value.mValue);
                SetWorkMode(workMode);
                double exp = Convert.ToDouble(configDatas["Exposure"].Value.mValue);
                SetCameraExposure(ref exp);
                double gain = Convert.ToDouble(configDatas["Gain"].Value.mValue);
                SetCameraGain(ref gain);
                double acqLineRate = Convert.ToDouble(configDatas["AcquisitionLineRate"].Value.mValue);
                SetAcquisitionLineRateAbs(ref acqLineRate);
                bool acqLineRateEnable = Convert.ToBoolean(configDatas["AcquisitionLineRateEnable"].Value.mValue);
                SetAcquisitionLineRateEnable(ref acqLineRateEnable);
                double lineDebouncerTime = Convert.ToDouble(configDatas["LineDebouncerTime"].Value.mValue);
                SetLineDebouncerTime(ref lineDebouncerTime);
                string reverseScanDirection = Convert.ToString(configDatas["ReverseScanDirection"].Value.mValue);
                SetReverseScanDirection(ref reverseScanDirection);
                if (workMode == 4 || workMode == 5 || workMode == 6)
                {
                    int divider = Convert.ToInt32(configDatas["Divider"].Value.mValue);
                    SetDivider(ref divider);
                    int multiplier = Convert.ToInt32(configDatas["Multiplier"].Value.mValue);
                    SetMultiplier(ref multiplier);
                    int recMode = Convert.ToInt32(configDatas["RotaryEncoderMode"].Value.mValue);
                    SetRotaryEncoderCounterMode(ref recMode);
                }
                if (workMode != 1)
                {
                    int offsetX = Convert.ToInt32(configDatas["OffsetX"].Value.mValue);
                    SetOffsetX(ref offsetX);
                    int width = Convert.ToInt32(configDatas["Width"].Value.mValue);
                    SetWidth(ref width);
                    int height = Convert.ToInt32(configDatas["Height"].Value.mValue);
                    SetHeight(ref height);
                }
                if (workMode == 6)
                {
                    int count = Convert.ToInt32(configDatas["TriggerFrameCount"].Value.mValue);
                    SetTriggerFrameCount(ref count);
                }
                GetCameraParam();
            }
        }

        public bool GetCameraParam()
        {
            try
            {
                configDatas["WorkMode"].Value.mValue = camera.WorkMode;
                configDatas["Width"].Value.mValue = camera.Width;
                configDatas["Height"].Value.mValue = camera.Height;
                configDatas["OffsetX"].Value.mValue = camera.OffsetX;
                configDatas["Exposure"].Value.mValue = camera.ExposureTime;
                configDatas["Gain"].Value.mValue = camera.GainRaw;
                configDatas["TriggerFrameCount"].Value.mValue = camera.TriggerFrameCount;
                configDatas["AcquisitionLineRate"].Value.mValue = camera.AcquisitionLineRate;
                configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRateAbs;
                configDatas["AcquisitionLineRateEnable"].Value.mValue = camera.AcquisitionLineRateEnable;
                configDatas["Divider"].Value.mValue = camera.Divider;
                configDatas["Multiplier"].Value.mValue = camera.Multiplier;
                configDatas["RotaryEncoderMode"].Value.mValue = camera.RotaryEncoderMode;
                configDatas["LineDebouncerTime"].Value.mValue = camera.LineDebouncingPeriod;
                configDatas["IP"].Value.mValue = camera.IPAddress;
                configDatas["ReverseScanDirection"].Value.mValue = camera.ReverseScanDirection;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [Setting("Width")]
        public void SetWidth(ref int width)
        {
            camera.Width = width;
        }

        [Setting("Height")]
        public void SetHeight(ref int height)
        {
            camera.Height = height;
        }

        [Setting("OffsetX")]
        public void SetOffsetX(ref int offX)
        {
            camera.OffsetX = offX;
        }

        [Setting("Exposure")]
        public void SetCameraExposure(ref double exposure)
        {
            camera.ExposureTime = exposure;
            configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRateAbs;
        }

        [Setting("Gain")]
        public void SetCameraGain(ref double gain)
        {
            camera.GainRaw = gain;
        }

        [Setting("LineDebouncerTime")]
        public void SetLineDebouncerTime(ref double lineDebouncerTime)
        {
            camera.LineDebouncingPeriod = lineDebouncerTime;
        }

        [Setting("ReverseScanDirection")]
        public void SetReverseScanDirection(ref string reverseScanDirection)
        {
            camera.ReverseScanDirection = reverseScanDirection;
        }

        [Setting("Divider")]
        public void SetDivider(ref int divider)
        {
            camera.Divider = divider;
        }

        [Setting("Multiplier")]
        public void SetMultiplier(ref int multiplier)
        {
            camera.Multiplier = multiplier;
        }

        [Setting("AcquisitionLineRate")]
        public void SetAcquisitionLineRateAbs(ref double lineRate)
        {
            camera.AcquisitionLineRate = lineRate;
            configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRateAbs;
        }

        [Setting("AcquisitionLineRateEnable")]
        public void SetAcquisitionLineRateEnable(ref bool lineRateEnable)
        {
            camera.AcquisitionLineRateEnable = lineRateEnable;
            configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRateAbs;
        }

        [Setting("TriggerFrameCount")]
        public void SetTriggerFrameCount(ref int count)
        {
            camera.TriggerFrameCount = count;
        }

        [Setting("RotaryEncoderMode")]
        public void SetRotaryEncoderCounterMode(ref int mode)
        {
            camera.RotaryEncoderMode = mode;
        }

        [Setting("WorkMode")]
        public void SetWorkMode(ref int mode)
        {
            camera.WorkMode = mode;
        }

        public bool StartGrab()
        {
            camera.StartGrab();
            stopGrabFlag = false;
            acqCount = 0;
            if (this.UpdateStartStopStatus != null)
            {
                this.UpdateStartStopStatus(stopGrabFlag);
            }
            return true;
        }

        public bool StopGrab()
        {
            camera.StopGrab();
            stopGrabFlag = true;
            if (this.UpdateStartStopStatus != null)
            {
                this.UpdateStartStopStatus(stopGrabFlag);
            }
            return true;
        }

        public bool Snap()
        {
            if (acqCount == 0)
            {
                LogUtil.Log("Dahua2DLineGige(" + serialNum + ")单帧采集！");
                acqCount++;
                acqOK = false;
                int timeout = Convert.ToInt32(configDatas["Timeout"].Value.mValue);
                DateTime startTime = DateTime.Now;
                TimeSpan timeSpan = default(TimeSpan);
                try
                {
                    camera.SoftwareTriggerDevice();
                }
                catch (Exception)
                {
                }
                Task.Run(delegate
                {
                    while (true)
                    {
                        timeSpan = DateTime.Now - startTime;
                        if (acqOK || timeSpan.TotalMilliseconds > (double)timeout)
                        {
                            break;
                        }
                        Thread.Sleep(3);
                    }
                    if (timeSpan.TotalMilliseconds > (double)timeout)
                    {
                        acqCount = 0;
                        ErrorAck(5);
                        LogUtil.LogError("Dahua2DLineGige(" + serialNum + ")采集时间超时！");
                    }
                });
            }
            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void Camera_ImageArrived(object sender, DaHua_GigeLineScan.ImageData image)
        {
            try
            {
                acqOK = true;
                FrameNum = image.ImageCount;
                if (image.ImageType == 3)
                {
                    cogImage = new CogImage24PlanarColor(image.Bitmap);
                }
                else if (image.ImageType == 1)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
                    cogImage = ImageData.GetMonoImage(image.Height, image.Width, buffer);
                }
                if (UpdateImage != null)
                {
                    UpdateImage(new ImageData(cogImage));
                }
                acqCount = 0;
            }
            catch (Exception ex)
            {
                LogUtil.LogError("Dahua2DLineGige(" + serialNum + ")图像回调出错：" + ex.Message);
            }
        }

        private void ErrorAck(byte errorCode)
        {
            if (this.errorOccured != null)
            {
                this.errorOccured(this, new HardwareErrorEventArgs(errorCode));
            }
        }

        private void Camera_ExceptionEvnet(string message)
        {
            LogUtil.LogError("Dahua2DLineGige(" + serialNum + ")产生错误：" + message);
        }

        private void Bv_DahuaGigeLineScan_CameraParamChanged(object sender, CameraParamChangeArgs e)
        {
            bool @return = this.ExecuteMethod(e);
        }
    }
}
