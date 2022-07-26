using System.Windows.Controls;
using System.Linq;
using System.Windows.Automation;

namespace AutomationTest
{
    public abstract class XamlTextBlockAutomationControl : TextBlockAutomationControl
    {
        public XamlTextBlockAutomationControl(string automationId) : base(automationId) { }
        
        public override void Setup(StackPanel stackPanel)
        {
            TextBlock = stackPanel.Children.OfType<TextBlock>().First(tb => AutomationProperties.GetAutomationId(tb) == AutomationId);
            Setup();
        }
    }
}
