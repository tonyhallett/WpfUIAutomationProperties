using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusToolBar : ToolBar
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusToolBarAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeToolBar);
        }
    }
}
