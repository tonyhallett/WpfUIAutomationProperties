using Newtonsoft.Json;

namespace CodeGen
{
    public static class InstanceSerializer {
        public static readonly JsonSerializerSettings Settings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
        };
        public static InstanceSerializationResult Serialize(object instance)
        {
            var type = instance.GetType();

            string? serialized;
            try
            {
                serialized = JsonConvert.SerializeObject(instance,Settings);
            }
            catch (Exception ex)
            {
                return new InstanceSerializationResult(type, InstanceSerializationResultType.SerializeException)
                { 
                    ErrorMessage = ex.Message,
                };
            }
            if (serialized != null)
            {
                try
                {
                    var deserialized = JsonConvert.DeserializeObject(serialized, type);
                    var deserializedType = deserialized!.GetType();
                    if (deserializedType != type)
                    {
                        return new InstanceSerializationResult(type, InstanceSerializationResultType.DifferentSerializedType)
                        {
                            DeserilizedType = deserializedType,
                            Deserialized = deserialized
                        };
                    }
                    else
                    {
                        return new InstanceSerializationResult(type, InstanceSerializationResultType.Success)
                        {
                            Deserialized = deserialized
                        };
                    }
                }
                catch (Exception ex)
                {
                    return new InstanceSerializationResult(type, InstanceSerializationResultType.DeserializeException)
                    {
                        ErrorMessage = ex.Message
                    };
                }
            }

            return new InstanceSerializationResult(type, InstanceSerializationResultType.Null);
        }
    }
}
