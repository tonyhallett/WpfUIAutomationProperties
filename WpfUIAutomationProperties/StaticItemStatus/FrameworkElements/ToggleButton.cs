using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusToggleButton : ToggleButton
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusToggleButtonAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeToggleButton);
        }
    }
}
