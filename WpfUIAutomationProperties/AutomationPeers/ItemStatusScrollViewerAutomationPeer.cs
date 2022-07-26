using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusScrollViewerAutomationPeer : ScrollViewerAutomationPeer
    {
        private readonly ScrollViewer owner;
        private readonly Func<ScrollViewer, string, string> getItemStatus;

        public ItemStatusScrollViewerAutomationPeer(ScrollViewer owner, Func<ScrollViewer,string,string> getItemStatus) : base(owner)
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
