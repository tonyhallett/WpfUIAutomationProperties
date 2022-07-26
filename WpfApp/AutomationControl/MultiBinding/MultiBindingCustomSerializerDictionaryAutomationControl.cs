using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class MultiBindingCustomSerializerDictionaryAutomationControl : TextBlockAutomationControl
    {
        public const string AutomationIdentity = "MultiBindingCustomSerializerDictionary";
        private static JsonConvertSerializer ItemStatusSerializer = new JsonConvertSerializer();
        public MultiBindingCustomSerializerDictionaryAutomationControl() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.ItemStatusSerializer = ItemStatusSerializer;
            ItemStatus.Apply(TextBlock, CustomSerialized.DependencyProperties);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return CustomSerialized.DebugIsExpectedItemStatus(TextBlock, itemStatus, ItemStatusSerializer);
        }
    }
}
