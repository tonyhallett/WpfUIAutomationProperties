using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusLabelAutomationPeer : LabelAutomationPeer
    {
        private readonly Label owner;
        private readonly Func<Label, string, string> getItemStatus;

        public ItemStatusLabelAutomationPeer(Label owner, Func<Label,string,string> getItemStatus) : base(owner)
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
