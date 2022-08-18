using WpfUIAutomationProperties.MultiBinding;

namespace AutomationTest
{
    public class XamlAttachedSerializedTypeTest : XamlTextBlockTestBase
    {
        public const string AutomationIdentity = "AttachedSerializedTypeTextBlock";
        public XamlAttachedSerializedTypeTest() : base(AutomationIdentity)
        {

        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock,itemStatus, ItemStatus.Serializer);
        }
    }
}
