using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfUIAutomationProperties.Serialization;

namespace WpfUIAutomationProperties.StaticConstructor
{
	public static class ItemStatus
	{
		public static IItemStatusSerializer ItemStatusSerializer { get; set; } = new ItemStatusTypedDictionarySerializer();

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
			List<IConvertDependencyProperty> dependencyPropertyItemStatusProviders,
			Func<Dictionary<string, object>, object> itemStatusesConverter = null,
			Func<object, string> itemStatusSerializer = null
		)
		{
			itemStatusSerializer = itemStatusSerializer ?? ItemStatusSerializer.Serialize;
			var itemStatus = new SerializedConvertedDependencyPropertiesItemStatusSetter(
				typeof(TFrameworkElement),
                dependencyPropertyItemStatusProviders,
                itemStatusSerializer,
                itemStatusesConverter);
			foreach (var dependencyPropertyItemStatusProvider in dependencyPropertyItemStatusProviders)
			{
				var dependencyProperty = dependencyPropertyItemStatusProvider.DependencyProperty;
				dependencyProperty.OverrideMetadata(typeof(TFrameworkElement), new FrameworkPropertyMetadata((depObj, args) =>
				{
					itemStatus.PropertyChanged(dependencyPropertyItemStatusProvider, args.NewValue, depObj as FrameworkElement);
				}));
			}
		}

		public static void ForType<TFrameworkElement, TSerialize>() where TSerialize : new()
        {
			var convertDependencyProperties = MappedFrameworkElementPropertiesFactory.Get<TSerialize, TFrameworkElement>().ConvertDependencyProperties;
			var reflectionActivatingPropertySetter = ReflectionActivatingPropertySetterFactory.Get<TSerialize>();

			ForType<TFrameworkElement>(
				convertDependencyProperties.ToList(),
				namedItemStatuses => reflectionActivatingPropertySetter.ActivateAndSetProperties(
					namedItemStatuses.Select(kvp => (kvp.Key, kvp.Value))
				)
			);
        }
    }

}
