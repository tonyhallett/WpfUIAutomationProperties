using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusCheckBoxAutomationPeer : CheckBoxAutomationPeer
    {
        private readonly CheckBox owner;
        private readonly Func<CheckBox, string, string> getItemStatus;

        public ItemStatusCheckBoxAutomationPeer(CheckBox owner, Func<CheckBox,string,string> getItemStatus) : base(owner)
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
