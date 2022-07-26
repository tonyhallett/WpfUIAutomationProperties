using System;

namespace ItemStatusAutomationPeerGeneration
{
    public static class MsTypeHelper
    {
        public static bool IsDerivable(Type type)
        {
            return !(type.IsSealed || type.HasNoPublicConstructor());
        }
    }
}
