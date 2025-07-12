using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IKapBoardClassLibrary;
using NovaVision.BaseClass;

namespace NovaVision.Hardware.C_2DGigeLineScan.SDK_IKapLineScanTool
{
    public class Camera_IKapLineScan : CameraLine2DBase
    {
        private delegate void IKapCallBackProc(IntPtr pParam);

        private IntPtr m_hBoard = new IntPtr(-1);

        private BmpImage m_bmpImage = new BmpImage();

        private static uint nPCIeDevCount = 0u;

        public static Dictionary<string, string> IkapList = new Dictionary<string, string>();

        private uint SNSequence = 0u;

        private int ret = 1;

        private int result = -1;

        private int kapSerialPortBautRate = 9600;

        private string kapSerialPortName = "COM13";

        private IKapCallBackProc OnFrameReadyProc;

        private int mIndex = 0;

        private bool bSuccess = false;

        private string strRead = "";

        private string[] strReadItem;

        private string strWrite = "";

        private SerialPortControl port = new SerialPortControl();

        public TriggerMode2DLinear TriggerMode
        {
            get
            {
                int ret = 0;
                int nValue = 0;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435484u, ref nValue);
                CheckIKapBoard(ret);
                if (nValue == 0)
                {
                    triggerSelectorMode = TriggerMode2DLinear.Time_Software;
                }
                else
                {
                    ret = IKapBoard.IKapGetInfo(m_hBoard, 268435485u, ref nValue);
                    CheckIKapBoard(ret);
                    if (nValue == 2)
                    {
                        triggerSelectorMode = TriggerMode2DLinear.RotaryEncoder;
                    }
                    else
                    {
                        triggerSelectorMode = TriggerMode2DLinear.Time_Line1;
                    }
                }
                return triggerSelectorMode;
            }
        }

