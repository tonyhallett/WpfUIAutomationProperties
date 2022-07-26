using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.Serialization;
using WpfUIAutomationProperties.StaticConstructor;

namespace AutomationTest
{
    public class StaticCtorItemStatusTextBlockDpsDictionaryAutomationControl : StaticCtorItemStatusTextBlockAutomationControlBase
    {
        public class StaticCtorItemStatusTextBlockDpsDictionary : TextBlock
        {
            static StaticCtorItemStatusTextBlockDpsDictionary()
            {
                WpfUIAutomationProperties.StaticConstructor.ItemStatus.ForType<StaticCtorItemStatusTextBlockDpsDictionary>(
                    new List<System.Windows.DependencyProperty>
                    {
                    TextBlock.FontFamilyProperty,
                    TextBlock.BackgroundProperty,
                    TextBlock.FontSizeProperty,
                    DemoAttached.SomeAttachedProperty
                    }
                );
            }
        }

        public const string AutomationIdentity = "StaticCtorItemStatusTextBlockDpsDictionary";
        public StaticCtorItemStatusTextBlockDpsDictionaryAutomationControl() : base(AutomationIdentity)
        {
            
        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var dictionary = ItemStatus.ItemStatusSerializer.Deserialize<Dictionary<string,object>>(itemStatus);
            if(dictionary.Values.Count != 4)
            {
                return false;
            }

            return dictionary.Get<FontFamily>(TextBlock.FontFamilyProperty.Name).Source == TextBlock.FontFamily.Source
                && dictionary.Get<SolidColorBrush>(TextBlock.BackgroundProperty.Name).Color.ToString() == TextBlock.Background.ToString()
                && dictionary.Get<double>(TextBlock.FontSizeProperty.Name) == TextBlock.FontSize
                && dictionary.Get<int>(DemoAttached.SomeAttachedProperty.Name) == DemoAttached.GetSomeAttached(TextBlock);
        }

        protected override TextBlock GetItemStatusTextBlock()
        {
            return new StaticCtorItemStatusTextBlockDpsDictionary();
        }
    }

}
