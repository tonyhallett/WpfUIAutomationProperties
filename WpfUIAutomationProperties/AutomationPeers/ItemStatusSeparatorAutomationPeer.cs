using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusSeparatorAutomationPeer : SeparatorAutomationPeer
    {
        private readonly Separator owner;
        private readonly Func<Separator, string, string> getItemStatus;

        public ItemStatusSeparatorAutomationPeer(Separator owner, Func<Separator,string,string> getItemStatus) : base(owner)
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
