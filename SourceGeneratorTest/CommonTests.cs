using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
using SerializedTypeSourceGenerator;
using System.Text;

namespace SourceGeneratorTest
{
    public class CommonTests : TestsBase
    {
        [Test]
        public Task Should_Generate_SerializedType_When_Another_Is_Not_Generated()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeAttribute(typeof(TextBlock), ""MissingProperty"")]
    public partial class NotGenerated
    {

    }

    [SerializedTypeAttribute(typeof(TextBlock), nameof(TextBlock.Text))]
    public partial class TextBlockSerialized
    {

    }
}
";
            var expectedGenerated = @"// Auto-generated code
using System;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public string Text { get; set; }
    }
}
";
            return TestGeneratesWithReferences(
                code, 
                expectedGenerated, 
                testState => SerializedTypeGeneratorMsBuildOptions.AddPropertiesNotFoundBehaviour(testState, "Ignore")
            );
        }

        [Test]
        public Task Should_Generate_Multiple_SerializedTypes()
        {
            var code1 = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeAttribute(typeof(TextBlock), nameof(TextBlock.Text))]
    public partial class TextBlockSerialized
    {

    }
}
";
            var code2 = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeAttribute(typeof(CheckBox), nameof(CheckBox.IsChecked))]
    public partial class CheckBoxSerialized
    {

    }
}
";
            var expectedGenerated1 = @"// Auto-generated code
using System;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public string Text { get; set; }
    }
}
";
            var expectedGenerated2 = @"// Auto-generated code
using System;

namespace TestSourceGenerator
{
    partial class CheckBoxSerialized
    {
        public Nullable<bool> IsChecked { get; set; }
    }
}
";
            var tester = new CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier>()
            {
                TestState =
                {
                    Sources = { code1, code2 },
                    GeneratedSources =
                    {
                        (typeof(SourceGenerator), "TextBlockSerialized.g.cs", SourceText.From(expectedGenerated1, Encoding.UTF8)),
                        (typeof(SourceGenerator), "CheckBoxSerialized.g.cs", SourceText.From(expectedGenerated2, Encoding.UTF8))
                    }
                }
            };
            TestStateReferences.AddReferences(tester);
            return tester.RunAsync();
        }

        [Test]
        public Task Should_Not_Generate_When_Attribute_Has_Error_Diagnostics()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedType(typeof(unknown), ""PropertyName"")]
    // this is good but will not be generated
    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(string), nameof(TextBlock.FontSize))]
    public partial class Serialized
    {

    }
}
";
            var expectedDiagnostic = DiagnosticResult.CompilerError("CS0246")
                .WithSpan(6, 28, 6, 35).WithArguments("unknown");
            
            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnostic);

        }

        [Test]
        public Task Should_Not_Generate_When_Duplicate_Properties_And_Should_Report_Each_Duplicate_As_Error()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;

