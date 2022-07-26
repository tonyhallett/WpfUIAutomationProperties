using System.Collections.Generic;
using System.Windows.Media;
using WpfUIAutomationProperties;
using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;
using WpfTextBlock = System.Windows.Controls.TextBlock;

namespace AutomationTest
{
    public class MultiBindingCDpsDictionaryAutomationControl : TextBlockAutomationControl
    {
        public const string AutomationIdentity = "MultiBindingCDpsDictionary";
        public MultiBindingCDpsDictionaryAutomationControl() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.Apply(TextBlock, new List<IConvertDependencyProperty>
            {
                new FuncConvertDependencyProperty<FontFamily>(WpfTextBlock.FontFamilyProperty,ff => ff.Source),
                new ToStringConvertDependencyProperty(WpfTextBlock.BackgroundProperty),
                new NoConvertDependencyProperty(WpfTextBlock.FontSizeProperty),
                new NoConvertDependencyProperty(DemoAttached.SomeAttachedProperty)
            });
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var dictionary = ItemStatus.ItemStatusSerializer.Deserialize<Dictionary<string, object>>(itemStatus);

            if (dictionary.Count != 4) return false;

            return dictionary.Get<string>(WpfTextBlock.FontFamilyProperty.Name) == TextBlock.FontFamily.Source
                && dictionary.Get<string>(WpfTextBlock.BackgroundProperty.Name) == TextBlock.Background.ToString()
                && dictionary.Get<double>(WpfTextBlock.FontSizeProperty.Name) == TextBlock.FontSize
                && dictionary.Get<int>(DemoAttached.SomeAttachedProperty.Name) == DemoAttached.GetSomeAttached(TextBlock);
        }
    }
}
