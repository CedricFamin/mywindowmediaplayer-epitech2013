using System;
using System.Globalization;
using System.Windows.Data;

namespace MWMP.Models.Converter
{
    class TimeSpanToMilisecondConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(System.Convert.ToDouble(value));
            return time;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            return ((TimeSpan)value).TotalMilliseconds;
        }
    }
}
