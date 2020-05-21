using EntityLibrary;
using Newtonsoft.Json;
using RepositoryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;

namespace WebApplication.Models
{
    public static class MyScheduler
    {
        public static void IntervalInSeconds(int hour, int sec, double interval, Action task)
        {
            interval = interval / 3600;
            SchedulerService.Instance.ScheduleTask(hour, sec, interval, task);
        }
        public static void IntervalInMinutes(int hour, int min, double interval, Action task)
        {
            interval = interval / 60;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }
        public static void IntervalInHours(int hour, int min, double interval, Action task)
        {
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }
        public static void IntervalInDays(int hour, int min, double interval, Action task)
        {
            interval = interval * 24;
            SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
        }

        public static List<Timer> TimerList()
        {
            return SchedulerService.Instance.TimerList();
        }

        public static void StopTimerList()
        {
            SchedulerService.Instance.StopTimerList();
        }

        public static void SetTimer(int? UserID = 0)
        {
            try
            {
                if (UserID == 0)
                {
                    return;
                }
                MyScheduler.StopTimerList();


                string emailStr = WebApiServiceClass.GetEmailSettingsList();

                EmailSettings email = JsonConvert.DeserializeObject<List<EmailSettings>>(emailStr).FirstOrDefault();

                SendEmail sEmail = new SendEmail(email, UserID);

                List<TargetApps> targetappList = new List<TargetApps>();

                string strTargetApp = WebApiServiceClass.GetTargetAppList();

                targetappList = JsonConvert.DeserializeObject<List<TargetApps>>(strTargetApp);

                foreach (var app in targetappList.Where(p => p.UserLoginID == UserID))
                {
                    if (app.IntervalType == "H")
                    {
                        MyScheduler.IntervalInHours(DateTime.Now.Hour, DateTime.Now.Minute, app.TimeInterval ?? 1,
                                 () =>
                                 {
                                     CheckSite(app.TargetUrl, UserID, sEmail);
                                 });
                    }
                    if (app.IntervalType == "M")
                    {
                        MyScheduler.IntervalInMinutes(DateTime.Now.Hour, DateTime.Now.Minute, app.TimeInterval ?? 10,
                                () =>
                                {
                                    CheckSite(app.TargetUrl, UserID, sEmail);
                                });
                    }
                    if (app.IntervalType == "S")
                    {
                        MyScheduler.IntervalInSeconds(DateTime.Now.Hour, DateTime.Now.Minute, app.TimeInterval ?? 30,
                                () =>
                                {
                                    CheckSite(app.TargetUrl, UserID, sEmail);
                                });
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        async public static void CheckSite(string url, int? UserID, IMessage mail)
        {
            try
            {
                HttpClient client = new HttpClient();
                var checkingResponse = await client.GetAsync(url);

                if (!checkingResponse.IsSuccessStatusCode)
                {
                    MessageManager mm = new MessageManager(mail);
                    mm.SendMessage(url);
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}