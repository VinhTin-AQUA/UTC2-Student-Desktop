using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.API.IntermediateModels.HocPhi;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;

namespace UTC2_Student.MVVM.ViewModels
{
    public class HocPhiViewModel : ViewModelBase
    {
        private List<HocPhiModel> hocPhiModels;
        private decimal hocPhiPhaiNop;
        private decimal hocPhiDaThu;

        public List<HocPhiModel> HocPhiModels
        {
            get { return hocPhiModels; }
            set { hocPhiModels = value;OnPropertyChanged(); }
        }

        public decimal HocPhiPhaiNop
        {
            get { return hocPhiPhaiNop;}
            set { hocPhiPhaiNop = value; OnPropertyChanged(); }
        }

        public decimal HocPhiDaThu
        {
            get { return hocPhiDaThu; }
            set { hocPhiDaThu = value; OnPropertyChanged(); }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HocPhiViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            
        }

        public async Task GetAllHocPhi()
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            HocPhiModels = await ApiRepository.Ins.GetAllHocPhi();
#pragma warning restore CS8601 // Possible null reference assignment.

            HocPhiPhaiNop = HocPhiModels!.Sum(p => p.PHAI_THU);
            HocPhiDaThu = HocPhiModels!.Sum(p => p.THU_DUOC);
        }


    }


}
