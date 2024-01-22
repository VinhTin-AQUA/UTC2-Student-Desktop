using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using UTC2_Student.API;
using UTC2_Student.MVVM.Views;
using UTC2_Student.Stores;

namespace UTC2_Student
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Init();
            WindowStore.LoginView.Show();
            //WindowStore.MainWindow.Show();
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
    }

}
