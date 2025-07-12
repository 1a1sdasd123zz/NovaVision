using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using NovaVision.BaseClass;
using ThridLibray;

namespace NovaVision.Hardware._014_SDK_IRAPLE
{
    public class Camera_IRAYPLE : Camera2DBase
    {
        public enum IMGCNV_EBayerDemosaic
        {
            IMGCNV_DEMOSAIC_NEAREST_NEIGHBOR = 0,
            IMGCNV_DEMOSAIC_BILINEAR = 1,
            IMGCNV_DEMOSAIC_EDGE_SENSING = 2,
            IMGCNV_DEMOSAIC_NOT_SUPPORT = 255
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct IMGCNV_SOpenParam
        {
            public int width;

            public int height;

            public int paddingX;

            public int paddingY;

            public int dataSize;

            public uint pixelForamt;
        }

        private int nRet;

        private bool m_bGrabbing;

        private IDevice m_dev;

        public static Dictionary<string, IDeviceInfo> D_devices = new Dictionary<string, IDeviceInfo>();

        public static List<Camera_IRAYPLE> L_devices = new List<Camera_IRAYPLE>();

        public override double Exposure
        {
            get
            {
                using IFloatParameter p = m_dev.ParameterCollection[new FloatName("ExposureTime")];
                return p.GetValue();
            }
            set
            {
                if (_exposure == value)
                {
                    return;
                }
                using IFloatParameter p = m_dev.ParameterCollection[new FloatName("ExposureTime")];
                if (p.SetValue(value))
                {
                    _exposure = value;
                }
                else
                {
                    LogUtil.LogError($"大华相机{SN},设置曝光{value}失败！");
                }
            }
        }
        public override double Gain
        {
            get
            {
                using IFloatParameter p = m_dev.ParameterCollection[new FloatName("GainRaw")];
                return p.GetValue();
            }
            set
            {
                if (_gain == value)
                {
                    return;
                }
                using IFloatParameter p = m_dev.ParameterCollection[new FloatName("GainRaw")];
                if (p.SetValue(value))
                {
                    _gain = value;
                }
                else
                {
                    LogUtil.LogError($"大华相机{SN},设置曝光{value}失败！");
                }
            }
        }

        public Camera_IRAYPLE(string externSN)
        {
            SN = externSN;
        }

