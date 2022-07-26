using ItemStatusAutomationPeerGeneration;
using WpfUIAutomationProperties.Serialization;

namespace CodeGen
{
    internal static class StaticItemStatusControlsWriter
    {
        internal static void Write<TItemStatusSerializer>(
            string itemStatusDirectoryPath,
            string frameworkElementsDirectoryPath,

            string automationPeersDirectoryPath
        ) where TItemStatusSerializer : IItemStatusSerializer
        {
            var frameworkElementAndPeerTypes = ItemStatusAutomationPeersWriter.Write(automationPeersDirectoryPath, Namespaces.AutomationPeers);

            var typeNames = new List<string>();
            var namespaces = new List<string> { };

            foreach (var frameworkElementAndPeerType in frameworkElementAndPeerTypes)
            {
                var frameworkElementType = frameworkElementAndPeerType.FrameworkElementType;
                typeNames.Add(frameworkElementType.Name);
                namespaces.Add(frameworkElementType.Namespace!);

                var frameworkElementPath = Path.Combine(frameworkElementsDirectoryPath, frameworkElementType.Name + ".cs");
                File.WriteAllText(frameworkElementPath, StaticItemStatusFrameworkElement.GetCode(frameworkElementType.Name, frameworkElementType.Namespace!));

            }

            var itemStatusPath = Path.Combine(itemStatusDirectoryPath, "ItemStatus.cs");
            File.WriteAllText(itemStatusPath, StaticItemStatus.GetCode(typeNames, namespaces,typeof(TItemStatusSerializer)));
        }
    }
}
