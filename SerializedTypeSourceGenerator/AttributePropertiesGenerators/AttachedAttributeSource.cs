using Microsoft.CodeAnalysis;

namespace SerializedTypeSourceGenerator
{
    internal static class AttachedAttributeSource { 
        public static AttributeSource For(ITypeSymbol ownerTypeSymbol,string dependencyPropertyName)
        {
            var (ownerTypeName, ownerTypeNamespaces) = RoslynSymbolHelper.GetTypeNameAndNamespaces(ownerTypeSymbol);
            ownerTypeNamespaces.Add("WpfUIAutomationProperties.Serialization");
            var source = $@"        [AttachedProperty(typeof({ownerTypeName}), ""{dependencyPropertyName}"")]";
            return new AttributeSource
            {
                Source = source,
                Namespaces = ownerTypeNamespaces
            };
        }
    }
}
