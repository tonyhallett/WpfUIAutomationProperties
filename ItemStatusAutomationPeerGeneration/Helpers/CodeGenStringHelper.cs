using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemStatusAutomationPeerGeneration
{
    public static class CodeGenStringHelper
    {
        public static string GetUsings(IEnumerable<string> namespaces)
        {
            return Build(namespaces.Distinct().Select(ns => $"using {ns};"));
        }

        public static string Build(IEnumerable<string> parts)
        {
            var builder = new StringBuilder();
            foreach (var part in parts)
            {
                builder.AppendLine(part);
            }
            return builder.ToString();
        }
    }
}
