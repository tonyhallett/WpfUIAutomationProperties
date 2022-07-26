using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusTreeViewItemAutomationPeer : TreeViewItemAutomationPeer
    {
        private readonly TreeViewItem owner;
        private readonly Func<TreeViewItem, string, string> getItemStatus;

        public ItemStatusTreeViewItemAutomationPeer(TreeViewItem owner, Func<TreeViewItem,string,string> getItemStatus) : base(owner)
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
