using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.MVVM.Views;

namespace UTC2_Student.Stores
{
    public static class WindowStore
    {
        private static MainWindow? mainWinDow = null;
        private static LoginView? loginView = null;

        public static MainWindow MainWindow
        { 
            get 
            {
                if (mainWinDow == null)
                {
                    mainWinDow = new MainWindow();
                }
                return mainWinDow; 
            } 
        }

        public static LoginView LoginView
        {
            get
            {
                if(loginView == null)
                {
                    loginView = new LoginView();
                }
                return loginView;
            }
        }
    }
}
