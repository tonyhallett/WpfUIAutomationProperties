using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusFrameAutomationPeer : FrameAutomationPeer
    {
        private readonly Frame owner;
        private readonly Func<Frame, string, string> getItemStatus;

        public ItemStatusFrameAutomationPeer(Frame owner, Func<Frame,string,string> getItemStatus) : base(owner)
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
