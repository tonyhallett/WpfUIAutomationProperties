using Microsoft.CodeAnalysis;

namespace SerializedTypeSourceGenerator
{
    public interface ISerializedProperty {
        ITypeSymbol TypeSymbol { get; }
        string Name { get;}
        AttributeSource GetAttributeSource();
        Location Location { get; }
        Diagnostic Diagnostic { get; }
    }
}
