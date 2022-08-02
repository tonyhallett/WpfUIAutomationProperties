using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializedTypeSourceGenerator
{
    internal static class SourceProvider
    {
        public static (string hintName, string source) GetSource(SerializedType serializedType)
        {
            var serializedTypeName = serializedType.Symbol.Name;
            var isStruct = serializedType.Symbol.TypeKind == TypeKind.Struct;
            var classOrStruct = isStruct ? "struct" : "class";
            var propertyStringBuilder = new StringBuilder();
            var usingsStringBuilder = new StringBuilder();
            var namespaces = new List<string>();

            foreach (var serializedProperty in serializedType.Properties)
            {
                var propertySymbolType = serializedProperty.TypeSymbol;
                var (typeName, typeNamespaces) = RoslynSymbolHelper.GetTypeNameAndNamespaces(propertySymbolType);
                namespaces.AddRange(typeNamespaces);

                var attribute = serializedProperty.GetAttributeSource();
                var attributeSource = attribute == null ? string.Empty : attribute.Source + Environment.NewLine;
                if (attribute != null)
                {
                    namespaces.AddRange(attribute.Namespaces);
                }
                var property = $@"{attributeSource}        public {typeName} {serializedProperty.Name} {{ get; set; }}";
                propertyStringBuilder.AppendLine(property);
            }

            namespaces.Distinct().ToList().ForEach(ns => usingsStringBuilder.AppendLine($"using {ns};"));

            var serializedTypeInGlobalNamespace = serializedType.Symbol.ContainingNamespace.IsGlobalNamespace;

            string source = serializedTypeInGlobalNamespace ? $@"// Auto-generated code
{usingsStringBuilder}
partial class {serializedTypeName}
{{
{propertyStringBuilder}}}
" :
$@"// Auto-generated code
{usingsStringBuilder}
namespace {serializedType.Symbol.ContainingNamespace.ToDisplayString()}
{{
    partial {classOrStruct} {serializedTypeName}
    {{
{propertyStringBuilder}    }}
}}
";
            return ($"{serializedTypeName}.g.cs", source);
        }
    } 
}
