using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusMenuItem : MenuItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusMenuItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeMenuItem);
        }
    }
}
