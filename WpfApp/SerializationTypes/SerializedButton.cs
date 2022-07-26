using System.Windows;
using System.Windows.Controls;

namespace AutomationTest
{
    public class SerializedButton
    {
        public bool IsCancel { get; set; }
        public ClickMode ClickMode { get; set; }
        public bool HasContent { get; set; }
        public FontWeight FontWeight { get; set; }
        
        public double ActualHeight { get; set; }
        public bool AllowDrop { get; set; }
    }
}
