using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WpfUIAutomationProperties.Serialization
{
    public class ItemStatusTypedDictionarySerializer : JsonConvertSerializer
    {
        public ItemStatusTypedDictionarySerializer()
        {
            Settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                // taken from https://dotnetfiddle.net/Y0EQKPhttps://stackoverflow.com/questions/35592256/json-net-how-do-i-include-type-name-handling-for-primitive-c-sharp-types-that
                ContractResolver = UntypedToTypedValueContractResolver.Instance,
                Converters = new[] { new StringEnumConverter() }
            };
        }
    }
}
