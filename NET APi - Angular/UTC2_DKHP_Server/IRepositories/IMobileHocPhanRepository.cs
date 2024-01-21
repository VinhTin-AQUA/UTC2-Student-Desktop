using UTC2_DKHP_Server.Models.Login;

namespace UTC2_DKHP_Server.IRepositories
{
    public interface IMobileHocPhanRepository
    {
        Task<HttpResponseMessage> Login(LoginModel model);
        
        Task<HttpResponseMessage> GetDotDK();

        Task<HttpResponseMessage> KetQuaDK(int id_dot_DK);

        Task<HttpResponseMessage> GetDanhSachHocPhan(int idMonHoc);

        public Task<HttpResponseMessage?> GuiApiDK(string id);

        //Task<string> DangKy(TimeSpan timeSpan, List<int> ids);
    }
}
