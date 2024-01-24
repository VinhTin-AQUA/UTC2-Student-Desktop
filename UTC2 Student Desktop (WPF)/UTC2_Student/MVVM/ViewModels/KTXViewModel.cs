using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.API.IntermediateModels.KTX;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;

namespace UTC2_Student.MVVM.ViewModels
{
    public class KTXViewModel : ViewModelBase
    {
        private LichSuKTX lichSuKTXs;
        private string status;
        private string statusText;

        public LichSuKTX LichSuKTXs
        {
            get { return lichSuKTXs; }
            set { lichSuKTXs = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get { return statusText; }
            set { statusText = value; OnPropertyChanged(); }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public KTXViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Status = "Visible";
            StatusText = "Hidden";
        }

        public async Task GetLichSuKTX()
        {
            var r = await ApiRepository.Ins.GetLichSuKTX();
            if( r == null )
            {
                Status = "Hidden";
                StatusText = "Visible";
            } else
            {
                LichSuKTXs = r[0];
                Status = "Visible";
                StatusText = "Hidden";
            }
        }
    }
}
