using Microsoft.CodeAnalysis;
using System.Text;

namespace SourceGeneratorTest
{
    public class GenerationDetails
    {
        public GenerationDetails(
            List<PropertyDetails> properties,
            List<string> usings,
            List<string> generatedUsingNamespaces,
            string? additionalCode = null)
        {
            Properties = properties;
            Usings = usings;
            GeneratedUsingNamespaces = generatedUsingNamespaces;
            AdditionalCode = additionalCode;
        }
        public string? AdditionalCode { get; set; }
        public List<string> Usings { get; }
        public List<string> GeneratedUsingNamespaces { get; }
        public List<PropertyDetails> Properties { get; }

    }

    public class PropertyDetails
    {
        public PropertyDetails(string propertyArgument, string generatedPropertyType, string generatedPropertyName,string? generatedPropertyAttribute = null)
        {
            PropertyArgument = propertyArgument;
            GeneratedPropertyType = generatedPropertyType;
            GeneratedPropertyName = generatedPropertyName;
            GeneratedPropertyAttribute = generatedPropertyAttribute;
        }

        public string PropertyArgument { get; set; }
        public string GeneratedPropertyType { get; set; }
        public string GeneratedPropertyName { get; set; }
        public string? GeneratedPropertyAttribute { get; set; }
    }

    public abstract class SerializedTypeAttributeTestsBase : TestsBase
    {
        public SerializedTypeAttributeTestsBase(
            string attributeNameWithAttributeSuffix,
            string typeArgument,
            GenerationDetails singlePropertyTest, 
            GenerationDetails multiplesPropertiesTest
        )
        {
            AttributeNameWithoutAttributeSuffix = attributeNameWithAttributeSuffix.Replace("Attribute", "");
            TypeArgument = typeArgument;
            SinglePropertyTestDetails = singlePropertyTest;
            MultiplesPropertiesTestDetails = multiplesPropertiesTest;
        }

        public string AttributeNameWithoutAttributeSuffix { get; }
        public string TypeArgument { get; }
        public string? AdditionalTypeArgument { get; protected set; }
        protected GenerationDetails SinglePropertyTestDetails { get; set; }
        protected GenerationDetails MultiplesPropertiesTestDetails { get; set; }
        protected 

        private string GetUsings(List<string> namespacesOrUsings,bool isUsing)
        {
            var usingBuilder = new StringBuilder();
            var num = namespacesOrUsings.Count;
            for (var i = 0;i < num; i++)
            {
                var isLast = i == num - 1;
                var namespaceOrUsing = namespacesOrUsings[i];
                if (isUsing)
                {
                    usingBuilder.Append(namespaceOrUsing);
                }
                else
                {
                    usingBuilder.Append($"using {namespaceOrUsing};");
                }
                if (!isLast)
                {
                    usingBuilder.AppendLine("");
                }
                
            }
            return usingBuilder.ToString();
        }
        protected Task SinglePropertyTest(
            List<string> usings,
            List<string> generatedUsingNamespaces,
            string attributeName,
            PropertyDetails propertyDetails,
            IEnumerable<string> additionalCode,
            bool withNamespace = true,
            bool partialClass = true
            )
        {
            return PropertiesTest(
                usings,
                generatedUsingNamespaces,
                attributeName,
                propertyDetails.PropertyArgument,
                new List<PropertyDetails> { propertyDetails },
                additionalCode,
                withNamespace,
                partialClass
                );
        }

