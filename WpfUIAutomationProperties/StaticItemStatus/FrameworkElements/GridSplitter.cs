using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusGridSplitter : GridSplitter
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusGridSplitterAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeGridSplitter);
        }
    }
}
