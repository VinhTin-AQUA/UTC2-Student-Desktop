using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UTC2_Student.MVVM.Converters
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal updateString = System.Convert.ToDecimal(value);

            if (updateString == null)
            {
                return null;
            }
            string formattedNumber = string.Format("{0:N0}", updateString) + " VNĐ";
            return formattedNumber;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDecimal(value);
        }
    }
}
