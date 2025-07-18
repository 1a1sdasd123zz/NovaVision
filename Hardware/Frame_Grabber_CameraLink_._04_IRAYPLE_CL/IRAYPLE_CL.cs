using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CaptureCard_Net;
using NovaVision.BaseClass;
using System.Drawing.Imaging;
using System.Drawing;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_._04_IRAYPLE_CL;

/// <summary>
/// CameraLink相机
/// </summary>
public class IRAYPLE_CL : Camera2DBase
{
    public static readonly Dictionary<string, CardDev> Card_devices = new();
    public static readonly Dictionary<string, uint> Card_devices_uIndex = new();
    private static readonly Dictionary<string, CamDev> Camera_devices = new();//只考虑一张卡挂载一个相机的情况
    //public static Dictionary<string, uint> Card_devices_uIndex = new();
    private readonly CardDev card = new CardDev();
    readonly CamDev camera = new CamDev();

    private IMVFGDefine.IMV_FG_FrameCallBack frameCallBack;

    private int InputCount = 0;
    //private uint uIndex = 0;

    public IRAYPLE_CL(string sn)
    {
        SN = sn;
        card = Card_devices[SN];
        //camera = Camera_devices[SN];
    }

    public override double Exposure
    {
        get
        {
            try
            {
                double stParam = new double();
                if (camera.IMV_FG_GetDoubleFeatureValue("ExposureTime", ref stParam) == 0)
                {
                    return stParam;
                }
            }
            catch (Exception)
            {
            }
            return 0;
        }
        set
        {
            if (Exposure != value)
            {
                var res = camera.IMV_FG_SetDoubleFeatureValue("ExposureTime", value);
                if (res != 0)
                {
                    LogUtil.LogError("Set ExposureTime feature value failed!");
                }
            }
        }
    }

