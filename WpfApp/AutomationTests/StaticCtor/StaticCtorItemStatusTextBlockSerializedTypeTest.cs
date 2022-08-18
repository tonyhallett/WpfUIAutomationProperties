using System.Windows.Controls;
using WpfUIAutomationProperties.StaticConstructor;

namespace AutomationTest
{
    public class StaticCtorItemStatusTextBlockSerializedTypeTest : StaticCtorItemStatusTextBlockAutomationControlBase
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
        public StaticCtorItemStatusTextBlockSerializedTypeTest() : base(AutomationIdentity) { }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock, itemStatus, ItemStatus.Serializer);
        }

        protected override TextBlock GetItemStatusTextBlock() => new StaticCtorItemStatusTextBlockSerializedType();
    }

}
