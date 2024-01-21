using System.Configuration;
using System.Data;
using System.Windows;
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
            //WindowStore.LoginView.Show();
            WindowStore.MainWindow.Show();
        }
    }

}
