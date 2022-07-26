using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusContextMenu : ContextMenu
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusContextMenuAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeContextMenu);
        }
    }
}
