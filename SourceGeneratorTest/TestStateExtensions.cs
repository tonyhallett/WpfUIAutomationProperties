using Microsoft.CodeAnalysis.Testing;
using System.Text;

namespace SourceGeneratorTest
{
    public static class TestStateExtensions {
        public static void AddMsBuildCompilerVisibleProperties(this SolutionState solutionState, params (string Name, string Value)[] nameValues)
        {
            // todo - do you need to add this before each ?
            var stringBuilder = new StringBuilder($"is_global = true{Environment.NewLine}");
            foreach(var nameValue in nameValues)
            {
                stringBuilder.AppendLine($"build_property.{nameValue.Name} = {nameValue.Value}");
            }
            
            solutionState.AnalyzerConfigFiles.Add(("/.globalconfig", stringBuilder.ToString()));
        }
    }

}