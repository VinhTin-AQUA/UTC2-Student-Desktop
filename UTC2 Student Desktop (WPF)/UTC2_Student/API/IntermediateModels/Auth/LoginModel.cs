using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.Repositories.IntermediateModels.Auth
{
    public class LoginModel
    {
        private static LoginModel? _instance = null;
        private static object lockObject = new object();

        public string MSSV { get; set; } = "";
        public string Password { get; set; } = "";

        //private LoginModel() { }

        public static LoginModel? Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoginModel();
                        }
                    }
                }
                return _instance;
            }
            set { _instance = value; }
        }
    }
}
