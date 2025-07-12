using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using MvFGCtrlC.NET;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Helper;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_._05_HIK_CL
{
    public class HikCL : Bv_Camera, IAcquisition2DLineScan3D, IDisposable
    {
        private CDevice m_cDevice = null;

        private CStream m_cStream = null;

        private static CSystem m_cSystem;

        private static List<CInterface> m_stIFInfoList;

        private static List<MV_FG_DEVICE_INFO> m_stDeviceList;

        public static Dictionary<string, CInterface> D_cards;

        public static Dictionary<string, MV_FG_DEVICE_INFO> D_devices;

        public static Dictionary<string, HikCL> devicesOpened;

        public static HashSet<string> cameraSN;

        public static HashSet<string> cardSN;

        public static Dictionary<string, string> DeviceBindCard;

        private string m_SerialCamera = "";

        private FrameGrabberConfigData _configDatas;

        private string m_ConfigFileName = "M_DEFAULT";

        private readonly string _vendorName = "HikCL";

        private readonly CameraCategory CATEGORY = CameraCategory.C_2DLineCL;

        private bool objectCreated = false;

        private bool stopGrabFlag = true;

        private bool acqOK = false;

        private uint splitCount = 0u;

        private IntPtr pData;

        private IntPtr pImageBuffer;

        private IntPtr pTemp;

        private byte[] byteArrImageData;

        private static int nRet;

        public override string VendorName => _vendorName;

        public override CameraCategory Category => CATEGORY;

        public override string SerialNum => m_SerialCamera;

        public override FrameGrabberConfigData ConfigDatas => _configDatas;

        public bool ObjectCreated => objectCreated;

        public bool StopGrabFlag => stopGrabFlag;

        public event HardwareErrorEventHandler errorOccured;

        public event Action<bool> UpdateStartStopStatus;

        static HikCL()
        {
            m_cSystem = new CSystem();
            m_stIFInfoList = new List<CInterface>();
            m_stDeviceList = new List<MV_FG_DEVICE_INFO>();
            D_cards = new Dictionary<string, CInterface>();
            D_devices = new Dictionary<string, MV_FG_DEVICE_INFO>();
            devicesOpened = new Dictionary<string, HikCL>();
            cardSN = new HashSet<string>();
            cameraSN = new HashSet<string>();
        }

        public HikCL(string SNcamera, FrameGrabberConfigData paramValues)
        {
            m_SerialCamera = SNcamera;
            InitiallParams();
            if (paramValues != null && paramValues.CameraOrGrabberParams.Count == _configDatas.CameraOrGrabberParams.Count)
            {
                _configDatas = paramValues;
            }
            _configDatas.CameraParamChanged += HikCL_CameraParamChanged;
        }

        private void HikCL_CameraParamChanged(object sender, CameraParamChangeArgs e)
        {
            StopGrab();
            bool @return = this.ExecuteMethod(e);
        }

        public static void EnumInterface(bool ReadXml)
        {
            cardSN.Clear();
            bool bChanged = false;
            HikCL.nRet = m_cSystem.UpdateInterfaceList(29u, ref bChanged);
            if (HikCL.nRet != 0)
            {
                LogUtil.LogError("Enum Interface failed, ErrorCode:" + HikCL.nRet.ToString("X"));
                return;
            }
            uint m_nInterfaceNum = 0u;
            HikCL.nRet = m_cSystem.GetNumInterfaces(ref m_nInterfaceNum);
            if (HikCL.nRet != 0)
            {
                LogUtil.LogError("Get interface number failed, ErrorCode:" + HikCL.nRet.ToString("X"));
                return;
            }
            if (m_nInterfaceNum == 0)
            {
                LogUtil.LogError("No interface found");
                return;
            }

            MV_FG_INTERFACE_INFO stIfInfo = default(MV_FG_INTERFACE_INFO);
            for (uint i = 0u; i < m_nInterfaceNum; i++)
            {
                HikCL.nRet = m_cSystem.GetInterfaceInfo(i, ref stIfInfo);
                if (HikCL.nRet != 0)
                {
                    LogUtil.LogError("Get interface info failed, ErrorCode:" + HikCL.nRet.ToString("X"));
                    break;
                }
                uint nTLayerType = stIfInfo.nTLayerType;
                uint num = nTLayerType;
                if (num != 4)
                {
                    continue;
                }
                MV_CML_INTERFACE_INFO stCmlIFInfo = (MV_CML_INTERFACE_INFO)CAdditional.ByteToStruct(stIfInfo.SpecialInfo.stCMLIfInfo, typeof(MV_CML_INTERFACE_INFO));
                cardSN.Add(stCmlIFInfo.chInterfaceID);
                if (!ReadXml)
                {
                    continue;
                }
                CInterface m_cInterface = null;
                int nRet = m_cSystem.OpenInterface(i, out m_cInterface);
                if (nRet != 0)
                {
                    LogUtil.LogError("Open Interface failed, ErrorCode:" + nRet.ToString("X"));
                    break;
                }
                CParam cParam = new CParam(m_cInterface);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + stCmlIFInfo.chInterfaceID + ".xml"))
                {
                    if (cParam.FeatureLoad(stCmlIFInfo.chInterfaceID + ".xml") != 0)
                    {
                        LogUtil.LogError("采集卡配置文件" + stCmlIFInfo.chInterfaceID + ".xml读取失败");
                    }
                    else
                    {
                        LogUtil.LogError("采集卡配置文件" + stCmlIFInfo.chInterfaceID + ".xml读取成功");
                    }
                }
                if (!D_cards.ContainsKey(stCmlIFInfo.chInterfaceID))
                {
                    D_cards.Add(stCmlIFInfo.chInterfaceID, m_cInterface);
                }
                else
                {
                    D_cards[stCmlIFInfo.chInterfaceID] = m_cInterface;
                }
                LoadCamera(stCmlIFInfo.chInterfaceID);
            }
        }

        public static bool Save(string chInterfaceID)
        {
            try
            {
                int Result = 0;
                bool bChanged = false;
                HikCL.nRet = m_cSystem.UpdateInterfaceList(29u, ref bChanged);
                if (HikCL.nRet != 0)
                {
                    LogUtil.LogError("Enum Interface failed, ErrorCode:" + HikCL.nRet.ToString("X"));
                    Result++;
                }
                uint m_nInterfaceNum = 0u;
                HikCL.nRet = m_cSystem.GetNumInterfaces(ref m_nInterfaceNum);
                if (HikCL.nRet != 0)
                {
                    LogUtil.LogError("Get interface number failed, ErrorCode:" + HikCL.nRet.ToString("X"));
                    Result++;
                }
                if (m_nInterfaceNum == 0)
                {
                    LogUtil.LogError("No interface found");
                    Result++;
                }
                MV_FG_INTERFACE_INFO stIfInfo = default(MV_FG_INTERFACE_INFO);
                for (uint i = 0u; i < m_nInterfaceNum; i++)
                {
                    HikCL.nRet = m_cSystem.GetInterfaceInfo(i, ref stIfInfo);
                    if (HikCL.nRet != 0)
                    {
                        LogUtil.LogError("Get interface info failed, ErrorCode:" + HikCL.nRet.ToString("X"));
                        Result++;
                    }
                    uint nTLayerType = stIfInfo.nTLayerType;
                    uint num = nTLayerType;
                    if (num != 4)
                    {
                        continue;
                    }
                    MV_CML_INTERFACE_INFO stCmlIFInfo = (MV_CML_INTERFACE_INFO)CAdditional.ByteToStruct(stIfInfo.SpecialInfo.stCMLIfInfo, typeof(MV_CML_INTERFACE_INFO));
                    if (stCmlIFInfo.chInterfaceID == chInterfaceID)
                    {
                        CInterface m_cInterface = null;
                        int nRet = m_cSystem.OpenInterface(i, out m_cInterface);
                        if (nRet != 0)
                        {
                            LogUtil.LogError("Open Interface failed, ErrorCode:" + nRet.ToString("X"));
                            Result++;
                        }
                        CParam cParam = new CParam(m_cInterface);
                        nRet = cParam.FeatureSave(stCmlIFInfo.chInterfaceID + ".xml");
                        m_cInterface.CloseInterface();
                        Result = ((nRet != 0) ? (Result + 1) : 0);
                    }
                }
                if (Result == 0)
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

        public static void EnumCards()
        {
            D_cards.Clear();
            cameraSN.Clear();
            D_devices.Clear();
            EnumInterface(ReadXml: true);
        }

        public static void LoadCamera(string chInterfaceID)
        {
            if (D_cards == null || !D_cards.ContainsKey(chInterfaceID))
            {
                return;
            }
            CInterface stIFInfo = D_cards[chInterfaceID];
            bool bChanged = false;
            nRet = stIFInfo.UpdateDeviceList(ref bChanged);
            if (nRet != 0)
            {
                LogUtil.LogError("Update device list failed, ErrorCode:" + nRet.ToString("X"));
                return;
            }
            uint nDeviceNum = 0u;
            nRet = stIFInfo.GetNumDevices(ref nDeviceNum);
            if (nRet != 0)
            {
                LogUtil.LogError("Get devices number failed, ErrorCode:" + nRet.ToString("X"));
                return;
            }
            if (nDeviceNum == 0)
            {
                LogUtil.LogError("No Device found");
                return;
            }

            MV_FG_DEVICE_INFO stDeviceInfo = default(MV_FG_DEVICE_INFO);
            for (uint i = 0u; i < nDeviceNum; i++)
            {
                nRet = stIFInfo.GetDeviceInfo(i, ref stDeviceInfo);
                if (nRet != 0)
                {
                    LogUtil.LogError("Get device info failed, ErrorCode:" + nRet.ToString("X"));
                    break;
                }
                string strShowDevInfo = null;
                switch (stDeviceInfo.nDevType)
                {
                    case 1u:
                        {
                            MV_GEV_DEVICE_INFO stGevDevInfo = (MV_GEV_DEVICE_INFO)CAdditional.ByteToStruct(stDeviceInfo.DevInfo.stGEVDevInfo, typeof(MV_GEV_DEVICE_INFO));
                            if (!D_devices.ContainsKey(stGevDevInfo.chDeviceID))
                            {
                                D_devices.Add(stGevDevInfo.chDeviceID, stDeviceInfo);
                            }
                            else
                            {
                                D_devices[stGevDevInfo.chDeviceID] = stDeviceInfo;
                            }
                            cameraSN.Add(stGevDevInfo.chDeviceID);
                            if (!DeviceBindCard.ContainsKey(stGevDevInfo.chDeviceID))
                            {
                                DeviceBindCard.Add(stGevDevInfo.chDeviceID, chInterfaceID);
                            }
                            else
                            {
                                DeviceBindCard[stGevDevInfo.chDeviceID] = chInterfaceID;
                            }
                            break;
                        }
                    case 4u:
                        {
                            MV_CXP_DEVICE_INFO stCxpDevInfo = (MV_CXP_DEVICE_INFO)CAdditional.ByteToStruct(stDeviceInfo.DevInfo.stCXPDevInfo, typeof(MV_CXP_DEVICE_INFO));
                            if (!D_devices.ContainsKey(stCxpDevInfo.chDeviceID))
                            {
                                D_devices.Add(stCxpDevInfo.chDeviceID, stDeviceInfo);
                            }
                            else
                            {
                                D_devices[stCxpDevInfo.chDeviceID] = stDeviceInfo;
                            }
                            cameraSN.Add(stCxpDevInfo.chDeviceID);
                            if (!DeviceBindCard.ContainsKey(stCxpDevInfo.chDeviceID))
                            {
                                DeviceBindCard.Add(stCxpDevInfo.chDeviceID, chInterfaceID);
                            }
                            else
                            {
                                DeviceBindCard[stCxpDevInfo.chDeviceID] = chInterfaceID;
                            }
                            break;
                        }
                    case 3u:
                        {
                            MV_CML_DEVICE_INFO stCmlDevInfo = (MV_CML_DEVICE_INFO)CAdditional.ByteToStruct(stDeviceInfo.DevInfo.stCMLDevInfo, typeof(MV_CML_DEVICE_INFO));
                            if (!D_devices.ContainsKey(stCmlDevInfo.chDeviceID))
                            {
                                D_devices.Add(stCmlDevInfo.chDeviceID, stDeviceInfo);
                            }
                            else
                            {
                                D_devices[stCmlDevInfo.chDeviceID] = stDeviceInfo;
                            }
                            cameraSN.Add(stCmlDevInfo.chDeviceID);
                            if (!DeviceBindCard.ContainsKey(stCmlDevInfo.chDeviceID))
                            {
                                DeviceBindCard.Add(stCmlDevInfo.chDeviceID, chInterfaceID);
                            }
                            else
                            {
                                DeviceBindCard[stCmlDevInfo.chDeviceID] = chInterfaceID;
                            }
                            break;
                        }
                    case 5u:
                        {
                            MV_XoF_DEVICE_INFO stXoFDevInfo = (MV_XoF_DEVICE_INFO)CAdditional.ByteToStruct(stDeviceInfo.DevInfo.stXoFDevInfo, typeof(MV_XoF_DEVICE_INFO));
                            if (!D_devices.ContainsKey(stXoFDevInfo.chDeviceID))
                            {
                                D_devices.Add(stXoFDevInfo.chDeviceID, stDeviceInfo);
                            }
                            else
                            {
                                D_devices[stXoFDevInfo.chDeviceID] = stDeviceInfo;
                            }
                            cameraSN.Add(stXoFDevInfo.chDeviceID);
                            if (!DeviceBindCard.ContainsKey(stXoFDevInfo.chDeviceID))
                            {
                                DeviceBindCard.Add(stXoFDevInfo.chDeviceID, chInterfaceID);
                            }
                            else
                            {
                                DeviceBindCard[stXoFDevInfo.chDeviceID] = chInterfaceID;
                            }
                            break;
                        }
                    default:
                        strShowDevInfo = strShowDevInfo + "Unknown device[" + i + "]";
                        break;
                }
            }
        }

        public unsafe void ImageCallbackFunc(ref MV_FG_BUFFER_INFO stBufferInfo, IntPtr pUser)
        {
            acqOK = true;
            MV_FG_PIXEL_TYPE pixelType = MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_RGB8_Packed;
            if (IsColorPixelFormat(stBufferInfo.enPixelType))
            {
                if (stBufferInfo.enPixelType == MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_RGB8_Packed)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToRGB(m_cStream, pData, stBufferInfo, pImageBuffer);
                    if (nRet != 0)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }
                byte* pBufForSaveImage = (byte*)(void*)pTemp;
                uint nSupWidth = (stBufferInfo.nWidth + 3) & 0xFFFFFFFCu;
                for (int nRow = 0; nRow < stBufferInfo.nHeight; nRow++)
                {
                    for (int col = 0; col < stBufferInfo.nWidth; col++)
                    {
                        byteArrImageData[nRow * nSupWidth + col] = pBufForSaveImage[nRow * stBufferInfo.nWidth * 3 + 3 * col];
                        byteArrImageData[stBufferInfo.nWidth * stBufferInfo.nHeight + nRow * nSupWidth + col] = pBufForSaveImage[nRow * stBufferInfo.nWidth * 3 + (3 * col + 1)];
                        byteArrImageData[stBufferInfo.nWidth * stBufferInfo.nHeight * 2 + nRow * nSupWidth + col] = pBufForSaveImage[nRow * stBufferInfo.nWidth * 3 + (3 * col + 2)];
                    }
                }
                pTemp = Marshal.UnsafeAddrOfPinnedArrayElement(byteArrImageData, 0);
            }
            else
            {
                if (!IsMonoPixelFormat(stBufferInfo.enPixelType))
                {
                    return;
                }
                if (stBufferInfo.enPixelType == MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono8)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToMono8(m_cStream, pData, pImageBuffer, stBufferInfo);
                    if (nRet != 0)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }
                pixelType = MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono8;
            }
            if (IsMonoPixelFormat(stBufferInfo.enPixelType) && splitCount >= 2)
            {
                byte[] byteTrans = new byte[stBufferInfo.nHeight * stBufferInfo.nWidth];
                if (SmokeMono8Img(pTemp, stBufferInfo.nHeight, stBufferInfo.nWidth, Convert.ToInt32(splitCount), ref byteTrans))
                {
                    Bitmap bmp = new Bitmap(Convert.ToInt32(stBufferInfo.nWidth), Convert.ToInt32(stBufferInfo.nHeight), PixelFormat.Format8bppIndexed);
                    BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);
                    Marshal.Copy(byteTrans, 0, bitmapData.Scan0, byteTrans.Length);
                    bmp.UnlockBits(bitmapData);
                    CogImage8Grey cogImage8Grey = new CogImage8Grey(bmp);
                    ImageData imageData3 = new ImageData(cogImage8Grey);
                    if (UpdateImage != null)
                    {
                        UpdateImage(imageData3);
                    }
                    GC.Collect();
                }
                else
                {
                    ImageData imageData2 = new ImageData(GetOutputImage(stBufferInfo.nHeight, stBufferInfo.nWidth, pTemp, pixelType));
                    if (UpdateImage != null)
                    {
                        UpdateImage(imageData2);
                    }
                    GC.Collect();
                }
            }
            else
            {
                ImageData imageData = new ImageData(GetOutputImage(stBufferInfo.nHeight, stBufferInfo.nWidth, pTemp, pixelType));
                if (UpdateImage != null)
                {
                    UpdateImage(imageData);
                }
                GC.Collect();
            }
        }

        public static Cognex.VisionPro.ICogImage GetOutputImage(uint nHeight, uint nWidth, IntPtr pImageBuf, MV_FG_PIXEL_TYPE enPixelType)
        {
            Cognex.VisionPro.ICogImage tmpImage;
            try
            {
                if (enPixelType == MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono8)
                {
                    CogImage8Root cogImage8Root = new CogImage8Root();
                    cogImage8Root.Initialize((int)nWidth, (int)nHeight, pImageBuf, (int)nWidth, null);
                    CogImage8Grey cogImage8Grey = new CogImage8Grey();
                    cogImage8Grey.SetRoot(cogImage8Root);
                    tmpImage = cogImage8Grey.Copy();
                    GC.Collect();
                }
                else
                {
                    uint m_nRowStep = nWidth * nHeight;
                    CogImage8Root image0 = new CogImage8Root();
                    IntPtr ptr0 = new IntPtr(pImageBuf.ToInt64());
                    image0.Initialize((int)nWidth, (int)nHeight, ptr0, (int)nWidth, null);
                    CogImage8Root image1 = new CogImage8Root();
                    IntPtr ptr1 = new IntPtr(pImageBuf.ToInt64() + m_nRowStep);
                    image1.Initialize((int)nWidth, (int)nHeight, ptr1, (int)nWidth, null);
                    CogImage8Root image2 = new CogImage8Root();
                    IntPtr ptr2 = new IntPtr(pImageBuf.ToInt64() + m_nRowStep * 2);
                    image2.Initialize((int)nWidth, (int)nHeight, ptr2, (int)nWidth, null);
                    CogImage24PlanarColor colorImage = new CogImage24PlanarColor();
                    colorImage.SetRoots(image0, image1, image2);
                    tmpImage = colorImage.Copy(CogImageCopyModeConstants.CopyPixels);
                    GC.Collect();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return tmpImage;
        }

        private bool IsMonoPixelFormat(MV_FG_PIXEL_TYPE enType)
        {
            switch (enType)
            {
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono8:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono10_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono12_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono10:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono12:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsColorPixelFormat(MV_FG_PIXEL_TYPE enType)
        {
            switch (enType)
            {
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGR8:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerRG8:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGB8:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerBG8:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGR10_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerRG10_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGB10_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerBG10_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGR12_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerRG12_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGB12_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerBG12_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGR10:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerRG10:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGB10:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerBG10:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGR12:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerRG12:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerGB12:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BayerBG12:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_YUV422_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_YUV422_YUYV_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_RGB8_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BGR8_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_RGBA8_Packed:
                case MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_BGRA8_Packed:
                    return true;
                default:
                    return false;
            }
        }

        public int ConvertToMono8(CStream obj, IntPtr pInData, IntPtr pOutData, MV_FG_BUFFER_INFO stFrameInfo)
        {
            if (IntPtr.Zero == pInData || IntPtr.Zero == pOutData)
            {
                return -2145844734;
            }
            int nRet = 0;
            CImageProcess cImgProc = new CImageProcess(obj);
            MV_FG_CONVERT_PIXEL_INFO stConvertPixelInfo = default(MV_FG_CONVERT_PIXEL_INFO);
            stConvertPixelInfo.stInputImageInfo.pImageBuf = pInData;
            stConvertPixelInfo.stInputImageInfo.nImageBufLen = stFrameInfo.nFilledSize;
            stConvertPixelInfo.stInputImageInfo.nHeight = stFrameInfo.nHeight;
            stConvertPixelInfo.stInputImageInfo.nWidth = stFrameInfo.nWidth;
            stConvertPixelInfo.stInputImageInfo.enPixelType = stFrameInfo.enPixelType;
            stConvertPixelInfo.stOutputImageInfo.enPixelType = MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_Mono8;
            stConvertPixelInfo.stOutputImageInfo.pImageBuf = pOutData;
            stConvertPixelInfo.stOutputImageInfo.nImageBufSize = stFrameInfo.nWidth * stFrameInfo.nHeight * 3;
            stConvertPixelInfo.enCfaMethod = MV_FG_CFA_METHOD.MV_FG_CFA_METHOD_OPTIMAL;
            nRet = cImgProc.ConvertPixelType(ref stConvertPixelInfo);
            if (nRet != 0)
            {
                return -1;
            }
            return nRet;
        }

        public int ConvertToRGB(CStream obj, IntPtr pSrc, MV_FG_BUFFER_INFO stFrameInfo, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return -2145844734;
            }

            CImageProcess cImgProc = new CImageProcess(obj);
            MV_FG_CONVERT_PIXEL_INFO stConvertPixelInfo = default(MV_FG_CONVERT_PIXEL_INFO);
            stConvertPixelInfo.stInputImageInfo.pImageBuf = pSrc;
            stConvertPixelInfo.stInputImageInfo.nImageBufLen = stFrameInfo.nFilledSize;
            stConvertPixelInfo.stInputImageInfo.nHeight = stFrameInfo.nHeight;
            stConvertPixelInfo.stInputImageInfo.nWidth = stFrameInfo.nWidth;
            stConvertPixelInfo.stInputImageInfo.enPixelType = stFrameInfo.enPixelType;
            stConvertPixelInfo.stOutputImageInfo.enPixelType = MV_FG_PIXEL_TYPE.MV_FG_PIXEL_TYPE_RGB8_Packed;
            stConvertPixelInfo.stOutputImageInfo.pImageBuf = pDst;
            stConvertPixelInfo.stOutputImageInfo.nImageBufSize = stFrameInfo.nWidth * stFrameInfo.nHeight * 3;
            stConvertPixelInfo.enCfaMethod = MV_FG_CFA_METHOD.MV_FG_CFA_METHOD_OPTIMAL;
            if (cImgProc.ConvertPixelType(ref stConvertPixelInfo) != 0)
            {
                return -1;
            }
            return 0;
        }

        public static bool SmokeMono8Img(IntPtr ptrBefore, uint height, uint width, int splitCount, ref byte[] byteAfter)
        {
            int length = Convert.ToInt32(width * height);
            if ((long)height % (long)splitCount != 0)
            {
                return false;
            }
            if (byteAfter.Length != length)
            {
                return false;
            }
            try
            {
                byte[] byteBefore = new byte[length];
                Marshal.Copy(ptrBefore, byteBefore, 0, length);
                int temp = Convert.ToInt32((long)height / (long)splitCount);
                for (int i = 0; i < splitCount; i++)
                {
                    for (int j = 0; j < temp; j++)
                    {
                        Buffer.BlockCopy(byteBefore, Convert.ToInt32((i + j * splitCount) * width), byteAfter, Convert.ToInt32((i * temp + j) * width), Convert.ToInt32(width));
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool OpenDevice()
        {
            if (D_devices == null || D_devices.Count == 0 || !D_devices.ContainsKey(m_SerialCamera))
            {
                ErrorAck(1);
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]打开失败");
                objectCreated = false;
                return false;
            }
            string cInterfaceID = DeviceBindCard[m_SerialCamera];
            nRet = D_cards[cInterfaceID].OpenDeviceByID(m_SerialCamera, out m_cDevice);
            if (nRet != 0)
            {
                ErrorAck(1);
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]打开失败");
                objectCreated = false;
                return false;
            }
            CParam cDeviceParam = new CParam(m_cDevice);
            MV_FG_ENUMVALUE enumvalue = default(MV_FG_ENUMVALUE);
            if (cDeviceParam.GetEnumValue("MultiLightControl", ref enumvalue) == 0)
            {
                splitCount = enumvalue.nCurValue;
            }
            if (!devicesOpened.ContainsKey(m_SerialCamera))
            {
                devicesOpened.Add(m_SerialCamera, this);
            }
            else
            {
                devicesOpened[m_SerialCamera] = this;
            }
            LogUtil.Log("HikCL相机[" + m_SerialCamera + "]打开成功");
            objectCreated = true;
            GetCameraParam();
            return true;
        }

        public override void CloseDevice()
        {
            m_cDevice.CloseDevice();
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
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

        [Setting("WorkMode")]
        public override void SetWorkMode(int workMode)
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            int[] ret = new int[6];
            switch (workMode)
            {
                case 1:
                    ret[0] = cDeviceParam.SetEnumValue("TriggerSelector", 6u);
                    ret[1] = cDeviceParam.SetEnumValue("TriggerMode", 0u);
                    ret[2] = cDeviceParam.SetEnumValue("TriggerSelector", 9u);
                    ret[3] = cDeviceParam.SetEnumValue("TriggerMode", 0u);
                    break;
                case 2:
                    ret[0] = cDeviceParam.SetEnumValue("TriggerSelector", 6u);
                    ret[1] = cDeviceParam.SetEnumValue("TriggerMode", 1u);
                    ret[2] = cDeviceParam.SetEnumValue("TriggerSource", 7u);
                    ret[3] = cDeviceParam.SetEnumValue("TriggerSelector", 9u);
                    ret[4] = cDeviceParam.SetEnumValue("TriggerMode", 0u);
                    break;
                case 3:
                    ret[0] = cDeviceParam.SetEnumValue("TriggerSelector", 6u);
                    ret[1] = cDeviceParam.SetEnumValue("TriggerMode", 1u);
                    ret[2] = cDeviceParam.SetEnumValue("TriggerSource", 1u);
                    ret[3] = cDeviceParam.SetEnumValue("TriggerSelector", 9u);
                    ret[4] = cDeviceParam.SetEnumValue("TriggerMode", 0u);
                    break;
                case 4:
                    ret[0] = cDeviceParam.SetEnumValue("TriggerSelector", 6u);
                    ret[1] = cDeviceParam.SetEnumValue("TriggerMode", 1u);
                    ret[2] = cDeviceParam.SetEnumValue("TriggerSource", 7u);
                    ret[3] = cDeviceParam.SetEnumValue("TriggerSelector", 9u);
                    ret[4] = cDeviceParam.SetEnumValue("TriggerMode", 1u);
                    break;
                case 5:
                    ret[0] = cDeviceParam.SetEnumValue("TriggerSelector", 6u);
                    ret[1] = cDeviceParam.SetEnumValue("TriggerMode", 1u);
                    ret[3] = cDeviceParam.SetEnumValue("TriggerSelector", 9u);
                    ret[4] = cDeviceParam.SetEnumValue("TriggerMode", 1u);
                    break;
            }
        }

        [Setting("ExposureTime")]
        public void SetCameraExposure(ref int exposure)
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            if (exposure < 5)
            {
                exposure = 5;
            }
            else if (exposure > 10000)
            {
                exposure = 10000;
            }
            int ret = cDeviceParam.SetFloatValue("ExposureTime", exposure);
            if (ret != 0)
            {
                LogUtil.LogError($"HikCL相机[{m_SerialCamera}]设置相机曝光失败,eode{ret}");
            }
            else
            {
                LogUtil.Log($"HikCL相机[{m_SerialCamera}]设置相机曝光成功,值{exposure}");
            }
        }

        [Setting("Gain")]
        public void SetCameraGain(ref float gain)
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            if (gain < 0f)
            {
                gain = 0.1f;
            }
            int ret = cDeviceParam.SetFloatValue("Gain", gain);
            if (ret != 0)
            {
                LogUtil.LogError($"HikCL相机[{m_SerialCamera}]设置相机增益失败,eode{ret}");
            }
            else
            {
                LogUtil.Log($"HikCL相机[{m_SerialCamera}]设置相机增益成功,值{gain}");
            }
        }

        [Setting("ScanDirection")]
        public void SetCameraScanDirection(ref int direction)
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            bool temp = false;
            if (direction < 0)
            {
                direction = 0;
            }
            else if (direction > 1)
            {
                direction = 1;
            }
            if (direction == 1)
            {
                temp = true;
            }
            cDeviceParam.SetEnumValue("TDIMode", 1u);
            int ret = cDeviceParam.SetBoolValue("ReverseScanDirection", temp);
            if (ret != 0)
            {
                LogUtil.LogError($"HikCL相机[{m_SerialCamera}]设置相机扫描方向失败,eode{ret}");
            }
            else
            {
                LogUtil.Log($"HikCL相机[{m_SerialCamera}]设置相机扫描方向成功,值{direction}");
            }
        }

        [Setting("ScanWidth")]
        public void SetCameraScanWidth(ref int width)
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            if (width < 0)
            {
                width = 100;
            }
            int ret = cDeviceParam.SetIntValue("Width", (uint)width);
            if (ret != 0)
            {
                LogUtil.LogError($"HikCL相机[{m_SerialCamera}]设置相机扫描宽度失败,eode{ret}");
            }
            else
            {
                LogUtil.Log($"HikCL相机[{m_SerialCamera}]设置相机扫描宽度成功,值{width}");
            }
        }

        [Setting("ScanHeight")]
        public void SetCameraScanHeight(ref int height)
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            if (height < 0)
            {
                height = 100;
            }
            int ret = cDeviceParam.SetIntValue("Height", (uint)height);
            if (ret != 0)
            {
                LogUtil.LogError($"HikCL相机[{m_SerialCamera}]设置相机扫描高度失败,eode{ret}");
            }
            else
            {
                LogUtil.Log($"HikCL相机[{m_SerialCamera}]设置相机扫描高度成功,值{height}");
            }
        }

        public bool GetCameraParam()
        {
            CParam cDeviceParam = new CParam(m_cDevice);
            int[] ret = new int[11];
            bool temp = true;
            try
            {
                MV_FG_FLOATVALUE floatvalue = default(MV_FG_FLOATVALUE);
                ret[0] = cDeviceParam.GetFloatValue("ExposureTime", ref floatvalue);
                _configDatas["ExposureTime"].Value.mValue = (int)floatvalue.fCurValue;
                ret[1] = cDeviceParam.GetFloatValue("Gain", ref floatvalue);
                _configDatas["Gain"].Value.mValue = floatvalue.fCurValue;
                bool ScanDirectionbool = false;
                int ScanDirectionint = 0;
                ret[2] = cDeviceParam.GetBoolValue("ReverseScanDirection", ref ScanDirectionbool);
                if (ScanDirectionbool)
                {
                    ScanDirectionint = 1;
                }
                _configDatas["ScanDirection"].Value.mValue = ScanDirectionint;
                MV_FG_INTVALUE intvalue = default(MV_FG_INTVALUE);
                ret[3] = cDeviceParam.GetIntValue("Width", ref intvalue);
                _configDatas["ScanWidth"].Value.mValue = (int)intvalue.nCurValue;
                ret[4] = cDeviceParam.GetIntValue("Height", ref intvalue);
                _configDatas["ScanHeight"].Value.mValue = (int)intvalue.nCurValue;
                MV_FG_ENUMVALUE enumvalue = default(MV_FG_ENUMVALUE);
                ret[5] = cDeviceParam.SetEnumValue("TriggerSelector", 6u);
                ret[6] = cDeviceParam.GetEnumValue("TriggerMode", ref enumvalue);
                uint TriggerMode1 = enumvalue.nCurValue;
                ret[7] = cDeviceParam.GetEnumValue("TriggerSource", ref enumvalue);
                uint TriggerSource1 = enumvalue.nCurValue;
                ret[8] = cDeviceParam.SetEnumValue("TriggerSelector", 9u);
                ret[9] = cDeviceParam.GetEnumValue("TriggerMode", ref enumvalue);
                uint TriggerMode2 = enumvalue.nCurValue;
                ret[10] = cDeviceParam.GetEnumValue("TriggerSource", ref enumvalue);
                uint TriggerSource2 = enumvalue.nCurValue;
                _configDatas["WorkMode"].Value.mValue = GetWorkMode(new uint[2] { TriggerMode1, TriggerSource1 }, new uint[2] { TriggerMode2, TriggerSource2 });
            }
            catch (Exception)
            {
                return false;
            }
            for (int i = 0; i < ret.Length; i++)
            {
                if (ret[i] != 0)
                {
                    temp = false;
                }
            }
            return temp;
        }

        private int GetWorkMode(uint[] FrameSet, uint[] LineSet)
        {
            int workmode = 2;
            if (FrameSet[0] == 0 && LineSet[0] == 0)
            {
                workmode = 1;
            }
            else if (FrameSet[0] == 1 && LineSet[0] == 0)
            {
                workmode = ((FrameSet[1] != 7) ? 3 : 2);
            }
            else if (FrameSet[0] == 0 && LineSet[0] == 1)
            {
                workmode = 4;
            }
            else if (FrameSet[0] == 1 && LineSet[0] == 1)
            {
                workmode = 5;
            }
            return workmode;
        }

        private void ErrorAck(byte errorCode)
        {
            if (this.errorOccured != null)
            {
                this.errorOccured(this, new HardwareErrorEventArgs(errorCode));
            }
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
                    mValue = m_SerialCamera
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
            ParamElement workMode = new ParamElement
            {
                Name = "WorkMode",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 2
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
            ParamElement cameraScanWidth = new ParamElement
            {
                Name = "ScanWidth",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1000
                }
            };
            ParamElement cameraScanHeight = new ParamElement
            {
                Name = "ScanHeight",
                Type = "Int32",
                Value = new XmlObject
                {
                    mValue = 1000
                }
            };
            _configDatas.CameraOrGrabberParams.AddRange(new ParamElement[11]
            {
            serial, vendorName, category, workMode, timeout, cameraVendorName, cameraExposureTime, cameraGain, cameraScanDirection, cameraScanWidth,
            cameraScanHeight
            });
        }

        public bool StartGrab()
        {
            uint nStreamNum = 0u;
            if (m_cDevice.GetNumStreams(ref nStreamNum) != 0)
            {
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]开始采集失败,Get stream number failed:{0:x8}");
                return false;
            }
            if (nStreamNum == 0)
            {
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]开始采集失败,No stream available");
                return false;
            }
            if (m_cDevice.OpenStream(0u, out m_cStream) != 0)
            {
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]开始采集失败,Open stream failed:{0:x8}");
                return false;
            }
            if (m_cStream.SetBufferNum(3u) != 0)
            {
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]开始采集失败,Set buffer number failed:{0:x8}");
                return false;
            }
            if (m_cStream.RegisterImageCallBack(ImageCallbackFunc, IntPtr.Zero) != 0)
            {
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]开始采集失败,Register image callback failed:{0:x8}");
                return false;
            }
            if (m_cStream.StartAcquisition() == 0)
            {
                stopGrabFlag = false;
                LogUtil.Log("HikCL相机[" + m_SerialCamera + "]开始采集成功");
            }
            else
            {
                stopGrabFlag = true;
                LogUtil.LogError("HikCL相机[" + m_SerialCamera + "]开始采集失败");
            }
            if (this.UpdateStartStopStatus != null)
            {
                this.UpdateStartStopStatus(stopGrabFlag);
            }
            return stopGrabFlag;
        }

        public bool StopGrab()
        {
            m_cStream.StopAcquisition();
            m_cStream.CloseStream();
            stopGrabFlag = true;
            if (this.UpdateStartStopStatus != null)
            {
                this.UpdateStartStopStatus(stopGrabFlag);
            }
            return stopGrabFlag;
        }

        public bool Snap()
        {
            acqOK = false;
            stopGrabFlag = false;
            int timeout = Convert.ToInt32(_configDatas["Timeout"].Value.mValue);
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            CParam cDeviceParam = new CParam(m_cDevice);
            nRet = cDeviceParam.SetCommandValue("TriggerSoftware");
            if (nRet != 0)
            {
                return false;
            }
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
                if (timeSpan.TotalMilliseconds > (double)timeout)
                {
                    LogUtil.LogError("HIKCL(" + m_SerialCamera + ")采集时间超时！");
                }
            });
            return true;
        }
    }
}
