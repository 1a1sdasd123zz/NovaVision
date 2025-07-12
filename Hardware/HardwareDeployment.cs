using System;
using System.Xml.Serialization;

namespace NovaVision.Hardware
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("HardwareDeployment", IsNullable = false)]
    public class HardwareDeployment
    {
        public string Vendor { get; set; }

        public int state { get; set; }
    }
}
