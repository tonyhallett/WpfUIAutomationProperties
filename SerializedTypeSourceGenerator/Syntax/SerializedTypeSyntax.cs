using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypeSyntax
    {
        public static IEnumerable<SerializedTypeDeclarationSyntaxWithGenerators> GetTypesWithSerializedTypeAttribute(SyntaxTree syntaxTree)
        {
            return new SerializedTypeSyntaxWalker().Visit(syntaxTree)
                .GroupBy(serializedTypeAttribute => serializedTypeAttribute.ClassOrStruct)
                .Select(g => new SerializedTypeDeclarationSyntaxWithGenerators { 
                    ClassOrStruct = g.Key, 
                    AttributeGenerators = g.ToList() 
                });
        }
    }
}
