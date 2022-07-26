using System.Windows;
using System;

namespace WpfUIAutomationProperties.MultiBinding
{
    public class Attacher
    {
        #region serialized type
        public static Type GetSerializedType(DependencyObject obj)
        {
            return (Type)obj.GetValue(SerializedTypeProperty);
        }

        public static void SetSerializedType(DependencyObject obj, Type value)
        {
            obj.SetValue(SerializedTypeProperty, value);
        }

        public static readonly DependencyProperty SerializedTypeProperty =
            DependencyProperty.RegisterAttached("SerializedType", typeof(Type), typeof(Attacher), new PropertyMetadata(null,(target,args) =>
            {
                ItemStatus.Apply(args.NewValue as Type, target as FrameworkElement);
            }));
        #endregion

        #region dependency properties
        public static readonly DependencyProperty ItemStatusDependencyPropertiesProperty =
            DependencyProperty.RegisterAttached(
          "ItemStatusDependencyProperties",
          typeof(DependencyPropertyList),
          typeof(Attacher),
          new FrameworkPropertyMetadata(defaultValue: new DependencyPropertyList(), propertyChangedCallback:(target,args) =>
          {
              ItemStatus.Apply(target as FrameworkElement, args.NewValue as DependencyPropertyList);
          })
        );

        public static DependencyPropertyList GetItemStatusDependencyProperties(FrameworkElement target) =>
            (DependencyPropertyList)target.GetValue(ItemStatusDependencyPropertiesProperty);

        public static void SetItemStatusDependencyProperties(FrameworkElement target, DependencyPropertyList value) => 
            target.SetValue(ItemStatusDependencyPropertiesProperty, value);
        #endregion

        #region convert dependency properties
        public static readonly DependencyProperty ItemStatusConvertDependencyPropertiesProperty =
            DependencyProperty.RegisterAttached(
          "ItemStatusConvertDependencyProperties",
          typeof(ConvertDependencyPropertyList),
          typeof(Attacher),
          new FrameworkPropertyMetadata(defaultValue: new ConvertDependencyPropertyList(), propertyChangedCallback: (target, args) =>
          {
              ItemStatus.Apply(target as FrameworkElement, args.NewValue as ConvertDependencyPropertyList);
          })
        );

        public static ConvertDependencyPropertyList GetItemStatusConvertDependencyProperties(FrameworkElement target) =>
            (ConvertDependencyPropertyList)target.GetValue(ItemStatusDependencyPropertiesProperty);

        public static void SetItemStatusConvertDependencyProperties(FrameworkElement target, ConvertDependencyPropertyList value) =>
            target.SetValue(ItemStatusConvertDependencyPropertiesProperty, value);
        #endregion
    }
}
