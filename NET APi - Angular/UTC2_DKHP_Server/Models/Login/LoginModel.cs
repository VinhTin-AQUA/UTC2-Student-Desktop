using System.ComponentModel.DataAnnotations;

namespace UTC2_DKHP_Server.Models.Login
{
    public class LoginModel
    {
        private static LoginModel? _instance = null;
        private static object lockObject = new object();

        [Required]
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
