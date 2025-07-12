using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;

namespace NovaVision.Hardware.C_2DGigeLineScan.Hikrobot
{
    public class Camera_MVS_GigeLineScan
    {
        public delegate void ImageArrivedEvnetHandler(object sender, ImageData image);

        public delegate void ExceptionEvnetHandler(string message);

        private enum TriggerMode
        {
            Continuous = 1,
            TimeSoftware = 2,
            RotarySoftware = 4,
            TimeHardware = 3,
            RotaryHardware = 5,
            FrameBurst = 6
        }

        private enum TriggerSource
        {
            Line0 = 0,
            Line1 = 1,
            Line3 = 3,
            Line4 = 5,
            EncoderModuleOut = 6,
            Software = 7,
            FrequencyConverter = 8,
            Action1 = 22,
            Anyway = 25
        }

        private enum TriggerSelector
        {
            FrameBurstStart = 6,
            LineStart = 9
        }

        private enum EncoderOutputModeEnum
        {
            AnyDirection,
            ForwardOnly,
            BackwardOnly
        }

        private enum PreampGain
        {
            gain_1200x = 1200,
            gain_2700x = 2700,
            gain_4600x = 4600
        }

        private CCamera _camera = null;

        private string m_SerialNum;

        private bool isOpened = false;

        private cbOutputExdelegate _ImageCallback;

        private cbExceptiondelegate _ExCallback;

        private string _ipAddress = "0.0.0.0";

        private int workMode = 0;

        public int imageCount = 0;

        private int imageCountMax = 0;

        private int _stitchFlag = 0;

        private long g_nPayloadSize = 0L;

        private long m_nRowStep = 0L;

        private object obj = new object();

        public List<Cognex.VisionPro.ICogImage> imageDatas = new List<Cognex.VisionPro.ICogImage>();

        private CogCopyRegionTool imageStitcher;

        public static List<string> SerialNums = new List<string>();

        private static Dictionary<string, CCameraInfo> dicDevices = new Dictionary<string, CCameraInfo>();

        private int nRet;

        public object lock_ImageConvert = new object();

        public int Width
        {
            get
            {
                return GetIntValue("Width");
            }
            set
            {
                SetValue("Width", value);
            }
        }

        public int Height
        {
            get
            {
                return GetIntValue("Height");
            }
            set
            {
                SetValue("Height", value);
            }
        }

        public int OffsetX
        {
            get
            {
                return GetIntValue("OffsetX");
            }
            set
            {
                SetValueEx("OffsetX", value);
            }
        }

        public int AcquisitionBurstFrameCount
        {
            get
            {
                return GetIntValue("AcquisitionBurstFrameCount");
            }
            set
            {
                int enumValue = GetIntOfEnumValue("TriggerSelector");
                if (enumValue == 6)
                {
                    SetValueEx("AcquisitionBurstFrameCount", value);
                }
            }
        }

        public int StitchFlag
        {
            get
            {
                return _stitchFlag;
            }
            set
            {
                _stitchFlag = value;
            }
        }

        public int ImgCount
        {
            get
            {
                return imageCountMax;
            }
            set
            {
                imageCountMax = value;
            }
        }

        public int AcquisitionLineRate
        {
            get
            {
                return GetIntValue("AcquisitionLineRate");
            }
            set
            {
                SetValue("AcquisitionLineRate", value);
            }
        }

        public double ExposureTime
        {
            get
            {
                return GetDoubleValue("ExposureTime");
            }
            set
            {
                SetValue("ExposureTime", value);
            }
        }

        public int ResultingLineRate => GetIntValue("ResultingLineRate");

        public int LineDebouncerTimeNs
        {
            get
            {
                return GetIntValue("LineDebouncerTimeNs");
            }
            set
            {
                SetValue("LineDebouncerTimeNs", value);
            }
        }

        public int PreDivider
        {
            get
            {
                return GetIntValue("PreDivider");
            }
            set
            {
                SetValue("PreDivider", value);
            }
        }

        public int Multiplier
        {
            get
            {
                return GetIntValue("Multiplier");
            }
            set
            {
                SetValue("Multiplier", value);
            }
        }

        public int PostDivider
        {
            get
            {
                return GetIntValue("PostDivider");
            }
            set
            {
                SetValue("PostDivider", value);
            }
        }

        public int EncoderOutputMode
        {
            get
            {
                return GetIntOfEnumValue("EncoderOutputMode");
            }
            set
            {
                EncoderOutputModeEnum encoderOutputModeEnum = (EncoderOutputModeEnum)value;
                SetEnumValue("EncoderOutputMode", encoderOutputModeEnum.ToString());
            }
        }

