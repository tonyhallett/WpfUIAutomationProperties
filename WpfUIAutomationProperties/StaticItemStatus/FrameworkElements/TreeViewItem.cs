using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusTreeViewItem : TreeViewItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusTreeViewItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeTreeViewItem);
        }
    }
}
