using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusRichTextBoxAutomationPeer : RichTextBoxAutomationPeer
    {
        private readonly RichTextBox owner;
        private readonly Func<RichTextBox, string, string> getItemStatus;

        public ItemStatusRichTextBoxAutomationPeer(RichTextBox owner, Func<RichTextBox,string,string> getItemStatus) : base(owner)
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
