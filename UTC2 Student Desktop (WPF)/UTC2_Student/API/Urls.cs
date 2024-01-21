using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.Repositories.IntermediateModels.Auth;

namespace UTC2_Student.API
{
    public static class Urls
    {
        public static string baseUrl { get; set; } = "http://apiportalstudent7mobile.utc2.edu.vn/api/v1";
        public static string loginUrl { get; set; } = "/auth/login";
        public static string dSDotDKUrl { get; set; } = "/DangKy_MonHoc";
        public static string ketQuaDKUrl { get; set; } = "/Partial_DanhSachDaDK";
        public static string danhSachHocPhanUrl { get; set; } = "/DanhSach_MonHoc_KNAndMH";
        public static string dangKyUrl { get; set; } = "/Luu_KetQuaDangKy";

        public static string LoginUrl()
        {
            return baseUrl + loginUrl;
        }

        public static string DSDotDKUrl(AuthModel auth)
        {
            if (auth.result == null)
            {
                return baseUrl + dSDotDKUrl;
            }

            string queries = $"?KHOA_NGANH_ID={auth.result[0].khoA_NGANH_ID}&SINHVIEN_ID={auth.result[0].sinhvieN_ID}&ID_NGANH={auth.result[0].iD_NGANH}&ID_HEDAOTAO={auth.result[0].iD_HEDAOTAO}&id_khoa={auth.result[0].id_khoa}&LOPHOC_ID={auth.result[0].lophoC_ID}&ID_KHOAHOC={auth.result[0].iD_KHOAHOC}&id_cn={auth.result[0].id_cn}&ID_SINHVIEN_NAMHOC={auth.result[0].iD_SINHVIEN_NAMHOC}&MA_DVIQLY={auth.result[0].mA_DVIQLY}&NIENCHE_OR_TINCHI={auth.result[0].nienchE_OR_TINCHI}&NamHocKy={auth.result[0].namHocKy}&SoHocKy={auth.result[0].soHocKy}&HocKyTruoc={auth.result[0].hocKyTruoc}";


            return baseUrl + dSDotDKUrl + queries;
        }

        public static string KetQuaDKUrl(AuthModel auth, int id_dot_Dk)
        {
            if (auth.result == null)
            {
                return baseUrl + ketQuaDKUrl;
            }
            string queries = $"?MA_DVIQLY={auth.result[0].mA_DVIQLY}&NIENCHE_OR_TINCHI={auth.result[0].nienchE_OR_TINCHI}&SINHVIEN_ID={auth.result[0].sinhvieN_ID}&ID_NGANH={auth.result[0].iD_NGANH}&ID_KHOAHOC={auth.result[0].iD_KHOAHOC}&ID_HP_THAMSO={id_dot_Dk}";

            return baseUrl + ketQuaDKUrl + queries;
        }

        public static string DanhSachHocPhanUrl(AuthModel auth, int idMonHoc)
        {
            if (auth.result == null)
            {
                return baseUrl + ketQuaDKUrl;
            }
            string queries = $"?KHOA_NGANH_ID={auth.result[0].khoA_NGANH_ID}&pIsMobile={1}&MonHoc={idMonHoc}&SINHVIEN_ID={auth.result[0].sinhvieN_ID}&ID_NGANH={auth.result[0].iD_NGANH}&ID_HEDAOTAO={auth.result[0].iD_HEDAOTAO}&id_khoa={auth.result[0].id_khoa}&LOPHOC_ID={auth.result[0].lophoC_ID}&ID_KHOAHOC={auth.result[0].iD_KHOAHOC}&id_cn={auth.result[0].id_cn}&ID_SINHVIEN_NAMHOC={auth.result[0].iD_SINHVIEN_NAMHOC}&MA_DVIQLY={auth.result[0].mA_DVIQLY}&NIENCHE_OR_TINCHI={auth.result[0].nienchE_OR_TINCHI}&NamHocKy={auth.result[0].namHocKy}&SoHocKy={auth.result[0].soHocKy}&HocKyTruoc={auth.result[0].hocKyTruoc}";

            return baseUrl + danhSachHocPhanUrl + queries;
        }

        public static string DangKyUrl()
        {
            return baseUrl + dangKyUrl;
        }
    }
}
