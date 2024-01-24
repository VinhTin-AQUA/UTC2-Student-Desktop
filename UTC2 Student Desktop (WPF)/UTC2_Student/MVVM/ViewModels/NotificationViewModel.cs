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


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public NotificationViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Task.Run(() => GetThongBao());

            PrevPageCommand = new AsyncRelayCommand(ExexutePrevPageCommand);
            NextPageCommand = new AsyncRelayCommand(ExecuteNextPageCommand);
            //CurrentPage = 1;
            //TotalPages = 1;
        }

        private async Task GetThongBao()
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            NoTificationResponse = await ApiRepository.Ins.GetThongBaos(1);
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            CurrentPage = NoTificationResponse.responseData.currentPage;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            TotalPages = NoTificationResponse.responseData.totalPages;
            Notices = NoTificationResponse.responseData.rows!;
        }

        private async Task ExexutePrevPageCommand(object obj)
        {
            CurrentPage--;
            if(CurrentPage <= 0)
            {
                CurrentPage = 1;
            }
#pragma warning disable CS8601 // Possible null reference assignment.
            NoTificationResponse = await ApiRepository.Ins.GetThongBaos(CurrentPage);
#pragma warning restore CS8601 // Possible null reference assignment.
            Notices = NoTificationResponse!.responseData!.rows!;
        }

        private async Task ExecuteNextPageCommand(object obj)
        {
            CurrentPage++;
            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }
#pragma warning disable CS8601 // Possible null reference assignment.
            NoTificationResponse = await ApiRepository.Ins.GetThongBaos(CurrentPage);
#pragma warning restore CS8601 // Possible null reference assignment.
            Notices = NoTificationResponse!.responseData!.rows!;
        }

    }
}
