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
    public class Camera_HIKLineScanCamL : CameraLine2DBase
    {
        private CCamera myCamera;

        public cbOutputExdelegate ImageCallback;

        private cbExceptiondelegate pCallBackFunc;

        public static Dictionary<string, CCamera> D_cameras = new Dictionary<string, CCamera>();

        public static List<CCameraInfo> m_pDeviceList = new List<CCameraInfo>();

        public static Dictionary<string, CCamLCameraInfo> D_camlDevices = new Dictionary<string, CCamLCameraInfo>();

        public static Dictionary<string, CCameraInfo> D_devices = new Dictionary<string, CCameraInfo>();

        public static List<Camera_HIKLineScanCamL> L_devices = new List<Camera_HIKLineScanCamL>();

        private object obj = new object();

        protected ImageData imageData;

        private bool m_bGrabbing;

        private long g_nPayloadSize = 0L;

        private long m_nRowStep = 0L;

        private uint nBaudrateAblity = 0u;

        private uint nCurrentBaudrate = 0u;

        private IntPtr pTemp;

        private IntPtr pImageBuffer;

        private byte[] byteArrImageData;

        private int nRet;

        public override float Exposure
        {
            get
            {
                CFloatValue stParam = new CFloatValue();
                if (myCamera.GetFloatValue("ExposureTime", ref stParam) == 0)
                {
                    exposure = stParam.CurValue;
                    SettingParams.ExposureTime = (int)exposure;
                    return exposure;
                }
                return 0f;
            }
            set
            {
                if (exposure != value)
                {
                    if (myCamera.SetEnumValue("ExposureAuto", 0u) != 0)
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") 关闭自动曝光模式失败！");
                    }
                    if (myCamera.SetFloatValue("ExposureTime", value) == 0)
                    {
                        exposure = value;
                        SettingParams.ExposureTime = (int)exposure;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") 设置曝光参数失败！");
                    }
                }
            }
        }

        public override float Gain
        {
            get
            {
                CFloatValue stParam = new CFloatValue();
                if (myCamera.GetFloatValue("Gain", ref stParam) == 0)
                {
                    gain = Convert.ToInt32(stParam.CurValue);
                    SettingParams.Gain = (int)gain;
                    return gain;
                }
                return 0;
            }
            set
            {
                if (!(gain == value))
                {
                    if (myCamera.SetEnumValue("GainAuto", 0u) != 0)
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") 关闭自动增益模式失败！");
                    }
                    if (myCamera.SetFloatValue("Gain", (int)value) == 0)
                    {
                        gain = value;
                        SettingParams.Gain = (int)gain;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") 设置增益参数失败！");
                    }
                }
            }
        }

        public override long ScanWidth
        {
            get
            {
                CIntValue stParam = new CIntValue();
                if (myCamera.GetIntValue("Width", ref stParam) == 0)
                {
                    scanWidth = Convert.ToInt64(stParam.CurValue);
                    SettingParams.ScanWidth = (int)scanWidth;
                    return scanWidth;
                }
                return 0L;
            }
            set
            {
                if (scanWidth != value)
                {
                    if (myCamera.StopGrabbing() != 0)
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Stop Grabbing Fail");
                    }
                    else if (myCamera.SetIntValue("Width", (uint)value) == 0)
                    {
                        scanWidth = value;
                        SettingParams.ScanWidth = (int)scanWidth;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Set Width Parameter Fail");
                    }
                }
            }
        }

        public override long ScanHeight
        {
            get
            {
                CIntValue stParam = new CIntValue();
                if (myCamera.GetIntValue("Height", ref stParam) == 0)
                {
                    scanHeight = Convert.ToInt64(stParam.CurValue);
                    SettingParams.ScanHeight = (int)scanHeight;
                    return scanHeight;
                }
                return 0L;
            }
            set
            {
                if (scanHeight != value)
                {
                    if (myCamera.StopGrabbing() != 0)
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Stop Grabbing Fail");
                    }
                    else if (myCamera.SetIntValue("Height", (uint)value) == 0)
                    {
                        scanHeight = value;
                        SettingParams.ScanHeight = (int)scanHeight;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Set Height Parameter Fail");
                    }
                }
            }
        }

        public override long OffsetX
        {
            get
            {
                CIntValue stParam = new CIntValue();
                if (myCamera.GetIntValue("OffsetX", ref stParam) == 0)
                {
                    offsetX = Convert.ToInt64(stParam.CurValue);
                    SettingParams.OffsetX = (int)offsetX;
                    return offsetX;
                }
                return 0L;
            }
            set
            {
                if (offsetX != value)
                {
                    if (myCamera.StopGrabbing() != 0)
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Stop Grabbing Fail");
                    }
                    else if (myCamera.SetIntValue("OffsetX", (uint)value) == 0)
                    {
                        offsetX = value;
                        SettingParams.OffsetX = (int)offsetX;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Set OffsetX Parameter Fail");
                    }
                }
            }
        }

        public override long AcqLineRate
        {
            get
            {
                CFloatValue stParam = new CFloatValue();
                if (myCamera.GetFloatValue("AcquisitionFrameRate", ref stParam) == 0)
                {
                    acqLineRate = Convert.ToInt64(stParam.CurValue);
                    SettingParams.AcqLineRate = (int)acqLineRate;
                    return acqLineRate;
                }
                return 0L;
            }
            set
            {
                if (acqLineRate != value)
                {
                    if (myCamera.SetFloatValue("AcquisitionLineRate", value) == 0)
                    {
                        acqLineRate = value;
                        SettingParams.AcqLineRate = (int)acqLineRate;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Set AcquisitionLineRate Parameter Fail");
                    }
                }
            }
        }

        public override int RotaryDirection
        {
            get
            {
                CIntValue stParam = new CIntValue();
                if (myCamera.GetIntValue("EncoderOutputMode", ref stParam) == 0)
                {
                    rotaryDirection = Convert.ToInt32(stParam.CurValue);
                    return rotaryDirection;
                }
                return 0;
            }
            set
            {
                if (rotaryDirection != value)
                {
                    if (myCamera.SetIntValue("EncoderOutputMode", (uint)value) == 0)
                    {
                        rotaryDirection = value;
                    }
                    else
                    {
                        LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Set EncoderOutputMode Fail");
                    }
                }
            }
        }

        public Camera_HIKLineScanCamL(string externSN)
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
                D_camlDevices.Clear();
                D_devices.Clear();
                if (CSystem.EnumDevices(8u, ref m_pDeviceList) != 0)
                {
                    LogUtil.LogError("HIKVision LineScanCamL Enum Devices Fail");
                    return;
                }
                for (int i = 0; i < m_pDeviceList.Count; i++)
                {
                    if (m_pDeviceList[i].nTLayerType == 8)
                    {
                        CCamLCameraInfo camLInfo = (CCamLCameraInfo)m_pDeviceList[i];
                        if (camLInfo.chModelName.Substring(0, 5) == "MV-CL" && camLInfo.chManufacturerName.Contains("Hikrobot"))
                        {
                            D_camlDevices.Add(camLInfo.chSerialNumber, camLInfo);
                            D_devices.Add(camLInfo.chSerialNumber, m_pDeviceList[i]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                LogUtil.Log("海康2D线扫相机CameraLink枚举异常！");
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
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Open Device Fail");
                camErrCode = CamErrCode.ConnectFailed;
                isConnected = false;
                return -1;
            }
            nRet = myCamera.CAML_SetDeviceBaudrate(MV_CAML_BAUDRATE.MV_CAML_BAUDRATE_115200);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Set device bauderate fail");
            }
            MV_CAML_BAUDRATE temp = MV_CAML_BAUDRATE.MV_CAML_BAUDRATE_9600;
            nRet = myCamera.CAML_GetDeviceBaudrate(ref temp);
            nCurrentBaudrate = (uint)temp;
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get device bauderate fail");
            }
            CIntValue stParam = new CIntValue();
            nRet = myCamera.GetIntValue("Height", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get Height Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            long nHeight = stParam.CurValue;
            nRet = myCamera.GetIntValue("Width", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get Width Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return -1;
            }
            long nWidth = stParam.CurValue;
            CEnumValue stEnumVal = new CEnumValue();
            nRet = myCamera.GetEnumValue("TriggerMode", ref stEnumVal);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get Trigger Mode failed");
                return -1;
            }
            uint nTriggerMode = stEnumVal.CurValue;
            CFloatValue stFloatVal = new CFloatValue();
            nRet = myCamera.GetFloatValue("AcquisitionFrameRate", ref stFloatVal);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get AcquisitionFrameRate failed");
                return -1;
            }
            bool bBoolVal = false;
            nRet = myCamera.GetBoolValue("AcquisitionFrameRateEnable", ref bBoolVal);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get AcquisitionFrameRateEnable failed");
                return -1;
            }
            CStringValue stStrVal = new CStringValue();
            nRet = myCamera.GetStringValue("DeviceUserID", ref stStrVal);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get DeviceUserID failed");
                return -1;
            }
            string nDeviceUserID = stStrVal.CurValue;
            if (8 == device.nTLayerType)
            {
                CCamLCameraInfo stCamLDeviceInfo = (CCamLCameraInfo)device;
                string sn = stCamLDeviceInfo.chSerialNumber;
                string modelName = stCamLDeviceInfo.chModelName;
                string vendorName = stCamLDeviceInfo.chManufacturerName;
                string versionName = (Version = stCamLDeviceInfo.chDeviceVersion);
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
            Start_Grab(sate: true);
            return 0;
        }

        public void BeforeStartGrab()
        {
            CIntValue stParam = new CIntValue();
            nRet = myCamera.GetIntValue("Height", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get Height Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return;
            }
            long nHeight = stParam.CurValue;
            nRet = myCamera.GetIntValue("Width", ref stParam);
            if (nRet != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Get Width Fail");
                camErrCode = CamErrCode.ConnectFailed;
                return;
            }
            long nWidth = stParam.CurValue;
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
        }

        public void SetHardwareTriggerMode()
        {
        }

        public void SetRotaryEncoderTriggerMode()
        {
        }

        public void SetContinousTriggerMode()
        {
        }

        public void RestartGrab()
        {
            if (myCamera.StartGrabbing() != 0)
            {
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Restart Grabbing Fail");
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

        public void RotaryEncoderGrab()
        {
            SetTriggerSelector(TriggerMode2DLinear.RotaryEncoder);
        }

        public static void CloseAllCameras()
        {
            try
            {
                foreach (Camera_HIKLineScanCamL item in L_devices)
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
                    LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") 采集时间超时！");
                }
            });
            if (acqOk)
            {
                return 0;
            }
            return -1;
        }

        public override int Start_Grab(bool sate)
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
                LogUtil.LogError("HIKVisionLineScanCamL(" + cameraSN + ") Stop Grabbing Fail");
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
