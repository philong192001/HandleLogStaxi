using System.ComponentModel;
using System.Reflection;

namespace LoggerELK.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this System.Enum val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
           .GetType()
           .GetField(val.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }

    public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}
