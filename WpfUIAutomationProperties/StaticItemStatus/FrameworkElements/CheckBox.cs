using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusCheckBox : CheckBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusCheckBoxAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeCheckBox);
        }
    }
}
