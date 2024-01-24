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

namespace UTC2_Student.MVVM.Views.DKHP
{
    /// <summary>
    /// Interaction logic for DangKyView.xaml
    /// </summary>
    public partial class DangKyView : UserControl
    {
        private DangKyViewModel dataContext;
        public DangKyView()
        {
            InitializeComponent();
            dataContext = new DangKyViewModel();
            this.DataContext = dataContext;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            dataContext.IdHocPhanXoa.Add(checkBox.Tag.ToString());
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            dataContext.IdHocPhanXoa.Remove(checkBox.Tag.ToString());
        }
    }
}
