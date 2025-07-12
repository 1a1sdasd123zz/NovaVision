using System;
using System.Reflection;

namespace NovaVision.BaseClass.DataBase
{
    internal static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentException("value");
            }
            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), inherit: false);
            if (attributes != null && attributes.Length != 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }
}
