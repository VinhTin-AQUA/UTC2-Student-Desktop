using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UTC2_Student.MVVM.Converters
{
    public class TKBConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Không có";
            }

            string? updateString = value.ToString();
            if (updateString == null)
            {
                return null;
            }
            updateString = updateString.Replace("&rarr;", "→");
            updateString = updateString.Replace("<br>", "");
            updateString = updateString.Replace("</br>", "");
            updateString = updateString.Replace("<b>", "");
            updateString = updateString.Replace("</b>", "");
            updateString = updateString.Replace("&emsp;", "  ");
            return updateString;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? updateString = value.ToString();

            return updateString;
        }
    }
}
