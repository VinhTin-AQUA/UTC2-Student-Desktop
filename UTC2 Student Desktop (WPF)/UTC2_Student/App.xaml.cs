using System.Configuration;
using System.Data;
using System.Windows;
using UTC2_Student.MVVM.Views;

namespace UTC2_Student
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //LoginView loginView = new LoginView();
            //loginView.Show();

            MainWindow mainView = new MainWindow();
            mainView.Show();
        }
    }

}
