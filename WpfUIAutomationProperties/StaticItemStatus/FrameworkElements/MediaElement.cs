using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusMediaElement : MediaElement
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusMediaElementAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeMediaElement);
        }
    }
}
