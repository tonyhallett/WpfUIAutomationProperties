using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
using SerializedTypeSourceGenerator;
using System.Text;

namespace SourceGeneratorTest
{
    // Note that I had to add nuget Microsoft,CodeAnalysis.CSharp.Workspaces to remove version conflicts
    public abstract class TestsBase
    {
        protected Task TestGeneratesWithReferences(string code, string expectedGenerated, Action<SolutionState>? additionalSetup = null)
        {
            var tester = new CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier>()
            {
                TestState =
                {
                    Sources = { code },
                    GeneratedSources =
                    {
                        (typeof(SourceGenerator), "TextBlockSerialized.g.cs", SourceText.From(expectedGenerated, Encoding.UTF8))
                    }
                }
            };
            TestStateReferences.AddReferences(tester);

            additionalSetup?.Invoke(tester.TestState);
            
            return tester.RunAsync();
        }

        protected Task TestNoGenerationWithNoDiagnostics(string code, Action<SolutionState>? additionalSetup = null)
        {
            var tester = GetTesterWithReferencesNoGeneration(code);
            additionalSetup?.Invoke(tester.TestState);
            return tester.RunAsync();

        }

        protected Task TestNoGenerationWithDiagnosticWithReferences(string code, params DiagnosticResult[] expectedDiagnostics)
        {
            var tester = GetTesterWithReferencesNoGeneration(code);
            tester.TestState.ExpectedDiagnostics.AddRange(expectedDiagnostics);
            return tester.RunAsync();
        }

        protected Task TestNoGenerationWithDiagnosticWithReferences(string code, Action<SolutionState> additionalSetup, params DiagnosticResult[] expectedDiagnostics)
        {
            var tester = GetTesterWithReferencesNoGeneration(code);
            tester.TestState.ExpectedDiagnostics.AddRange(expectedDiagnostics);
            additionalSetup.Invoke(tester.TestState);
            return tester.RunAsync();
        }

        private CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier> GetTesterWithReferencesNoGeneration(string code)
        {
            var tester = new CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier>()
            {
                TestState =
                {
                    Sources = { code },
                },
            };
            TestStateReferences.AddReferences(tester);
            return tester;
        }
    }
}