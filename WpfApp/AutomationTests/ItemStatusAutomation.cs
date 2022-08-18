using System.Collections.Generic;
using System.Linq;

namespace AutomationTest
{
    public static class ItemStatusAutomation {
        internal static List<IItemStatusTestController> Controllers() => new List<IItemStatusTestController>
        {
            new SerializeForMappedItemStatusTextBlockTest(),
            new ForItemStatusTextBlockTest(),
            new SerializeForCustomItemStatusTextBlockTest(),

            new StaticCtorItemStatusTextBlockDpsDictionaryTest(),
            new StaticCtorItemStatusTextBlockCDpsDictionaryTest(),
            new StaticCtorItemStatusTextBlockSerializedTypeTest(),
            new StaticCtorItemStatusTextBlockCustomSerializerTest(),
            new StaticCtorItemStatusTextBlockCustomSerializerForCallTest(),

            new MultiBindingDpsTest(),
            new MultiBindingCDpsTest(),
            new MultiBindingSerializedTypeTest(),
            new MultiBindingCustomSerializerTest(),
            new MultiBindingCustomSerializerArgumentTest(),
            new XamlAttachedConvertDependencyPropertiesTest(),
            new XamlAttachedSerializedTypeTest(),

            new SerializedTextBlockSerializeConvertTest(),
            new SerializedTextBlockPropertyDerivedTypeTest(),
            new SerializedTypeAttachedPropertyTest()
        };
        public static List<IItemStatusTest> ItemStatusTests() => Controllers().Cast<IItemStatusTest>().ToList();
    }
}
