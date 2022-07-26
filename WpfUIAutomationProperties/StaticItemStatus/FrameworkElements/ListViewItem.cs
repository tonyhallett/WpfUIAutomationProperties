using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusListViewItem : ListViewItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusListViewItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeListViewItem);
        }
    }
}
