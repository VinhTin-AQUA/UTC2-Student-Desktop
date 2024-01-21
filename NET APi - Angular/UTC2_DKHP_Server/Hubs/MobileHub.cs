using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using UTC2_DKHP_Server.IRepositories;
using UTC2_DKHP_Server.Models;
using UTC2_DKHP_Server.Models.Login;
using UTC2_DKHP_Server.Repositories;
using UTC2_DKHP_Server.Services;

namespace UTC2_DKHP_Server.Hubs
{
    public class MobileHub : Hub
    {
        private readonly IMobileHocPhanRepository mobileHocPhanRepository;
        static int completedTasksTotal = 0;
        static List<CancellationTokenSource> ctss; // quản lý trạng thái run của các tác vụ A,B
        CancellationTokenSource loginSource = null;

        public MobileHub(IMobileHocPhanRepository mobileHocPhanRepository)
        {
            this.mobileHocPhanRepository = mobileHocPhanRepository;
        }

        //public override async Task OnConnectedAsync()
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, "Come2Chat");
        //    await Clients.Caller.SendAsync("UserConected");
        //}

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "DKHP");
        //    await base.OnDisconnectedAsync(exception);
        //}

        public async Task SendMessage(Notice notice)
        {
            await Clients.All.SendAsync("ReceiveMessage", notice);
        }

        public async Task LoggedInFail()
        {
            await Clients.All.SendAsync("LoggedInFail", false);
        }

        public async Task DangKy(string mssv, string password, List<string> ids)
        {
            LoginModel.Instance.MSSV = mssv;
            LoginModel.Instance.Password = password;

            // đợi 5p rồi mới đăng nhập
            //await Task.Delay(TimeSpan.FromMinutes(5));

            var response = await mobileHocPhanRepository.Login(LoginModel.Instance);

            while (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await LoggedInFail();
                    return;
                }
                response = await mobileHocPhanRepository.Login(LoginModel.Instance);
            }

            // lấy nội dung trả về ở dạng chuỗi
            var content = await response.Content.ReadAsStringAsync();

            // chuyển sang json cần có 1 đối tượng với các trường dữ liệu tương ứng
            AuthModel.Instance = JsonConvert.DeserializeObject<AuthModel>(content);

            ctss = new List<CancellationTokenSource>();
            await DangKyHP(ids);
            Console.WriteLine("Da huy");
            await Clients.All.SendAsync("Test", "Đăng ký xong");
        }

        /* ==================================================================== */
        private async Task DangKyHP(List<string> ids)
        {
            if (ids.Count() <= 0)
            {
                return;
            }

            ctss = new List<CancellationTokenSource>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < ids.Count(); i++)
            {
                ctss.Add(new CancellationTokenSource());
            }
            loginSource = new CancellationTokenSource();
            //Console.WriteLine("Dang cho toi thoi diem dang ky");
            //await Task.Delay(timeSpan);
            //Console.WriteLine("Start");

            for (int i = 0; i < ids.Count(); i++)
            {
                int index = i;
                tasks.Add(Task.Run(() => GuiAPIDangKy(index, ids[index].Trim()), ctss[index].Token));
            }
            tasks.Add(Task.Run(() => LoginAgain(), loginSource.Token));

            // thực thi tác vụ
            await Task.WhenAll(tasks);

            Reset();
        }

        private async Task GuiAPIDangKy(int cancelIndex, string id)
        {
            while (true)
            {
                if (ctss[cancelIndex].Token.IsCancellationRequested == false)
                {
                    // đăng ký
                    var response = await mobileHocPhanRepository.GuiApiDK(id);
                    string content = await response.Content.ReadAsStringAsync();


                    if (response.IsSuccessStatusCode)
                    {
                        completedTasksTotal++;

                        // đăng ký thành công
                        if (content == "\"Y|\"")
                        {
                            Notice notice = new Notice
                            {
                                IdHocPhan = id,
                                Status = true,
                                Message = "Đăng ký thành công"
                            };
                            await SendMessage(notice);
                            break;
                        }

                        //đăng ký lại môn đã đăng ký
                        if (content == "\"N|Bạn chưa chọn học phần hoặc đã chọn học phần thực hành mà không chọn học phần lý thuyết đi kèm!.\"")
                        {
                            Notice notice = new Notice
                            {
                                IdHocPhan = id,
                                Status = true,
                                Message = "N|Bạn chưa chọn học phần hoặc đã chọn học phần thực hành mà không chọn học phần lý thuyết đi kèm!."
                            };
                            await SendMessage(notice);
                            break;
                        }

                        //đăng ký lại môn đã đăng ký
                        if (content == "\"N|Chưa tới thời gian đăng ký hoặc đã hết hạn.\"")
                        {
                            Notice notice = new Notice
                            {
                                IdHocPhan = id,
                                Status = true,
                                Message = "\"N|Chưa tới thời gian đăng ký hoặc đã hết hạn.\""
                            };
                            await SendMessage(notice);
                            break;
                        }
                    }
                } 
                else
                {
                    Console.WriteLine("break");
                    break;
                }
            }
        }

        private async Task LoginAgain()
        {
            while (loginSource.Token.IsCancellationRequested == false)
            {
                await Task.Delay(TimeSpan.FromMinutes(4.5), loginSource.Token); //

                // nếu tác vụ A,B hoàn thành có nghĩa completedTasksTotal == 2 thì kết thúc tác vụ
                if (completedTasksTotal == ctss.Count())
                {
                    loginSource.Cancel();
                    break;
                }

                // cancel để dừng tác vụ A,B
                for (int i = 0; i < ctss.Count(); i++)
                {
                    ctss[i].Cancel();
                }

                // login
                var response = await mobileHocPhanRepository.Login(LoginModel.Instance);

                while (!response.IsSuccessStatusCode)
                {
                    response = await mobileHocPhanRepository.Login(LoginModel.Instance);
                }

                // lấy nội dung trả về ở dạng chuỗi
                var content = await response.Content.ReadAsStringAsync();

                // chuyển sang json cần có 1 đối tượng với các trường dữ liệu tương ứng
                AuthModel.Instance = JsonConvert.DeserializeObject<AuthModel>(content);

                // sau khi làm gì đó thì cho 2 tác vụ A,B tiếp tục
                for (int i = 0; i < ctss.Count(); i++)
                {
                    ctss[i] = new CancellationTokenSource();
                }
            }
        }

        private void Reset()
        {
            if(ctss != null)
            {
                for (int i = 0; i < ctss.Count(); i++)
                {
                    if (ctss[i] != null)
                    {
                        ctss[i].Cancel();
                    }
                }
            }
            
            completedTasksTotal = 0;
            if(loginSource != null)
            {
                loginSource.Cancel();
            }
        }
    }
}
