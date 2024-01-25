using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.API.IntermediateModels.NotificationResponses;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.Auth;

namespace UTC2_Student.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region properties
        private bool isShowed;
        private ViewModelBase _currentChildView;
        private string userName;

        public bool IsShowed
        {
            get { return isShowed; }
            set { isShowed = value; OnPropertyChanged(); }
        }

        public ViewModelBase CurrntChildView
        {
            get { return _currentChildView; }
            set { _currentChildView = value; OnPropertyChanged(); }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        public ICommand NavigateNotifycationCommand {  get; set; }
        public ICommand NavigateDKHPCommand {  get; set; }
        public ICommand NavigateHocPhiCommand { get; set; }
        public ICommand NavigateLichThiCommand { get; set; }
        public ICommand NavigateKTXCommand { get; set; }
        public ICommand NavigateDiemCommand { get; set; }
        public ICommand NavigateTKBCommand { get; set; }
        public ICommand NavigateProfileCommand { get; set; }

        #endregion

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            IsShowed = true;
            NavigateNotifycationCommand = new RelayCommand(ExecuteNavigateNotifycationCommand);
            NavigateDKHPCommand = new RelayCommand(ExecuteNavigateDKHPCommand);
            NavigateHocPhiCommand = new RelayCommand(ExecuteNavigateHocPhiCommand);
            NavigateLichThiCommand = new RelayCommand(ExecuteNavigateHLichThiCommand);
            NavigateKTXCommand = new RelayCommand(ExecuteNavigateKTXCommand);
            NavigateDiemCommand = new RelayCommand(ExecuteNavigateDiemCommand);
            NavigateTKBCommand = new RelayCommand(ExecuteNavigateTKBCommand);
            NavigateProfileCommand = new RelayCommand(ExecuteNavigateProfileCommand);
            UserName = AuthModel.Instance!.result[0].hodem + AuthModel.Instance.result[0].ten;

            // default child view
            CurrntChildView = new NotificationViewModel();
        }

        // Execute

        #region commands

        private void ExecuteNavigateNotifycationCommand(object obj)
        {
            CurrntChildView = new NotificationViewModel();
        }

        private void ExecuteNavigateDKHPCommand(object obj)
        {
            CurrntChildView = new DKHPViewModel();
        }

        private void ExecuteNavigateHocPhiCommand(object obj)
        {
            CurrntChildView = new HocPhiViewModel();
        }

        private void ExecuteNavigateHLichThiCommand(object obj)
        {
            CurrntChildView = new LichThiViewModel();
        }

        private void ExecuteNavigateKTXCommand(object obj)
        {
            CurrntChildView = new KTXViewModel();
        }

        private void ExecuteNavigateDiemCommand(object obj)
        {
            CurrntChildView = new DiemViewModel();
        }

        private void ExecuteNavigateTKBCommand(object obj)
        {
            CurrntChildView = new TKBViewModel();
        }

        private void ExecuteNavigateProfileCommand(object obj)
        {
            CurrntChildView = new ProfileViewModel();
        }
        #endregion
    }
}
