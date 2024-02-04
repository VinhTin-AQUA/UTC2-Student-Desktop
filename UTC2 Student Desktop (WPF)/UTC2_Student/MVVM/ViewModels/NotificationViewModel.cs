using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTC2_Student.API;
using UTC2_Student.API.IntermediateModels.NotificationResponses;
using UTC2_Student.MVVM.Core;
using UTC2_Student.MVVM.Models;
using UTC2_Student.Repositories;

namespace UTC2_Student.MVVM.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private NotificationResponse notificationResponse;

        private int currentPage;
        private int totalPages;
        private List<Notice> notices;

        public int CurrentUrlId { get; set; }

        public NotificationResponse? NoTificationResponse
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

        public List<NoticeUrlModel> NoticeTypes { get; set; }

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
            NoticeTypes = [
                new() { Id = 0, Name = "Thông báo chung", Url = Urls.GetThongBaoChungApi(1) },
                new() { Id = 1, Name = "Danh sách thời khóa biểu - lịch thi", Url = Urls.GetTKBLichThiApi(1) },
                new() { Id = 2, Name = "Kế hoạch năm học", Url = Urls.GetKeHoachnamHocApi(1) },
            ];
            CurrentUrlId = 0;
        }

        private async Task GetThongBao()
        {
            NoTificationResponse = await ApiRepository.Ins!.GetThongBao(CurrentUrlId);
            CurrentPage = NoTificationResponse!.responseData!.currentPage;
            TotalPages = NoTificationResponse.responseData.totalPages;
            Notices = NoTificationResponse.responseData.rows!;
        }

        private async Task ExexutePrevPageCommand(object obj)
        {
            CurrentPage--;
            if (CurrentPage <= 0)
            {
                CurrentPage = 1;
            }
            NoTificationResponse = await ApiRepository.Ins.GetThongBao(CurrentUrlId, CurrentPage);
            Notices = NoTificationResponse!.responseData!.rows!;
        }

        private async Task ExecuteNextPageCommand(object obj)
        {
            CurrentPage++;
            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }
            NoTificationResponse = await ApiRepository.Ins.GetThongBao(CurrentUrlId, CurrentPage);
            Notices = NoTificationResponse!.responseData!.rows!;
        }

        public async Task ExecuteChooseNoticeType(int urlId)
        {
            CurrentPage = 1;
            CurrentUrlId = urlId;
            await GetThongBao();
        }

    }
}
