using Lmi3d.GoSdk;

namespace NovaVision.Hardware._007_SDK_LMI3DTool
{
    internal class SensorOperation
    {
        public static double Get_MaxFrameRate(GoSensor sensor)
        {
            GoSetup setup = sensor.Setup;
            return setup.FrameRate;
        }

        public static bool Set_FrameRate(GoSensor sensor, double frameRate)
        {
            if (Get_TriggerSource(sensor) == 0)
            {
                GoSetup setup = sensor.Setup;
                try
                {
                    setup.FrameRate = frameRate;
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static double Get_Exposure(GoSensor sensor)
        {
            GoSetup setup = sensor.Setup;
            return setup.GetExposure(0);
        }

        public static bool Set_Exposure(GoSensor sensor, double exposure)
        {
            GoSetup setup = sensor.Setup;
            try
            {
                setup.SetExposure(0, exposure);
                sensor.Flush();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static GoTrigger Get_TriggerSource(GoSensor sensor)
        {
            GoSetup setup = sensor.Setup;
            return setup.TriggerSource;
        }

        public static void Set_TriggerSource(GoSensor sensor, GoTrigger trigger)
        {
            try
            {
                GoSetup setup = sensor.Setup;
                setup.TriggerSource = trigger;
                sensor.Flush();
            }
            catch
            {
            }
        }

        public static bool Set_EncoderParams(GoSensor sensor, double encoderSpacing, GoEncoderTriggerMode encoderTriggerMode)
        {
            GoSetup setup = sensor.Setup;
            Set_TriggerSource(sensor, 1);
            setup.EncoderTriggerMode = encoderTriggerMode;
            try
            {
                setup.EncoderSpacing = encoderSpacing;
                sensor.Flush();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void ChangeJob(GoSensor sensor, string jobName)
        {
            if (sensor.State == 11)
            {
                sensor.Stop();
            }
            sensor.CopyFile(jobName, "_live.job");
        }

        public static bool SetScanFixedLengthWithTriggerControl(GoSensor sensor, double length, GoSurfaceGenerationStartTrigger startTrigger)
        {
            GoSetup setup = sensor.Setup;
            GoSurfaceGeneration goSurfaceGeneration = setup.GetSurfaceGeneration();
            try
            {
                goSurfaceGeneration.GenerationType = 1;
                goSurfaceGeneration.FixedLengthStartTrigger = startTrigger;
                goSurfaceGeneration.FixedLengthLength = length;
                sensor.Flush();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SetTriggerControl(GoSensor sensor, GoSurfaceGenerationStartTrigger startTrigger)
        {
            GoSetup setup = sensor.Setup;
            GoSurfaceGeneration goSurfaceGeneration = setup.GetSurfaceGeneration();
            try
            {
                goSurfaceGeneration.GenerationType = 1;
                goSurfaceGeneration.FixedLengthStartTrigger = startTrigger;
                sensor.Flush();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SetScanFixedLength(GoSensor sensor, double length)
        {
            GoSetup setup = sensor.Setup;
            GoSurfaceGeneration goSurfaceGeneration = setup.GetSurfaceGeneration();
            try
            {
                goSurfaceGeneration.GenerationType = 1;
                goSurfaceGeneration.FixedLengthLength = length;
                sensor.Flush();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static double GetScanLength(GoSensor sensor)
        {
            GoSetup setup = sensor.Setup;
            GoSurfaceGeneration goSurfaceGeneration = setup.GetSurfaceGeneration();
            return goSurfaceGeneration.FixedLengthLength;
        }

        public static void SetTravelSpeedWithResolution(GoSensor sensor, double encoderResolution, double speed)
        {
            GoTransform transform = sensor.Transform;
            transform.EncoderResolution = encoderResolution;
            transform.Speed = speed;
            sensor.Flush();
        }

        public static void SetTravelSpeed(GoSensor sensor, double speed)
        {
            GoTransform transform = sensor.Transform;
            transform.Speed = speed;
            sensor.Flush();
        }

        public static void SetEncoderResolution(GoSensor sensor, double encoderResolution)
        {
            GoTransform transform = sensor.Transform;
            transform.EncoderResolution = encoderResolution;
            sensor.Flush();
        }

        public static double GetTravelSpeed(GoSensor sensor)
        {
            GoTransform transform = sensor.Transform;
            return transform.Speed;
        }

        public static double GetEncoderResolution(GoSensor sensor)
        {
            GoTransform transform = sensor.Transform;
            return transform.EncoderResolution;
        }

        public static void SetYPitch(GoSensor sensor, double yPitch)
        {
            GoSetup goSetup = sensor.Setup;
            if (sensor.Family == 2)
            {
                sensor.Setup.SetSpacingInterval(0, yPitch);
            }
        }
    }
}
