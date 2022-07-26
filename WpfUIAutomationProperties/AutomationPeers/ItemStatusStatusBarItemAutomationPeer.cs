using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusStatusBarItemAutomationPeer : StatusBarItemAutomationPeer
    {
        private readonly StatusBarItem owner;
        private readonly Func<StatusBarItem, string, string> getItemStatus;

        public ItemStatusStatusBarItemAutomationPeer(StatusBarItem owner, Func<StatusBarItem,string,string> getItemStatus) : base(owner)
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