        public int ScanDirection
        {
            get
            {
                return GetBoolValue("ReverseScanDirection") ? 1 : 0;
            }
            set
            {
                SetEnumValue("TDIMode", 1);
                SetValue("ReverseScanDirection", value == 1);
            }
        }

        public string GainRaw
        {
            get
            {
                int enumValue = GetIntOfEnumValue("PreampGain");
                PreampGain preampGain = (PreampGain)enumValue;
                return preampGain.ToString();
            }
            set
            {
                SetEnumValue("PreampGain", value);
            }
        }

        public double Gamma
        {
            get
            {
                return GetDoubleValue("Gamma");
            }
            set
            {
                SetValue("Gamma", value);
            }
        }

        public string IPAddress => _ipAddress;

        public int WorkMode
        {
            get
            {
                return workMode;
            }
            set
            {
                SetWorkMode((TriggerMode)value);
            }
        }

        public int EncoderCounter => GetIntValue("EncoderCounter");

        public event ImageArrivedEvnetHandler ImageArrivedEvent;

        public event ExceptionEvnetHandler ExceptionEvent;

        private void ExceptionPush(string msg)
        {
            if (this.ExceptionEvent != null)
            {
                this.ExceptionEvent(msg);
            }
        }

        private void ImageArrived(object sender, ImageData image)
        {
            if (this.ImageArrivedEvent != null)
            {
                this.ImageArrivedEvent(sender, image);
            }
        }

        public Camera_MVS_GigeLineScan(string SN)
        {
            m_SerialNum = SN;
        }

        public static void EnumCameras(Action<string> showMsg)
        {
            SerialNums.Clear();
            dicDevices.Clear();
            int nRet = 0;
            List<CCameraInfo> stDevList = new List<CCameraInfo>();
            nRet = CSystem.EnumDevices(1u, ref stDevList);
            if (nRet != 0)
            {
                showMsg?.Invoke($"Enum device failed:{nRet:x8}");
            }
            else
            {
                if (stDevList.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < stDevList.Count; i++)
                {
                    if (1 != stDevList[i].nTLayerType)
                    {
                        continue;
                    }
                    CGigECameraInfo stGigEDeviceInfo = (CGigECameraInfo)stDevList[i];
                    if (stGigEDeviceInfo.chModelName.Substring(0, 5).Equals("MV-CL"))
                    {
                        SerialNums.Add(stGigEDeviceInfo.chSerialNumber);
                        if (dicDevices.ContainsKey(stGigEDeviceInfo.chSerialNumber))
                        {
                            dicDevices[stGigEDeviceInfo.chSerialNumber] = stDevList[i];
                        }
                        else
                        {
                            dicDevices.Add(stGigEDeviceInfo.chSerialNumber, stDevList[i]);
                        }
                    }
                }
            }
        }

