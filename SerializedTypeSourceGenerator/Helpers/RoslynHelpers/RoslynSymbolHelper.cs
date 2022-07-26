using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    public static class RoslynSymbolHelper
    {
        public static T ConstructorArgumentValue<T>(this AttributeData attributeData, int index)
        {
            return (T)attributeData.ConstructorArguments[index].Value;
        }

        public static IEnumerable<T> ConstructorArgumentValues<T>(this AttributeData attributeData, int index)
        {
            return attributeData.ConstructorArguments[index].Values.Select(typedConstant => (T)typedConstant.Value);
        }

        public static bool DerivesFrom(this INamedTypeSymbol lookupSymbol, string baseTypeName, string baseTypeNamespace)
        {
            var derivesFrom = false;
            while (lookupSymbol != null)
            {
                if (lookupSymbol.Name == baseTypeName && lookupSymbol.ContainingNamespace.ToDisplayString() == baseTypeNamespace)
                {
                    derivesFrom = true;
                    break;
                }
                lookupSymbol = lookupSymbol.BaseType;
            }
            return derivesFrom;
        }

        public static INamedTypeSymbol FindInterface(this ITypeSymbol derivedType, ITypeSymbol interfaceType)
        {
            return derivedType.AllInterfaces.FirstOrDefault(interfaceSymbol =>
            {
                if (interfaceSymbol.ContainingAssembly.Name == interfaceType.ContainingAssembly.Name
                    && interfaceSymbol.ContainingNamespace.ToDisplayString() == interfaceType.ContainingNamespace.ToDisplayString()
                    && interfaceSymbol.Name == interfaceType.Name)
                {
                    return true;
                }
                return false;
            });
        }

        public static bool DerivesFrom(this ITypeSymbol derivedType, ITypeSymbol baseType)
        {
            var derivesFrom = false;

            var lookupSymbol = derivedType;
            while (lookupSymbol != null)
            {
                if (SymbolEqualityComparer.Default.Equals(lookupSymbol,baseType))
                {
                    derivesFrom = true;
                    break;
                }
                lookupSymbol = lookupSymbol.BaseType;
            }
            return derivesFrom;
        }

        public static bool HasMember(this ITypeSymbol lookupType, string memberName)
        {
            while (true)
            {
                if (lookupType.GetMembers(memberName).Any())
                {
                    return true;
                }
                
                lookupType = lookupType.BaseType;
                if (lookupType == null)
                {
                    break;
                }
            }
            return false;
        }

        public static IPropertySymbol LookupProperty(this ITypeSymbol lookupType, string propertyName)
        {
            IPropertySymbol propertySymbol = null;
            while (true)
            {
                var memberSymbol = lookupType.GetMembers(propertyName).FirstOrDefault();
                if (memberSymbol == null)
                {
                    lookupType = lookupType.BaseType;
                    if (lookupType == null)
                    {
                        break;
                    }
                }
                else
                {
                    if (memberSymbol.Kind == SymbolKind.Property)
                    {
                        propertySymbol = memberSymbol as IPropertySymbol;
                    }
                    break;
                }

            }
            return propertySymbol;
        }

        public static ITypeSymbol DependencyPropertyType(this ITypeSymbol ownerType, string dependencyPropertyName)
        {
            var name = DependencyPropertyName.ToPropertyName(dependencyPropertyName);
            var getAccessorMember = ownerType.GetMembers($"Get{name}").FirstOrDefault();
            if (getAccessorMember != null && getAccessorMember is IMethodSymbol getAccessorMethod)
            {
                return getAccessorMethod.ReturnType;
            }

            var setAccessorMember = ownerType.GetMembers($"Set{name}").FirstOrDefault();
            if (setAccessorMember != null && setAccessorMember is IMethodSymbol setAccessorMethod)
            {
                var parameters = setAccessorMethod.Parameters;
                if (parameters.Count() == 2)
                {
                    return parameters[1].Type;
                }
            }
            return null;
        }

        public static (string name, List<string> namespaces) GetTypeNameAndNamespaces(ITypeSymbol type)
        {
            switch (type.Kind)
            {
                case SymbolKind.ArrayType:
                    var arrayTypeSymbol = type as IArrayTypeSymbol;
                    var namespaces = new List<string> { type.ContainingNamespace.ToDisplayString() };
                    var (arrayElementName, arrayElementNamespaces) = GetTypeNameAndNamespaces(arrayTypeSymbol.ElementType);
                    namespaces.AddRange(arrayElementNamespaces);
                    return ($"{arrayElementName}[]", namespaces);
                case SymbolKind.ErrorType:
                    throw new ArgumentException("Type is IErrorType", nameof(type));
                case SymbolKind.DynamicType:
                    return ("dynamic", new List<string> { type.ContainingNamespace.ToDisplayString() });
                case SymbolKind.FunctionPointerType:
                    throw new NotImplementedException();
                case SymbolKind.PointerType:
                    throw new NotImplementedException();
                case SymbolKind.NamedType:
                    return GetTypeNameAndNamespaces(type as INamedTypeSymbol);
                default:
                    throw new Exception("Unexpected type symbol");
            }
        }

        public static (string name, List<string> namespaces) GetTypeNameAndNamespaces(INamedTypeSymbol type)
        {
            var namespaces = new List<string> { type.ContainingNamespace.ToDisplayString() };
            var typeArguments = type.TypeArguments;
            var typeName = TypeKeywords.Get(type.Name);
            if (typeArguments.Length == 0)
            {
                return (typeName, namespaces);
            }

            typeName += "<";
            for (var i = 0; i < typeArguments.Length; i++)
            {
                var typeArgument = typeArguments[i];
                var (name, typeNamespaces) = GetTypeNameAndNamespaces(typeArgument);
                namespaces.AddRange(typeNamespaces);
                typeName += name;
                if (i != typeArguments.Length - 1)
                {
                    typeName += ", ";
                }
            }

            return (typeName += ">", namespaces.Where(ns => ns != null).ToList());
        }
    } 
}
