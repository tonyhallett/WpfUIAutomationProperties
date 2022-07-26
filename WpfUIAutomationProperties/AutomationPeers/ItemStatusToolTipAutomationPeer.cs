using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusToolTipAutomationPeer : ToolTipAutomationPeer
    {
        private readonly ToolTip owner;
        private readonly Func<ToolTip, string, string> getItemStatus;

        public ItemStatusToolTipAutomationPeer(ToolTip owner, Func<ToolTip,string,string> getItemStatus) : base(owner)
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
