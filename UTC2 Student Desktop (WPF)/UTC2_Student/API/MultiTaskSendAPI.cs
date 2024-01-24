using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UTC2_Student.Repositories;
using UTC2_Student.Repositories.IntermediateModels.Auth;
using UTC2_Student.Stores;

namespace UTC2_Student.API
{
    public class MultiTaskSendAPI
    {
        static int completedTasksTotal = 0;
        static List<CancellationTokenSource> ctss; // quản lý trạng thái run của các tác vụ A,B
        CancellationTokenSource loginSource = null;

        private static MultiTaskSendAPI ins = null;

        public static MultiTaskSendAPI Ins
        {
            get 
            { 
                if (ins == null)
                {
                    ins = new MultiTaskSendAPI();
                }
                return ins; 
            }
        }


        public MultiTaskSendAPI()
        {
            
        }

        public void SendMessage(string id, string message)
        {
            var hocPhan = HocPhanDaChonStore.HocPhans
                .Where(h => h.Id == id)
                .FirstOrDefault();
            hocPhan.Status = message;
        }

        public void LoggedInFail(string id)
        {

        }

        public async Task DangKy(List<string> ids)
        {
            // đợi 5p rồi mới đăng nhập
            //await Task.Delay(TimeSpan.FromMinutes(5));

            var response = await ApiRepository.Ins.Login(LoginModel.Instance.MSSV, LoginModel.Instance.Password);

            ctss = new List<CancellationTokenSource>();
            await DangKyHP(ids);

            Console.WriteLine("Da huy");
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
                    var response = await ApiRepository.Ins.GuiApiDK(id);
                    string content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode == true)
                    {
                        completedTasksTotal++;

                        // đăng ký thành công
                        if (content == "\"Y|\"")
                        {
                            //Notice notice = new Notice
                            //{
                            //    IdHocPhan = id,
                            //    Status = true,
                            //    Message = "Đăng ký thành công"
                            //};
                            SendMessage(id, content);
                            break;
                        }

                        //đăng ký lại môn đã đăng ký
                        if (content == "\"N|Bạn chưa chọn học phần hoặc đã chọn học phần thực hành mà không chọn học phần lý thuyết đi kèm!.\"")
                        {
                            //Notice notice = new Notice
                            //{
                            //    IdHocPhan = id,
                            //    Status = true,
                            //    Message = "N|Bạn chưa chọn học phần hoặc đã chọn học phần thực hành mà không chọn học phần lý thuyết đi kèm!."
                            //};
                            SendMessage(id, content);
                            break;
                        }

                        //đăng ký lại môn đã đăng ký
                        if (content == "\"N|Chưa tới thời gian đăng ký hoặc đã hết hạn.\"")
                        {
                            //Notice notice = new Notice
                            //{
                            //    IdHocPhan = id,
                            //    Status = true,
                            //    Message = "\"N|Chưa tới thời gian đăng ký hoặc đã hết hạn.\""
                            //};
                            SendMessage(id, content);
                            break;
                        }

                        //đăng ký lại môn đã đăng ký
                        if (content == "\"N|Lỗi thực hiện, xin vui lòng thực hiện sau: Object reference not set to an instance of an object..\"")
                        {
                            //Notice notice = new Notice
                            //{
                            //    IdHocPhan = id,
                            //    Status = true,
                            //    Message = "\"N|Chưa tới thời gian đăng ký hoặc đã hết hạn.\""
                            //};
                            SendMessage(id, content);
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
                var response = await ApiRepository.Ins.Login(LoginModel.Instance.MSSV, LoginModel.Instance.Password);

                // sau khi làm gì đó thì cho 2 tác vụ A,B tiếp tục
                for (int i = 0; i < ctss.Count(); i++)
                {
                    ctss[i] = new CancellationTokenSource();
                }
            }
        }

        private void Reset()
        {
            if (ctss != null)
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
            if (loginSource != null)
            {
                loginSource.Cancel();
            }
        }
    }
}
