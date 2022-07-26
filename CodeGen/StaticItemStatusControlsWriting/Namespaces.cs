namespace CodeGen
{
    internal class Namespaces
    {
        private const string root = "WpfUIAutomationProperties";
        public static string AutomationPeers => $"{root}.AutomationPeers";
        public static string StaticItemStatus = $"{root}.StaticItemStatus";
        public static string FrameworkElementTypes = $"{root}.Serialization.FrameworkElementTypes";
    }
}
