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

        public HocPhiViewModel()
        {
            
        }

        public async Task GetAllHocPhi()
        {
            HocPhiModels = await ApiRepository.Ins.GetAllHocPhi();

            HocPhiPhaiNop = HocPhiModels.Sum(p => p.PHAI_THU);
            HocPhiDaThu = HocPhiModels.Sum(p => p.THU_DUOC);
        }


    }


}
