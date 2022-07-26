using System.Collections.Generic;

namespace WpfUIAutomationProperties.Serialization
{
    public static class TypedDictionaryExtensions
    {
        public static T Get<T>(this IDictionary<string,object> dictionary,string key)
        {
            return (T)dictionary[key];
        }
    }
}
