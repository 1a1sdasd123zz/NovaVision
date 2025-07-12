using System.ComponentModel;

namespace NovaVision.BaseClass
{
    public class Property
    {
        public Property(string sCategory, string sDisplayName, string sName, string sDescription, object sValue, bool sReadonly, bool sVisible)
        {
            this.Category = sCategory;
            this.DisplayName = sDisplayName;
            this.Name = sName;
            this.Description = sDescription;
            this.Value = sValue;
            this.ReadOnly = sReadonly;
            this.Visible = sVisible;
        }

        public string Category { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public object Value { get; set; }

        public bool ReadOnly { get; set; }

        public bool Visible { get; set; }

        public TypeConverter Converter { get; set; }

        public virtual object Editor { get; set; }
    }
}
