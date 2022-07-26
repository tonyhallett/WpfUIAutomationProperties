using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusGridViewHeaderRowPresenter : GridViewHeaderRowPresenter
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusGridViewHeaderRowPresenterAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeGridViewHeaderRowPresenter);
        }
    }
}
