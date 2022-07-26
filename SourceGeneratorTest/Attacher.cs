namespace SourceGeneratorTest
{
    public class Attacher
    {
        public const string Code = @"
namespace AttacherNamespace
{
    using System.Windows;

    public class Attacher
    {
        public static int GetMyProperty(DependencyObject obj)
        {
            return (int)obj.GetValue(MyPropertyProperty);
        }

        public static void SetMyProperty(DependencyObject obj, int value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached(""MyProperty"", typeof(int), typeof(Attacher), new PropertyMetadata(0));

        public static int GetMyProperty2(DependencyObject obj)
        {
            return (int)obj.GetValue(MyProperty2Property);
        }

        public static void SetMyProperty2(DependencyObject obj, int value)
        {
            obj.SetValue(MyProperty2Property, value);
        }

        public static readonly DependencyProperty MyProperty2Property =
            DependencyProperty.RegisterAttached(""MyProperty2"", typeof(int), typeof(Attacher), new PropertyMetadata(0));

    }
}";

    }
}