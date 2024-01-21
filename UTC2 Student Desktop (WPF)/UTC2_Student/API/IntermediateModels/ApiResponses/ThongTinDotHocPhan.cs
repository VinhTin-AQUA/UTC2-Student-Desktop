using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.Repositories.IntermediateModels.ApiResponses
{
    public class ThongTinDotHocPhan
    {
        private static ThongTinDotHocPhan? instance { get; set; } = null;
        private static readonly object lockObject = new object();

        #region props
        public Count? count { get; set; } = null;

        public object? danhSachDotDangKy { get; set; } = null;

        public List<DotHocPhan> dotHocPhans { get; set; } = [];

        public List<MonHoc> monhoccombox { get; set; }

        public List<KhoaNganh> khoanganh { get; set; } = [];

        public object[] hocphan_dk { get; set; } = [];
        public object? hocphan_dk_all { get; set; } = null;
        public object? hocphan_dk_kkttuchon { get; set; } = null;
        public object[] huy { get; set; } = [];
        public List<HocPhan> hocphan_ct { get; set; } = [];
        public object[] hocphan_ct_khung { get; set; } = [];
        public object[] lichsudk { get; set; } = [];
        public object[] lichsuhuy { get; set; } = [];


        public DemDSHuy demdshuy { get; set; }
        public TSDangKyMonHoc tsdangkymonhoc { get; set; }

        public object[] tsMonHoc { get; set; } = [];
        public object[] checkedTienQuyet { get; set; } = [];
        public object[] ktcheckdt { get; set; } = [];

        public ThamSoTinChi thamsotinchi { get; set; }

        public int selectedKN { get; set; }
        public object? listTrangthaimon { get; set; } = null;
        public object? listTrangthaimontttn { get; set; } = null;
        public object? listHocphantienQuyet { get; set; } = null;



        #endregion

        // private: ngăn chặn tạo đối tượng từ bên ngoài
        private ThongTinDotHocPhan()
        {

        }

        public static ThongTinDotHocPhan Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new ThongTinDotHocPhan();
                        }
                    }
                }
                return instance;
            }
            set { instance = value; }
        }
    }
}
