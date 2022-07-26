using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusSliderAutomationPeer : SliderAutomationPeer
    {
        private readonly Slider owner;
        private readonly Func<Slider, string, string> getItemStatus;

        public ItemStatusSliderAutomationPeer(Slider owner, Func<Slider,string,string> getItemStatus) : base(owner)
        {
            this.owner = owner;
            this.getItemStatus = getItemStatus;
        }

        protected override string GetItemStatusCore()
        {
            return getItemStatus(this.owner, base.GetItemStatusCore());
        }
    }
}
