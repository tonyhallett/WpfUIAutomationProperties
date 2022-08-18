using System.Linq;
using System.Windows;

namespace AutomationTest
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IItemStatusTestController controller;

        public MainWindow(string[] args)
        {
            InitializeComponent();

            var automationId = args[0];
            controller = ItemStatusAutomation.Controllers()
                .First(controller => controller.AutomationId == automationId);

            controller.IsDebug = ApplyWhenDebugOnly.Apply;
            controller.Setup(stackPanel);
            
            if (ApplyWhenDebugOnly.Apply)
            {
                controller.DebugSetup();
            }
            
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.Change();
        }

    }
}
