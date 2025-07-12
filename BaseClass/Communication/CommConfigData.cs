using System;
using System.Xml.Serialization;

namespace NovaVision.BaseClass.Communication
{
    [XmlType(AnonymousType = true)]
    [XmlRoot("CommunicationCards", IsNullable = false)]
    [Serializable]
    public class CommConfigData
    {
        public string CommCategory { get; set; }

        public string SerialNum { get; set; }

        public string CommType { get; set; }

        public string RoleName { get; set; }

        public int ModeIndex { get; set; }

        public int Rack { get; set; }

        public int Slot { get; set; }

        public int ControlDBNum { get; set; }

        public int StatusDBNum { get; set; }

        public string LocalIp { get; set; }

        public string LocalSubnet { get; set; }

        public string LocalPort { get; set; }

        public string LocalHostName { get; set; }

        public string RemoteIp { get; set; }

        public string RemotePort { get; set; }

        public int ConnectTimeout { get; set; } = 3000;

        public string HBStr { get; set; } = "HB";

        public bool HBFlag { get; set; } = false;
    }
}
