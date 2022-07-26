using Microsoft.CodeAnalysis;
using SerializedTypeSourceGeneratorAttributes;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeGenerator : SerializedTypeFrameworkElementAttributeGeneratorBase<SerializedTypeAttribute>
    {
        protected override int PropertiesIndex { get; } = 1;
        public class SerializedTypeProperty : ISerializedProperty
        {
            public SerializedTypeProperty(IPropertySymbol symbol, string name, Location location)
            {
                if (symbol != null)
                {
                    TypeSymbol = symbol.Type;
                }
                
                Name = name;
                this.Location = location;
            }

            public ITypeSymbol TypeSymbol { get; }
            public string Name { get; }

            public AttributeSource GetAttributeSource()
            {
                return null;
            }

            public Location Location { get; }
            public Diagnostic Diagnostic { get; }
        }

        protected override IEnumerable<ISerializedProperty> GetSerializedProperties()
        {
            return GetPropertyNameLocations().Select(propertyNameLocation =>
                new SerializedTypeProperty(
                        frameworkElementNamedTypeSymbol.LookupProperty(propertyNameLocation.PropertyName),
                        propertyNameLocation.PropertyName,
                        propertyNameLocation.Location
                    )
            );
        }
    }
}
