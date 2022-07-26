using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusThumb : Thumb
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusThumbAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeThumb);
        }
    }
}
