using System;
using System.Xml.Serialization;

namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("Keyence", IsNullable = false)]
    public class KeyenceConfig
    {
        public string LaserHead { get; set; }

        public double Coefficient { get; set; }
    }
}
