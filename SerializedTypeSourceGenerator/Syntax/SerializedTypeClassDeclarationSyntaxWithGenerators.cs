using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeClassDeclarationSyntaxWithGenerators
    {
        public ClassDeclarationSyntax ClassDeclarationSyntax { get; set; }
        public List<SerializedTypeAttributeSyntaxWithGenerator> AttributeGenerators { get; set; }
    }
}
