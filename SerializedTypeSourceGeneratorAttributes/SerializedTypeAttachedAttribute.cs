﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace SerializedTypeSourceGeneratorAttributes
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class SerializedTypeAttachedAttribute : Attribute
    {
        public SerializedTypeAttachedAttribute(Type attachedType, string firstDependencyProperty, params string[] additionalDependencyProperties) { }
    }
}
