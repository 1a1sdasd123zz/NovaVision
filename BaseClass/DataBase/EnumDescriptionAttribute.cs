using System;

namespace NovaVision.BaseClass.DataBase
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;

        public string Description => description;

        public EnumDescriptionAttribute(string description)
        {
            this.description = description;
        }
    }
}