        public bool Open()
        {
            bool bRet = false;
            try
            {
                if (!dicDevices.ContainsKey(m_SerialNum) || isOpened)
                {
                    return false;
                }
                int nRet = 0;
                CCameraInfo stDevInfo = dicDevices[m_SerialNum];
                _camera = new CCamera();
                nRet = _camera.CreateHandle(ref stDevInfo);
                if (nRet != 0)
                {
                    ExceptionPush($"Create device failed:{nRet:x8}");
                    return false;
                }
                nRet = _camera.OpenDevice();
                if (nRet != 0)
                {
                    ExceptionPush($"Open device failed:{nRet:x8}");
                    return false;
                }
                CIntValue stParam = new CIntValue();
                nRet = _camera.GetIntValue("PayloadSize", ref stParam);
                if (nRet != 0)
                {
                    ExceptionPush(string.Format("Get PayloadSize Fail", nRet));
                    return false;
                }
                g_nPayloadSize = stParam.CurValue;
                nRet = _camera.GetIntValue("Height", ref stParam);
                if (nRet != 0)
                {
                    ExceptionPush(string.Format("Get Height Fail", nRet));
                    return false;
                }
                long nHeight = stParam.CurValue;
                nRet = _camera.GetIntValue("Width", ref stParam);
                if (nRet != 0)
                {
                    ExceptionPush(string.Format("Get Width Fail", nRet));
                    return false;
                }
                long nWidth = stParam.CurValue;
                if (stDevInfo.nTLayerType == 1)
                {
                    int nPacketSize = _camera.GIGE_GetOptimalPacketSize();
                    if (nPacketSize > 0)
                    {
                        nRet = _camera.SetIntValue("GevSCPSPacketSize", (uint)nPacketSize);
                        if (nRet != 0)
                        {
                            ExceptionPush($"Warning: Set Packet Size failed {nRet:x8}");
                        }
                    }
                    else
                    {
                        ExceptionPush($"Warning: Get Packet Size failed {nPacketSize:x8}");
                    }
                    CGigECameraInfo stGigEDeviceInfo = (CGigECameraInfo)stDevInfo;
                    uint nIp1 = (stGigEDeviceInfo.nCurrentIp & 0xFF000000u) >> 24;
                    uint nIp2 = (stGigEDeviceInfo.nCurrentIp & 0xFF0000) >> 16;
                    uint nIp3 = (stGigEDeviceInfo.nCurrentIp & 0xFF00) >> 8;
                    uint nIp4 = stGigEDeviceInfo.nCurrentIp & 0xFFu;
                    string sn = stGigEDeviceInfo.chSerialNumber;
                    _ipAddress = $"{nIp1}.{nIp2}.{nIp3}.{nIp4}";
                }
                _ExCallback = ExceptionCallBack;
                nRet = _camera.RegisterExceptionCallBack(_ExCallback, IntPtr.Zero);
                if (nRet != 0)
                {
                    ExceptionPush($"Register ExceptionCallBack failed:{nRet:x8}");
                    return false;
                }
                nRet = 0;
                nRet += _camera.SetBoolValue("AcquisitionLineRateEnable", bValue: true);
                int lineRate = ResultingLineRate;
                nRet += _camera.SetIntValue("AcquisitionLineRat", (uint)lineRate);
                m_nRowStep = nWidth * nHeight;
                _ImageCallback = ImageCallbackFunc;
                nRet = _camera.RegisterImageCallBackEx(_ImageCallback, IntPtr.Zero);
                if (nRet != 0)
                {
                    ExceptionPush($"Register image callback failed!");
                    return false;
                }
                if (nRet != 0)
                {
                    ExceptionPush($"Start grabbing failed:{nRet:x8}");
                    return false;
                }
                isOpened = true;
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPush("Error Opening camera:" + ex.Message);
                return false;
            }
        }

        public void Close()
        {
            if (_camera != null)
            {
                _ImageCallback = null;
                _camera.CloseDevice();
                _camera.DestroyHandle();
                isOpened = false;
            }
        }

        public void SoftwareTriggerDevice(object obj = null)
        {
            if (_camera != null)
            {
                int nRet = _camera.SetCommandValue("TriggerSoftware");
                if (nRet != 0)
                {
                    ExceptionPush($"TriggerSoftware execute failed:{nRet:x8}");
                }
            }
        }

        public void StartGrab()
        {
            imageCount = 0;
            if (_camera != null)
            {
                BeforeStartGrab();
                int nRet = _camera.StartGrabbing();
                if (nRet != 0)
                {
                    ExceptionPush($"Start grabbing failed:{nRet:x8}");
                }
            }
        }

        public bool BeforeStartGrab()
        {
            CIntValue stParam = new CIntValue();
            nRet = _camera.GetIntValue("Height", ref stParam);
            if (nRet != 0)
            {
                ExceptionPush(string.Format("Get Height Fail", nRet));
                return false;
            }
            long nHeight = stParam.CurValue;
            nRet = _camera.GetIntValue("Width", ref stParam);
            if (nRet != 0)
            {
                ExceptionPush(string.Format("Get Width Fail", nRet));
                return false;
            }
            long nWidth = stParam.CurValue;
            m_nRowStep = nWidth * nHeight;
            nRet = 0;
            return true;
        }

        public void StopGrab()
        {
            if (_camera != null)
            {
                int nRet = _camera.StopGrabbing();
                if (nRet != 0)
                {
                    ExceptionPush($"Stop grabbing failed:{nRet:x8}");
                }
            }
        }

        private int CvtGvspPixelFormatType(MvGvspPixelType enType)
        {
            int nRet = 0;
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                    nRet = 1;
                    break;
                case MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                    nRet = 3;
                    break;
            }
            return nRet;
        }

        private void ExceptionCallBack(uint nMsgType, IntPtr pUser)
        {
            if (nMsgType == 32769)
            {
                ExceptionPush("Error camera disconnected! ");
                _camera.CloseDevice();
                _camera.DestroyHandle();
                isOpened = false;
            }
        }

