﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace SerializedTypeSourceGeneratorAttributes
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class SerializedTypeTypedPropertyAttribute : Attribute
    {
        public SerializedTypeTypedPropertyAttribute(Type wpfFrameworkType, Type propertyType,string firstProperty, params string[] additionalProperties) { }
    }
}
