using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusGridViewHeaderRowPresenterAutomationPeer : GridViewHeaderRowPresenterAutomationPeer
    {
        private readonly GridViewHeaderRowPresenter owner;
        private readonly Func<GridViewHeaderRowPresenter, string, string> getItemStatus;

        public ItemStatusGridViewHeaderRowPresenterAutomationPeer(GridViewHeaderRowPresenter owner, Func<GridViewHeaderRowPresenter,string,string> getItemStatus) : base(owner)
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
