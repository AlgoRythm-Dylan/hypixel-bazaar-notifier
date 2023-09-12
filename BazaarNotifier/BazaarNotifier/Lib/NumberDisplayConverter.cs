using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib
{
    public class NumberDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string format;
            if(parameter == null)
            {
                format = "n2";
            }
            else
            {
                format = parameter.ToString();
            }
            if(value is int)
            {
                return ((int)value).ToString(format);
            }
            else if(value is long)
            {
                return ((long)value).ToString(format);
            }
            else if (value is double)
            {
                return ((double)value).ToString(format);
            }
            else if (value is float)
            {
                return ((float)value).ToString(format);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
