namespace SerializedTypeSourceGenerator
{
    internal static class DependencyPropertyName
    {
        public static bool IsNot(string dependencyPropertyName)
        {
            return !dependencyPropertyName.EndsWith("Property");
        }
        public static string ToPropertyName(string dependencyPropertyName)
        {
            return dependencyPropertyName.Substring(0, dependencyPropertyName.Length - 8);
        } 
    }
}
