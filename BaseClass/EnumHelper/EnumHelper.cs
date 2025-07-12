using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace NovaVision.BaseClass.EnumHelper;

public static class EnumHelper
{
    public static T GetEnumFromDescription<T>(string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                is DescriptionAttribute attr)
            {
                if (attr.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                // 如果没有特性，尝试直接匹配枚举名称
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }
        throw new ArgumentException($"未找到匹配的枚举值: {description}");
    }
}

public static class EnumExtensions
{
    public static T ToEnum<T>(this string description) where T : Enum
    {
        return EnumHelper.GetEnumFromDescription<T>(description);
    }

    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }


    public static string[] GetEnumDescription(this Type t)
    {
        List<string> desValues = new List<string>();
        if (!t.IsEnum)
            return null;
        var val = t.GetEnumValues();
        foreach (var item in val)
        {
            string des = GetDescription((Enum)item);
            desValues.Add(des);
        }
        return desValues.ToArray();
    }

    public static bool TryParseEnumForDesc<IEnum>(this string val, out IEnum en) where IEnum : Enum
    {
        try
        {
            IEnum defa = default(IEnum);
            var items = defa.GetType().GetEnumValues();
            foreach (var item in items)
            {
                if (((Enum)item).GetDescription().Equals(val))
                {
                    en = (IEnum)item;
                    return true;
                }
            }
            en = default(IEnum);
            return false;
        }
        catch (Exception)
        {

            en = default(IEnum);
            return false;
        }
    }
}
