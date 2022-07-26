using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusComboBoxItem : ComboBoxItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusComboBoxItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeComboBoxItem);
        }
    }
}
