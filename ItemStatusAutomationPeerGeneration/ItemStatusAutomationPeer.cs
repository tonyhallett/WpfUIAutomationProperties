namespace ItemStatusAutomationPeerGeneration
{
    internal static class ItemStatusAutomationPeer
    {
        public static string GetCode(Type frameworkElementType, Type automationPeerType,string @namespace)
        {
            var frameworkElementTypeName = frameworkElementType.Name;
            var namespaces = new List<string> { "System", frameworkElementType.Namespace!, automationPeerType.Namespace! };

            return $@"{CodeGenStringHelper.GetUsings(namespaces)}

namespace {@namespace}
{{
    public class ItemStatus{frameworkElementTypeName}AutomationPeer : {automationPeerType.Name}
    {{
        private readonly {frameworkElementTypeName} owner;
        private readonly Func<{frameworkElementTypeName}, string, string> getItemStatus;

        public ItemStatus{frameworkElementTypeName}AutomationPeer({frameworkElementTypeName} owner, Func<{frameworkElementTypeName},string,string> getItemStatus) : base(owner)
        {{
            this.owner = owner;
            this.getItemStatus = getItemStatus;
        }}

        protected override string GetItemStatusCore()
        {{
            return getItemStatus(this.owner, base.GetItemStatusCore());
        }}
    }}
}}
";
        }
    }
}
