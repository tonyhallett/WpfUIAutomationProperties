using System.Collections.Generic;
using System.Linq;

namespace AutomationTest
{
    public static class ItemStatusAutomation {
        internal static List<ITestableAutomationControl> Controllers() => new List<ITestableAutomationControl>
        {
            new SerializeForMappedItemStatusTextBlockAutomationControl(),
            new ForItemStatusTextBlockAutomationControl(),
            new SerializeForCustomItemStatusTextBlockAutomationControl(),

            new StaticCtorItemStatusTextBlockDpsDictionaryAutomationControl(),
            new StaticCtorItemStatusTextBlockCDpsDictionaryAutomationControl(),
            new StaticCtorItemStatusTextBlockSerializedTypeAutomationControl(),
            new StaticCtorItemStatusTextBlockCustomSerializerAutomationControl(),
            new StaticCtorItemStatusTextBlockCustomSerializerForCallAutomationControl(),

            new MultiBindingDpsDictionaryAutomationControl(),
            new MultiBindingCDpsDictionaryAutomationControl(),
            new MultiBindingSerializedTypeAutomationControl(),
            new MultiBindingCustomSerializerDictionaryAutomationControl(),
            new MultiBindingCustomSerializerArgumentDictionaryAutomationControl(),
            new XamlAttachedConvertDependencyPropertiesTextBlockAutomationControl(),
            new XamlAttachedSerializedTypeTextBlockAutomationControl(),

            new SerializedTextBlockSerializeConvertAutomationControl(),
            new SerializedTextBlockPropertyDerivedTypeAutomationControl()
        };
        public static List<ITestItemStatus> TestableAutomationControls() => Controllers().OfType<ITestItemStatus>().ToList();
    }
}
