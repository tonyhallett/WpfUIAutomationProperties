using System;
using System.Collections.Generic;

namespace WpfUIAutomationProperties.Serialization
{
    internal static class MappedFrameworkElementPropertiesFactory
    {
        private static readonly Dictionary<TypesKey, IMappedFrameworkElementProperties> lookup = new Dictionary<TypesKey, IMappedFrameworkElementProperties>();
        
        public static IMappedFrameworkElementProperties Get<TSerialized, TFrameworkElement>()
        {
            return Get(typeof(TSerialized), typeof(TFrameworkElement));
        }

        public static IMappedFrameworkElementProperties Get(Type serializedObjectType, Type frameworkElementType)
        {
            var key = new TypesKey(serializedObjectType, frameworkElementType);

            if (!lookup.TryGetValue(key, out var serializedType))
            {
                serializedType = new MappedFrameworkElementProperties(serializedObjectType, frameworkElementType);
                lookup.Add(key, serializedType);
            }
            return serializedType;
        }
    }
}
