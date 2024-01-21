using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using UTC2_DKHP_Server.IRepositories;
using UTC2_DKHP_Server.Models.DKHPMobile;
using UTC2_DKHP_Server.Models.Login;
using UTC2_DKHP_Server.Services;

namespace UTC2_DKHP_Server.Repositories
{
    public class MobileRepository : IMobileHocPhanRepository
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly MobileUrls mobileUrlService;


        static bool stopTasks = false;
        static Random random = new Random();
        static int completedTasksTotal = 0;
        static List<CancellationTokenSource> ctss; // quản lý trạng thái run của các tác vụ A,B


        public MobileRepository(IHttpClientFactory httpClientFactory,
            MobileUrls mobileUrlService)
        {
            this.httpClientFactory = httpClientFactory;
            this.mobileUrlService = mobileUrlService;
        }

        public async Task<HttpResponseMessage> GetDanhSachHocPhan(int idMonHoc)
        {
            using (var http = httpClientFactory.CreateClient())
            {
                http.Timeout = TimeSpan.FromDays(10);
                // đính kèm header
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);

                string danhSachMonHocDangKyUrl = mobileUrlService.DanhSachHocPhanUrl(AuthModel.Instance, idMonHoc);
                HttpResponseMessage response = await http.GetAsync(danhSachMonHocDangKyUrl);
                return response;
            }
        }



        public async Task<HttpResponseMessage> GetDotDK()
        {
            using (var httpCLient = httpClientFactory.CreateClient())
            {
                httpCLient.Timeout = TimeSpan.FromDays(10);
                // đính kèm header
                httpCLient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);

                string dotDKUrl = mobileUrlService.DSDotDKUrl(AuthModel.Instance);

                // goi api
                HttpResponseMessage response = await httpCLient.GetAsync(dotDKUrl);

                return response;
            }
        }

        public async Task<HttpResponseMessage> KetQuaDK(int id_dot_DK)
        {
            using (var httpCLient = httpClientFactory.CreateClient())
            {
                httpCLient.Timeout = TimeSpan.FromDays(10);
                // đính kèm header
                httpCLient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);

                string ketQuaDKUrl = mobileUrlService.KetQuaDKUrl(AuthModel.Instance, id_dot_DK);

                // goi api
                HttpResponseMessage response = await httpCLient.GetAsync(ketQuaDKUrl);
                return response;
            }
        }

        public async Task<HttpResponseMessage> Login(LoginModel model)
        {
            using (var httpCLient = httpClientFactory.CreateClient())
            {
                httpCLient.Timeout = TimeSpan.FromDays(10);
                string loginUrl = mobileUrlService.LoginUrl();
                var loginInfo = new { Email = model.MSSV, Password = model.Password };

                LoginModel.Instance.MSSV = model.MSSV;
                LoginModel.Instance.Password = model.Password;

                var jsonContext = new StringContent(JsonConvert.SerializeObject(loginInfo), System.Text.Encoding.UTF8, "application/json");

                // goi api
                HttpResponseMessage response = await httpCLient.PostAsync(loginUrl, jsonContext);

                return response;
            }
        }


        public async Task<HttpResponseMessage?> GuiApiDK(string id)
        {
            if(AuthModel.Instance != null)
            {
                using (var httpCLient = httpClientFactory.CreateClient())
                {
                    httpCLient.Timeout = TimeSpan.FromDays(10);
                    string dangKyUrl = mobileUrlService.DangKyUrl();

                    var payload = new 
                    {
                        ID = id,
                        MA_DVIQLY = AuthModel.Instance.result[0].mA_DVIQLY,
                        NamHocKy = AuthModel.Instance.result[0].namHocKy,
                        SoHocKy = AuthModel.Instance.result[0].soHocKy,
                        MA_SINHVIEN = AuthModel.Instance.result[0].mA_SINHVIEN,
                        NIENCHE_OR_TINCHI = AuthModel.Instance.result[0].nienchE_OR_TINCHI,
                        KHOA_NGANH_ID = AuthModel.Instance.result[0].khoA_NGANH_ID,
                        SINHVIEN_ID = AuthModel.Instance.result[0].sinhvieN_ID,
                        ID_HEDAOTAO = AuthModel.Instance.result[0].iD_HEDAOTAO,
                        ID_NGANH = AuthModel.Instance.result[0].iD_NGANH,
                        LOPHOC_ID = AuthModel.Instance.result[0].lophoC_ID,
                        id_cn = AuthModel.Instance.result[0].id_cn,
                        ID_KHOAHOC = AuthModel.Instance.result[0].iD_KHOAHOC,
                        id_khoa = AuthModel.Instance.result[0].id_khoa,
                        ID_SINHVIEN_NAMHOC = AuthModel.Instance.result[0].iD_SINHVIEN_NAMHOC,
                        isJob = 0,
                        isMb = 1,
                        isnangdiem = "0",
                        HocKyTruoc = AuthModel.Instance.result[0].hocKyTruoc,
                    };

                    var jsonContext = new StringContent(JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json");

                    httpCLient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);

                    // goi api
                    HttpResponseMessage response = await httpCLient.PostAsync(dangKyUrl, jsonContext);

                    return response;
                }
            }
            return null;
        }



        //public async Task<string> DangKy(TimeSpan timeSpan, List<int> ids)
        //{
        //    if (ids.Count() <= 0)
        //    {
        //        return "Không có ids";
        //    }

        //    if (timeSpan > TimeSpan.Zero)
        //    {
        //        ctss = new List<CancellationTokenSource>();
        //        List<Task> tasks = new List<Task>();

        //        for (int i = 0; i < ids.Count(); i++)
        //        {
        //            ctss.Add(new CancellationTokenSource());
        //        }
        //        Console.WriteLine("Dang cho toi thoi diem dang ky");
        //        await Task.Delay(timeSpan);
        //        Console.WriteLine("Start");

        //        for (int i = 0; i < ids.Count(); i++)
        //        {
        //            int index = i;
        //            tasks.Add(Task.Run(() => GuiAPIDangKy(index, ids[index]), ctss[index].Token));
        //        }
        //        tasks.Add(Task.Run(() => LoginAgain()));

        //        // thực thi tác vụ
        //        await Task.WhenAll(tasks);

        //        for(int i = 0; i < ctss.Count(); i++)
        //        {
        //            ctss[i].Cancel();
        //        }
        //        Console.WriteLine("End");

        //        return "Hàm được thực thi vào thời điểm đã đặt.";
        //    }
        //    return "Lịch thực thi đã quá hạn";
        //}

        //private async Task<string> GuiAPIDangKy(int cancelIndex, int id)
        //{
        //    while (true)
        //    {
        //        if (ctss[cancelIndex].Token.IsCancellationRequested == false)
        //        {
        //            // đợi call api
        //            await Task.Delay(200);

        //            int r = random.Next(1, 101);

        //            if (r == id)
        //            {
        //                ctss[cancelIndex].Cancel();
        //                completedTasksTotal++; // tăng thêm 1 tác vụ hoàn thành
        //                return "DK thanh cong: " + id;
        //            }
        //            Console.WriteLine($"DK {id} that bai. Dang dang nhap lai {r}");
        //        }
        //    }
        //}

        //private async Task LoginAgain()
        //{
        //    while (true)
        //    {
        //        await Task.Delay(1000); // đợi sau 3s A,B chạy thì do somthing

        //        // nếu tác vụ A,B hoàn thành có nghĩa completedTasksTotal == 2 thì kết thúc tác vụ
        //        if (completedTasksTotal == ctss.Count())
        //        {
        //            break;
        //        }

        //        // cancel để dừng tác vụ A,B
        //        for (int i = 0; i < ctss.Count(); i++)
        //        {
        //            ctss[i].Cancel();
        //        }

        //        Console.WriteLine("Login Again");
        //        //await Task.Delay(3000);
        //        //Thread.Sleep(3000);
        //        Console.WriteLine("Done");

        //        // sau khi làm gì đó thì cho 2 tác vụ A,B tiếp tục
        //        for (int i = 0; i < ctss.Count(); i++)
        //        {
        //            ctss[i] = new CancellationTokenSource();
        //        }
        //    }
        //}
    }
}
