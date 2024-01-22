using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Windows;
using UTC2_Student.API;
using UTC2_Student.MVVM.Views;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.Auth;
using UTC2_Student.Stores;

namespace UTC2_Student
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            //Init();
            //if(await CheckLogin() == false)
            //{
            //    WindowStore.LoginView.Show();
            //}
            //else
            //{
            //    WindowStore.MainWindow.Show();
            //}

            WindowStore.MainWindow.Show();
        }

        private void Init()
        {
            if(Directory.Exists(DataHelper.DataDirectory) == false)
            {
                Directory.CreateDirectory(DataHelper.DataDirectory);
            } 

            if(File.Exists(DataHelper.AccountDataPath) == false)
            {
                File.Create(DataHelper.AccountDataPath);
            }

            if (File.Exists(DataHelper.AuthModelDataPath) == false)
            {
                File.Create(DataHelper.AuthModelDataPath);
            }
        }

        private async Task<bool> CheckLogin()
        {
            DataHelper.ReadAccount();
            DataHelper.ReadAuthModel();

            if(LoginModel.Instance.MSSV == "" ||
                LoginModel.Instance.Password == "" ||
                AuthModel.Instance.result == null ||
                AuthModel.Instance.v == null)
            {
                return false;
            }

            if (DataHelper.CheckForInternetConnection() == false)
            {
                return false;
            }

            var response = await ApiRepository.Ins.Login(LoginModel.Instance.MSSV, LoginModel.Instance.Password);

            if (response == null)
            {
                return false;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            return true;
        }
    }
}
