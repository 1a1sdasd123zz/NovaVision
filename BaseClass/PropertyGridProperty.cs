using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NovaVision.BaseClass
{
    [Serializable]
    public class PropertyGridProperty : ICustomTypeDescriptor
    {
        public int Count
        {
            get
            {
                return this._listProperty.Count;
            }
        }

        public void Add(Property value)
        {
            bool flag = value != null;
            if (flag)
            {
                bool flag2 = !this._listProperty.Contains(value);
                if (flag2)
                {
                    this._listProperty.Add(value);
                }
            }
        }

        public void Clear()
        {
            this._listProperty.Clear();
        }

        public void Remove(Property value)
        {
            bool flag = value != null && this._listProperty.Contains(value);
            if (flag)
            {
                this._listProperty.Remove(value);
            }
        }

        public Property this[int index]
        {
            get
            {
                return this._listProperty[index];
            }
            set
            {
                this._listProperty[index] = value;
            }
        }

        public Property this[object name]
        {
            get
            {
                return this._listProperty.FirstOrDefault((Property p) => p.Name == name.ToString());
            }
            set
            {
                Property pre = this._listProperty.FirstOrDefault((Property p) => p.Name == name.ToString());
            }
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] newProps = new PropertyDescriptor[this._listProperty.Count];
            for (int i = 0; i < this._listProperty.Count; i++)
            {
                Property prop = this[i];
                newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
            }
            return new PropertyDescriptorCollection(newProps);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        public List<Property> _listProperty = new List<Property>();
    }
}
