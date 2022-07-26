using System;
using System.Collections.Generic;

namespace WpfUIAutomationProperties.Serialization
{
    public static class ReflectionActivatingPropertySetterFactory
    {
        private static readonly Dictionary<Type, IReflectionActivatingPropertySetter> store = 
            new Dictionary<Type, IReflectionActivatingPropertySetter>();

        public static IReflectionActivatingPropertySetter Get<TActivated>() where TActivated : new()
        {
            if(!store.TryGetValue(typeof(TActivated), out var reflectionActivatingPropertySetter))
            {
                reflectionActivatingPropertySetter = new ReflectionActivatingPropertySetter<TActivated>();
                store[typeof(TActivated)] = reflectionActivatingPropertySetter;
            }
            return reflectionActivatingPropertySetter as ReflectionActivatingPropertySetter<TActivated>;
        }

        public static IReflectionActivatingPropertySetter Get(Type typeActivated)
        {
            if (!store.TryGetValue(typeActivated, out var reflectionActivatingPropertySetter))
            {
                var constructedType = typeof(ReflectionActivatingPropertySetter<>).MakeGenericType(typeActivated);
                reflectionActivatingPropertySetter = Activator.CreateInstance(constructedType) as IReflectionActivatingPropertySetter;
                store[typeActivated] = reflectionActivatingPropertySetter;
            }
            return reflectionActivatingPropertySetter;
        }
    }
}
