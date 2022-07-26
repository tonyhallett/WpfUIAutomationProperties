using Microsoft.CodeAnalysis;
using SerializedTypeSourceGeneratorAttributes;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeAttachedTypedPropertyGenerator : SerializedTypeAttributeGeneratorBase<SerializedTypeAttachedTypedPropertyAttribute>
    {
        protected override int PropertiesIndex => 2;

        public class AttachedTypedSerializedProperty : TypedSerializedProperty
        {
            private readonly ITypeSymbol ownerTypeSymbol;
            private string dependencyPropertyName;
            public AttachedTypedSerializedProperty(
                ITypeSymbol ownerTypeSymbol,
                ITypeSymbol propertyTypeSymbol,
                ITypeSymbol serializedPropertyTypeSymbol,
                Location serializedPropertyTypeLocation,
                string dependencyPropertyName,
                Location dependencyPropertyNameLocation) : 
                    base(propertyTypeSymbol, serializedPropertyTypeSymbol, serializedPropertyTypeLocation, dependencyPropertyName, dependencyPropertyNameLocation)
            {
                this.ownerTypeSymbol = ownerTypeSymbol;
                this.dependencyPropertyName = dependencyPropertyName;
                if (DependencyPropertyName.IsNot(dependencyPropertyName))
                {
                    Name = dependencyPropertyName;
                }
                else
                {
                    Name = DependencyPropertyName.ToPropertyName(dependencyPropertyName);
                }
                
            }

            public override AttributeSource GetAttributeSource()
            {
                return AttachedAttributeSource.For(ownerTypeSymbol, dependencyPropertyName);
            }
        }

        protected override IEnumerable<ISerializedProperty> GetSerializedProperties()
        {
            var serializedPropertyTypeSymbol = attributeData.ConstructorArgumentValue<ITypeSymbol>(1);
            var serializedPropertyTypeLocation = attributeSyntax.ArgumentList.Arguments[1].GetLocation();

            var attachedTypeSymbol = attributeData.ConstructorArgumentValue<ITypeSymbol>(0);

            return GetPropertyNameLocations().Select(propertyNameAndLocation =>
            {
                var dependencyPropertyName = propertyNameAndLocation.PropertyName;
                var dependencyPropertyNameLocation = propertyNameAndLocation.Location;

                if (DependencyPropertyName.IsNot(dependencyPropertyName))
                {
                    return new AttachedTypedSerializedProperty(
                        attachedTypeSymbol, 
                        null, 
                        null, 
                        serializedPropertyTypeLocation, 
                        dependencyPropertyName, 
                        dependencyPropertyNameLocation);
                }

                return new AttachedTypedSerializedProperty(
                        attachedTypeSymbol,
                        attachedTypeSymbol.DependencyPropertyType(dependencyPropertyName),
                        serializedPropertyTypeSymbol,
                        serializedPropertyTypeLocation,
                        dependencyPropertyName,
                        dependencyPropertyNameLocation);
            });
        }
    }
}
