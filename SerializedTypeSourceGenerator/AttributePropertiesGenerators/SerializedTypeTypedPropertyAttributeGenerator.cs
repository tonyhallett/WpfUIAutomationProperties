using Microsoft.CodeAnalysis;
using SerializedTypeSourceGeneratorAttributes;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeTypedPropertyAttributeGenerator : SerializedTypeFrameworkElementAttributeGeneratorBase<SerializedTypeTypedPropertyAttribute>
    {
        protected override int PropertiesIndex => 2;

        protected override IEnumerable<ISerializedProperty> GetSerializedProperties()
        {
            var propertyType = attributeData.ConstructorArgumentValue<ITypeSymbol>(1);
            var propertyTypeLocation = attributeSyntax.ArgumentList.Arguments[1].GetLocation();

            return GetPropertyNameLocations().Select(propertyNameLocation =>
            {
                var propertyName = propertyNameLocation.PropertyName;
                var propertySymbol = frameworkElementNamedTypeSymbol.LookupProperty(propertyName);
                return new TypedSerializedProperty(propertySymbol?.Type, propertyType, propertyTypeLocation, propertyName, propertyNameLocation.Location);
            });
        }
    }
}
