using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ThridLibray;

namespace NovaVision.Hardware.C_2DGigeLineScan.iRAYPLE
{
    internal class DaHua_GigeLineScan
    {
        public delegate void ImageArrivedEventHandler(object sender, ImageData image);

        public delegate void LogPushEventHandler(string msg);

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

        private enum TriggerMode
        {
            Continuous = 1,
            TimeSoftware = 2,
            RotarySoftware = 4,
            TimeHardware = 3,
            RotaryHardware = 5,
            FrameBurst = 6
        }

        private enum TriggerSelector
        {
            FrameStart = 1,
            LineStart,
            FrameActive,
            FrameBurstActive,
            FrameBurstStart
        }

        private enum TriggerSource
        {
            Software = 0,
            Line1 = 1,
            Line2 = 2,
            Line3 = 3,
            Line4 = 4,
            FrequencyConverter0 = 5,
            RotaryEncoder0 = 6,
            Line5 = 8
        }

        private enum TriggerActivation
        {
            RisingEdge = 0,
            FallingEdge = 1,
            LevelLow = 3,
            LevelHigh = 4
        }

        private enum RotaryEncoderModeEnum
        {
            ForwardOnly,
            AnyDirection
        }

        public class ImageData
        {
            public int Width;

            public int Height;

            public long ImageCount;

            public byte[] Data;

            public IntPtr Ptr;

            public int ImageType;

            public Bitmap Bitmap;
        }

        private string m_serialNum;

        private IDevice m_device;

        public bool m_bStartGrab = false;

        private int workMode = 0;

        public static List<string> SerialNums = new List<string>();

        private static Dictionary<string, IDevice> mDic_Devices = new Dictionary<string, IDevice>();

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

        public int TriggerFrameCount
        {
            get
            {
                return GetIntValue("TriggerFrameCount");
            }
            set
            {
                SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstStart.ToString());
                SetValue("TriggerFrameCount", value);
            }
        }

        public double AcquisitionLineRate
        {
            get
            {
                return GetDoubleValue("AcquisitionLineRate");
            }
            set
            {
                SetValue("AcquisitionLineRate", value);
            }
        }

