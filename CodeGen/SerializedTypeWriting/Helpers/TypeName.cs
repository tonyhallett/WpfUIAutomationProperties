namespace CodeGen
{
    public static class TypeName {
        private readonly static Dictionary<string, string> typeKeyword = new()
        {
            { "Boolean","bool"},
            { "Byte","byte"},
            { "SByte","sbyte"},
            { "Char","char"},
            { "Decimal","decimal"},
            { "Double","double"},
            { "Single","float"},
            { "Int32","int"},
            { "UInt32","uint"},
            { "IntPtr","nuint"},
            { "Int64","long"},
            { "UInt64","ulong"},
            { "Int16","short"},
            { "UInt16","ushort"},
            { "Object","object"},
            { "String","string"},
        };

        public static string Get(Type type)
        {
            if (type.IsGenericType)
            {
                var n = type.Name;
                var index = n.IndexOf("`");
                var genericName = n[..index];

                var argsPart = string.Join(",", type.GetGenericArguments().Select(typeArgumentType => TypeName.Get(typeArgumentType)));
                return $"{genericName}<{argsPart}>";
            }
            
            var name = type.Name;
            if (typeKeyword.TryGetValue(name, out var keywordName))
            {
                return keywordName;
            }
            return name;
        }
    }
}
