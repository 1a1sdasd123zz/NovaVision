using System;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware.Frame_Grabber;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_.CameraLinkCamera
{
    public class Hikrobot_CL : Bv_Camera, IDisposable
    {
        private bool _disposed;

        private CameraSerialPort _comPort;

        private FrameGrabberConfigData _configDatas;

        private readonly string _vendorName = "Hikrobt_CL";

        private readonly CameraCategory CATEGORY = CameraCategory.C_2DLineCL;

        public override string VendorName => _vendorName;

        public override CameraCategory Category => CATEGORY;

        public override string SerialNum => _configDatas.CameraOrGrabberParams[0].Value.ToString();

        public override FrameGrabberConfigData ConfigDatas => _configDatas;

        public Hikrobot_CL(CameraSerialPort comPort, FrameGrabberConfigData paramValues)
        {
            _comPort = comPort;
            InitiallParams();
            if (paramValues != null && paramValues.CameraOrGrabberParams.Count == _configDatas.CameraOrGrabberParams.Count)
            {
                _configDatas = paramValues;
            }
            UpdateNowConfigData();
            _configDatas.CameraParamChanged += Hikrobot_CL_CameraParamChanged;
            _disposed = false;
        }

        private bool CheckPortState()
        {
            if (_comPort.IsOpen())
            {
                return true;
            }
            return _comPort.OpenPort();
        }

        public override bool OpenDevice()
        {
            bool b_ret = false;
            b_ret = CheckPortState();
            UpdateNowConfigData();
            return b_ret;
        }

        public override void CloseDevice()
        {
            Dispose();
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

        protected override void InitiallParams()
        {
            _configDatas = new FrameGrabberConfigData();
            ParamElement serial = new ParamElement
            {
                Name = "Serial",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = "serial:1234"
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
                    mValue = "Hikrobot CL"
                }
            };
            ParamElement portName = new ParamElement
            {
                Name = "PortName",
                Type = "String",
                Value = new XmlObject
                {
                    mValue = _comPort.PortName
                }
            };
            ParamElement exp = new ParamElement
            {
                Name = "ExposureTime",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 50
                }
            };
            ParamElement gain = new ParamElement
            {
                Name = "Gain",
                Type = "Single",
                Value = new XmlObject
                {
                    mValue = 1f
                }
            };
            ParamElement lineRate = new ParamElement
            {
                Name = "LineRate",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 10000
                }
            };
            ParamElement direction = new ParamElement
            {
                Name = "ScanDirection",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 0
                }
            };
            ParamElement triggerMode = new ParamElement
            {
                Name = "TriggerMode",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 513
                }
            };
            ParamElement clConfig = new ParamElement
            {
                Name = "CL Config",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 2
                }
            };
            ParamElement defaultUserSet = new ParamElement
            {
                Name = "DefaultUserSet",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1
                }
            };
            _configDatas.CameraOrGrabberParams.AddRange(new ParamElement[12]
            {
            serial, vendorName, category, modelName, portName, exp, gain, lineRate, direction, triggerMode,
            clConfig, defaultUserSet
            });
        }

        public int GetCameraDirection()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "r ReverseScanDirection";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget ReverseScanDirection:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.'));
                }
            }
            return ret;
        }

        public int GetCameraExposureTime()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "r ExposureTime";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget ExposureTime:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.'));
                }
            }
            return ret;
        }

        public float GetCameraGain()
        {
            float ret = 0f;
            if (CheckPortState())
            {
                string commandStr = "r PreampGain";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget PreampGain:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = (float)Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.')) / 1000f;
                }
            }
            return ret;
        }

        public int GetCameraLineRate()
        {
            int ret = -1;
            if (CheckPortState())
            {
                string commandStr = "r AcquisitionLineRate";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget AcquisitionLineRate:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.'));
                }
            }
            return ret;
        }

        public bool GetCameraParam()
        {
            bool b_ret = false;
            _configDatas.CameraOrGrabberParams[5].Value.mValue = GetCameraExposureTime();
            _configDatas.CameraOrGrabberParams[6].Value.mValue = GetCameraGain();
            _configDatas.CameraOrGrabberParams[7].Value.mValue = GetCameraLineRate();
            _configDatas.CameraOrGrabberParams[8].Value.mValue = GetCameraDirection();
            _configDatas.CameraOrGrabberParams[9].Value.mValue = GetCameraTriggerMode();
            _configDatas.CameraOrGrabberParams[10].Value.mValue = GetCLConfig();
            return b_ret;
        }

        public int GetCameraTriggerMode()
        {
            int ret = -1;
            if (CheckPortState())
            {
                string commandStr = "r TriggerMode";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget TriggerMode:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.'));
                }
            }
            return ret;
        }

        public int GetCLConfig()
        {
            int ret = -1;
            if (CheckPortState())
            {
                string commandStr = "r DeviceTapGeometry";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget DeviceTapGeometry:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.'));
                }
            }
            return ret;
        }

        public int GetCurrentUserSet()
        {
            int ret = -1;
            if (CheckPortState())
            {
                string commandStr = "r UserSetCurrent";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals("HKVS $ " + commandStr + "\r\nFailed\r\n") || respStr.Contains("Failed"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                string tempStr = "HKVS $ " + commandStr + "\r\nSuccess!\r\n\r\nget UserSetCurrent:";
                if (respStr.StartsWith(tempStr))
                {
                    ret = Convert.ToInt32(respStr.Substring(tempStr.Length).TrimEnd('.'));
                }
            }
            return ret;
        }

        public bool LoadUserSet(ref int index)
        {
            bool b_ret = true;
            if (index < 0)
            {
                index = 0;
            }
            else if (index > 3)
            {
                index = 3;
            }
            if (CheckPortState())
            {
                string commandStr = "w UserSetSelector " + index;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
                else
                {
                    string commandStr2 = "w UserSetLoad 1";
                    string respStr2 = _comPort.WriteDataToPortWithResponse(commandStr2, 1500);
                    if (respStr2.Equals(commandStr2 + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr2.Contains("Failed") || respStr2.Contains("Wrong input format."))
                    {
                        respStr2 = _comPort.WriteDataToPortWithResponse(commandStr2, 1500);
                    }
                    if (!respStr2.EndsWith("Success!\r\n"))
                    {
                        b_ret = false;
                    }
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        [Setting("DefaultUserSet")]
        public bool SetDefaultUserSet(ref int index)
        {
            return true;
        }

        public bool SaveUserSet(ref int index)
        {
            bool b_ret = true;
            if (index < 1)
            {
                index = 1;
            }
            else if (index > 9)
            {
                index = 9;
            }
            if (CheckPortState())
            {
                string commandStr = "w UserSetSelector " + index;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
                else
                {
                    string commandStr2 = "w UserSetSave 1";
                    string respStr2 = _comPort.WriteDataToPortWithResponse(commandStr2, 1500);
                    if (respStr2.Equals(commandStr2 + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr2.Contains("Failed") || respStr2.Contains("Wrong input format."))
                    {
                        respStr2 = _comPort.WriteDataToPortWithResponse(commandStr2, 1500);
                    }
                    if (!respStr2.EndsWith("Success!\r\n"))
                    {
                        b_ret = false;
                    }
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        public bool SetBaudRate(ref ComBaudRate baudRate)
        {
            return true;
        }

        [Setting("ScanDirection")]
        public bool SetCameraDirection(ref int direction)
        {
            bool b_ret = true;
            if (direction < 0)
            {
                direction = 0;
            }
            else if (direction > 1)
            {
                direction = 1;
            }
            if (CheckPortState())
            {
                string commandStr = "w ReverseScanDirection " + direction;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        [Setting("ExposureTime")]
        public bool SetCameraExposureTime(ref int exposure)
        {
            bool b_ret = true;
            if (exposure < 5)
            {
                exposure = 5;
            }
            else if (exposure > 10000)
            {
                exposure = 10000;
            }
            if (CheckPortState())
            {
                string commandStr = "w ExposureTime " + exposure;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        [Setting("Gain")]
        public bool SetCameraGain(ref float gain)
        {
            bool b_ret = true;
            if (gain < 1f)
            {
                gain = 1f;
            }
            else if (gain > 1f && gain < 1.4f)
            {
                gain = 1f;
            }
            else if (gain > 1.4f && gain < 1.6f)
            {
                gain = 1.4f;
            }
            else if (gain > 1.6f && gain < 2.4f)
            {
                gain = 1.6f;
            }
            else if (gain > 2.4f && gain < 3.2f)
            {
                gain = 2.4f;
            }
            else if (gain > 3.2f)
            {
                gain = 3.2f;
            }
            if (CheckPortState())
            {
                string commandStr = "w PreampGain " + gain * 1000f;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
                if (b_ret)
                {
                    _configDatas.CameraOrGrabberParams[6].Value.mValue = gain;
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        [Setting("LineRate")]
        public bool SetCameraLineRate(ref int lineRate)
        {
            bool b_ret = true;
            if (lineRate < 99)
            {
                lineRate = 300;
            }
            else if (lineRate > 100000)
            {
                lineRate = 100000;
            }
            if (CheckPortState())
            {
                string commandStr = "w AcquisitionLineRate " + lineRate;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        [Setting("TriggerMode")]
        public bool SetCameraTriggerMode(ref int triggerMode)
        {
            bool b_ret = true;
            if (triggerMode < 1 || (triggerMode > 1 && triggerMode < 65))
            {
                triggerMode = 1;
            }
            else if (triggerMode > 65 && triggerMode < 513)
            {
                triggerMode = 65;
            }
            else if (triggerMode > 513 && triggerMode < 577)
            {
                triggerMode = 513;
            }
            else if (triggerMode > 577)
            {
                triggerMode = 577;
            }
            if (CheckPortState())
            {
                string commandStr1 = "w TriggerMode " + triggerMode;
                string respStr1 = _comPort.WriteDataToPortWithResponse(commandStr1, 1000);
                if (respStr1.Equals(commandStr1 + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr1.Contains("Failed") || respStr1.Contains("Wrong input format."))
                {
                    respStr1 = _comPort.WriteDataToPortWithResponse(commandStr1, 1000);
                }
                if (!respStr1.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
                if (b_ret)
                {
                    _configDatas.CameraOrGrabberParams[9].Value.mValue = triggerMode;
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        [Setting("CL Config")]
        public bool SetCLConfig(ref int configMode)
        {
            bool b_ret = true;
            if (configMode < 0)
            {
                configMode = 0;
            }
            else if (configMode > 2)
            {
                configMode = 2;
            }
            int tempMode = 0;
            switch (configMode)
            {
                case 0:
                    tempMode = 16843137;
                    break;
                case 1:
                    tempMode = 16908673;
                    break;
                case 2:
                    tempMode = 17432961;
                    break;
            }
            if (CheckPortState())
            {
                string commandStr = "w DeviceTapGeometry " + tempMode;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                if (respStr.Equals(commandStr + "\r\nFailed!\r\n\r\nWrong input format.\r\n") || respStr.Contains("Failed") || respStr.Contains("Wrong input format."))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1500);
                }
                if (!respStr.EndsWith("Success!\r\n"))
                {
                    b_ret = false;
                }
            }
            else
            {
                b_ret = false;
            }
            return b_ret;
        }

        public void UpdateNowConfigData()
        {
            if (_comPort.IsOpen())
            {
                int defaultUserSet = Convert.ToInt32(_configDatas.CameraOrGrabberParams[10].Value.mValue);
                LoadUserSet(ref defaultUserSet);
                int exp = Convert.ToInt32(_configDatas.CameraOrGrabberParams[5].Value.mValue);
                SetCameraExposureTime(ref exp);
                float gain = Convert.ToSingle(_configDatas.CameraOrGrabberParams[6].Value.mValue);
                SetCameraGain(ref gain);
                int direc = Convert.ToInt32(_configDatas.CameraOrGrabberParams[8].Value.mValue);
                SetCameraDirection(ref direc);
                int triggerMode = Convert.ToInt32(_configDatas.CameraOrGrabberParams[9].Value.mValue);
                SetCameraTriggerMode(ref triggerMode);
            }
        }

        public bool DownLoadCameraParam()
        {
            return GetCameraParam();
        }

        public bool LoadCameraParam(FrameGrabberConfigData paramValues)
        {
            bool b_ret = false;
            try
            {
                for (int i = 0; i < _configDatas.CameraOrGrabberParams.Count; i++)
                {
                    _configDatas[i] = paramValues[i];
                }
                b_ret = true;
            }
            catch (Exception)
            {
                return b_ret;
            }
            return b_ret;
        }

        public override void SetWorkMode(int workMode)
        {
            try
            {
                switch (workMode)
                {
                    case 1:
                        {
                            int mode = 1;
                            SetCameraTriggerMode(ref mode);
                            break;
                        }
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        {
                            int mode = 513;
                            SetCameraTriggerMode(ref mode);
                            break;
                        }
                    case 6:
                        break;
                }
            }
            catch
            {
            }
        }

        private void Hikrobot_CL_CameraParamChanged(object sender, CameraParamChangeArgs e)
        {
            bool @return = this.ExecuteMethod(e);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_comPort.IsOpen())
                {
                    _comPort.ClosePort();
                }
                _configDatas.CameraParamChanged -= Hikrobot_CL_CameraParamChanged;
            }
            _comPort = null;
            _configDatas = null;
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
