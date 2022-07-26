using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal class SerializedPropertiesResult
    {
        public IEnumerable<ISerializedProperty> SerializedProperties { get; set; }
        public Diagnostic Diagnostic { get; set; }
    }
}
