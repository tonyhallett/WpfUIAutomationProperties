using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusMediaElementAutomationPeer : MediaElementAutomationPeer
    {
        private readonly MediaElement owner;
        private readonly Func<MediaElement, string, string> getItemStatus;

        public ItemStatusMediaElementAutomationPeer(MediaElement owner, Func<MediaElement,string,string> getItemStatus) : base(owner)
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
