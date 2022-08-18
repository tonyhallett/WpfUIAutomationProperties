using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;
using WpfTextBlock = System.Windows.Controls.TextBlock;

namespace AutomationTest
{
    public class MultiBindingDpsTest : TextBlockItemStatusTestBase
    {
        public const string AutomationIdentity = "MultiBindingDpsDictionary";
        public MultiBindingDpsTest() : base(AutomationIdentity) { }
        public override void DebugSetup()
        {
            ItemStatus.ForElement(TextBlock, new List<DependencyProperty>
            {
                WpfTextBlock.FontFamilyProperty,
                WpfTextBlock.BackgroundProperty,
                WpfTextBlock.FontSizeProperty,
                DemoAttached.SomeAttachedProperty
            });
        }
        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var dictionary = ItemStatus.Serializer.Deserialize<Dictionary<string,object>>(itemStatus);
            
            if (dictionary.Count != 4) return false;

            return dictionary.Get<FontFamily>(WpfTextBlock.FontFamilyProperty.Name).Source == TextBlock.FontFamily.Source
                && dictionary.Get<Brush>(WpfTextBlock.BackgroundProperty.Name).ToString() == TextBlock.Background.ToString()
                && dictionary.Get<double>(WpfTextBlock.FontSizeProperty.Name) == TextBlock.FontSize
                && dictionary.Get<int>(DemoAttached.SomeAttachedProperty.Name) == DemoAttached.GetSomeAttached(TextBlock);
        }
    }
}
