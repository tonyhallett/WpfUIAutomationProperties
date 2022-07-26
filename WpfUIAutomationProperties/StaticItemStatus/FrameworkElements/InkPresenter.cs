using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusInkPresenter : InkPresenter
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusInkPresenterAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeInkPresenter);
        }
    }
}
