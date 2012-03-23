using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace Medias
{
    class MilisecondsToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan time = TimeSpan.FromMilliseconds((int)value);
            return time.ToString("H:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Parse((string)value).TotalMilliseconds;
        }
    }
}
