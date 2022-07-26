using System;
using System.Windows;

namespace WpfUIAutomationProperties
{
    public class FuncConvertDependencyProperty<T> : IConvertDependencyProperty
    {
        private readonly Func<T, object> convert;

        public FuncConvertDependencyProperty(DependencyProperty dependencyProperty, Func<T,object> convert)
        {
            DependencyProperty = dependencyProperty;
            this.convert = convert;
        }
        public DependencyProperty DependencyProperty { get; }

        public object Convert(object value) => this.convert((T)value);
    }
}
