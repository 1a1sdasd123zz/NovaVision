using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using DALSA.SaperaLT.SapClassBasic;
using Microsoft.Win32;
using NovaVision.BaseClass;

namespace NovaVision.Hardware.DalsaTool
{
    public class Camera_Dalsa2D : CameraLine2DBase
    {
        public enum ServerCategory
        {
            ServerAll,
            ServerAcq,
            ServerAcqDevice
        }

        public class MyComBoxItem
        {
            private string Camdesc;

            private bool itemData;

            public bool ItemData
            {
                get
                {
                    return itemData;
                }
                set
                {
                    itemData = value;
                }
            }

            public MyComBoxItem(string Text)
            {
                Camdesc = Text;
                itemData = false;
            }

            public MyComBoxItem(string Text, bool ItemData)
            {
                Camdesc = Text;
                itemData = ItemData;
            }

            public override string ToString()
            {
                return Camdesc;
            }
        }

        public class MyCameraInfo
        {
            private string _sn;

            private string _manufacturerPatNumber;

            private string _vendorName;

            private string _version;

            private string _ip;

            private string _sensorStatus;

            private string _deviceUserId;

            public string ManufacturerPatNumber
            {
                get
                {
                    return _manufacturerPatNumber;
                }
                set
                {
                    _manufacturerPatNumber = value;
                }
            }

            public string VendorName
            {
                get
                {
                    return _vendorName;
                }
                set
                {
                    _vendorName = value;
                }
            }

            public string Version
            {
                get
                {
                    return _version;
                }
                set
                {
                    _version = value;
                }
            }

            public string Ip
            {
                get
                {
                    return _ip;
                }
                set
                {
                    _ip = value;
                }
            }

            public string SensorStatus
            {
                get
                {
                    return _sensorStatus;
                }
                set
                {
                    _sensorStatus = value;
                }
            }

            public string DeviceUserId
            {
                get
                {
                    return _deviceUserId;
                }
                set
                {
                    _deviceUserId = value;
                }
            }

            public MyCameraInfo(string sn)
            {
                _sn = sn;
            }
        }

        public static bool isCreate = false;

        private DALSA.SaperaLT.SapClassBasic.SapAcqDevice m_AcqDevice;

        private DALSA.SaperaLT.SapClassBasic.SapBuffer m_Buffers;

        private DALSA.SaperaLT.SapClassBasic.SapAcqDeviceToBuf m_Xfer;

        private DALSA.SaperaLT.SapClassBasic.SapView m_View;

        private string m_ConfigFile = "";

        public MyCameraInfo myCameraInfo;

        private TriggerModeEnum triggerMode;

        public int triggerSelectorNum;

        private Stopwatch RunTimes = new Stopwatch();

        public static List<MyComBoxItem> cameraTextList = new List<MyComBoxItem>();

        public static Dictionary<string, string> D_dalsa = new Dictionary<string, string>();

        public static Dictionary<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice> D_sapAcqDevice = new Dictionary<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice>();

        public static Dictionary<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice> D_sapAcqDeviceNext = new Dictionary<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice>();

        public static ServerCategory m_ServerCategory = ServerCategory.ServerAll;

        private static string m_ServerName = "";

        public static int m_ServerIndex = 0;

        public static int m_ResourceIndex = 0;

        public static string m_ResourceName = "";

        private Cognex.VisionPro.ICogImage m_OutputImage;

        private int m_acqTimeOut = 10;

        public string ConfigFile
        {
            get
            {
                if (m_ConfigFile != null)
                {
                    return m_ConfigFile;
                }
                return "";
            }
        }

        public ServerCategory ServCategory => m_ServerCategory;

