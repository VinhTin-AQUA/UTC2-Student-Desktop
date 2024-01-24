using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.NotificationResponses
{
    public class NotificationResponse
    {
        public string message { get; set; } = "";

        public ResponseData? responseData { get; set; }

        public string status { get; set; } = "";
        public string timeStamp { get; set; } = "";
        public string violations { get; set; } = "";


    }
}
