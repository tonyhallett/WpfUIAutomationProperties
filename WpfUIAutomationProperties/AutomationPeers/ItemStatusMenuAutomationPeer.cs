using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusMenuAutomationPeer : MenuAutomationPeer
    {
        private readonly Menu owner;
        private readonly Func<Menu, string, string> getItemStatus;

        public ItemStatusMenuAutomationPeer(Menu owner, Func<Menu,string,string> getItemStatus) : base(owner)
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
