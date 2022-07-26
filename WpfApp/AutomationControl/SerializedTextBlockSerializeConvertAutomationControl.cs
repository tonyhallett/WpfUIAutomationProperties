using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class SerializedTextBlockSerializeConvertAutomationControl : TextBlockAutomationControl
    {
        public const string AutomationIdentity = "SerializeConvert";

        public class SerializeConvertBrush : ISerializeConvert<Brush>
        {
            public string BrushType { get; set; }
            public void Convert(Brush brush)
            {
                BrushType = brush.GetType().Name;
            }
        }

        public class SerializedTypeWithConvert
        {
            public SerializeConvertBrush Background { get; set; }
        }

        public SerializedTextBlockSerializeConvertAutomationControl() : base(AutomationIdentity) { }

        public override void DebugSetup()
        {
            ItemStatus.Apply<SerializedTypeWithConvert, TextBlock>(TextBlock);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var serializedTypeWithConvert = ItemStatus.ItemStatusSerializer.Deserialize<SerializedTypeWithConvert>(itemStatus);
            return serializedTypeWithConvert.Background.BrushType == typeof(SolidColorBrush).Name;
        }
    }
}
