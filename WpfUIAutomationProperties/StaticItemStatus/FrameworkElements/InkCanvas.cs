using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusInkCanvas : InkCanvas
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusInkCanvasAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeInkCanvas);
        }
    }
}
