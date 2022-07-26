using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusRepeatButtonAutomationPeer : RepeatButtonAutomationPeer
    {
        private readonly RepeatButton owner;
        private readonly Func<RepeatButton, string, string> getItemStatus;

        public ItemStatusRepeatButtonAutomationPeer(RepeatButton owner, Func<RepeatButton,string,string> getItemStatus) : base(owner)
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
