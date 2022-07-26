using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusExpander : Expander
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusExpanderAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeExpander);
        }
    }
}
