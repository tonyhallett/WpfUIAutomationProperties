using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfUIAutomationProperties
{
    internal class FuncValueConverter : IValueConverter
    {
        private readonly Func<object, object> converter;

        public FuncValueConverter(Func<object,object> converter)
        {
            this.converter = converter;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.converter(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
