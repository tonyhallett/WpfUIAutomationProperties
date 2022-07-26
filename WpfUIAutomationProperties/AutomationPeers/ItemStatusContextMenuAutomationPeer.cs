using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusContextMenuAutomationPeer : ContextMenuAutomationPeer
    {
        private readonly ContextMenu owner;
        private readonly Func<ContextMenu, string, string> getItemStatus;

        public ItemStatusContextMenuAutomationPeer(ContextMenu owner, Func<ContextMenu,string,string> getItemStatus) : base(owner)
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
