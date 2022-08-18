using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class MultiBindingCustomSerializerArgumentTest : TextBlockItemStatusTestBase
    {
        public const string AutomationIdentity = "MultiBindingCustomSerializerArgumentDictionary";
        private static JsonConvertSerializer ItemStatusSerializer = new JsonConvertSerializer();
        public MultiBindingCustomSerializerArgumentTest() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.ForElement(TextBlock, CustomSerialized.DependencyProperties, null, ItemStatusSerializer.Serialize);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return CustomSerialized.DebugIsExpectedItemStatus(TextBlock, itemStatus, ItemStatusSerializer);
        }
    }
}
