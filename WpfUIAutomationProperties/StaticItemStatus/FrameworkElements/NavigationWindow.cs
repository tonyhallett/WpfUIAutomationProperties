using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Navigation;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusNavigationWindow : NavigationWindow
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusNavigationWindowAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeNavigationWindow);
        }
    }
}
