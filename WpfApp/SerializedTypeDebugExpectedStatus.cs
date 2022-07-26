using System.Windows.Controls;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public static class SerializedTypeDebugExpectedStatus
    {
        public static bool IsExpected(TextBlock textBlock, string itemStatus,IItemStatusSerializer serializer)
        {
            var serializedTextBlock = serializer.Deserialize<SerializedTextBlock>(itemStatus);
            var simpleSame = serializedTextBlock.FontSize == textBlock.FontSize
            && serializedTextBlock.SomeAttached == DemoAttached.GetSomeAttached(textBlock)
            && serializedTextBlock.FontFamily.Source == textBlock.FontFamily.Source;
            var brushTypesEqual = serializedTextBlock.Background.GetType() == textBlock.Background.GetType();
            var serializedColor = serializedTextBlock.Background.ToString();
            var expectedColor = textBlock.Background.ToString();
            var colorEqual = serializedColor == expectedColor;
            return brushTypesEqual && colorEqual && simpleSame;
        }
    }
}
