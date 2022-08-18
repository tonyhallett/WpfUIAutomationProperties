using System.Windows.Controls;
using AutomationItemStatus = WpfUIAutomationProperties.StaticItemStatus.ItemStatus;
using WpfUIAutomationProperties.StaticItemStatus;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class SerializeForCustomItemStatusTextBlockTest : ItemStatusTextBlockTestBase
    {
        public class CustomItemStatusSerializer : IItemStatusSerializer
        {
            public T Deserialize<T>(string serialized)
            {
                throw new System.NotImplementedException();
            }

            public string Serialize(object itemStatus)
            {
                return (itemStatus as TextBlockItemStatus).FontFamilySource;
            }
        }

        public class TextBlockItemStatus
        {
            public string FontFamilySource { get; }
            public TextBlockItemStatus(TextBlock textBlock)
            {
                FontFamilySource = textBlock.FontFamily.Source;
            }
        }

        public const string AutomationIdentity = "SerializeForCustomItemStatusTextBlock";
        public SerializeForCustomItemStatusTextBlockTest() : base(AutomationIdentity)
        { }

        public override void DebugSetup()
        {
            AutomationItemStatus.Serializer = new CustomItemStatusSerializer();
            
            AutomationItemStatus.SerializeForTextBlock = (textBlock, itemStatus) =>
            {
                return new TextBlockItemStatus(textBlock);
            };
        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return itemStatus == TextBlock.FontFamily.Source;
        }
    }
}
