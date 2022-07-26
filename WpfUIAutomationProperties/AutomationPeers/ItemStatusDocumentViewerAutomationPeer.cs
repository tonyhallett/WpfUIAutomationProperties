using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusDocumentViewerAutomationPeer : DocumentViewerAutomationPeer
    {
        private readonly DocumentViewer owner;
        private readonly Func<DocumentViewer, string, string> getItemStatus;

        public ItemStatusDocumentViewerAutomationPeer(DocumentViewer owner, Func<DocumentViewer,string,string> getItemStatus) : base(owner)
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
