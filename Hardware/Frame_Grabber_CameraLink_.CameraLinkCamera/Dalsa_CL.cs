using System;
using System.Text;
using NovaVision.BaseClass.Helper;
using NovaVision.Hardware.Frame_Grabber;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_.CameraLinkCamera
{
    public class Dalsa_CL : Bv_Camera, IDisposable
    {
        private bool _disposed;

        private CameraSerialPort _comPort;

        private FrameGrabberConfigData _configDatas;

        private readonly string _vendorName = "Dalsa_CL";

        private readonly CameraCategory CATEGORY = CameraCategory.C_2DLineCL;

        public CameraSerialPort ComPort => _comPort;

        public override string VendorName => _vendorName;

        public override CameraCategory Category => CATEGORY;

        public override string SerialNum => _configDatas.CameraOrGrabberParams[0].Value.ToString();

        public override FrameGrabberConfigData ConfigDatas => _configDatas;

        public Dalsa_CL(CameraSerialPort comPort, FrameGrabberConfigData paramValues)
        {
            _comPort = comPort;
            InitiallParams();
            if (paramValues != null && paramValues.CameraOrGrabberParams.Count == _configDatas.CameraOrGrabberParams.Count)
            {
                _configDatas = paramValues;
            }
            UpdateNowConfigData();
            _configDatas.CameraParamChanged += Dalsa_CL_CameraParamChanged;
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

        public int GetCameraDirection()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "get 'scd";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] directStrs = new string[2];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, directStrs, 0, 2);
                    try
                    {
                        ret = Convert.ToInt32(directStrs[1]);
                    }
                    catch
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        public int GetCameraExposureTime()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "get 'set";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] expStrs = new string[1];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, expStrs, 0, 1);
                    try
                    {
                        ret = Convert.ToInt32(expStrs[0]) / 1000;
                    }
                    catch
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        public float GetCameraGain()
        {
            float ret = 0f;
            if (CheckPortState())
            {
                string commandStr = "get 'ssg";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] gains = new string[4];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, gains, 0, 4);
                    try
                    {
                        ret = Convert.ToSingle(gains[0]);
                    }
                    catch
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        public int GetCameraLineRate()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "get 'ssf";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] lineRateStrs = new string[1];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, lineRateStrs, 0, 1);
                    try
                    {
                        ret = Convert.ToInt32(lineRateStrs[0]);
                    }
                    catch
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        public bool GetCameraParam()
        {
            bool b_ret = false;
            if (CheckPortState())
            {
                string commandStr = "gcp";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 5000);
                for (int i = 0; i < 3; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 5000);
                        break;
                    }
                }
                if (respStr.EndsWith("\r\nUSER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    string[] strParams = respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None);
                    _configDatas.CameraOrGrabberParams[3].Value.mValue = strParams[1].Replace(" ", "").Remove(0, 5);
                    _configDatas.CameraOrGrabberParams[4].Value.mValue = strParams[5].Replace(" ", "").Remove(0, 7);
                    _configDatas.CameraOrGrabberParams[5].Value.mValue = Convert.ToInt32(strParams[14].Replace(" ", "").Remove(0, 8).TrimEnd("[ns]".ToCharArray()));
                    _configDatas.CameraOrGrabberParams[6].Value.mValue = Convert.ToSingle(strParams[28].Replace(" ", "").Remove(0, 10));
                    _configDatas.CameraOrGrabberParams[7].Value.mValue = Convert.ToInt32(strParams[10].Replace(" ", "").Remove(0, 8).TrimEnd("[Hz]".ToCharArray()));
                    string[] temps = strParams[10].Replace(" ", "").Split(',');
                    string tempStr1 = temps[0];
                    string tempStr2 = temps[1];
                    if (tempStr1.Contains("Internal"))
                    {
                        string text = tempStr2;
                        string text2 = text;
                        if (!(text2 == "Forward"))
                        {
                            if (text2 == "Reverse")
                            {
                                _configDatas.CameraOrGrabberParams[8].Value.mValue = 1;
                            }
                        }
                        else
                        {
                            _configDatas.CameraOrGrabberParams[8].Value.mValue = 0;
                        }
                    }
                    else
                    {
                        _configDatas.CameraOrGrabberParams[8].Value.mValue = 2;
                    }
                    _configDatas.CameraOrGrabberParams[9].Value.mValue = ((!strParams[9].Replace(" ", "").Remove(0, 7).Equals("Off")) ? 1 : 0);
                    switch (strParams[40].Replace(" ", "").Remove(0, 8))
                    {
                        case "Base":
                            _configDatas.CameraOrGrabberParams[10].Value.mValue = 0;
                            break;
                        case "Medium":
                            _configDatas.CameraOrGrabberParams[10].Value.mValue = 1;
                            break;
                        case "Full":
                            _configDatas.CameraOrGrabberParams[10].Value.mValue = 2;
                            break;
                        case "Deca":
                            _configDatas.CameraOrGrabberParams[10].Value.mValue = 3;
                            break;
                    }
                    _configDatas.CameraOrGrabberParams[11].Value.mValue = Convert.ToInt32(strParams[8].Replace(" ", "").Remove(0, 10));
                    _configDatas.VendorNameKey = $"{_vendorName},{_configDatas.CameraOrGrabberParams[4].Value}";
                }
            }
            return b_ret;
        }

        public int GetCameraTriggerMode()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "get 'stm";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] triggerModeStrs = new string[1];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.Contains("??"))
                {
                    respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, triggerModeStrs, 0, 1);
                    try
                    {
                        ret = Convert.ToInt32(triggerModeStrs[0]);
                    }
                    catch
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        public int GetCLConfig()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "get 'clm";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] clConfigStrs = new string[1];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, clConfigStrs, 0, 1);
                    try
                    {
                        ret = Convert.ToInt32(clConfigStrs[0]);
                    }
                    catch
                    {
                        return ret;
                    }
                }
            }
            return ret;
        }

        public int GetCurrentUserSet()
        {
            int ret = 0;
            if (CheckPortState())
            {
                string commandStr = "get 'usl";
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                string[] currentSetStrs = new string[1];
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (respStr.EndsWith("USER>") && !respStr.Equals("\r\nUSER>") && !respStr.Contains("Incorrect number of parameters"))
                {
                    Array.Copy(respStr.Split(new string[1] { "\r\n" }, StringSplitOptions.None), 1, currentSetStrs, 0, 1);
                    try
                    {
                        ret = Convert.ToInt32(currentSetStrs[0]);
                    }
                    catch
                    {
                        return ret;
                    }
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
            else if (index > 8)
            {
                index = 8;
            }
            if (CheckPortState())
            {
                string commandStr = "usl " + index;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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

        [Setting("DefaultUserSet")]
        public bool SetDefaultUserSet(ref int index)
        {
            bool b_ret = true;
            if (index < 0)
            {
                index = 0;
            }
            else if (index > 8)
            {
                index = 8;
            }
            if (CheckPortState())
            {
                string commandStr = "usd " + index;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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

        public bool SaveUserSet(ref int index)
        {
            bool b_ret = true;
            if (index < 0)
            {
                index = 0;
            }
            else if (index > 8)
            {
                index = 8;
            }
            if (CheckPortState())
            {
                string commandStr = "uss " + index;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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

        public bool SetBaudRate(ref ComBaudRate baudRate)
        {
            bool b_ret = true;
            if (CheckPortState())
            {
                string commandStr = "sbr ";
                switch (baudRate)
                {
                    case ComBaudRate.Baud_9600:
                        commandStr += "9600";
                        break;
                    case ComBaudRate.Baud_57600:
                        commandStr += "57600";
                        break;
                    case ComBaudRate.Baud_115200:
                        commandStr += "115200";
                        break;
                    case ComBaudRate.Baud_230400:
                        commandStr += "230400";
                        break;
                    case ComBaudRate.Baud_460800:
                        commandStr += "460800";
                        break;
                    case ComBaudRate.Baud_921600:
                        commandStr += "921600";
                        break;
                }
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Contains("Set terminal baud rate..."))
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

        [Setting("ScanDirection")]
        public bool SetCameraDirection(ref int direction)
        {
            bool b_ret = true;
            if (direction < 0)
            {
                direction = 0;
            }
            else if (direction > 2)
            {
                direction = 2;
            }
            if (CheckPortState())
            {
                string commandStr = "scd " + direction;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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
            if (exposure < 4)
            {
                exposure = 4;
            }
            else if (exposure > 3330)
            {
                exposure = 3330;
            }
            if (CheckPortState())
            {
                string commandStr = "set " + exposure * 1000;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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
            if (gain < 0f)
            {
                gain = 0f;
            }
            else if (gain > 100f)
            {
                gain = 100f;
            }
            if (CheckPortState())
            {
                string commandStr = "ssg 0 f" + gain;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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

        [Setting("LineRate")]
        public bool SetCameraLineRate(ref int lineRate)
        {
            bool b_ret = true;
            if (lineRate < 300)
            {
                lineRate = 300;
            }
            else if (lineRate > 48000)
            {
                lineRate = 48000;
            }
            if (CheckPortState())
            {
                string commandStr = "ssf " + lineRate;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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
            if (triggerMode < 0)
            {
                triggerMode = 0;
            }
            else if (triggerMode > 1)
            {
                triggerMode = 1;
            }
            if (CheckPortState())
            {
                string commandStr = "stm " + triggerMode;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
                {
                    b_ret = false;
                }
            }
            else
            {
                b_ret = false;
            }
            if (b_ret)
            {
                _configDatas["TriggerMode"].Value.mValue = triggerMode;
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
            else if (configMode > 3)
            {
                configMode = 3;
            }
            if (CheckPortState())
            {
                string commandStr = "clm " + configMode;
                string respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                for (int i = 0; i < 1; i++)
                {
                    if (respStr.Contains("?") || respStr.Contains("\0") || respStr.Equals(""))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(Encoding.ASCII.GetString(new byte[1] { 27 }), 1000);
                    }
                    if (respStr.EndsWith("USER>"))
                    {
                        respStr = _comPort.WriteDataToPortWithResponse(commandStr, 1000);
                        break;
                    }
                }
                if (!respStr.Equals("\r\nUSER>"))
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
                int temp = 0;
                switch (workMode)
                {
                    case 1:
                    case 2:
                        temp = 0;
                        SetCameraTriggerMode(ref temp);
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        temp = 1;
                        SetCameraTriggerMode(ref temp);
                        break;
                }
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dalsa_CL_CameraParamChanged(object sender, CameraParamChangeArgs e)
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
                _configDatas.CameraParamChanged -= Dalsa_CL_CameraParamChanged;
            }
            _comPort = null;
            _configDatas = null;
            _disposed = true;
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
                    mValue = "Model-Name"
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
                    mValue = 1
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
    }
}
