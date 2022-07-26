﻿using System.Windows.Controls;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class StaticCtorItemStatusTextBlockCustomSerializerTest : StaticCtorItemStatusTextBlockAutomationControlBase
    {
        private static JsonConvertSerializer ItemStatusSerializer = new JsonConvertSerializer();
        public class StaticCtorItemStatusTextBlockCustomSerializer : TextBlock
        {
            
            static StaticCtorItemStatusTextBlockCustomSerializer()
            {
                WpfUIAutomationProperties.StaticConstructor.ItemStatus.Serializer = ItemStatusSerializer;
                WpfUIAutomationProperties.StaticConstructor.ItemStatus
                    .ForType<StaticCtorItemStatusTextBlockCustomSerializer>(
                        CustomSerialized.DependencyProperties
                    );
            }
        }

        public const string AutomationIdentity = "StaticCtorItemStatusTextBlockCustomSerializer";
        public StaticCtorItemStatusTextBlockCustomSerializerTest() : base(AutomationIdentity) { }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return CustomSerialized.DebugIsExpectedItemStatus(TextBlock, itemStatus, ItemStatusSerializer);
        }

        protected override TextBlock GetItemStatusTextBlock() => new StaticCtorItemStatusTextBlockCustomSerializer();

    }

}
