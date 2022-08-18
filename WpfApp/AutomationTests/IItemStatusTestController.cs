using System.Windows.Controls;

namespace AutomationTest
{
    public interface IItemStatusTestController : IItemStatusTest
    {
        void Setup(StackPanel panel);
        void DebugSetup();
        void Change();
    }
}
