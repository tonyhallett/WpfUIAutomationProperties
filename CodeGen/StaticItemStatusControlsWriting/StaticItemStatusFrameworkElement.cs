using ItemStatusAutomationPeerGeneration;

namespace CodeGen
{
    internal static class StaticItemStatusFrameworkElement
    {
        public static string GetCode(string typeName, string elementNamespace)
        {
            var usings = CodeGenStringHelper.GetUsings(new List<string>
            {
                "System.Windows.Automation.Peers",
                Namespaces.AutomationPeers,
                elementNamespace
            });
            return $@"{usings}

namespace {Namespaces.StaticItemStatus}
{{
    public class ItemStatus{typeName} : {typeName}
    {{
        protected override AutomationPeer OnCreateAutomationPeer()
        {{
            return new ItemStatus{typeName}AutomationPeer(this, ItemStatus.{StaticItemStatus.GetSerializeItemStatusForTypeMethodName(typeName)});
        }}
    }}
}}
";
        }
        
    }
}
