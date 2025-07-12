using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using IKapBoardClassLibrary;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_._03_IKap
{
    internal delegate void IKapCallBackProc(IntPtr pParam);
    public delegate void IkapImageArrived(object sender, IKapImageData image);
    public class ItekVulcan
    {
        public IntPtr m_hBoard = new IntPtr(-1);

        private string m_SerialNum = "";

        private string version = "";

        private string portName = "";

        private int m_ImageWidth;

        private int m_ImageHeight;

        private int mDepth;

        private int mChannels;

        private int mImageType = 1;

        private int m_nCurFrameIndex = 0;

        private int m_nTotalFrameCount = 5;

        private bool isStartGrab = false;

        private bool isCreated = false;

        private IKapCallBackProc OnFrameReadyProc;

        public static List<string> boardNames;

        public static Dictionary<string, ItekVulcan> dicBoards;

        public bool IsStartGrab => isStartGrab;

        public int TotalFrameCount => m_nTotalFrameCount;

        public bool IsCreated => isCreated;

        public string PortName => portName;

        public event IkapImageArrived ImageArrivd;

        static ItekVulcan()
        {
            boardNames = new List<string>();
            dicBoards = new Dictionary<string, ItekVulcan>();
        }

        public ItekVulcan(string SN)
        {
            m_SerialNum = SN;
        }

        private static int CheckIKapBoard(int ret, Action<string> showMsg)
        {
            if (ret != 1)
            {
                string sErrMsg = "";
                IKapBoard.IKAPERRORINFO tIKei = default(IKapBoard.IKAPERRORINFO);
                IKapBoard.IKapGetLastError(ref tIKei, bErrorReset: true);
                sErrMsg = "Error" + sErrMsg + "Board Type\t = 0x" + tIKei.uBoardType.ToString("X4") + "\n" + "Board Index\t = 0x" + tIKei.uBoardIndex.ToString("X4") + "\n" + "Error Code\t = 0x" + tIKei.uErrorCode.ToString("X4") + "\n";
                showMsg?.Invoke(sErrMsg);
                return -1;
            }
            return 0;
        }

        public static void EnumIkapBoards(Action<string> showMsg)
        {
            boardNames.Clear();
            int ret = 1;
            uint nPCIeDevCount = 0u;
            string sMesg = "";
            uint resourceNameSize = 0u;
            IKapBoard.IKAPERRORINFO tIKei = default(IKapBoard.IKAPERRORINFO);
            ret = IKapBoard.IKapGetBoardCount(2u, ref nPCIeDevCount);
            CheckIKapBoard(ret, showMsg);
            if (nPCIeDevCount == 0)
            {
                showMsg?.Invoke("Get board count 0");
            }
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
                if (tIKei.uErrorCode != 1)
                {
                    sMesg = "Get Device Name Fail. Error Code:" + tIKei.uErrorCode;
                }
                else
                {
                    sMesg = resourceName.ToString();
                    if (!boardNames.Contains(sMesg))
                    {
                        boardNames.Add(sMesg);
                    }
                }
                showMsg?.Invoke(sMesg);
            }
        }

        public bool OpenCard(string configFileName, Action preSetting, Action<string> showMsg)
        {
            bool b_ret = false;
            int ret = 1;
            uint resourceIndex = Convert.ToUInt32(m_SerialNum.Substring(m_SerialNum.Length - 1));
            m_hBoard = IKapBoard.IKapOpen(2u, resourceIndex);
            if (m_hBoard.Equals(-1))
            {
                showMsg?.Invoke("Open device(" + m_SerialNum + ") failure");
                return b_ret;
            }
            int nValue = 0;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435463u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            version = $"Ver {nValue}";
            ret = IKapBoard.IKapGetSerialPort(m_hBoard, ref nValue);
            CheckIKapBoard(ret, showMsg);
            portName = $"COM{nValue}";
            if (configFileName != null)
            {
                ret = IKapBoard.IKapLoadConfigurationFromFile(m_hBoard, configFileName);
                CheckIKapBoard(ret, showMsg);
            }
            else
            {
                showMsg?.Invoke("Fail to get configuration, using default setting!");
            }
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435467u, m_nTotalFrameCount);
            CheckIKapBoard(ret, showMsg);
            int grab_mode = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435470u, grab_mode);
            CheckIKapBoard(ret, showMsg);
            isCreated = true;
            OnFrameReadyProc = OnFrameReadyFunc;
            ret = IKapBoard.IKapRegisterCallback(m_hBoard, 1u, Marshal.GetFunctionPointerForDelegate(OnFrameReadyProc), m_hBoard);
            CheckIKapBoard(ret, showMsg);
            preSetting?.Invoke();
            GetImageInfos(showMsg);
            if (dicBoards.ContainsKey(m_SerialNum))
            {
                dicBoards[m_SerialNum] = this;
            }
            else
            {
                dicBoards.Add(m_SerialNum, this);
            }
            return true;
        }

        public void CloseCard(Action<string> showMsg)
        {
            if (isStartGrab)
            {
                StopGrab(showMsg);
            }
            int ret = 1;
            ret = IKapBoard.IKapUnRegisterCallback(m_hBoard, 1u);
            CheckIKapBoard(ret, showMsg);
            if (!m_hBoard.Equals(-1))
            {
                IKapBoard.IKapClose(m_hBoard);
                m_hBoard = (IntPtr)(-1);
            }
            dicBoards.Remove(m_SerialNum);
            isCreated = false;
            GC.SuppressFinalize(this);
        }

        private void OnFrameReadyFunc(IntPtr pParam)
        {
            IntPtr pUserBuffer = IntPtr.Zero;
            int nFrameSize = 0;
            int nFrameCount = 0;
            IKapBoard.IKAPBUFFERSTATUS status = default(IKapBoard.IKAPBUFFERSTATUS);
            IKapBoard.IKapGetInfo(pParam, 268435467u, ref nFrameCount);
            IKapBoard.IKapGetBufferStatus(pParam, m_nCurFrameIndex, ref status);
            if (status.uFull == 1)
            {
                IKapBoard.IKapGetInfo(pParam, 268435465u, ref nFrameSize);
                IKapBoard.IKapGetBufferAddress(pParam, m_nCurFrameIndex, ref pUserBuffer);
                byte[] imageBuffer = new byte[m_ImageHeight * m_ImageWidth * mChannels];
                Marshal.Copy(pUserBuffer, imageBuffer, 0, imageBuffer.Length);
                byte[] tempR = new byte[m_ImageHeight * m_ImageWidth];
                byte[] tempG = new byte[m_ImageHeight * m_ImageWidth];
                byte[] tempB = new byte[m_ImageHeight * m_ImageWidth];
                for (int i = 0; i < m_ImageHeight * m_ImageWidth * mChannels; i += mChannels)
                {
                    tempB[i / mChannels] = imageBuffer[i];
                    tempG[i / mChannels] = imageBuffer[i + 1];
                    tempR[i / mChannels] = imageBuffer[i + 2];
                }
                Buffer.BlockCopy(tempR, 0, imageBuffer, 0, tempR.Length);
                Buffer.BlockCopy(tempG, 0, imageBuffer, m_ImageHeight * m_ImageWidth, tempG.Length);
                Buffer.BlockCopy(tempB, 0, imageBuffer, m_ImageHeight * m_ImageWidth * 2, tempB.Length);
                if (this.ImageArrivd != null)
                {
                    IKapImageData ImageData = new IKapImageData
                    {
                        Width = m_ImageWidth,
                        Height = m_ImageHeight,
                        Stride = m_ImageWidth * mChannels,
                        Data = imageBuffer,
                        ImageType = mImageType
                    };
                    this.ImageArrivd(this, ImageData);
                }
                GC.Collect();
            }
            m_nCurFrameIndex++;
            m_nCurFrameIndex %= m_nTotalFrameCount;
        }

        public void GetImageInfos(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435457u, ref m_ImageWidth);
            CheckIKapBoard(ret, showMsg);
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435458u, ref m_ImageHeight);
            CheckIKapBoard(ret, showMsg);
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435459u, ref mDepth);
            CheckIKapBoard(ret, showMsg);
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435466u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            switch (nValue)
            {
                case 0:
                    mChannels = 1;
                    break;
                case 1:
                case 3:
                    mChannels = 3;
                    break;
                case 2:
                case 4:
                    mChannels = 4;
                    break;
            }
            switch (mChannels)
            {
                case 1:
                    mImageType = 1;
                    break;
                case 3:
                    mImageType = 3;
                    break;
                case 4:
                    mImageType = 4;
                    break;
                case 2:
                    break;
            }
        }

        public void StartGrab(Action<string> showMsg)
        {
            m_nCurFrameIndex = 0;
            int ret = 1;
            ret = IKapBoard.IKapStartGrab(m_hBoard, 0);
            if (CheckIKapBoard(ret, showMsg) == 0)
            {
                isStartGrab = true;
            }
            else
            {
                isStartGrab = false;
            }
        }

        public void StopGrab(Action<string> showMsg)
        {
            int ret = 1;
            ret = IKapBoard.IKapStopGrab(m_hBoard);
            if (CheckIKapBoard(ret, showMsg) == 0)
            {
                isStartGrab = false;
            }
            else
            {
                isStartGrab = true;
            }
        }

        public void Snap(Action<string> showMsg)
        {
            m_nCurFrameIndex = 0;
            int ret = 1;
            ret = IKapBoard.IKapStartGrab(m_hBoard, 1);
            isStartGrab = true;
            CheckIKapBoard(ret, showMsg);
        }

        public void SetScanType(int type, Action<string> showMsg)
        {
            if (type != 0 && type != 1)
            {
                showMsg?.Invoke("输入的type值不满足要求！");
                return;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435462u, type);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetImageType(int imageType, Action<string> showMsg)
        {
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435466u, imageType);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetImageOffsetX(ref int imageOffsetX, Action<string> showMsg)
        {
            if (imageOffsetX % 16 != 0)
            {
                showMsg?.Invoke("ImageOffsetX值必须是16的倍数");
                imageOffsetX = 0;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435536u, imageOffsetX);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetDataFormat(int dataFormat, Action<string> showMsg)
        {
            if (dataFormat != 8 && dataFormat != 10 && dataFormat != 12 && dataFormat != 14 && dataFormat != 16)
            {
                showMsg?.Invoke("输入数据格式不满足要求！");
                return;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435459u, dataFormat);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetImageHeight(ref int height, Action<string> showMsg)
        {
            int ret = 1;
            if (height < 1000)
            {
                height = 1000;
            }
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435458u, height);
            m_ImageHeight = height;
            CheckIKapBoard(ret, showMsg);
        }

        public void SetImageWidth(ref int width, Action<string> showMsg)
        {
            int tapNum = GetTapNum(showMsg);
            if (tapNum == 1 && width % 1 != 0)
            {
                showMsg?.Invoke("输入的Width必须是1的倍数！");
                return;
            }
            if (tapNum == 2 && width % 2 != 0)
            {
                showMsg?.Invoke("输入的Width必须是2的倍数！");
                return;
            }
            if (tapNum == 3 && width % 10 != 0)
            {
                showMsg?.Invoke("输入的Width必须是10的倍数！");
                return;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435457u, width);
            m_ImageWidth = width;
            CheckIKapBoard(ret, showMsg);
        }

        public void SetTapNum(ref int tap, Action<string> showMsg)
        {
            if (tap < 1)
            {
                tap = 1;
            }
            else if (tap > 3)
            {
                tap = 3;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435475u, tap);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetMultiplier(ref int multiplier, Action<string> showMsg)
        {
            if (multiplier < 0 || multiplier > 5)
            {
                showMsg?.Invoke("输入的倍频索引不满足要求！");
                return;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435524u, multiplier);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetDivider(ref int divider, Action<string> showMsg)
        {
            if (divider < 0 || divider > 255)
            {
                showMsg?.Invoke("输入的Divider不满足要求！");
                return;
            }
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435492u, divider);
            CheckIKapBoard(ret, showMsg);
        }

        public void SetBufferFrameCount(int frameCount, Action<string> showMsg)
        {
            int ret = 1;
            ret = IKapBoard.IKapSetInfo(m_hBoard, 268435467u, frameCount);
            m_nTotalFrameCount = frameCount;
            CheckIKapBoard(ret, showMsg);
        }

        public void SetWorkMode(int workMode, Action<string> showMsg)
        {
            int ret = 1;
            switch ((byte)workMode)
            {
                case 1:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 0);
                    CheckIKapBoard(ret, showMsg);
                    break;
                case 2:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435495u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435496u, 9000);
                    CheckIKapBoard(ret, showMsg);
                    break;
                case 3:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435485u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435537u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435538u, 1000);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435495u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435496u, 9000);
                    CheckIKapBoard(ret, showMsg);
                    break;
                case 4:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435495u, 5);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435502u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435503u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435504u, 5);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435507u, 0);
                    CheckIKapBoard(ret, showMsg);
                    break;
                case 5:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435485u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435537u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435538u, 1000);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435495u, 5);
                    CheckIKapBoard(ret, showMsg);
                    break;
                case 6:
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435484u, 0);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435480u, 1);
                    CheckIKapBoard(ret, showMsg);
                    ret = IKapBoard.IKapSetInfo(m_hBoard, 268435495u, 5);
                    CheckIKapBoard(ret, showMsg);
                    break;
            }
        }

        public int GetScanType(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435462u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetImageType(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435466u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetImageOffsetX(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435536u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetDataFormat(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435459u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetImageHeight(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435458u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetImageWidth(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435457u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetTapNum(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435475u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetMultiplierIndex(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435524u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetDivider(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435492u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetBufferFrameCount(Action<string> showMsg)
        {
            int nValue = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435467u, ref nValue);
            CheckIKapBoard(ret, showMsg);
            return nValue;
        }

        public int GetWorkMode(Action<string> showMsg)
        {
            int nValue = 0;
            int triggerMode = 0;
            int ret = 1;
            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435484u, ref triggerMode);
            if (triggerMode == 0)
            {
                int sourceCC2 = 0;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435480u, ref sourceCC2);
                switch (sourceCC2)
                {
                    case 0:
                        nValue = 1;
                        break;
                    case 1:
                        {
                            int integrationTriggerSource2 = 0;
                            ret = IKapBoard.IKapGetInfo(m_hBoard, 268435495u, ref integrationTriggerSource2);
                            switch (integrationTriggerSource2)
                            {
                                case 0:
                                    nValue = 2;
                                    break;
                                case 5:
                                    nValue = 4;
                                    break;
                            }
                            break;
                        }
                }
            }
            else
            {
                int sourceCC1 = 0;
                int boardTriggerSource = -1;
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435480u, ref sourceCC1);
                ret = IKapBoard.IKapGetInfo(m_hBoard, 268435485u, ref boardTriggerSource);
                if (sourceCC1 == 1 && boardTriggerSource == 0)
                {
                    int integrationTriggerSource = 0;
                    ret = IKapBoard.IKapGetInfo(m_hBoard, 268435495u, ref integrationTriggerSource);
                    switch (integrationTriggerSource)
                    {
                        case 0:
                            nValue = 3;
                            break;
                        case 5:
                            nValue = 5;
                            break;
                    }
                }
            }
            if (nValue == 0)
            {
                SetWorkMode(1, showMsg);
                nValue = 1;
            }
            return nValue;
        }
    }
}
