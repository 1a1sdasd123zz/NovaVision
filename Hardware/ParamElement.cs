﻿using System;
using System.Xml.Serialization;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Helper;
using NovaVision.BaseClass.Module;

namespace NovaVision.Hardware
{
    [Serializable]
    [XmlRoot("Param", IsNullable = false)]
    public class ParamElement : IElement, IChangedEvent
    {
        private string _name;

        private string _type;

        private XmlObject _value = new XmlObject();

        [XmlElement("ParamName")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [XmlElement("ParamType")]
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        [XmlElement("ParamValue")]
        public XmlObject Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public event ChangeEventHandler Changed;

        public event ChangeEventHandler Changing;
    }
}
