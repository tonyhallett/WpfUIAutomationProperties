using Microsoft.CodeAnalysis;

namespace SerializedTypeSourceGenerator
{
    internal class TypedSerializedProperty : ISerializedProperty
    {
        public TypedSerializedProperty(
            ITypeSymbol propertyTypeSymbol,
            ITypeSymbol serializedPropertyTypeSymbol,
            Location serializedPropertyTypeLocation,
            string name,
            Location propertyNameLocation)
        {
            Location = propertyNameLocation;
            if (propertyTypeSymbol != null)
            {
                Diagnostic = SerializedType.CreateDiagnosticIfPropertyTypeIsNotValid(
                    serializedPropertyTypeSymbol, propertyTypeSymbol, serializedPropertyTypeLocation);
            }
            TypeSymbol = serializedPropertyTypeSymbol;
            Name = name;
        }


        public ITypeSymbol TypeSymbol { get; }
        public string Name { get; protected set; }

        public virtual AttributeSource GetAttributeSource()
        {
            return null;
        }

        public Location Location { get; }
        public Diagnostic Diagnostic { get; }
    }
}
