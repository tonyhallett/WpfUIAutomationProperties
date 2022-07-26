using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusSlider : Slider
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusSliderAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeSlider);
        }
    }
}
