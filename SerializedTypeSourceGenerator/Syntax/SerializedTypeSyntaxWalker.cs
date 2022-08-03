using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal class AttributedClassOrStruct
    {
        public AttributedClassOrStruct(SyntaxNode classOrStruct, bool isClass, List<UsingDirectiveSyntax> usings)
        {
            ClassOrStruct = classOrStruct;
            IsClass = isClass;
            Usings = usings.AsReadOnly();
        }
        public SyntaxNode ClassOrStruct { get; set; }
        public bool IsClass { get; set; }
       
        public void AddAttribute(AttributeSyntax attribute)
        {
            Attributes.Add(attribute);
        }
        // todo readonly
        public List<AttributeSyntax> Attributes { get; } = new List<AttributeSyntax>();
        public IReadOnlyCollection<UsingDirectiveSyntax> Usings { get;}
    }

    internal class SerializedTypeSyntaxWalker : CSharpSyntaxWalker
    {
        private List<UsingDirectiveSyntax> globalUsings = new List<UsingDirectiveSyntax>();
        private Stack<List<UsingDirectiveSyntax>> usingsStack = new Stack<List<UsingDirectiveSyntax>>();
        private List<AttributedClassOrStruct> attributedClassesOrStructs = new List<AttributedClassOrStruct>();
        private AttributedClassOrStruct currentAttributedClassOrStruct;

        public IReadOnlyCollection<AttributedClassOrStruct> AttributedClassesOrStructs => attributedClassesOrStructs;
        public IReadOnlyCollection<UsingDirectiveSyntax> GlobalUsings => globalUsings;
        public SyntaxTree SyntaxTree { get; }

        public SerializedTypeSyntaxWalker(SyntaxTree syntaxTree)
        {
            this.SyntaxTree = syntaxTree;
            usingsStack.Push(new List<UsingDirectiveSyntax>());
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
            VisitClassOrStructAttributes(node.AttributeLists, node, true);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            VisitClassOrStructAttributes(node.AttributeLists, node, false);
        }

        private void VisitClassOrStructAttributes(SyntaxList<AttributeListSyntax> attributeLists,SyntaxNode classOrStruct,bool isClass)
        {
            currentAttributedClassOrStruct = new AttributedClassOrStruct(classOrStruct, isClass, usingsStack.Peek());
            attributedClassesOrStructs.Add(currentAttributedClassOrStruct);
            foreach (var attributeList in attributeLists)
            {
                VisitAttributeList(attributeList);
            }
        }

        #endregion

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            usingsStack.Push(new List<UsingDirectiveSyntax>(usingsStack.Peek()));
            base.VisitNamespaceDeclaration(node);
            usingsStack.Pop();

        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            if (node.GlobalKeyword.IsKind(SyntaxKind.None))
            {
                usingsStack.Peek().Add(node);
            }
            else
            {
                globalUsings.Add(node);
            }
        }

        public override void VisitAttribute(AttributeSyntax node)
        {
            if (currentAttributedClassOrStruct != null)
            {
                currentAttributedClassOrStruct.AddAttribute(node);
            }
        }
    }
}
