using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusToolBarAutomationPeer : ToolBarAutomationPeer
    {
        private readonly ToolBar owner;
        private readonly Func<ToolBar, string, string> getItemStatus;

        public ItemStatusToolBarAutomationPeer(ToolBar owner, Func<ToolBar,string,string> getItemStatus) : base(owner)
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
