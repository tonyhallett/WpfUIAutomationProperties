using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusMenuItemAutomationPeer : MenuItemAutomationPeer
    {
        private readonly MenuItem owner;
        private readonly Func<MenuItem, string, string> getItemStatus;

        public ItemStatusMenuItemAutomationPeer(MenuItem owner, Func<MenuItem,string,string> getItemStatus) : base(owner)
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
