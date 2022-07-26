using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class PropertiesNotFoundBehaviourProvider
    {
        private enum PropertiesNotFoundBehaviour { Error, Ignore, Warning }
        public const string MsBuildPropertyName = "SerializedType_PropertiesNotFoundBehaviour";

        private static readonly Dictionary<
            PropertiesNotFoundBehaviour, 
            Func<SerializedType, List<ISerializedProperty>, (bool,IEnumerable<Diagnostic>)>
        > propertiesNotFoundBehaviours =
            new Dictionary<
                PropertiesNotFoundBehaviour, 
                Func<SerializedType, List<ISerializedProperty>, (bool, IEnumerable<Diagnostic>)>
            >
            {
                { PropertiesNotFoundBehaviour.Ignore, IgnorePropertiesNotFoundBehaviour },
                { PropertiesNotFoundBehaviour.Warning, WarnPropertiesNotFoundBehaviour },
                { PropertiesNotFoundBehaviour.Error, ErrorPropertiesNotFoundBehaviour },
            };


        private static (bool, IEnumerable<Diagnostic>) IgnorePropertiesNotFoundBehaviour(SerializedType serializedTypeSymbol, List<ISerializedProperty> propertiesNotFound)
        {
            return (true,Enumerable.Empty<Diagnostic>());
        }

        private static (bool, IEnumerable<Diagnostic>) WarnPropertiesNotFoundBehaviour(SerializedType serializedTypeSymbol, List<ISerializedProperty> propertiesNotFound)
        {
            return (true, GetPropertiesNotFoundDiagnostics(false, propertiesNotFound));
        }

        private static (bool, IEnumerable<Diagnostic>) ErrorPropertiesNotFoundBehaviour(SerializedType serializedTypeSymbol, List<ISerializedProperty> propertiesNotFound)
        {
            return (propertiesNotFound.Count == 0, GetPropertiesNotFoundDiagnostics(true, propertiesNotFound));
        }

        private static DiagnosticDescriptor GetPropertiesNotFoundDiagnosticDescriptor(bool isError)
        {
            return SerializedTypeDiagnosticDescriptor.Create(
                        1,
                        "Property not found",
                        "Property {0} not found",
                        isError ? DiagnosticSeverity.Error : DiagnosticSeverity.Warning
                    );
        }

        private static IEnumerable<Diagnostic> GetPropertiesNotFoundDiagnostics(bool isError, List<ISerializedProperty> propertiesNotFound)
        {
            return propertiesNotFound.Select(propertyNotFound => 
                Diagnostic.Create( 
                    GetPropertiesNotFoundDiagnosticDescriptor(isError),
                    propertyNotFound.Location,
                    propertyNotFound.Name)
            );
            
        }

        public static Func<SerializedType, List<ISerializedProperty>, (bool, IEnumerable<Diagnostic>)> Provide(GeneratorExecutionContext context)
        {
            var propertiesNotFoundBehaviourOption = context.GetMSBuildProperty(MsBuildPropertyName, "Error");
            Enum.TryParse(propertiesNotFoundBehaviourOption, out PropertiesNotFoundBehaviour propertiesNotFoundBehaviour);
            return propertiesNotFoundBehaviours[propertiesNotFoundBehaviour];
        }
    }
}
