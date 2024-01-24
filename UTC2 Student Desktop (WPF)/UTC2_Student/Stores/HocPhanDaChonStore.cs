using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.API.IntermediateModels.ApiResponses;
using UTC2_Student.Repositories.IntermediateModels.ApiResponses;

namespace UTC2_Student.Stores
{
    public static class HocPhanDaChonStore
    {
        public static ObservableCollection<HocPhanDaChon>? HocPhans { get; set; }
    }
}
