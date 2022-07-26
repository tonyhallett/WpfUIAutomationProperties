using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusUserControlAutomationPeer : UserControlAutomationPeer
    {
        private readonly UserControl owner;
        private readonly Func<UserControl, string, string> getItemStatus;

        public ItemStatusUserControlAutomationPeer(UserControl owner, Func<UserControl,string,string> getItemStatus) : base(owner)
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
