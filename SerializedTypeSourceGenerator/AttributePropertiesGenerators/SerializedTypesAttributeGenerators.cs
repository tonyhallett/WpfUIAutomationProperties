using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal static class SerializedTypesAttributeGenerators
    {
        public static List<ISerializedTypeAttributeGenerator> Generators { get; } = new List<ISerializedTypeAttributeGenerator>
        {
            new SerializedTypeGenerator(),
            new SerializedTypeAttachedGenerator(),
            new SerializedTypeTypedPropertyAttributeGenerator(),
            new SerializedTypeAttachedTypedPropertyGenerator()
        };
        
    }
}
