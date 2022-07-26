using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using SerializedTypeSourceGenerator;

namespace SourceGeneratorTest
{
    public static class MalformedDependencyPropertyTest {

        public static Task Should_Report_Diagnostic_According_To_MsBuild_Option(string option, string attributePrefix)
        {
            var code = @$"
namespace TestSourceGenerator {{
{attributePrefix}, ""PropertyDoesNotEndWith"")]
    public partial class TextBlockSerialized
    {{

    }}
}}
";
            var expectedDiagnostic = new DiagnosticResult(
                new DiagnosticDescriptor(
                    "SerializedType1",
                    "Property not found",
                    "Property {0} not found",
                    "SerializedType",
                    option == "Warning" ? DiagnosticSeverity.Warning : DiagnosticSeverity.Error,
                    true
                )
            )
            .WithSpan(3, attributePrefix.Length + 3, 3, attributePrefix.Length + 27).WithMessage("Property PropertyDoesNotEndWith not found");

            var tester = new CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier>()
            {
                TestState =
                {
                    Sources = { code },
                }
            };

            TestStateReferences.AddReferences(tester);

            if (!String.IsNullOrEmpty(option))
            {
                SerializedTypeGeneratorMsBuildOptions.AddPropertiesNotFoundBehaviour(tester.TestState, option);
            }

            if (option != "Ignore")
            {
                tester.TestState.ExpectedDiagnostics.Add(expectedDiagnostic);
            }

            return tester.RunAsync();
        }
    }
}