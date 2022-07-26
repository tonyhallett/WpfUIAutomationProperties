using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedType
    {
        internal static IEnumerable<SerializedType> From(IEnumerable<SerializedTypeClassDeclarationSyntaxWithGenerators> classesWithAttribute, Func<SemanticModel> semanticModelProvider)
        {
            return classesWithAttribute.Select(classWithAttribute =>
            {
                var serializedTypes = classWithAttribute.AttributeGenerators;
                var semanticModel = semanticModelProvider();
                
                var classSymbol = semanticModel.GetDeclaredSymbol(classWithAttribute.ClassDeclarationSyntax);
                var serializedProperties = new List<ISerializedProperty>();
                var allAttributeErrorDiagnostics = new List<Diagnostic>();
                var customDiagnostics = new List<Diagnostic>();
                var isSerializedType = false;
                foreach(var attributeData in classSymbol.GetAttributes())
                {
                    var correspondingSyntax = attributeData.ApplicationSyntaxReference.GetSyntax();
                    var correspondingAttributeWithGenerator = classWithAttribute.AttributeGenerators.FirstOrDefault(serializedTypeAttribute => serializedTypeAttribute.AttributeSyntax == correspondingSyntax);

                    if (correspondingAttributeWithGenerator != null)
                    {
                        isSerializedType = true;
                        var attributeErrorDiagnostics = semanticModel.GetErrorDiagnostics(correspondingSyntax).ToList();
                        allAttributeErrorDiagnostics.AddRange(attributeErrorDiagnostics);
                        if (!attributeErrorDiagnostics.Any())
                        {
                            var serializedPropertiesResult = correspondingAttributeWithGenerator.Generator.GetProperties(attributeData, correspondingSyntax as AttributeSyntax);
                            if (serializedPropertiesResult.Diagnostic != null)
                            {
                                customDiagnostics.Add(serializedPropertiesResult.Diagnostic);
                            }
                            else
                            {
                                serializedProperties.AddRange(serializedPropertiesResult.SerializedProperties);
                            }
                        }
                    }
                }

                if (isSerializedType)
                {
                    return new SerializedType(allAttributeErrorDiagnostics, customDiagnostics, serializedProperties, classSymbol);
                }
                return null;
            }).Where(stc => stc != null);
        }
        public static Diagnostic CreateDiagnosticIfPropertyTypeIsNotValid(
            ITypeSymbol serializedPropertyTypeSymbol,
            ITypeSymbol propertyTypeSymbol,
            Location serializedPropertyTypeLocation)
        {
            if (serializedPropertyTypeSymbol.SpecialType == SpecialType.System_String)
            {
                return null;
            }

            if (!serializedPropertyTypeSymbol.DerivesFrom(propertyTypeSymbol))
            {
                var serializeConvertTypeArgument = GetSerializeConvertTypeArgument(serializedPropertyTypeSymbol);
                if (serializeConvertTypeArgument == null || !SymbolEqualityComparer.Default.Equals(serializeConvertTypeArgument, propertyTypeSymbol))
                {
                    return Diagnostic.Create(
                        GetTypeDoesNotDerivedFromISerializeConvertDiagnosticDescriptor(),
                        serializedPropertyTypeLocation, TypeKeywords.Get(serializedPropertyTypeSymbol.Name),
                        TypeKeywords.Get(propertyTypeSymbol.Name)
                    );
                }
            }

            return null;
        }

        private static ITypeSymbol GetSerializeConvertTypeArgument(ITypeSymbol serializedPropertyTypeSymbol)
        {
            if (WpfAutomationProperties.ISerializeConvertSymbol == null) return null;
            var closedSerializeConvert =  serializedPropertyTypeSymbol.FindInterface(
                WpfAutomationProperties.ISerializeConvertSymbol
            );
            if (closedSerializeConvert == null) return null;

            return closedSerializeConvert.TypeArguments[0];
        }

        private static DiagnosticDescriptor GetTypeDoesNotDerivedFromISerializeConvertDiagnosticDescriptor()
        {
            return SerializedTypeDiagnosticDescriptor.Create(
                       3,
                       "Serialized type property type is not valid",
                       "Serialized type property type {0} is not valid. Should derive from {1}, be of type string or implement ISerializeConvert<{1}>",
                       DiagnosticSeverity.Error
                    );

        }
        public SerializedType(List<Diagnostic> attributesErrorDiagnostics, List<Diagnostic> customDiagnostics, List<ISerializedProperty> serializedProperties, INamedTypeSymbol symbol)
        {
            AttributesErrorDiagnostics = attributesErrorDiagnostics;
            CustomDiagnostics = customDiagnostics;
            Properties = serializedProperties;
            Symbol = symbol;

        }

        public INamedTypeSymbol Symbol { get; set; }
        public List<Diagnostic> AttributesErrorDiagnostics { get; }
        public List<Diagnostic> CustomDiagnostics { get; }
        public List<ISerializedProperty> Properties { get; }

    }
}
