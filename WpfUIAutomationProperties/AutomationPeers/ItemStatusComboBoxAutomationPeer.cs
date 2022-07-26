using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusComboBoxAutomationPeer : ComboBoxAutomationPeer
    {
        private readonly ComboBox owner;
        private readonly Func<ComboBox, string, string> getItemStatus;

        public ItemStatusComboBoxAutomationPeer(ComboBox owner, Func<ComboBox,string,string> getItemStatus) : base(owner)
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
