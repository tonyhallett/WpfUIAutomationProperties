using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusFlowDocumentPageViewer : FlowDocumentPageViewer
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusFlowDocumentPageViewerAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeFlowDocumentPageViewer);
        }
    }
}
