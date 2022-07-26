using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusTextBlock : TextBlock
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusTextBlockAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeTextBlock);
        }
    }
}
