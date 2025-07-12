using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware.Frame_Grabber;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_._03_IKap
{
    public class Bv_Vulcan : Bv_Camera, IAcquisition2DLineScan3D, IDisposable
    {
        private bool objectCreated = false;

        private bool stopGrabFlag = true;

        private string m_SerialNum = "";

        private string _portName = "";

        private string m_ConfigFileName = "M_DEFAULT";

        private readonly string _vendorName = "IKap";

        private readonly CameraCategory CATEGORY = CameraCategory.C_2DLineCL;

        private FrameGrabberConfigData _configDatas;

        private ItekVulcan itekVulcan;

        private Bv_Camera camera;

        private CameraSerialPort serialPort;

        private Cognex.VisionPro.ICogImage m_OutputImage;

        private bool acqOK = false;

        private int acqCount = 0;

        public static List<string> vulcanSerialList;

        public static Dictionary<string, Bv_Vulcan> dic_Cards;

        public override string VendorName => _vendorName;

        public override CameraCategory Category => CATEGORY;

        public override string SerialNum => m_SerialNum;

        public override FrameGrabberConfigData ConfigDatas => _configDatas;

        public bool ObjectCreated => objectCreated;

        public bool StopGrabFlag => stopGrabFlag;

        public event HardwareErrorEventHandler errorOccured;

        public event Action<bool> UpdateStartStopStatus;

        static Bv_Vulcan()
        {
            vulcanSerialList = new List<string>();
            dic_Cards = new Dictionary<string, Bv_Vulcan>();
        }

        public Bv_Vulcan(string SN, FrameGrabberConfigData paramValues)
        {
            m_SerialNum = SN;
            itekVulcan = new ItekVulcan(m_SerialNum);
            InitiallParams();
            if (paramValues != null && paramValues.CameraOrGrabberParams.Count == _configDatas.CameraOrGrabberParams.Count)
            {
                _configDatas = paramValues;
                string configPath = _configDatas.CameraOrGrabberParams[10].Value.ToString();
                LoadConfig(configPath);
                UpdateNowConfigData();
            }
            _configDatas.CameraParamChanged += Vulcan_CL_CameraParamChanged;
        }

        public static void EnumCards()
        {
            ItekVulcan.EnumIkapBoards(null);
            vulcanSerialList = ItekVulcan.boardNames;
        }

        public override bool OpenDevice()
        {
            bool b_ret = false;
            itekVulcan.ImageArrivd += Bv_Vulcan_ImageArrivd;
            b_ret = itekVulcan.OpenCard(m_ConfigFileName, UpdateNowConfigData, null);
            if (b_ret)
            {
                objectCreated = true;
                _configDatas["PortName"].Value.mValue = itekVulcan.PortName;
                _configDatas["ModelName"].Value.mValue = m_SerialNum;
                if (!dic_Cards.ContainsKey(m_SerialNum))
                {
                    dic_Cards.Add(m_SerialNum, this);
                }
                else
                {
                    dic_Cards[m_SerialNum] = this;
                }
                b_ret = OpenCamera();
                if (!b_ret)
                {
                    ErrorAck(8);
                }
                camera?.SetWorkMode(Convert.ToInt32(_configDatas["WorkMode"].Value.mValue));
            }
            else
            {
                objectCreated = false;
                ErrorAck(1);
            }
            return b_ret;
        }

        public bool OpenCamera()
        {
            if (camera != null)
            {
                camera.CloseDevice();
            }
            if (serialPort == null)
            {
                serialPort = new CameraSerialPort(_configDatas["PortName"].Value.ToString(), 9600);
            }
            if (!serialPort.IsOpen())
            {
            }
            camera = CLCameraFactory.CreateCLCamera(_configDatas, serialPort, Convert.ToUInt32(m_SerialNum.Substring(m_SerialNum.Length - 1)));
            return camera.OpenDevice();
        }

        public override void CloseDevice()
        {
            if (camera != null)
            {
                camera.CloseDevice();
            }
            itekVulcan.ImageArrivd -= Bv_Vulcan_ImageArrivd;
            itekVulcan.CloseCard(null);
            _configDatas.CameraParamChanged -= Vulcan_CL_CameraParamChanged;
            if (dic_Cards.ContainsKey(m_SerialNum))
            {
                dic_Cards.Remove(m_SerialNum);
            }
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void Bv_Vulcan_ImageArrivd(object sender, IKapImageData image)
        {
            acqOK = true;
            GCHandle handle = GCHandle.Alloc(image.Data, GCHandleType.Pinned);
            IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
            if (image.ImageType == 3)
            {
                m_OutputImage = ImageData.GetRGBImage(image.Height, image.Width, buffer);
            }
            else if (image.ImageType == 1)
            {
                m_OutputImage = ImageData.GetMonoImage(image.Height, image.Width, buffer);
            }
            if (UpdateImage != null)
            {
                UpdateImage(new ImageData(m_OutputImage));
            }
            handle.Free();
            if (_configDatas.CameraOrGrabberParams[9].Value.ToString().Equals("6") && ++acqCount == Convert.ToInt32(_configDatas.CameraOrGrabberParams[16].Value.mValue))
            {
                StopGrab();
            }
        }

        public bool Snap()
        {
            acqOK = false;
            int timeout = Convert.ToInt32(_configDatas["Timeout"].Value.mValue);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            itekVulcan.Snap(null);
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
                if (itekVulcan.IsStartGrab)
                {
                    itekVulcan.StopGrab(null);
                }
                if (timeSpan.TotalMilliseconds > (double)timeout)
                {
                    ErrorAck(5);
                }
            });
            return true;
        }

        public bool StartGrab()
        {
            itekVulcan.StartGrab(null);
            stopGrabFlag = !itekVulcan.IsStartGrab;
            acqCount = 0;
            if (this.UpdateStartStopStatus != null)
            {
                this.UpdateStartStopStatus(stopGrabFlag);
            }
            return true;
        }

        public bool StopGrab()
        {
            itekVulcan.StopGrab(null);
            stopGrabFlag = !itekVulcan.IsStartGrab;
            if (this.UpdateStartStopStatus != null)
            {
                this.UpdateStartStopStatus(stopGrabFlag);
            }
            return true;
        }

        public override bool SetParams(FrameGrabberConfigData paramCollection)
        {
            bool b_ret = false;
            try
            {
                for (int i = 0; i < _configDatas.CameraOrGrabberParams.Count; i++)
                {
                    _configDatas[i] = paramCollection[i];
                }
                b_ret = true;
            }
            catch (Exception)
            {
                return b_ret;
            }
            return b_ret;
        }

        [Setting("BufferFrameCount")]
        public void SetBufferFrameCount(int bufferFrameCount)
        {
            itekVulcan.SetBufferFrameCount(bufferFrameCount, null);
        }

        [Setting("WorkMode")]
        public override void SetWorkMode(int workMode)
        {
            itekVulcan.SetWorkMode(workMode, null);
            camera?.SetWorkMode(workMode);
        }

        [Setting("TapNum")]
        public void SetTapNum(int tapNum)
        {
            itekVulcan.SetTapNum(ref tapNum, null);
        }

        [Setting("ExposureTime")]
        public void SetCameraExposure(ref int exposure)
        {
            if (camera != null)
            {
                camera.ConfigDatas.SetElementValue("ExposureTime", exposure);
            }
        }

        [Setting("Gain")]
        public void SetCameraGain(ref float gain)
        {
            if (camera != null)
            {
                camera.ConfigDatas.SetElementValue("Gain", gain);
            }
        }

        [Setting("ScanDirection")]
        public void SetCameraScanDirection(ref int direction)
        {
            if (camera != null)
            {
                camera.ConfigDatas.SetElementValue("ScanDirection", direction);
            }
        }

        [Setting("Divider")]
        public void SetShaftEncoderDrop(ref int drop)
        {
            itekVulcan.SetDivider(ref drop, null);
        }

        [Setting("Multiplier")]
        public void SetShaftEncoderMultiplier(ref int multiplier)
        {
            itekVulcan.SetMultiplier(ref multiplier, null);
        }

        [Setting("FrameCount")]
        public void SetFrameCount(int frameCount)
        {
            _configDatas["FrameCount"].Value.mValue = frameCount;
        }

        protected override void InitiallParams()
        {
            _configDatas = new FrameGrabberConfigData();
            ParamElement serial = new ParamElement
            {
                Name = "Serial",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = m_SerialNum
                }
            };
            ParamElement vendorName = new ParamElement
            {
                Name = "VendorName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = _vendorName
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
            ParamElement modelName = new ParamElement
            {
                Name = "ModelName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = "Model-Name"
                }
            };
            ParamElement portName = new ParamElement
            {
                Name = "PortName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = "COM13"
                }
            };
            ParamElement drop = new ParamElement
            {
                Name = "Divider",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
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
            ParamElement tapNum = new ParamElement
            {
                Name = "TapNum",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 3
                }
            };
            ParamElement bufferFrameCount = new ParamElement
            {
                Name = "BufferFrameCount",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 5
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
            ParamElement configPath = new ParamElement
            {
                Name = "ConfigPath",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = m_ConfigFileName
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
            ParamElement cameraVendorName = new ParamElement
            {
                Name = "CameraVendorName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = ""
                }
            };
            ParamElement cameraExposureTime = new ParamElement
            {
                Name = "ExposureTime",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 50
                }
            };
            ParamElement cameraGain = new ParamElement
            {
                Name = "Gain",
                Type = "Single",
                Value = new XmlObject
                {
                    mValue = 1f
                }
            };
            ParamElement cameraScanDirection = new ParamElement
            {
                Name = "ScanDirection",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
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
            _configDatas.CameraOrGrabberParams.AddRange(new ParamElement[17]
            {
            serial, vendorName, category, modelName, portName, drop, multiplier, tapNum, bufferFrameCount, workMode,
            configPath, timeout, cameraVendorName, cameraExposureTime, cameraGain, cameraScanDirection, frameCount
            });
        }

        public void LoadConfig(string configPath)
        {
            if (File.Exists(configPath) && configPath.EndsWith(".vlcf"))
            {
                string m_ConfigFileNameSave = m_ConfigFileName;
                if (string.Compare(m_ConfigFileName, configPath, StringComparison.Ordinal) == 0)
                {
                    return;
                }
                m_ConfigFileName = configPath;
                if (itekVulcan.IsCreated)
                {
                    itekVulcan.CloseCard(null);
                    if (!itekVulcan.OpenCard(m_ConfigFileName, UpdateNowConfigData, null))
                    {
                        m_ConfigFileName = m_ConfigFileNameSave;
                        itekVulcan.OpenCard(m_ConfigFileName, UpdateNowConfigData, null);
                    }
                }
                _configDatas["ConfigPath"].Value.mValue = m_ConfigFileName;
            }
            else
            {
                ErrorAck(3);
            }
        }

        public bool GetCameraParam()
        {
            bool b_ret = false;
            try
            {
                _configDatas["Divider"].Value.mValue = itekVulcan.GetDivider(null);
                _configDatas["Multiplier"].Value.mValue = itekVulcan.GetMultiplierIndex(null);
                _configDatas["WorkMode"].Value.mValue = itekVulcan.GetWorkMode(null);
                _configDatas["TapNum"].Value.mValue = itekVulcan.GetTapNum(null);
                _configDatas["BufferFrameCount"].Value.mValue = itekVulcan.GetBufferFrameCount(null);
                _configDatas["ExposureTime"].Value.mValue = Convert.ToInt32(camera.ConfigDatas["ExposureTime"].Value);
                _configDatas["Gain"].Value = camera.ConfigDatas["Gain"].Value;
                _configDatas["ScanDirection"].Value = camera.ConfigDatas["ScanDirection"].Value;
                b_ret = true;
            }
            catch (Exception)
            {
                return b_ret;
            }
            return b_ret;
        }

        public void UpdateNowConfigData()
        {
            if (itekVulcan.IsCreated)
            {
                SetTapNum(Convert.ToInt32(_configDatas["TapNum"].Value.mValue));
                byte workMode = Convert.ToByte(_configDatas["WorkMode"].Value.mValue);
                SetWorkMode(workMode);
                if (workMode == 4 || workMode == 5 || workMode == 6)
                {
                    int drop = Convert.ToInt32(_configDatas["Divider"].Value.mValue);
                    SetShaftEncoderDrop(ref drop);
                    int multiplier = Convert.ToInt32(_configDatas["Multiplier"].Value.mValue);
                    SetShaftEncoderMultiplier(ref multiplier);
                }
            }
        }

        private void ErrorAck(byte errorCode)
        {
            if (this.errorOccured != null)
            {
                this.errorOccured(this, new HardwareErrorEventArgs(errorCode));
            }
        }

        private void Vulcan_CL_CameraParamChanged(object sender, CameraParamChangeArgs e)
        {
            bool @return = this.ExecuteMethod(e);
        }
    }
}
