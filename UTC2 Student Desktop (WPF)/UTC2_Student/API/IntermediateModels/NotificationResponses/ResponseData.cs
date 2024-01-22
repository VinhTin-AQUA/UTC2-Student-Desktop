using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.NotificationResponses
{
    public class ResponseData
    {
        public int count {  get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; } 
        public List<Notice> rows { get; set; }
    }
}
