using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using SerializedTypeSourceGeneratorAttributes;

namespace SourceGeneratorTest
{
    public class SerializedTypeAttachedTypedPropertyAttributeTests : SerializedTypeAttributeTestsBase
    {
        public SerializedTypeAttachedTypedPropertyAttributeTests() : base(
            nameof(SerializedTypeAttachedTypedPropertyAttribute),
            "Attacher",
            new GenerationDetails(
                new List<PropertyDetails> {
                    new PropertyDetails(
                        "nameof(Attacher.MyPropertyProperty)",
                        "string",
                        "MyProperty",
                        "[AttachedProperty(typeof(Attacher), \"MyPropertyProperty\")]"
                    )
                },
                new List<string> { "using AttacherNamespace;" },
                new List<string> { "System", "AttacherNamespace", "WpfUIAutomationProperties.Serialization" },
                Attacher.Code
            ),
            new GenerationDetails(
                new List<PropertyDetails> {
                    new PropertyDetails(
                        "nameof(Attacher.MyPropertyProperty)",
                        "string",
                        "MyProperty",
                        "[AttachedProperty(typeof(Attacher), \"MyPropertyProperty\")]"
                    ),
                    new PropertyDetails(
                        "nameof(Attacher.MyProperty2Property)",
                        "string",
                        "MyProperty2",
                        "[AttachedProperty(typeof(Attacher), \"MyProperty2Property\")]"
                    )
                },
                new List<string> { "using AttacherNamespace;" },
                new List<string> { "System", "AttacherNamespace", "WpfUIAutomationProperties.Serialization" },
                Attacher.Code
            )
            
        )
        {
            AdditionalTypeArgument = "string";
        }
        [Test]
        public Task Should_Generate_String_Properties()
        {
            var code = @"
namespace AttacherNamespace
{
    using System.Windows;

    public class Attacher
    {
        public static int GetMyProperty(DependencyObject obj)
        {
            return (int)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, int value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached(""MyProperty"", typeof(int), typeof(Attacher), new PropertyMetadata(0));
    }
}


namespace TestSourceGenerator
{
    using AttacherNamespace;

    [SerializedTypeSourceGeneratorAttributes.SerializedTypeAttachedTypedPropertyAttribute(typeof(Attacher), typeof(string), nameof(Attacher.MyPropertyProperty))]
    public partial class TextBlockSerialized
    {

    }
}
";
            var expectedGenerated = @"// Auto-generated code
using System;
using AttacherNamespace;
using WpfUIAutomationProperties.Serialization;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        [AttachedProperty(typeof(Attacher), ""MyPropertyProperty"")]
        public string MyProperty { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }

        [Test]
        public Task Should_Generate_Derivations()
        {
            var code = @"
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace TestSourceGenerator
{
    public class Attacher
    {
        public static Brush GetMyProperty(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, Brush value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached(""MyProperty"", typeof(Brush), typeof(Attacher), new PropertyMetadata(null));
    }


    [SerializedTypeSourceGeneratorAttributes.SerializedTypeAttachedTypedPropertyAttribute(typeof(Attacher), typeof(SolidColorBrush), nameof(Attacher.MyPropertyProperty))]    
    public partial class TextBlockSerialized
        {

        }
    }
";
            var expectedGenerated = @"// Auto-generated code
using System.Windows.Media;
using TestSourceGenerator;
using WpfUIAutomationProperties.Serialization;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        [AttachedProperty(typeof(Attacher), ""MyPropertyProperty"")]
        public SolidColorBrush MyProperty { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }

        [Test]
        public Task Should_Generate_ISerializeConvert()
        {
            var code = @"
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.Serialization;
namespace TestSourceGenerator
{
    public class Attacher
    {
        public static Brush GetMyProperty(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, Brush value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached(""MyProperty"", typeof(Brush), typeof(Attacher), new PropertyMetadata(null));
    }

    public class TestSerializePropertyConvert : ISerializeConvert<Brush>
    {
        public void Convert(Brush value)
        {
        }
    }

    [SerializedTypeSourceGeneratorAttributes.SerializedTypeAttachedTypedPropertyAttribute(typeof(Attacher), typeof(TestSerializePropertyConvert), nameof(Attacher.MyPropertyProperty))]       
    public partial class TextBlockSerialized
    {

    }
    }
";
            // todo do not add unnecessary usings
            var expectedGenerated = @"// Auto-generated code
using TestSourceGenerator;
using WpfUIAutomationProperties.Serialization;

namespace TestSourceGenerator
{
    partial class TextBlockSerialized
    {
        [AttachedProperty(typeof(Attacher), ""MyPropertyProperty"")]
        public TestSerializePropertyConvert MyProperty { get; set; }
    }
}
";
            return TestGeneratesWithReferences(code, expectedGenerated);
        }

        [TestCase("")]
        [TestCase("Ignore")]
        [TestCase("Warning")]
        [TestCase("Error")]

        public Task Should_Report_Diagnostic_For_Malformed_Dependency_Property_Name_According_To_MsBuild_Option(string option)
        {
            return MalformedDependencyPropertyTest.Should_Report_Diagnostic_According_To_MsBuild_Option(
                option, 
                "[SerializedTypeSourceGeneratorAttributes.SerializedTypeAttachedTypedPropertyAttribute(typeof(string),typeof(string)"
            );
        }

        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_The_Serialized_Property_Type_Is_Not_Valid()
        {
            var code = @"
using System.Windows.Controls;
using System.Windows;
using SerializedTypeSourceGeneratorAttributes;

namespace TestSourceGenerator {
    public class Attacher
    {
        public static string GetMyProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, string value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached(""MyProperty"", typeof(string), typeof(Attacher), new PropertyMetadata(""""));
    }

    [SerializedTypeAttachedTypedPropertyAttribute(typeof(Attacher), typeof(double), nameof(Attacher.MyPropertyProperty))]
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
            ).WithMessage("Serialized type property type double is not valid. Should derive from string, be of type string or implement ISerializeConvert<string>")
            .WithSpan(23, 69, 23, 83);


            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnosticResult);
        }

        [Test]
        public Task Should_Add_Error_Diagnostic_And_Not_Generate_If_ISerializeConvert_Has_Incorrect_Type_Argument()
        {
            var code = @"
using System.Windows;
using System.Windows.Controls;
using SerializedTypeSourceGeneratorAttributes;
using WpfUIAutomationProperties.Serialization;
namespace TestSourceGenerator {
    public class Attacher
    {
        public static string GetMyProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, string value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached(""MyProperty"", typeof(string), typeof(Attacher), new PropertyMetadata(""""));
    }

    public class IncorrectPropertyConvert : ISerializeConvert<double>
    {
        public void Convert(double value)
        {
        }
    }
    [SerializedTypeAttachedTypedPropertyAttribute(typeof(Attacher), typeof(IncorrectPropertyConvert), nameof(Attacher.MyPropertyProperty))]
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
            ).WithMessage("Serialized type property type IncorrectPropertyConvert is not valid. Should derive from string, be of type string or implement ISerializeConvert<string>")
            .WithSpan(29, 69, 29, 101);

            return TestNoGenerationWithDiagnosticWithReferences(code, expectedDiagnosticResult);
        }

    }
}