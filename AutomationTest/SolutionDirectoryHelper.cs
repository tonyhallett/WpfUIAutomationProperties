using System.IO;
using System.Linq;
using System.Reflection;

namespace AutomationTest
{
    public static class SolutionDirectoryHelper {
        public static string Get()
        {
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var dirInfo = new DirectoryInfo(assemblyDirectory);
            while (true)
            {
                if (dirInfo.GetFiles("*.sln").Any())
                {
                    break;
                }
                dirInfo = dirInfo.Parent;
            }
            return dirInfo.FullName;
        }
    }
}