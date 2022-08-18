using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties;
using WpfUIAutomationProperties.Serialization;
using WpfUIAutomationProperties.StaticConstructor;

namespace AutomationTest
{
    public class StaticCtorItemStatusTextBlockCDpsDictionaryTest : StaticCtorItemStatusTextBlockAutomationControlBase
    {
        public class StaticCtorItemStatusTextBlockCDpsDictionaryTextBlock : TextBlock
        {
            static StaticCtorItemStatusTextBlockCDpsDictionaryTextBlock()
            {
                WpfUIAutomationProperties.StaticConstructor.ItemStatus.ForType<StaticCtorItemStatusTextBlockCDpsDictionaryTextBlock>(
                    new List<IConvertDependencyProperty>
                    {
                        new FuncConvertDependencyProperty<FontFamily>(TextBlock.FontFamilyProperty,ff => ff.Source),
                        new ToStringConvertDependencyProperty(TextBlock.BackgroundProperty),
                        new ToStringConvertDependencyProperty(TextBlock.TextAlignmentProperty),
                        new NoConvertDependencyProperty(TextBlock.PaddingProperty),
                        new NoConvertDependencyProperty(DemoAttached.SomeAttachedProperty)
                    }
                );
            }
        }
        public const string AutomationIdentity = "StaticCtorItemStatusTextBlockCDpsDictionary";
        public StaticCtorItemStatusTextBlockCDpsDictionaryTest() : base(AutomationIdentity)
        {

        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var dictionary = ItemStatus.Serializer.Deserialize<Dictionary<string, object>>(itemStatus);
            if (dictionary.Values.Count != 5)
            {
                return false;
            }

            return dictionary.Get<string>(TextBlock.FontFamilyProperty.Name) == TextBlock.FontFamily.Source
                && dictionary.Get<string>(TextBlock.BackgroundProperty.Name) == TextBlock.Background.ToString()
                && dictionary.Get<string>(TextBlock.TextAlignmentProperty.Name) == TextBlock.TextAlignment.ToString()
                && dictionary.Get<Thickness>(TextBlock.PaddingProperty.Name) == TextBlock.Padding
                && dictionary.Get<int>(DemoAttached.SomeAttachedProperty.Name) == DemoAttached.GetSomeAttached(TextBlock);
        }

        protected override TextBlock GetItemStatusTextBlock()
        {
            return new StaticCtorItemStatusTextBlockCDpsDictionaryTextBlock();
        }
    }

}
