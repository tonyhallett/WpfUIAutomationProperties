using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeDeclarationSyntaxWithGenerators
    {
        public SyntaxNode ClassOrStruct { get; set; }
        public SyntaxTree SyntaxTree
        {
            get
            {
                return AttributeGenerators[0].SyntaxTree;
            }
        }
        public List<SerializedTypeAttributeSyntaxWithGenerator> AttributeGenerators { get; set; }
    }
}
