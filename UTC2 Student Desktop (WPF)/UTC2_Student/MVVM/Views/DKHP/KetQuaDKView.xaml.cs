using System;
using System.Collections.Generic;
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
using UTC2_Student.MVVM.ViewModels.DKHP;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;

namespace UTC2_Student.MVVM.Views.DKHP
{
    /// <summary>
    /// Interaction logic for KetQuaDKView.xaml
    /// </summary>
    public partial class KetQuaDKView : UserControl
    {
        private KetQuaDKViewModel ketQuaDKViewModel;
        public KetQuaDKView()
        {
            InitializeComponent();
            ketQuaDKViewModel = new KetQuaDKViewModel();
            this.DataContext = ketQuaDKViewModel;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await ketQuaDKViewModel.GetDotDK();
            dotHocPhan.SelectedIndex = 0;
        }

        private async void dotHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Kiểm tra xem có mục nào được chọn không
            if (comboBox.SelectedItem != null)
            {
                var r = (DotHocPhan)comboBox.SelectedItem;
                await ketQuaDKViewModel.GetHocPhanDKTheoDot(r.iddot);
            }
        }
    }
}
