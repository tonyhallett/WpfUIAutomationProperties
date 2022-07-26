using System.Collections.Generic;

namespace WpfUIAutomationProperties.Serialization
{
    internal interface IMappedFrameworkElementProperties
    {
        IEnumerable<IConvertDependencyProperty> ConvertDependencyProperties { get; }
    }
}
