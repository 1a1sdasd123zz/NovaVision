using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using NovaVision.BaseClass;

namespace NovaVision.Hardware.C_2DGigeLineScan.Hikrobot
{

    public class Camera_HIKLineScanGige : CameraLine2DBase
    {
        private CCamera myCamera;

        public cbOutputExdelegate ImageCallback;

        private cbExceptiondelegate pCallBackFunc;

        public static Dictionary<string, CCamera> D_cameras = new Dictionary<string, CCamera>();

        public static List<CCameraInfo> m_pDeviceList;

        public static Dictionary<string, CGigECameraInfo> D_gigeDevices = new Dictionary<string, CGigECameraInfo>();

        public static Dictionary<string, CUSBCameraInfo> D_usbDevices = new Dictionary<string, CUSBCameraInfo>();

        public static Dictionary<string, CCameraInfo> D_devices = new Dictionary<string, CCameraInfo>();

        public static List<Camera_HIKLineScanGige> L_devices = new List<Camera_HIKLineScanGige>();

        private object obj = new object();

        protected ImageData imageData;

        private bool m_bGrabbing;

        private long g_nPayloadSize = 0L;

        private long m_nRowStep = 0L;

        private IntPtr pTemp;

        private IntPtr pImageBuffer;

        private byte[] byteArrImageData;

        private int nRet;

