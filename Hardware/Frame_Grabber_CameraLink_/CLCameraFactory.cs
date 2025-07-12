using System;
using NovaVision.Hardware.Frame_Grabber_CameraLink_.CameraLinkCamera;
using NovaVision.Hardware.Frame_Grabber;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_
{
    public class CLCameraFactory
    {
        public static Bv_Camera CreateCLCamera(FrameGrabberConfigData configDatas, CameraSerialPort serialPort)
        {
            Bv_Camera camera = null;
            switch (configDatas["CameraVendorName"].Value.ToString())
            {
                //case "Itek_CL":
                //    camera = new Itek_CL(serialPort, null);
                //    camera.ConfigDatas["ExposureTime"].Value.mValue = Convert.ToInt32(configDatas["ExposureTime"].Value.mValue);
                //    camera.ConfigDatas["Gain"].Value.mValue = configDatas["Gain"].Value.mValue;
                //    camera.ConfigDatas["ScanDirection"].Value.mValue = configDatas["ScanDirection"].Value.mValue;
                //    break;
                //case "Dalsa_CL":
                //    camera = new Dalsa_CL(serialPort, null);
                //    camera.ConfigDatas["ExposureTime"].Value.mValue = Convert.ToInt32(configDatas["ExposureTime"].Value.mValue);
                //    camera.ConfigDatas["Gain"].Value.mValue = configDatas["Gain"].Value.mValue;
                //    camera.ConfigDatas["ScanDirection"].Value.mValue = configDatas["ScanDirection"].Value.mValue;
                //    break;
                case "Hikrobot_CL":
                    camera = new Hikrobot_CL(serialPort, null);
                    camera.ConfigDatas["ExposureTime"].Value.mValue = Convert.ToInt32(configDatas["ExposureTime"].Value.mValue);
                    camera.ConfigDatas["Gain"].Value.mValue = configDatas["Gain"].Value.mValue;
                    camera.ConfigDatas["ScanDirection"].Value.mValue = configDatas["ScanDirection"].Value.mValue;
                    break;
            }
            return camera;
        }

        public static Bv_Camera CreateCLCamera(FrameGrabberConfigData configDatas, CameraSerialPort serialPort, uint camIndex)
        {
            Bv_Camera camera = null;
            //string text = configDatas["CameraVendorName"].Value.ToString();
            //string text2 = text;
            //if (text2 == "Itek_CL")
            //{
            //    camera = new Itek_CL(serialPort, null, camIndex);
            //    camera.ConfigDatas["ExposureTime"].Value.mValue = Convert.ToInt32(configDatas["ExposureTime"].Value.mValue);
            //    camera.ConfigDatas["Gain"].Value.mValue = configDatas["Gain"].Value.mValue;
            //    camera.ConfigDatas["ScanDirection"].Value.mValue = configDatas["ScanDirection"].Value.mValue;
            //}
            return camera;
        }
    }
}