        protected Task PropertiesTest(
            List<string> usings,
            List<string> generatedUsingNamespaces,
            string attributeName,
            string propertyArguments,
            List<PropertyDetails> properties,
            IEnumerable<string> additionalCode,
            bool withNamespace = true,
            bool partialClass = true
            )
        {
            var additionalTypeArgument = AdditionalTypeArgument == null ? "" : $"typeof({AdditionalTypeArgument}),";
            var classOrStruct = partialClass ? "class" : "struct";
            var code = withNamespace ? @$"
{GetUsings(usings, true)}
namespace TestSourceGenerator {{

    [{attributeName}(typeof({TypeArgument}),{additionalTypeArgument} {propertyArguments})]
    partial {classOrStruct} TextBlockSerialized
    {{

    }}
}}
" :
@$"
{GetUsings(usings, true)}
    [{attributeName}(typeof({TypeArgument}),{additionalTypeArgument} {propertyArguments})]
    partial {classOrStruct} TextBlockSerialized
    {{

    }}
";
            var generatedPropertiesBuilder = new StringBuilder();
            var numProperties = properties.Count;
            for (var i = 0; i< properties.Count;i++)
            {
                var property = properties[i];
                if (property.GeneratedPropertyAttribute != null)
                {
                    generatedPropertiesBuilder.AppendLine($"        {property.GeneratedPropertyAttribute}");
                }
                generatedPropertiesBuilder.Append($"        public {property.GeneratedPropertyType} {property.GeneratedPropertyName} {{ get; set; }}");
                var isLast = i == numProperties - 1;
                if (!isLast)
                {
                    generatedPropertiesBuilder.AppendLine("");
                }
            }
            
            var expectedGenerated = withNamespace ? @$"// Auto-generated code
{GetUsings(generatedUsingNamespaces, false)}

namespace TestSourceGenerator
{{
    partial {classOrStruct} TextBlockSerialized
    {{
{generatedPropertiesBuilder}
    }}
}}
" :
@$"// Auto-generated code
{GetUsings(generatedUsingNamespaces, false)}

partial {classOrStruct} TextBlockSerialized
{{
{generatedPropertiesBuilder}
}}
";
            return TestGeneratesWithReferences(code, expectedGenerated, testState =>
            {
                foreach (var code in additionalCode)
                {
                    testState.Sources.Add(code);
                }
            });
        }

        private const string SerializedTypeSourceGeneratorAttributesNamespace = "SerializedTypeSourceGeneratorAttributes";

