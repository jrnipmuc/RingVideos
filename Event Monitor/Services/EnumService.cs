using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Event_Monitor.Services
{
    internal static class EnumService
    {
        /// <summary>
        /// Gets the Description attribute value for an enum value
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null) return value.ToString();

            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        /// Gets a list of enum values with their descriptions for binding to ComboBox
        /// </summary>
        public static List<EnumItem<T>> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new EnumItem<T>
                {
                    Value = e,
                    Display = e.GetDescription()
                })
                .ToList();
        }

        public class EnumItem<T> where T : Enum
        {
            public T Value { get; set; }
            public string Display { get; set; }

            // For ComboBox binding
            public int Id => Convert.ToInt32(Value);
        }
    }
}
