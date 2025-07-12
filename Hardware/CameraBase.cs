﻿using System;
using System.Collections.Generic;

namespace NovaVision.Hardware
{
    public class CameraBase
    {
        public Action<ImageData> UpdateImage;

        public static readonly Dictionary<string, string> CameraType = new Dictionary<string, string>
        {
            { "2D", "2D" },
            { "2D_LineScan", "2D_LineScan" },
            { "3D", "3D" }
        };

        public static readonly string[] Cam3DVendor = new string[5] { "LMI(乐姆迈)", "Keyence(基恩士)", "LVM(翌视)", "SSZN(深视)", "CognexDS(DS系列)" };

        public CamParams SettingParams = new CamParams();

        public CamErrCode camErrCode = CamErrCode.ConnectFailed;

        public bool isConnected;

        public ICam_Handle<CameraMessage> cam_Handle;

        public virtual void SetCameraSetting(CameraConfigData c)
        {
        }

        public virtual int Start_Grab(bool state)
        {
            return -1;
        }

        public virtual int Stop_Grab(bool state)
        {
            return -1;
        }

        public virtual int SoftTriggerOnce()
        {
            return -1;
        }
    }
}
