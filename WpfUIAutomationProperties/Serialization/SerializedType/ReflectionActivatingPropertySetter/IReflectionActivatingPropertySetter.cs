using System.Collections.Generic;

namespace WpfUIAutomationProperties.Serialization
{
    public interface IReflectionActivatingPropertySetter
    {
        object ActivateAndSetProperties(Dictionary<string,object> propertyValues);
    }
}