        public TriggerModeEnum TriggerMode
        {
            get
            {
                m_AcqDevice.SetFeatureValue("TriggerSelector", 0);
                m_AcqDevice.GetFeatureValue("TriggerMode", out string triggerStr0);
                m_AcqDevice.SetFeatureValue("TriggerSelector", 1);
                m_AcqDevice.GetFeatureValue("TriggerMode", out string triggerStr1);
                m_AcqDevice.SetFeatureValue("TriggerSelector", 2);
                m_AcqDevice.GetFeatureValue("TriggerMode", out string triggerStr2);
                m_AcqDevice.SetFeatureValue("TriggerSelector", 3);
                m_AcqDevice.GetFeatureValue("TriggerMode", out string triggerStr3);
                m_AcqDevice.SetFeatureValue("TriggerSelector", 4);
                m_AcqDevice.GetFeatureValue("TriggerMode", out string triggerStr4);
                if (triggerStr0.Contains("On"))
                {
                    triggerSelectorNum = 0;
                    triggerMode = TriggerModeEnum.On;
                }
                else if (triggerStr1.Contains("On"))
                {
                    triggerSelectorNum = 1;
                }
                else if (triggerStr2.Contains("On"))
                {
                    triggerSelectorNum = 2;
                    triggerMode = TriggerModeEnum.On;
                }
                else if (triggerStr3.Contains("On"))
                {
                    triggerSelectorNum = 3;
                    triggerMode = TriggerModeEnum.On;
                }
                else if (triggerStr4.Contains("On"))
                {
                    triggerSelectorNum = 4;
                    triggerMode = TriggerModeEnum.On;
                }
                else
                {
                    triggerSelectorNum = 5;
                    triggerMode = TriggerModeEnum.Off;
                }
                return triggerMode;
            }
            set
            {
                if (value == TriggerModeEnum.On)
                {
                    if (m_AcqDevice.SetFeatureValue("TriggerMode", 1))
                    {
                        triggerMode = TriggerModeEnum.On;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 触发模式设置失败");
                    }
                }
                else if (m_AcqDevice.SetFeatureValue("TriggerMode", 0))
                {
                    triggerMode = TriggerModeEnum.Off;
                }
                else
                {
                    LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 触发模式设置失败");
                }
            }
        }

        public override float Exposure
        {
            get
            {
                m_AcqDevice.GetFeatureIndexByName("ExposureTime", out var index);
                m_AcqDevice.GetFeatureValue(index, out string value);
                if (value.Contains("."))
                {
                    value = value.Substring(0, value.IndexOf('.'));
                }
                float.TryParse(value, out var temp);
                exposure = temp;
                SettingParams.ExposureTime = (int)exposure;
                return exposure;
            }
            set
            {
                if ((double)exposure != (double)value)
                {
                    if (m_AcqDevice.SetFeatureValue("ExposureTime", (double)value))
                    {
                        exposure = value;
                        SettingParams.ExposureTime = (int)exposure;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 曝光设置失败");
                    }
                }
            }
        }

        public override float Gain
        {
            get
            {
                m_AcqDevice.GetFeatureIndexByName("Gain", out var index);
                m_AcqDevice.GetFeatureValue(index, out string value);
                if (value.Contains("."))
                {
                    value = value.Substring(0, value.IndexOf('.'));
                }
                //decimal.TryParse(value, out gain);
                SettingParams.Gain = (int)gain;
                return gain;
            }
            set
            {
                if ((double)gain != (double)value)
                {
                    if (m_AcqDevice.SetFeatureValue("Gain", (double)value))
                    {
                        gain = value;
                        SettingParams.Gain = (int)gain;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 增益设置失败");
                    }
                }
            }
        }

        public override long ScanWidth
        {
            get
            {
                m_AcqDevice.GetFeatureIndexByName("Width", out var index);
                m_AcqDevice.GetFeatureValue(index, out string value);
                if (value.Contains("."))
                {
                    value = value.Substring(0, value.IndexOf('.'));
                }
                long.TryParse(value, out scanWidth);
                SettingParams.ScanWidth = (int)scanWidth;
                return scanWidth;
            }
            set
            {
                if (scanWidth != value)
                {
                    if (m_AcqDevice.SetFeatureValue("Width", value))
                    {
                        scanWidth = value;
                        SettingParams.ScanWidth = (int)scanWidth;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 扫描宽度设置失败");
                    }
                }
            }
        }

