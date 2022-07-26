using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusGridViewColumnHeaderAutomationPeer : GridViewColumnHeaderAutomationPeer
    {
        private readonly GridViewColumnHeader owner;
        private readonly Func<GridViewColumnHeader, string, string> getItemStatus;

        public ItemStatusGridViewColumnHeaderAutomationPeer(GridViewColumnHeader owner, Func<GridViewColumnHeader,string,string> getItemStatus) : base(owner)
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
