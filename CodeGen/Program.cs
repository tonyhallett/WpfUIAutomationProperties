using CodeGen;
using System.Reflection;
using WpfUIAutomationProperties.Serialization;

static string GetWpfUIAutomationPropertiesDirectory()
{
    var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    var dirInfo = new DirectoryInfo(assemblyDirectory!);
    while (true)
    {
        if (dirInfo.GetFiles("*.sln").Any())
        {
            break;
        }
        dirInfo = dirInfo.Parent!;
    }
    var directoryPath =  Path.Combine(dirInfo.FullName, "WpfUIAutomationProperties");
    if (!Directory.Exists(directoryPath))
    {
        throw new Exception("WpfUIAutomationProperties does not exist");
    }
    return directoryPath;
}


var wpfUIAutomationPropertiesDirectory = GetWpfUIAutomationPropertiesDirectory();
var staticItemStatusDirectoryPath = Path.Combine(wpfUIAutomationPropertiesDirectory, "StaticItemStatus");
StaticItemStatusControlsWriter.Write<ItemStatusTypedDictionarySerializer>(
    staticItemStatusDirectoryPath,
    Path.Combine(staticItemStatusDirectoryPath,"FrameworkElements"),
    Path.Combine(wpfUIAutomationPropertiesDirectory, "AutomationPeers")
);

//if (!Analyzer.ShouldProceed())
//{
//    throw new Exception("Cannot proceed");
//}

/*
    Not generating as have written the Source Generator and that there is an
    issue in that there is a slight difference between .Net Framework and .Net Core
    one of the types has an additional property in .Net Core.
    Also this is currently set up to include Brush properties.
*/
//var frameworkElementSerializedTypesDirectoryPath = Path.Combine(wpfUIAutomationPropertiesDirectory, "Serialization", "FrameworkElementSerializedTypes");
//FrameworkElementSerializeObjectWriter.Write(frameworkElementSerializedTypesDirectoryPath, Namespaces.FrameworkElementTypes);


