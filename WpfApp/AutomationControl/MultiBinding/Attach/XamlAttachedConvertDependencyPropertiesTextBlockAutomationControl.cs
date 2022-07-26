﻿using System.Windows.Controls;
using WpfUIAutomationProperties.MultiBinding;
using System.Collections.Generic;
using WpfUIAutomationProperties.Serialization;
using System.Windows.Media;

namespace AutomationTest
{
    public class XamlAttachedConvertDependencyPropertiesTextBlockAutomationControl : XamlTextBlockAutomationControl
    {
        public const string AutomationIdentity = "AttachedConvertDependencyPropertiesTextBlock";
        public XamlAttachedConvertDependencyPropertiesTextBlockAutomationControl() : base(AutomationIdentity)
        {

        }

        protected override bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            var dictionary = ItemStatus.ItemStatusSerializer.Deserialize<Dictionary<string, object>>(itemStatus);
            if (dictionary.Count != 2) return false;
            return dictionary.Get<FontFamily>(TextBlock.FontFamilyProperty.Name).Source == TextBlock.FontFamily.Source
                && dictionary.Get<string>(TextBlock.BackgroundProperty.Name) == TextBlock.Background.ToString();
        }
    }
}
