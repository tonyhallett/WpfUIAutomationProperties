using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusViewport3D : Viewport3D
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusViewport3DAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeViewport3D);
        }
    }
}
