using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusScrollBarAutomationPeer : ScrollBarAutomationPeer
    {
        private readonly ScrollBar owner;
        private readonly Func<ScrollBar, string, string> getItemStatus;

        public ItemStatusScrollBarAutomationPeer(ScrollBar owner, Func<ScrollBar,string,string> getItemStatus) : base(owner)
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
