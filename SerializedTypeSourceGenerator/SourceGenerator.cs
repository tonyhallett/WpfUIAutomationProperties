using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        private Func<SerializedType, List<ISerializedProperty>, (bool, IEnumerable<Diagnostic>)> propertiesNotFoundBehaviour;
        private static readonly DiagnosticDescriptor duplicatePropertyDescriptor = 
            SerializedTypeDiagnosticDescriptor.Create(
                4,
                "Duplicate property",
                "Serialized type {0} has duplicate property {1}",
                DiagnosticSeverity.Error
            );

        public void Execute(GeneratorExecutionContext context)
        {
            //Debugger.Launch();
            WpfAutomationProperties.Initialize(context.Compilation);
            Generate(context);
        }

        private void Generate(GeneratorExecutionContext context)
        {
            propertiesNotFoundBehaviour = PropertiesNotFoundBehaviourProvider.Provide(context);
            GenerateFromSyntaxTrees(context, context.Compilation.SyntaxTrees);
        }

        private void GenerateFromSyntaxTrees(GeneratorExecutionContext context, IEnumerable<SyntaxTree> syntaxTrees)
        {
            var serializedTypes = GetSerializedTypes(
                    syntaxTrees,
                    (st) => context.Compilation.GetSemanticModel(st)
                );

            foreach (var serializedType in serializedTypes)
            {
                GenerateFromSerializedType(context, serializedType);
            }
        }

        private void GenerateFromSerializedType(GeneratorExecutionContext context, SerializedType serializedType)
        {
            if (!ReportSerializedTypeDiagnostics(context, serializedType))
            {
                var reportedPropertyDiagnostics = ReportSerializedTypePropertyDiagnostics(context, serializedType);
                var hasDuplicateProperties = ReportDuplicateProperties(context, serializedType);
                var shouldAddSource = ApplyPropertiesNotFoundBehaviour(context, serializedType);
                var hasNonSourceGeneratorErrorDiagnostics = serializedType.AttributesErrorDiagnostics.Any();

                if (!reportedPropertyDiagnostics && 
                    !hasDuplicateProperties && 
                    shouldAddSource && 
                    !hasNonSourceGeneratorErrorDiagnostics && 
                    serializedType.Properties.Any()
                )
                {
                    AddSource(context, serializedType);
                }
            }
        }

        private void AddSource(GeneratorExecutionContext context, SerializedType serializedType)
        {
            var (hintName, source) = SourceProvider.GetSource(serializedType);
            context.AddSource(hintName, source);
        }

        private bool ApplyPropertiesNotFoundBehaviour(GeneratorExecutionContext context, SerializedType serializedType)
        {
            var propertiesNotFound = serializedType.Properties.Where(serializedProperty => serializedProperty.TypeSymbol == null).ToList();
            var (shouldAddSource, diagnostics) = propertiesNotFoundBehaviour(serializedType, propertiesNotFound);
            foreach (var diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic);
            }
            if (shouldAddSource)
            {
                foreach (var propertyNotFound in propertiesNotFound)
                {
                    serializedType.Properties.Remove(propertyNotFound);
                }
            }
            return shouldAddSource;
        }

        private bool ReportSerializedTypePropertyDiagnostics(GeneratorExecutionContext context, SerializedType serializedType)
        {
            var reportedDiagnostics = false;
            var propertiesWithDiagnostics = serializedType.Properties.Where(serializedProperty => serializedProperty.Diagnostic != null);
            foreach (var propertyWithDiagnostic in propertiesWithDiagnostics)
            {
                reportedDiagnostics = true;
                context.ReportDiagnostic(propertyWithDiagnostic.Diagnostic);
            }
            return reportedDiagnostics;
        }

        private bool ReportSerializedTypeDiagnostics(GeneratorExecutionContext context, SerializedType serializedType)
        {
            var reportedDiagnostics = false;
            
            foreach (var customDiagnostic in serializedType.CustomDiagnostics)
            {
                reportedDiagnostics = true;
                context.ReportDiagnostic(customDiagnostic);
            }
            return reportedDiagnostics;
        }

        private bool ReportDuplicateProperties(GeneratorExecutionContext context, SerializedType serializedType)
        {
            var serializedTypeSymbol = serializedType.Symbol;
            var serializedTypeName = serializedTypeSymbol.Name;

            var hasDuplicates = false;
            var propertiesGroupedByName = serializedType.Properties.GroupBy(serializedProperty => serializedProperty.Name);
            var attributeDuplicateProperties = new List<ISerializedProperty>();
            foreach(var grouping in propertiesGroupedByName)
            {
                if (grouping.Count() > 1)
                {
                    hasDuplicates = true;
                    foreach(var property in grouping)
                    {
                        attributeDuplicateProperties.Add(property);
                        ReportDuplicateProperty(context, grouping.Key, property.Location, serializedTypeName);
                    }
                   
                }
            }
            
            
            var possibleTypeDuplicateProperties = serializedType.Properties.Except(attributeDuplicateProperties);

            foreach (var property in possibleTypeDuplicateProperties)
            {
                if (serializedTypeSymbol.HasMember(property.Name))
                {
                    hasDuplicates = true;
                    ReportDuplicateProperty(context, property.Name, property.Location, serializedTypeName);
                }
            }

            return hasDuplicates;
        }

        private static void ReportDuplicateProperty(GeneratorExecutionContext context,string propertyName, Location propertyLocation, string serializedTypeName)
        {
            var diagnostic = Diagnostic.Create(duplicatePropertyDescriptor, propertyLocation, serializedTypeName, propertyName);
            context.ReportDiagnostic(diagnostic);
        }

        private IEnumerable<SerializedType> GetSerializedTypes(IEnumerable<SyntaxTree> syntaxTrees, Func<SyntaxTree, SemanticModel> semanticModelProvider)
        {
            var typesWithAttribute = SerializedTypeSyntax.GetTypesWithSerializedTypeAttribute(syntaxTrees);
            return SerializedType.From(typesWithAttribute, semanticModelProvider);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
