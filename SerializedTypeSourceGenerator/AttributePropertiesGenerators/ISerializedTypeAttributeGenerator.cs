using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SerializedTypeSourceGenerator
{
    internal interface ISerializedTypeAttributeGenerator
    {
        string ContainingAssemblyName { get; }
        string FullyQualifiedName { get; }
        string Namespace { get; }
        string TypeNameWithoutAttribute { get; }

        SerializedPropertiesResult GetProperties(
            AttributeData attributeData, 
            AttributeSyntax attributeSyntax
        );
    }
}