        public override long ScanHeight
        {
            get
            {
                m_AcqDevice.GetFeatureIndexByName("Height", out var index);
                m_AcqDevice.GetFeatureValue(index, out string value);
                if (value.Contains("."))
                {
                    value = value.Substring(0, value.IndexOf('.'));
                }
                long.TryParse(value, out scanHeight);
                SettingParams.ScanHeight = (int)scanHeight;
                return scanHeight;
            }
            set
            {
                if (scanHeight != value)
                {
                    if (m_AcqDevice.SetFeatureValue("Height", value))
                    {
                        scanHeight = value;
                        SettingParams.ScanHeight = (int)scanHeight;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 扫描高度设置失败");
                    }
                }
            }
        }

        public override long OffsetX
        {
            get
            {
                m_AcqDevice.GetFeatureIndexByName("OffsetX", out var index);
                m_AcqDevice.GetFeatureValue(index, out string value);
                if (value.Contains("."))
                {
                    value = value.Substring(0, value.IndexOf('.'));
                }
                long.TryParse(value, out offsetX);
                SettingParams.OffsetX = (int)offsetX;
                return offsetX;
            }
            set
            {
                if (offsetX != value)
                {
                    if (m_AcqDevice.SetFeatureValue("OffsetX", value))
                    {
                        offsetX = value;
                        SettingParams.OffsetX = (int)offsetX;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 横向偏移量设置失败");
                    }
                }
            }
        }

        public override long TimerDuration
        {
            get
            {
                m_AcqDevice.GetFeatureValue("timerDuration", out string durationStr);
                if (durationStr.Contains("."))
                {
                    durationStr = durationStr.Substring(0, durationStr.IndexOf('.'));
                }
                long.TryParse(durationStr, out var timerDuration);
                SettingParams.TimerDuration = (int)timerDuration;
                return timerDuration;
            }
            set
            {
                if (timerDuration != (int)value)
                {
                    m_AcqDevice.SetFeatureValue("timerSelector", 0);
                    m_AcqDevice.SetFeatureValue("timerMode", 0);
                    if (m_AcqDevice.SetFeatureValue("timerDuration", (int)value))
                    {
                        timerDuration = value;
                        SettingParams.TimerDuration = (int)timerDuration;
                        m_AcqDevice.SetFeatureValue("timerMode", 1);
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") TimerDuration设置失败");
                    }
                }
            }
        }

        public override long AcqLineRate
        {
            get
            {
                m_AcqDevice.GetFeatureValue("AcquisitionLineRate", out string rateStr);
                if (rateStr.Contains("."))
                {
                    rateStr = rateStr.Substring(0, rateStr.IndexOf('.'));
                }
                long.TryParse(rateStr, out acqLineRate);
                SettingParams.AcqLineRate = Convert.ToInt32(acqLineRate);
                return acqLineRate;
            }
            set
            {
                if ((double)acqLineRate != (double)value)
                {
                    if (m_AcqDevice.SetFeatureValue("AcquisitionLineRate", (double)value))
                    {
                        acqLineRate = value;
                        SettingParams.AcqLineRate = (int)acqLineRate;
                    }
                    else
                    {
                        LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 线扫行频设置失败");
                    }
                }
            }
        }

        public int AcqTimeOut
        {
            get
            {
                return m_acqTimeOut;
            }
            set
            {
                m_acqTimeOut = value;
            }
        }

        public Camera_Dalsa2D(string externSN)
        {
            cameraSN = externSN;
            if (!isCreate)
            {
                DALSA.SaperaLT.SapClassBasic.SapManager.ServerEventType = DALSA.SaperaLT.SapClassBasic.SapManager.EventType.ServerDisconnected;
                DALSA.SaperaLT.SapClassBasic.SapManager.ServerNotify += SapManager_ServerNotify;
                DALSA.SaperaLT.SapClassBasic.SapManager.ServerNotifyContext = "";
                isCreate = true;
            }
        }

