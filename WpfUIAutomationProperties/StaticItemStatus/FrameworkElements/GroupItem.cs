using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusGroupItem : GroupItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusGroupItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeGroupItem);
        }
    }
}
