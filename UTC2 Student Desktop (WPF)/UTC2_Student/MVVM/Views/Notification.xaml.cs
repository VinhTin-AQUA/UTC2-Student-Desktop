using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UTC2_Student.API;

namespace UTC2_Student.MVVM.Views
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        public Notification()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button radioButton = sender as Button;

            if(radioButton != null )
            {
                // Mở trình duyệt mặc định để truy cập đường link
                Process.Start(new ProcessStartInfo(Urls.GetThongBaoWeb(radioButton.Tag.ToString()))
                {
                    UseShellExecute = true
                });
            }
        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string updateString = value.ToString();

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
