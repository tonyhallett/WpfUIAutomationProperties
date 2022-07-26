using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal abstract class SerializedTypeAttributeGeneratorBase<T> : SerializedTypeAttributeCheckingGeneratorBase<T> where T : Attribute
    {
        protected abstract int PropertiesIndex { get;}
        protected IEnumerable<PropertyNameLocation> GetPropertyNameLocations()
        {
            var firstProperty = attributeData.ConstructorArgumentValue<string>(PropertiesIndex);
            var additionalPropertyNames = attributeData.ConstructorArgumentValues<string>(PropertiesIndex + 1);
            var propertyNames = new string[] { firstProperty }.Concat(additionalPropertyNames);
            var locations = GetLocations(attributeSyntax);
            return propertyNames.Select((propertyName, i) => new PropertyNameLocation(propertyName, locations[i]));
        }

        private List<Location> GetLocations(AttributeSyntax attributeSyntax)
        {
            var arguments = attributeSyntax.ArgumentList.Arguments;
            var locations = new List<Location>{ arguments[PropertiesIndex].GetLocation() };
            if (arguments.Count >= PropertiesIndex + 2)
            {
                var paramsArgument = arguments[PropertiesIndex + 1];
                var paramsExpression = paramsArgument.Expression;
                var paramsExpressionKind = paramsExpression.Kind();
                IEnumerable<Location> additionalLocations = null;
                if (paramsExpressionKind == Microsoft.CodeAnalysis.CSharp.SyntaxKind.ArrayCreationExpression)
                {
                    var arrayCreationExpression = (ArrayCreationExpressionSyntax)paramsExpression;
                    var initializer = arrayCreationExpression.Initializer;
                    additionalLocations = initializer.Expressions.Select(expression => expression.GetLocation());
                }
                else
                {
                    additionalLocations = arguments.Skip(PropertiesIndex + 1).Select(argument => argument.GetLocation());
                }
                locations.AddRange(additionalLocations);
            }
            
            return locations;
        }
    }
}
