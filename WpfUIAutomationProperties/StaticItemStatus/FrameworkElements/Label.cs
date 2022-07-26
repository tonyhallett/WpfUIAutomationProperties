using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusLabel : Label
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusLabelAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeLabel);
        }
    }
}
