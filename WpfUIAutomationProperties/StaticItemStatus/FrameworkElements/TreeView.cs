using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusTreeView : TreeView
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusTreeViewAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeTreeView);
        }
    }
}
