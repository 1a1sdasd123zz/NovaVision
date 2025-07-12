using System;
using System.Xml.Serialization;

namespace NovaVision.BaseClass.Module
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("root", IsNullable = false)]
    public class CalibInfo
    {
        public string Explain { get; set; }

        public CalibInfo() { }
        public CalibInfo(string explain)
        {
            Explain = explain;
        }
    }
}
