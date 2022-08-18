using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.MultiBinding;

namespace AutomationTest
{
    public class SerializedTextBlockPropertyDerivedTypeTest : TextBlockItemStatusTestBase
    {
        public const string AutomationIdentity = "SerializePropertyDerivedType";

        public class SerializedTypeDerivedProperty
        {
            public SolidColorBrush Background { get; set; }
        }

        public SerializedTextBlockPropertyDerivedTypeTest() : base(AutomationIdentity) { }

        public override void DebugSetup()
        {
            ItemStatus.ForElement<SerializedTypeDerivedProperty, TextBlock>(TextBlock);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var serializedTypeWithConvert = ItemStatus.Serializer.Deserialize<SerializedTypeDerivedProperty>(itemStatus);
            return serializedTypeWithConvert.Background.Color.ToString() == TextBlock.Background.ToString();
        }
    }
}
