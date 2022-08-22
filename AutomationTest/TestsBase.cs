using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AutomationTest
{


    /* 
        Cannot use
        [TestFixture(true)]
        [TestFixture(false)]

        but getting "The calling thread cannot access this object because a different thread owns it."
        when [TestFixture(true)] - debug is run at the same time as [TestFixture(false)]
        run alone there is no issue....
    */

    public abstract class TestsBase
    {
        protected readonly bool isDebug;
        private Process appiumProcess;
        private WindowsDriver windowsDriver;

        public TestsBase(bool isDebug)
        {
            this.isDebug = isDebug;
        }


        [SetUp]
        public void Setup()
        {
            appiumProcess = Appium.Start();
            Appium.StartWinAppDriver();
        }

        [TearDown]
        public void TearDown()
        {
            if (this.windowsDriver != null)
            {
                this.windowsDriver.Quit();
            }

            if (!this.appiumProcess.HasExited)
            {
                _ = this.appiumProcess.CloseMainWindow();
            }
        }

        private string GetAppPath()
        {
#if NET472
            var netFramework = true;
#else
            var netFramework = false;
#endif
            var targetFramework = netFramework ? "net472" : "net6.0-windows";
            return CrossPlatformExeFinder.Find(isDebug, "WpfApp", targetFramework);
        }

        [TestCase(SerializeForMappedItemStatusTextBlockTest.AutomationIdentity)]
        [TestCase(ForItemStatusTextBlockTest.AutomationIdentity)]
        [TestCase(SerializeForCustomItemStatusTextBlockTest.AutomationIdentity)]

        [TestCase(StaticCtorItemStatusTextBlockDpsDictionaryTest.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockCDpsDictionaryTest.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockSerializedTypeTest.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockCustomSerializerTest.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockCustomSerializerForCallTest.AutomationIdentity)]

        [TestCase(MultiBindingDpsTest.AutomationIdentity)]
        [TestCase(MultiBindingCDpsTest.AutomationIdentity)]
        [TestCase(MultiBindingSerializedTypeTest.AutomationIdentity)]
        [TestCase(MultiBindingCustomSerializerTest.AutomationIdentity)]
        [TestCase(MultiBindingCustomSerializerArgumentTest.AutomationIdentity)]
        [TestCase(XamlAttachedConvertDependencyPropertiesTest.AutomationIdentity)]
        [TestCase(XamlAttachedSerializedTypeTest.AutomationIdentity)]

        [TestCase(SerializedTextBlockSerializeConvertTest.AutomationIdentity)]
        [TestCase(SerializedTextBlockPropertyDerivedTypeTest.AutomationIdentity)]
        [TestCase(SerializedTypeAttachedPropertyTest.AutomationIdentity)]
        [Apartment(ApartmentState.STA)]
        public void HasExpectedItemStatus(string automationId)
        {
            var appiumOptions = new AppiumOptions
            {
                App = GetAppPath()
            };
            appiumOptions.AddAppArguments(automationId);

            this.windowsDriver = new WindowsDriver(appiumOptions);

            var testItemStatus = ItemStatusAutomation.ItemStatusTests()
                .Single(tis => tis.AutomationId == automationId);
            testItemStatus.IsDebug = isDebug;
            var element = windowsDriver.FindElement(MobileBy.AccessibilityId(automationId));

            void AssertItemStatus(Func<string, bool> expectation,bool isFirst)
            {
                var itemStatus = element.GetAttribute("ItemStatus");
                
                Assert.That(expectation(itemStatus), Is.True, () =>
                {
                    var firstOrChange = isFirst ? "First" : "Change";
                    var debugOrRelease = isDebug ? "Debug" : "Release";
                    return $"Unexpected item status in {debugOrRelease} mode,  for {firstOrChange} : {itemStatus}";
                });
            }
            
            AssertItemStatus(testItemStatus.IsExpectedInitialItemStatus, true);

            var changeBtn = windowsDriver.FindElement(MobileBy.AccessibilityId(Constants.ChangeButtonAutomationId));
            changeBtn.Click();

            AssertItemStatus(testItemStatus.IsExpectedChangedItemStatus, false);
        }
    }
}