using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UTC2_Student.MVVM.Converters
{
    public class DateConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? updateString = value.ToString();

            if (updateString == null)
            {
                return null;
            }

            DateTime dateTime = DateTime.ParseExact(updateString, "yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal);
            // Lấy định dạng của hệ thống
            string systemDateTimeFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.SortableDateTimePattern;

            // Chuyển đổi thành định dạng của hệ thống
            string formattedDateString = dateTime.ToString(systemDateTimeFormat);
            var r = formattedDateString.Split("T");
            return r[1] + " - " + r[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int year = (int)value;

            return new DateTime(year, 1, 1);
        }
    }
}
