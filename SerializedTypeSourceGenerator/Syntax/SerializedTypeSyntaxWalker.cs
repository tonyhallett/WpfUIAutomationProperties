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

        public override void VisitAttribute(AttributeSyntax node)
        {
            var classDeclarationSyntax = node.Parent?.Parent as ClassDeclarationSyntax;
            if (classDeclarationSyntax != null)
            {
                var attributeGenerator = SerializedTypesAttributeGenerators.Generators.FirstOrDefault(generator =>
                {
                    return node.IsOfType(
                            aliasedUsings.AliasesOf(generator.FullyQualifiedName).ToList(),
                            generator.Namespace,
                            generator.TypeNameWithoutAttribute
                        );
                });
                if (attributeGenerator != null)
                {
                    serializedTypeAttributeSyntaxWithGenerators.Add(new SerializedTypeAttributeSyntaxWithGenerator { Generator = attributeGenerator, AttributeSyntax = node });
                }
                
            }
            
        }
    }
}
