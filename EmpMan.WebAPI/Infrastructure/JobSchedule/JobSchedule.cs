using EmpMan.Common.Enums;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Service;
using EmpMan.Web.Controllers;
using EmpMan.Web.Infrastructure.Core;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web;

namespace EmpMan.Web.Infrastructure.JobSchedule
{
    public class SendSMSJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //var permissions = ServiceFactory.Get<IJobSchedulerService>().GetAll();

            //using (var message = new MailMessage("user@gmail.com", "user@live.co.uk"))
            //{
            //    message.Subject = "Test";
            //    message.Body = "Test at " + DateTime.Now;
            //    using (SmtpClient client = new SmtpClient
            //    {
            //        EnableSsl = true,
            //        Host = "smtp.gmail.com",
            //        Port = 587,
            //        Credentials = new NetworkCredential("user@gmail.com", "password")
            //    })
            //    {
            //        client.Send(message);
            //    }
            //}
        }
    }

    public class SendSmsDevInterviewDateNotifyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //var httpClient = HttpClientHelper.GetHttpClient(context.JobDetail.JobDataMap["context"] as HttpContext);
            //HttpResponseMessage response = httpClient.GetAsync("http://localhost:5000/" + "api/dept/getall").Result;

            /*
            var job = ServiceFactory.Get<IJobSchedulerService>().GetDevInterviewDateNotifyJob(EmailOrSmsNotify.SMS);
            foreach ( var item in job)
            {
                String resultSmsSend = SmsHelper.SmsHelper.sendSpeedSMS(new string[] { item.Item1 }, item.Item2, 4, "");
            }
            */
            IDbFactory dbFactory;
            IJobSchedulerRepository repository;
            IUnitOfWork unitOfWork;

            dbFactory = new DbFactory();
            repository = new JobSchedulerRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
            var job = repository.GetDevInterviewDateNotifyJob(NotifyMethod.SMS);
            foreach (var item in job)
            {
                String resultSmsSend = SmsHelper.SmsHelper.sendSpeedSMS(new string[] { item.Item1 }, item.Item2, 4, "");
                JObject json = JObject.Parse(resultSmsSend);
                //cap nhat gia tri sau khi da gui tin nhan
                int[] idArray = item.Item3;
                int jobStatus = 1;
                if (json.GetValue("status").ToString() == "success")
                {
                    //thanh cong 
                    jobStatus = 9;
                }
                if (json.GetValue("status").ToString() == "error")
                {
                    //that bai
                    jobStatus = 2;
                }
                repository.UpdateDevInterviewDateNotifyList(NotifyMethod.SMS, idArray, jobStatus, resultSmsSend);

            }


        }
    }

    public class RecruitmentUpdateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //kiểm tra danh sách các 
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/yourcustomobjects").Result;
            if (response.IsSuccessStatusCode)
            {
                //var yourcustomobjects = response.Content.ReadAsAsync<IEnumerable<YourCustomObject>>().Result;
                //foreach (var x in yourcustomobjects)
                //{
                //    //Call your store method and pass in your own object
                //    string sql = @"UPDATE RES 
                //                    SET 
                //                     InterviewResult =N'Không chọn phỏng vấn'
                //                    FROM RecruitmentStaffs RES
                //                    WHERE
                //                     (
                //                      RES.ID 	IN ( 
                //                         select 
                //                          RES.ID
                //                         from 
                //                          RecruitmentStaffs RES
                //                          LEFT JOIN Recruitments REC ON ( RES.RecruitmentID = REC.ID AND ISNULL(REC.IsFinished ,0)=0)
                //                          LEFT OUTER JOIN RecruitmentInterviews REI ON (RES.RecruitmentID = REI.RecruitmentID AND RES.RecruitmentStaffID = REI.RecruitmentStaffID) 
                //                         where  REI.RecruitmentID IS NULL AND  DATEDIFF(MINUTE, REC.AnsRecruitDeptDeadlineDate , GETDATE()) >0
                //                         )
                //                     )";
                //}
            }
            else
            {
                //Something has gone wrong, handle it here
            }

        }
    }
    public class JobSchedule
    {
        public static void StartSendSmsDevInterviewDateNotifyJob(int jobIntervalInMinutes = 5)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendSmsDevInterviewDateNotifyJob>().Build();
            //job.JobDataMap["context"] = httpContext;

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(jobIntervalInMinutes)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Xu ly tin nhan cho het han thu viec
        /// </summary>
        public static void StartSendSmsTrialStaffEndTrialDateNotifyJob(int jobIntervalInMinutes = 60)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendSmsTrialStaffEndTrialDateNotifyJob>().Build();
            //job.JobDataMap["context"] = httpContext;

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(jobIntervalInMinutes)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Hop dong chinh thuc
        /// </summary>
        public static void StartSendSmsTrialStaffToDevContractDateNotifyJob(int jobIntervalInMinutes = 60)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendSmsTrialStaffToDevContractDateNotifyJob>().Build();
            //job.JobDataMap["context"] = httpContext;

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(jobIntervalInMinutes)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Chuan bi vao thu viec
        /// </summary>
        public static void StartSendSmsTrialStaffStartTrialDateNotifyJob(int jobIntervalInMinutes = 60)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendSmsTrialStaffStartTrialDateNotifyJob>().Build();
            //job.JobDataMap["context"] = httpContext;

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(2)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

    }

    public class SendSmsTrialStaffEndTrialDateNotifyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            IDbFactory dbFactory;
            IJobSchedulerRepository repository;
            IUnitOfWork unitOfWork;

            dbFactory = new DbFactory();
            repository = new JobSchedulerRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
            var job = repository.GetTrialStaffEndTrialDateNotifyJob(NotifyMethod.SMS);
            foreach (var item in job)
            {
                String resultSmsSend = SmsHelper.SmsHelper.sendSpeedSMS(new string[] { item.Item1 }, item.Item2, 4, "");
                JObject json = JObject.Parse(resultSmsSend);
                //cap nhat gia tri sau khi da gui tin nhan
                int[] idArray = item.Item3;
                int jobStatus = 1;
                if (json.GetValue("status").ToString() == "success")
                {
                    //thanh cong 
                    jobStatus = 9;
                }
                if (json.GetValue("status").ToString() == "error")
                {
                    //that bai
                    jobStatus = 2;
                }
                repository.UpdateTrialStaffEndTrialDateNotifyList(NotifyMethod.SMS, idArray, jobStatus, resultSmsSend);

            }
        }
    }

    public class SendSmsTrialStaffToDevContractDateNotifyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            IDbFactory dbFactory;
            IJobSchedulerRepository repository;
            IUnitOfWork unitOfWork;

            dbFactory = new DbFactory();
            repository = new JobSchedulerRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
            var job = repository.GetTrialStaffToDevContractDateNotifyJob(NotifyMethod.SMS);
            foreach (var item in job)
            {
                String resultSmsSend = SmsHelper.SmsHelper.sendSpeedSMS(new string[] { item.Item1 ,"0967808590"}, item.Item2, 4, "");
                JObject json = JObject.Parse(resultSmsSend);
                //cap nhat gia tri sau khi da gui tin nhan
                int[] idArray = item.Item3;
                int jobStatus = 1;
                if (json.GetValue("status").ToString() == "success")
                {
                    //thanh cong 
                    jobStatus = 9;
                }
                if (json.GetValue("status").ToString() == "error")
                {
                    //that bai
                    jobStatus = 2;
                }
                repository.UpdateTrialStaffToDevContractDateNotifyList(NotifyMethod.SMS, idArray, jobStatus, resultSmsSend);

            }
        }
    }

    public class SendSmsTrialStaffStartTrialDateNotifyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            IDbFactory dbFactory;
            IJobSchedulerRepository repository;
            IUnitOfWork unitOfWork;

            dbFactory = new DbFactory();
            repository = new JobSchedulerRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
            var job = repository.GetTrialStaffStartTrialDateNotifyJob(NotifyMethod.SMS);
            foreach (var item in job)
            {
                String resultSmsSend = SmsHelper.SmsHelper.sendSpeedSMS(new string[] { item.Item1 }, item.Item2, 4, "");
                JObject json = JObject.Parse(resultSmsSend);
                //cap nhat gia tri sau khi da gui tin nhan
                int[] idArray = item.Item3;
                int jobStatus = 1;
                if (json.GetValue("status").ToString() == "success")
                {
                    //thanh cong 
                    jobStatus = 9;
                }
                if (json.GetValue("status").ToString() == "error")
                {
                    //that bai
                    jobStatus = 2;
                }
                repository.UpdateTrialStaffStartTrialDateNotifyList(NotifyMethod.SMS, idArray, jobStatus, resultSmsSend);

            }
        }
    }
}