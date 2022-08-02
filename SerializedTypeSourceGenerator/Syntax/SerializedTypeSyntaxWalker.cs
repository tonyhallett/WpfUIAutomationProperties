using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeSyntaxWalker : CSharpSyntaxWalker
    {
        private List<UsingDirectiveSyntax> aliasedUsings = new List<UsingDirectiveSyntax>();
        private List<SerializedTypeAttributeSyntaxWithGenerator> serializedTypeAttributeSyntaxWithGenerators = new List<SerializedTypeAttributeSyntaxWithGenerator>();

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

        public List<SerializedTypeAttributeSyntaxWithGenerator> Visit(SyntaxTree syntaxTree)
        {
            this.Visit(syntaxTree.GetRoot());
            return serializedTypeAttributeSyntaxWithGenerators;
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            // not all usings should be considered.....
            // should create a scope as go
            if (node.Alias != null)
            {
                aliasedUsings.Add(node);
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

        public override void VisitAttribute(AttributeSyntax node)
        {
            var classOrStructDeclarationSyntax = node.Parent?.Parent;
            if (classOrStructDeclarationSyntax != null)
            {
                var attributeGenerator = SerializedTypesAttributeGenerators.Generators.FirstOrDefault(generator =>
                {
                    var attributeAliases = GetApplicableAliases(generator.FullyQualifiedName, generator.Namespace);
                    return AttributeIsSerializedTypeAttribute(
                            node,
                            attributeAliases,
                            generator.Namespace,
                            generator.TypeNameWithoutAttribute
                        );
                });

                if (attributeGenerator != null)
                {
                    serializedTypeAttributeSyntaxWithGenerators.Add(
                        new SerializedTypeAttributeSyntaxWithGenerator { 
                            Generator = attributeGenerator, 
                            AttributeSyntax = node,
                            ClassOrStruct = classOrStructDeclarationSyntax
                        });
                }
                
            }
            
        }
    }
}
