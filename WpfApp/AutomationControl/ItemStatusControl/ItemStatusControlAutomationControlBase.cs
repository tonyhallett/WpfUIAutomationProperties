using AutomationItemStatus = WpfUIAutomationProperties.StaticItemStatus.ItemStatus;
using System.Windows.Automation;

namespace AutomationTest
{
    public abstract class ItemStatusControlAutomationControlBase : TextBlockAutomationControl
    {
        protected const string ItemStatusPropertyValue = "From AutomationProperties.ItemStatusProperty";
        public ItemStatusControlAutomationControlBase(string automationId) : base(automationId) { }

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
