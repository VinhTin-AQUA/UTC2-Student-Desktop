using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.Repositories.IntermediateModels.ApiResponses
{
    public class KhoaNganh
    {
        public int khoA_NGANH_ID { get; set; }
        public int iD_KHOAHOC { get; set; }
        public int iD_NGANH { get; set; }
        public string teN_KHOAHOC { get; set; } = "";
        public string teN_NGANH { get; set; } = "";
        public string caP_NGANH { get; set; } = "";
        public int iD_NGANH_CHA { get; set; }
    }
}
