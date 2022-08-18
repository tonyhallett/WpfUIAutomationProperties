using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class MultiBindingCustomSerializerTest : TextBlockItemStatusTestBase
    {
        public const string AutomationIdentity = "MultiBindingCustomSerializerDictionary";
        private static JsonConvertSerializer ItemStatusSerializer = new JsonConvertSerializer();
        public MultiBindingCustomSerializerTest() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.Serializer = ItemStatusSerializer;
            ItemStatus.ForElement(TextBlock, CustomSerialized.DependencyProperties);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return CustomSerialized.DebugIsExpectedItemStatus(TextBlock, itemStatus, ItemStatusSerializer);
        }
    }
}
