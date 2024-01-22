using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.Repositories.IntermediateModels.Auth
{
    public class AuthModel
    {
        private static AuthModel? instance = null;
        private static object lockObj = new object();


        public Information[] result { get; set; }
        public string v { get; set; }


        // private: ngăn chặn đối tượng từ bên ngoài
        private AuthModel()
        {

        }

        public static AuthModel? Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new AuthModel();
                        }
                    }
                }
                return instance;
            }

            set { instance = value; }
        }
    }
}
