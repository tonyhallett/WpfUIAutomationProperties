using System.Collections.Generic;
using System.Windows.Controls;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public class CustomSerialized
    {
        public static List<System.Windows.DependencyProperty> DependencyProperties = new List<System.Windows.DependencyProperty>
        {
            TextBlock.FontFamilyProperty,
            TextBlock.BackgroundProperty,
            TextBlock.FontSizeProperty,
            DemoAttached.SomeAttachedProperty
        };

        public static bool DebugIsExpectedItemStatus(TextBlock textBlock, string itemStatus, JsonConvertSerializer serializer)
        {
            var dictionary = serializer.Deserialize<Dictionary<string, object>>(itemStatus);

            if (dictionary.Count != 4) return false;
            return dictionary.Get<string>(TextBlock.FontFamilyProperty.Name) == textBlock.FontFamily.Source
                && dictionary.Get<string>(TextBlock.BackgroundProperty.Name) == textBlock.Background.ToString()
                && dictionary.Get<double>(TextBlock.FontSizeProperty.Name) == textBlock.FontSize
                // jsonconvert will deserialize int as long
                && dictionary.Get<long>(DemoAttached.SomeAttachedProperty.Name) == DemoAttached.GetSomeAttached(textBlock);
        }
    }

}
