using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusListBox : ListBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusListBoxAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeListBox);
        }
    }
}
