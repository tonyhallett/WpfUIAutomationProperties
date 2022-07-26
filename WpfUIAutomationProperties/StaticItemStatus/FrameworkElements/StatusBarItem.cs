using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusStatusBarItem : StatusBarItem
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusStatusBarItemAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeStatusBarItem);
        }
    }
}
