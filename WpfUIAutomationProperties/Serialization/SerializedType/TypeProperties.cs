using System;
using System.Collections.Generic;
using System.Reflection;

namespace WpfUIAutomationProperties.Serialization
{
    internal static class TypeProperties
    {
        private static readonly Dictionary<Type, PropertyInfo[]> propertiesStore = new Dictionary<Type, PropertyInfo[]>();
        public static PropertyInfo[] Get(Type type)
        {
            if(!propertiesStore.TryGetValue(type, out var properties))
            {
                properties = type.GetProperties();
                propertiesStore[type] = properties;
            }
            return properties;
        }
    }
}
