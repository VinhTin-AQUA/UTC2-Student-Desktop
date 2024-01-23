using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;

namespace UTC2_Student.MVVM.ViewModels.DKHP
{
    public class KetQuaDKViewModel : ViewModelBase
    {
        private List<DotHocPhan> dotHocPhans;
        private List<HocPhan> hocPhans;
        private string tongSoTC;

        public List<DotHocPhan> DotHocPhans
        {
            get { return dotHocPhans; }
            set { dotHocPhans = value; OnPropertyChanged(); }
        }

        public List<HocPhan> HocPhans
        {
            get { return hocPhans; }
            set { hocPhans = value; OnPropertyChanged(); }
        }

        public string TongSoTC
        {
            get { return tongSoTC;}
            set { tongSoTC = value; OnPropertyChanged(); }
        }

        //public ICommand GetDanhSachDaDKCommand { get; set; }

        public KetQuaDKViewModel()
        {
            //GetDanhSachDaDKCommand = new RelayCommand(ExecuteGetDanhSachDaDKCommand);
        }

        public async Task GetDotDK()
        {
            DotHocPhans = await ApiRepository.Ins.GetDotDK();
        }

        public async Task GetHocPhanDKTheoDot(int idDot)
        {
            HocPhans = await ApiRepository.Ins.KetQuaDK(idDot);
            if(HocPhans == null || HocPhans.Count == 0)
            {
                TongSoTC = "Không có môn học nào được đăng ký";
            }
            else
            {
                TongSoTC = "Tổng số tín chỉ: " + HocPhans.Sum(p => p.sO_TC).ToString();
            }
        }
    }
}
