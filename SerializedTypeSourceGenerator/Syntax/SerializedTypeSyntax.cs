using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypeSyntax
    {
        public static IEnumerable<SerializedTypeClassDeclarationSyntaxWithGenerators> GetClassesWithSerializedTypeAttribute(SyntaxTree syntaxTree)
        {
            return GetSerializedTypeAttributeNodes(syntaxTree)
                .GroupBy(serializedTypeAttribute => serializedTypeAttribute.AttributeSyntax.Class())
                .Select(g => new SerializedTypeClassDeclarationSyntaxWithGenerators { ClassDeclarationSyntax = g.Key,AttributeGenerators = g.ToList()});
        }

        private static IEnumerable<SerializedTypeAttributeSyntaxWithGenerator> GetSerializedTypeAttributeNodes(SyntaxTree syntaxTree)
        {
            var root = syntaxTree.GetRoot();

            var usings = root.DescendantNodesOfType<UsingDirectiveSyntax>().ToList();
            var attributes = root.DescendantNodesOfType<AttributeSyntax>();

            return SerializedTypesAttributeGenerators.Generators.SelectMany(generator =>
            {
                var aliases = root.DescendantNodesOfType<UsingDirectiveSyntax>()
                    .AliasedUsingsOf(generator.FullyQualifiedName).ToList();

                return attributes.Where(attribute => attribute.IsOfType(
                        aliases,
                        generator.Namespace,
                        generator.TypeNameWithoutAttribute
                    ))
                .Select(attribute => new SerializedTypeAttributeSyntaxWithGenerator
                {
                    AttributeSyntax = attribute,
                    Generator = generator
                });
            });
        }
    }
}
