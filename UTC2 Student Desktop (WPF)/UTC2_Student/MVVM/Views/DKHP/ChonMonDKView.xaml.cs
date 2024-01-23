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
    /// Interaction logic for ChonMonDKView.xaml
    /// </summary>
    public partial class ChonMonDKView : UserControl
    {
        private ChonMonDKViewModel chonMonDKViewModel;
        
        public ChonMonDKView()
        {
            InitializeComponent();
             chonMonDKViewModel = new ChonMonDKViewModel();
            this.DataContext = chonMonDKViewModel;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await chonMonDKViewModel.GetDSMonHoc();
            lopHocPhan.SelectedIndex = 0;
        }

        private async void lopHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;

            if (combo != null)
            {
                chonMonDKViewModel.Reset();
                MonHoc monHoc = combo.SelectedItem as MonHoc;
                await chonMonDKViewModel.GetLopHocPhanByMonHoc(monHoc.iD_MONHOC);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if(check != null)
            {
                chonMonDKViewModel.SoLopHocPhanDaChonMoiMon++;
                if(chonMonDKViewModel.SoLopHocPhanDaChonMoiMon > 2)
                {
                    check.IsChecked = false;
                    chonMonDKViewModel.SoLopHocPhanDaChonMoiMon = 2;
                } else
                {
                    chonMonDKViewModel.IdHocPhans[chonMonDKViewModel.SoLopHocPhanDaChonMoiMon - 1].id = check.Tag.ToString();
                    chonMonDKViewModel.IdHocPhans[chonMonDKViewModel.SoLopHocPhanDaChonMoiMon - 1].name = check.Content.ToString();
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            chonMonDKViewModel.SoLopHocPhanDaChonMoiMon--;
            if(chonMonDKViewModel.SoLopHocPhanDaChonMoiMon < 0)
            {
                chonMonDKViewModel.SoLopHocPhanDaChonMoiMon = 0;
            } 
            else
            {
                chonMonDKViewModel.IdHocPhans[chonMonDKViewModel.SoLopHocPhanDaChonMoiMon].id = "";
                chonMonDKViewModel.IdHocPhans[chonMonDKViewModel.SoLopHocPhanDaChonMoiMon].name = "";
            }
        }
    }
}
