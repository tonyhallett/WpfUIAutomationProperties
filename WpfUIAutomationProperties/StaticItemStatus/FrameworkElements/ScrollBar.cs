using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusScrollBar : ScrollBar
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusScrollBarAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeScrollBar);
        }
    }
}
