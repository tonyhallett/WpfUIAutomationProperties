using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusFlowDocumentPageViewerAutomationPeer : FlowDocumentPageViewerAutomationPeer
    {
        private readonly FlowDocumentPageViewer owner;
        private readonly Func<FlowDocumentPageViewer, string, string> getItemStatus;

        public ItemStatusFlowDocumentPageViewerAutomationPeer(FlowDocumentPageViewer owner, Func<FlowDocumentPageViewer,string,string> getItemStatus) : base(owner)
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
