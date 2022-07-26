using System.Windows.Controls;
using WpfUIAutomationProperties.StaticConstructor;

namespace AutomationTest
{
    public class StaticCtorItemStatusTextBlockSerializedTypeAutomationControl : StaticCtorItemStatusTextBlockAutomationControlBase
    {
        public class StaticCtorItemStatusTextBlockSerializedType : TextBlock
        {
            static StaticCtorItemStatusTextBlockSerializedType()
            {
                WpfUIAutomationProperties.StaticConstructor.ItemStatus
                    .ForType<StaticCtorItemStatusTextBlockSerializedType, SerializedTextBlock>();
            }
        }

        public const string AutomationIdentity = "StaticCtorItemStatusTextBlockSerializedType";
        public StaticCtorItemStatusTextBlockSerializedTypeAutomationControl() : base(AutomationIdentity) { }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock, itemStatus, ItemStatus.ItemStatusSerializer);
        }

        protected override TextBlock GetItemStatusTextBlock() => new StaticCtorItemStatusTextBlockSerializedType();
    }

}
