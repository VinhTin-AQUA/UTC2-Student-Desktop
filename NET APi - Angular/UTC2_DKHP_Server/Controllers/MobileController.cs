using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UTC2_DKHP_Server.IRepositories;
using UTC2_DKHP_Server.Models.DKHPMobile;
using UTC2_DKHP_Server.Models.Login;


namespace UTC2_DKHP_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IMobileHocPhanRepository dangKyHocPhanRepository;

        public MobileController(IMobileHocPhanRepository dangKyHocPhanRepository)
        {
            this.dangKyHocPhanRepository = dangKyHocPhanRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(LoginModel.Instance.MSSV != "" && LoginModel.Instance.Password != "")
            {
                model.MSSV = LoginModel.Instance.MSSV;
                model.Password = LoginModel.Instance.Password;
            }

            var response = await dangKyHocPhanRepository.Login(model);

            if (response.IsSuccessStatusCode)
            {
                // lấy nội dung trả về ở dạng chuỗi
                var content = await response.Content.ReadAsStringAsync();

                // chuyển sang json cần có 1 đối tượng với các trường dữ liệu tương ứng
                AuthModel.Instance = JsonConvert.DeserializeObject<AuthModel>(content);

                return Ok(new {
                    statusCode = response.StatusCode,
                    content = AuthModel.Instance,
                    mssv = LoginModel.Instance.MSSV,
                    password = LoginModel.Instance.Password
                });
            }
            else
            {
                return BadRequest(new { statusCode = response.StatusCode, content = "UnAuthorize" });
            }
        }


        [HttpGet("thong-tin-dot-dk-danh-sach-nganh-hoc")]
        public async Task<IActionResult> GetDotDK()
        {
            var response = await dangKyHocPhanRepository.GetDotDK();

            if (response.IsSuccessStatusCode)
            {
                // lấy nội dung trả về ở dạng chuỗi
                var content = await response.Content.ReadAsStringAsync();
                ThongTinDotHocPhan.Instance = JsonConvert.DeserializeObject<ThongTinDotHocPhan>(content)!;
                return Ok(new { status_code = response.StatusCode, content = ThongTinDotHocPhan.Instance });
            }
            else
            {
                return BadRequest(new { statusCode = response.StatusCode, content = "UnAuthorize" });
            }
        }


        [HttpGet("ket-qua-dk")]
        public async Task<IActionResult> KetQuaDK(int id_dot_DK)
        {
            var response = await dangKyHocPhanRepository.KetQuaDK(id_dot_DK);

            if (response.IsSuccessStatusCode)
            {
                // lấy nội dung trả về ở dạng chuỗi
                var content = await response.Content.ReadAsStringAsync();

                List<HocPhan> list = JsonConvert.DeserializeObject<List<HocPhan>>(content)!;

                return Ok(new { status_code = response.StatusCode, ketQuaDK = list });
            }
            else
            {
                return BadRequest(new { status_code = response.StatusCode, content = "UnAuthorize" });
            }
        }


        [HttpGet("danh-sach-hoc-phan/{idMonHoc}")]
        public async Task<IActionResult> GetDanhSachMHDK(int idMonHoc)
        {
            var response = await dangKyHocPhanRepository.GetDanhSachHocPhan(idMonHoc);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                ThongTinDotHocPhan.Instance = JsonConvert.DeserializeObject<ThongTinDotHocPhan>(content)!;
                return Ok(new { status_code = response.StatusCode, thongTinDotHocPhan = ThongTinDotHocPhan.Instance });
            }
            return BadRequest(new { status_code = response.StatusCode, content = "UnAuthorize" });
        }

        //[HttpPost("dang-ky")]
        //public async Task<IActionResult> DangKy([FromBody] List<int> ids)
        //{
        //    // Tính toán thời gian còn lại cho đến khi đến thời điểm cụ thể
        //    //TimeSpan delayTime = targetDateTime - DateTime.Now;

        //    var result = await dangKyHocPhanRepository.DangKy(TimeSpan.FromSeconds(5), ids);

        //    return Ok(new { result });
        //}
    }
}
