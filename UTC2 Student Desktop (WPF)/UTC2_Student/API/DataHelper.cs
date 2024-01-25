using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net;
using UTC2_Student.API.IntermediateModels.ApiResponses;
using UTC2_Student.Repositories.IntermediateModels.Auth;

namespace UTC2_Student.API
{
    public static class DataHelper
    {
        private static readonly string dataDirectory = Directory.GetCurrentDirectory() + @"/Data";

        private static readonly string accountDataPath = @"/account.json";

        private static readonly string authModelDataPath = @"/auth_model.json";
        private static readonly string idHocPhanPath = @"/idHocPhans.json";
        

        public static string DataDirectory
        {
            get { return dataDirectory; }
        }

        public static string AccountDataPath
        {
            get { return dataDirectory + accountDataPath; }
        }

        public static string AuthModelDataPath
        {
            get { return dataDirectory + authModelDataPath; }
        }

        public static string IdHocPhanPath
        {
            get { return dataDirectory + idHocPhanPath; }
        }

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string? url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("fa") => // Iran
                        "http://www.aparat.com",
                    { Name: var n } when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };

#pragma warning disable SYSLIB0014 // Type or member is obsolete
                HttpWebRequest? request = (HttpWebRequest)WebRequest.Create(url);
#pragma warning restore SYSLIB0014 // Type or member is obsolete
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #region account
        public static async Task SaveAccount()
        {
            string json = JsonConvert.SerializeObject(LoginModel.Instance, Formatting.Indented);
            using (FileStream fs = new FileStream(AccountDataPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteAsync(json);
                }
            }
        }

        public static async Task ReadAccount()
        {
            string jsonText = "";
            using (FileStream fs = new FileStream(AccountDataPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    jsonText = await sr.ReadToEndAsync();
                }
            }
            LoginModel.Instance = JsonConvert.DeserializeObject<LoginModel>(jsonText);
            int t = 0;
        }

        public static async Task ClearAccount()
        {
            var account = new
            {
            };
            string json = JsonConvert.SerializeObject(account, Formatting.Indented);
            using (var fs = new FileStream(AccountDataPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteAsync(json);
                }
            }
        }

        #endregion

        #region auth model

        public static async Task SaveAuthModel()
        {
            string json = JsonConvert.SerializeObject(AuthModel.Instance, Formatting.Indented);
            using (var fs = new FileStream(AuthModelDataPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteAsync(json);
                }
            }
        }

        public static async Task ReadAuthModel()
        {
            string jsonText = "";
            using (FileStream fs = new FileStream(AuthModelDataPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    jsonText = await sr.ReadToEndAsync();
                }
            }
            AuthModel.Instance = JsonConvert.DeserializeObject<AuthModel>(jsonText);
        }

        public static async Task ClearAuthModel()
        {
            var account = new
            {
            };
            string json = JsonConvert.SerializeObject(account, Formatting.Indented);
            using (var fs = new FileStream(AccountDataPath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    await sw.WriteAsync(json);
                }
            }
        }

        #endregion

        #region id hoc phan

        public static async Task SaveIdHocPhans(HocPhanDaChon hocPhanDaChon)
        {
            ObservableCollection<HocPhanDaChon> hocPhanDaChons = await ReadIdHocPhans();
            if (hocPhanDaChons.Any(p => p.Id == hocPhanDaChon.Id) == false)
            {
                hocPhanDaChons.Add(hocPhanDaChon);
            }
            string json = JsonConvert.SerializeObject(hocPhanDaChons, Formatting.Indented);
            using (var fs = new FileStream(IdHocPhanPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var fw = new StreamWriter(fs))
                {
                    await fw.WriteAsync(json);
                }
            }
        }

        public static async Task<ObservableCollection<HocPhanDaChon>> ReadIdHocPhans()
        {
            string jsonText = "";

            using (var fs = new FileStream(IdHocPhanPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                using(var fr = new StreamReader(fs))
                {
                    jsonText = await fr.ReadToEndAsync();
                }
            }

            ObservableCollection<HocPhanDaChon>? hocPhanDaChons = JsonConvert.DeserializeObject<ObservableCollection<HocPhanDaChon>>(jsonText);

            if (hocPhanDaChons == null)
            {
                return new ObservableCollection<HocPhanDaChon>();
            }
            return hocPhanDaChons;
        }

        public static async Task RemoveIdHocPhans(List<string> ids)
        {
            ObservableCollection<HocPhanDaChon> hocPhanDaChons = await ReadIdHocPhans();

            foreach (var p in ids)
            {
                var hocPhan = hocPhanDaChons.Where(hp => hp.Id == p).FirstOrDefault();

                if (hocPhan != null)
                {
                    hocPhanDaChons.Remove(hocPhan);
                }
            }

            string json = JsonConvert.SerializeObject(hocPhanDaChons, Formatting.Indented);

            using(var fs = new FileStream(IdHocPhanPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var sw = new StreamWriter(fs)) 
                {
                    await sw.WriteAsync(json);
                }
            }
        }

        #endregion


        //public static void RemoveAccount()
        //{
        //    string filePath = @"\Data\account.json";

        //    using (StreamWriter sw = new StreamWriter(filePath, false))
        //    {
        //        sw.Write(string.Empty);
        //    }
        //}

        public static void ClearData()
        {
            // xóa thông tin tài khoản
            string account = DataHelper.AccountDataPath;
            using (StreamWriter sw = new StreamWriter(account, false))
            {
                sw.Write(string.Empty);
            }

            // xóa thông tin authorization
            string authModel = DataHelper.AuthModelDataPath;
            using (StreamWriter sw = new StreamWriter(authModel, false))
            {
                sw.Write(string.Empty);
            }
        }
    }
}
