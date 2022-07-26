using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusListBoxAutomationPeer : ListBoxAutomationPeer
    {
        private readonly ListBox owner;
        private readonly Func<ListBox, string, string> getItemStatus;

        public ItemStatusListBoxAutomationPeer(ListBox owner, Func<ListBox,string,string> getItemStatus) : base(owner)
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
