using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusDocumentViewer : DocumentViewer
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusDocumentViewerAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeDocumentViewer);
        }
    }
}
