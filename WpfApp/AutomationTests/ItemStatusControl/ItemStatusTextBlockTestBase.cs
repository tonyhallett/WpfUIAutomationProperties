using AutomationItemStatus = WpfUIAutomationProperties.StaticItemStatus.ItemStatus;
using System.Windows.Automation;
using WpfUIAutomationProperties.StaticItemStatus;

namespace AutomationTest
{
    public abstract class ItemStatusTextBlockTestBase : TextBlockItemStatusTestBase
    {
        protected const string ItemStatusPropertyValue = "From AutomationProperties.ItemStatusProperty";
        public ItemStatusTextBlockTestBase(string automationId) : base(automationId) {
            TextBlock = new ItemStatusTextBlock();
        }

        protected override void AdditionalSetup()
        {
            AutomationItemStatus.UseItemStatusDependencyProperty = !ApplyWhenDebugOnly.Apply;
            AutomationProperties.SetItemStatus(TextBlock, ItemStatusPropertyValue);
        }

        protected override bool ReleaseIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return itemStatus == ItemStatusPropertyValue;
        }
    }
}
