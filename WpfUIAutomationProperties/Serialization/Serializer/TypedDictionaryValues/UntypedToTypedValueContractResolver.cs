using Newtonsoft.Json.Serialization;
using System;

namespace WpfUIAutomationProperties.Serialization
{
    public class UntypedToTypedValueContractResolver : DefaultContractResolver
    {
        // As of 7.0.1, Json.NET suggests using a static instance for "stateless" contract resolvers, for performance reasons.
        // http://www.newtonsoft.com/json/help/html/ContractResolver.htm
        // http://www.newtonsoft.com/json/help/html/M_Newtonsoft_Json_Serialization_DefaultContractResolver__ctor_1.htm
        // "Use the parameterless constructor and cache instances of the contract resolver within your application for optimal performance."
        // See also https://stackoverflow.com/questions/33557737/does-json-net-cache-types-serialization-information
        static UntypedToTypedValueContractResolver instance;

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static UntypedToTypedValueContractResolver() { instance = new UntypedToTypedValueContractResolver(); }

        public static UntypedToTypedValueContractResolver Instance { get { return instance; } }

        protected override JsonDictionaryContract CreateDictionaryContract(Type objectType)
        {
            var contract = base.CreateDictionaryContract(objectType);

            if (contract.DictionaryValueType == typeof(object) && contract.ItemConverter == null)
            {
                contract.ItemConverter = new UntypedToTypedValueConverter();
            }

            return contract;
        }
    }
}
