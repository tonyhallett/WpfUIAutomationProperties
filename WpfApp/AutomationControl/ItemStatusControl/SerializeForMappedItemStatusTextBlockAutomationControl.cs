using System.Windows.Controls;
using AutomationItemStatus = WpfUIAutomationProperties.StaticItemStatus.ItemStatus;
using WpfUIAutomationProperties.StaticItemStatus;

namespace AutomationTest
{
    public class SerializeForMappedItemStatusTextBlockAutomationControl : ItemStatusControlAutomationControlBase
    {
        public const string AutomationIdentity = "SerializeForMappedItemStatusTextBlock";
        public SerializeForMappedItemStatusTextBlockAutomationControl() : base(AutomationIdentity) {
            TextBlock = new ItemStatusTextBlock();
        }
        
        public override void DebugSetup()
        {
            var textBlockMapper = new FrameworkElementMapper<SerializedTextBlock, TextBlock>();
            AutomationItemStatus.SerializeForTextBlock = (textBlock, itemStatus) =>
            {
                return textBlockMapper.Map(textBlock);
            };
        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return SerializedTypeDebugExpectedStatus.IsExpected(TextBlock, itemStatus, ItemStatus.ItemStatusSerializer);
        }
    }
}
