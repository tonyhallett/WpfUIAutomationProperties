using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusButton : Button
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusButtonAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeButton);
        }
    }
}
