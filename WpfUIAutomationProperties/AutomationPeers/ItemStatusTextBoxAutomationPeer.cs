using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusTextBoxAutomationPeer : TextBoxAutomationPeer
    {
        private readonly TextBox owner;
        private readonly Func<TextBox, string, string> getItemStatus;

        public ItemStatusTextBoxAutomationPeer(TextBox owner, Func<TextBox,string,string> getItemStatus) : base(owner)
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
