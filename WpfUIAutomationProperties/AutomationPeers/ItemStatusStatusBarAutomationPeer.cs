using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusStatusBarAutomationPeer : StatusBarAutomationPeer
    {
        private readonly StatusBar owner;
        private readonly Func<StatusBar, string, string> getItemStatus;

        public ItemStatusStatusBarAutomationPeer(StatusBar owner, Func<StatusBar,string,string> getItemStatus) : base(owner)
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
