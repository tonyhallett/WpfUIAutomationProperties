using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfUIAutomationProperties.MultiBinding
{
    internal class SerializingItemStatusConverter : IMultiValueConverter
    {
        private readonly Func<Dictionary<string,object>, object> converter;
        private readonly Func<object, string> serializer;

        public SerializingItemStatusConverter(
            Func<Dictionary<string,object>,object> converter,
            Func<object,string> serializer
        )
        {
            this.converter = converter;
            this.serializer = serializer;
        }
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var namedStatuses = values.Select(v => ((string propertyName, object value))v).ToDictionary(t => t.propertyName, t=> t.value);
            return serializer(converter(namedStatuses));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
