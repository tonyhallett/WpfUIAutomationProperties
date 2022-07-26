using System.Windows;

namespace WpfUIAutomationProperties
{
    public interface IConvertDependencyProperty
    {
        DependencyProperty DependencyProperty { get; }
        object Convert(object value);
    }
}
