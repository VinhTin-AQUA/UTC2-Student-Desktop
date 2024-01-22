using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.NotificationResponses
{
    public class Notice
    {
        public string id {  get; set; }
        public string image {  get; set; }
        public string title {  get; set; }
        public string sub_title {  get; set; }
        public string seo_text {  get; set; }
        public string display {  get; set; }
        public string featured {  get; set; }
        public string send_app {  get; set; }
        public string view {  get; set; }
        public string type {  get; set; }
        public string created_at {  get; set; }
        public string created_by {  get; set; }
        public string updated_at {  get; set; }
        public string updated_by {  get; set; }
    }
}
