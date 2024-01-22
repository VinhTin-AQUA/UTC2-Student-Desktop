﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.Repositories.IntermediateModels.Auth;

namespace UTC2_Student.API
{
    public static class DataHelper
    {
        private static readonly string dataDirectory = @"./Data";

        private static readonly string accountDataPath = @"/account.json";

        private static readonly string authModelDataPath = @"/auth_model.json";

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

            //string path = @"Data\saveJson.json";
            //string jsonText = File.ReadAllText(path);
            //Account account = JsonConvert.DeserializeObject<Account>(jsonText);
            //Console.WriteLine(account.Name);
            //Console.WriteLine(account.Age);
            //Console.WriteLine(account.Activated);
            //account.Memebers.ForEach(m => {
            //    Console.WriteLine(m);
            //});
        }

        public static void ReadAuthModel()
        {

            //string path = @"Data\saveJson.json";
            //string jsonText = File.ReadAllText(path);
            //Account account = JsonConvert.DeserializeObject<Account>(jsonText);
            //Console.WriteLine(account.Name);
            //Console.WriteLine(account.Age);
            //Console.WriteLine(account.Activated);
            //account.Memebers.ForEach(m => {
            //    Console.WriteLine(m);
            //});
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
