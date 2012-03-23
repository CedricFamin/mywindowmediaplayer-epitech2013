using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace MWMP.Models.Converter
{
    class MilisecondsToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan time = TimeSpan.FromMilliseconds((int)value);
            return time.ToString("h':'mm':'ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan time;
            if (TimeSpan.TryParse((string)value, out time))
                return time.TotalMilliseconds;
            return 0;
        }
    }
}
