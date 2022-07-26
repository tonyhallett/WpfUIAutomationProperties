using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    internal static class SemanticModelExtensions
    {
        public static IEnumerable<Diagnostic> GetErrorDiagnostics(this SemanticModel semanticModel, SyntaxNode syntaxNode)
        {
            return semanticModel.GetDiagnostics(syntaxNode.Span).Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error);
        }
    }
}
