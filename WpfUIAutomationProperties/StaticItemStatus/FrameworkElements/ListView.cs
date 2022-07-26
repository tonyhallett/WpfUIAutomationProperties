using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusListView : ListView
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusListViewAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeListView);
        }
    }
}
