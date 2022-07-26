using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using SerializedTypeSourceGenerator;
using SerializedTypeSourceGeneratorAttributes;

namespace SourceGeneratorTest
{
    public class SerializedTypeTypedPropertyAttributeTests : SerializedTypeAttributeTestsBase
    {
        public SerializedTypeTypedPropertyAttributeTests() : base(
            nameof(SerializedTypeTypedPropertyAttribute),
            "TextBlock",
            new GenerationDetails(
                new List<PropertyDetails> {  
                    new PropertyDetails("nameof(TextBlock.FontSize)","string","FontSize")
                },
                new List<string> { "using System.Windows.Controls;"},
                new List<string> { "System"}
            ),
            new GenerationDetails(
                new List<PropertyDetails> {
                    new PropertyDetails("nameof(TextBlock.FontSize)","string","FontSize"),
                    new PropertyDetails("nameof(TextBlock.Background)","string","Background")
                },
                new List<string> { "using System.Windows.Controls;" },
                new List<string> { "System" }
            )
        )
        {
            AdditionalTypeArgument = "string";
        }

        [Test]
        public Task Should_Generate_String_Properties()
        {
            var code = @"
using System.Windows.Controls;
namespace TestSourceGenerator
{
    [SerializedTypeSourceGeneratorAttributes.SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(string), nameof(TextBlock.FontSize))]
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
        public string FontSize { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }

        [Test]
        public Task Should_Generate_Derivations()
        {
            var code = @"
using System.Windows.Controls;
using System.Windows.Media;
namespace TestSourceGenerator
{
    [SerializedTypeSourceGeneratorAttributes.SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(SolidColorBrush), nameof(TextBlock.Background))]
    public partial class TextBlockSerialized
    {

    }
}
";
            var expectedGenerated = @"// Auto-generated code
using System.Windows.Media;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public SolidColorBrush Background { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }

        [Test]
        public Task Should_Generate_ISerializeConvert()
        {
            var code = @"
using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.Serialization;
namespace TestSourceGenerator
{
    public class TestSerializePropertyConvert : ISerializeConvert<double>
    {
        public string ConvertedFontSize { get; set; }
        public void Convert(double value)
        {
            ConvertedFontSize = value.ToString();
        }
    }

    [SerializedTypeSourceGeneratorAttributes.SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(TestSerializePropertyConvert), nameof(TextBlock.FontSize))]
    public partial class TextBlockSerialized
    {

    }
}
";
            // todo do not add unnecessary usings
            var expectedGenerated = @"// Auto-generated code
using TestSourceGenerator;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public TestSerializePropertyConvert FontSize { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }

        [Test]
        public Task Should_Generate_ISerializeConvert_When_On_Base_Class()
        {
            var code = @"
using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.Serialization;
namespace TestSourceGenerator
{
    public abstract class Base : ISerializeConvert<double>
    {
        protected string More { get; set;} = """";
        public string ConvertedFontSize { get; set; }
        public void Convert(double value)
        {
            ConvertedFontSize = value.ToString() + More;
        }
    }

    public class TestSerializePropertyConvert : Base
    {
        public TestSerializePropertyConvert(){
            More = ""more"";
        }
        
    }

    [SerializedTypeSourceGeneratorAttributes.SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(TestSerializePropertyConvert), nameof(TextBlock.FontSize))]
    public partial class TextBlockSerialized
    {

    }
}
";
            // todo do not add unnecessary usings
            var expectedGenerated = @"// Auto-generated code
using TestSourceGenerator;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        public TestSerializePropertyConvert FontSize { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }


        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_The_Type_Is_Not_A_Framework_Element()
        {
            var code = @"
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeTypedPropertyAttribute(typeof(int), typeof(string), ""PropertyName"")]
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
            ).WithMessage("Type int is not a framework element")
            .WithSpan(5, 43, 5, 54);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnosticResult);
        }

        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_The_Serialized_Property_Type_Is_Not_Valid()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(double), nameof(TextBlock.Background))]
    // this is good but will not get generated
    [SerializedTypeAttribute(typeof(TextBlock), nameof(TextBlock.Text))]
    public partial class Serialized
    {

    }
}
";

            var expectedDiagnosticResult = new DiagnosticResult(
                new DiagnosticDescriptor(
                    "SerializedType3",
                    "Serialized type property type is not valid",
                    "Serialized type property type {0} is not valid. Should derive from {1}, be of type string or implement ISerializeConvert<{1}>",
                    "SerializedType",
                    DiagnosticSeverity.Error,
                    true
                )
            ).WithMessage("Serialized type property type double is not valid. Should derive from Brush, be of type string or implement ISerializeConvert<Brush>")
            .WithSpan(6, 62, 6, 76);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnosticResult);
        }

        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_The_Serialized_Property_Type_Is_Not_Valid_No_WpfUIAutomation_Reference()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
namespace TestSourceGenerator {

    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(double), nameof(TextBlock.Background))]
    public partial class Serialized
    {

    }
}
";
            var tester = new CSharpSourceGeneratorTest<SourceGenerator, NUnitVerifier>()
            {
                TestState =
                {
                    Sources = { code },
                },
            };
            TestStateReferences.AddReferences(tester, false);

            var expectedDiagnosticResult = new DiagnosticResult(
                new DiagnosticDescriptor(
                    "SerializedType3",
                    "Serialized type property type is not valid",
                    "Serialized type property type {0} is not valid. Should derive from {1}, be of type string or implement ISerializeConvert<{1}>",
                    "SerializedType",
                    DiagnosticSeverity.Error,
                    true
                )
            ).WithMessage("Serialized type property type double is not valid. Should derive from Brush, be of type string or implement ISerializeConvert<Brush>")
            .WithSpan(6, 62, 6, 76);

            tester.TestState.ExpectedDiagnostics.Add(expectedDiagnosticResult);

            return tester.RunAsync();
        }

        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_ISerializeConvert_Has_Incorrect_Type_Argument()
        {
            var code = @"
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
using WpfUIAutomationProperties.Serialization;
namespace TestSourceGenerator {

    public class IncorrectPropertyConvert : ISerializeConvert<double>
    {
        public void Convert(double value)
        {
        }
    }

    [SerializedTypeTypedPropertyAttribute(typeof(TextBlock), typeof(IncorrectPropertyConvert), nameof(TextBlock.Background))]
    public partial class Serialized
    {

    }
}
";
            var expectedDiagnosticResult = new DiagnosticResult(
                new DiagnosticDescriptor(
                    "SerializedType3",
                    "Serialized type property type is not valid",
                    "Serialized type property type {0} is not valid. Should derive from {1}, be of type string or implement ISerializeConvert<{1}>",
                    "SerializedType",
                    DiagnosticSeverity.Error,
                    true
                )
            ).WithMessage("Serialized type property type IncorrectPropertyConvert is not valid. Should derive from Brush, be of type string or implement ISerializeConvert<Brush>")
            .WithSpan(14, 62, 14, 94);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnosticResult);
        }

        
    }
}