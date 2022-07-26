using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.MultiBinding;

namespace AutomationTest
{
    public class SerializedTextBlockPropertyDerivedTypeAutomationControl : TextBlockAutomationControl
    {
        public const string AutomationIdentity = "SerializePropertyDerivedType";

        public class SerializedTypeDerivedProperty
        {
            public SolidColorBrush Background { get; set; }
        }

        public SerializedTextBlockPropertyDerivedTypeAutomationControl() : base(AutomationIdentity) { }

        public override void DebugSetup()
        {
            ItemStatus.Apply<SerializedTypeDerivedProperty, TextBlock>(TextBlock);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var serializedTypeWithConvert = ItemStatus.ItemStatusSerializer.Deserialize<SerializedTypeDerivedProperty>(itemStatus);
            return serializedTypeWithConvert.Background.Color.ToString() == TextBlock.Background.ToString();
        }
    }
}
