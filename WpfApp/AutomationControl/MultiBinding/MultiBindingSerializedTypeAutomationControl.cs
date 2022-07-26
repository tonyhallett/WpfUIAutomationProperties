using WpfUIAutomationProperties.MultiBinding;
using WpfTextBlock = System.Windows.Controls.TextBlock;

namespace AutomationTest
{
    public class MultiBindingSerializedTypeAutomationControl : TextBlockAutomationControl
    {
        public const string AutomationIdentity = "MultiBindingSerializedType";
        public MultiBindingSerializedTypeAutomationControl() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.Apply<SerializedTextBlock, WpfTextBlock>(TextBlock);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock, itemStatus, ItemStatus.ItemStatusSerializer);
        }
    }
}
