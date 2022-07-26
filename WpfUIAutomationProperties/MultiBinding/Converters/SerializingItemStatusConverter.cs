using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfUIAutomationProperties.MultiBinding
{
    internal class SerializingItemStatusConverter : IMultiValueConverter
    {
        private readonly Func<IEnumerable<(string propertyName, object value)>, object> converter;
        private readonly Func<object, string> serializer;

        public SerializingItemStatusConverter(
            Func<IEnumerable<(string propertyName, object value)>,object> converter,
            Func<object,string> serializer
        )
        {
            this.converter = converter;
            this.serializer = serializer;
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var namedStatuses = values.Select(v => ((string propertyName, object value))v);
            return serializer(converter(namedStatuses));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
