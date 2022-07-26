using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using SerializedTypeSourceGeneratorAttributes;

namespace SourceGeneratorTest
{
    public class SerializedTypeAttributeTests : SerializedTypeAttributeTestsBase
    {
        public SerializedTypeAttributeTests() : base(
            nameof(SerializedTypeAttribute),
            "TextBlock",
            new GenerationDetails(
                new List<PropertyDetails> { new PropertyDetails("nameof(TextBlock.Text)", "string", "Text") },
                new List<string> { "using System.Windows.Controls;" },
                new List<string> { "System" }
            ),
            new GenerationDetails(
                 new List<PropertyDetails> {
                    new PropertyDetails("nameof(TextBlock.Text)", "string", "Text"),
                    new PropertyDetails("\"FontSize\"", "double", "FontSize")
                 },
                new List<string> { "using System.Windows.Controls;" },
                new List<string> { "System" }
            )
        )
        {

        }



        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_The_Type_Is_Not_A_Framework_Element()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedType(typeof(string), ""PropertyName"")]
    // this is good but will not be generated
    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(string), nameof(TextBlock.FontSize))]
    public partial class Serialized
    {

    }
}
";

            var expectedDiagnosticResult = new DiagnosticResult(
                new DiagnosticDescriptor(
                    "SerializedType2",
                    "Type is not a framework element",
                    "Type {0} is not a framework element",
                    "SerializedType",
                    DiagnosticSeverity.Error,
                    true)
            ).WithMessage("Type string is not a framework element")
            .WithSpan(6, 21, 6, 35);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnosticResult);

        }

        [Test]
        public Task Should_Generate_Properties_Defined_In_Base_Class()
        {
            string code = @"
using System.Windows.Controls;
namespace TestSourceGenerator {
[SerializedTypeSourceGeneratorAttributes.SerializedType(typeof(TextBlock),nameof(TextBlock.ActualHeight))]
public partial class TextBlockSerialized {

}
}
";
            
            string expectedGenerated = @"// Auto-generated code
using System;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public double ActualHeight { get; set; }
    }
}
";
            return base.TestGeneratesWithReferences(code, expectedGenerated);
        }

    }
}