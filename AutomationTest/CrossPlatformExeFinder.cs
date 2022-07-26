using System.IO;

namespace AutomationTest
{
    public static class CrossPlatformExeFinder {
        public static string Find(bool isDebug, string appName,string targetFramework)
        {
            var configuration = isDebug ? "Debug" : "Release";
            return Path.Combine(SolutionDirectoryHelper.Get(), appName, "bin", configuration, targetFramework, $"{appName}.exe");
        }

    }
}