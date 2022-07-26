using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusGroupBox : GroupBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusGroupBoxAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeGroupBox);
        }
    }
}
