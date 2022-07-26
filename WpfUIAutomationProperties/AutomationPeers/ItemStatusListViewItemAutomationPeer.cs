using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusListViewItemAutomationPeer : ListBoxItemWrapperAutomationPeer
    {
        private readonly ListViewItem owner;
        private readonly Func<ListViewItem, string, string> getItemStatus;

        public ItemStatusListViewItemAutomationPeer(ListViewItem owner, Func<ListViewItem,string,string> getItemStatus) : base(owner)
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
