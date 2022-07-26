using Newtonsoft.Json;

namespace WpfUIAutomationProperties.Serialization
{
    public class JsonConvertSerializer : IItemStatusSerializer
    {
        protected JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };

        public string Serialize(object itemStatus)
        {
            return JsonConvert.SerializeObject(itemStatus, Settings);
        }

        public T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized, Settings);
        }
    }
}
