using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusTreeViewAutomationPeer : TreeViewAutomationPeer
    {
        private readonly TreeView owner;
        private readonly Func<TreeView, string, string> getItemStatus;

        public ItemStatusTreeViewAutomationPeer(TreeView owner, Func<TreeView,string,string> getItemStatus) : base(owner)
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
