using Microsoft.CodeAnalysis;
using SerializedTypeSourceGeneratorAttributes;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedTypeAttachedGenerator : SerializedTypeAttributeGeneratorBase<SerializedTypeAttachedAttribute>
    {
        protected override int PropertiesIndex => 1;

        public class AttachedSerializedProperty : ISerializedProperty
        {
            public AttachedSerializedProperty(
                ITypeSymbol ownerTypeSymbol, 
                ITypeSymbol attachedPropertyTypeSymbol, 
                string dependencyPropertyName, 
                Location location)
            {
                
                Location = location;
                TypeSymbol = attachedPropertyTypeSymbol;
                this.dependencyPropertyName = dependencyPropertyName;
                Name = DependencyPropertyName.ToPropertyName(dependencyPropertyName);
                this.ownerTypeSymbol = ownerTypeSymbol;
            }

            public AttachedSerializedProperty(string malformedDependencyPropertyName, Location location)
            {
                Location = location;
                Name = malformedDependencyPropertyName;
            }

            public ITypeSymbol TypeSymbol { get; }
            public string Name { get; }

            private readonly ITypeSymbol ownerTypeSymbol;
            private readonly string dependencyPropertyName;

            public Location Location { get; }
            public Diagnostic Diagnostic { get; }

            public AttributeSource GetAttributeSource()
            {
                return AttachedAttributeSource.For(ownerTypeSymbol, dependencyPropertyName);
            }
        }

        protected override IEnumerable<ISerializedProperty> GetSerializedProperties()
        {
            var attachedTypeSymbol = attributeData.ConstructorArgumentValue<ITypeSymbol>(0);

            return GetPropertyNameLocations().Select(propertyNameLocation =>
            {
                var dependencyPropertyName = propertyNameLocation.PropertyName;
                var location = propertyNameLocation.Location;
                if (DependencyPropertyName.IsNot(dependencyPropertyName))
                {
                    return new AttachedSerializedProperty(dependencyPropertyName, location);
                }
                return new AttachedSerializedProperty(
                        attachedTypeSymbol,
                        attachedTypeSymbol.DependencyPropertyType(dependencyPropertyName),
                        dependencyPropertyName,
                        location
                    );
            });
        }
    }
}
