using ItemStatusAutomationPeerGeneration;
using System.Collections.Generic;
using System.Linq;

namespace CodeGen
{
    internal static class StaticItemStatus
    {
        private const string UseItemStatusDependencyProperty = "UseItemStatusDependencyProperty";
        public static string GetSerializeItemStatusForTypeMethodName(string typeName)
        {
            return $"SerializeItemStatusForType{typeName}";
        }
        public static string GetCode(List<string> typeNames, List<string> namespaces, Type itemStatusSerializerType)
        {
            namespaces.Add("System");
            namespaces.Add("WpfUIAutomationProperties.Serialization");
            namespaces.Add(itemStatusSerializerType.Namespace!);

            var elementParameterName = "element";
            var itemStatusParameterName = "itemStatus";

            string GetMembersForType(string typeName)
            {
                string invokeGetItemStatus(string funcField) => $"{funcField}({elementParameterName}, {itemStatusParameterName})";

                var forTypeName = $"For{typeName}";
                var serializeForTypeName = $"Serialize{forTypeName}";


                return $@"
        internal static string {GetSerializeItemStatusForTypeMethodName(typeName)}({typeName} {elementParameterName}, string {itemStatusParameterName}){{
            if ({UseItemStatusDependencyProperty}){{
                return itemStatus;
            }}

            if({serializeForTypeName} != null){{
               return ItemStatusSerializer.Serialize({invokeGetItemStatus(serializeForTypeName)});
            }}
            else{{
                return {invokeGetItemStatus(forTypeName)};
            }}
        }}
        public static Func<{typeName},string,object> {serializeForTypeName} {{get;set;}}
        public static Func<{typeName},string,string> {forTypeName} {{get;set;}}

";
            }

            return $@"{CodeGenStringHelper.GetUsings(namespaces)}

namespace {Namespaces.StaticItemStatus}
{{
    public static class ItemStatus
    {{
        public static bool {UseItemStatusDependencyProperty} {{get;set;}}
        public static IItemStatusSerializer ItemStatusSerializer {{ get; set; }} = new {itemStatusSerializerType.Name}();
        {CodeGenStringHelper.Build(typeNames.Select(typeName => GetMembersForType(typeName)))}
        
    }}
}}";
        }
    }
}
