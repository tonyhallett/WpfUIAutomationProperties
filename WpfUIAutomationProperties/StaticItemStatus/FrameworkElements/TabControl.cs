using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusTabControl : TabControl
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusTabControlAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeTabControl);
        }
    }
}
