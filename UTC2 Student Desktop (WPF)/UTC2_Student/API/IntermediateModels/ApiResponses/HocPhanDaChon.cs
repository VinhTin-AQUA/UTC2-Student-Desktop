using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UTC2_Student.API.IntermediateModels.ApiResponses
{
    public class HocPhanDaChon : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private string status;

        public string Id 
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); } 
        }
        public string Name 
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string Status 
        {
            get {  return status; }
            set { status = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
