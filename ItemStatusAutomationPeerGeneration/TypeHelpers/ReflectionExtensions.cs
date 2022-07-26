using System;
using System.Linq;

namespace ItemStatusAutomationPeerGeneration
{
    internal static class ReflectionExtensions
    {
        public static bool HasNoPublicConstructor(this Type type)
        {
            return type.GetConstructors().All(c => c.IsPrivate);
        }
    }
}
