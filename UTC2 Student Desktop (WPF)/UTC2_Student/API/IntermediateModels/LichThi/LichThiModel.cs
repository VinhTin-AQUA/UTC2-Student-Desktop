using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.LichThi
{
    public class LichThiModel
    {    
        public string NGAY_THI { get; set; }
        public decimal LOPHOCPHAN_ID { get; set; }
        public decimal LICHTHI_ID { get; set; }
        public decimal LOP_HOCPHAN_CTIET_ID { get; set; }
        public string GIOTHI { get; set; }
        public string MONTHI { get; set; }
        public string MAHP { get; set; }
        public string LANTHI { get; set; }
        public string NAM_HOC { get; set; }
        public string HOC_KY { get; set; }
        public string PHONG_THI { get; set; }
        public decimal ID_HP_THAMSO { get; set; }
    }
}
