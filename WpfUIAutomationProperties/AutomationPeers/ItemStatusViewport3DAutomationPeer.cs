using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusViewport3DAutomationPeer : Viewport3DAutomationPeer
    {
        private readonly Viewport3D owner;
        private readonly Func<Viewport3D, string, string> getItemStatus;

        public ItemStatusViewport3DAutomationPeer(Viewport3D owner, Func<Viewport3D,string,string> getItemStatus) : base(owner)
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
