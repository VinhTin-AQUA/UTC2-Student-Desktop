using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.API;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories.IntermediateModels.Auth;
using UTC2_Student.Stores;

namespace UTC2_Student.MVVM.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private AuthModel authModel;
        private string hoTen;
        private string mA_LOP;
        private string mA_SINHVIEN;
        private string ngaY_SINH;
        private string email;
        private string dieN_THOAI_DD;
        private string khicaN_BANTINCHO_AI_DIACHI;

        public AuthModel AuthModel { get { return authModel; } set {  authModel = value; OnPropertyChanged(); } }

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; OnPropertyChanged(); }
        }

        public string Ma_Lop
        {
            get { return mA_LOP; }
            set { mA_LOP = value; OnPropertyChanged(); }
        }

        public string MA_SINHVIEN
        {
            get { return mA_SINHVIEN; }
            set { mA_SINHVIEN = value; OnPropertyChanged(); }
        }

        public string NgaY_SINH
        {
            get { return ngaY_SINH; }
            set { ngaY_SINH = value; OnPropertyChanged(); }
        }

        public string DieN_THOAI_DD
        {
            get { return dieN_THOAI_DD; }
            set { dieN_THOAI_DD = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        public string KhicaN_BANTINCHO_AI_DIACHI
        {
            get { return khicaN_BANTINCHO_AI_DIACHI; }
            set { khicaN_BANTINCHO_AI_DIACHI = value; OnPropertyChanged(); }
        }

        public ICommand DangXuatCommand { get; set; }
        public ICommand NavigaeDoiMKCommand { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProfileViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            AuthModel = AuthModel.Instance!;
            HoTen = AuthModel.Instance!.result[0].hodem! + " " + AuthModel.Instance!.result[0].ten!;
            Ma_Lop = AuthModel.Instance!.result[0].mA_LOP!;
            MA_SINHVIEN = AuthModel.Instance!.result[0].mA_SINHVIEN!;
            NgaY_SINH = AuthModel.Instance!.result[0].ngaY_SINH!;
            DieN_THOAI_DD =AuthModel.Instance!.result[0].email!;
            Email = AuthModel.Instance!.result[0].dieN_THOAI_DD!;
            KhicaN_BANTINCHO_AI_DIACHI = AuthModel.Instance!.result[0].khicaN_BANTINCHO_AI_DIACHI!;

            DangXuatCommand = new RelayCommand(ExecuteDangXuatCommand);
            NavigaeDoiMKCommand = new RelayCommand(ExecuteNavigaeDoiMKCommand);
        }

        private void ExecuteDangXuatCommand(object obj)
        {
            var r = System.Windows.MessageBox.Show("Bạn có muốn đăng xuất","Đăng xuất", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Question);

            if( r == System.Windows.MessageBoxResult.Cancel )
            {
                return;
            }

            DataHelper.ClearAccount();
            DataHelper.ClearAuthModel();

            WindowStore.LoginView!.Left = WindowStore.MainWindow!.Left;
            WindowStore.LoginView.Top = WindowStore.MainWindow.Top;
            WindowStore.LoginView.Width = WindowStore.MainWindow.Width;
            WindowStore.LoginView.Height = WindowStore.MainWindow.Height;
            WindowStore.LoginView.Show();
            WindowStore.MainWindow.Close();
            WindowStore.MainWindow = null;
        }

        private void ExecuteNavigaeDoiMKCommand(object obj)
        {
            System.Windows.MessageBox.Show("Chức năng này chưa hoàn thiện.");
        }
    }
}
