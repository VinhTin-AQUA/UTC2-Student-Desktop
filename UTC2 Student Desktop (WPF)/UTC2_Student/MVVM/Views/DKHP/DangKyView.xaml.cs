﻿using System;
using System.Collections.Generic;
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
using UTC2_Student.MVVM.ViewModels.DKHP;

namespace UTC2_Student.MVVM.Views.DKHP
{
    /// <summary>
    /// Interaction logic for DangKyView.xaml
    /// </summary>
    public partial class DangKyView : UserControl
    {
        public DangKyView()
        {
            InitializeComponent();
            DangKyViewModel dataContext = new DangKyViewModel();
            this.DataContext = dataContext;
        }
    }
}