        [Test]
        public Task Should_Generate_From_Fully_Qualified_Async()
        {
            var fullyQualifiedAttributeName = $"{SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}Attribute";
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                fullyQualifiedAttributeName,
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode)
                );
        }

        private IEnumerable<string> GetAdditionalCode(params string?[] additionalCode)
        {
            return additionalCode.Where(additional => additional != null).OfType<string>();
        }

        [Test]
        public Task Should_Generate_From_Using_Async()
        {
            SinglePropertyTestDetails.Usings.Add($"using {SerializedTypeSourceGeneratorAttributesNamespace};");
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                $"{AttributeNameWithoutAttributeSuffix}Attribute",
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode)
                );
        }

        [Test]
        public Task Should_Work_With_Global_Usings()
        {
            var globalUsing = @"
global using SerializedTypeSourceGeneratorAttributes;
";

            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                $"{AttributeNameWithoutAttributeSuffix}Attribute",
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode,globalUsing)
                );
        }

        [Test]
        public Task Should_Work_With_Global_Class_Alias()
        {
            var globalUsing = @$"
global using Alias = SerializedTypeSourceGeneratorAttributes.{AttributeNameWithoutAttributeSuffix}Attribute;
";

            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                "Alias",
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode, globalUsing)
                );
        }

        [Test]
        public Task Should_Work_With_Global_Namespace_Alias()
        {
            var globalUsing = @$"
global using Alias = SerializedTypeSourceGeneratorAttributes;
";

            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                $"Alias.{AttributeNameWithoutAttributeSuffix}",
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode, globalUsing)
                );
        }

        [Test]
        public Task Should_Generate_Without_Attribute_Suffix_Async()
        {
            var fullyQualifiedAttributeName = $"{SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}";
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                fullyQualifiedAttributeName,
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode)
                );
        }

        [Test]
        public Task Should_Generate_When_Class_Aliased_Async()
        {
            var aliased = "Aliased";
            var aliasedUsing = $"using {aliased} = {SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}Attribute;";
            SinglePropertyTestDetails.Usings.Add(aliasedUsing);
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                aliased,
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode)
                );
        }

        [Test]
        public Task Should_Generate_When_Namespace_Aliased_Async()
        {
            var aliasedNamespace = "AliasedNamespace";
            var aliasedUsing = $"using {aliasedNamespace} = {SerializedTypeSourceGeneratorAttributesNamespace};";
            SinglePropertyTestDetails.Usings.Add(aliasedUsing);
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                $"{aliasedNamespace}.{AttributeNameWithoutAttributeSuffix}",
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode)
                );
        }

        [Test]
        public Task Should_Not_Look_At_Different_Namespace_Async()
        {
            var attributeWithSuffix = $"{AttributeNameWithoutAttributeSuffix}Attribute";
            var code = @$"
using System;

namespace DifferentNamespace {{
    public class {attributeWithSuffix} : Attribute
    {{
    }}
}}

namespace TestSourceGenerator {{

    [DifferentNamespace.{AttributeNameWithoutAttributeSuffix}]
    public partial class TextBlockSerialized
    {{

    }}
}}
";
            return TestNoGenerationWithNoDiagnostics(code);
        }

        [Test]
        public Task Should_Generate_Multiple_Properties_Params_Async()
        {
            var propertyArgumentsBuilder = new StringBuilder();
            var numProperties = MultiplesPropertiesTestDetails.Properties.Count;
            for (var i = 0; i < numProperties; i++)
            {
                var isLast = i == numProperties - 1;
                propertyArgumentsBuilder.Append(MultiplesPropertiesTestDetails.Properties[i].PropertyArgument);
                if (!isLast)
                {
                    propertyArgumentsBuilder.Append(",");
                }

            }
            return PropertiesTest(
                MultiplesPropertiesTestDetails.Usings,
                MultiplesPropertiesTestDetails.GeneratedUsingNamespaces,
                $"{SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}",
                propertyArgumentsBuilder.ToString(),
                MultiplesPropertiesTestDetails.Properties,
                GetAdditionalCode(MultiplesPropertiesTestDetails.AdditionalCode)
            );
            
        }

        [Test]
        public Task Should_Generate_Multiple_Properties_String_Array_Async()
        {
            var propertyArgumentsBuilder = new StringBuilder(
                $"{MultiplesPropertiesTestDetails.Properties[0].PropertyArgument}, new string[]{{");
            var numProperties = MultiplesPropertiesTestDetails.Properties.Count;
            for (var i = 1; i < numProperties; i++)
            {
                var isLast = i == numProperties - 1;
                propertyArgumentsBuilder.Append(MultiplesPropertiesTestDetails.Properties[i].PropertyArgument);
                if (!isLast)
                {
                    propertyArgumentsBuilder.Append(",");
                }

            }
            propertyArgumentsBuilder.Append("}");

            return PropertiesTest(
                MultiplesPropertiesTestDetails.Usings,
                MultiplesPropertiesTestDetails.GeneratedUsingNamespaces,
                $"{SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}",
                propertyArgumentsBuilder.ToString(),
                MultiplesPropertiesTestDetails.Properties,
                GetAdditionalCode(MultiplesPropertiesTestDetails.AdditionalCode)
            );
        }

        [Test] 
        public Task Should_Generate_Without_Namespace()
        {
            var fullyQualifiedAttributeName = $"{SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}Attribute";
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                fullyQualifiedAttributeName,
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode),
                false
                );
        }

        [Test]
        public Task Should_Generate_Partial_Struct_When_Attribute_Applied_To_Struct()
        {
            var fullyQualifiedAttributeName = $"{SerializedTypeSourceGeneratorAttributesNamespace}.{AttributeNameWithoutAttributeSuffix}Attribute";
            return SinglePropertyTest(
                SinglePropertyTestDetails.Usings,
                SinglePropertyTestDetails.GeneratedUsingNamespaces,
                fullyQualifiedAttributeName,
                SinglePropertyTestDetails.Properties[0],
                GetAdditionalCode(SinglePropertyTestDetails.AdditionalCode),
                true,
                false
                );
        }
    }
}