using System.Collections.Generic;

namespace SerializedTypeSourceGenerator
{
    internal static class TypeKeywords
    {
        private readonly static Dictionary<string, string> typeKeywords = new Dictionary<string, string>()
        {
            { "Boolean", "bool" },
            { "Byte", "byte" },
            { "SByte", "sbyte" },
            { "Char", "char" },
            { "Decimal", "decimal" },
            { "Double", "double" },
            { "Single", "float" },
            { "Int32", "int" },
            { "UInt32", "uint" },
            { "IntPtr", "nuint" },
            { "Int64", "long" },
            { "UInt64", "ulong" },
            { "Int16", "short" },
            { "UInt16", "ushort" },
            { "Object", "object" },
            { "String", "string" },
        };

        public static string Get(string typeName)
        {
            if (typeKeywords.TryGetValue(typeName, out var value))
            {
                return value;
            }
            return typeName;
        }
    }
}
