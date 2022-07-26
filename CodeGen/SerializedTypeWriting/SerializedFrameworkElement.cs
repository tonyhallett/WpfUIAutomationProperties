using ItemStatusAutomationPeerGeneration;
using System.Text;
using System.Windows;

namespace CodeGen
{
    public static class SerializedFrameworkElement {
        private class DpWithComment
        {

            public string Comment { get; set; }

            public DpWithComment(string comment, DependencyProperty dependencyProperty)
            {
                Comment = comment;
                DependencyProperty = dependencyProperty;
            }

            public DependencyProperty DependencyProperty { get; set; }
        }
        public static string GetCode(TypeDps typeDps,string @namespace)
        {
            var dpComments = GetIncludedDpsWithComments(typeDps.DependencyProperties);
            var classCode = $@"
{GetUsingStatements(typeDps.DependencyProperties)}
namespace {@namespace} {{

    public class {typeDps.Type.Name}Serialized {{
{GetCodeProperties(dpComments)}
    }}
}}
";
            return classCode;
        }

        private static string GetUsingStatements(List<DependencyProperty> dps)
        {
            var distinctNamespaces = dps.SelectMany(dp =>
            {
                var type = dp.PropertyType;
                IEnumerable<Type> types = new Type[] { type };
                if (type.IsGenericType)
                {
                    types = types.Concat(type.GetGenericArguments());
                }
                return types;
            }).Distinct().Select(t => t.Namespace!).Distinct();
            return CodeGenStringHelper.GetUsings(distinctNamespaces);
        }

        private static List<DpWithComment> GetIncludedDpsWithComments(IEnumerable<DependencyProperty> dps, bool includeConditional = true)
        {
            var includedDps = dps.Where(dp => !SerializationRestrictions.IgnoredDependencyProperties.Contains(dp));
            return includedDps.Select(dp =>
            {
                var excluded = SerializationRestrictions.IgnoreTypes.Any(et => et.Type == dp.PropertyType);
                var comment = "";

                if (!excluded)
                {
                    var explainedConditionalType = SerializationRestrictions.ConditionallySafeTypes.FirstOrDefault(et => et.Type == dp.PropertyType);
                    if (explainedConditionalType != null)
                    {
                        if (includeConditional)
                        {
                            comment = explainedConditionalType.Explanation;
                        }
                        else
                        {
                            excluded = true;
                        }
                    }
                }

                return new
                {
                    Excluded = excluded,
                    Comment = comment,
                    Dp = dp
                };
            }).Where(a => !a.Excluded).Select(a => new DpWithComment(a.Comment!, a.Dp)).ToList();
        }

        private static string GetCodeProperties(List<DpWithComment> dpsWithComment)
        {
            var codePropertyDetails = dpsWithComment.Select(dpWithComment => new { 
                PropertyType = TypeName.Get(dpWithComment.DependencyProperty.PropertyType),
                dpWithComment.DependencyProperty.Name,
                dpWithComment.Comment 
                }
            ).OrderBy(a => a.PropertyType);

            var stringBuilder = new StringBuilder();
            foreach (var codePropertyDetail in codePropertyDetails)
            {
                stringBuilder.Append(GetCodeProperty(codePropertyDetail.PropertyType, codePropertyDetail.Name, codePropertyDetail.Comment!));
            }
            return stringBuilder.ToString();
        }

        private static string GetCodeProperty(string type, string name, string comment)
        {
            if(comment != "")
            {
                comment = "        // " + comment + Environment.NewLine;
            }
            return $@"
{comment}        public {type} {name} {{get;set;}}
";
        }

    }
}
