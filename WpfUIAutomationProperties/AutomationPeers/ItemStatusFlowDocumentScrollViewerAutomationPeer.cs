using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusFlowDocumentScrollViewerAutomationPeer : FlowDocumentScrollViewerAutomationPeer
    {
        private readonly FlowDocumentScrollViewer owner;
        private readonly Func<FlowDocumentScrollViewer, string, string> getItemStatus;

        public ItemStatusFlowDocumentScrollViewerAutomationPeer(FlowDocumentScrollViewer owner, Func<FlowDocumentScrollViewer,string,string> getItemStatus) : base(owner)
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
