using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusDocumentPageView : DocumentPageView
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusDocumentPageViewAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeDocumentPageView);
        }
    }
}
