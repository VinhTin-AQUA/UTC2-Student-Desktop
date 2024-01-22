using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.HocPhi
{
    public class HocPhiModel
    {
        public float STT { get; set; }
        public bool CHON { get; set; }
        public string LOAI { get; set; }
        public string TEN { get; set; }
        public decimal PHAI_THU { get; set; }
        public object DANGKY_ID { get; set; }
        public string NGAY_DKY { get; set; }
        public string NGAY_DKY1 { get; set; }
        public decimal THU_DUOC { get; set; }
        public string GHI_CHU { get; set; }
        public string THU_TIEN { get; set; }
        public float DANGKY_NOP_ID { get; set; }
        public object HOCPHI_THILAN2_ID { get; set; }
        public object ID_DOTTHILAN2 { get; set; }
        public object ID_DOT_KTX { get; set; }
        public object ID_DANGKY_KTX { get; set; }
        public object ID_DOT_HL { get; set; }
        public object ID_DANGKY_HL { get; set; }
        public object ID_DOT_TL { get; set; }
        public object ID_DANGKY_TL { get; set; }
        public object NAM_HOCID { get; set; }
        public object NAMHOC_DOTID { get; set; }
        public object ID_DANGKY_HP { get; set; }
        public object TENDOT { get; set; }
    }
}
