using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using UTC2_Student.API.IntermediateModels.ApiResponses;
using UTC2_Student.Repositories.IntermediateModels.Auth;

namespace UTC2_Student.API
{
    public static class DataHelper
    {
        private static readonly string dataDirectory = @"./Data";

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

        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
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

                var request = (HttpWebRequest)WebRequest.Create(url);
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

        public static void SaveAccount()
        {
            string json = JsonConvert.SerializeObject(LoginModel.Instance, Formatting.Indented);
            string path = DataHelper.AccountDataPath;
            File.WriteAllText(path, json);
        }

        public static void SaveAuthModel()
        {
            string json = JsonConvert.SerializeObject(AuthModel.Instance, Formatting.Indented);
            string path = DataHelper.AuthModelDataPath;
            File.WriteAllText(path, json);
        }

        public static void ReadAccount()
        {
            string jsonText = File.ReadAllText(AccountDataPath);
            LoginModel.Instance = JsonConvert.DeserializeObject<LoginModel>(jsonText);
        }

        public static void ReadAuthModel()
        {
            string jsonText = File.ReadAllText(AuthModelDataPath);
            AuthModel.Instance = JsonConvert.DeserializeObject<AuthModel>(jsonText);
        }

        public static void SaveIdHocPhans(HocPhanDaChon hocPhanDaChon)
        {
            List<HocPhanDaChon> hocPhanDaChons = ReadIdHocPhans();
            hocPhanDaChons.Add(hocPhanDaChon);

            string json = JsonConvert.SerializeObject(hocPhanDaChons, Formatting.Indented);
            File.WriteAllText(IdHocPhanPath, json);
        }

        public static List<HocPhanDaChon> ReadIdHocPhans()
        {
            string jsonText = File.ReadAllText(IdHocPhanPath);
            List<HocPhanDaChon> hocPhanDaChons = JsonConvert.DeserializeObject<List<HocPhanDaChon>>(jsonText);
            if (hocPhanDaChons == null)
            {
                return new List<HocPhanDaChon>();
            }
            return hocPhanDaChons;
        }

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
