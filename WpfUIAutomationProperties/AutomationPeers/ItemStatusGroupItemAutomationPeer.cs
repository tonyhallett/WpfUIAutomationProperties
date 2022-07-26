using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusGroupItemAutomationPeer : GroupItemAutomationPeer
    {
        private readonly GroupItem owner;
        private readonly Func<GroupItem, string, string> getItemStatus;

        public ItemStatusGroupItemAutomationPeer(GroupItem owner, Func<GroupItem,string,string> getItemStatus) : base(owner)
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
