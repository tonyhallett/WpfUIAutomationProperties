using System;
using System.Windows;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusWindowAutomationPeer : WindowAutomationPeer
    {
        private readonly Window owner;
        private readonly Func<Window, string, string> getItemStatus;

        public ItemStatusWindowAutomationPeer(Window owner, Func<Window,string,string> getItemStatus) : base(owner)
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
