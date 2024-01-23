using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.KTX
{
    public class LichSuKTX
    {
        public string SemesterName { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string CreatedDate { get; set; }
        public string DormitoryRoomTypeName { get; set; }
        public string HouseName { get; set; }
        public string DormitoryRoomName { get; set; }
        public string DormitoryRoomSlotName { get; set; }
        public string Price { get; set; }
        public string Mess { get; set; }
        public int StatusCode { get; set; }
    }
}
