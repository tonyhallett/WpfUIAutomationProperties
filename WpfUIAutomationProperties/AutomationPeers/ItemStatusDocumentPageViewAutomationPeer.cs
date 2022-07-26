using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusDocumentPageViewAutomationPeer : DocumentPageViewAutomationPeer
    {
        private readonly DocumentPageView owner;
        private readonly Func<DocumentPageView, string, string> getItemStatus;

        public ItemStatusDocumentPageViewAutomationPeer(DocumentPageView owner, Func<DocumentPageView,string,string> getItemStatus) : base(owner)
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
