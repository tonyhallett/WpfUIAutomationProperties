using System;
using System.Diagnostics.CodeAnalysis;

namespace SerializedTypeSourceGeneratorAttributes
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SerializedTypeAttachedTypedPropertyAttribute : Attribute
    {
        public SerializedTypeAttachedTypedPropertyAttribute(Type attachedType, Type propertyType, string firstDependencyProperty, params string[] additionalDependencyProperties) { }
    }
}
