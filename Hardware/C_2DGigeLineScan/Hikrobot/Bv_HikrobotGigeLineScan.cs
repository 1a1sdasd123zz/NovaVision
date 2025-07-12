using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro.ImageProcessing;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware.Frame_Grabber_CameraLink_;

namespace NovaVision.Hardware.C_2DGigeLineScan.Hikrobot
{

    public class Bv_HikrobotGigeLineScan : Bv_Camera, IAcquisition2DLineScan3D, IDisposable
    {
        private string serialNum = "";

        private readonly string VENDORNAME = "Hikrobort2DLineGige";

        private readonly CameraCategory CATEGORY = CameraCategory.C_2DLineGige;

        private bool objectCreated = false;

        private bool stopGrabFlag = true;

        private bool acqOK = false;

        private Cognex.VisionPro.ICogImage cogImage;

        private int acqCount = 0;

        private List<Cognex.VisionPro.ICogImage> imageDatas = new List<Cognex.VisionPro.ICogImage>();

        private CogCopyRegionTool imageStitcher;

        private FrameGrabberConfigData configDatas;

        private Camera_MVS_GigeLineScan camera;

        public static List<string> SerialNumList;

        public static Dictionary<string, Bv_HikrobotGigeLineScan> CamreasDic;

        public long FrameNum = 0L;

        public override string VendorName => VENDORNAME;

        public override CameraCategory Category => CATEGORY;

        public override string SerialNum => serialNum;

        public override FrameGrabberConfigData ConfigDatas => configDatas;

        public bool ObjectCreated
        {
            get
            {
                return objectCreated;
            }
            set
            {
                objectCreated = value;
            }
        }

        public event HardwareErrorEventHandler errorOccured;

        public event Action<bool> UpdateStartStopStatus;

        static Bv_HikrobotGigeLineScan()
        {
            SerialNumList = new List<string>();
            CamreasDic = new Dictionary<string, Bv_HikrobotGigeLineScan>();
        }

        public Bv_HikrobotGigeLineScan(string SN, FrameGrabberConfigData paramValues)
        {
            serialNum = SN;
            camera = new Camera_MVS_GigeLineScan(serialNum);
            InitiallParams();
            if (paramValues != null && paramValues.CameraOrGrabberParams.Count == configDatas.CameraOrGrabberParams.Count)
            {
                SetParams(paramValues);
            }
        }

        public static void EnumCards()
        {
            Camera_MVS_GigeLineScan.EnumCameras(null);
            SerialNumList = Camera_MVS_GigeLineScan.SerialNums;
        }

        public override bool OpenDevice()
        {
            bool bRet = false;
            camera.ImageArrivedEvent += Camera_ImageArrived;
            camera.ExceptionEvent += Camera_ExceptionEvnet;
            configDatas.CameraParamChanged += Bv_HikrobotGigeLineScan_CameraParamChanged;
            bRet = camera.Open();
            if (bRet)
            {
                objectCreated = true;
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
                objectCreated = false;
                ErrorAck(1);
            }
            return bRet;
        }

        public override void CloseDevice()
        {
            camera.ImageArrivedEvent -= Camera_ImageArrived;
            camera.ExceptionEvent -= Camera_ExceptionEvnet;
            configDatas.CameraParamChanged -= Bv_HikrobotGigeLineScan_CameraParamChanged;
            camera.Close();
            objectCreated = false;
            if (CamreasDic.ContainsKey(serialNum))
            {
                CamreasDic.Remove(serialNum);
            }
            Dispose();
        }

