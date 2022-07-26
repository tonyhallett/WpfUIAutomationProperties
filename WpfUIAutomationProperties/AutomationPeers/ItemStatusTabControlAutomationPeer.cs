using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusTabControlAutomationPeer : TabControlAutomationPeer
    {
        private readonly TabControl owner;
        private readonly Func<TabControl, string, string> getItemStatus;

        public ItemStatusTabControlAutomationPeer(TabControl owner, Func<TabControl,string,string> getItemStatus) : base(owner)
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
