using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusComboBox : ComboBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusComboBoxAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeComboBox);
        }
    }
}
