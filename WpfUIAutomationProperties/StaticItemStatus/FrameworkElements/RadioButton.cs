using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusRadioButton : RadioButton
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusRadioButtonAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeRadioButton);
        }
    }
}
