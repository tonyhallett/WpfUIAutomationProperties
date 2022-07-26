using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfUIAutomationProperties.StaticConstructor
{
    internal class SerializedConvertedDependencyPropertiesItemStatusSetter
	{
		private readonly Dictionary<string, object> values;
        private readonly Func<Dictionary<string, object>, object> itemStatusesConverter;
        private readonly Func<object, string> itemStatusSerializer;

        public SerializedConvertedDependencyPropertiesItemStatusSetter(
			Type frameworkElementType,
            List<IConvertDependencyProperty> convertDependencyProperties,
			Func<object, string> itemStatusSerializer,
			Func<Dictionary<string, object>, object> itemStatusesConverter = null)
        {
			this.itemStatusesConverter = itemStatusesConverter ?? (itemStatuses => itemStatuses);
			this.itemStatusSerializer = itemStatusSerializer;

            values = convertDependencyProperties.ToDictionary(
                convertDependencyProperty => convertDependencyProperty.DependencyProperty.Name,
                convertDependencyProperty =>
                {
                    var metadata = convertDependencyProperty.DependencyProperty.GetMetadata(frameworkElementType);
                    var defaultValue = metadata.DefaultValue;
                    return convertDependencyProperty.Convert(defaultValue);
                }
            );
        }

        public void PropertyChanged(
			IConvertDependencyProperty convertDependencyProperty, 
			object value,
			FrameworkElement element
		)
		{
			UpdateValues(convertDependencyProperty, value);
			SetAutomationStatus(element);
		}

		private void UpdateValues(
			IConvertDependencyProperty convertDependencyProperty,
			object value
        )
        {
			var changedDp = convertDependencyProperty.DependencyProperty;
			values[changedDp.Name] = convertDependencyProperty.Convert(value);
		}

		private void SetAutomationStatus(FrameworkElement element)
        {
			var itemStatus = itemStatusSerializer(itemStatusesConverter(values));
			System.Windows.Automation.AutomationProperties.SetItemStatus(element, itemStatus);
		}
    }
}