        private void SapManager_ServerNotify(object sender, SapServerNotifyEventArgs args)
        {
            if (args.EventType != DALSA.SaperaLT.SapClassBasic.SapManager.EventType.ServerDisconnected)
            {
                return;
            }
            DALSA.SaperaLT.SapClassBasic.SapLocation loc = new DALSA.SaperaLT.SapClassBasic.SapLocation(args.ServerIndex, args.ResourceIndex);
            foreach (KeyValuePair<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice> item in D_sapAcqDevice)
            {
                DALSA.SaperaLT.SapClassBasic.SapAcqDevice sapAcqDevice = item.Value;
                if (DALSA.SaperaLT.SapClassBasic.SapManager.IsSameLocation(sapAcqDevice.Location, loc))
                {
                    if (cam_Handle != null)
                    {
                        CameraMessage cameraMessage = new CameraMessage(item.Key, true);
                        cam_Handle.CamConnectedLostHandle(cameraMessage);
                    }
                    CameraOperator.camera2DLineCollection.Remove(item.Key);
                    LogUtil.LogError("DalsaLineScan 相机(" + item.Key + ") 掉线！");
                    break;
                }
            }
        }

        public static void EnumCamera()
        {
            try
            {
                cameraTextList.Clear();
                D_sapAcqDevice.Clear();
                D_dalsa.Clear();
                if (!InitServer())
                {
                    LogUtil.Log("未查找到DalsaLineScan相机");
                }
            }
            catch (Exception)
            {
                LogUtil.Log("DALSALineScan相机枚举异常！");
            }
        }

