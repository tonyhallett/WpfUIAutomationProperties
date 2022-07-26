using Microsoft.CodeAnalysis.Testing;
using SerializedTypeSourceGenerator;

namespace SourceGeneratorTest
{
    public static class SerializedTypeGeneratorMsBuildOptions
    {
        public static void AddPropertiesNotFoundBehaviour(SolutionState testState, string value)
        {
            testState.AddMsBuildCompilerVisibleProperties(
                (PropertiesNotFoundBehaviourProvider.MsBuildPropertyName, value)
            );
        }
    }
}