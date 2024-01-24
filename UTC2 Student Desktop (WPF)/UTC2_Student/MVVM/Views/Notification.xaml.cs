﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using UTC2_Student.API;

namespace UTC2_Student.MVVM.Views
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        public Notification()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button? radioButton = sender as Button;

            if(radioButton != null )
            {
                // Mở trình duyệt mặc định để truy cập đường link
                Process.Start(new ProcessStartInfo(Urls.GetThongBaoWeb(radioButton.Tag.ToString()!))
                {
                    UseShellExecute = true
                });
            }
        }
    }
}
