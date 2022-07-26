using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusTextBox : TextBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusTextBoxAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeTextBox);
        }
    }
}
