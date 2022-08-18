using System.Collections.Generic;
using System.Windows.Controls;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class StaticCtorItemStatusTextBlockCustomSerializerForCallTest : StaticCtorItemStatusTextBlockAutomationControlBase
    {
        private static JsonConvertSerializer ItemStatusSerializer = new JsonConvertSerializer();
        public class StaticCtorItemStatusTextBlockCustomSerializerForCall : TextBlock
        {
            
            static StaticCtorItemStatusTextBlockCustomSerializerForCall()
            {
                WpfUIAutomationProperties.StaticConstructor.ItemStatus
                    .ForType<StaticCtorItemStatusTextBlockCustomSerializerForCall>(
                        CustomSerialized.DependencyProperties
                        , null, ItemStatusSerializer.Serialize
                    );
            }
        }

        public const string AutomationIdentity = "StaticCtorItemStatusTextBlockCustomSerializerForCall";
        public StaticCtorItemStatusTextBlockCustomSerializerForCallTest() : base(AutomationIdentity) { }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return CustomSerialized.DebugIsExpectedItemStatus(TextBlock, itemStatus, ItemStatusSerializer);
        }

        protected override TextBlock GetItemStatusTextBlock() => new StaticCtorItemStatusTextBlockCustomSerializerForCall();

    }

}
