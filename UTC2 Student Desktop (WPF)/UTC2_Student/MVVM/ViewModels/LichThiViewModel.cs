using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UTC2_Student.API.IntermediateModels.LichThi;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;

namespace UTC2_Student.MVVM.ViewModels
{
    public class LichThiViewModel : ViewModelBase
    {
        private List<LichThiModel> lichThi;
        private string searchString;
        private List<LichThiModel> lichThiTemp;

        public List<LichThiModel> LichThi
        {
            get { return lichThi; }
            set { lichThi = value; OnPropertyChanged(); }
        }

        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; set; }

        public LichThiViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearchCommand);
        }

        public async Task GetLichThiByHocKy()
        {
            LichThi = await ApiRepository.Ins.GetLichThiByHocKy();
            lichThiTemp = LichThi;
        }

        private void ExecuteSearchCommand(object obj)
        {
            if(string.IsNullOrEmpty(searchString))
            {
                LichThi = lichThiTemp;
                return;
            }
            LichThi = lichThiTemp
                .Where(p => p.MONTHI.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                .Select(p => new LichThiModel
                {
                    NGAY_THI = p.MONTHI,
                    LOPHOCPHAN_ID = p.LOPHOCPHAN_ID,
                    LICHTHI_ID = p.LICHTHI_ID,
                    LOP_HOCPHAN_CTIET_ID = p.LOP_HOCPHAN_CTIET_ID,
                    GIOTHI = p.GIOTHI,
                    MONTHI = p.MONTHI,
                    MAHP = p.MAHP,
                    LANTHI = p.LANTHI,
                    NAM_HOC = p.NAM_HOC,
                    HOC_KY = p.HOC_KY,
                    PHONG_THI = p.PHONG_THI,
                    ID_HP_THAMSO = p.ID_HP_THAMSO
                }).ToList();
        }
    }
}
