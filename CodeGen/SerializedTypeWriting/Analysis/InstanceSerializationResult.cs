using System;

namespace CodeGen
{
    public class InstanceSerializationResult
    {
        public InstanceSerializationResult(Type type, InstanceSerializationResultType resultType)
        {
            Type = type;
            ResultType = resultType;
        }
        public Type Type { get; set; }
        public InstanceSerializationResultType ResultType { get; set; }
        public string? ErrorMessage { get; set; }
        public Type? DeserilizedType { get; set; }
        
        public object? Deserialized { get; internal set; }
    }
}
