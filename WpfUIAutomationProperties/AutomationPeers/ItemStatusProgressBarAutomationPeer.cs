using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusProgressBarAutomationPeer : ProgressBarAutomationPeer
    {
        private readonly ProgressBar owner;
        private readonly Func<ProgressBar, string, string> getItemStatus;

        public ItemStatusProgressBarAutomationPeer(ProgressBar owner, Func<ProgressBar,string,string> getItemStatus) : base(owner)
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