        public bool Snap()
        {
            acqOK = false;
            int timeout = Convert.ToInt32(configDatas["Timeout"].Value.mValue);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            int encoderCounterStart = camera.EncoderCounter;
            camera.SoftwareTriggerDevice();
            Task.Run(delegate
            {
                while (true)
                {
                    timeSpan = DateTime.Now - now;
                    if (acqOK || timeSpan.TotalMilliseconds > (double)timeout)
                    {
                        break;
                    }
                    Thread.Sleep(3);
                }
                int encoderCounter = camera.EncoderCounter;
                LogUtil.Log($"Hikrobort2DLineGige({serialNum}),编码器起始值{encoderCounterStart}，编码器结束值{encoderCounter}，编码器接收脉冲数{encoderCounter - encoderCounterStart}");
                if (camera.StitchFlag == 1)
                {
                    camera.StopGrab();
                }
                camera.imageDatas.Clear();
                if (timeSpan.TotalMilliseconds > (double)timeout)
                {
                    ErrorAck(5);
                    LogUtil.LogError("Hikrobort2DLineGige(" + serialNum + ")采集时间超时！");
                }
            });
            return true;
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

        private void Camera_ExceptionEvnet(string message)
        {
            LogUtil.LogError("Hikrobort2DLineGige(" + serialNum + ")产生错误：" + message);
        }

        private void Camera_ImageArrived(object sender, ImageData image)
        {
            acqOK = true;
            FrameNum = camera.imageCount;
            if (UpdateImage != null)
            {
                UpdateImage(image);
            }
        }

        public override bool SetParams(FrameGrabberConfigData paramCollection)
        {
            bool bRet = false;
            try
            {
                for (int i = 0; i < configDatas.CameraOrGrabberParams.Count; i++)
                {
                    configDatas[i] = paramCollection[i];
                }
                bRet = true;
            }
            catch (Exception)
            {
                return bRet;
            }
            return bRet;
        }

        public void UpdateNowConfigData()
        {
            if (objectCreated)
            {
                int workMode = Convert.ToInt32(configDatas["WorkMode"].Value.mValue);
                SetWorkMode(workMode);
                double exp = Convert.ToDouble(configDatas["Exposure"].Value.mValue);
                SetCameraExposure(ref exp);
                string gain = Convert.ToString(configDatas["Gain"].Value.mValue);
                SetCameraGain(ref gain);
                int acqLineRate = Convert.ToInt32(configDatas["AcquisitionLineRate"].Value.mValue);
                SetAcquisitionLineRateAbs(ref acqLineRate);
                int lineDebouncerTime = Convert.ToInt32(configDatas["LineDebouncerTime"].Value.mValue);
                SetLineDebouncerTime(ref lineDebouncerTime);
                if (workMode == 4 || workMode == 5 || workMode == 6)
                {
                    int dropPre = Convert.ToInt32(configDatas["PreDivider"].Value.mValue);
                    SetShaftEncoderDropPre(ref dropPre);
                    int multiplier = Convert.ToInt32(configDatas["Multiplier"].Value.mValue);
                    SetShaftEncoderMultiplier(ref multiplier);
                    int dropPost = Convert.ToInt32(configDatas["PostDivider"].Value.mValue);
                    SetShaftEncoderDropPost(ref dropPost);
                    int semMode = Convert.ToInt32(configDatas["EncoderOutputMode"].Value.mValue);
                    SetEncoderOutputMode(ref semMode);
                    int count = Convert.ToInt32(configDatas["FrameCount"].Value.mValue);
                    SetAcquisitionFrameCount(ref count);
                    int ScanDirection = Convert.ToInt32(configDatas["ScanDirection"].Value.mValue);
                    SetScanDirection(ref ScanDirection);
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
                int flag = Convert.ToInt32(configDatas["StitchFlag"].Value.mValue);
                SetStitchFlag(ref flag);
                int imgcount = Convert.ToInt32(configDatas["ImgCount"].Value.mValue);
                SetImgCount(ref imgcount);
                GetCameraParam();
            }
        }

        public bool GetCameraParam()
        {
            bool bRet = false;
            try
            {
                configDatas["WorkMode"].Value.mValue = camera.WorkMode;
                configDatas["Width"].Value.mValue = camera.Width;
                configDatas["Height"].Value.mValue = camera.Height;
                configDatas["OffsetX"].Value.mValue = camera.OffsetX;
                configDatas["Exposure"].Value.mValue = camera.ExposureTime;
                configDatas["Gain"].Value.mValue = camera.GainRaw;
                configDatas["FrameCount"].Value.mValue = camera.AcquisitionBurstFrameCount;
                configDatas["AcquisitionLineRate"].Value.mValue = camera.AcquisitionLineRate;
                configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRate;
                configDatas["PreDivider"].Value.mValue = camera.PreDivider;
                configDatas["Multiplier"].Value.mValue = camera.Multiplier;
                configDatas["PostDivider"].Value.mValue = camera.PostDivider;
                configDatas["EncoderOutputMode"].Value.mValue = camera.EncoderOutputMode;
                configDatas["LineDebouncerTime"].Value.mValue = camera.LineDebouncerTimeNs;
                configDatas["IP"].Value.mValue = camera.IPAddress;
                configDatas["ScanDirection"].Value.mValue = camera.ScanDirection;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                    mValue = "HikRobot"
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
                Type = "String",
                Value = new XmlObject
                {
                    mValue = "gain_1200x"
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
                Name = "FrameCount",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1
                }
            };
            ParamElement acqLineRate = new ParamElement
            {
                Name = "AcquisitionLineRate",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 10000
                }
            };
            ParamElement resultLineRate = new ParamElement
            {
                Name = "ResultLineRate",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 10000
                }
            };
            ParamElement semMode = new ParamElement
            {
                Name = "EncoderOutputMode",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement dropPre = new ParamElement
            {
                Name = "PreDivider",
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
            ParamElement dropPost = new ParamElement
            {
                Name = "PostDivider",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1
                }
            };
            ParamElement lineDebouncerTime = new ParamElement
            {
                Name = "LineDebouncerTime",
                Type = "Int32",
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
            ParamElement StitchFlag = new ParamElement
            {
                Name = "StitchFlag",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement ImgCount = new ParamElement
            {
                Name = "ImgCount",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement ScanDirection = new ParamElement
            {
                Name = "ScanDirection",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            configDatas.CameraOrGrabberParams.AddRange(new ParamElement[23]
            {
            serial, vendorName, category, cameraVendorName, workMode, cameraExposureTime, cameraGain, timeout, offsetX, width,
            height, frameCount, acqLineRate, resultLineRate, semMode, dropPre, multiplier, dropPost, lineDebouncerTime, ip,
            StitchFlag, ImgCount, ScanDirection
            });
        }

        [Setting("Exposure")]
        public void SetCameraExposure(ref double exposure)
        {
            camera.ExposureTime = exposure;
            configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRate;
        }

        [Setting("Gain")]
        public void SetCameraGain(ref string gain)
        {
            camera.GainRaw = gain;
        }

        [Setting("LineDebouncerTime")]
        public void SetLineDebouncerTime(ref int lineDebouncerTime)
        {
            camera.LineDebouncerTimeNs = lineDebouncerTime;
        }

        [Setting("WorkMode")]
        public override void SetWorkMode(int workMode)
        {
            camera.WorkMode = workMode;
        }

        [Setting("PreDivider")]
        public void SetShaftEncoderDropPre(ref int dropPre)
        {
            camera.PreDivider = dropPre;
        }

        [Setting("Multiplier")]
        public void SetShaftEncoderMultiplier(ref int multiplier)
        {
            camera.Multiplier = multiplier;
        }

        [Setting("PostDivider")]
        public void SetShaftEncoderDropPost(ref int dropPost)
        {
            camera.PostDivider = dropPost;
        }

        [Setting("OffsetX")]
        public void SetOffsetX(ref int offsetX)
        {
            camera.OffsetX = offsetX;
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

        [Setting("AcquisitionLineRate")]
        public void SetAcquisitionLineRateAbs(ref int lineRate)
        {
            camera.AcquisitionLineRate = lineRate;
            configDatas["ResultLineRate"].Value.mValue = camera.ResultingLineRate;
        }

        [Setting("EncoderOutputMode")]
        public void SetEncoderOutputMode(ref int mode)
        {
            camera.EncoderOutputMode = mode;
        }

        [Setting("FrameCount")]
        public void SetAcquisitionFrameCount(ref int count)
        {
            camera.AcquisitionBurstFrameCount = count;
        }

        [Setting("StitchFlag")]
        public void SetStitchFlag(ref int flag)
        {
            camera.StitchFlag = flag;
        }

        [Setting("ImgCount")]
        public void SetImgCount(ref int count)
        {
            camera.ImgCount = count;
        }

        [Setting("ScanDirection")]
        public void SetScanDirection(ref int ScanDirection)
        {
            camera.ScanDirection = ScanDirection;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void ErrorAck(byte errorCode)
        {
            if (this.errorOccured != null)
            {
                this.errorOccured(this, new HardwareErrorEventArgs(errorCode));
            }
        }

        private void Bv_HikrobotGigeLineScan_CameraParamChanged(object sender, CameraParamChangeArgs e)
        {
            bool @return = this.ExecuteMethod(e);
        }
    }
}
