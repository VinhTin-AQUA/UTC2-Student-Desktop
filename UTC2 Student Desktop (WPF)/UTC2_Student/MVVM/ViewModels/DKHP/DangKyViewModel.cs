using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using UTC2_Student.API;
using UTC2_Student.API.IntermediateModels.ApiResponses;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;
using UTC2_Student.Repositories.IntermediateModels.Auth;
using UTC2_Student.Stores;

namespace UTC2_Student.MVVM.ViewModels.DKHP
{
    public class DangKyViewModel : ViewModelBase
    {
        private ObservableCollection<HocPhanDaChon> hocPhans;
        private List<string> idHocPhanXoa;

        public ObservableCollection<HocPhanDaChon> HocPhans
        {
            get { return hocPhans; }
            set { hocPhans = value; OnPropertyChanged(); }
        }


        public List<string> IdHocPhanXoa
        {
            get { return idHocPhanXoa; }
            set { idHocPhanXoa = value; OnPropertyChanged(); }
        }

        public ICommand DangKyCommand { get; set; }
        public ICommand XoaIdHocPhanCommand { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DangKyViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _ReadIdHocPhan();
            IdHocPhanXoa = new List<string>();
            HocPhanDaChonStore.HocPhans = HocPhans;
            DangKyCommand = new AsyncRelayCommand(ExecuteDangKyCommand);
            XoaIdHocPhanCommand = new RelayCommand(ExecuteXoaIdHocPhanCommand);
        }

        public async void _ReadIdHocPhan()
        {
            HocPhans = await DataHelper.ReadIdHocPhans();
        }

        private async Task ExecuteDangKyCommand(object obj)
        {
            if(HocPhans.Count() < 0)
            {
                return;
            }
            var ids = HocPhans.Select(h => h.Id).ToList();
            await MultiTaskSendAPI.Ins.DangKy(ids);
        }

        private void ExecuteXoaIdHocPhanCommand(object obj)
        {
            if(IdHocPhanXoa.Count() <= 0)
            {
                System.Windows.MessageBox.Show("Vui lòng chọn học phần", "Chưa chọn lớp học phần",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var r = System.Windows.MessageBox.Show("Bạn có muốn xóa không?", "Xóa học phần lên lịch",
                MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (r == MessageBoxResult.Cancel)
            {
                return;
            }

            foreach (var p in IdHocPhanXoa)
            {
                var hocPhan = HocPhans.Where(hp => hp.Id == p).FirstOrDefault();
                if (hocPhan != null)
                {
                    HocPhans.Remove(hocPhan);
                }
            }
            DataHelper.RemoveIdHocPhans(IdHocPhanXoa);
            IdHocPhanXoa.Clear();
        }
    }
}
