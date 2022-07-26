using ItemStatusAutomationPeerGeneration;

namespace CodeGen
{
    public static class FrameworkElementSerializeObjectWriter { 
        public static void Write(string directoryPath, string @namespace)
        {
            var allTypeDps = FrameworkElementTypeProvider.Types().Where(t => !t.IsGenericType).Select(type => new TypeDps(type));
            foreach(var typeDps in allTypeDps)
            {
                var code = SerializedFrameworkElement.GetCode(typeDps, @namespace);
                File.WriteAllText(System.IO.Path.Combine(directoryPath, $"{typeDps.Type.Name}.cs"), code);
            }
        }
    }
}
