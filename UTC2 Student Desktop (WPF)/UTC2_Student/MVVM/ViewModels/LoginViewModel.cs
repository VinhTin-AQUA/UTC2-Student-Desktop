﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.API;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;
using UTC2_Student.Stores;

namespace UTC2_Student.MVVM.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private bool isShowed;
        private string mssv;
        private string password;
        private string errorMessage;
        private bool canLogin;


        public bool IsShowed
        {
            get { return isShowed; }
            set { isShowed = value; OnPropertyChanged(); }
        }

        public string MSSV
        {
            get { return mssv; }
            set { mssv = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(); }
        }

        public bool CanLogin
        {
            get { return canLogin; }
            set { canLogin = value; OnPropertyChanged(); }
        }


        public ICommand LoginCommand { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public LoginViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            IsShowed = true;
            CanLogin = true;
            LoginCommand = new AsyncRelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private async Task ExecuteLoginCommand(object obj)
        {
            CanLogin = false;
            if (DataHelper.CheckForInternetConnection() == false)
            {
                ErrorMessage = "Vui lòng kết nối Enternet !!!";
                return;
            } 
            var response =  await ApiRepository.Ins.Login(MSSV, Password);
            if(response == null)
            {
                ErrorMessage = "Vui lòng kết nối Enternet !!!";
                return;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ErrorMessage = "Tài khoảng hoặc mật khẩu không chính xác";
            }
            else
            {
                WindowStore.MainWindow!.Left = WindowStore.LoginView!.Left;
                WindowStore.MainWindow.Top = WindowStore.LoginView.Top;
                WindowStore.MainWindow.Show();
                WindowStore.LoginView.Close();
                WindowStore.LoginView = null;
            }
            CanLogin = true;
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return
                (string.IsNullOrEmpty(MSSV) != true &&
                string.IsNullOrEmpty(Password) != true);
        }
    }
}
