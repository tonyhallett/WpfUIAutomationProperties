using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Diagnostics;

namespace AutomationTest
{
    public class Appium
    {
        public const string Uri = "http://127.0.0.1:4723/wd/hub";
        private const string DefaultX86Path = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
        public static Process Start()
        {
            var processStartInfo = new ProcessStartInfo
            {
                UseShellExecute = true, // necessary if cross platform
                FileName = "appium"
            };

            return Process.Start(processStartInfo);
        }
        public static void StartWinAppDriver(string path = DefaultX86Path) => Process.Start(path, Uri);
        public static WindowsDriver CreateWindowsDriver(AppiumOptions appiumOptions) =>
            new WindowsDriver(new Uri(Uri), appiumOptions);

    }
}