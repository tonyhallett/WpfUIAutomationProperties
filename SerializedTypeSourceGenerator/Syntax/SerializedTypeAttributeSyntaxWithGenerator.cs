﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeAttributeSyntaxWithGenerator
    {
        public AttributeSyntax AttributeSyntax { get; set; }
        public ISerializedTypeAttributeGenerator Generator { get; set; }
    }
}
