using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusProgressBar : ProgressBar
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusProgressBarAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeProgressBar);
        }
    }
}
