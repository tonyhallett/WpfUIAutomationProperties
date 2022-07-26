using WpfUIAutomationProperties.MultiBinding;

namespace AutomationTest
{
    public class XamlAttachedSerializedTypeTextBlockAutomationControl : XamlTextBlockAutomationControl
    {
        public const string AutomationIdentity = "AttachedSerializedTypeTextBlock";
        public XamlAttachedSerializedTypeTextBlockAutomationControl() : base(AutomationIdentity)
        {

        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock,itemStatus, ItemStatus.ItemStatusSerializer);
        }
    }
}
