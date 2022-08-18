using System.Windows.Controls;
using WpfUIAutomationProperties.MultiBinding;
using System.Collections.Generic;
using WpfUIAutomationProperties.Serialization;
using System.Windows.Media;

namespace AutomationTest
{
    public class XamlAttachedConvertDependencyPropertiesTest : XamlTextBlockTestBase
    {
        public const string AutomationIdentity = "AttachedConvertDependencyPropertiesTextBlock";
        public XamlAttachedConvertDependencyPropertiesTest() : base(AutomationIdentity)
        {

        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var dictionary = ItemStatus.Serializer.Deserialize<Dictionary<string, object>>(itemStatus);
            if (dictionary.Count != 2) return false;
            return dictionary.Get<FontFamily>(TextBlock.FontFamilyProperty.Name).Source == TextBlock.FontFamily.Source
                && dictionary.Get<string>(TextBlock.BackgroundProperty.Name) == TextBlock.Background.ToString();
        }
    }
}
