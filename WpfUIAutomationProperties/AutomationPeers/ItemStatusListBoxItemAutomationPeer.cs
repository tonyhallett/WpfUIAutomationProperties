using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusListBoxItemAutomationPeer : ListBoxItemWrapperAutomationPeer
    {
        private readonly ListBoxItem owner;
        private readonly Func<ListBoxItem, string, string> getItemStatus;

        public ItemStatusListBoxItemAutomationPeer(ListBoxItem owner, Func<ListBoxItem,string,string> getItemStatus) : base(owner)
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
