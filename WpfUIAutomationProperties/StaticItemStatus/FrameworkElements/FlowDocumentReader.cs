using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusFlowDocumentReader : FlowDocumentReader
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusFlowDocumentReaderAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeFlowDocumentReader);
        }
    }
}
