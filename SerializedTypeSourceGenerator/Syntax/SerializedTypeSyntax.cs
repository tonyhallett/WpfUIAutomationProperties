using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypeSyntax
    {
        public static IEnumerable<SerializedTypeDeclarationSyntaxWithGenerators> GetTypesWithSerializedTypeAttribute(
            IEnumerable<SyntaxTree> syntaxTrees,
            CancellationToken cancellationToken
        )
        {
            var walkers = syntaxTrees.Select(syntaxTree => new SerializedTypeSyntaxWalker(syntaxTree)).ToList();

            var aliasedGlobalUsings = walkers.SelectMany(walker => walker.GlobalUsings)
                .Where(@using => @using.Alias != null).ToList();

            return walkers.SelectMany(walker =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return GetSerializedTypesFromSyntaxTree(walker.AttributedClassesOrStructs, aliasedGlobalUsings, walker.SyntaxTree);
            });
        }

        private static IEnumerable<SerializedTypeDeclarationSyntaxWithGenerators> GetSerializedTypesFromSyntaxTree(
            IEnumerable<AttributedClassOrStruct> attributedClassesOrStructs,
            List<UsingDirectiveSyntax> aliasedGlobalUsings,
            SyntaxTree syntaxTree
        )
        {
            return attributedClassesOrStructs.Select(attributedClassOrStruct =>
            {
                var classOrStructAliasUsings = attributedClassOrStruct.Usings.Where(@using => @using.Alias != null);
                var aliasedUsings = aliasedGlobalUsings.Concat(classOrStructAliasUsings).ToList();

                var attributeGenerators = GetAttributeGenerators(attributedClassOrStruct.Attributes, aliasedUsings);

                if (attributeGenerators.Any())
                {
                    return new SerializedTypeDeclarationSyntaxWithGenerators(
                        attributedClassOrStruct.ClassOrStruct,
                        attributedClassOrStruct.IsClass,
                        syntaxTree,
                        attributeGenerators
                    );

                }
                else
                {
                    return null;
                }
            }).Where(serializedType => serializedType != null);
        }
        
        private static List<SerializedTypeAttributeSyntaxWithGenerator> GetAttributeGenerators(List<AttributeSyntax> attributes, List<UsingDirectiveSyntax> aliasedUsings)
        {
            return attributes.Select(attribute =>
            {
                var attributeGenerator = FindGenerator(attribute, aliasedUsings);

                if (attributeGenerator != null)
                {
                    return new SerializedTypeAttributeSyntaxWithGenerator
                    {
                        Generator = attributeGenerator,
                        AttributeSyntax = attribute,
                    };
                }
                return null;
            }).Where(attributeGenerator => attributeGenerator != null).ToList();
        }

        private static ISerializedTypeAttributeGenerator FindGenerator(
            AttributeSyntax attribute,
            List<UsingDirectiveSyntax> aliasedUsings
        )
        {
            return SerializedTypesAttributeGenerators.Generators.FirstOrDefault(generator =>
            {
                var attributeAliases = GetApplicableAliases(aliasedUsings, generator.FullyQualifiedName, generator.Namespace);

                return AttributeIsSerializedTypeAttribute(
                        attribute,
                        attributeAliases,
                        generator.Namespace,
                        generator.TypeNameWithoutAttribute
                    );
            });
        }

        private static List<(string alias, bool typeAlias)> GetApplicableAliases(
            IEnumerable<UsingDirectiveSyntax> aliasedUsings,
            string fullyQualifiedName,
            string @namespace
        )
        {
            List<(string alias, bool typeAlias)> attributeAliases = new List<(string alias, bool typeAlias)>();
            foreach (var aliasedUsing in aliasedUsings)
            {
                var alias = aliasedUsing.Alias.Name.ToString();
                if (aliasedUsing.Name.ToString() == fullyQualifiedName)
                {
                    attributeAliases.Add((alias, true));
                }
                else if (aliasedUsing.Name.ToString() == @namespace)
                {
                    attributeAliases.Add((alias, false));
                }
            }
            return attributeAliases;
        }

        private static bool AttributeIsSerializedTypeAttribute(
            AttributeSyntax attributeSyntax,
            List<(string alias, bool typeAlias)> aliases,
            string matchingNamespace,
            string attributeNameWithoutSuffix
        )
        {
            var attributeNameWithSuffix = $"{attributeNameWithoutSuffix}Attribute";
            bool NameMatches(string simpleName)
            {
                return simpleName == attributeNameWithoutSuffix || simpleName == attributeNameWithSuffix;
            }

            var name = attributeSyntax.Name;
            if (name is QualifiedNameSyntax qualifiedNameSyntax)
            {
                var @namespace = qualifiedNameSyntax.Left.ToString();
                if (NameMatches(qualifiedNameSyntax.Right.ToString()))
                {
                    return @namespace == matchingNamespace || aliases.Any(al => !al.typeAlias && al.alias == @namespace);
                }
                return false;
            }

            var identifierName = (name as IdentifierNameSyntax).ToString();
            return NameMatches(identifierName) || aliases.Any(al => al.typeAlias && al.alias == identifierName);
        }

    }
}
