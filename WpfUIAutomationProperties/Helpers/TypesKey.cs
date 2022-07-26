using System;

namespace WpfUIAutomationProperties
{
    internal class TypesKey : IEquatable<TypesKey>
    {
        public TypesKey(params Type[] types)
        {
            Types = types;
        }

        public Type[] Types { get; }

        public bool Equals(TypesKey other)
        {
            if (Types.Length != other.Types.Length) return false;
            for (int i = 0; i < Types.Length; i++)
            {
                if (Types[i] != (other.Types[i])) return false;
            }
            return true;
        }
    }
}
