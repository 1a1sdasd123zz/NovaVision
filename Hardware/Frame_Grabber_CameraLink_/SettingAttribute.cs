using System;

namespace NovaVision.Hardware.Frame_Grabber_CameraLink_
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SettingAttribute : Attribute
    {
        public string Discription { get; private set; }

        public SettingAttribute(string discription)
        {
            Discription = discription;
        }
    }
}