namespace TestSourceGenerator {

    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(string), nameof(TextBlock.Text))]
    [SerializedTypeAttribute(typeof(TextBlock), nameof(TextBlock.Text))]
    // this attribute is good but there will be no generation
    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(string), nameof(TextBlock.Background))]
    public partial class TextBlockSerialized
    {

    }
}
";
            var expectedDiagnosticDescriptor = new DiagnosticDescriptor(
                "SerializedType4",
                "Duplicate property",
                "Serialized type {0} has duplicate property {1}",
                "SerializedType",
                DiagnosticSeverity.Error,
                true
            );

            var expectedDiagnostic1 = new DiagnosticResult(
                expectedDiagnosticDescriptor
            ).WithArguments("TextBlockSerialized", "Text")
            .WithSpan(7, 78, 7, 100);

            var expectedDiagnostic2 = new DiagnosticResult(
                expectedDiagnosticDescriptor
            ).WithArguments("TextBlockSerialized", "Text")
            .WithSpan(8, 49, 8, 71);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnostic1, expectedDiagnostic2);
        }

        [Test]
        public Task Should_Not_Generate_When_Duplicate_Properties_Defined_On_The_Serialized_Type_And_Should_Report_Each_Duplicate_As_Error()
        {
            var code = @"
using System.Windows.Media;
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;

namespace TestSourceGenerator {

    public class Base {
        public string Text { get; set; }
    }

    [SerializedTypeAttribute(typeof(TextBlock), nameof(TextBlock.Text), nameof(TextBlock.Background))]
    public partial class TextBlockSerialized : Base
    {
        public Brush Background { get; set; }
    }
}
";
            var expectedDiagnosticDescriptor = new DiagnosticDescriptor(
                "SerializedType4",
                "Duplicate property",
                "Serialized type {0} has duplicate property {1}",
                "SerializedType",
                DiagnosticSeverity.Error,
                true
            );

            var expectedDiagnostic1 = new DiagnosticResult(
                expectedDiagnosticDescriptor
            ).WithArguments("TextBlockSerialized", "Text")
            .WithSpan(12, 49, 12, 71);

            var expectedDiagnostic2 = new DiagnosticResult(
                expectedDiagnosticDescriptor
            ).WithArguments("TextBlockSerialized", "Background")
            .WithSpan(12, 73, 12, 101);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnostic1, expectedDiagnostic2);
        }

        private const string MissingPropertyCode = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeAttribute(typeof(TextBlock),nameof(TextBlock.Background), ""MissingProperty"")]
    public partial class TextBlockSerialized
    {

    }
}
";
        private const string MissingPropertyIgnoreWarningExpectedGeneratedCode = @"// Auto-generated code
using System.Windows.Media;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public Brush Background { get; set; }
    }
}
";

        [Test]
        public Task Should_Ignore_Missing_Properties_And_Generate_When_Configured_So_In_Project_Async()
        {
            return TestGeneratesWithReferences(
                MissingPropertyCode,
                MissingPropertyIgnoreWarningExpectedGeneratedCode,
                testState =>
                {
                    SerializedTypeGeneratorMsBuildOptions.AddPropertiesNotFoundBehaviour(testState, "Ignore");
                }
            );
        }

        [Test]
        public Task Should_Add_Warning_Diagnostic_And_Generate_When_Configured_To_Warn_In_Project_Async()
        {
            return TestGeneratesWithReferences(
                MissingPropertyCode,
                MissingPropertyIgnoreWarningExpectedGeneratedCode,
                testState =>
                {
                    testState.ExpectedDiagnostics.Add(GetMissingPropertyDiagnostic(false));
                    SerializedTypeGeneratorMsBuildOptions.AddPropertiesNotFoundBehaviour(testState, "Warning");
                }
            );
        }

        [Test]
        public Task Should_Create_Error_Diagnostic_And_Not_Generate_When_Configured_So_In_Project()
        {
            return TestNoGenerationWithDiagnosticWithReferences(
                MissingPropertyCode,
                testState => SerializedTypeGeneratorMsBuildOptions.AddPropertiesNotFoundBehaviour(testState, "Error"),
                GetMissingPropertyDiagnostic(true)
            );
        }

        [Test]
        public Task Should_Create_Error_Diagnostic_And_Not_Generate_By_Default_Async()
        {
            return TestNoGenerationWithDiagnosticWithReferences(
                MissingPropertyCode,
                GetMissingPropertyDiagnostic(true)
            );
        }

        private DiagnosticResult GetMissingPropertyDiagnostic(bool isError)
        {
            return new DiagnosticResult(
                    new DiagnosticDescriptor(
                        "SerializedType1",
                        "Property not found",
                        "Property {0} not found",
                        "SerializedType",
                         isError ? DiagnosticSeverity.Error : DiagnosticSeverity.Warning,
                         true)
                ).WithSpan(6, 78, 6, 95).WithMessage("Property MissingProperty not found");
        }

    }
}