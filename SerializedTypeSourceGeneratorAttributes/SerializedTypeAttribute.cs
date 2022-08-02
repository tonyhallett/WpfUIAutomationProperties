using System;
using System.Diagnostics.CodeAnalysis;

namespace SerializedTypeSourceGeneratorAttributes
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class SerializedTypeAttribute : Attribute
    {
        public SerializedTypeAttribute(Type wpfFrameworkType,string firstProperty, params string[] additionalProperties) { }
        
    }
}
