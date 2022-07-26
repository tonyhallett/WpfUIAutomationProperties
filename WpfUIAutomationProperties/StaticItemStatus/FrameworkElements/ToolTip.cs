using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusToolTip : ToolTip
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusToolTipAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeToolTip);
        }
    }
}