        public bool AcquisitionLineRateEnable
        {
            get
            {
                return GetBoolValue("AcquisitionLineRateEnable");
            }
            set
            {
                SetValue("AcquisitionLineRateEnable", value);
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

        public double ResultingLineRateAbs => GetDoubleValue("ResultingLineRateAbs");

        public double LineDebouncingPeriod
        {
            get
            {
                return GetDoubleValue("LineDebouncingPeriod");
            }
            set
            {
                SetValue("LineDebouncingPeriod", value);
            }
        }

        public string ReverseScanDirection
        {
            get
            {
                return GetStringOfEnumValue("ReverseScanDirection");
            }
            set
            {
                SetEnumValue("ReverseScanDirection", value);
            }
        }

        public int Divider
        {
            get
            {
                return GetIntValue("Divider");
            }
            set
            {
                SetValue("Divider", value);
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

        public int RotaryEncoderMode
        {
            get
            {
                string str = GetStringOfEnumValue("RotaryEncoderMode");
                return (int)(RotaryEncoderModeEnum)Enum.Parse(typeof(RotaryEncoderModeEnum), str);
            }
            set
            {
                RotaryEncoderModeEnum rotaryEncoderModeEnum = (RotaryEncoderModeEnum)value;
                SetEnumValue("RotaryEncoderMode", rotaryEncoderModeEnum.ToString());
            }
        }

        public double GainRaw
        {
            get
            {
                return GetDoubleValue("GainRaw");
            }
            set
            {
                SetValue("GainRaw", value);
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

        public string IPAddress => GetStringValue("GevPersistentIPAddress");

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

        public event ImageArrivedEventHandler ImageArrivedEvent;

        public event LogPushEventHandler LogPushEvent;

        private void LogPush(string msg)
        {
            if (this.LogPushEvent != null)
            {
                this.LogPushEvent(msg);
            }
        }

        private void ImageArrived(object sender, ImageData image)
        {
            if (this.ImageArrivedEvent != null)
            {
                this.ImageArrivedEvent(sender, image);
            }
        }

        public DaHua_GigeLineScan(string sn)
        {
            m_serialNum = sn;
        }

        public static void EnumDevices(Action<string> showMsg)
        {
            SerialNums.Clear();
            mDic_Devices.Clear();
            try
            {
                List<IDeviceInfo> listDevice = Enumerator.EnumerateDevices();
                if (listDevice.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < listDevice.Count; i++)
                {
                    IDevice dev = Enumerator.GetDeviceByIndex(i);
                    IDeviceInfo devInfo = dev.DeviceInfo;
                    if (devInfo.Vendor.Contains("Huaray") && devInfo.Model.Substring(0, 3).Equals("L50"))
                    {
                        SerialNums.Add(devInfo.SerialNumber);
                        if (mDic_Devices.ContainsKey(devInfo.SerialNumber))
                        {
                            mDic_Devices[devInfo.SerialNumber] = dev;
                        }
                        else
                        {
                            mDic_Devices.Add(devInfo.SerialNumber, dev);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public bool Open()
        {
            bool bRet = false;
            try
            {
                if (!mDic_Devices.ContainsKey(m_serialNum))
                {
                    return false;
                }
                m_device = mDic_Devices[m_serialNum];
                m_device.CameraOpened += MyDevice_CameraOpened;
                m_device.ConnectionLost += MyDevice_ConnectionLost;
                m_device.CameraClosed += MyDevice_CameraClosed;
                if (!m_device.Open())
                {
                    bRet = false;
                    LogPush("Open camera failed");
                    return bRet;
                }
                bRet &= SetEnumValue("AcquisitionMode", AcquisitionModeEnum.Continuous);
                using (IFloatParameter floatParameter = m_device.ParameterCollection[ParametrizeNameSet.ExposureTime])
                {
                    floatParameter.SetValue(32.0);
                }
                using (IFloatParameter p = m_device.ParameterCollection[ParametrizeNameSet.GainRaw])
                {
                    p.SetValue(1.0);
                }
                m_device.StreamGrabber.SetBufferCount(8);
                m_device.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                bRet = true;
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Opening camera:" + ex.Message);
            }
            return bRet;
        }

        public void Close()
        {
            m_bStartGrab = false;
            try
            {
                if (m_device == null)
                {
                    LogPush("Device is invalid.");
                    return;
                }
                m_device.StreamGrabber.ImageGrabbed -= StreamGrabber_ImageGrabbed;
                m_device.ShutdownGrab();
                m_device.Close();
            }
            catch (Exception ex)
            {
                LogPush("Error Closing device:" + ex.Message);
            }
        }

        public void SoftwareTriggerDevice()
        {
            if (m_device.IsOpen && m_device.IsGrabbing)
            {
                bool bState;
                try
                {
                    bState = SetCommand("TriggerSoftware");
                }
                catch (Exception ex)
                {
                    LogPush(ex.Message);
                    bState = false;
                }
                if (!bState)
                {
                    LogPush("大华线扫相机" + m_serialNum + ",执行软触发命令失败！");
                }
            }
            else
            {
                LogPush($"相机打开状态{m_device.IsOpen},采集状态{m_device.IsGrabbing}");
            }
        }

        public void StartGrab()
        {
            if (m_device.IsOpen && m_device.IsGrabbing)
            {
                return;
            }
            try
            {
                if (!m_device.StreamGrabber.Start())
                {
                    LogPush("Start grabbing failed");
                    m_bStartGrab = false;
                }
                else
                {
                    bool isStart = m_device.StreamGrabber.IsStart;
                    m_bStartGrab = true;
                }
            }
            catch (Exception ex)
            {
                m_bStartGrab = false;
                LogPush("Error start grabbing:" + ex.Message);
            }
        }

        public void StopGrab()
        {
            if (!m_device.IsOpen || !m_device.IsGrabbing)
            {
                return;
            }
            try
            {
                if (!m_device.StreamGrabber.Stop())
                {
                    LogPush("Stop grabbing failed");
                }
                else
                {
                    m_bStartGrab = false;
                }
            }
            catch (Exception ex)
            {
                m_bStartGrab = false;
                LogPush("Error stop grabbing:" + ex.Message);
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

        private void StreamGrabber_ImageGrabbed(object sender, GrabbedEventArgs e)
        {
            try
            {
                IGrabbedRawData grebRawDate = e.GrabResult.Clone();
                int rawImageSize = grebRawDate.ImageSize;
                ImageData imagedata = new ImageData();
                if (CvtGvspPixelFormatType(grebRawDate.PixelFmt) == 1)
                {
                    imagedata.Data = grebRawDate.Image;
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
                        LogPush("图像转码出错！");
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
                        imagedata.Bitmap = bitmap;
                        if (colorPtr != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(colorPtr);
                        }
                    }
                }
                imagedata.Width = grebRawDate.Width;
                imagedata.Height = grebRawDate.Height;
                imagedata.ImageType = CvtGvspPixelFormatType(grebRawDate.PixelFmt);
                imagedata.ImageCount = grebRawDate.BlockID;
                ImageArrived(sender, imagedata);
            }
            catch (Exception ex)
            {
                LogPush("Error StreamGrabber_ImageGrabbed:" + ex.Message);
            }
        }

        private void MyDevice_CameraOpened(object sender, EventArgs e)
        {
        }

        private void MyDevice_CameraClosed(object sender, EventArgs e)
        {
        }

        private void MyDevice_ConnectionLost(object sender, EventArgs e)
        {
            m_bStartGrab = false;
            m_device.ShutdownGrab();
            m_device.Dispose();
            m_device = null;
        }

        private int GetIntValue(string featureName)
        {
            try
            {
                using IIntegraParameter p = m_device.ParameterCollection[new IntegerName(featureName)];
                return (int)p.GetValue();
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
                using IFloatParameter p = m_device.ParameterCollection[new FloatName(featureName)];
                return p.GetValue();
            }
            catch (Exception)
            {
                return -1.0;
            }
        }

        private bool GetBoolValue(string featureName)
        {
            try
            {
                using IBooleanParameter p = m_device.ParameterCollection[new BooleanName(featureName)];
                return p.GetValue();
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetStringValue(string featureName)
        {
            try
            {
                using IStringParameter p = m_device.ParameterCollection[new StringName(featureName)];
                return p.GetValue();
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        private string GetStringOfEnumValue(string featureName)
        {
            try
            {
                using IEnumParameter p = m_device.ParameterCollection[new EnumName(featureName)];
                return p.GetValue();
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        private void GetIntMinMax(string featureName, out int min, out int max)
        {
            try
            {
                using IIntegraParameter p = m_device.ParameterCollection[new IntegerName(featureName)];
                min = (int)p.GetMinimum();
                max = (int)p.GetMaximum();
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
                using IFloatParameter p = m_device.ParameterCollection[new FloatName(featureName)];
                min = p.GetMinimum();
                max = p.GetMaximum();
            }
            catch (Exception)
            {
                min = -1.0;
                max = -1.0;
            }
        }

        private bool SetValue(string featureName, int value)
        {
            bool bRet = false;
            try
            {
                using IIntegraParameter p = m_device.ParameterCollection[new IntegerName(featureName)];
                bRet = ValueIsValid(featureName, value) && p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetValueEx(string featureName, int value)
        {
            bool bRet = false;
            try
            {
                using IIntegraParameter p = m_device.ParameterCollection[new IntegerName(featureName)];
                bRet = p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetValue(string featureName, double value)
        {
            bool bRet = false;
            try
            {
                using IFloatParameter p = m_device.ParameterCollection[new FloatName(featureName)];
                bRet = ValueIsValid(featureName, value) && p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetValueEx(string featureName, double value)
        {
            bool bRet = false;
            try
            {
                using IFloatParameter p = m_device.ParameterCollection[new FloatName(featureName)];
                bRet = p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetValue(string featureName, bool value)
        {
            bool bRet = false;
            try
            {
                using IBooleanParameter p = m_device.ParameterCollection[new BooleanName(featureName)];
                bRet = p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetValue(string featureName, string value)
        {
            bool bRet = false;
            try
            {
                using IStringParameter p = m_device.ParameterCollection[new StringName(featureName)];
                bRet = p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetEnumValue(string featureName, string value)
        {
            bool bRet = false;
            try
            {
                using IEnumParameter p = m_device.ParameterCollection[new EnumName(featureName)];
                bRet = p.SetValue(value);
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private bool SetCommand(string featureName)
        {
            bool bRet = false;
            try
            {
                using ICommandParameter p = m_device.ParameterCollection[new CommandName(featureName)];
                bRet = p.Execute();
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
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

        private void SetWorkMode(TriggerMode value)
        {
            switch (value)
            {
                case TriggerMode.Continuous:
                    SetContinuousMode();
                    break;
                case TriggerMode.TimeSoftware:
                    SetTimeSoftwareMode();
                    break;
                case TriggerMode.RotarySoftware:
                    SetRotarySoftwareMode();
                    break;
                case TriggerMode.TimeHardware:
                    SetTimeHardwareMode();
                    break;
                case TriggerMode.RotaryHardware:
                    SetRotaryHardwareMode();
                    break;
                case TriggerMode.FrameBurst:
                    SetFrameBurstMode();
                    break;
            }
            workMode = (int)value;
        }

        private bool SetContinuousMode()
        {
            bool bRet = true;
            try
            {
                bRet &= SetEnumValue("AcquisitionMode", AcquisitionModeEnum.Continuous);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.LineStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                if (!bRet)
                {
                    LogPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Setting workMode:" + ex.Message);
            }
            return bRet;
        }

        private bool SetTimeSoftwareMode()
        {
            bool bRet = true;
            try
            {
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.LineStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.Software.ToString());
                if (!bRet)
                {
                    LogPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Setting workMode:" + ex.Message);
            }
            return bRet;
        }

        private bool SetRotarySoftwareMode()
        {
            bool bRet = true;
            try
            {
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.LineStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.FrequencyConverter0.ToString());
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.Software.ToString());
                bRet &= SetEnumValue("FrequencyConverterSelector", "FrequencyConverter0");
                bRet &= SetEnumValue("InputSource", "RotaryEncoder0");
                bRet &= SetEnumValue("RotaryEncoderLineSelector", "PhaseA");
                bRet &= SetEnumValue("RotaryEncoderLineSource", "Line1");
                bRet &= SetEnumValue("RotaryEncoderLineSelector", "PhaseB");
                bRet &= SetEnumValue("RotaryEncoderLineSource", "Line2");
                bRet &= SetEnumValue("LineSelector", "Line1");
                bRet &= SetEnumValue("LineFormat", "RS422");
                bRet &= SetEnumValue("LineSelector", "Line2");
                bRet &= SetEnumValue("LineFormat", "RS422");
                if (!bRet)
                {
                    LogPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Setting workMode:" + ex.Message);
            }
            return bRet;
        }

        private bool SetTimeHardwareMode()
        {
            bool bRet = true;
            try
            {
                bRet &= SetEnumValue("AcquisitionMode", AcquisitionModeEnum.Continuous);
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.Line5.ToString());
                bRet &= SetEnumValue("TriggerActivation", TriggerActivation.RisingEdge.ToString());
                bRet &= SetEnumValue("LineSelector", "Line5");
                bRet &= SetEnumValue("LineFormat", "OptoCoupled");
                if (!bRet)
                {
                    LogPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Setting workMode:" + ex.Message);
            }
            return bRet;
        }

        private bool SetRotaryHardwareMode()
        {
            bool bRet = true;
            try
            {
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.Line5.ToString());
                bRet &= SetEnumValue("TriggerActivation", TriggerActivation.RisingEdge.ToString());
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.LineStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.FrequencyConverter0.ToString());
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("FrequencyConverterSelector", "FrequencyConverter0");
                bRet &= SetEnumValue("InputSource", "RotaryEncoder0");
                bRet &= SetEnumValue("RotaryEncoderLineSelector", "PhaseA");
                bRet &= SetEnumValue("RotaryEncoderLineSource", "Line1");
                bRet &= SetEnumValue("RotaryEncoderLineSelector", "PhaseB");
                bRet &= SetEnumValue("RotaryEncoderLineSource", "Line2");
                bRet &= SetEnumValue("LineSelector", "Line1");
                bRet &= SetEnumValue("LineFormat", "RS422");
                bRet &= SetEnumValue("LineSelector", "Line2");
                bRet &= SetEnumValue("LineFormat", "RS422");
                bRet &= SetEnumValue("LineSelector", "Line5");
                bRet &= SetEnumValue("LineFormat", "OptoCoupled");
                if (!bRet)
                {
                    LogPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Setting workMode:" + ex.Message);
            }
            return bRet;
        }

        private bool SetFrameBurstMode()
        {
            bool bRet = true;
            try
            {
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.LineStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.FrequencyConverter0.ToString());
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstActive.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.Off);
                bRet &= SetEnumValue("TriggerSelector", TriggerSelector.FrameBurstStart.ToString());
                bRet &= SetEnumValue("TriggerMode", TriggerModeEnum.On);
                bRet &= SetEnumValue("TriggerSource", TriggerSource.Software.ToString());
                bRet &= SetEnumValue("FrequencyConverterSelector", "FrequencyConverter0");
                bRet &= SetEnumValue("InputSource", "RotaryEncoder0");
                bRet &= SetEnumValue("RotaryEncoderLineSelector", "PhaseA");
                bRet &= SetEnumValue("RotaryEncoderLineSource", "Line1");
                bRet &= SetEnumValue("RotaryEncoderLineSelector", "PhaseB");
                bRet &= SetEnumValue("RotaryEncoderLineSource", "Line2");
                bRet &= SetEnumValue("LineSelector", "Line1");
                bRet &= SetEnumValue("LineFormat", "RS422");
                bRet &= SetEnumValue("LineSelector", "Line2");
                bRet &= SetEnumValue("LineFormat", "RS422");
                if (!bRet)
                {
                    LogPush("Setting workMode failed");
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                LogPush("Error Setting workMode:" + ex.Message);
            }
            return bRet;
        }
    }
}