        private static bool InitServer()
        {
            try
            {
                for (int i = 0; i < DALSA.SaperaLT.SapClassBasic.SapManager.GetServerCount(); i++)
                {
                    bool bAcq = (m_ServerCategory == ServerCategory.ServerAcq || m_ServerCategory == ServerCategory.ServerAll) && DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(i, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq) > 0;
                    bool bAcqDevice = (m_ServerCategory == ServerCategory.ServerAcqDevice || m_ServerCategory == ServerCategory.ServerAll) && DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(i, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.AcqDevice) > 0 && DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(i, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq) == 0;
                    if (bAcq)
                    {
                        string serverName2 = DALSA.SaperaLT.SapClassBasic.SapManager.GetServerName(i);
                        cameraTextList.Add(new MyComBoxItem(serverName2, ItemData: true));
                    }
                    else if (bAcqDevice)
                    {
                        string serverName = DALSA.SaperaLT.SapClassBasic.SapManager.GetServerName(i);
                        if (!serverName.Contains("CameraLink_") && !serverName.Contains("LP1") && !serverName.Contains("LP2") && serverName.Contains("Linea_"))
                        {
                            InitResource(serverName);
                        }
                    }
                }
                D_sapAcqDeviceNext.Clear();
                foreach (KeyValuePair<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice> item in D_sapAcqDevice)
                {
                    D_sapAcqDeviceNext.Add(item.Key, item.Value);
                }
                if (cameraTextList.Count <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void InitResource(string serverName)
        {
            int i = 0;
            if (DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(serverName, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq) == 0)
            {
                for (i = 0; i < DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(serverName, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.AcqDevice); i++)
                {
                    DALSA.SaperaLT.SapClassBasic.SapLocation m_ServerLocation = new DALSA.SaperaLT.SapClassBasic.SapLocation(serverName, i);
                    string deviceName = DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceName(m_ServerLocation, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.AcqDevice);
                    DALSA.SaperaLT.SapClassBasic.SapAcqDevice acqDevice = new DALSA.SaperaLT.SapClassBasic.SapAcqDevice(m_ServerLocation);
                    if (!acqDevice.Initialized)
                    {
                        if (DALSA.SaperaLT.SapClassBasic.SapManager.IsResourceAvailable(m_ServerLocation, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.AcqDevice))
                        {
                            if (!acqDevice.Create())
                            {
                                LogUtil.LogError("DalsaLineScan(" + deviceName + ") 相机创建失败");
                                continue;
                            }
                        }
                        else
                        {
                            DALSA.SaperaLT.SapClassBasic.SapAcqDevice _acqDevice = new DALSA.SaperaLT.SapClassBasic.SapAcqDevice();
                            bool IsExist = false;
                            foreach (KeyValuePair<string, DALSA.SaperaLT.SapClassBasic.SapAcqDevice> item2 in D_sapAcqDeviceNext)
                            {
                                _acqDevice = item2.Value;
                                if (_acqDevice.Location.ServerName == serverName)
                                {
                                    IsExist = true;
                                    break;
                                }
                            }
                            if (!IsExist)
                            {
                                LogUtil.LogError("DalsaLineScan(" + deviceName + ") 相机资源被占用或已掉线！");
                                continue;
                            }
                            acqDevice = _acqDevice;
                        }
                    }
                    acqDevice.GetFeatureIndexByName("deviceManufacturerPartNumber", out var index);
                    acqDevice.GetFeatureValue(index, out string manufacturerPatNumber);
                    if (!D_sapAcqDevice.ContainsKey(deviceName))
                    {
                        D_sapAcqDevice.Add(deviceName, acqDevice);
                    }
                    if (!D_dalsa.ContainsKey(deviceName))
                    {
                        D_dalsa.Add(deviceName, deviceName);
                    }
                    string text = $"{manufacturerPatNumber}({deviceName})";
                    MyComBoxItem item = new MyComBoxItem(text);
                    cameraTextList.Add(item);
                }
                return;
            }
            for (i = 0; i < DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(serverName, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq); i++)
            {
                string resourceName = DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceName(serverName, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq, i);
                if (DALSA.SaperaLT.SapClassBasic.SapManager.IsResourceAvailable(serverName, DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq, i))
                {
                    if (i == m_ResourceIndex)
                    {
                        m_ResourceName = resourceName;
                    }
                }
                else
                {
                    m_ResourceIndex = 0;
                }
            }
        }

        public override int OpenCamera()
        {
            try
            {
                m_AcqDevice = D_sapAcqDevice[cameraSN];
                if (DALSA.SaperaLT.SapClassBasic.SapBuffer.IsBufferTypeSupported(m_AcqDevice.Location, DALSA.SaperaLT.SapClassBasic.SapBuffer.MemoryType.ScatterGather))
                {
                    m_Buffers = new DALSA.SaperaLT.SapClassBasic.SapBufferWithTrash(2, m_AcqDevice, DALSA.SaperaLT.SapClassBasic.SapBuffer.MemoryType.ScatterGather);
                }
                else
                {
                    m_Buffers = new DALSA.SaperaLT.SapClassBasic.SapBufferWithTrash(2, m_AcqDevice, DALSA.SaperaLT.SapClassBasic.SapBuffer.MemoryType.ScatterGatherPhysical);
                }
                m_Xfer = new DALSA.SaperaLT.SapClassBasic.SapAcqDeviceToBuf(m_AcqDevice, m_Buffers);
                m_Xfer.Pairs[0].EventType = DALSA.SaperaLT.SapClassBasic.SapXferPair.XferEventType.EndOfFrame;
                m_Xfer.XferNotify += xfer_XferNotify;
                m_Xfer.XferNotifyContext = this;
                if (!CameraOperator.camera2DLineCollection._2DLineCameras.ContainsKey(cameraSN) && !CreateObjects())
                {
                    DisposeObjects();
                    return -1;
                }
                m_AcqDevice.GetFeatureIndexByName("deviceManufacturerPartNumber", out var index);
                m_AcqDevice.GetFeatureIndexByName("DeviceVendorName", out var index2);
                m_AcqDevice.GetFeatureIndexByName("DeviceVersion", out var index3);
                m_AcqDevice.GetFeatureIndexByName("DeviceUserID", out var index4);
                m_AcqDevice.GetFeatureIndexByName("GevCurrentIPAddress", out var index5);
                m_AcqDevice.GetFeatureValue(index, out string manufacturerPatNumber);
                m_AcqDevice.GetFeatureValue(index2, out string vendorName);
                m_AcqDevice.GetFeatureValue(index3, out string version);
                m_AcqDevice.GetFeatureValue(index4, out string _);
                m_AcqDevice.GetFeatureValue(index5, out string ip);
                DeviceIP = ip;
                VendorName = vendorName;
                ModelName = manufacturerPatNumber;
                Version = version;
                if (!CameraOperator.camera2DLineCollection._2DLineCameras.ContainsKey(cameraSN))
                {
                    CameraOperator.camera2DLineCollection.Add(cameraSN, this);
                }
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(cameraSN, true);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                return 0;
            }
            catch (Exception)
            {
                LogUtil.LogError("DalsaLineScan 相机连接异常！");
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
        }

        private bool CreateObjects()
        {
            if (m_Buffers != null && !m_Buffers.Initialized)
            {
                if (!m_Buffers.Create())
                {
                    DisposeObjects();
                    return false;
                }
                m_Buffers.Clear();
            }
            if (m_View != null && !m_View.Initialized && !m_View.Create())
            {
                DisposeObjects();
                return false;
            }
            if (m_Xfer != null && m_Xfer.Pairs[0] != null)
            {
                m_Xfer.Pairs[0].Cycle = DALSA.SaperaLT.SapClassBasic.SapXferPair.CycleMode.NextWithTrash;
                if (m_Xfer.Pairs[0].Cycle != DALSA.SaperaLT.SapClassBasic.SapXferPair.CycleMode.NextWithTrash)
                {
                    DisposeObjects();
                    return false;
                }
            }
            if (m_Xfer != null && !m_Xfer.Initialized && !m_Xfer.Create())
            {
                DisposeObjects();
                return false;
            }
            return true;
        }

        public void DisposeObjects()
        {
            if (m_Xfer != null)
            {
                m_Xfer.Dispose();
                m_Xfer = null;
            }
            if (m_View != null)
            {
                m_View.Dispose();
                m_View = null;
            }
            if (m_Buffers != null)
            {
                m_Buffers.Dispose();
                m_Buffers = null;
            }
        }

        private void xfer_XferNotify(object sender, SapXferNotifyEventArgs argsNotify)
        {
            acqOk = true;
            Camera_Dalsa2D camera = argsNotify.Context as Camera_Dalsa2D;
            int height = (int)camera.scanHeight;
            int width = (int)camera.scanWidth;
            if (!camera.m_Buffers.GetAddress(out var mptr))
            {
                return;
            }
            switch (camera.m_Buffers.Format)
            {
                case SapFormat.Mono8:
                    m_OutputImage = GetOutputImage(height, width, mptr);
                    break;
                case SapFormat.RGB8888:
                    {
                        byte[] imageBuffer = new byte[height * width * 4];
                        Marshal.Copy(mptr, imageBuffer, 0, imageBuffer.Length);
                        byte[] tempR = new byte[height * width];
                        byte[] tempG = new byte[height * width];
                        byte[] tempB = new byte[height * width];
                        for (int i = 0; i < width * height * 4; i += 4)
                        {
                            tempB[i / 4] = imageBuffer[i];
                            tempG[i / 4] = imageBuffer[i + 1];
                            tempR[i / 4] = imageBuffer[i + 2];
                        }
                        Buffer.BlockCopy(tempR, 0, imageBuffer, 0, tempR.Length);
                        Buffer.BlockCopy(tempG, 0, imageBuffer, height * width, tempG.Length);
                        Buffer.BlockCopy(tempB, 0, imageBuffer, height * width * 2, tempB.Length);
                        GCHandle handle = GCHandle.Alloc(imageBuffer, GCHandleType.Pinned);
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(imageBuffer, 0);
                        m_OutputImage = ImageData.GetRGBImage(height, width, buffer);
                        handle.Free();
                        break;
                    }
                case SapFormat.RGBR888:
                    {
                        byte[] imageBuffer_RGBR = new byte[height * width * 3];
                        Marshal.Copy(mptr, imageBuffer_RGBR, 0, imageBuffer_RGBR.Length);
                        byte[] tempR_RGBR = new byte[height * width];
                        byte[] tempG_RGBR = new byte[height * width];
                        byte[] tempB_RGBR = new byte[height * width];
                        for (int j = 0; j < width * height * 3; j += 3)
                        {
                            tempR_RGBR[j / 3] = imageBuffer_RGBR[j];
                            tempG_RGBR[j / 3] = imageBuffer_RGBR[j + 1];
                            tempB_RGBR[j / 3] = imageBuffer_RGBR[j + 2];
                        }
                        Buffer.BlockCopy(tempR_RGBR, 0, imageBuffer_RGBR, 0, tempR_RGBR.Length);
                        Buffer.BlockCopy(tempG_RGBR, 0, imageBuffer_RGBR, height * width, tempG_RGBR.Length);
                        Buffer.BlockCopy(tempB_RGBR, 0, imageBuffer_RGBR, height * width * 2, tempB_RGBR.Length);
                        GCHandle handle_RGBR = GCHandle.Alloc(imageBuffer_RGBR, GCHandleType.Pinned);
                        IntPtr buffer_RGBR = Marshal.UnsafeAddrOfPinnedArrayElement(imageBuffer_RGBR, 0);
                        m_OutputImage = ImageData.GetRGBImage(height, width, buffer_RGBR);
                        handle_RGBR.Free();
                        break;
                    }
                default:
                    m_OutputImage = GetOutputImage(height, width, mptr);
                    break;
            }
            if (UpdateImage != null)
            {
                UpdateImage(new ImageData(m_OutputImage));
            }
            GC.Collect();
        }

        public Cognex.VisionPro.ICogImage GetOutputImage(int nHeight, int nWidth, IntPtr pImageBuf)
        {
            try
            {
                CogImage8Root cogImage8Root = new CogImage8Root();
                cogImage8Root.Initialize(nWidth, nHeight, pImageBuf, nWidth, null);
                CogImage8Grey cogImage8Grey = new CogImage8Grey();
                cogImage8Grey.SetRoot(cogImage8Root);
                return cogImage8Grey.ScaleImage(nWidth, nHeight);
            }
            catch
            {
                return null;
            }
        }

        public override void SetRotaryDirection(TriggerMode2DLinear triggerSelector, int index)
        {
            if (triggerSelector == TriggerMode2DLinear.RotaryEncoder)
            {
                SetTriggerSelector(triggerSelector);
                switch (index)
                {
                    case 1:
                        m_AcqDevice.SetFeatureValue("rotaryEncoderDirection", 1);
                        break;
                    case 2:
                        m_AcqDevice.SetFeatureValue("rotaryEncoderDirection", 0);
                        break;
                }
                RotaryDirection = index;
                SettingParams.RotaryDirection = index;
            }
        }

        private void LoadSettings()
        {
            string KeyPath = "Software\\Teledyne DALSA\\" + Application.ProductName + "\\SapAcquisition";
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(KeyPath);
            if (RegKey != null)
            {
                m_ServerName = RegKey.GetValue("Server", "").ToString();
                m_ResourceIndex = (int)RegKey.GetValue("Resource", 0);
                if (File.Exists(RegKey.GetValue("ConfigFile", "").ToString()))
                {
                    m_ConfigFile = RegKey.GetValue("ConfigFile", "").ToString();
                }
            }
        }

        private void SaveSettings()
        {
            string KeyPath = "Software\\Teledyne DALSA\\" + Application.ProductName + "\\SapAcquisition";
            RegistryKey RegKey = Registry.CurrentUser.CreateSubKey(KeyPath);
            RegKey.SetValue("Server", m_ServerName);
            RegKey.SetValue("ConfigFile", m_ConfigFile);
            RegKey.SetValue("Resource", m_ResourceIndex);
        }

        public void Snap()
        {
            try
            {
                if (!m_Xfer.Snap())
                {
                    LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 采集图像失败！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CheckSettings()
        {
            if (m_ServerCategory == ServerCategory.ServerAll)
            {
                if (DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(new DALSA.SaperaLT.SapClassBasic.SapLocation(m_ServerName, m_ResourceIndex), DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.Acq) > 0)
                {
                    m_ServerCategory = ServerCategory.ServerAcq;
                }
                else if (DALSA.SaperaLT.SapClassBasic.SapManager.GetResourceCount(new DALSA.SaperaLT.SapClassBasic.SapLocation(m_ServerName, m_ResourceIndex), DALSA.SaperaLT.SapClassBasic.SapManager.ResourceType.AcqDevice) > 0)
                {
                    m_ServerCategory = ServerCategory.ServerAcqDevice;
                }
            }
            SaveSettings();
        }

        public override void SetTriggerSelector(TriggerMode2DLinear triggerSelector)
        {
            triggerSelectorMode = triggerSelector;
            switch (triggerSelector)
            {
                case TriggerMode2DLinear.Time_Line1:
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 0);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSource", 6);
                    m_AcqDevice.SetFeatureValue("timerSelector", 0);
                    m_AcqDevice.SetFeatureValue("timerMode", 0);
                    m_AcqDevice.SetFeatureValue("timerStartSource", 25);
                    m_AcqDevice.SetFeatureValue("timerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 1);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 2);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 3);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 4);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    break;
                case TriggerMode2DLinear.Time_Software:
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 0);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSource", 16);
                    m_AcqDevice.SetFeatureValue("timerSelector", 0);
                    m_AcqDevice.SetFeatureValue("timerMode", 0);
                    m_AcqDevice.SetFeatureValue("timerStartSource", 25);
                    m_AcqDevice.SetFeatureValue("timerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 1);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 2);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 3);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 4);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    break;
                case TriggerMode2DLinear.RotaryEncoder:
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 1);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSource", 10);
                    m_AcqDevice.SetFeatureValue("timerSelector", 0);
                    m_AcqDevice.SetFeatureValue("timerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 0);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 2);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 3);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 4);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    break;
                case TriggerMode2DLinear.Test_Software:
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 0);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 1);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 2);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 3);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 4);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    break;
                case TriggerMode2DLinear.RotaryEncoder_Hardware:
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 1);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSource", 10);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 0);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 1);
                    m_AcqDevice.SetFeatureValue("TriggerSource", 8);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 2);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 3);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    m_AcqDevice.SetFeatureValue("TriggerSelector", 4);
                    m_AcqDevice.SetFeatureValue("TriggerMode", 0);
                    break;
            }
            SettingParams.TriggerMode = (int)triggerSelector;
        }

        public override int SoftwareTriggerOnce()
        {
            acqOk = false;
            bStopFlag = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            Snap();
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
                if (!bStopFlag)
                {
                    Stop_Grab(state: true);
                }
                if (timeSpan.TotalMilliseconds > timeout)
                {
                    LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 采集时间超时！");
                }
            });
            if (acqOk)
            {
                return 0;
            }
            return -1;
        }

        public override int Start_Grab(bool state)
        {
            try
            {
                if (state && !m_Xfer.Grab())
                {
                    LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 开始采集失败！");
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override int Stop_Grab(bool state)
        {
            bStopFlag = true;
            try
            {
                if (state)
                {
                    if (m_Xfer.Freeze())
                    {
                        return 0;
                    }
                    LogUtil.LogError("DalsaLineScan(" + cameraSN + ") 停止采集失败！");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return -1;
        }

        public override void DestroyObjects()
        {
            if (m_Xfer != null && m_Xfer.Initialized)
            {
                m_Xfer.Destroy();
            }
            if (m_View != null && m_View.Initialized)
            {
                m_View.Destroy();
            }
            if (m_Buffers != null && m_Buffers.Initialized)
            {
                m_Buffers.Destroy();
            }
            camErrCode = CamErrCode.ConnectFailed;
            CameraOperator.camera2DLineCollection.Remove(cameraSN);
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(cameraSN, false);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
        }
    }
}
