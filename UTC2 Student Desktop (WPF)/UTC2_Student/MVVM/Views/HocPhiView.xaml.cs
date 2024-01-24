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
using UTC2_Student.API.IntermediateModels.HocPhi;
using UTC2_Student.MVVM.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UTC2_Student.MVVM.Views
{
    /// <summary>
    /// Interaction logic for HocPhiView.xaml
    /// </summary>
    public partial class HocPhiView : UserControl
    {
        private HocPhiViewModel hocPhiModel;
        public HocPhiView()
        {
            InitializeComponent();
            hocPhiModel = new HocPhiViewModel();
            this.DataContext = hocPhiModel;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           await hocPhiModel.GetAllHocPhi();
        }
    }
}
