using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusTabItemAutomationPeer : TabItemWrapperAutomationPeer
    {
        private readonly TabItem owner;
        private readonly Func<TabItem, string, string> getItemStatus;

        public ItemStatusTabItemAutomationPeer(TabItem owner, Func<TabItem,string,string> getItemStatus) : base(owner)
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
