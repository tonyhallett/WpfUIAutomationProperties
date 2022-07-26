using System.Collections.Generic;
using System.IO;

namespace ItemStatusAutomationPeerGeneration
{
    public static class ItemStatusAutomationPeersWriter
    {
        public static List<FrameworkElementAutomationPeerTypes> Write(
            string automationPeersDirectoryPath,
            string @namespace
        )
        {
            var frameworkElementAndPeerTypes = FrameworkElementAutomationPeerTypesProvider.Provide();

            var typeNames = new List<string>();
            var namespaces = new List<string> { };

            foreach (var frameworkElementAndPeerType in frameworkElementAndPeerTypes)
            {
                var frameworkElementType = frameworkElementAndPeerType!.FrameworkElementType!;
                typeNames.Add(frameworkElementType.Name);
                namespaces.Add(frameworkElementType.Namespace!);

                var peerPath = Path.Combine(automationPeersDirectoryPath, 
                    $"ItemStatus{frameworkElementType.Name}AutomationPeer.cs"
                );
                File.WriteAllText(peerPath, ItemStatusAutomationPeer.GetCode(
                    frameworkElementType, 
                    frameworkElementAndPeerType.AutomationPeerType!, 
                    @namespace)
                );
            }

            return frameworkElementAndPeerTypes;
        }
    }
}
