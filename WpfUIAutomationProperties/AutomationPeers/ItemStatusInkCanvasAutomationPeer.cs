using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusInkCanvasAutomationPeer : InkCanvasAutomationPeer
    {
        private readonly InkCanvas owner;
        private readonly Func<InkCanvas, string, string> getItemStatus;

        public ItemStatusInkCanvasAutomationPeer(InkCanvas owner, Func<InkCanvas,string,string> getItemStatus) : base(owner)
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
