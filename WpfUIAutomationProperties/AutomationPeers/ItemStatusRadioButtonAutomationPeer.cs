using System;
using System.Windows.Controls;
using System.Windows.Automation.Peers;


namespace WpfUIAutomationProperties.AutomationPeers
{
    public class ItemStatusRadioButtonAutomationPeer : RadioButtonAutomationPeer
    {
        private readonly RadioButton owner;
        private readonly Func<RadioButton, string, string> getItemStatus;

        public ItemStatusRadioButtonAutomationPeer(RadioButton owner, Func<RadioButton,string,string> getItemStatus) : base(owner)
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
