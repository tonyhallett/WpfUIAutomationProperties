using Microsoft.CodeAnalysis;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class GeneratorExecutionContextExtensions
    {
        public static string GetMSBuildProperty(
            this GeneratorExecutionContext context,
            string name,
            string defaultValue = "")
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.{name}", out var value);
            return value ?? defaultValue;
        }
    }
}
