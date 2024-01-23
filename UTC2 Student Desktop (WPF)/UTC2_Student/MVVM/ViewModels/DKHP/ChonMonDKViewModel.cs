﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UTC2_Student.API;
using UTC2_Student.API.IntermediateModels.ApiResponses;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UTC2_Student.MVVM.ViewModels.DKHP
{
    public class ChonMonDKViewModel : ViewModelBase
    {
        private List<MonHoc> monHocs;
        private List<HocPhan> hocPhans;
        private int soLopHocPhanDaChonMoiMon;
        private List<HocPhanDaChon> idHocPhans;

        public List<MonHoc> MonHocs { get { return monHocs; } set { monHocs = value; OnPropertyChanged(); } }

        public List<HocPhan> HocPhans
        {
            get { return hocPhans; }
            set { hocPhans = value; OnPropertyChanged(); }
        }

        public int SoLopHocPhanDaChonMoiMon
        {
            get { return soLopHocPhanDaChonMoiMon; }
            set { soLopHocPhanDaChonMoiMon = value; OnPropertyChanged(); }
        }

        public List<HocPhanDaChon> IdHocPhans
        {
            get { return idHocPhans; }
            set { idHocPhans = value; OnPropertyChanged(); }
        }

        public ICommand LuuIdHocPhanCommand { get; set; }


        public ChonMonDKViewModel()
        {
            LuuIdHocPhanCommand = new RelayCommand(ExecuteLuuIdHocPhanCommand);
            IdHocPhans = [new HocPhanDaChon(), new HocPhanDaChon(), new HocPhanDaChon()];
        }

        public async Task GetDSMonHoc()
        {
            MonHocs = await ApiRepository.Ins.GetDSMonHoc();

            //MonHocs = new List<MonHoc>
            //{
            //    new MonHoc {
            //        iD_MONHOC= 1,
            //        teN_MONHOC= "Sucralfate"
            //    }, new MonHoc{
            //                    iD_MONHOC= 2,
            //        teN_MONHOC= "ALUMINUM ZIRCONIUM TETRACHLOROHYDREX GLY"
            //    },  new MonHoc{
            //                    iD_MONHOC= 3,
            //        teN_MONHOC= "CONIUM MACULATUM FLOWERING TOP"
            //    }, new MonHoc {
            //                    iD_MONHOC= 4,
            //        teN_MONHOC= "dextromethorphan, guaifenesin, and phenylephrine"
            //    }, new MonHoc {
            //                    iD_MONHOC= 5,
            //        teN_MONHOC= "NALOXONE HYDROCHLORIDE"
            //    }, new MonHoc {
            //                    iD_MONHOC= 6,
            //        teN_MONHOC= "Octinoxate and Titanium Dioxide"
            //    },  new MonHoc{
            //                    iD_MONHOC= 7,
            //        teN_MONHOC= "Acetaminophen, Dextromethorphan HBr, Doxylamine Succinate"
            //    }, new MonHoc {
            //                    iD_MONHOC= 8,
            //        teN_MONHOC= "OCTINOXATE, OXYBENZONE, OCTISALATE, OCTOCRYLENE, and AVOBENZONE"
            //    }, new MonHoc {
            //                    iD_MONHOC= 9,
            //        teN_MONHOC= "norethindrone acetate and ethinyl estradiol"
            //    },  new MonHoc{
            //                    iD_MONHOC= 10,
            //        teN_MONHOC = "Risperidone"
            //    }
            //};
        }

        public async Task GetLopHocPhanByMonHoc(int idMonHoc)
        {
            HocPhans = await ApiRepository.Ins.GetDanhSachHocPhan(idMonHoc);
            //HocPhans = new List<HocPhan>
            //{
            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },
            //    new HocPhan
            //    {
            //        tuChon = "nullam sit amet turpis elementum,",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //    new HocPhan
            //    {
            //        tuChon = "nunc purus phasellus in felis donec semper",
            //        sO_TC = 3,
            //        teN_MONHOC = "nonummy maecenas tincidunt",
            //        mA_MONHOC = "nullam",
            //        mA_LOP = "vestibulum",
            //        thanH_TIEN = 8659,
            //        teN_LOP = "eu",
            //        soSVDK = 86,
            //        loP_HOCPHAN_CTIET_ID = 2,
            //        ngaY_DKY = "6/27/2023",
            //        sinhvieN_ID = 8161,
            //        iD_NGANH = 9238,
            //        hoC_KY = " vestibulum sagittis sapien cum sociis",
            //        lanHocMH = 3,
            //        creatE_USER = "sed ante vivamus tortor duis",
            //        iD_KHOAHOC = 10,
            //        idkhoikienthuc = 1,
            //        iD_MONHOC = 7,
            //        loP_HOCPHAN_CTIET_ID_CHA = 9,
            //        tkb = "lacus purus aliquet at feugiat non pretium"
            //    },

            //};
        }

        private void ExecuteLuuIdHocPhanCommand(object obj)
        {
            var hocPhan = new HocPhanDaChon();
            if (string.IsNullOrEmpty(IdHocPhans[1].id) == true)
            {
                hocPhan.id = IdHocPhans[0].id;
                hocPhan.name = IdHocPhans[0].name;
            }
            else
            {
                hocPhan.id = IdHocPhans[0].id +","+ IdHocPhans[1].id;
                hocPhan.name = IdHocPhans[0].name + ", "+ IdHocPhans[1].name;
            }

            DataHelper.SaveIdHocPhans(hocPhan);
            Reset();
        }

        public void Reset()
        {
            SoLopHocPhanDaChonMoiMon = 0;
            IdHocPhans[0].id = "";
            IdHocPhans[0].name = "";
            IdHocPhans[1].id = "";
            IdHocPhans[1].name = "";
            IdHocPhans[2].id = "";
            IdHocPhans[2].name = "";
        }
    }
}
