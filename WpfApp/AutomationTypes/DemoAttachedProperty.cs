using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutomationTest
{
    internal class DemoAttached
    {
        public static int GetSomeAttached(DependencyObject obj)
        {
            return (int)obj.GetValue(SomeAttachedProperty);
        }

        public static void SetSomeAttached(DependencyObject obj, int value)
        {
            obj.SetValue(SomeAttachedProperty, value);
        }

        public static readonly DependencyProperty SomeAttachedProperty =
            DependencyProperty.RegisterAttached("SomeAttached", typeof(int), typeof(DemoAttached), new PropertyMetadata(0));


    }
}
