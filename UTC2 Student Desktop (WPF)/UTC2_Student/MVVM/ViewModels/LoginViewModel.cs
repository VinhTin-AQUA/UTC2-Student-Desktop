using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Stores;

namespace UTC2_Student.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool isShowed;

        public bool IsShowed
        {
            get { return isShowed; }
            set { isShowed = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            IsShowed = true;
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
        }

        private void ExecuteLoginCommand(object obj)
        {
            WindowStore.MainWindow.Left = WindowStore.LoginView.Left;
            WindowStore.MainWindow.Top = WindowStore.LoginView.Top;

            WindowStore.LoginView.Close();
            WindowStore.MainWindow.Show();
        }
    }
}
