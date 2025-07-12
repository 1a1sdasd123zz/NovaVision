using Cognex.VisionPro;
using NovaVision.BaseClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NvtLvmSdk;

namespace NovaVision.Hardware._010_SDK_LVM3DTool
{
    public class Camera_LVM3D : Camera3DBase
    {
        public CameraApi camera_api = new CameraApi();

        public int dev_id;

        private string cameraName = "dev_0";

        private CameraModel.get_frame_cb_t cb_pcld = null;

        public CameraModel.grab_mode_t grab_mode;

        public int connect_type = 0;

        public CameraModel.lvm_depth_map_t depth_map;

        public CameraModel.lvm_point_cloud_t point_cloud;

        public CameraModel.lvm_image_t image;

        [NonSerialized]
        private SafeBufferExt _bufHeight;

        private bool ConvertFlag = false;

        public CameraModel.dev_info_t Dev_Info_T = default(CameraModel.dev_info_t);

        public CameraModel.dev_img_param_t Img_Param_T = default(CameraModel.dev_img_param_t);

        public CameraModel.dev_pcld_param_t Pcld_Param_T = default(CameraModel.dev_pcld_param_t);

        public CameraModel.dev_depth_map_param_t Depth_Param_T = default(CameraModel.dev_depth_map_param_t);

        public CameraModel.dev_trigger_param_t Trigger_Param_T = default(CameraModel.dev_trigger_param_t);

        public CameraModel.dev_motor_param_t Motor_Param_T = default(CameraModel.dev_motor_param_t);

        public CameraModel.custom_calib_t Calib_Param_T = default(CameraModel.custom_calib_t);

        public bool CCDGrabState = false;

        public Camera_LVM3D(string externIp)
        {
            _cameraIp = externIp;
            dev_id = Convert.ToInt32(externIp.Split('.')[2]);
            cameraName = string.Format($"{dev_id}#Camera");
            _cameraVendor = CameraBase.Cam3DVendor[2];
        }

        public string GetSDKVersion()
        {
            return camera_api.GetSDKVersion();
        }

        public bool Camera_Connect(CameraModel.get_frame_cb_t cb = null)
        {
            bool ret = false;
            if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
            {
                ret = camera_api.Cam_Connect(cameraName, _cameraIp);
            }
            else if (CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV == camera_api.connect_type)
            {
                ret = camera_api.Cam_Group_Connect(cameraName, _cameraIp);
            }
            else if (CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV == camera_api.connect_type)
            {
                camera_api.Dev_Handle_List.Clear();
                ret = camera_api.Cam_Connect(cameraName, _cameraIp);
                camera_api.Dev_Handle_List.Add(camera_api.devHandle);
            }
            if (ret)
            {
                if (cb != null)
                {
                    cb_pcld = cb.Invoke;
                    GC.KeepAlive(cb_pcld);
                }
                if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
                {
                    CameraModel.lvm_dev_t dev_T = Marshal.PtrToStructure<CameraModel.lvm_dev_t>(camera_api.devHandle);
                    if (dev_T.error_type != 25)
                    {
                        return false;
                    }
                    _version = GetSDKVersion();
                    InitPara(ref dev_T);
                }
                else if (CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV == camera_api.connect_type)
                {
                    CameraModel.lvm_dev_group_t dev_group_T = Marshal.PtrToStructure<CameraModel.lvm_dev_group_t>(camera_api.Dev_Group_Handle);
                    for (int j = 0; j < dev_group_T.dev_num; j++)
                    {
                        InitPara(ref dev_group_T.dev[j]);
                        if (j == 0)
                        {
                            dev_id = dev_group_T.dev[j].dev_id;
                        }
                        else if (1 != j)
                        {
                        }
                    }
                }
                else if (CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV == camera_api.connect_type)
                {
                    for (int i = 0; i < camera_api.Dev_Handle_List.Count; i++)
                    {
                        camera_api.devHandle = camera_api.Dev_Handle_List[i];
                        CameraModel.lvm_dev_t dev_T2 = Marshal.PtrToStructure<CameraModel.lvm_dev_t>(camera_api.devHandle);
                        InitPara(ref dev_T2);
                    }
                }
                return true;
            }
            return false;
        }

