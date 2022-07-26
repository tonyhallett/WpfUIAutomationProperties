using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusInkPresenterAutomationPeer : InkPresenterAutomationPeer
    {
        private readonly InkPresenter owner;
        private readonly Func<InkPresenter, string, string> getItemStatus;

        public ItemStatusInkPresenterAutomationPeer(InkPresenter owner, Func<InkPresenter,string,string> getItemStatus) : base(owner)
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
