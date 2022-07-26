using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusFrame : Frame
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusFrameAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeFrame);
        }
    }
}
