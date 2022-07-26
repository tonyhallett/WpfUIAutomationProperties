using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls.Primitives;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusStatusBar : StatusBar
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusStatusBarAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeStatusBar);
        }
    }
}
