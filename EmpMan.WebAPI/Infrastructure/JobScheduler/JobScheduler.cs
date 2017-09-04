using EmpMan.Service;
using EmpMan.Web.Controllers;
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

namespace EmpMan.Web.Infrastructure.JobScheduler
{
    public class SendSMSJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
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
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendSMSJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(5)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}