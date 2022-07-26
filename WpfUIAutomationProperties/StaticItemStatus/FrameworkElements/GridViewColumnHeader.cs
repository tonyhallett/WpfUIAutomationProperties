using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusGridViewColumnHeader : GridViewColumnHeader
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusGridViewColumnHeaderAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeGridViewColumnHeader);
        }
    }
}
