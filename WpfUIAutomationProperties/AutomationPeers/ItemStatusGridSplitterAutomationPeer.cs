using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusGridSplitterAutomationPeer : GridSplitterAutomationPeer
    {
        private readonly GridSplitter owner;
        private readonly Func<GridSplitter, string, string> getItemStatus;

        public ItemStatusGridSplitterAutomationPeer(GridSplitter owner, Func<GridSplitter,string,string> getItemStatus) : base(owner)
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
