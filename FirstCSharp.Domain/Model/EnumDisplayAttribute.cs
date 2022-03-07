

namespace FirstCSharp.Domain.Model
{
    using System;
    using System.Linq;

    public class EnumDisplayAttribute : Attribute
    {
        //建構子
        public EnumDisplayAttribute(string display)
        {
            Display = display;
        }

        //屬性
        public string Display { get; private set; }


    }

    //擴充方法::必須定義為非泛型靜態類別中的靜態方法。
    public static class EnumExtention
    {
        public static string ToDisplay(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field != null)
            {
                var attr = field.GetCustomAttributes(typeof(EnumDisplayAttribute), true).FirstOrDefault() as EnumDisplayAttribute;

                if (attr != null)
                {
                    return attr.Display;
                }
            }

            return string.Empty;
        }
    }
}
