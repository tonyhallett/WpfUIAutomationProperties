using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using SerializedTypeSourceGenerator;
using SerializedTypeSourceGeneratorAttributes;
using WpfUIAutomationProperties.Serialization;

namespace SourceGeneratorTest
{
    public static class TestStateReferences
    {
        public static void AddReferences(
            CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier> tester,
            bool addWpfUIAutomation = true
        )
        {
            if (addWpfUIAutomation)
            {
                tester.TestState.AdditionalReferences.Add(typeof(ISerializeConvert<>).Assembly);
            }
            tester.TestState.AdditionalReferences.Add(typeof(SerializedTypeAttachedAttribute).Assembly);
            tester.TestState.ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net472.Wpf;
            //tester.ReferenceAssemblies = ReferenceAssemblies.NetFramework.Net472.Wpf;
        }
    }
}