using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusWindow : Window
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusWindowAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeWindow);
        }
    }
}
