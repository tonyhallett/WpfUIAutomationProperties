using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusExpanderAutomationPeer : ExpanderAutomationPeer
    {
        private readonly Expander owner;
        private readonly Func<Expander, string, string> getItemStatus;

        public ItemStatusExpanderAutomationPeer(Expander owner, Func<Expander,string,string> getItemStatus) : base(owner)
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
