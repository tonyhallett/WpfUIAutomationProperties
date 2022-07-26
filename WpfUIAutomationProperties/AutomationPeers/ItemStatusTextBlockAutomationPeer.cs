using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusTextBlockAutomationPeer : TextBlockAutomationPeer
    {
        private readonly TextBlock owner;
        private readonly Func<TextBlock, string, string> getItemStatus;

        public ItemStatusTextBlockAutomationPeer(TextBlock owner, Func<TextBlock,string,string> getItemStatus) : base(owner)
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
