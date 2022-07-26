using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties;

namespace AutomationTest
{
    public class DemoForegroundConverterDependencyProperty : FuncConvertDependencyPropertyNotNull<Brush>
    {
        public DemoForegroundConverterDependencyProperty() :
            base(TextBlock.BackgroundProperty, brush => (brush as SolidColorBrush).Color.ToString())
        {
        }
    }

}
