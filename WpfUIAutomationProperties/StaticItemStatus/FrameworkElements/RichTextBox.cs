using System.Windows.Automation.Peers;
using WpfUIAutomationProperties.AutomationPeers;
using System.Windows.Controls;


namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class ItemStatusRichTextBox : RichTextBox
    {
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ItemStatusRichTextBoxAutomationPeer(this, ItemStatus.SerializeItemStatusForTypeRichTextBox);
        }
    }
}
