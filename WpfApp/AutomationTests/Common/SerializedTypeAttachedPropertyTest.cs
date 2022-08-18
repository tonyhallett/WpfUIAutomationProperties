using System.Windows;
using System.Windows.Controls;
using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class SerializedTypeAttachedPropertyTest : TextBlockItemStatusTestBase
    {
        public static int GetAProperty(DependencyObject obj)
        {
            return (int)obj.GetValue(APropertyProperty);
        }

        public static void SetAProperty(DependencyObject obj, int value)
        {
            obj.SetValue(APropertyProperty, value);
        }

        public static readonly DependencyProperty APropertyProperty =
            DependencyProperty.RegisterAttached("AProperty", typeof(int), typeof(SerializedTypeAttachedPropertyTest), new PropertyMetadata(0));


        public const string AutomationIdentity = "SerializedTypeAttachedProperty";

        public class SerializedTypeAttachedProperty
        {
            [AttachedProperty(typeof(SerializedTypeAttachedPropertyTest),nameof(SerializedTypeAttachedPropertyTest.APropertyProperty))]
            public int AProperty { get; set; }
        }

        public SerializedTypeAttachedPropertyTest() : base(AutomationIdentity) { }

        public override void DebugSetup()
        {
            SetAProperty(TextBlock, 123);
            ItemStatus.ForElement<SerializedTypeAttachedProperty, TextBlock>(TextBlock);
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var serializedTypeWithConvert = ItemStatus.Serializer.Deserialize<SerializedTypeAttachedProperty>(itemStatus);
            return serializedTypeWithConvert.AProperty == 123;
        }
    }
}
