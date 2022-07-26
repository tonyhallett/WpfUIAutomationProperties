using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypeSyntax
    {
        public static IEnumerable<SerializedTypeClassDeclarationSyntaxWithGenerators> GetClassesWithSerializedTypeAttribute(SyntaxTree syntaxTree)
        {
            return new SerializedTypeSyntaxWalker().Visit(syntaxTree).GroupBy(serializedTypeAttribute => serializedTypeAttribute.AttributeSyntax.Class())
                .Select(g => new SerializedTypeClassDeclarationSyntaxWithGenerators { ClassDeclarationSyntax = g.Key, AttributeGenerators = g.ToList() });
        }
    }
}
