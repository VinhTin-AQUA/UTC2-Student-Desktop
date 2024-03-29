﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.Repositories.IntermediateModels.Auth;

namespace UTC2_Student.API
{
    public static class Urls
    {
        #region DKHP
        private static string baseApi { get; set; } = "http://apiportalstudent7mobile.utc2.edu.vn/api/v1";
        private static string loginApi { get; set; } = "/auth/login";
        private static string dSDotDKApi { get; set; } = "/DangKy_MonHoc";
        private static string ketQuaDKApi { get; set; } = "/Partial_DanhSachDaDK";
        private static string danhSachHocPhanApi { get; set; } = "/DanhSach_MonHoc_KNAndMH";
        private static string dangKyApi { get; set; } = "/Luu_KetQuaDangKy";
        private static string huyKQDKApi { get; set; } = "/Huy_KetQuaDangKy";


        #endregion

        #region thong bao

        private static string baseWebApi = "https://utc2.edu.vn/api/v1.0";

        private static string getThongBaoChungApi = "/post";
        private static string tKBLichThiAPi = "/post";
        private static string keHoachNamHocApi = "/post";

        private static string accessThongBaoChungWeb  = "https://utc2.edu.vn/sinh-vien/thong-bao/";
        private static string accessTKBLichThiWeb = "https://utc2.edu.vn/sinh-vien/thoi-khoa-bieu/";
        private static string accessKeHoachNamHocWeb = "https://utc2.edu.vn/chi-tiet-bai-viet/";

        #endregion

        #region hoc phi
        private static string hocPhiBaseApi { get; set; } = "http://apihocphinew.utc2.edu.vn";

        private static string getHocPhiApi { get; set; } = "/Service_NotificationSV.svc/GetAllHocPhiBySV";
        // http://apihocphinew.utc2.edu.vn/Service_NotificationSV.svc/GetAllHocPhiBySV

        #endregion

        #region lich thi
        private static string lichThiBaseApi { get; set; } = "http://tmsappapi.utc2.edu.vn";

        private static string getLichThiByHocKy { get; set; } = "/ServiceNotification_SV.svc/GetLichThiByHocKySV";
        #endregion

        #region ktx
        private static string ktxBaseApi = "http://apiktxnew.utc2.edu.vn";
        private static string getLichSuKTX = "/Service_DangKyKTX.svc/jsonRentHistory";

        // http://apiktxnew.utc2.edu.vn/Service_DangKyKTX.svc/jsonRentHistory
        #endregion

        // =====================================
        #region DKHP
        public static string LoginApi()
        {
            return baseApi + loginApi;
        }

        public static string DSDotDKApi(AuthModel auth)
        {
            if (auth.result == null)
            {
                return baseApi + dSDotDKApi;
            }

            string queries = $"?KHOA_NGANH_ID={auth.result[0].khoA_NGANH_ID}&SINHVIEN_ID={auth.result[0].sinhvieN_ID}&ID_NGANH={auth.result[0].iD_NGANH}&ID_HEDAOTAO={auth.result[0].iD_HEDAOTAO}&id_khoa={auth.result[0].id_khoa}&LOPHOC_ID={auth.result[0].lophoC_ID}&ID_KHOAHOC={auth.result[0].iD_KHOAHOC}&id_cn={auth.result[0].id_cn}&ID_SINHVIEN_NAMHOC={auth.result[0].iD_SINHVIEN_NAMHOC}&MA_DVIQLY={auth.result[0].mA_DVIQLY}&NIENCHE_OR_TINCHI={auth.result[0].nienchE_OR_TINCHI}&NamHocKy={auth.result[0].namHocKy}&SoHocKy={auth.result[0].soHocKy}&HocKyTruoc={auth.result[0].hocKyTruoc}";


            return baseApi + dSDotDKApi + queries;
        }

        public static string KetQuaDKApi(AuthModel auth, int id_dot_Dk)
        {
            if (auth.result == null)
            {
                return baseApi + ketQuaDKApi;
            }
            string queries = $"?MA_DVIQLY={auth.result[0].mA_DVIQLY}&NIENCHE_OR_TINCHI={auth.result[0].nienchE_OR_TINCHI}&SINHVIEN_ID={auth.result[0].sinhvieN_ID}&ID_NGANH={auth.result[0].iD_NGANH}&ID_KHOAHOC={auth.result[0].iD_KHOAHOC}&ID_HP_THAMSO={id_dot_Dk}";

            return baseApi + ketQuaDKApi + queries;
        }

