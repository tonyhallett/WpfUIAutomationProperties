using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusToggleButtonAutomationPeer : ToggleButtonAutomationPeer
    {
        private readonly ToggleButton owner;
        private readonly Func<ToggleButton, string, string> getItemStatus;

        public ItemStatusToggleButtonAutomationPeer(ToggleButton owner, Func<ToggleButton,string,string> getItemStatus) : base(owner)
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
