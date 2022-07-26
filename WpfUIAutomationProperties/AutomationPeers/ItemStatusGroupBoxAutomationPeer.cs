using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusGroupBoxAutomationPeer : GroupBoxAutomationPeer
    {
        private readonly GroupBox owner;
        private readonly Func<GroupBox, string, string> getItemStatus;

        public ItemStatusGroupBoxAutomationPeer(GroupBox owner, Func<GroupBox,string,string> getItemStatus) : base(owner)
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