        public override int Open_Sensor()
        {
            camera_api.connect_type = CameraModel.connect_mode_t.SINGEL_DEV;
            if (Camera_Connect(frame_callback))
            {
                isConnected = true;
                camErrCode = CamErrCode.ConnectSuccess;
                if (cam_Handle != null)
                {
                    CameraMessage cameraMessage = new CameraMessage(_cameraSn, true);
                    cam_Handle.CamStateChangeHandle(cameraMessage);
                }
                return 0;
            }
            isConnected = false;
            camErrCode = CamErrCode.ConnectFailed;
            LogUtil.LogError(CameraBase.Cam3DVendor[2] + ":" + _cameraSn + " 相机连接失败");
            return -1;
        }

        public override void Close_Sensor()
        {
            Camera_Close();
            isConnected = false;
            laserState = false;
            camErrCode = CamErrCode.ConnectFailed;
            CameraOperator.camera3DCollection.Remove(_cameraSn);
            if (cam_Handle != null)
            {
                CameraMessage cameraMessage = new CameraMessage(_cameraSn, false);
                cam_Handle.CamStateChangeHandle(cameraMessage);
            }
        }

        public bool Camera_Close()
        {
            ClearnData();
            camera_api.Cam_Close();
            return true;
        }

        public bool StartGrab(string mode, int buffer_num = 2)
        {
            ClearnData();
            switch (mode)
            {
                case "深度图":
                    grab_mode = CameraModel.grab_mode_t.DEPTH_MAP;
                    break;
                case "点云":
                    grab_mode = CameraModel.grab_mode_t.POINT_CLOUD;
                    break;
                case "图像":
                    grab_mode = CameraModel.grab_mode_t.IMAGE;
                    break;
                default:
                    grab_mode = CameraModel.grab_mode_t.DEPTH_MAP;
                    break;
            }
            bool ret = false;
            if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
            {
                ret = camera_api.GrabStart(grab_mode, buffer_num, cb_pcld);
            }
            else if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV)
            {
                CameraModel.lvm_dev_group_t dev_group_T = Marshal.PtrToStructure<CameraModel.lvm_dev_group_t>(camera_api.Dev_Group_Handle);
                for (int j = 0; j < dev_group_T.dev_num; j++)
                {
                    IntPtr handle_T = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CameraModel.lvm_dev_t)));
                    dev_group_T.dev[j].grab_mode = grab_mode;
                    Marshal.StructureToPtr(dev_group_T.dev[j], handle_T, fDeleteOld: true);
                    camera_api.devHandle = handle_T;
                    ret = camera_api.GrabStart(grab_mode, buffer_num);
                    dev_group_T.dev[j] = Marshal.PtrToStructure<CameraModel.lvm_dev_t>(camera_api.devHandle);
                    Marshal.StructureToPtr(dev_group_T, camera_api.Dev_Group_Handle, fDeleteOld: true);
                }
            }
            else if (camera_api.connect_type == CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV)
            {
                for (int i = 0; i < camera_api.Dev_Handle_List.Count; i++)
                {
                    camera_api.devHandle = camera_api.Dev_Handle_List[i];
                    ret = camera_api.GrabStart(grab_mode, buffer_num);
                }
            }
            return ret;
        }

        public override int Start_Grab(bool state)
        {
            if (triggerMode3D == TriggerMode3D.Encoder_Software || triggerMode3D == TriggerMode3D.Time_Software)
            {
                return 0;
            }
            if (StartGrab("深度图"))
            {
                CCDGrabState = true;
                return 0;
            }
            CCDGrabState = false;
            LogUtil.LogError(CameraBase.Cam3DVendor[2] + ":" + _cameraSn + " 开始采集失败");
            return -1;
        }

        public bool StopGrab()
        {
            bool ret = false;
            if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
            {
                ret = camera_api.GrabStop();
            }
            else if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV)
            {
                CameraModel.lvm_dev_group_t dev_group = Marshal.PtrToStructure<CameraModel.lvm_dev_group_t>(camera_api.Dev_Group_Handle);
                for (int j = 0; j < dev_group.dev_num; j++)
                {
                    IntPtr handle_T = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CameraModel.lvm_dev_t)));
                    Marshal.StructureToPtr(dev_group.dev[j], handle_T, fDeleteOld: false);
                    camera_api.devHandle = handle_T;
                    ret = camera_api.GrabStop();
                    dev_group.dev[j] = Marshal.PtrToStructure<CameraModel.lvm_dev_t>(camera_api.devHandle);
                    Marshal.StructureToPtr(dev_group, camera_api.Dev_Group_Handle, fDeleteOld: true);
                }
            }
            else if (camera_api.connect_type == CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV)
            {
                for (int i = 0; i < camera_api.Dev_Handle_List.Count; i++)
                {
                    camera_api.devHandle = camera_api.Dev_Handle_List[i];
                    ret = camera_api.GrabStop();
                }
            }
            return ret;
        }

        public override int SoftTriggerOnce()
        {
            acqOk = false;
            bStopFlag = false;
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = default(TimeSpan);
            if (StartGrab("深度图"))
            {
                int locationStart = 0;
                int counterStart = 0;
                int pulseStart = 0;
                int ret = get_encoder_para(camera_api.devHandle, ref locationStart, ref counterStart, ref pulseStart);
                CCDGrabState = true;
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
                    int location = 0;
                    int counter = 0;
                    int pulse = 0;
                    ret += get_encoder_para(camera_api.devHandle, ref location, ref counter, ref pulse);
                    if (ret == 0)
                    {
                        LogUtil.Log(CameraBase.Cam3DVendor[2] + ":" + _cameraSn + "编码器计数获取成功");
                    }
                    else
                    {
                        LogUtil.Log(CameraBase.Cam3DVendor[2] + ":" + _cameraSn + "编码器计数获取失败");
                    }
                    if (timeSpan.TotalMilliseconds > timeout)
                    {
                        LogUtil.Log($"{CameraBase.Cam3DVendor[2]}:{_cameraSn} 采集超时，起始：{pulseStart}[counter{counterStart}]，结束：{pulse}[counter{counter}]，接收脉冲数：{pulse - pulseStart}，行程：{(double)(pulse - pulseStart) * SettingParams.EncoderResolution}mm");
                        Task.Run(delegate
                        {
                            Bitmap bmp = new Bitmap(100, 100);
                            CogImage16Range externCogImage = new CogImage16Range(bmp);
                            ImageData obj = new ImageData(externCogImage);
                            if (UpdateImage != null)
                            {
                                UpdateImage(obj);
                            }
                        });
                    }
                    else
                    {
                        LogUtil.Log($"{CameraBase.Cam3DVendor[2]}:{_cameraSn} 采集正常，起始：{pulseStart}[counter{counterStart}]，结束：{pulse}[counter{counter}]，接收脉冲数：{pulse - pulseStart}，行程：{(double)(pulse - pulseStart) * SettingParams.EncoderResolution}mm");
                    }
                });
                laserState = false;
                return 0;
            }
            CCDGrabState = false;
            LogUtil.LogError(CameraBase.Cam3DVendor[2] + ":" + _cameraSn + " 软触发失败！");
            return -1;
        }

        public override int Stop_Grab(bool state)
        {
            if (!CCDGrabState)
            {
                return 0;
            }
            if (StopGrab())
            {
                bStopFlag = true;
                laserState = false;
                return 0;
            }
            LogUtil.LogError(CameraBase.Cam3DVendor[2] + ":" + _cameraSn + " 停止采集失败");
            return -1;
        }

        public bool GetData(ref object objData)
        {
            bool flag = false;
            switch (grab_mode)
            {
                case CameraModel.grab_mode_t.IMAGE:
                    flag = camera_api.GetImageData(ref image);
                    objData = image;
                    return flag;
                case CameraModel.grab_mode_t.POINT_CLOUD:
                    flag = camera_api.GetPcldData(ref point_cloud);
                    objData = point_cloud;
                    return flag;
                case CameraModel.grab_mode_t.DEPTH_MAP:
                    flag = camera_api.GetDepthData(ref depth_map);
                    objData = depth_map;
                    return flag;
                default:
                    return false;
            }
        }

        public bool GetData()
        {
            bool flag = false;
            switch (grab_mode)
            {
                case CameraModel.grab_mode_t.IMAGE:
                    flag = camera_api.GetImageData(ref image);
                    break;
                case CameraModel.grab_mode_t.POINT_CLOUD:
                    flag = camera_api.GetPcldData(ref point_cloud);
                    break;
                case CameraModel.grab_mode_t.DEPTH_MAP:
                    flag = camera_api.GetDepthData(ref depth_map);
                    break;
                default:
                    return false;
            }
            return flag;
        }

        public bool ConvertDepthToPcld()
        {
            if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
            {
                ConvertFlag = camera_api.ConvertMapToPcld(ref depth_map, ref point_cloud);
            }
            else if (camera_api.connect_type != CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV && camera_api.connect_type != CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV)
            {
            }
            return ConvertFlag;
        }

        public bool FreeMemory()
        {
            return camera_api.GetDataDone();
        }

        public void ClearnData()
        {
            if (grab_mode == CameraModel.grab_mode_t.DEPTH_MAP && ConvertFlag)
            {
                Marshal.FreeHGlobal(point_cloud.p);
            }
        }

        public bool SavePara()
        {
            return camera_api.SavePara();
        }

        public void InitPara(ref CameraModel.lvm_dev_t dev_T)
        {
            Dev_Info_T = Marshal.PtrToStructure<CameraModel.dev_info_t>(dev_T.dev_info);
            Img_Param_T = Marshal.PtrToStructure<CameraModel.dev_img_param_t>(dev_T.img_param);
            Trigger_Param_T = Marshal.PtrToStructure<CameraModel.dev_trigger_param_t>(dev_T.trigger_param);
            Pcld_Param_T = Marshal.PtrToStructure<CameraModel.dev_pcld_param_t>(dev_T.pcld_param);
            Depth_Param_T = Marshal.PtrToStructure<CameraModel.dev_depth_map_param_t>(dev_T.depth_map_param);
            Motor_Param_T = Marshal.PtrToStructure<CameraModel.dev_motor_param_t>(dev_T.motor_param);
            Calib_Param_T = Marshal.PtrToStructure<CameraModel.custom_calib_t>(dev_T.calib_para);
            char[] modelTypeArray = Dev_Info_T.dev_type;
            _cameraModelName = CharArrayToString(modelTypeArray);
            char[] snArray = Dev_Info_T.sn;
            _cameraSn = CharArrayToString(snArray);
            char[] ipArray = Dev_Info_T.ip;
            _cameraIp = CharArrayToString(ipArray);
            exposure = GetExposure();
            _acqLineRate = GetAcqLineRate();
            GetScanLength();
            GetYPitch();
            if (!CameraOperator.camera3DCollection._3DCameras.ContainsKey(_cameraSn))
            {
                CameraOperator.camera3DCollection.Add(_cameraSn, this);
            }
        }

        public void GetErrorCode(ref string errorStr)
        {
            switch (Marshal.PtrToStructure<CameraModel.lvm_dev_t>(camera_api.devHandle).error_type)
            {
                case 32:
                    errorStr = "SDK输入出错，请检查输入！";
                    break;
                case 49:
                    errorStr = "SDK内存分配失败！";
                    break;
                case 39:
                    errorStr = "SDK中上一个数据未处理结束！";
                    break;
                case 33:
                    errorStr = "SDK打开设备失败！";
                    break;
                case 546:
                    errorStr = "SDK获取设备参数失败！";
                    break;
                case 35:
                    errorStr = "SDK已经注册回调函数！";
                    break;
                case 36:
                    errorStr = "SDK没有数据可以拿取！";
                    break;
                case 37:
                    errorStr = "SDK没有数据在缓存中！";
                    break;
                case 38:
                    errorStr = "SDK没有数据拿取，不需要报告拿取结束状态！";
                    break;
                case 40:
                    errorStr = "SDK上之前数据还没有使用！";
                    break;
                case 41:
                    errorStr = "SDK中参数设置有误，请检查参数设置！";
                    break;
            }
        }

        public override void SetExposure(ref double newExposure)
        {
            if (Img_Param_T.exposure_time != (int)newExposure)
            {
                Img_Param_T.exposure_time = (int)newExposure;
                if (SetImagePara(Img_Param_T))
                {
                    exposure = newExposure;
                }
                SettingParams.ExposureTime = (int)exposure;
            }
        }

        public int GetExposure()
        {
            return Img_Param_T.exposure_time;
        }

        public override void SetAcqLineRate(ref double newRate)
        {
            if (Trigger_Param_T.trigger_time != (float)newRate)
            {
                Trigger_Param_T.trigger_time = (float)newRate;
                if (SetTriggerPara(ref Trigger_Param_T))
                {
                    _acqLineRate = newRate;
                }
                SettingParams.AcqLineRate = (int)_acqLineRate;
            }
        }

        public float GetAcqLineRate()
        {
            return Trigger_Param_T.trigger_time;
        }

        public override void SetTriggerMode(TriggerMode3D triggerMode)
        {
            triggerMode3D = triggerMode;
            Trigger_Param_T.trigger_number_ctrl = 1;
            Trigger_Param_T.trigger_number = -1;
            switch (triggerMode)
            {
                case TriggerMode3D.Time_ExternTrigger:
                    Trigger_Param_T.trigger_src = GetTriggerMode(CameraModel.TriggerMode.TIME_TRIGGER);
                    Trigger_Param_T.trigger_ctrl_type = 1;
                    SetTriggerPara(ref Trigger_Param_T);
                    break;
                case TriggerMode3D.Time_Software:
                    Trigger_Param_T.trigger_src = GetTriggerMode(CameraModel.TriggerMode.TIME_TRIGGER);
                    Trigger_Param_T.trigger_ctrl_type = 0;
                    SetTriggerPara(ref Trigger_Param_T);
                    break;
                case TriggerMode3D.Encoder_ExternTrigger:
                    Trigger_Param_T.trigger_src = GetTriggerMode(CameraModel.TriggerMode.ECODER_AB);
                    Trigger_Param_T.trigger_ctrl_type = 1;
                    SetTriggerPara(ref Trigger_Param_T);
                    break;
                case TriggerMode3D.Encoder_Software:
                    Trigger_Param_T.trigger_src = GetTriggerMode(CameraModel.TriggerMode.ECODER_AB);
                    Trigger_Param_T.trigger_ctrl_type = 0;
                    SetTriggerPara(ref Trigger_Param_T);
                    break;
            }
            SettingParams.TriggerMode = (int)triggerMode;
        }

        public override void SetYPitch(ref double yPitch)
        {
            if (Calib_Param_T.dir[1] != yPitch)
            {
                Calib_Param_T.dir[1] = yPitch;
                if (SetCalibPara(Calib_Param_T))
                {
                    y_pitch_mm = yPitch;
                }
                SettingParams.y_pitch_mm = y_pitch_mm;
            }
        }

        public double GetYPitch()
        {
            y_pitch_mm = Calib_Param_T.dir[1];
            return y_pitch_mm;
        }

        public void SetScanLength(int newScanLength)
        {
            if (Pcld_Param_T.line_number != newScanLength)
            {
                Pcld_Param_T.line_number = newScanLength;
                if (SetPCLDPara(Pcld_Param_T))
                {
                    _profileCount = (uint)newScanLength;
                }
                Depth_Param_T.height = newScanLength;
                if (SetDepthMapPara(Depth_Param_T))
                {
                }
                SettingParams.ScanLines = (int)_profileCount;
                SettingParams.ScanLength = (int)_profileCount;
            }
        }

        public void SetROI_TopButtom(int newTop, int newButtom)
        {
            if (Pcld_Param_T.img_roi_top != newTop || Pcld_Param_T.img_roi_bottom != newButtom)
            {
                Pcld_Param_T.img_roi_top = newTop;
                Pcld_Param_T.img_roi_bottom = newButtom;
                if (SetPCLDPara(Pcld_Param_T))
                {
                    LogUtil.Log($"{_cameraIp}_LVM_ROI Top={Pcld_Param_T.img_roi_top},Buttom={Pcld_Param_T.img_roi_bottom}");
                }
                SettingParams.ROI_Top = newTop;
                SettingParams.ROI_Buttom = newButtom;
            }
        }

        public void SetROI_Top(int newTop)
        {
            if (Pcld_Param_T.img_roi_top != newTop)
            {
                Pcld_Param_T.img_roi_top = newTop;
                if (SetPCLDPara(Pcld_Param_T))
                {
                }
                SettingParams.ROI_Top = newTop;
            }
        }

        public void SetROI_Buttom(int newButtom)
        {
            if (Pcld_Param_T.img_roi_bottom != newButtom)
            {
                Pcld_Param_T.img_roi_bottom = newButtom;
                if (SetPCLDPara(Pcld_Param_T))
                {
                }
                SettingParams.ROI_Buttom = newButtom;
            }
        }

        public override void SetScanLines(ref int length)
        {
            SetScanLength(length);
        }

        public override void SetROI_Top_Buttom(ref int top, ref int buttom)
        {
            SetROI_TopButtom(top, buttom);
        }

        public override void SetROI_Top(ref int top)
        {
            SetROI_Top(top);
        }

        public override void SetROI_Buttom(ref int buttom)
        {
            SetROI_Buttom(buttom);
        }

        public uint GetScanLength()
        {
            _profileCount = (uint)Pcld_Param_T.line_number;
            return _profileCount;
        }

        public bool SetImagePara(CameraModel.dev_img_param_t Img_Param_T)
        {
            return camera_api.SetImageParam(ref Img_Param_T);
        }

        public bool SetPCLDPara(CameraModel.dev_pcld_param_t para)
        {
            return camera_api.SetPointCloudParam(ref para);
        }

        public bool SetDepthMapPara(CameraModel.dev_depth_map_param_t para)
        {
            return camera_api.SetDepthMapParam(ref para);
        }

        public bool SetTriggerSource(CameraModel.TriggerMode mode)
        {
            Trigger_Param_T.trigger_src = GetTriggerMode(mode);
            return camera_api.SetTriggerParam(ref Trigger_Param_T);
        }

        public bool SetTriggerPara(ref CameraModel.dev_trigger_param_t para)
        {
            return camera_api.SetTriggerParam(ref para);
        }

        public bool SetCalibPara(CameraModel.custom_calib_t calib)
        {
            return camera_api.SetCalibParam(ref calib);
        }

        public bool GenPcldFile(string fileName, string filePath)
        {
            bool r = false;
            if (grab_mode == CameraModel.grab_mode_t.DEPTH_MAP)
            {
                r = ConvertDepthToPcld();
                if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
                {
                    r = camera_api.GenPCLDFile(filePath + "\\" + fileName, ref point_cloud);
                }
                else if (camera_api.connect_type != CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV && camera_api.connect_type != CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV)
                {
                }
            }
            else if (grab_mode == CameraModel.grab_mode_t.POINT_CLOUD)
            {
                if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
                {
                    r = camera_api.GenPCLDFile(filePath + "\\" + fileName, ref point_cloud);
                }
                else if (camera_api.connect_type != CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV && camera_api.connect_type != CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV)
                {
                }
            }
            return r;
        }

        public bool GenDepthFile(string fileName, string filePath)
        {
            bool r = false;
            if (camera_api.connect_type == CameraModel.connect_mode_t.SINGEL_DEV)
            {
                r = camera_api.GenDepthMapFile(filePath + "\\" + fileName, ref depth_map);
            }
            else if (camera_api.connect_type != CameraModel.connect_mode_t.SINGEL_GROUP_MULTIGPLE_DEV && camera_api.connect_type != CameraModel.connect_mode_t.MULTIGPLE_GROUP_MULTIGPLE_DEV)
            {
            }
            return r;
        }

        private int frame_callback(int dev_id, CameraModel.grab_mode_t grab_Mode, IntPtr data)
        {
            if (data != IntPtr.Zero)
            {
                switch (grab_Mode)
                {
                    case CameraModel.grab_mode_t.DEPTH_MAP:
                        ReceiveDepthMapData(dev_id, data);
                        break;
                    default:
                        return -1;
                    case CameraModel.grab_mode_t.IMAGE:
                    case CameraModel.grab_mode_t.POINT_CLOUD:
                        break;
                }
                return 0;
            }
            return -1;
        }

        private void ReceivePCLDData(int dev_id, IntPtr data)
        {
        }

        private void ReceiveDepthMapData(int dev_id, IntPtr data)
        {
            depth_map = Marshal.PtrToStructure<CameraModel.lvm_depth_map_t>(data);
            if (depth_map.height != depth_map.grab_lines)
            {
                return;
            }
            acqOk = true;
            dataContext.xResolution = depth_map.delta_x;
            dataContext.xOffset = depth_map.x_min;
            dataContext.yResolution = depth_map.delta_y;
            dataContext.zResolution = depth_map.delta_z;
            dataContext.zOffset = depth_map.z_min;
            int copySize = depth_map.width * depth_map.height * Marshal.SizeOf(typeof(ushort));
            byte[] dataArray = new byte[copySize];
            _bufHeight = new SafeBufferExt(copySize);
            Marshal.Copy(depth_map.data, dataArray, 0, copySize);
            Marshal.Copy(dataArray, 0, _bufHeight, copySize);
            ConvertMapToPcld(ref depth_map, ref point_cloud);
            CameraModel.p3d_t[] pointClouds = new CameraModel.p3d_t[depth_map.width * depth_map.height];
            IntPtr src = point_cloud.p;
            for (int i = 0; i < depth_map.width * depth_map.height; i++)
            {
                IntPtr ptr = new IntPtr(src.ToInt64() + i * Marshal.SizeOf(typeof(CameraModel.p3d_t)));
                CameraModel.p3d_t p3d = Marshal.PtrToStructure<CameraModel.p3d_t>(ptr);
                if (float.IsNaN(p3d.x) || float.IsNaN(p3d.y) || float.IsNaN(p3d.z))
                {
                    p3d.x = (p3d.y = (p3d.z = 0f));
                }
                pointClouds[i] = p3d;
            }
            Marshal.FreeHGlobal(src);
            laserData = new double[depth_map.height, depth_map.width];
            laserData = CSVOperator.GetPointCloudDataLVM(pointClouds, depth_map.width, depth_map.height, dataContext);
            if (ShowPointCloudDelegate != null)
            {
                ShowPointCloudDelegate(laserData, depth_map.delta_x, depth_map.delta_y);
            }
            Task.Run(delegate
            {
                CogImage16Range externCogImage = ImageData.Lmi3DTransformToRange(dataContext, _bufHeight, depth_map.width, depth_map.height);
                ImageData obj = new ImageData(externCogImage);
                if (UpdateImage != null)
                {
                    UpdateImage(obj);
                }
            });
        }

        public bool SetFileInfoToDev()
        {
            bool r1 = camera_api.SetImageParam(ref Img_Param_T);
            bool r2 = camera_api.SetPointCloudParam(ref Pcld_Param_T);
            bool r3 = camera_api.SetDepthMapParam(ref Depth_Param_T);
            bool r4 = camera_api.SetTriggerParam(ref Trigger_Param_T);
            bool r5 = camera_api.SetDevParam(ref Dev_Info_T);
            bool r6 = camera_api.SetMotorParam(ref Motor_Param_T);
            return r1 && r2 && r3 && r4 && r5 && r6;
        }

        private int GetTriggerMode(CameraModel.TriggerMode trigger)
        {
            int mode = 17;
            switch (trigger)
            {
                case CameraModel.TriggerMode.EXTERNAL_TRIGGER:
                    mode = 17;
                    break;
                case CameraModel.TriggerMode.ECODER_A:
                    mode = 18;
                    break;
                case CameraModel.TriggerMode.ECODER_B:
                    mode = 19;
                    break;
                case CameraModel.TriggerMode.ECODER_AB:
                    mode = 20;
                    break;
                case CameraModel.TriggerMode.TIME_TRIGGER:
                    mode = 21;
                    break;
            }
            return mode;
        }

        public string CharArrayToString(char[] charArray)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charArray.Length && !charArray[i].Equals('\0'); i++)
            {
                sb.Append(charArray[i]);
            }
            return sb.ToString();
        }

        public static bool ConvertMapToPcld(ref CameraModel.lvm_depth_map_t depth_T, ref CameraModel.lvm_point_cloud_t pcd_T)
        {
            IntPtr depth_data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CameraModel.lvm_depth_map_t)));
            Marshal.StructureToPtr(depth_T, depth_data, fDeleteOld: false);
            pcd_T = default(CameraModel.lvm_point_cloud_t);
            int size = depth_T.width * depth_T.height * Marshal.SizeOf(typeof(CameraModel.p3d_t));
            pcd_T.p = Marshal.AllocHGlobal(size);
            IntPtr pcd_data = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CameraModel.lvm_point_cloud_t)));
            Marshal.StructureToPtr(pcd_T, pcd_data, fDeleteOld: false);
            bool r = depth_map_to_pcld(depth_data, pcd_data) == 0;
            if (r)
            {
                pcd_T = Marshal.PtrToStructure<CameraModel.lvm_point_cloud_t>(pcd_data);
            }
            Marshal.FreeHGlobal(depth_data);
            Marshal.FreeHGlobal(pcd_data);
            return r;
        }

        [DllImport("nvt_lvm_sdk.dll")]
        private static extern int depth_map_to_pcld(IntPtr depth_map, IntPtr pc);

        [DllImport("nvt_lvm_sdk.dll")]
        private static extern int get_encoder_para(IntPtr dev, ref int location, ref int counter, ref int pulse);
    }
}