        public override float Exposure
        {
            get
            {
                try
                {
                    CFloatValue stParam = new CFloatValue();
                    if (myCamera.GetFloatValue("ExposureTime", ref stParam) == 0)
                    {
                        exposure = stParam.CurValue;
                        SettingParams.ExposureTime = (int)exposure;
                        return exposure;
                    }
                }
                catch (Exception)
                {
                }
                return 0f;
            }
            set
            {
                try
                {
                    if (exposure != value)
                    {
                        myCamera.SetEnumValue("ExposureAuto", 0u);
                        myCamera.SetFloatValue("ExposureTime", value);
                        exposure = value;
                        SettingParams.ExposureTime = (int)exposure;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override float Gain
        {
            get
            {
                try
                {
                    CFloatValue stParam = new CFloatValue();
                    if (myCamera.GetFloatValue("Gain", ref stParam) == 0)
                    {
                        gain = Convert.ToInt32(stParam.CurValue);
                        SettingParams.Gain = (int)gain;
                        return gain;
                    }
                }
                catch (Exception)
                {
                }
                return 0;
            }
            set
            {
                try
                {
                    if (!(gain == value))
                    {
                        myCamera.SetEnumValue("GainAuto", 0u);
                        myCamera.SetFloatValue("Gain", (int)value);
                        gain = value;
                        SettingParams.Gain = (int)gain;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override long ScanWidth
        {
            get
            {
                try
                {
                    CIntValue stParam = new CIntValue();
                    if (myCamera.GetIntValue("Width", ref stParam) == 0)
                    {
                        scanWidth = Convert.ToInt64(stParam.CurValue);
                        SettingParams.ScanWidth = (int)scanWidth;
                        return scanWidth;
                    }
                }
                catch (Exception)
                {
                }
                return 0L;
            }
            set
            {
                try
                {
                    if (scanWidth != value)
                    {
                        nRet = myCamera.StopGrabbing();
                        if (nRet != 0)
                        {
                            LogUtil.LogError("HIKVisionLineScan(" + cameraSN + ") Stop Grabbing Fail");
                        }
                        myCamera.SetIntValue("Width", (uint)value);
                        scanWidth = value;
                        SettingParams.ScanWidth = (int)scanWidth;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override long ScanHeight
        {
            get
            {
                try
                {
                    CIntValue stParam = new CIntValue();
                    if (myCamera.GetIntValue("Height", ref stParam) == 0)
                    {
                        scanHeight = Convert.ToInt64(stParam.CurValue);
                        SettingParams.ScanHeight = (int)scanHeight;
                        return scanHeight;
                    }
                }
                catch (Exception)
                {
                }
                return 0L;
            }
            set
            {
                try
                {
                    if (scanHeight != value)
                    {
                        nRet = myCamera.StopGrabbing();
                        if (nRet != 0)
                        {
                            LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Stop Grabbing Fail");
                        }
                        myCamera.SetIntValue("Height", (uint)value);
                        scanHeight = value;
                        SettingParams.ScanHeight = (int)scanHeight;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override long OffsetX
        {
            get
            {
                try
                {
                    CIntValue stParam = new CIntValue();
                    if (myCamera.GetIntValue("OffsetX", ref stParam) == 0)
                    {
                        offsetX = Convert.ToInt64(stParam.CurValue);
                        SettingParams.OffsetX = (int)offsetX;
                        return offsetX;
                    }
                }
                catch (Exception)
                {
                }
                return 0L;
            }
            set
            {
                try
                {
                    if (offsetX != value)
                    {
                        nRet = myCamera.StopGrabbing();
                        if (nRet != 0)
                        {
                            LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Stop Grabbing Fail");
                        }
                        myCamera.SetIntValue("OffsetX", (uint)value);
                        offsetX = value;
                        SettingParams.OffsetX = (int)offsetX;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override long AcqLineRate
        {
            get
            {
                try
                {
                    CIntValue stParam = new CIntValue();
                    if (myCamera.GetIntValue("AcquisitionLineRate", ref stParam) == 0)
                    {
                        acqLineRate = Convert.ToInt64(stParam.CurValue);
                        SettingParams.AcqLineRate = (int)acqLineRate;
                        return acqLineRate;
                    }
                }
                catch (Exception)
                {
                }
                return 0L;
            }
            set
            {
                try
                {
                    if (acqLineRate != value)
                    {
                        myCamera.SetIntValue("AcquisitionLineRate", (uint)value);
                        acqLineRate = value;
                        SettingParams.AcqLineRate = (int)acqLineRate;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override int RotaryDirection
        {
            get
            {
                try
                {
                    CIntValue stParam = new CIntValue();
                    if (myCamera.GetIntValue("EncoderOutputMode", ref stParam) == 0)
                    {
                        rotaryDirection = Convert.ToInt32(stParam.CurValue);
                        return rotaryDirection;
                    }
                }
                catch (Exception)
                {
                }
                return 0;
            }
            set
            {
                try
                {
                    if (rotaryDirection != value)
                    {
                        myCamera.SetIntValue("EncoderOutputMode", (uint)value);
                        rotaryDirection = value;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public Camera_HIKLineScanGige(string externSN)
        {
            cameraSN = externSN;
            pCallBackFunc = cbExceptiondelegate;
        }

        private void cbExceptiondelegate(uint nMsgType, IntPtr pUser)
        {
            if (nMsgType == 32769)
            {
                Console.WriteLine("海康线扫相机掉线了");
                CameraOperator.camera2DLineCollection.Remove(cameraSN);
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(cameraSN, true);
                    cam_Handle.CamConnectedLostHandle(cameraMessage);
                }
            }
        }

        public static void EnumCamera()
        {
            try
            {
                D_gigeDevices.Clear();
                D_usbDevices.Clear();
                D_devices.Clear();
                if (CSystem.EnumDevices(5u, ref m_pDeviceList) != 0)
                {
                    LogUtil.LogError("HIKVision LineScanGige Enum Devices Fail");
                    return;
                }
                for (int i = 0; i < m_pDeviceList.Count; i++)
                {
                    if (m_pDeviceList[i].nTLayerType == 1)
                    {
                        CGigECameraInfo gigeInfo = (CGigECameraInfo)m_pDeviceList[i];
                        if (gigeInfo.chModelName.Substring(0, 5) == "MV-CL" && (gigeInfo.chManufacturerName == "GEV" || gigeInfo.chManufacturerName.Contains("Hik")))
                        {
                            D_gigeDevices.Add(gigeInfo.chSerialNumber, gigeInfo);
                            D_devices.Add(gigeInfo.chSerialNumber, m_pDeviceList[i]);
                        }
                    }
                    else if (m_pDeviceList[i].nTLayerType == 4)
                    {
                        CUSBCameraInfo usbInfo = (CUSBCameraInfo)m_pDeviceList[i];
                        if (usbInfo.chModelName.Substring(0, 5) == "MV-CL" && (usbInfo.chManufacturerName == "GEV" || usbInfo.chManufacturerName.Contains("Hik")))
                        {
                            D_usbDevices.Add(usbInfo.chSerialNumber, usbInfo);
                            D_devices.Add(usbInfo.chSerialNumber, m_pDeviceList[i]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                LogUtil.Log("海康2D线扫相机Gige枚举异常！");
            }
        }

        public override int OpenCamera()
        {
            nRet = -1;
            CCameraInfo device = D_devices[cameraSN];
            myCamera = new CCamera();
            nRet = myCamera.CreateHandle(ref device);
            if (nRet != 0)
            {
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
                return -1;
            }
            nRet = myCamera.OpenDevice();
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Open Device Fail");
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
                return -1;
            }
            CIntValue stParam = new CIntValue();
            nRet = myCamera.GetIntValue("PayloadSize", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Get PayloadSize Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            g_nPayloadSize = stParam.CurValue;
            nRet = myCamera.GetIntValue("Height", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Get Height Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            long nHeight = stParam.CurValue;
            nRet = myCamera.GetIntValue("Width", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Get Width Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            long nWidth = stParam.CurValue;
            if (1 == device.nTLayerType)
            {
                CGigECameraInfo stGigEDeviceInfo = (CGigECameraInfo)device;
                uint nIp1 = (stGigEDeviceInfo.nCurrentIp & 0xFF000000u) >> 24;
                uint nIp2 = (stGigEDeviceInfo.nCurrentIp & 0xFF0000) >> 16;
                uint nIp3 = (stGigEDeviceInfo.nCurrentIp & 0xFF00) >> 8;
                uint nIp4 = stGigEDeviceInfo.nCurrentIp & 0xFFu;
                string sn = stGigEDeviceInfo.chSerialNumber;
                string ipAddress = $"{nIp1}.{nIp2}.{nIp3}.{nIp4}";
                string macAddress = stGigEDeviceInfo.nCurrentSubNetMask.ToString();
                string modelName = stGigEDeviceInfo.chModelName;
                string vendorName = stGigEDeviceInfo.chManufacturerName;
                string userDefinedName = stGigEDeviceInfo.UserDefinedName;
                DeviceIP = ipAddress;
                VendorName = vendorName;
                ModelName = modelName;
                if (!D_cameras.ContainsKey(sn))
                {
                    L_devices.Add(this);
                    D_cameras.Add(sn, myCamera);
                }
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(cameraSN, true);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                if (!CameraOperator.camera2DLineCollection._2DLineCameras.ContainsKey(sn))
                {
                    CameraOperator.camera2DLineCollection.Add(sn, this);
                }
            }
            m_nRowStep = nWidth * nHeight;
            myCamera.SetEnumValue("AcquisitionMode", 2u);
            SetTriggerSelector(TriggerMode2DLinear.Time_Software);
            ImageCallback = ImageCallbackFunc;
            nRet = myCamera.RegisterImageCallBackEx(ImageCallback, IntPtr.Zero);
            myCamera.RegisterExceptionCallBack(pCallBackFunc, IntPtr.Zero);
            Start_Grab(state: true);
            return 0;
        }

        public void BeforeStartGrab()
        {
            nRet = 0;
            pImageBuffer = Marshal.AllocHGlobal((int)m_nRowStep * 3);
            if (!(pImageBuffer == IntPtr.Zero))
            {
                pTemp = IntPtr.Zero;
                byteArrImageData = new byte[m_nRowStep * 3];
            }
        }

        public unsafe void ImageCallbackFunc(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            acqOk = true;
            MvGvspPixelType pixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            if (IsColorPixelFormat(pFrameInfo.enPixelType))
            {
                if (pFrameInfo.enPixelType == MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToRGB(obj, pData, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType, pImageBuffer);
                    if (nRet != 0)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }
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
            else
            {
                if (!IsMonoPixelFormat(pFrameInfo.enPixelType))
                {
                    return;
                }
                if (pFrameInfo.enPixelType == MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    pTemp = pData;
                }
                else
                {
                    nRet = ConvertToMono8(myCamera, pData, pImageBuffer, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType);
                    if (nRet != 0)
                    {
                        return;
                    }
                    pTemp = pImageBuffer;
                }
                pixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            ImageData imageData = new ImageData(ImageData.GetOutputImage(pFrameInfo.nHeight, pFrameInfo.nWidth, pTemp, pixelType));
            if (UpdateImage != null)
            {
                UpdateImage(imageData);
            }
        }

        public void SetSoftwareTriggerMode()
        {
            nRet = myCamera.SetEnumValue("TriggerMode", 0u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ")TriggerMode设置成OFF模式失败！");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerSelector", 6u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSelector Fail");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerSource", 7u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSource as Software Fail");
            }
        }

        public void SetHardwareTriggerMode()
        {
            nRet = myCamera.SetEnumValue("TriggerSelector", 6u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSelector Fail");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerMode", 1u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerMode Fail");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerSource", 0u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSource Fail");
            }
        }

        public void SetRotaryEncoderTriggerMode()
        {
            nRet = myCamera.SetEnumValue("TriggerSelector", 6u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSelector Fail");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerMode", 0u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") 编码器触发模式设置失败！");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerSelector", 9u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSelector Fail");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerMode", 1u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") 编码器触发模式设置失败！");
            }
        }

        public void SetContinousTriggerMode()
        {
            nRet = myCamera.SetEnumValue("TriggerMode", 0u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") 连续触发模式设置失败！");
                return;
            }
            nRet = myCamera.SetEnumValue("TriggerSelector", 9u);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Set TriggerSelector Fail");
            }
        }

        public void RestartGrab()
        {
            if (myCamera.StartGrabbing() != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Restart Grabbing Fail");
            }
        }

        public void ContinousGrab()
        {
            SetTriggerSelector(TriggerMode2DLinear.Test_Software);
        }

        public void HardwareGrab()
        {
            SetTriggerSelector(TriggerMode2DLinear.Time_Line1);
        }

        public void RotaryEncoderGrad()
        {
            SetTriggerSelector(TriggerMode2DLinear.RotaryEncoder);
        }

        public static void CloseAllCameras()
        {
            try
            {
                foreach (Camera_HIKLineScanGige item in L_devices)
                {
                    if (item.m_bGrabbing)
                    {
                        item.m_bGrabbing = false;
                        item.myCamera.StopGrabbing();
                    }
                    item.myCamera.CloseDevice();
                    item.m_bGrabbing = false;
                    item.myCamera.DestroyHandle();
                }
            }
            catch (Exception)
            {
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

        public int ConvertToRGB(object obj, IntPtr pSrc, ushort nHeight, ushort nWidth, MvGvspPixelType nPixelType, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return -2147483644;
            }
            int nRet = 0;
            CCamera device = obj as CCamera;
            CPixelConvertParam cPixelConvertParam = new CPixelConvertParam();
            cPixelConvertParam.InImage.ImageAddr = pSrc;
            if (IntPtr.Zero == cPixelConvertParam.InImage.ImageAddr)
            {
                return -1;
            }
            cPixelConvertParam.InImage.Width = nWidth;
            cPixelConvertParam.InImage.Height = nHeight;
            cPixelConvertParam.InImage.PixelType = nPixelType;
            cPixelConvertParam.InImage.ImageSize = (uint)(nWidth * nHeight * (((uint)nPixelType >> 16) & 0xFF) >> 3);
            cPixelConvertParam.OutImage.ImageSize = (uint)((long)(nWidth * nHeight) * 24L >> 3);
            cPixelConvertParam.OutImage.ImageAddr = pDst;
            cPixelConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            cPixelConvertParam.OutImage.ImageSize = (uint)(nWidth * nHeight * 3);
            if (myCamera.ConvertPixelType(ref cPixelConvertParam) != 0)
            {
                return -1;
            }
            return 0;
        }

        public override void SetTriggerSelector(TriggerMode2DLinear triggerSelector)
        {
            triggerSelectorMode = triggerSelector;
            switch (triggerSelector)
            {
                case TriggerMode2DLinear.Time_Line1:
                    SetHardwareTriggerMode();
                    break;
                case TriggerMode2DLinear.Time_Software:
                    SetSoftwareTriggerMode();
                    break;
                case TriggerMode2DLinear.RotaryEncoder:
                    SetRotaryEncoderTriggerMode();
                    break;
                case TriggerMode2DLinear.Test_Software:
                    SetContinousTriggerMode();
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
            nRet = myCamera.SetCommandValue("TriggerSoftware");
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Trigger Fail");
            }
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
                    LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") 采集时间超时！");
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
            int nRet = myCamera.StartGrabbing();
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Start Grabbing Fail");
                return nRet;
            }
            BeforeStartGrab();
            return nRet;
        }

        public override int Stop_Grab(bool sate)
        {
            bStopFlag = true;
            nRet = myCamera.StopGrabbing();
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanGige(" + cameraSN + ") Stop Grabbing Fail");
                return -1;
            }
            SetTriggerSelector(TriggerMode2DLinear.Time_Software);
            RestartGrab();
            m_bGrabbing = false;
            return 0;
        }

        public override void DestroyObjects()
        {
            if (m_bGrabbing)
            {
                m_bGrabbing = false;
                myCamera.StopGrabbing();
            }
            myCamera.CloseDevice();
            m_bGrabbing = false;
            isConnected = false;
            camErrCode = CamErrCode.ConnectFailed;
            CameraOperator.camera2DLineCollection.Remove(cameraSN);
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(cameraSN, false);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
        }

        public override void SetRotaryDirection(TriggerMode2DLinear triggerSelector, int index)
        {
            if (triggerSelector == TriggerMode2DLinear.RotaryEncoder)
            {
                switch (index)
                {
                    case 0:
                        RotaryDirection = 0;
                        break;
                    case 1:
                        RotaryDirection = 1;
                        break;
                    case 2:
                        RotaryDirection = 2;
                        break;
                }
                SettingParams.RotaryDirection = index;
            }
        }
    }

}
