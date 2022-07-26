using System.Windows;

namespace WpfUIAutomationProperties
{
    public class NoConvertDependencyProperty : IConvertDependencyProperty
    {
        public NoConvertDependencyProperty(DependencyProperty dependencyProperty)
        {
            DependencyProperty = dependencyProperty;
        }

        public NoConvertDependencyProperty() { }

        public DependencyProperty DependencyProperty { get; set; }

        public object Convert(object value) => value;
    }
}
