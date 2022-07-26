using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusRepeatButton : RepeatButton
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusRepeatButtonAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeRepeatButton);
        }
    }
}
