using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.MVVM.Core;
using UTC2_Student.MVVM.ViewModels.DKHP;

namespace UTC2_Student.MVVM.ViewModels
{
    public class DKHPViewModel : ViewModelBase
    {
        private ViewModelBase currentChildView;

        public ViewModelBase CurrentChildCiew
        { 
            get { return currentChildView; } 
            set { currentChildView = value; OnPropertyChanged(); } 
        }

        public ICommand NavigateKQDK { get; set; }
        public ICommand NavigateChonMon { get; set; }
        public ICommand NavigateDK { get; set; }
        public ICommand NavigateHuyDK { get; set; }
        public ICommand NavigateHDSD { get; set; }


        public DKHPViewModel()
        {
            NavigateKQDK = new RelayCommand(ExecuteNavigateKQDK);
            NavigateChonMon = new RelayCommand(ExecuteNavigateChonMon);
            NavigateDK = new RelayCommand(ExecuteNavigateDK);
            NavigateHuyDK = new RelayCommand(ExecuteNavigateHuyDK);
            NavigateHDSD = new RelayCommand(ExecuteNavigateHDSD);

            //default child view
            ExecuteNavigateKQDK(null);
        }

        private void ExecuteNavigateKQDK(object obj)
        {
            CurrentChildCiew = new KetQuaDKViewModel();
        }

        private void ExecuteNavigateChonMon(object obj)
        {
            CurrentChildCiew = new ChonMonDKViewModel();
        }

        private void ExecuteNavigateDK(object obj)
        {
            CurrentChildCiew = new DangKyViewModel();
        }

        private void ExecuteNavigateHuyDK(object obj)
        {
            CurrentChildCiew = new HuyDangKyViewModel();
        }

        private void ExecuteNavigateHDSD(object obj)
        {
            CurrentChildCiew = new HuongDanDangKyViewModel();
        }
    }
}
