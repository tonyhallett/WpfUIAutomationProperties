using System.Windows.Media;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class SerializedTextBlock
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Brush Background { get; set; }
        public FontFamily FontFamily { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public double FontSize { get; set; }

        [AttachedProperty(typeof(DemoAttached), nameof(DemoAttached.SomeAttachedProperty))]
        public int SomeAttached { get; set; }
    }
}
