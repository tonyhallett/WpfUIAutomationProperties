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

        [TestCase(SerializeForMappedItemStatusTextBlockAutomationControl.AutomationIdentity)]
        [TestCase(ForItemStatusTextBlockAutomationControl.AutomationIdentity)]
        [TestCase(SerializeForCustomItemStatusTextBlockAutomationControl.AutomationIdentity)]

        [TestCase(StaticCtorItemStatusTextBlockDpsDictionaryAutomationControl.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockCDpsDictionaryAutomationControl.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockSerializedTypeAutomationControl.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockCustomSerializerAutomationControl.AutomationIdentity)]
        [TestCase(StaticCtorItemStatusTextBlockCustomSerializerForCallAutomationControl.AutomationIdentity)]

        [TestCase(MultiBindingDpsDictionaryAutomationControl.AutomationIdentity)]
        [TestCase(MultiBindingCDpsDictionaryAutomationControl.AutomationIdentity)]
        [TestCase(MultiBindingSerializedTypeAutomationControl.AutomationIdentity)]
        [TestCase(MultiBindingCustomSerializerDictionaryAutomationControl.AutomationIdentity)]
        [TestCase(MultiBindingCustomSerializerArgumentDictionaryAutomationControl.AutomationIdentity)]
        [TestCase(XamlAttachedConvertDependencyPropertiesTextBlockAutomationControl.AutomationIdentity)]
        [TestCase(XamlAttachedSerializedTypeTextBlockAutomationControl.AutomationIdentity)]

        [TestCase(SerializedTextBlockSerializeConvertAutomationControl.AutomationIdentity)]
        [TestCase(SerializedTextBlockPropertyDerivedTypeAutomationControl.AutomationIdentity)]
        [Apartment(ApartmentState.STA)]
        public void HasExpectedItemStatus(string automationId)
        {
            var appiumOptions = new AppiumOptions
            {
                App = GetAppPath()
            };
            appiumOptions.AddAppArguments(automationId);

            this.windowsDriver = new WindowsDriver(appiumOptions);

            var testItemStatus = ItemStatusAutomation.TestableAutomationControls()
                .Single(tis => tis.AutomationId == automationId);
            testItemStatus.IsDebug = isDebug;
            var element = windowsDriver.FindElement(MobileBy.AccessibilityId(automationId));

            void AssertItemStatus(Func<string, bool> expectation)
            {
                var itemStatus = element.GetAttribute("ItemStatus");
                Assert.That(expectation(itemStatus), Is.True);
            }
            
            AssertItemStatus(testItemStatus.IsExpectedInitialItemStatus);

            var changeBtn = windowsDriver.FindElement(MobileBy.AccessibilityId(Constants.ChangeButtonAutomationId));
            changeBtn.Click();

            AssertItemStatus(testItemStatus.IsExpectedChangedItemStatus);
        }
    }
}