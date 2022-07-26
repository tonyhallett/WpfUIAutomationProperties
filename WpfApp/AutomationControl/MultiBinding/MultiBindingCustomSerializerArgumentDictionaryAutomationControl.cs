using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class MultiBindingCustomSerializerArgumentDictionaryAutomationControl : TextBlockAutomationControl
    {
        public const string AutomationIdentity = "MultiBindingCustomSerializerArgumentDictionary";
        private static JsonConvertSerializer ItemStatusSerializer = new JsonConvertSerializer();
        public MultiBindingCustomSerializerArgumentDictionaryAutomationControl() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.Apply(TextBlock, CustomSerialized.DependencyProperties, null, ItemStatusSerializer.Serialize);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return CustomSerialized.DebugIsExpectedItemStatus(TextBlock, itemStatus, ItemStatusSerializer);
        }
    }
}
