using Microsoft.CodeAnalysis;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypeDiagnosticDescriptor
    {
        public const string Category = "SerializedType";

        public static DiagnosticDescriptor Create(int id, string title, string messageFormat, DiagnosticSeverity severity)
        {
            return new DiagnosticDescriptor(
                    $"SerializedType{id}",
                    title,
                    messageFormat,
                    Category, // does not get reported in visual studio Error List https://github.com/dotnet/roslyn/issues/62720
                    severity,
                    true);
        }
    }
}