        public static string DanhSachHocPhanApi(AuthModel auth, int idMonHoc)
        {
            if (auth.result == null)
            {
                return baseApi + ketQuaDKApi;
            }
            string queries = $"?KHOA_NGANH_ID={auth.result[0].khoA_NGANH_ID}&pIsMobile={1}&MonHoc={idMonHoc}&SINHVIEN_ID={auth.result[0].sinhvieN_ID}&ID_NGANH={auth.result[0].iD_NGANH}&ID_HEDAOTAO={auth.result[0].iD_HEDAOTAO}&id_khoa={auth.result[0].id_khoa}&LOPHOC_ID={auth.result[0].lophoC_ID}&ID_KHOAHOC={auth.result[0].iD_KHOAHOC}&id_cn={auth.result[0].id_cn}&ID_SINHVIEN_NAMHOC={auth.result[0].iD_SINHVIEN_NAMHOC}&MA_DVIQLY={auth.result[0].mA_DVIQLY}&NIENCHE_OR_TINCHI={auth.result[0].nienchE_OR_TINCHI}&NamHocKy={auth.result[0].namHocKy}&SoHocKy={auth.result[0].soHocKy}&HocKyTruoc={auth.result[0].hocKyTruoc}";

            return baseApi + danhSachHocPhanApi + queries;
        }

        public static string DangKyApi()
        {
            return baseApi + dangKyApi;
        }

        public static string HuyKQDKApi()
        {
            return (baseApi + huyKQDKApi);
        }

        #endregion

        #region thong bao

        public static string GetThongBaoChungApi(int currentPage = 1, int pageSize = 20)
        {
            string sortField = "created_at";
            string sortOrder = "DESC";
            string filters = "type%3D%3DSTUDENT_ANNOUNCEMENT%2Cdisplay%3D%3Dtrue%2C%20%20";
            string subCategorys = "";
            string _params = $@"?currentPage={currentPage}&pageSize={pageSize}&sortField={sortField}&sortOrder={sortOrder}&filters={filters}&subCategorys={subCategorys}";

            return baseWebApi + getThongBaoChungApi + _params;
        }

        public static string GetTKBLichThiApi(int currentPage = 1, int pageSize = 20)
        {
            string sortField = "created_at";
            string sortOrder = "DESC";
            string filters = "type%3D%3DSCHEDULE_ANNOUNCEMENT%2C%20display%3D%3Dtrue%2C%20%20";
            string subCategorys = "";
            string _params = $@"?currentPage={currentPage}&pageSize={pageSize}&sortField={sortField}&sortOrder={sortOrder}&filters={filters}&subCategorys={subCategorys}";
            return baseWebApi + tKBLichThiAPi + _params;
        }

        public static string GetKeHoachnamHocApi(int currentPage = 1, int pageSize = 20)
        {
            string sortField = "created_at";
            string sortOrder = "DESC";
            string filters = "type%3D%3DPOST%2Cdisplay%3D%3Dtrue%2C%2C%0A%20%20%20%20%20%20%20%20%2C%0A%20%20%20%20%20%20%20%20";
            string subCategorys = "c02d0836-3a5b-4eae-99e1-7217018dfa11";
            string _params = $@"?currentPage={currentPage}&pageSize={pageSize}&sortField={sortField}&sortOrder={sortOrder}&filters={filters}&subCategorys={subCategorys}";
            return baseWebApi + keHoachNamHocApi + _params;
        }

        public static string AccessThongBaoChungWeb(string url, int currentUrlId)
        {
            if (currentUrlId == 0)
            {
                return accessThongBaoChungWeb + url;
            }
            else if (currentUrlId == 1)
            {
                return accessTKBLichThiWeb + url;
            }
            return accessKeHoachNamHocWeb + url;
        }

        #endregion

        #region hocPhi

        public static string GetHocPhiApi()
        {
            return hocPhiBaseApi + getHocPhiApi;
        }

        #endregion

        #region lich thi

        public static string GetLichThiByHocKy()
        {
            return lichThiBaseApi + getLichThiByHocKy;
        }

        #endregion

        #region KTX

        public static string GetLichSuKTX()
        {
            return ktxBaseApi + getLichSuKTX;
        }

        #endregion
    }
}
