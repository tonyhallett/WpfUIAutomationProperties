using OpenQA.Selenium.Appium;

namespace AutomationTest
{
    internal static class WindowsAppiumOptionsExtensions
    {
        public static AppiumOptions AddAppArguments(this AppiumOptions appiumOptions, string arguments)
        {
            appiumOptions.AddAdditionalAppiumOption("appArguments", arguments);
            return appiumOptions;
        }
    }
}