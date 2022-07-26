using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusSeparator : Separator
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusSeparatorAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeSeparator);
        }
    }
}
