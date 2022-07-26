using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusComboBoxItemAutomationPeer : ListBoxItemWrapperAutomationPeer
    {
        private readonly ComboBoxItem owner;
        private readonly Func<ComboBoxItem, string, string> getItemStatus;

        public ItemStatusComboBoxItemAutomationPeer(ComboBoxItem owner, Func<ComboBoxItem,string,string> getItemStatus) : base(owner)
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