        public static void EnumCameras()
        {
            try
            {
                D_devices.Clear();
                List<IDeviceInfo> listDivceInfo = Enumerator.EnumerateDevices(3u);
                foreach (IDeviceInfo deviceInfo in listDivceInfo)
                {
                    D_devices.Add(deviceInfo.SerialNumber, deviceInfo);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华2D相机枚举异常！" + ex.ToString());
            }
        }

        public static Camera_IRAYPLE FindCamera(string deviceSN)
        {
            try
            {
                for (int i = 0; i < L_devices.Count; i++)
                {
                    if (L_devices[i].SN == deviceSN)
                    {
                        return L_devices[i];
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override int OpenCamera()
        {
            try
            {
                nRet = -1;
                IDeviceInfo deviceInfo = D_devices[SN];
                m_dev = Enumerator.GetDeviceByIndex(deviceInfo.Index);
                m_dev.CameraOpened += OnCameraOpen;
                m_dev.ConnectionLost += OnConnectLoss;
                m_dev.CameraClosed += OnCameraClose;
                if (!m_dev.Open())
                {
                    LogUtil.LogError("大华相机" + deviceInfo.SerialNumber + "打开失败！");
                    camErrCode = CamErrCode.ConnectFailed;
                    return nRet;
                }
                camErrCode = CamErrCode.ConnectSuccess;
                if (deviceInfo.DeviceTypeEx == 1)
                {
                    IGigeDeviceInfo gigeDeviceInfo = Enumerator.GigeCameraInfo(deviceInfo.Index);
                    DeviceIP = gigeDeviceInfo.IpAddress;
                    vendorName = deviceInfo.Vendor;
                    modelName = deviceInfo.Model;
                    _userDefinedName = deviceInfo.Name;
                    DeviceInfoStr = $"{gigeDeviceInfo.IpAddress} | {gigeDeviceInfo.MacAddress} | {vendorName} | {modelName}";
                    triggerMode = TriggerMode2D.Software;
                }
                else
                {
                    vendorName = deviceInfo.Vendor;
                    modelName = deviceInfo.Model;
                    _userDefinedName = deviceInfo.Name;
                    DeviceInfoStr = $"{vendorName} | {modelName}";
                    triggerMode = TriggerMode2D.Software;
                }
                using (IEnumParameter enumParameter = m_dev.ParameterCollection[new EnumName("AcquisitionMode")])
                {
                    enumParameter.SetValue("Continuous");
                }
                using (IEnumParameter enumParameter2 = m_dev.ParameterCollection[new EnumName("TriggerMode")])
                {
                    enumParameter2.SetValue("On");
                }
                using (IEnumParameter p = m_dev.ParameterCollection[new EnumName("TriggerSource")])
                {
                    p.SetValue("Software");
                }
                m_dev.StreamGrabber.SetBufferCount(8);
                m_dev.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(deviceInfo.SerialNumber))
                {
                    CameraOperator.camera2DCollection.Add(deviceInfo.SerialNumber, this);
                }
                StartGrab();
                isConnected = true;
                return 0;
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华相机打开失败！" + ex.ToString());
                return -1;
            }
        }

        private void OnCameraOpen(object sender, EventArgs e)
        {
        }

        private void OnCameraClose(object sender, EventArgs e)
        {
        }

        private void OnConnectLoss(object sender, EventArgs e)
        {
            m_bGrabbing = false;
            m_dev.ShutdownGrab();
            m_dev.Dispose();
            m_dev = null;
            CameraOperator.camera2DCollection.Remove(SN);
            camErrCode = CamErrCode.ConnectLost;
            LogUtil.LogError("大华" + SN + "相机掉线！");
        }

        private void OnImageGrabbed(object sender, GrabbedEventArgs e)
        {
            try
            {
                acqOk = true;
                Cognex.VisionPro.ICogImage cogImage = null;
                IGrabbedRawData grebRawDate = e.GrabResult.Clone();
                int rawImageSize = grebRawDate.ImageSize;
                if (CvtGvspPixelFormatType(grebRawDate.PixelFmt) == 1)
                {
                    IntPtr raw = Marshal.UnsafeAddrOfPinnedArrayElement(grebRawDate.Image, 0);
                    cogImage = ImageData.GetMonoImage(grebRawDate.Height, grebRawDate.Width, raw);
                }
                else if (CvtGvspPixelFormatType(grebRawDate.PixelFmt) == 3)
                {
                    IntPtr rawPtr = Marshal.UnsafeAddrOfPinnedArrayElement(grebRawDate.Image, 0);
                    int datdSize = RGBFactory.EncodeLen(grebRawDate.Width, grebRawDate.Height, color: true);
                    IntPtr colorPtr = Marshal.AllocHGlobal(datdSize);
                    IMGCNV_SOpenParam sOpenParam = default(IMGCNV_SOpenParam);
                    sOpenParam.width = grebRawDate.Width;
                    sOpenParam.height = grebRawDate.Height;
                    sOpenParam.paddingX = 0;
                    sOpenParam.paddingY = 0;
                    sOpenParam.dataSize = grebRawDate.ImageSize;
                    sOpenParam.pixelForamt = (uint)grebRawDate.PixelFmt;
                    int nDesDataSize = 0;
                    if (IMGCNV_ConvertToBGR24_Ex(rawPtr, ref sOpenParam, colorPtr, ref nDesDataSize, IMGCNV_EBayerDemosaic.IMGCNV_DEMOSAIC_EDGE_SENSING) != 0)
                    {
                        LogUtil.LogError("大华相机" + SN + "图像转码出错！");
                    }
                    else
                    {
                        Bitmap bitmap = new Bitmap(grebRawDate.Width, grebRawDate.Height, PixelFormat.Format24bppRgb);
                        Rectangle bitmapRect = default(Rectangle);
                        bitmapRect.Height = bitmap.Height;
                        bitmapRect.Width = bitmap.Width;
                        BitmapData bmpData = bitmap.LockBits(bitmapRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        CopyMemory(bmpData.Scan0, colorPtr, bmpData.Stride * bitmap.Height);
                        bitmap.UnlockBits(bmpData);
                        cogImage = new CogImage24PlanarColor(bitmap);
                        if (colorPtr != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(colorPtr);
                        }
                    }
                }
                if (UpdateImage != null)
                {
                    UpdateImage(new ImageData(cogImage));
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华相机" + SN + "回调出错:" + ex.Message);
            }
        }


        public override void SetExposure(double exposure)
        {
            Exposure = exposure;
            SettingParams.ExposureTime = (int)Exposure;
        }

        public override void SetGain(double gain)
        {
            Gain = gain;
            SettingParams.Gain = (int)Gain;
        }

        public override void SetTriggerMode(TriggerMode2D triggerMode)
        {
            switch (triggerMode)
            {
                case TriggerMode2D.Software:
                    SetSoftwareTriggerMode();
                    break;
                case TriggerMode2D.Hardware:
                    SetHardwareTriggerMode();
                    break;
                case TriggerMode2D.Continous:
                    SetContinousTriggerMode();
                    break;
            }
            SettingParams.TriggerMode = (int)triggerMode;
        }

        public override int SoftwareTriggerOnce()
        {
            LogUtil.Log("Dahua2DGige(" + SN + ")单帧采集！");
            acqOk = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            StartGrab();
            if (m_dev.IsOpen && m_dev.IsGrabbing)
            {
                bool bState;
                using (ICommandParameter p = m_dev.ParameterCollection[new CommandName("TriggerSoftware")])
                {
                    bState = p.Execute();
                }
                if (!bState)
                {
                    LogUtil.LogError("大华线扫相机" + SN + ",执行软触发命令失败！");
                    return -1;
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
                    if (timeSpan.TotalMilliseconds > timeout)
                    {
                        LogUtil.LogError("Dahua2DGige(" + SN + ")采集时间超时！");
                    }
                });
                if (acqOk)
                {
                    return 0;
                }
                return -1;
            }
            LogUtil.LogError($"相机打开状态{m_dev.IsOpen},采集状态{m_dev.IsGrabbing}");
            return -1;
        }

        public override void ContinousGrab()
        {
            StartGrab();
            SetTriggerMode(TriggerMode2D.Continous);
        }

        public override void HardwareGrab()
        {
            SetTriggerMode(TriggerMode2D.Hardware);
        }

        public override int CloseCamera()
        {
            m_bGrabbing = false;
            try
            {
                if (m_dev == null)
                {
                    LogUtil.LogError("Device is invalid.");
                    return -1;
                }
                m_dev.StreamGrabber.ImageGrabbed -= OnImageGrabbed;
                m_dev.ShutdownGrab();
                m_dev.Close();
                CameraOperator.camera2DCollection.Remove(SN);
                return 0;
            }
            catch (Exception ex)
            {
                LogUtil.LogError("大华{SN}相机关闭失败:" + ex.Message);
                return -1;
            }
        }

        public void StartGrab()
        {
            if (m_dev.IsOpen && m_dev.IsGrabbing)
            {
                return;
            }
            try
            {
                if (!m_dev.StreamGrabber.Start())
                {
                    m_bGrabbing = false;
                    return;
                }
                bool isStart = m_dev.StreamGrabber.IsStart;
                m_bGrabbing = true;
            }
            catch (Exception ex)
            {
                m_bGrabbing = false;
                LogUtil.LogError("大华相机采集开启失败:" + ex.Message);
            }
        }

        public override int StopGrab()
        {
            if (m_dev.IsOpen && m_dev.IsGrabbing)
            {
                try
                {
                    if (!m_dev.StreamGrabber.Stop())
                    {
                        return -1;
                    }
                    m_bGrabbing = false;
                    SetTriggerMode(TriggerMode2D.Software);
                    return 0;
                }
                catch (Exception ex)
                {
                    m_bGrabbing = false;
                    LogUtil.LogError("大华相机采集停止失败:" + ex.Message);
                    return -1;
                }
            }
            return 0;
        }

        public void SetSoftwareTriggerMode()
        {
            bool nRet;
            using (IEnumParameter enumParameter = m_dev.ParameterCollection[new EnumName("TriggerMode")])
            {
                nRet = enumParameter.SetValue("On");
            }
            bool nRet2;
            using (IEnumParameter p = m_dev.ParameterCollection[new EnumName("TriggerSource")])
            {
                nRet2 = p.SetValue("Software");
            }
            if (nRet && nRet2)
            {
                triggerMode = TriggerMode2D.Software;
            }
        }

        public void SetHardwareTriggerMode()
        {
            bool nRet;
            using (IEnumParameter enumParameter = m_dev.ParameterCollection[new EnumName("TriggerMode")])
            {
                nRet = enumParameter.SetValue("On");
            }
            bool nRet2;
            using (IEnumParameter p = m_dev.ParameterCollection[new EnumName("TriggerSource")])
            {
                nRet2 = p.SetValue("Line1");
            }
            if (nRet && nRet2)
            {
                triggerMode = TriggerMode2D.Hardware;
            }
        }

        public void SetContinousTriggerMode()
        {
            bool nRet;
            using (IEnumParameter enumParameter = m_dev.ParameterCollection[new EnumName("TriggerMode")])
            {
                nRet = enumParameter.SetValue("Off");
            }
            bool nRet2;
            using (IEnumParameter p = m_dev.ParameterCollection[new EnumName("TriggerSource")])
            {
                nRet2 = p.SetValue("Software");
            }
            if (nRet && nRet2)
            {
                triggerMode = TriggerMode2D.Continous;
            }
        }

        private int CvtGvspPixelFormatType(GvspPixelFormatType pixelFmt)
        {
            int nRet = 0;
            switch (pixelFmt)
            {
                case GvspPixelFormatType.gvspPixelMono8:
                case GvspPixelFormatType.gvspPixelMono8S:
                case GvspPixelFormatType.gvspPixelMono10Packed:
                case GvspPixelFormatType.gvspPixelMono12Packed:
                case GvspPixelFormatType.gvspPixelMono10:
                case GvspPixelFormatType.gvspPixelMono12:
                case GvspPixelFormatType.gvspPixelMono16:
                case GvspPixelFormatType.gvspPixelMono14:
                    nRet = 1;
                    break;
                case GvspPixelFormatType.gvspPixelBayGR8:
                case GvspPixelFormatType.gvspPixelBayRG8:
                case GvspPixelFormatType.gvspPixelBayGB8:
                case GvspPixelFormatType.gvspPixelBayBG8:
                case GvspPixelFormatType.gvspPixelBayGR10Packed:
                case GvspPixelFormatType.gvspPixelBayRG10Packed:
                case GvspPixelFormatType.gvspPixelBayGB10Packed:
                case GvspPixelFormatType.gvspPixelBayBG10Packed:
                case GvspPixelFormatType.gvspPixelBayGR12Packed:
                case GvspPixelFormatType.gvspPixelBayRG12Packed:
                case GvspPixelFormatType.gvspPixelBayGB12Packed:
                case GvspPixelFormatType.gvspPixelBayBG12Packed:
                case GvspPixelFormatType.gvspPixelBayGR10:
                case GvspPixelFormatType.gvspPixelBayRG10:
                case GvspPixelFormatType.gvspPixelBayGB10:
                case GvspPixelFormatType.gvspPixelBayBG10:
                case GvspPixelFormatType.gvspPixelBayGR12:
                case GvspPixelFormatType.gvspPixelBayRG12:
                case GvspPixelFormatType.gvspPixelBayGB12:
                case GvspPixelFormatType.gvspPixelBayBG12:
                case GvspPixelFormatType.gvspPixelBayGR16:
                case GvspPixelFormatType.gvspPixelBayRG16:
                case GvspPixelFormatType.gvspPixelBayGB16:
                case GvspPixelFormatType.gvspPixelBayBG16:
                case GvspPixelFormatType.gvspPixelRGB565P:
                case GvspPixelFormatType.gvspPixelBGR565P:
                case GvspPixelFormatType.gvspPixelRGB8:
                case GvspPixelFormatType.gvspPixelBGR8:
                case GvspPixelFormatType.gvspPixelRGB8Planar:
                case GvspPixelFormatType.gvspPixelRGBA8:
                case GvspPixelFormatType.gvspPixelBGRA8:
                case GvspPixelFormatType.gvspPixelRGB10V1Packed:
                case GvspPixelFormatType.gvspPixelRGB10P32:
                case GvspPixelFormatType.gvspPixelRGB12V1Packed:
                case GvspPixelFormatType.gvspPixelRGB10:
                case GvspPixelFormatType.gvspPixelBGR10:
                case GvspPixelFormatType.gvspPixelRGB12:
                case GvspPixelFormatType.gvspPixelBGR12:
                case GvspPixelFormatType.gvspPixelRGB10Planar:
                case GvspPixelFormatType.gvspPixelRGB12Planar:
                case GvspPixelFormatType.gvspPixelRGB16Planar:
                case GvspPixelFormatType.gvspPixelRGB16:
                    nRet = 3;
                    break;
            }
            return nRet;
        }

        [DllImport("ImageConvert.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int IMGCNV_ConvertToBGR24_Ex(IntPtr pSrcData, ref IMGCNV_SOpenParam pOpenParam, IntPtr pDstData, ref int pDstDataSize, IMGCNV_EBayerDemosaic eBayerDemosaic);

        [DllImport("Kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory")]
        internal static extern void CopyMemory(IntPtr pDst, IntPtr pSrc, int len);
    }
}
