using System;
using System.Windows;

namespace WpfUIAutomationProperties
{
    public class FuncConvertDependencyPropertyNotNull<T> : IConvertDependencyProperty
    {
        public FuncConvertDependencyPropertyNotNull(DependencyProperty dependencyProperty, Func<T, object> convert)
        {
            DependencyProperty = dependencyProperty;
            this.convert = convert;
        }
        public DependencyProperty DependencyProperty { get; }
        public object Convert(object value) => value == null ? null : this.convert((T)value);

        private Func<T, object> convert;
    }
}
