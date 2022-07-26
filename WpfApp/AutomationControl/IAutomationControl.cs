using System.Windows.Controls;

namespace AutomationTest
{
    public interface IAutomationControl {
        void Setup(StackPanel panel);
        void DebugSetup();
        void Change();
    }
}
