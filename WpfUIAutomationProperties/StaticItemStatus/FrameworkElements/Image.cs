using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusImage : Image
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusImageAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeImage);
        }
    }
}
