using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.API;
using UTC2_Student.API.IntermediateModels.HocPhi;
using UTC2_Student.API.IntermediateModels.KTX;
using UTC2_Student.API.IntermediateModels.LichThi;
using UTC2_Student.API.IntermediateModels.NotificationResponses;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;
using UTC2_Student.Repositories.IntermediateModels.Auth;
using static System.Net.WebRequestMethods;

namespace UTC2_Student.Repositories
{
    public class ApiRepository
    {
        private static ApiRepository? _ins = null;

        public static ApiRepository Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ApiRepository();
                }
                return _ins;
            }
        }

        private ApiRepository()
        {
        }

        #region DKHP
        public async Task<List<HocPhan>> GetDanhSachHocPhan(int idMonHoc)
        {
            string danhSachMonHocDangKyUrl = Urls.DanhSachHocPhanApi(AuthModel.Instance!, idMonHoc);
            var response = await GET(danhSachMonHocDangKyUrl, true);

            string content = await response.Content.ReadAsStringAsync();
            ThongTinDotHocPhan? thongTinDotHocPhan = JsonConvert.DeserializeObject<ThongTinDotHocPhan>(content);
            return thongTinDotHocPhan!.hocphan_ct;
        }

        public async Task<List<MonHoc>> GetDSMonHoc()
        {
            string dotDKUrl = Urls.DSDotDKApi(AuthModel.Instance!);
            var response = await GET(dotDKUrl, true);
            var content = await response.Content.ReadAsStringAsync();
            ThongTinDotHocPhan? list = JsonConvert.DeserializeObject<ThongTinDotHocPhan>(content);
            return list!.monhoccombox!;
        }

        public async Task<List<DotHocPhan>> GetDotDK()
        {
            string dotDKUrl = Urls.DSDotDKApi(AuthModel.Instance!);
            var response = await GET(dotDKUrl, true);
            var content = await response.Content.ReadAsStringAsync();
            ThongTinDotHocPhan? list = JsonConvert.DeserializeObject<ThongTinDotHocPhan>(content);
            return list!.dotHocPhans;
        }

        public async Task<List<HocPhan>> KetQuaDK(int id_dot_DK)
        {
            string ketQuaDKUrl = Urls.KetQuaDKApi(AuthModel.Instance!, id_dot_DK);
            var response = await GET(ketQuaDKUrl, true);
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                List<HocPhan>? hocPhans = JsonConvert.DeserializeObject<List<HocPhan>>(content);
                return hocPhans!;
            }
            catch
            {
                return new List<HocPhan> { };
            }
        }

        public async Task<HttpResponseMessage?> Login(string mssv, string pass)
        {
            using (var httpCLient = new HttpClient())
            {
                httpCLient.Timeout = TimeSpan.FromDays(10);
                string loginUrl = Urls.LoginApi();
                var loginInfo = new { Email = mssv, Password = pass };

                LoginModel.Instance!.MSSV = mssv;
                LoginModel.Instance.Password = pass;

                var jsonContext = new StringContent(JsonConvert.SerializeObject(loginInfo), System.Text.Encoding.UTF8, "application/json");

                // goi api
                HttpResponseMessage? response = null;

                //await Task.Delay(3000);
                //response = await httpCLient.PostAsync(loginUrl, jsonContext);
                //return response;

                while (true)
                {
                    response = await httpCLient.PostAsync(loginUrl, jsonContext);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        try
                        {
                            AuthModel.Instance = JsonConvert.DeserializeObject<AuthModel>(content);
                        }
                        catch
                        {
                            DataHelper.ClearData();
                            return null;
                        }
                        DataHelper.SaveAccount();
                        DataHelper.SaveAuthModel();
                        return response;
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        DataHelper.ClearData();
                        return response;
                    }
                }
            }
        }

        public async Task<HttpResponseMessage?> GuiApiDK(string id)
        {
            if (AuthModel.Instance != null)
            {
                using (var httpCLient = new HttpClient())
                {
                    httpCLient.Timeout = TimeSpan.FromDays(10);
                    string dangKyUrl = Urls.DangKyApi();

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
                    HttpResponseMessage? response = null;

                    while(true)
                    {
                        response = await httpCLient.PostAsync(dangKyUrl, jsonContext);

                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            await Login(LoginModel.Instance!.MSSV, LoginModel.Instance.Password);
                            httpCLient.DefaultRequestHeaders.Remove("Authorization");
                            httpCLient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);
                            response = await httpCLient.PostAsync(dangKyUrl, jsonContext);
                        }

                        if (response.IsSuccessStatusCode == false)
                        {
                            continue;
                        }
                        else if (response.IsSuccessStatusCode == true)
                        {
                            return response;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<string> HuyDKHocPhan(string id)
        {
            var data = new
            {
                ID = id,
                ID_KHOAHOC = AuthModel.Instance!.result[0].iD_KHOAHOC,
                ID_NGANH = AuthModel.Instance.result[0].iD_NGANH,
                MA_DVIQLY = AuthModel.Instance.result[0].mA_DVIQLY,
                MA_SINHVIEN = AuthModel.Instance.result[0].mA_SINHVIEN,
                NIENCHE_OR_TINCHI = AuthModel.Instance.result[0].nienchE_OR_TINCHI,
                SINHVIEN_ID = AuthModel.Instance.result[0].sinhvieN_ID,
                LOPHOC_ID = AuthModel.Instance.result[0].lophoC_ID,
                idCN = AuthModel.Instance.result[0].id_cn,
                NHocKy = AuthModel.Instance.result[0].namHocKy,
                SHocKy = AuthModel.Instance.result[0].soHocKy,
                idHeDaoTao = AuthModel.Instance.result[0].iD_HEDAOTAO,
                ID_SINHVIEN_NAMHOC = AuthModel.Instance.result[0].iD_SINHVIEN_NAMHOC
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            string url = Urls.HuyKQDKApi();

            using (var http = new HttpClient())
            {
                http.Timeout = TimeSpan.FromDays(10);
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);
                HttpResponseMessage? reponse = null;

                while(true)
                {
                    reponse = await http.PostAsync(url, jsonContent);

                    if (reponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await Login(LoginModel.Instance!.MSSV, LoginModel.Instance.Password);
                        http.DefaultRequestHeaders.Remove("Authorization");
                        http.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance.v);
                    }

                    if(reponse.IsSuccessStatusCode == false)
                    {
                        continue;
                    }

                    if(reponse.IsSuccessStatusCode)
                    {
                        var content = await reponse.Content.ReadAsStringAsync();
                        return content;
                    }
                }
            }
        }

        #endregion

        #region thong bao

        public async Task<NotificationResponse?> GetThongBaos(int currentPage = 1)
        {
            using (var httpCLient = new HttpClient())
            {
                httpCLient.Timeout = TimeSpan.FromDays(10);
                var url = Urls.GetThongBaoApi(currentPage, 20);
                var response = await httpCLient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    NotificationResponse? notificationResponse = JsonConvert.DeserializeObject<NotificationResponse>(content);
                    return notificationResponse!;
                };
                return null;
            }
        }

        #endregion

        #region hoc phi

        public async Task<List<HocPhiModel>?> GetAllHocPhi()
        {
            var sinhVienId = AuthModel.Instance!.result[0].sinhvieN_ID;
            var maDvQl = AuthModel.Instance.result[0].mA_DVIQLY;

            var payload = new
            {
                SINHVIEN_ID = sinhVienId,
                MA_DVIQLY = maDvQl
            };

            var jsonCOntent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var getAllHocPhiUrl = Urls.GetHocPhiApi();
            using (var httpCLient = new HttpClient())
            {
                httpCLient.Timeout = TimeSpan.FromDays(10);
                HttpResponseMessage response = await httpCLient.PostAsync(getAllHocPhiUrl, jsonCOntent);
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\\/", "/");
                var r = content.Remove(0, 1);
                r = r.Remove(r.Length - 1);
                r = r.Replace("\\", "");
               
                List<HocPhiModel>? hocPhis = JsonConvert.DeserializeObject<List<HocPhiModel>>(r);
                return hocPhis;
            }
        }

        #endregion

        #region lich thi

        public async Task<List<LichThiModel>?> GetLichThiByHocKy()
        {
            var data = new
            {
                SINHVIEN_ID = AuthModel.Instance!.result[0].sinhvieN_ID,
                MA_DVIQLY = AuthModel.Instance.result[0].mA_DVIQLY,
                NC_OR_TC = AuthModel.Instance.result[0].nienchE_OR_TINCHI,
                NAM_HOC = "0",
                HOC_KY = "0"
            };

            var url = Urls.GetLichThiByHocKy();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using(var http = new HttpClient())
            {
                http.Timeout = TimeSpan.FromDays(10);
                var response = await http.PostAsync(url, jsonContent);

                var content = await response.Content.ReadAsStringAsync();

                content = content.Replace("\\/", "/");
                content = content.Remove(0, 1);
                content = content.Remove(content.Length-1);
                content = content.Replace("\\", "");
                List<LichThiModel>? list = JsonConvert.DeserializeObject<List<LichThiModel>>(content);
                return list;
            }
        }

        #endregion

        #region KTX

        public async Task<List<LichSuKTX>?> GetLichSuKTX()
        {
            var url = Urls.GetLichSuKTX();
            var data = new
            {
                Student_ID = AuthModel.Instance!.result[0].sinhvieN_ID
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            using (var http = new HttpClient())
            {
                http.Timeout = TimeSpan.FromDays(10);
                http.DefaultRequestHeaders.Add("authorization", "646b10fa650c93c024244f49f1a88ac7fft123");
                var response = await http.PostAsync(url, jsonContent);

                if(response.IsSuccessStatusCode == false)
                {
                    return null;
                }
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\\/", "/");
                content = content.Replace("\\", "");
                content = content.Remove(0, 1);
                content = content.Remove(content.Length-1);

                List<LichSuKTX>? list = JsonConvert.DeserializeObject<List<LichSuKTX>>(content);
                return list;
            }
        }

        #endregion


        /* private methods */
        private async Task<HttpResponseMessage> GET(string url, bool hasToken = false)
        {
            HttpResponseMessage? response = null;

            using (var http = new HttpClient())
            {
                if(hasToken == true)
                {
                    http.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance!.v);
                }
                http.Timeout = TimeSpan.FromDays(10);

                while (true)
                {
                    response = await http.GetAsync(url);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        await Login(LoginModel.Instance!.MSSV, LoginModel.Instance.Password);
                        if (hasToken == true)
                        {
                            http.DefaultRequestHeaders.Remove("Authorization");
                            http.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthModel.Instance!.v);
                        }
                        response = await http.GetAsync(url);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        continue;
                    }
                }
            }
        }

        //private async Task<HttpResponseMessage> POST(string url, bool hasToken = false)
        //{
        //    return null;
        //}
    }
}
