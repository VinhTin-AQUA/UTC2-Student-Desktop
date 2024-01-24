using System;
using System.Collections.Generic;
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
using UTC2_Student.MVVM.ViewModels.DKHP;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;

namespace UTC2_Student.MVVM.Views.DKHP
{
    /// <summary>
    /// Interaction logic for HuyDangKyView.xaml
    /// </summary>
    public partial class HuyDangKyView : UserControl
    {
        private HuyDangKyViewModel dataContext;

        public HuyDangKyView()
        {
            InitializeComponent();
            dataContext = this.DataContext as HuyDangKyViewModel;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //await dataContext.GetDotDK();
            dotHocPhan.SelectedIndex = 0;
        }

        private async void dotHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Kiểm tra xem có mục nào được chọn không
            if (comboBox.SelectedItem != null)
            {
                var r = (DotHocPhan)comboBox.SelectedItem;
                await dataContext.GetHocPhanDKTheoDot(r.iddot);
            }
        }
    }
}
