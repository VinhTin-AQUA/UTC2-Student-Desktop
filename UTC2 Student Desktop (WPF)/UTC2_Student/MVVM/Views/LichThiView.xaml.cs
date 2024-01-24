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
using UTC2_Student.MVVM.ViewModels;

namespace UTC2_Student.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LichThiView.xaml
    /// </summary>
    public partial class LichThiView : UserControl
    {
        private LichThiViewModel? lichThiViewModel = null;
        public LichThiView()
        {
            InitializeComponent();
            lichThiViewModel = new LichThiViewModel();
            this.DataContext = lichThiViewModel;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await lichThiViewModel!.GetLichThiByHocKy();
        }
    }
}
