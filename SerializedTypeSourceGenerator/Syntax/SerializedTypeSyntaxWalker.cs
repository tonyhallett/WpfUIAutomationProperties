using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeSyntaxWalker : CSharpSyntaxWalker
    {
        private List<UsingDirectiveSyntax> aliasedUsings = new List<UsingDirectiveSyntax>();
        public List<UsingDirectiveSyntax> GlobalAliases { get; internal set; } = new List<UsingDirectiveSyntax>();

        public SerializedTypeSyntaxWalker(SyntaxTree syntaxTree)
        {
            this.syntaxTree = syntaxTree;
            this.Visit(syntaxTree.GetRoot());
        }

        #region reducing the walking
        /* 
            default behaviour if do no override such as VisitUsingDirective is DefaultVisit, which is to Visit each child node,
            the node accepts the visitor
            
            so could prevent iterating on child nodes 
        */

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            foreach(var attributeList in node.AttributeLists)
            {
                VisitAttributeList(attributeList);
            }
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            foreach (var attributeList in node.AttributeLists)
            {
                VisitAttributeList(attributeList);
            }
        }
        #endregion


        private void SetAliases(List<UsingDirectiveSyntax> globalAliases)
        {
            aliasedUsings.AddRange(globalAliases);
            generatorAliases = SerializedTypesAttributeGenerators.Generators.ToDictionary(
                generator => generator,
                generator => GetApplicableAliases(generator.FullyQualifiedName, generator.Namespace));
        }

        public List<SerializedTypeAttributeSyntaxWithGenerator> GetAttributeGenerators(List<UsingDirectiveSyntax> globalAliases)
        {
            SetAliases(globalAliases);

            var serializedTypeAttributeSyntaxWithGenerators = new List<SerializedTypeAttributeSyntaxWithGenerator>();
            foreach(var classOrStructAttribute in classOrStructAttributes)
            {
                var attributeGenerator = SerializedTypesAttributeGenerators.Generators.FirstOrDefault(generator =>
                {
                    var attributeAliases = generatorAliases[generator];
                    return AttributeIsSerializedTypeAttribute(
                            classOrStructAttribute,
                            attributeAliases,
                            generator.Namespace,
                            generator.TypeNameWithoutAttribute
                        );
                });

                if (attributeGenerator != null)
                {
                    serializedTypeAttributeSyntaxWithGenerators.Add(
                        new SerializedTypeAttributeSyntaxWithGenerator
                        {
                            Generator = attributeGenerator,
                            AttributeSyntax = classOrStructAttribute,
                            ClassOrStruct = classOrStructAttribute.Parent.Parent,
                            SyntaxTree = syntaxTree
                        });
                }
            }
            return serializedTypeAttributeSyntaxWithGenerators;
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            // not all usings should be considered.....
            // should create a scope as go
            if (node.Alias != null)
            {
                var k = node.GlobalKeyword.Kind();
                if(node.GlobalKeyword.Kind() != SyntaxKind.None)
                {
                    GlobalAliases.Add(node);
                }
                else
                {
                    aliasedUsings.Add(node);
                }
                
            }
           
        }

        private List<(string alias, bool typeAlias)> GetApplicableAliases(string fullyQualifiedName, string @namespace)
        {
            List<(string alias, bool typeAlias)> attributeAliases = new List<(string alias, bool typeAlias)>();
            foreach (var aliasedUsing in aliasedUsings)
            {
                var alias = aliasedUsing.Alias.Name.ToString();
                if(aliasedUsing.Name.ToString() == fullyQualifiedName)
                {
                    attributeAliases.Add((alias, true));
                }else if(aliasedUsing.Name.ToString() == @namespace)
                {
                    attributeAliases.Add((alias, false));
                }
            }
            return attributeAliases;
        }

        public static bool AttributeIsSerializedTypeAttribute(
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
                if (NameMatches(qualifiedNameSyntax.Right.ToString())){
                    return @namespace == matchingNamespace || aliases.Any(al => !al.typeAlias && al.alias == @namespace);
                }
                return false;
            }

            var identifierName = (name as IdentifierNameSyntax).ToString();
            return NameMatches(identifierName) || aliases.Any(al => al.typeAlias && al.alias == identifierName);
        }

        private List<AttributeSyntax> classOrStructAttributes = new List<AttributeSyntax>();
        private Dictionary<ISerializedTypeAttributeGenerator, List<(string alias, bool typeAlias)>> generatorAliases;
        private readonly SyntaxTree syntaxTree;

        public override void VisitAttribute(AttributeSyntax node)
        {
            var classOrStructDeclarationSyntax = node.Parent?.Parent;
            if (classOrStructDeclarationSyntax != null)
            {
                classOrStructAttributes.Add(node);
            }
            
        }
    }
}
