using SerializedTypeSourceGeneratorAttributes;

namespace SourceGeneratorTest
{
    public class SerializedTypeAttachedAttributeTests : SerializedTypeAttributeTestsBase
    {
        public SerializedTypeAttachedAttributeTests() : base(
            nameof(SerializedTypeAttachedAttribute),
            "Attacher",
            new GenerationDetails(
                new List<PropertyDetails> { 
                    new PropertyDetails(
                        "nameof(Attacher.MyPropertyProperty)",
                        "int",
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
                        "int",
                        "MyProperty",
                        "[AttachedProperty(typeof(Attacher), \"MyPropertyProperty\")]"
                    ),
                    new PropertyDetails(
                        "nameof(Attacher.MyProperty2Property)",
                        "int",
                        "MyProperty2",
                        "[AttachedProperty(typeof(Attacher), \"MyProperty2Property\")]"
                    )
                },
                new List<string> { "using AttacherNamespace;" },
                new List<string> { "System", "AttacherNamespace", "WpfUIAutomationProperties.Serialization" },
                Attacher.Code
            )
        )
        { }


        [TestCase("")]
        [TestCase("Ignore")]
        [TestCase("Warning")]
        [TestCase("Error")]

        public Task Should_Report_Diagnostic_For_Malformed_Dependency_Property_Name_According_To_MsBuild_Option(string option)
        {
            return MalformedDependencyPropertyTest.Should_Report_Diagnostic_According_To_MsBuild_Option(
                option, 
                "[SerializedTypeSourceGeneratorAttributes.SerializedTypeAttached(typeof(string)"
            );
        }
    }
}