using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusTabItem : TabItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusTabItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeTabItem);
        }
    }
}
