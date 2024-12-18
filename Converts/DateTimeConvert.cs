using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace UIDesign.Converts
{
    public class DateTimeConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime dateTime))
            {
                return dateTime;
            }
            return value;
        }
    }
}
