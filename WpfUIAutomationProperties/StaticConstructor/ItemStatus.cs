using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfUIAutomationProperties.Serialization;

namespace WpfUIAutomationProperties.StaticConstructor
{
	public static class ItemStatus
	{
		public static IItemStatusSerializer Serializer { get; set; } = new ItemStatusTypedDictionarySerializer();

		public static void ForType<TFrameworkElement>(
			List<DependencyProperty> dependencyProperties,
			Func<Dictionary<string, object>, object> itemStatusesConverter = null,
			Func<object, string> itemStatusSerializer = null
		)
		{
			ForType<TFrameworkElement>(
				dependencyProperties.Select(dp => new NoConvertDependencyProperty(dp))
					.ToList<IConvertDependencyProperty>(),
				itemStatusesConverter,
				itemStatusSerializer
			);
		}

		public static void ForType<TFrameworkElement>(
			List<IConvertDependencyProperty> convertDependencyProperties,
			Func<Dictionary<string, object>, object> itemStatusesConverter = null,
			Func<object, string> itemStatusSerializer = null
		)
		{
			var itemStatusSetter = new SerializedConvertedDependencyPropertiesItemStatusSetter(
				typeof(TFrameworkElement),
                convertDependencyProperties,
				itemStatusSerializer ?? Serializer.Serialize,
                itemStatusesConverter
			);

			foreach (var convertDependencyProperty in convertDependencyProperties)
			{
				var dependencyProperty = convertDependencyProperty.DependencyProperty;
				dependencyProperty.OverrideMetadata(typeof(TFrameworkElement), new FrameworkPropertyMetadata((depObj, args) =>
				{
					itemStatusSetter.PropertyChanged(convertDependencyProperty, args.NewValue, depObj as FrameworkElement);
				}));
			}
		}

		public static void ForType<TFrameworkElement, TSerialize>(Func<object, string> itemStatusSerializer = null) 
			where TSerialize : new()
        {
			var convertDependencyProperties = MappedFrameworkElementPropertiesFactory.Get<TSerialize, TFrameworkElement>().ConvertDependencyProperties;
			var reflectionActivatingPropertySetter = ReflectionActivatingPropertySetterFactory.Get<TSerialize>();

			ForType<TFrameworkElement>(
				convertDependencyProperties.ToList(),
				reflectionActivatingPropertySetter.ActivateAndSetProperties,
				itemStatusSerializer
			);
        }
    }

}
