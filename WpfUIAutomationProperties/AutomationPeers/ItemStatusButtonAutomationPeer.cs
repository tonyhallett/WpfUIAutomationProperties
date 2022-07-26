using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusButtonAutomationPeer : ButtonAutomationPeer
    {
        private readonly Button owner;
        private readonly Func<Button, string, string> getItemStatus;

        public ItemStatusButtonAutomationPeer(Button owner, Func<Button,string,string> getItemStatus) : base(owner)
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
