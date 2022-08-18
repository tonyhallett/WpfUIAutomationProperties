using System.Linq;
using System.Windows;
using WpfUIAutomationProperties.Serialization;

namespace WpfUIAutomationProperties.StaticItemStatus
{
    public class FrameworkElementMapper<TMapped, TFrameworkElement> 
        where TFrameworkElement : FrameworkElement
        where TMapped : new ()
    {
        private readonly IReflectionActivatingPropertySetter reflectionActivatingPropertySetter;
        private readonly IMappedFrameworkElementProperties mappedFrameworkElementProperties;

        public FrameworkElementMapper()
        {
            reflectionActivatingPropertySetter = ReflectionActivatingPropertySetterFactory.Get<TMapped>();
            mappedFrameworkElementProperties = MappedFrameworkElementPropertiesFactory.Get<TMapped, TFrameworkElement>();
        }
        public object Map(TFrameworkElement element)
        {
            return reflectionActivatingPropertySetter.ActivateAndSetProperties(
                mappedFrameworkElementProperties.ConvertDependencyProperties.ToDictionary(
                    cdp => cdp.DependencyProperty.Name,cdp => cdp.Convert(element.GetValue(cdp.DependencyProperty))
                )
            );
        }
    }
}
