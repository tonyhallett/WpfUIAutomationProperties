using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusMenu : Menu
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusMenuAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeMenu);
        }
    }
}
