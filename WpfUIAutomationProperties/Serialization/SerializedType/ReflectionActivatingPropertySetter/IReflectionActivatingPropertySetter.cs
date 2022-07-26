using System.Collections.Generic;

namespace WpfUIAutomationProperties.Serialization
{
    public interface IReflectionActivatingPropertySetter
    {
        object ActivateAndSetProperties(IEnumerable<(string propertyName, object value)> propertyValues);
    }
}