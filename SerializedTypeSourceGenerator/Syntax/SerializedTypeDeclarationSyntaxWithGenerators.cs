using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeDeclarationSyntaxWithGenerators
    {
        public SyntaxNode ClassOrStruct { get; set; }
        public List<SerializedTypeAttributeSyntaxWithGenerator> AttributeGenerators { get; set; }
    }
}
