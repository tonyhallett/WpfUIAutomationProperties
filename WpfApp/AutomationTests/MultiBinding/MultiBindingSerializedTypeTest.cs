using WpfUIAutomationProperties.MultiBinding;
using WpfTextBlock = System.Windows.Controls.TextBlock;

namespace AutomationTest
{
    public class MultiBindingSerializedTypeTest : TextBlockItemStatusTestBase
    {
        public const string AutomationIdentity = "MultiBindingSerializedType";
        public MultiBindingSerializedTypeTest() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.ForElement<SerializedTextBlock, WpfTextBlock>(TextBlock);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock, itemStatus, ItemStatus.Serializer);
        }
    }
}
