using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusUserControl : UserControl
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusUserControlAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeUserControl);
        }
    }
}
