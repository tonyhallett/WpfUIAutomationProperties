using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    public static class RoslynSyntaxHelper
    {
        public static ClassDeclarationSyntax Class(this AttributeSyntax attributeSyntax) =>
            attributeSyntax.Parent.Parent as ClassDeclarationSyntax;
        public static IEnumerable<T> DescendantNodesOfType<T>(this SyntaxNode syntaxNode) where T : SyntaxNode
        {
            return syntaxNode.DescendantNodes().OfType<T>();
        }
    }
}
