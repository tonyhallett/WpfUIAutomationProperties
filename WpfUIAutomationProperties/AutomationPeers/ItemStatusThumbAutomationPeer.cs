using System;
using System.Windows.Controls.Primitives;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusThumbAutomationPeer : ThumbAutomationPeer
    {
        private readonly Thumb owner;
        private readonly Func<Thumb, string, string> getItemStatus;

        public ItemStatusThumbAutomationPeer(Thumb owner, Func<Thumb,string,string> getItemStatus) : base(owner)
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
