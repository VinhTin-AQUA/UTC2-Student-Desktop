using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.Repositories.IntermediateModels.ApiResponses
{
    /* đối tượng của danh sách lớp học phần theo môn và đối tượng danh sách lớp học phần đã đăng ký nằm chung trong HocPhan */
    public class HocPhan
    {
        public string tuChon { get; set; } = "";
        public int iD_KHOA_NGANH_CTIET { get; set; }
        public int sO_TC { get; set; }
        public string teN_MONHOC { get; set; } = "";
        public string mA_MONHOC { get; set; } = "";
        public string mA_LOP { get; set; } = "";
        public decimal thanH_TIEN { get; set; }
        public string teN_LOP { get; set; } = "";
        public int soSVDK { get; set; }
        public int loP_HOCPHAN_CTIET_ID { get; set; }
        public string ngaY_DKY { get; set; } = "";
        public int sinhvieN_ID { get; set; }
        public int iD_NGANH { get; set; }
        public string hoC_KY { get; set; } = "";
        public int lanHocMH { get; set; }
        public string creatE_USER { get; set; } = "";
        public int iD_KHOAHOC { get; set; }
        public int idkhoikienthuc { get; set; }
        public int iD_MONHOC { get; set; }
        public int loP_HOCPHAN_CTIET_ID_CHA { get; set; }
        public int is_COHOCPHAN_THUCHANH { get; set; }
        public string tkb { get; set; } = "";
        public int lop_hocphan_id { get; set; }
        public int sO_SVIEN { get; set; }
        public object? iD_MONHOC_HOCTRUOC { get; set; }
        public object? iD_MONHOC_TIENQUYET { get; set; }
        public decimal doN_GIA { get; set; }
        public string kttkb { get; set; } = "";
        public bool checkTKB { get; set; }
        public bool checkedBoxMonHocdaotao { get; set; }
    }
}
