using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Media;
using WpfUIAutomationProperties.MultiBinding;
using WpfUIAutomationProperties.Serialization;

namespace AutomationTest
{
    public abstract class TextBlockItemStatusTestBase : IItemStatusTestController
    {
        private static FontFamily ChangedFontFamily = new FontFamily("Arial");
        private static SolidColorBrush StartingBackground = new SolidColorBrush(Color.FromArgb(50, 0, 0, 255));
        private static SolidColorBrush ChangedBackground = new SolidColorBrush(Color.FromArgb(75, 0, 255, 0));
        
        public TextBlockItemStatusTestBase(
            string automationId
        )
        {
            AutomationId = automationId;
        }
        
        public string AutomationId { get;}
        public TextBlock TextBlock { get; set; } = new TextBlock();
        public bool IsDebug { get; set; }

        public virtual void Setup(StackPanel stackPanel)
        {
            Setup();
            stackPanel.Children.Add(TextBlock);
        }

        protected virtual void Setup()
        {
            TextBlock.Background = StartingBackground;
            DemoAttached.SetSomeAttached(TextBlock, 123);
            if (AutomationProperties.GetAutomationId(TextBlock) != null)
            {
                AutomationProperties.SetAutomationId(TextBlock, AutomationId);
            }
            AdditionalSetup();
        }

        public void Change()
        {
            TextBlock.Background = ChangedBackground;
            TextBlock.FontFamily = ChangedFontFamily;
        }

        public virtual void DebugSetup() { }
        

        protected virtual void AdditionalSetup()
        {

        }
        
        public virtual bool IsExpectedInitialItemStatus(string initialItemStatus)
        {
            Setup();
            

            if (IsDebug)
            {
                return DebugIsExpectedItemStatus(initialItemStatus, true);
            }
            else
            {
                return ReleaseIsExpectedItemStatus(initialItemStatus, true);
            }
        }

        protected abstract bool DebugIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus);
        protected virtual bool ReleaseIsExpectedItemStatus(string itemStatus, bool isInitialItemStatus)
        {
            return itemStatus == null;
        }

        public virtual bool IsExpectedChangedItemStatus(string changedItemStatus)
        {
            Change();

            if (IsDebug)
            {
                return DebugIsExpectedItemStatus(changedItemStatus, false);
            }
            else
            {
                return ReleaseIsExpectedItemStatus(changedItemStatus, false);
            }
        }
        
    }
}