    public override double Gain
    {
        get
        {
            try
            {
                double stParam = new double();
                if (camera.IMV_FG_GetDoubleFeatureValue("GainRaw", ref stParam) == 0)
                {
                    return stParam;
                }
            }
            catch (Exception)
            {
            }
            return 0;
        }
        set
        {
            if (Gain != value)
            {
                var res = camera.IMV_FG_SetDoubleFeatureValue("GainRaw", value);
                if (res != 0)
                {
                    LogUtil.LogError("Set GainRaw feature value failed!");
                }
            }
        }
    }
    public static void EnumCameras()
    {
        try
        {
            LogUtil.Log($"SDK Version:{CardDev.IMV_FG_GetVersion()}");
            LogUtil.Log("Enum capture board interface info.");
            //枚举采集卡设备
            // Discover capture board device
            IMVFGDefine.IMV_FG_INTERFACE_INFO_LIST interfaceList = new IMVFGDefine.IMV_FG_INTERFACE_INFO_LIST();
            IMVFGDefine.IMV_FG_EInterfaceType interfaceTp = IMVFGDefine.IMV_FG_EInterfaceType.typeCLInterface;
            var res = CardDev.IMV_FG_EnumInterface((uint)interfaceTp, ref interfaceList);

            if (res != IMVFGDefine.IMV_FG_OK)
            {
                LogUtil.LogError($"Enumeration devices failed!errorCode:[{res}]");
                return;
            }
            if (interfaceList.nInterfaceNum == 0)
            {
                LogUtil.LogError($"No board device find.errorCode:[{res}]");
                return;
            }

            for (uint i = 0; i < interfaceList.nInterfaceNum; i++)
            {
                IMVFGDefine.IMV_FG_INTERFACE_INFO interfaceinfo = (IMVFGDefine.IMV_FG_INTERFACE_INFO)Marshal.PtrToStructure(
        interfaceList.pInterfaceInfoList + (int)(Marshal.SizeOf(typeof(IMVFGDefine.IMV_FG_INTERFACE_INFO)) * i),
        typeof(IMVFGDefine.IMV_FG_INTERFACE_INFO));

                // 打开采集卡设备
                //Open capture device
                LogUtil.Log("Open capture device.");

                CardDev c = new();
                //res = c.IMV_FG_OpenInterface(i);
                //if (res != IMVFGDefine.IMV_FG_OK)
                //{
                //    LogUtil.LogError($"Open cameralink capture board device failed!errorCode:[{res}]");
                //    return;
                //}

                if (!Card_devices.ContainsKey(interfaceinfo.serialNumber))
                {
                    Card_devices.Add(interfaceinfo.serialNumber, c);
                    Card_devices_uIndex.Add(interfaceinfo.serialNumber, i);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override int OpenCamera()
    {
        int res;
        if (card.IMV_FG_IsOpenInterface())
            return 0;
        res = card.IMV_FG_OpenInterface(Card_devices_uIndex[SN]);
        if (res != IMVFGDefine.IMV_FG_OK)
        {
            LogUtil.LogError($"Open cameralink capture board device failed!errorCode:[{res}]");
            return -1;
        }

        LogUtil.Log("Enum camera device.");
        // Discover capture board device
        IMVFGDefine.IMV_FG_EInterfaceType interfaceTp = IMVFGDefine.IMV_FG_EInterfaceType.typeCLInterface;
        IMVFGDefine.IMV_FG_DEVICE_INFO_LIST camListPtr = new IMVFGDefine.IMV_FG_DEVICE_INFO_LIST();
        //枚举相机设备
        // discover camera 

        res = CamDev.IMV_FG_EnumDevices((uint)interfaceTp, ref camListPtr);
        // 打开采集卡相机设备 
        // Connect to CamDev 
        if (camListPtr.nDevNum == 0)
        {
            LogUtil.LogError($"No camera device find.errorCode:[{res}]");
            return -1;
        }
        for (int j = 0; j < camListPtr.nDevNum; j++)
        {
            IMVFGDefine.IMV_FG_DEVICE_INFO devinfo = (IMVFGDefine.IMV_FG_DEVICE_INFO)Marshal.PtrToStructure(
                camListPtr.pDeviceInfoList + (int)(Marshal.SizeOf(typeof(IMVFGDefine.IMV_FG_DEVICE_INFO)) * j),
                typeof(IMVFGDefine.IMV_FG_DEVICE_INFO));
            if (!Camera_devices.ContainsKey(devinfo.serialNumber))
            {
                res = camera.IMV_FG_OpenDevice(IMVFGDefine.IMV_FG_ECreateHandleMode.IMV_FG_MODE_BY_INDEX, j);
                if (res == IMVFGDefine.IMV_FG_OK)
                {
                    Camera_devices.Add(SN, camera);
                    break;
                }

                if (res != IMVFGDefine.IMV_FG_OK && j == camListPtr.nDevNum - 1)
                {
                    //无法打开相机，说明该相机不在当前采集卡或者
                    LogUtil.LogError($"Enumeration camera devices failed!errorCode:[{res}]");
                    return -1;
                }
            }
        }
        isConnected = true;
        camErrCode = CamErrCode.ConnectSuccess;
        //设置缓存个数为8
        //set buffer count to 8
        res = card.IMV_FG_SetBufferCount(4);
        frameCallBack = new IMVFGDefine.IMV_FG_FrameCallBack(onGetFrame);
        res = card.IMV_FG_AttachGrabbing(frameCallBack, IntPtr.Zero);
        if (0 != Start_Grab())
            return -1;
        if (!CameraOperator.camera2DCollection._2DCameras.ContainsKey(SN))
        {
            CameraOperator.camera2DCollection.Add(SN, this);
            return 0;
        }

        return -1;
    }


    // 数据帧回调函数
    // Data frame callback function
    private void onGetFrame(ref IMVFGDefine.IMV_FG_Frame frame, IntPtr pUser)
    {
        IMVFGDefine.IMV_FG_String name = new IMVFGDefine.IMV_FG_String();
        card.IMV_FG_GetStringFeatureValue("DeviceUserID", ref name);
        LogUtil.Log($"[{name.str}]进入回调");
        if (frame.frameHandle == IntPtr.Zero)
        {
            LogUtil.LogError("frame is NULL");
            return;
        }
        try
        {
            Bitmap bmp = null;
            Cognex.VisionPro.CogImage8Grey cogImage = null;
            ConvertToBitmap(ref frame, ref bmp);
            if (frame.frameInfo.pixelFormat == IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_Mono8)
            {
                cogImage = new Cognex.VisionPro.CogImage8Grey(bmp);
            }
            else if (frame.frameInfo.pixelFormat == IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_RGB8)
            {

            }
            ImageData imageData = new ImageData(cogImage);
            UpdateImage?.Invoke(imageData);
            bmp.Dispose();
        }
        catch (Exception ex)
        {
            LogUtil.LogError("大华图像回调处理异常" + ex);
        }
    }


    /// <summary>
    /// 指针之间进行数据拷贝
    /// </summary>
    /// <param name="pDst">目标地址</param>
    /// <param name="pSrc">源地址</param>
    /// <param name="len">拷贝数据长度</param>
    [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", CharSet = CharSet.Ansi)]
    internal static extern void CopyMemory(IntPtr pDst, IntPtr pSrc, int len);
    private bool ConvertToBitmap(ref IMVFGDefine.IMV_FG_Frame frame, ref Bitmap bitmap)
    {
        IntPtr pDstRGB;
        BitmapData bmpData;
        Rectangle bitmapRect = new Rectangle();

        // mono8和BGR8裸数据不需要转码
        // mono8 and BGR8 raw data is not need to convert
        if (frame.frameInfo.pixelFormat != IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_Mono8
           && frame.frameInfo.pixelFormat != IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_BGR8)
        {
            IMVFGDefine.IMV_FG_PixelConvertParam stPixelConvertParam = new IMVFGDefine.IMV_FG_PixelConvertParam();
            int res = IMVFGDefine.IMV_FG_OK;

            //转目标内存 彩色
            var ImgSize = (int)frame.frameInfo.width * (int)frame.frameInfo.height * 3;

            //当内存申请失败，返回false
            try
            {
                pDstRGB = Marshal.AllocHGlobal(ImgSize);
            }
            catch
            {
                return false;
            }
            if (pDstRGB == IntPtr.Zero)
            {
                return false;
            }

            // 图像转换成BGR8
            // convert image to BGR8
            stPixelConvertParam.nWidth = frame.frameInfo.width;
            stPixelConvertParam.nHeight = frame.frameInfo.height;
            stPixelConvertParam.ePixelFormat = frame.frameInfo.pixelFormat;
            stPixelConvertParam.pSrcData = frame.pData;
            stPixelConvertParam.nSrcDataLen = frame.frameInfo.size;
            stPixelConvertParam.nPaddingX = frame.frameInfo.paddingX;
            stPixelConvertParam.nPaddingY = frame.frameInfo.paddingY;
            stPixelConvertParam.eBayerDemosaic = IMVFGDefine.IMV_FG_EBayerDemosaic.IMV_FG_DEMOSAIC_NEAREST_NEIGHBOR;
            stPixelConvertParam.eDstPixelFormat = IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_BGR8;
            stPixelConvertParam.pDstBuf = pDstRGB;
            stPixelConvertParam.nDstBufSize = (uint)ImgSize;

            res = card.IMV_FG_PixelConvert(ref stPixelConvertParam);
            if (res != IMVFGDefine.IMV_FG_OK)
            {
                Console.WriteLine("image convert to BGR failed!");
                return false;
            }
        }
        else
        {
            pDstRGB = frame.pData;
        }

        if (frame.frameInfo.pixelFormat == IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_Mono8)
        {
            // 用Mono8数据生成Bitmap
            bitmap = new Bitmap((int)frame.frameInfo.width, (int)frame.frameInfo.height, PixelFormat.Format8bppIndexed);
            ColorPalette colorPalette = bitmap.Palette;
            for (int i = 0; i != 256; ++i)
            {
                colorPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            bitmap.Palette = colorPalette;

            bitmapRect.Height = bitmap.Height;
            bitmapRect.Width = bitmap.Width;
            bmpData = bitmap.LockBits(bitmapRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            CopyMemory(bmpData.Scan0, pDstRGB, bmpData.Stride * bitmap.Height);
            bitmap.UnlockBits(bmpData);
        }
        else
        {
            // 用BGR24数据生成Bitmap
            bitmap = new Bitmap((int)frame.frameInfo.width, (int)frame.frameInfo.height, PixelFormat.Format24bppRgb);
            bitmapRect.Height = bitmap.Height;
            bitmapRect.Width = bitmap.Width;
            bmpData = bitmap.LockBits(bitmapRect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            CopyMemory(bmpData.Scan0, pDstRGB, bmpData.Stride * bitmap.Height);
            bitmap.UnlockBits(bmpData);
            if (frame.frameInfo.pixelFormat != IMVFGDefine.IMV_FG_EPixelType.IMV_FG_PIXEL_TYPE_BGR8)
            {
                Marshal.FreeHGlobal(pDstRGB);
            }
        }

        return true;
    }

    public int Start_Grab()
    {
        if (card.IMV_FG_IsGrabbing())
            return 0;
        try
        {
            // 开始拉流 
            // Start grabbing
            var res = card.IMV_FG_StartGrabbing();
            if (res != 0)
            {
                LogUtil.LogError("Start grabbing failed!");
                return -1;
            }

            return 0;
        }
        catch (Exception ex)
        {
            LogUtil.LogError("Start grabbing error!" + ex);
            return -1;
        }
    }

    public int Stop_Grab()
    {
        if (!card.IMV_FG_IsGrabbing())
            return 0;
        try
        {
            var res = card.IMV_FG_StopGrabbing();
            if (res == 0)
            {
                return 0;
            }
            return -1;
        }
        catch (Exception ex)
        {
            LogUtil.LogError("Stop grabbing error!" + ex);
            return -1;
        }
    }

    public override void SetExposure(double exposure)
    {
        Exposure = exposure;
    }

    public override void SetGain(double gain)
    {
        Gain = gain;
    }
    public override int CloseCamera()
    {
        if (card.IMV_FG_IsGrabbing())
            card.IMV_FG_StopGrabbing();
        if (camera.IMV_FG_IsDeviceOpen())
        {
            camera.IMV_FG_CloseDevice();
        }
        if (card.IMV_FG_IsOpenInterface())
        {
            card.IMV_FG_CloseInterface();
        }
        return -1;
    }
}
