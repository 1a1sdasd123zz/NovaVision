using System;
using System.Collections.Generic;
using Lmi3d.GoSdk;

namespace NovaVision.Hardware._007_SDK_LMI3DTool
{
    internal class SensorInformation
    {
        public static double XInterval(GoSensor _sensor)
        {
            double xInterval = 0.0;
            GoSetup goSetup = _sensor.Setup;
            return _sensor.Setup.GetSpacingIntervalSystemValue(0);
        }

        public static double YInterval(GoSensor _sensor)
        {
            double yInterval = 0.0;
            GoSetup goSetup = _sensor.Setup;
            if (_sensor.Family == 1)
            {
                yInterval = ((!(_sensor.Setup.TriggerSource == 0)) ? _sensor.Setup.EncoderSpacing : (_sensor.Transform.Speed / _sensor.Setup.FrameRateLimitMax));
            }
            else if (_sensor.Family == 2)
            {
                yInterval = _sensor.Setup.GetSpacingIntervalSystemValue(0);
            }
            return yInterval;
        }

        public static List<string> Get_SeneorJob(GoSensor _sensor)
        {
            List<string> sensorJobName = new List<string>();
            for (int i = 0; i < _sensor.FileCount; i++)
            {
                sensorJobName.Add(_sensor.GetFileName(i));
            }
            return sensorJobName;
        }

        public static string Get_SensorDefaultJob(GoSensor _sensor)
        {
            return _sensor.DefaultJob;
        }

        public static string Get_SensorStatus(GoSensor _sensor)
        {
            return _sensor.State.ToString();
        }

        public static string Get_SensorVersions(GoSystem system)
        {
            return system.SdkVersion().Format();
        }

        public static string Get_SensorScanMode(GoSensor _sensor)
        {
            return _sensor.ScanMode.ToString();
        }

        public static string Get_SensorSerialNumber(GoSensor _sensor)
        {
            return _sensor.Id.ToString();
        }

        public static string Get_SensorType(GoSensor _sensor)
        {
            string goMode = null;
            try
            {
                return _sensor.Model;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static string Get_SensorState(GoSensor _sensor)
        {
            return _sensor.State.ToString();
        }
    }
}
