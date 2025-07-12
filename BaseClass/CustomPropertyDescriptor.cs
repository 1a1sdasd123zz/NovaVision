using System;
using System.ComponentModel;

namespace NovaVision.BaseClass
{
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        public CustomPropertyDescriptor(ref Property myProperty, Attribute[] attrs) : base(myProperty.Name, attrs)
        {
            this._property = myProperty;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }

        public override object GetValue(object component)
        {
            return this._property.Value;
        }

        public override string Description
        {
            get
            {
                return this._property.Description;
            }
        }

        public override string Category
        {
            get
            {
                return this._property.Category;
            }
        }

        public override string DisplayName
        {
            get
            {
                return (!string.IsNullOrWhiteSpace(this._property.DisplayName)) ? this._property.DisplayName : this._property.Name;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this._property.ReadOnly;
            }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override void SetValue(object component, object value)
        {
            this._property.Value = value;
        }

        public override TypeConverter Converter
        {
            get
            {
                return this._property.Converter;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return this._property.Value.GetType();
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            return (this._property.Editor == null) ? base.GetEditor(editorBaseType) : this._property.Editor;
        }

        private Property _property;
    }
}
