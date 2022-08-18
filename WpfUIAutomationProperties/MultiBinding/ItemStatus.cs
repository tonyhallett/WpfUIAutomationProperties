using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Data;
using WpfUIAutomationProperties.Serialization;

namespace WpfUIAutomationProperties.MultiBinding
{
    public static class ItemStatus
    {
        public static IItemStatusSerializer Serializer { get; set; } = new ItemStatusTypedDictionarySerializer();
        #region Serialized Type
        public static void ForElement<TSerialized,TFrameworkElement>(
            TFrameworkElement element, 
            Func<object, string> serializer = null
        ) where TFrameworkElement : FrameworkElement where TSerialized : new()
        {
            var reflectionActivatingPropertySetter = ReflectionActivatingPropertySetterFactory.Get<TSerialized>();
            
            var mappedFrameworkElementProperties = 
                MappedFrameworkElementPropertiesFactory.Get<TSerialized,TFrameworkElement>();
            ForElement(element, 
                mappedFrameworkElementProperties.ConvertDependencyProperties,
                reflectionActivatingPropertySetter.ActivateAndSetProperties,
                serializer
            );
        }

        internal static void ForElement(
            Type serializedObjectType,
            FrameworkElement element, 
            Func<object, string> serializer = null
        )
        {
            var reflectionActivatingPropertySetter = ReflectionActivatingPropertySetterFactory.Get(serializedObjectType);
            var mappedFrameworkElementProperties = MappedFrameworkElementPropertiesFactory.Get(serializedObjectType, element.GetType());
            
            ForElement(element,
                mappedFrameworkElementProperties.ConvertDependencyProperties,
                reflectionActivatingPropertySetter.ActivateAndSetProperties,
                serializer
            );
        }

        #endregion

        public static void ForElement(
            FrameworkElement element, 
            IEnumerable<DependencyProperty> dps,
            Func<Dictionary<string,object>, object> converter = null,
            Func<object, string> serializer = null
        )
        {
            if (dps == null)
            {
                RemoveBinding(element);
            }
            else
            {
                ForElement(element, 
                    dps.Select(dp => new NoConvertDependencyProperty { DependencyProperty = dp }), 
                    converter, 
                    serializer
                );
            }
        }

        private static void RemoveBinding(FrameworkElement element) => 
            BindingOperations.ClearBinding(element, AutomationProperties.ItemStatusProperty);

        public static void ForElement(
            FrameworkElement element, 
            IEnumerable<IConvertDependencyProperty> convertDependencyProperties,
            Func<Dictionary<string,object>, object> converter = null,
            Func<object, string> serializer = null
        )
        {
            if (convertDependencyProperties == null)
            {
                RemoveBinding(element);
            }
            else
            {
                ApplyMultiBinding(element, convertDependencyProperties, converter, serializer);
            }
        }

        private static void ApplyMultiBinding(
            FrameworkElement element, 
            IEnumerable<IConvertDependencyProperty> convertDependencyProperties,
            Func<Dictionary<string,object>, object> converter,
            Func<object, string> serializer
        )
        {
            var multiBinding = GetNameValueConvertedMultiBinding(
                element, convertDependencyProperties, converter, serializer
            );
            element.SetBinding(AutomationProperties.ItemStatusProperty, multiBinding);
        }

        private static System.Windows.Data.MultiBinding GetNameValueConvertedMultiBinding(
            FrameworkElement element,
            IEnumerable<IConvertDependencyProperty> convertDependencyProperties,
            Func<Dictionary<string,object>, object> converter,
            Func<object, string> serializer
        )
        {
            serializer = serializer ?? Serializer.Serialize;
            converter = converter ?? ((d) => d);

            var multiBinding = new System.Windows.Data.MultiBinding
            {
                Converter = new SerializingItemStatusConverter(converter, serializer),
            };
            
            multiBinding.Bindings.AddRange(
                convertDependencyProperties.Select(
                    convertDependencyProperty => GetNameValueConvertedBinding(convertDependencyProperty, element)
                )
            );
            return multiBinding;
        }

        private static Binding GetNameValueConvertedBinding(
            IConvertDependencyProperty convertDependencyProperty, 
            FrameworkElement element
        ) {
            var binding = new Binding
            {
                Source = element,
                Mode = BindingMode.OneWay
            };
            var dependencyPropertyName = convertDependencyProperty.DependencyProperty.Name;
            binding.Path = new PropertyPath(convertDependencyProperty.DependencyProperty);
            binding.Converter = new FuncValueConverter((value) =>
            {
                return (dependencyPropertyName, convertDependencyProperty.Convert(value));
            });
            return binding;
        }
    }
}
