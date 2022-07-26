using System.Windows;

namespace WpfUIAutomationProperties
{
    public class ToStringConvertDependencyProperty : IConvertDependencyProperty
    {
        public ToStringConvertDependencyProperty(DependencyProperty dependencyProperty)
        {
            DependencyProperty = dependencyProperty;
        }

        public ToStringConvertDependencyProperty() { }

        public DependencyProperty DependencyProperty { get; set; }

        public object Convert(object value)
        {
            return value?.ToString();
        }
    }
}
