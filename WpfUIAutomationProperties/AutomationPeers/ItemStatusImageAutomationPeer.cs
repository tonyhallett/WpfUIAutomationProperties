using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusImageAutomationPeer : ImageAutomationPeer
    {
        private readonly Image owner;
        private readonly Func<Image, string, string> getItemStatus;

        public ItemStatusImageAutomationPeer(Image owner, Func<Image,string,string> getItemStatus) : base(owner)
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
