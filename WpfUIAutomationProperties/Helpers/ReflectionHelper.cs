using System;

namespace WpfUIAutomationProperties
{
    internal static class ReflectionHelper
    {
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
