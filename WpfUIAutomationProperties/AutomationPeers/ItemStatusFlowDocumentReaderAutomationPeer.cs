using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusFlowDocumentReaderAutomationPeer : FlowDocumentReaderAutomationPeer
    {
        private readonly FlowDocumentReader owner;
        private readonly Func<FlowDocumentReader, string, string> getItemStatus;

        public ItemStatusFlowDocumentReaderAutomationPeer(FlowDocumentReader owner, Func<FlowDocumentReader,string,string> getItemStatus) : base(owner)
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
