using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypeSyntax
    {
        public static IEnumerable<SerializedTypeDeclarationSyntaxWithGenerators> GetTypesWithSerializedTypeAttribute(IEnumerable<SyntaxTree> syntaxTrees)
        {
            var walkers = syntaxTrees.Select(syntaxTree => new SerializedTypeSyntaxWalker(syntaxTree)).ToList();
            var allGlobalAliases = walkers.SelectMany(walker => walker.GlobalAliases).ToList();
            return walkers.SelectMany(walker => walker.GetAttributeGenerators(allGlobalAliases))
                .GroupBy(serializedTypeAttribute => serializedTypeAttribute.ClassOrStruct)
                .Select(g => new SerializedTypeDeclarationSyntaxWithGenerators { 
                    ClassOrStruct = g.Key, 
                    AttributeGenerators = g.ToList() 
                });
        }
    }
}
