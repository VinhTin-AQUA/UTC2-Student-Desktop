using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.API;
using UTC2_Student.API.IntermediateModels.ApiResponses;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;

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


        public DangKyViewModel()
        {
            HocPhans = DataHelper.ReadIdHocPhans();
        }
    }
}
