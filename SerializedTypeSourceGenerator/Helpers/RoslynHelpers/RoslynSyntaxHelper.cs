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

        public static IEnumerable<string> AliasesOf(this IEnumerable<UsingDirectiveSyntax> usings,string fullyQualifiedName)
        {
            return usings.Select(u =>
            {
                if (u.Alias != null && u.Name.ToString() == fullyQualifiedName)
                {
                    return u.Alias.Name.ToString();
                }
                return null;
            }).Where(alias => alias != null);
        }
        
        public static bool IsOfType(this AttributeSyntax attributeSyntax, List<string> aliases, string matchingNamespace, string attributeNameWithoutSuffix)
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
                return @namespace == matchingNamespace && NameMatches(qualifiedNameSyntax.Right.ToString());
            }
            var identifierName = (name as IdentifierNameSyntax).ToString();
            return NameMatches(identifierName) || aliases.Contains(identifierName);
        }
    }
}
