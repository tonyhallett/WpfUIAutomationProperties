using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusListBoxItem : ListBoxItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusListBoxItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeListBoxItem);
        }
    }
}
