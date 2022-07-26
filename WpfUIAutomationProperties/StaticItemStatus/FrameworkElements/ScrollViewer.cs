using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusScrollViewer : ScrollViewer
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusScrollViewerAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeScrollViewer);
        }
    }
}