        private unsafe void ImageCallbackFunc(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            imageCount++;
            IntPtr pImageBuffer = Marshal.AllocHGlobal((int)m_nRowStep * 3);
            if (pImageBuffer == IntPtr.Zero)
            {
                return;
            }
            IntPtr pTemp = IntPtr.Zero;
            byte[] byteArrImageData = new byte[m_nRowStep * 3];
            MvGvspPixelType pixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            if (IsColorPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToRGB(obj, pData, ref pFrameInfo, pImageBuffer);
                    if (nRet != 0)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }
                byte* pBufForSaveImage2 = (byte*)(void*)pTemp;
                uint nSupWidth2 = (uint)(pFrameInfo.nWidth + 3) & 0xFFFFFFFCu;
                for (int nRow2 = 0; nRow2 < pFrameInfo.nHeight; nRow2++)
                {
                    for (int col2 = 0; col2 < pFrameInfo.nWidth; col2++)
                    {
                        byteArrImageData[nRow2 * nSupWidth2 + col2] = pBufForSaveImage2[nRow2 * pFrameInfo.nWidth * 3 + 3 * col2];
                        byteArrImageData[pFrameInfo.nWidth * pFrameInfo.nHeight + nRow2 * nSupWidth2 + col2] = pBufForSaveImage2[nRow2 * pFrameInfo.nWidth * 3 + (3 * col2 + 1)];
                        byteArrImageData[pFrameInfo.nWidth * pFrameInfo.nHeight * 2 + nRow2 * nSupWidth2 + col2] = pBufForSaveImage2[nRow2 * pFrameInfo.nWidth * 3 + (3 * col2 + 2)];
                    }
                }
                pTemp = Marshal.UnsafeAddrOfPinnedArrayElement(byteArrImageData, 0);
            }
            else if (IsHBColorPixelFormat(pFrameInfo.enPixelType))
            {
                nRet = ConvertHBColorToRGB(obj, pData, ref pFrameInfo, pImageBuffer);
                if (nRet != 0)
                {
                    return;
                }
                pTemp = pImageBuffer;
                byte* pBufForSaveImage = (byte*)(void*)pTemp;
                uint nSupWidth = (uint)(pFrameInfo.nWidth + 3) & 0xFFFFFFFCu;
                for (int nRow = 0; nRow < pFrameInfo.nHeight; nRow++)
                {
                    for (int col = 0; col < pFrameInfo.nWidth; col++)
                    {
                        byteArrImageData[nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + 3 * col];
                        byteArrImageData[pFrameInfo.nWidth * pFrameInfo.nHeight + nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 1)];
                        byteArrImageData[pFrameInfo.nWidth * pFrameInfo.nHeight * 2 + nRow * nSupWidth + col] = pBufForSaveImage[nRow * pFrameInfo.nWidth * 3 + (3 * col + 2)];
                    }
                }
                pTemp = Marshal.UnsafeAddrOfPinnedArrayElement(byteArrImageData, 0);
            }
            else if (IsMonoPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToMono8(_camera, pData, pImageBuffer, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType);
                    if (nRet != 0)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }
                pixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            if (StitchFlag == 1)
            {
                Cognex.VisionPro.ICogImage cogImage = ImageData.GetOutputImage(pFrameInfo.nHeight, pFrameInfo.nWidth, pTemp, pixelType);
                imageDatas.Add(cogImage);
                if (imageDatas.Count == ImgCount)
                {
                    imageStitcher = new CogCopyRegionTool();
                    if (IsColorPixelFormat(pFrameInfo.enPixelType) || IsHBColorPixelFormat(pFrameInfo.enPixelType))
                    {
                        CogImage24PlanarColor dstImage = new CogImage24PlanarColor();
                        dstImage.Allocate(pFrameInfo.nWidth, pFrameInfo.nHeight * imageDatas.Count);
                        imageStitcher.DestinationImage = dstImage;
                        imageStitcher.Region = null;
                        imageStitcher.RunParams.ImageAlignmentEnabled = true;
                        for (int i = 0; i < imageDatas.Count; i++)
                        {
                            imageStitcher.RunParams.DestinationImageAlignmentX = 0.0;
                            imageStitcher.RunParams.DestinationImageAlignmentY = imageDatas[i].Height * i;
                            imageStitcher.InputImage = imageDatas[i];
                            imageStitcher.Run();
                        }
                    }
                    else if (IsMonoPixelFormat(pFrameInfo.enPixelType))
                    {
                        CogImage8Grey dstImage2 = new CogImage8Grey();
                        dstImage2.Allocate(pFrameInfo.nWidth, pFrameInfo.nHeight * imageDatas.Count);
                        imageStitcher.DestinationImage = dstImage2;
                        imageStitcher.Region = null;
                        imageStitcher.RunParams.ImageAlignmentEnabled = true;
                        for (int j = 0; j < imageDatas.Count; j++)
                        {
                            imageStitcher.RunParams.DestinationImageAlignmentX = 0.0;
                            imageStitcher.RunParams.DestinationImageAlignmentY = imageDatas[j].Height * j;
                            imageStitcher.InputImage = imageDatas[j];
                            imageStitcher.Run();
                        }
                    }
                    Cognex.VisionPro.ICogImage imageS = imageStitcher.OutputImage;
                    ImageData imageData2 = new ImageData(imageS);
                    ImageArrived(this, imageData2);
                    imageDatas.Clear();
                    imageStitcher.Dispose();
                    imageStitcher = null;
                }
            }
            else
            {
                ImageData imageData = new ImageData(ImageData.GetOutputImage(pFrameInfo.nHeight, pFrameInfo.nWidth, pTemp, pixelType));
                ImageArrived(this, imageData);
            }
            if (pImageBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(pImageBuffer);
            }
        }

        private bool IsMonoPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_Mono12:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsHBMonoPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_HB_Mono8:
                case MvGvspPixelType.PixelType_Gvsp_HB_Mono10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_HB_Mono12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_HB_Mono10:
                case MvGvspPixelType.PixelType_Gvsp_HB_Mono12:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsColorPixelFormat(MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsHBColorPixelFormat(MvGvspPixelType enType)
        {
            if ((uint)(enType - -2130182136) <= 3u || enType == MvGvspPixelType.PixelType_Gvsp_HB_YUV422_Packed || enType == MvGvspPixelType.PixelType_Gvsp_HB_RGB8_Packed)
            {
                return true;
            }
            return false;
        }

        public int ConvertToMono8(object obj, IntPtr pInData, IntPtr pOutData, ushort nHeight, ushort nWidth, MvGvspPixelType nPixelType)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return -2147483644;
            }
            int nRet = 0;
            CCamera device = obj as CCamera;
            CPixelConvertParam cPixelConvertParam = new CPixelConvertParam();
            cPixelConvertParam.InImage.ImageAddr = pInData;
            if (IntPtr.Zero == cPixelConvertParam.InImage.ImageAddr)
            {
                return -1;
            }
            cPixelConvertParam.InImage.Width = nWidth;
            cPixelConvertParam.InImage.Height = nHeight;
            cPixelConvertParam.InImage.PixelType = nPixelType;
            cPixelConvertParam.InImage.ImageSize = (uint)(nWidth * nHeight * (((uint)nPixelType >> 16) & 0xFF) >> 3);
            cPixelConvertParam.OutImage.FrameLen = (uint)((long)(nWidth * nHeight) * 24L >> 3);
            cPixelConvertParam.OutImage.ImageAddr = pOutData;
            cPixelConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
            cPixelConvertParam.OutImage.ImageSize = (uint)(nWidth * nHeight * 3);
            nRet = device.ConvertPixelType(ref cPixelConvertParam);
            if (nRet != 0)
            {
                return -1;
            }
            return nRet;
        }

        public int ConvertToRGB(object obj, IntPtr pSrc, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return -2147483644;
            }
            int nRet = 0;
            CCamera device = obj as CCamera;
            CPixelConvertParam cPixelConvertParam = new CPixelConvertParam();
            cPixelConvertParam.InImage = new CImage(pSrc, pFrameInfo.enPixelType, pFrameInfo.nFrameLen, pFrameInfo.nHeight, pFrameInfo.nWidth, 0u, 0u);
            if (IntPtr.Zero == cPixelConvertParam.InImage.ImageAddr)
            {
                return -1;
            }
            cPixelConvertParam.OutImage.ImageSize = (uint)((long)(pFrameInfo.nHeight * pFrameInfo.nWidth) * 24L >> 3);
            cPixelConvertParam.OutImage.ImageAddr = pDst;
            cPixelConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            if (_camera.ConvertPixelType(ref cPixelConvertParam) != 0)
            {
                return -1;
            }
            return 0;
        }

        public int ConvertHBColorToRGB(object obj, IntPtr pSrc, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return -2147483644;
            }
            int nRet = 0;
            CCamera device = obj as CCamera;
            CDecodeParam CDcode = new CDecodeParam();
            MvGvspPixelType outPixelType = (MvGvspPixelType)0;
            switch (pFrameInfo.enPixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_HB_BayerBG8:
                    outPixelType = MvGvspPixelType.PixelType_Gvsp_BayerBG8;
                    break;
                case MvGvspPixelType.PixelType_Gvsp_HB_BayerGB8:
                    outPixelType = MvGvspPixelType.PixelType_Gvsp_BayerGB8;
                    break;
                case MvGvspPixelType.PixelType_Gvsp_HB_BayerGR8:
                    outPixelType = MvGvspPixelType.PixelType_Gvsp_BayerGR8;
                    break;
                case MvGvspPixelType.PixelType_Gvsp_HB_BayerRG8:
                    outPixelType = MvGvspPixelType.PixelType_Gvsp_BayerRG8;
                    break;
                case MvGvspPixelType.PixelType_Gvsp_HB_RGB8_Packed:
                    outPixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                    break;
                case MvGvspPixelType.PixelType_Gvsp_HB_YUV422_Packed:
                    outPixelType = MvGvspPixelType.PixelType_Gvsp_YUV422_Packed;
                    break;
            }
            if (outPixelType == MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
            {
                CDcode.InImage = new CImage(pSrc, pFrameInfo.enPixelType, pFrameInfo.nFrameLen, pFrameInfo.nHeight, pFrameInfo.nWidth, 0u, 0u);
                CDcode.OutImage.ImageSize = (uint)((long)(pFrameInfo.nWidth * pFrameInfo.nHeight) * 24L >> 3);
                CDcode.OutImage.ImageAddr = pDst;
                CDcode.OutImage.PixelType = outPixelType;
                CDcode.OutImage.ImageSize = (uint)(pFrameInfo.nWidth * pFrameInfo.nHeight * 3);
                nRet = _camera.HB_Decode(ref CDcode);
            }
            else
            {
                CDcode.InImage = new CImage(pSrc, pFrameInfo.enPixelType, pFrameInfo.nFrameLen, pFrameInfo.nHeight, pFrameInfo.nWidth, 0u, 0u);
                CDcode.OutImage = new CImage(pFrameInfo.nHeight, pFrameInfo.nWidth, outPixelType);
                nRet = _camera.HB_Decode(ref CDcode);
                if (nRet == 0)
                {
                    CPixelConvertParam cPixelConvertParam = new CPixelConvertParam();
                    cPixelConvertParam.InImage = new CImage(CDcode.OutImage.ImageAddr, outPixelType, CDcode.OutImage.ImageSize, CDcode.OutImage.Height, CDcode.OutImage.Width, CDcode.OutImage.ExtendHeight, CDcode.OutImage.ExtendWidth);
                    cPixelConvertParam.OutImage.ImageSize = (uint)((long)(CDcode.OutImage.Height * CDcode.OutImage.Width) * 24L >> 3);
                    cPixelConvertParam.OutImage.ImageAddr = pDst;
                    cPixelConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                    cPixelConvertParam.OutImage.ImageSize = (uint)(CDcode.OutImage.Height * CDcode.OutImage.Width * 3);
                    nRet = _camera.ConvertPixelType(ref cPixelConvertParam);
                }
            }
            if (nRet != 0)
            {
                return -1;
            }
            return 0;
        }

        private int GetIntValue(string featureName)
        {
            try
            {
                CIntValue stIntValue = new CIntValue();
                _camera.GetIntValue(featureName, ref stIntValue);
                return (int)stIntValue.CurValue;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private double GetDoubleValue(string featureName)
        {
            try
            {
                CFloatValue stFloatValue = new CFloatValue();
                _camera.GetFloatValue(featureName, ref stFloatValue);
                return stFloatValue.CurValue;
            }
            catch (Exception)
            {
                return -1.0;
            }
        }

        private string GetStringValue(string featureName)
        {
            try
            {
                CStringValue stStringValue = new CStringValue();
                _camera.GetStringValue(featureName, ref stStringValue);
                return stStringValue.CurValue;
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        private bool GetBoolValue(string featureName)
        {
            try
            {
                bool bValue = false;
                _camera.GetBoolValue(featureName, ref bValue);
                return bValue;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int GetIntOfEnumValue(string featureName)
        {
            try
            {
                CEnumValue stEnumValue = new CEnumValue();
                _camera.GetEnumValue(featureName, ref stEnumValue);
                return (int)stEnumValue.CurValue;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void GetIntMinMax(string featureName, out int min, out int max)
        {
            try
            {
                CIntValue stIntValue = new CIntValue();
                _camera.GetIntValue(featureName, ref stIntValue);
                min = (int)stIntValue.Min;
                max = (int)stIntValue.Max;
            }
            catch (Exception)
            {
                min = -1;
                max = -1;
            }
        }

        private void GetDoubleMinMax(string featureName, out double min, out double max)
        {
            try
            {
                CFloatValue stFloatValue = new CFloatValue();
                _camera.GetFloatValue(featureName, ref stFloatValue);
                min = stFloatValue.Min;
                max = stFloatValue.Max;
            }
            catch (Exception)
            {
                min = -1.0;
                max = -1.0;
            }
        }

        private int SetValue(string featureName, int value)
        {
            int nRet = -1;
            try
            {
                if (ValueIsValid(featureName, value))
                {
                    return _camera.SetIntValue(featureName, (uint)value);
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetValueEx(string featureName, int value)
        {
            int nRet = -1;
            try
            {
                return _camera.SetIntValue(featureName, (uint)value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetValue(string featureName, double value)
        {
            int nRet = -1;
            try
            {
                if (ValueIsValid(featureName, value))
                {
                    return _camera.SetFloatValue(featureName, (float)value);
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetValueEx(string featureName, double value)
        {
            int nRet = -1;
            try
            {
                return _camera.SetFloatValue(featureName, (float)value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetValue(string featureName, bool value)
        {
            int nRet = -1;
            try
            {
                return _camera.SetBoolValue(featureName, value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetValue(string featureName, string value)
        {
            int nRet = -1;
            try
            {
                return _camera.SetStringValue(featureName, value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetEnumValue(string featureName, string value)
        {
            int nRet = -1;
            try
            {
                return _camera.SetEnumValueByString(featureName, value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetEnumValue(string featureName, int value)
        {
            int nRet = -1;
            try
            {
                return _camera.SetEnumValue(featureName, (uint)value);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int SetCommand(string featureName)
        {
            int nRet = -1;
            try
            {
                return _camera.SetCommandValue(featureName);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private bool ValueIsValid(string featureName, int value)
        {
            try
            {
                int min = 0;
                int max = 0;
                GetIntMinMax(featureName, out min, out max);
                if (value >= min && value <= max)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValueIsValid(string featureName, double value)
        {
            try
            {
                double min = 0.0;
                double max = 0.0;
                GetDoubleMinMax(featureName, out min, out max);
                if (value >= min && value <= max)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SetWorkMode(TriggerMode mode)
        {
            if (_camera != null)
            {
                switch ((byte)mode)
                {
                    case 1:
                        SetContinuousMode();
                        break;
                    case 2:
                        SetTimeSoftwareMode();
                        break;
                    case 3:
                        SetTimeHardwareMode();
                        break;
                    case 4:
                        SetRotarySoftwareMode();
                        break;
                    case 5:
                        SetRotaryHardwareMode();
                        break;
                    case 6:
                        SetFrameBurstMode();
                        break;
                }
                workMode = (int)mode;
            }
        }

        private int SetContinuousMode()
        {
            int nRet = 0;
            try
            {
                nRet += _camera.SetEnumValueByString("TriggerSelector", "FrameBurstStart");
                nRet += _camera.SetEnumValue("TriggerMode", 0u);
                nRet += _camera.SetEnumValueByString("TriggerSelector", "LineStart");
                nRet += _camera.SetEnumValue("TriggerMode", 0u);
                if (nRet != 0)
                {
                    ExceptionPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                nRet = -1;
                ExceptionPush("Error Setting workMode:" + ex.Message);
            }
            return nRet;
        }

        private int SetTimeSoftwareMode()
        {
            int nRet = 0;
            try
            {
                nRet += _camera.SetEnumValueByString("TriggerSelector", "LineStart");
                nRet += _camera.SetEnumValue("TriggerMode", 0u);
                nRet += _camera.SetEnumValueByString("TriggerSelector", "FrameBurstStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 7u);
                if (nRet != 0)
                {
                    ExceptionPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                nRet = -1;
                ExceptionPush("Error Setting workMode:" + ex.Message);
            }
            return nRet;
        }

        private int SetRotarySoftwareMode()
        {
            int nRet = 0;
            try
            {
                nRet += _camera.SetEnumValueByString("TriggerSelector", "LineStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 8u);
                nRet += _camera.SetEnumValueByString("TriggerActivation", "RisingEdge");
                nRet += _camera.SetEnumValueByString("TriggerSelector", "FrameBurstStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 7u);
                nRet += _camera.SetEnumValueByString("EncoderSourceA", "Line0");
                nRet += _camera.SetEnumValueByString("EncoderSourceB", "Line1");
                nRet += _camera.SetEnumValueByString("LineSelector", "Line0");
                nRet += _camera.SetEnumValueByString("LineFormat", "Differential");
                nRet += _camera.SetEnumValueByString("LineSelector", "Line1");
                nRet += _camera.SetEnumValueByString("LineFormat", "Differential");
                nRet += _camera.SetEnumValueByString("InputSource", "EncoderModuleOut");
                nRet += _camera.SetEnumValueByString("SignalAlignment", "RisingEdge");
                if (nRet != 0)
                {
                    ExceptionPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                nRet = -1;
                ExceptionPush("Error Setting workMode:" + ex.Message);
            }
            return nRet;
        }

        private int SetTimeHardwareMode()
        {
            int nRet = 0;
            try
            {
                nRet = _camera.SetEnumValueByString("TriggerSelector", "LineStart");
                nRet = _camera.SetEnumValue("TriggerMode", 0u);
                nRet = _camera.SetEnumValueByString("TriggerSelector", "FrameBurstStart");
                nRet = _camera.SetEnumValue("TriggerMode", 1u);
                nRet = _camera.SetEnumValue("TriggerSource", 3u);
                nRet = _camera.SetEnumValueByString("TriggerActivation", "RisingEdge");
                if (nRet != 0)
                {
                    ExceptionPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                nRet = -1;
                ExceptionPush("Error Setting workMode:" + ex.Message);
            }
            return nRet;
        }

        private int SetRotaryHardwareMode()
        {
            int nRet = 0;
            try
            {
                nRet += _camera.SetEnumValueByString("TriggerSelector", "FrameBurstStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 3u);
                nRet += _camera.SetEnumValueByString("TriggerSelector", "LineStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 8u);
                nRet += _camera.SetEnumValueByString("TriggerActivation", "RisingEdge");
                nRet += _camera.SetEnumValueByString("EncoderSourceA", "Line0");
                nRet += _camera.SetEnumValueByString("EncoderSourceB", "Line1");
                nRet += _camera.SetEnumValueByString("LineSelector", "Line0");
                nRet += _camera.SetEnumValueByString("LineFormat", "Differential");
                nRet += _camera.SetEnumValueByString("LineSelector", "Line1");
                nRet += _camera.SetEnumValueByString("LineFormat", "Differential");
                nRet += _camera.SetEnumValueByString("InputSource", "EncoderModuleOut");
                nRet += _camera.SetEnumValueByString("SignalAlignment", "RisingEdge");
                if (nRet != 0)
                {
                    ExceptionPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                nRet = -1;
                ExceptionPush("Error Setting workMode:" + ex.Message);
            }
            return nRet;
        }

        private int SetFrameBurstMode()
        {
            int nRet = 0;
            try
            {
                nRet += _camera.SetEnumValueByString("TriggerSelector", "LineStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 8u);
                nRet += _camera.SetEnumValueByString("TriggerActivation", "RisingEdge");
                nRet += _camera.SetEnumValueByString("TriggerSelector", "FrameBurstStart");
                nRet += _camera.SetEnumValue("TriggerMode", 1u);
                nRet += _camera.SetEnumValue("TriggerSource", 7u);
                nRet += _camera.SetEnumValueByString("EncoderSourceA", "Line0");
                nRet += _camera.SetEnumValueByString("EncoderSourceB", "Line1");
                nRet += _camera.SetEnumValueByString("LineSelector", "Line0");
                nRet += _camera.SetEnumValueByString("LineFormat", "Differential");
                nRet += _camera.SetEnumValueByString("LineSelector", "Line1");
                nRet += _camera.SetEnumValueByString("LineFormat", "Differential");
                nRet += _camera.SetEnumValueByString("InputSource", "EncoderModuleOut");
                nRet += _camera.SetEnumValueByString("SignalAlignment", "RisingEdge");
                if (nRet != 0)
                {
                    ExceptionPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                nRet = -1;
                ExceptionPush("Error Setting workMode:" + ex.Message);
            }
            return nRet;
        }
    }
}
