using Microsoft.CodeAnalysis;
using System.Linq;

namespace SerializedTypeSourceGenerator
{
    public static class WpfAutomationProperties
    {
        public static void Initialize(Compilation compilation)
        {
            var wpfUIAutomationPropertiesMetadataReference = compilation.ExternalReferences.SingleOrDefault(mdr => mdr.Display.EndsWith("WpfUIAutomationProperties.dll"));

            if (wpfUIAutomationPropertiesMetadataReference != null)
            {
                wpfAutomationPropertiesAssemblySymbol = compilation.GetAssemblyOrModuleSymbol(wpfUIAutomationPropertiesMetadataReference) as IAssemblySymbol;
            }
        }

        private static IAssemblySymbol wpfAutomationPropertiesAssemblySymbol;
        private static INamedTypeSymbol iserializeConvertSymbol;
        public static INamedTypeSymbol ISerializeConvertSymbol
        {
            get
            {
                if (iserializeConvertSymbol == null && wpfAutomationPropertiesAssemblySymbol != null)
                {
                    iserializeConvertSymbol = wpfAutomationPropertiesAssemblySymbol.GetTypeByMetadataName("WpfUIAutomationProperties.Serialization.ISerializeConvert`1");
                }
                return iserializeConvertSymbol;
            }
        }
    }
}
