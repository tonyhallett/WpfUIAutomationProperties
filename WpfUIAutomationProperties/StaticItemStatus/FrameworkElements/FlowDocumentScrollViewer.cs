using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusFlowDocumentScrollViewer : FlowDocumentScrollViewer
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusFlowDocumentScrollViewerAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeFlowDocumentScrollViewer);
        }
    }
}
