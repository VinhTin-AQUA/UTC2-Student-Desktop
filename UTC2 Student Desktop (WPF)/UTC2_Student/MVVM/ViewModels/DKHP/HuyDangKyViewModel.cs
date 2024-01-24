using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;

namespace UTC2_Student.MVVM.ViewModels.DKHP
{
    public class HuyDangKyViewModel : ViewModelBase
    {
        private List<DotHocPhan> dotHocPhans;
        private string tongSoTC;
        private List<HocPhan> hocPhans;

        public List<DotHocPhan> DotHocPhans
        {
            get { return dotHocPhans; }
            set { dotHocPhans = value; OnPropertyChanged(); }
        }

        public string TongSoTC
        {
            get { return tongSoTC; }
            set { tongSoTC = value; OnPropertyChanged(); }
        }

        public List<HocPhan> HocPhans
        {
            get { return hocPhans; }
            set { hocPhans = value; OnPropertyChanged(); }
        }

        public ICommand RemoveHocPhanCommand { get; set; }

        public HuyDangKyViewModel()
        {
            RemoveHocPhanCommand = new AsyncRelayCommand(ExecuteRemoveHocPhanCommand);
            A();
        }

        public async void A()
        {
            await GetDotDK();
        }

        public async Task GetDotDK()
        {
            DotHocPhans = await ApiRepository.Ins.GetDotDK();
        }

        private async Task ExecuteRemoveHocPhanCommand(object obj)
        {
            //var hocPhanXoa = HocPhans
            //    .Where(hp => hp.loP_HOCPHAN_CTIET_ID.ToString() == obj.ToString())
            //    .FirstOrDefault();

            //if(hocPhanXoa == null)
            //{
            //    return;
            //}
            //var r = System.Windows.MessageBox.Show($"Bạn có muốn xóa học phần {hocPhanXoa.teN_LOP} không?",
            //    "Xóa học phần", System.Windows.MessageBoxButton.OKCancel, System.Windows.MessageBoxImage.Question);

            //if (r == System.Windows.MessageBoxResult.Cancel)
            //{
            //    return;
            //}

            using(var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("AA", "AA");
                //http.DefaultRequestHeaders.Remove("AA");
                //http.DefaultRequestHeaders.Add("AA", "BB");
                //http.DefaultRequestHeaders.Remove("AA");
                //http.DefaultRequestHeaders.Add("AA", "CC");
                string t = "";
                foreach(var h in http.DefaultRequestHeaders)
                {
                    t += $"{h.Key} {h.Value} \n";
                }
                System.Windows.MessageBox.Show(t);
            }


            //System.Windows.MessageBox.Show("Xóa thành công");
        }

        public async Task GetHocPhanDKTheoDot(int idDot)
        {
            HocPhans = await ApiRepository.Ins.KetQuaDK(idDot);
            if (HocPhans == null || HocPhans.Count == 0)
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