        public override float Exposure
        {
            get
            {
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return 0f;
                }
                bSuccess = port.WriteDataToPort("texp");
                if (bSuccess)
                {
                    strRead = port.ReadDataFromPort(4000);
                    strReadItem = strRead.Split('\r');
                }
                double.TryParse(strReadItem[0], out var nValue);
                exposure = (int)nValue;
                SettingParams.ExposureTime = (int)exposure;
                return exposure;
            }
            set
            {
                if (exposure == (float)(long)value)
                {
                    return;
                }
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return;
                }
                strWrite = "texp=" + value;
                bSuccess = port.WriteDataToPort(strWrite, 2000);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):设置曝光失败！");
                    return;
                }
                exposure = value;
                SettingParams.ExposureTime = (int)exposure;
            }
        }

        public override float Gain
        {
            get
            {
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return 0;
                }
                bSuccess = port.WriteDataToPort("pagn");
                if (bSuccess)
                {
                    strRead = port.ReadDataFromPort(4000);
                    strReadItem = strRead.Split('\r');
                }
                double.TryParse(strReadItem[0], out var nValue);
                gain = (int)nValue;
                SettingParams.Gain = (int)gain;
                return gain;
            }
            set
            {
                if (value > 2 && value < 0)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):增益超出范围");
                }
                else
                {
                    if (gain == (long)value)
                    {
                        return;
                    }
                    bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                    if (!bSuccess)
                    {
                        LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                        return;
                    }
                    strWrite = "pagn=" + value;
                    bSuccess = port.WriteDataToPort(strWrite, 2000);
                    if (!bSuccess)
                    {
                        LogUtil.LogError("IKapLineScan(" + cameraSN + "):设置增益失败！");
                        return;
                    }
                    gain = value;
                    SettingParams.Gain = (int)gain;
                }
            }
        }

        public override long ScanWidth
        {
            get
            {
                int ret = 0;
                int nValue = 0;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435457u, ref nValue);
                CheckIKapBoard(ret);
                scanWidth = nValue;
                SettingParams.ScanWidth = (int)scanWidth;
                return scanWidth;
            }
            set
            {
                if (scanWidth != value)
                {
                    int nvalue = (int)value;
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435457u, nvalue);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435467u, 5);
                    CheckIKapBoard(ret);
                    scanWidth = nvalue;
                    SettingParams.ScanWidth = (int)scanWidth;
                    CreateBuffer();
                }
            }
        }

        public override long ScanHeight
        {
            get
            {
                int ret = 0;
                int nValue = 0;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435458u, ref nValue);
                CheckIKapBoard(ret);
                scanHeight = nValue;
                SettingParams.ScanHeight = (int)scanHeight;
                return scanHeight;
            }
            set
            {
                if (scanHeight != value)
                {
                    int nvalue = (int)value;
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435458u, nvalue);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435467u, 5);
                    CheckIKapBoard(ret);
                    scanHeight = nvalue;
                    SettingParams.ScanHeight = (int)scanHeight;
                    CreateBuffer();
                }
            }
        }

        public override long OffsetX
        {
            get
            {
                int ret = 0;
                int nValue = 0;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435536u, ref nValue);
                CheckIKapBoard(ret);
                offsetX = nValue;
                SettingParams.OffsetX = (int)offsetX;
                return offsetX;
            }
            set
            {
                if (offsetX != value)
                {
                    int nvalue = (int)value;
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435536u, nvalue);
                    CheckIKapBoard(ret);
                    offsetX = nvalue;
                    SettingParams.OffsetX = (int)offsetX;
                }
            }
        }

        public override long AcqLineRate
        {
            get
            {
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return 0L;
                }
                bSuccess = port.WriteDataToPort("tprd");
                if (bSuccess)
                {
                    strRead = port.ReadDataFromPort(4000);
                    strReadItem = strRead.Split('\r');
                }
                double.TryParse(strReadItem[0], out var nValue);
                acqLineRate = (long)(1000000.0 / nValue);
                SettingParams.AcqLineRate = (int)acqLineRate;
                return acqLineRate;
            }
            set
            {
                if (acqLineRate == value)
                {
                    return;
                }
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return;
                }
                strWrite = "tprd=" + 1000000 / value;
                bSuccess = port.WriteDataToPort(strWrite, 2000);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):设置行频失败！");
                    return;
                }
                SettingParams.AcqLineRate = (int)acqLineRate;
                acqLineRate = value;
            }
        }

        public override long TapNum
        {
            get
            {
                int ret = 0;
                int nValue = 0;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435475u, ref nValue);
                CheckIKapBoard(ret);
                tapNum = nValue;
                SettingParams.TapNum = (int)tapNum;
                return tapNum;
            }
            set
            {
                if (tapNum != value)
                {
                    int nvalue = (int)value;
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435475u, nvalue);
                    CheckIKapBoard(ret);
                    tapNum = nvalue;
                    SettingParams.TapNum = (int)tapNum;
                }
            }
        }

        public override long LinePeriod
        {
            get
            {
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return 30L;
                }
                bSuccess = port.WriteDataToPort("tprd");
                if (bSuccess)
                {
                    strRead = port.ReadDataFromPort(4000);
                    strReadItem = strRead.Split('\r');
                }
                double.TryParse(strReadItem[0], out var nValue);
                linePeriod = (int)nValue;
                SettingParams.LinePeriod = (int)linePeriod;
                return linePeriod;
            }
            set
            {
                if (linePeriod == value)
                {
                    return;
                }
                bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                    return;
                }
                strWrite = "tprd=" + value;
                bSuccess = port.WriteDataToPort(strWrite, 2000);
                if (!bSuccess)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):设置线周期失败！");
                    return;
                }
                linePeriod = value;
                SettingParams.LinePeriod = (int)linePeriod;
            }
        }

        private static int CheckIKapBoard(int ret)
        {
            if (ret != 1)
            {
                string sErrMsg = "";
                IKapBoard.IKAPERRORINFO tIKei = default(IKapBoard.IKAPERRORINFO);
                IKapBoard.IKapGetLastError(ref tIKei, bErrorReset: true);
                sErrMsg = "IKapLineScan:Error" + sErrMsg + "Board Type\t = 0x" + tIKei.uBoardType.ToString("X4") + "\n" + "Board Index\t = 0x" + tIKei.uBoardIndex.ToString("X4") + "\n" + "Error Code\t = 0x" + tIKei.uErrorCode.ToString("X4") + "\n";
                LogUtil.LogError(sErrMsg);
                return -1;
            }
            return 0;
        }

        public Camera_IKapLineScan(string externSN)
        {
            cameraSN = externSN;
            VendorName = "IKap";
            DeviceIP = "0.0.0.0";
        }

        public static void EnumCamera()
        {
            try
            {
                IkapList.Clear();
                int ret = 1;
                uint resourceNameSize = 0u;
                IKapBoard.IKAPERRORINFO tIKei = default(IKapBoard.IKAPERRORINFO);
                ret = IKapBoard.IKapGetBoardCount(2u, ref nPCIeDevCount);
                CheckIKapBoard(ret);
                if (nPCIeDevCount >= 0)
                {
                    for (uint i = 0u; i < nPCIeDevCount; i++)
                    {
                        resourceNameSize = 0u;
                        StringBuilder resourceName = new StringBuilder(0);
                        IKapBoard.IKapGetBoardName(2u, i, resourceName, ref resourceNameSize);
                        IKapBoard.IKapGetLastError(ref tIKei, bErrorReset: true);
                        if (tIKei.uErrorCode == 29)
                        {
                            resourceName = new StringBuilder((int)resourceNameSize);
                            IKapBoard.IKapGetBoardName(2u, i, resourceName, ref resourceNameSize);
                        }
                        IKapBoard.IKapGetLastError(ref tIKei, bErrorReset: true);
                        if (tIKei.uErrorCode == 1)
                        {
                            IkapList.Add("IKap-" + i, "IKap-" + i);
                        }
                    }
                }
                else
                {
                    LogUtil.LogError("未查找到IKap相机");
                }
            }
            catch (Exception)
            {
            }
        }

        public override int OpenCamera()
        {
            try
            {
                string SnInt = cameraSN.Substring(5);
                SNSequence = (uint)Convert.ToInt32(SnInt);
                m_hBoard = IKapBoard.IKapOpen(2u, SNSequence);
                if (m_hBoard.Equals(-1))
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + "):Open device failure");
                    return -1;
                }
                ret = IKapBoard.IKapSetInfo(m_hBoard, 268435467u, 5);
                CheckIKapBoard(ret);
                int grab_mode = 1;
                ret = IKapBoard.IKapSetInfo(m_hBoard, 268435470u, grab_mode);
                CheckIKapBoard(ret);
                GetCameraInfo();
                OnFrameReadyProc = OnFrameReadyFunc;
                ret = IKapBoard.IKapRegisterCallback(m_hBoard, 1u, Marshal.GetFunctionPointerForDelegate(OnFrameReadyProc), m_hBoard);
                CheckIKapBoard(ret);
                CreateBuffer();
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
                isConnected = false;
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
        }

        private void GetCameraInfo()
        {
            int ret = 0;
            int nValue = 0;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435463u, ref nValue);
            CheckIKapBoard(ret);
            Version = nValue.ToString();
            triggerSelectorMode = TriggerMode;
            ret = IKapBoard.IKapGetSerialPort(m_hBoard, ref nValue);
            CheckIKapBoard(ret);
            kapSerialPortName = "COM" + nValue;
        }

        private void CreateBuffer()
        {
            int ret = 0;
            int nValue = 0;
            int nWidth = 0;
            int nHeight = 0;
            int nChannels = 3;
            int nDepth = 8;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435457u, ref nWidth);
            CheckIKapBoard(ret);
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435458u, ref nHeight);
            CheckIKapBoard(ret);
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435459u, ref nValue);
            CheckIKapBoard(ret);
            nDepth = ((nValue != 8) ? 16 : 8);
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435466u, ref nValue);
            CheckIKapBoard(ret);
            switch (nValue)
            {
                case 0:
                    nChannels = 1;
                    break;
                case 1:
                case 3:
                    nChannels = 3;
                    break;
                case 2:
                case 4:
                    nChannels = 4;
                    break;
            }
            if (!m_bmpImage.CreateImage(nWidth, nHeight, nDepth, nChannels))
            {
                LogUtil.LogError("IKapLineScan(" + cameraSN + "):创建图像失败");
            }
        }

        public void OnFrameReadyFunc(IntPtr pParam)
        {
            IntPtr pUserBuffer = IntPtr.Zero;
            int nFrameSize = 0;
            int nFrameCount = 0;
            IKapBoard.IKAPBUFFERSTATUS status = default(IKapBoard.IKAPBUFFERSTATUS);
            IKapBoard.IKapGetInfo(pParam, 268435467u, ref nFrameCount);
            IKapBoard.IKapGetBufferStatus(pParam, mIndex, ref status);
            if (status.uFull == 1)
            {
                IKapBoard.IKapGetInfo(pParam, 268435465u, ref nFrameSize);
                IKapBoard.IKapGetBufferAddress(pParam, mIndex, ref pUserBuffer);
                Bitmap img = m_bmpImage.WriteImageData(pUserBuffer, nFrameSize);
                Cognex.VisionPro.ICogImage image = ((img.PixelFormat != PixelFormat.Format8bppIndexed) ? ImageData.GetOutputRGBImage(img) : ImageData.GetOutputImage(img));
                ImageData imaData = new ImageData(image);
                if (UpdateImage != null)
                {
                    UpdateImage(imaData);
                }
                GC.Collect();
            }
            mIndex++;
            mIndex %= nFrameCount;
            if (status.uFull == 1)
            {
                acqOk = true;
            }
        }

        public override void SetRotaryDirection(TriggerMode2DLinear triggerSelector, int index)
        {
            if (triggerSelector != TriggerMode2DLinear.RotaryEncoder)
            {
                return;
            }
            SetTriggerSelector(triggerSelector);
            bSuccess = port.OpenPort(kapSerialPortName, kapSerialPortBautRate);
            if (!bSuccess)
            {
                LogUtil.LogError("IKapLineScan(" + cameraSN + "):打开串口失败");
                return;
            }
            switch (index)
            {
                case 1:
                    strWrite = "vdir=0";
                    bSuccess = port.WriteDataToPort(strWrite, 2000);
                    if (!bSuccess)
                    {
                        LogUtil.LogError("IKapLineScan(" + cameraSN + "):设置扫描方向从上到下失败！");
                        return;
                    }
                    break;
                case 2:
                    strWrite = "vdir=1";
                    bSuccess = port.WriteDataToPort(strWrite, 2000);
                    if (!bSuccess)
                    {
                        LogUtil.LogError("IKapLineScan(" + cameraSN + "):设置扫描方向从下到上失败！");
                        return;
                    }
                    break;
            }
            RotaryDirection = index;
            SettingParams.RotaryDirection = index;
        }

        public override void SetTriggerSelector(TriggerMode2DLinear triggerSelector)
        {
            triggerSelectorMode = triggerSelector;
            int ret = 0;
            switch (triggerSelector)
            {
                case TriggerMode2DLinear.Time_Line1:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 1);
                    CheckIKapBoard(ret);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435485u, 0);
                    CheckIKapBoard(ret);
                    break;
                case TriggerMode2DLinear.Time_Software:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 0);
                    CheckIKapBoard(ret);
                    break;
                case TriggerMode2DLinear.RotaryEncoder:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 1);
                    CheckIKapBoard(ret);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435485u, 2);
                    CheckIKapBoard(ret);
                    break;
                case TriggerMode2DLinear.Test_Software:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 0);
                    CheckIKapBoard(ret);
                    break;
                case TriggerMode2DLinear.RotaryEncoder_Hardware:
                    {
                        SetFrameTrigger();
                        int transfer_mode = 2;
                        ret = IKapBoard.IKapSetInfo(m_hBoard, 268435468u, transfer_mode);
                        CheckIKapBoard(ret);
                        break;
                    }
            }
            SettingParams.TriggerMode = (int)triggerSelector;
        }

        public override int SoftwareTriggerOnce()
        {
            mIndex = 0;
            ret = IKapBoard.IKapStartGrab(m_hBoard, 1);
            result = CheckIKapBoard(ret);
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
                if (!bStopFlag)
                {
                    Stop_Grab(sate: true);
                }
                if (timeSpan.TotalMilliseconds > timeout)
                {
                    LogUtil.LogError("IKapLineScan(" + cameraSN + ") 采集时间超时！");
                }
            });
            return result;
        }

        public override int Start_Grab(bool sate)
        {
            mIndex = 0;
            if (triggerSelectorMode == TriggerMode2DLinear.RotaryEncoder || triggerSelectorMode == TriggerMode2DLinear.Time_Software)
            {
                return 0;
            }
            ret = IKapBoard.IKapStartGrab(m_hBoard, 0);
            CheckIKapBoard(ret);
            return 0;
        }

        public override int Stop_Grab(bool sate)
        {
            mIndex = 0;
            bStopFlag = true;
            ret = IKapBoard.IKapStopGrab(m_hBoard);
            result = CheckIKapBoard(ret);
            return result;
        }

        public override void DestroyObjects()
        {
            ret = IKapBoard.IKapUnRegisterCallback(m_hBoard, 1u);
            if (!m_hBoard.Equals(-1))
            {
                IKapBoard.IKapClose(m_hBoard);
                m_hBoard = (IntPtr)(-1);
            }
            m_bmpImage.ReleaseImage();
            camErrCode = CamErrCode.ConnectFailed;
            CameraOperator.camera2DLineCollection.Remove(cameraSN);
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(cameraSN, false);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
        }

        private void SetFrameTrigger()
        {
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 1);
            CheckIKapBoard(ret);
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435485u, 0);
            CheckIKapBoard(ret);
        }

        private void SetLineTrigger()
        {
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 1);
            CheckIKapBoard(ret);
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435495u, 5);
            CheckIKapBoard(ret);
        }
    }
}
