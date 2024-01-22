using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.API.IntermediateModels.NotificationResponses;
using UTC2_Student.MVVM.Core;
using UTC2_Student.Repositories;

namespace UTC2_Student.MVVM.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private NotificationResponse notificationResponse;

        private int currentPage;
        private int totalPages;
        private List<Notice> notices;


        public NotificationResponse NoTificationResponse
        {
            get { return notificationResponse; }
            set { notificationResponse = value; OnPropertyChanged(); }
        }

        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged(); }
        }

        public int TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; OnPropertyChanged(); }
        }

        public List<Notice> Notices
        {
            get { return notices; }
            set { notices = value; OnPropertyChanged(); }
        }

        public ICommand PrevPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }


        public NotificationViewModel()
        {
            Task.Run(() => GetThongBao());

            PrevPageCommand = new AsyncRelayCommand(ExexutePrevPageCommand);
            NextPageCommand = new AsyncRelayCommand(ExecuteNextPageCommand);
            //CurrentPage = 1;
            //TotalPages = 1;
        }

        private async Task GetThongBao()
        {
            NoTificationResponse = await ApiRepository.Ins.GetThongBaos(1);
            CurrentPage = NoTificationResponse.responseData.currentPage;
            TotalPages = NoTificationResponse.responseData.totalPages;
            Notices = NoTificationResponse.responseData.rows;
        }

        private async Task ExexutePrevPageCommand(object obj)
        {
            CurrentPage--;
            if(CurrentPage <= 0)
            {
                CurrentPage = 1;
            }
            NoTificationResponse = await ApiRepository.Ins.GetThongBaos(CurrentPage);
            Notices = NoTificationResponse.responseData.rows;
        }

        private async Task ExecuteNextPageCommand(object obj)
        {
            CurrentPage++;
            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }
            NoTificationResponse = await ApiRepository.Ins.GetThongBaos(CurrentPage);
            Notices = NoTificationResponse.responseData.rows;
        }

    }
}
