using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private List<HocPhanDaChon> hocPhans;

        public List<HocPhanDaChon> HocPhans
        {
            get { return hocPhans; }
            set { hocPhans = value; OnPropertyChanged(); }
        }

        public ICommand DangKyCommand { get; set; }
        public DangKyViewModel()
        {
            HocPhans = DataHelper.ReadIdHocPhans();
            HocPhanDaChonStore.HocPhans = HocPhans;
            DangKyCommand = new AsyncRelayCommand(ExecuteDangKyCommand);
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
    }
}
