using System;
using System.Xml.Serialization;

namespace NovaVision.BaseClass.Module.Algorithm
{
    [Serializable]
    [XmlRoot("Terminal")]
    public class Terminal : ElementBase
    {
        public bool IsSaveToDB;
    }
}
