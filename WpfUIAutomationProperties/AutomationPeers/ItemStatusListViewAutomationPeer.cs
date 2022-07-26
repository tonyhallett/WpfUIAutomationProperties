using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusListViewAutomationPeer : ListViewAutomationPeer
    {
        private readonly ListView owner;
        private readonly Func<ListView, string, string> getItemStatus;

        public ItemStatusListViewAutomationPeer(ListView owner, Func<ListView,string,string> getItemStatus) : base(owner)
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
