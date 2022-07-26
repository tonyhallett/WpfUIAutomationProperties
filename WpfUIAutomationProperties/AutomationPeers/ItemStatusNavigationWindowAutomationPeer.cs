using System;
using System.Windows.Navigation;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusNavigationWindowAutomationPeer : NavigationWindowAutomationPeer
    {
        private readonly NavigationWindow owner;
        private readonly Func<NavigationWindow, string, string> getItemStatus;

        public ItemStatusNavigationWindowAutomationPeer(NavigationWindow owner, Func<NavigationWindow,string,string> getItemStatus) : base(owner)
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
