using System.Collections.Generic;
using System.ComponentModel;

namespace NovaVision.BaseClass
{
    public class DropDownListConverter : StringConverter
    {
        private List<string> _objects = new List<string>();
        public DropDownListConverter(List<string> objects)
        {
            this._objects = objects;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(this._objects);
        }
    }


}
