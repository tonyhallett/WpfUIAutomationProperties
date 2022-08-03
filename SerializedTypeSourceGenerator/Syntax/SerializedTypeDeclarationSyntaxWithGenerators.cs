using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeDeclarationSyntaxWithGenerators
    {
        public SerializedTypeDeclarationSyntaxWithGenerators(
            SyntaxNode classOrStruct,
            bool isClass,
            SyntaxTree syntaxTree, 
            List<SerializedTypeAttributeSyntaxWithGenerator> attributeGenerators)
        {
            ClassOrStruct = classOrStruct;
            IsClass = isClass;
            SyntaxTree = syntaxTree;
            AttributeGenerators = attributeGenerators;
        }
        public SyntaxNode ClassOrStruct { get; set; }
        public bool IsClass { get; set; }
        public SyntaxTree SyntaxTree { get; set; }
        public IReadOnlyList<SerializedTypeAttributeSyntaxWithGenerator> AttributeGenerators { get; set; }
    }
}
