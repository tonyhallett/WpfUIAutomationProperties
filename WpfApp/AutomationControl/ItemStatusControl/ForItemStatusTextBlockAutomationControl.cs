using System.Windows.Controls;
using AutomationItemStatus = WpfUIAutomationProperties.StaticItemStatus.ItemStatus;
using WpfUIAutomationProperties.StaticItemStatus;

namespace AutomationTest
{
    public class ForItemStatusTextBlockAutomationControl : ItemStatusControlAutomationControlBase
    {
        public const string AutomationIdentity = "ForItemStatusTextBlock";
        public ForItemStatusTextBlockAutomationControl() : base(AutomationIdentity)
        {
            TextBlock = new ItemStatusTextBlock();
        }

        public override void DebugSetup()
        {
            AutomationItemStatus.ForTextBlock = (textBlock, itemStatus) =>
            {
                return $"{itemStatus} - {textBlock.FontFamily.Source}";
            };
        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var defaultFontFamilySource = TextBlock.FontFamily.Source;
            var expected = $"{ItemStatusPropertyValue} - {defaultFontFamilySource}";
            return itemStatus == expected;
        }
    }
}
