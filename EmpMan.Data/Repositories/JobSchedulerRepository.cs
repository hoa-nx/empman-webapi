using EmpMan.Common.Enums;
using EmpMan.Common.ViewModels.Models;
using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IJobSchedulerRepository : IRepository<JobScheduler>
    {
        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetDevInterviewDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateDevInterviewDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string DevInterviewDateNotifySmsContent(IEnumerable<JobScheduler> list);

        //TrialStaffEndTrialDateNotify

        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetTrialStaffEndTrialDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateTrialStaffEndTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string TrialStaffEndTrialDateNotifySmsContent(IEnumerable<JobSchedulerViewModel> list);

        //Nhan chinh thuc

        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetTrialStaffToDevContractDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateTrialStaffToDevContractDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string TrialStaffToDevContractDateNotifySmsContent(IEnumerable<JobScheduler> list);

        //Thong bao nhan su vao THU VIEC

        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetTrialStaffStartTrialDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateTrialStaffStartTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string TrialStaffStartTrialDateNotifySmsContent(IEnumerable<JobScheduler> list);

        //DevJobLeavedDateNotify

        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetDevJobLeavedDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string DevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list);

        //ThankDevJobLeavedDateNotify

        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetThankDevJobLeavedDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateThankDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string ThankDevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list);


    }

    public class JobSchedulerRepository : RepositoryBase<JobScheduler>, IJobSchedulerRepository
    {
        private IDbFactory _factory;
        public JobSchedulerRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            _factory = dbFactory;
        }
        

        #region"DevInterviewDateNotify--gui tin nhan thong bao phong van"

        /// <summary>
        /// Thuc thi job gui tin nhan cho cac nguoi dang ky phong van
        /// </summary>
        public List<Tuple<string, string, int[]>> GetDevInterviewDateNotifyJob(NotifyMethod notifyType)
        {
            
            List<Tuple<string, string, int[]>> notifyMsgList = new List<Tuple<string, string, int[]>>();

            //get danh sach can gui tin nhan sms 
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    var notifyList = this.GetDevInterviewDateNotifyList(notifyType);

                    //get nhom theo tung ngay va tung phong
                    var grpList = notifyList.GroupBy(x => new { x.EventDate.Value.Date, x.SMSToNumber })
                                        .Select(grp => new {
                                            NotifyDate = grp.Key.Date,
                                            SMSToNumber = grp.Key.SMSToNumber,
                                            count = grp.Count(),
                                            items = grp.ToList()
                                        }).ToList();

                    //gui tin nhan cho tung nhom so dien thoai / ngay phong van
                    foreach (var item in grpList)
                    {
                        //CHUA xu ly truong hop tin nhan qua dai ????
                        var smsContent = this.DevInterviewDateNotifySmsContent(item.items);

                        var tuple = Tuple.Create(item.SMSToNumber, smsContent, item.items.Select( x => x.ID).ToArray());
                        notifyMsgList.Add(tuple);
                    }

                    //cap nhat sau khi da gui tin nhan 
                    //this.UpdateDevInterviewDateNotifyList(notifyType);

                    break;
                case NotifyMethod.EMAIL:

                    break;
            }

            return notifyMsgList;
        }

        /// <summary>
        /// Danh sach cac cuoc pv trong cung 1 ngay
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string DevInterviewDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            int i = 0;
            int MAX_ITEM_PER_PAGE = 15;
            bool isChanged = false;
            //truong hop nhieu qua thi tach thanh nhieu tin nhan
            List<string> smsList = new List<string>();

            string breakLine = "\\n";
            //"Lich PV thu viec.\\nNgay 25/09:\\n1.Vo Tien Dat ( 17:00 P.106B).\\nNgay 26/09:\\n1.Nguyen Trong Tien(09:30 P.104).\\n2.Phan Thi Tuong Vy(14:00 P.702)"
            string smsHeader = "Thong bao lich PV@Change@" + breakLine;
            string smsInterviewDate = "Ngay @InterviewDate@:" + breakLine;
            string smsInterviewInfo = "@No@.@Candidator@(@Time@ P.@Location@)" + breakLine;
            string smsContent = "";
            string smsFooter = breakLine + "Tran trong thong bao";

            foreach (JobScheduler item in list)
            {
                if (item.SMSNotifyCount>0) isChanged = true;

                if (i % MAX_ITEM_PER_PAGE == 0)
                {
                    if (i > 0)
                    {
                        smsList.Add(smsContent);

                    }
                    smsContent = smsHeader;
                    smsContent += smsInterviewDate.Replace("@InterviewDate@", item.EventDate.Value.Date.ToString("yyyy-MM-dd"));
                }

                smsContent += smsInterviewInfo.Replace("@No@", (i + 1).ToString()).Replace("@Candidator@", item.EventUser).Replace("@Time@", item.EventDate.Value.ToString("HH:mm")).Replace("@Location@", item.LocationEvent);

                i++;
            }

            if (isChanged)
            {
                smsContent = smsContent.Replace("@Change@", "(thay doi)");
            }
            else
            {
                smsContent = smsContent.Replace("@Change@", "");
            }

            //add sms vao list 
            smsList.Add(smsContent);
            

            return   smsContent;

        }

        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private string GetDevInterviewDateNotifySql(NotifyMethod notifyType)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
	                              ,REC.[ID] 
	                              ,REI.[RegInterviewEmpID] 
                                  ,CONVERT(DATE,Job.EventDate) ExecuteDateOnly   
                              FROM [dbo].[JobSchedulers] Job 
                              LEFT OUTER JOIN RecruitmentInterviews REI ON JOB.TableKey = REI.RecruitmentStaffID
                              LEFT OUTER JOIN Recruitments REC ON REI.RecruitmentID = REC.ID AND REC.IsFinished = 0
                              WHERE 
                                    Job.JobType = 1
	                            AND Job.JobStatus IN(0,2) --job chua thuc thi / thuc thi khong thanh cong
	                            AND Job.EventDate > GETDATE() --ngay dien ra su kien lon hon ngay hien tai
	                            AND ISNULL(Job.IsChanged,1) = 1  --co thay doi
                                AND ISNULL(Job.LocationEvent,'') <> '' 
                                AND SMSToNumber > 0
	                            AND NOT EXISTS(
					                            SELECT 1 
					                            FROM RecruitmentInterviews REI2 
					                            WHERE 
						                            (		REI2.ScheduleInterviewDate IS NULL 
							                            OR	REI2.ScheduleInterviewRoom IS NULL
						                            ) 
						                            AND  REI2.RecruitmentID = REC.ID )
                            ORDER BY 
	                            Job.EventDate
                            ; ";

            return sql;

        }

        /// <summary>
        /// Khi  gan toi thoi gian phong van thi nhan tin nhac lai
        /// </summary>
        /// 
        private string GetDevInterviewDateRemiderNotifySql(NotifyMethod notifyType, int limitMinute=30)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
	                              ,REC.[ID] 
	                              ,REI.[RegInterviewEmpID] 
                                  ,CONVERT(DATE,Job.EventDate) ExecuteDateOnly   
                              FROM [dbo].[JobSchedulers] Job 
                              LEFT OUTER JOIN RecruitmentInterviews REI ON JOB.TableKey = REI.RecruitmentStaffID
                              LEFT OUTER JOIN Recruitments REC ON REI.RecruitmentID = REC.ID AND REC.IsFinished = 0
                              WHERE 
	                              Job.SmSNotifyRemider = 0 --chua gui sms nhac event
                              AND Job.JobType = 1  
                              AND ISNULL(Job.LocationEvent,'') <> '' 
                              AND SMSToNumber > 0
	                          AND DATEDIFF(minute,GETDATE(),Job.EventDate) < " + limitMinute + @"
                              ORDER BY 
	                            Job.EventDate
                            ; ";

            return sql;

        }

        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private IEnumerable<JobScheduler> GetDevInterviewDateNotifyList(NotifyMethod notifyType)
        {

            string sql = this.GetDevInterviewDateNotifySql(notifyType);

            var data = this._factory.Init().Database.SqlQuery<JobScheduler>(sql);

            return data;

        }

        public void UpdateDevInterviewDateNotifyList(NotifyMethod notifyType , int[] jobId , int jobStatus , string sendSmsResult)
        {
            string sql = @"";
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    sql += @" UPDATE  [dbo].[JobSchedulers]  SET 
                                JobStatus = " + jobStatus + @"
                            ,   ActualRunJobDate = GETDATE()
                            ,   SMSNotifyCount = ISNULL(SMSNotifyCount,0) + 1
                            ,   IsChanged = 0 
                            ,   UpdatedDate = GETDATE()
                            WHERE ID IN(" + string.Join(",", jobId) + ")";
                    
                    break;

                case NotifyMethod.EMAIL:

                    break;
            }


            this._factory.Init().Database.ExecuteSqlCommand(sql);

        }

        #endregion


        #region"TrialStaffEndTrialDateNotify--Gui tin nhan thong bao het han thu viec"
        /// <summary>
        /// Thuc thi job gui tin nhan thong bao het han thu viec
        /// </summary>
        public List<Tuple<string, string, int[]>> GetTrialStaffEndTrialDateNotifyJob(NotifyMethod notifyType)
        {

            List<Tuple<string, string, int[]>> notifyMsgList = new List<Tuple<string, string, int[]>>();

            //get danh sach can gui tin nhan sms 
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                  var notifyList = this.GetTrialStaffEndTrialDateNotifyList(notifyType);
                    //****************** KHAC VOI CAC TRUONG HOP KHAC **************
                    //****************** NEU DA KY HD TRUOC NGAY THONG BAO THI SE HUY JOB(DOI TUONG LA NGAY HD CHUA CO **************
                    //get nhom theo tung ngay va tung phong
                    var grpList = notifyList.Where(x=> (x.ContractDate == null)).GroupBy(x => new { x.EventDate.Value.Date, x.SMSToNumber })
                                        .Select(grp => new {
                                            NotifyDate = grp.Key.Date,
                                            SMSToNumber = grp.Key.SMSToNumber,
                                            count = grp.Count(),
                                            items = grp.ToList()
                                        }).ToList();

                    //gui tin nhan cho tung nhom so dien thoai / ngay phong van
                    foreach (var item in grpList)
                    {
                        var smsContent = this.TrialStaffEndTrialDateNotifySmsContent(item.items);
                        var tuple = Tuple.Create(item.SMSToNumber, smsContent, item.items.Select(x => x.ID).ToArray());
                        notifyMsgList.Add(tuple);
                    }

                    //Trạng thái của job ( 0: chưa thực thi ; 1 : đang thực thi ; 2 : thực thi thất bại; 3 : hủy job  ; 9 : đã thực thi thành công )
                    //cap nhat cac job bi huy ( JobStatus = 3)
                    var cancelJob = notifyList.Where(x => x.ContractDate != null);
                    if (cancelJob.Count() > 0)
                    {
                        this.UpdateTrialStaffEndTrialDateNotifyList(NotifyMethod.SMS, cancelJob.Select(x => x.ID).ToArray(), 3, "");
                    }

                    break;
                case NotifyMethod.EMAIL:

                    break;
            }

            return notifyMsgList;
        }

        /// <summary>
        /// Danh sach cac cuoc pv trong cung 1 ngay
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TrialStaffEndTrialDateNotifySmsContent(IEnumerable<JobSchedulerViewModel> list)
        {
            int i = 0;
            int MAX_ITEM_PER_PAGE = 15;
            bool isChanged = false;

            string breakLine = "\\n";
            string smsHeader = "Thong bao het han thu viec" + breakLine;
            string smsEventDate = "Ngay @EventDate@ het han thu viec cua NV";
            string smsOtherInfo = "@Candidator@" + breakLine;
            string smsContent = "";
            string smsFooter = "Hay danh gia & thong bao ket qua.Xin cam on.";

            foreach (JobSchedulerViewModel item in list)
            {
                if (item.SMSNotifyCount > 0) isChanged = true;

                if (i % MAX_ITEM_PER_PAGE == 0)
                {
                    smsContent = smsHeader;

                    smsContent += smsEventDate.Replace("@EventDate@", item.EventDate.Value.Date.ToString("yyyy-MM-dd"));
                }

                smsContent += smsOtherInfo.Replace("@Candidator@", item.EventUser);
                smsContent += smsFooter;

                i++;
            }

            if (isChanged)
            {
                smsContent = smsContent.Replace("@Change@", "(thay doi)");
            }
            else
            {
                smsContent = smsContent.Replace("@Change@", "");
            }
            return smsContent;

        }

        /// <summary>
        /// lay danh sach cac thong tin het han thu viec
        /// </summary>
        /// 
        private string GetTrialStaffEndTrialDateNotifySql(NotifyMethod notifyType)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
                                  ,EMP.ContractDate ContractDate
                              FROM [dbo].[JobSchedulers] Job 
                              INNER JOIN EMPS EMP ON (Job.TableKey = EMP.ID AND Job.EventDate = EMP.EndTrialDate)
                              WHERE 
                                    Job.JobType = 3
	                            AND Job.JobStatus IN(0,2) --job chua thuc thi / thuc thi khong thanh cong
	                            AND Job.EventDate = CONVERT(DATE, GETDATE() + 1) --ngay dien ra su kien lon hon ngay hien tai
	                            AND ISNULL(Job.IsChanged,1) = 1  --co thay doi
                                AND SMSToNumber IS NOT NULL
                                AND (EMP.ContractDate IS NULL OR EMP.ContractDate > CONVERT(DATE, GETDATE()) ) --chua ky HD hoac ngay HD lon hon ngay hien tai
                                AND EMP.JobLeaveDate IS NULL
                            ORDER BY 
	                            Job.EventDate
                            ";

            return sql;

        }
               
        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private IEnumerable<JobSchedulerViewModel> GetTrialStaffEndTrialDateNotifyList(NotifyMethod notifyType)
        {

            string sql = this.GetTrialStaffEndTrialDateNotifySql(notifyType);

            var data = this._factory.Init().Database.SqlQuery<JobSchedulerViewModel>(sql);
             
            return data;

        }

        /// <summary>
        /// Cap nhat job
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus">Trạng thái của job ( 0: chưa thực thi ; 1 : đang thực thi ; 2 : thực thi thất bại; 3 : hủy job  ; 9 : đã thực thi thành công )</param>
        /// <param name="sendSmsResult"></param>
        public void UpdateTrialStaffEndTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            string sql = @"";
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    sql += @" UPDATE  [dbo].[JobSchedulers]  SET 
                                JobStatus = " + jobStatus + @"
                            ,   ActualRunJobDate = GETDATE()
                            ,   SMSNotifyCount = ISNULL(SMSNotifyCount,0) + 1
                            ,   IsChanged = 0 
                            ,   UpdatedDate = GETDATE()
                            WHERE ID IN(" + string.Join(",", jobId) + ")";

                    break;

                case NotifyMethod.EMAIL:

                    break;
            }


            this._factory.Init().Database.ExecuteSqlCommand(sql);

        }

        #endregion
        

        #region"TrialStaffToDevContractDateNotify--Gui tin nhan thong bao nhan chinh thuc nhan vien"
        /// <summary>
        /// Thuc thi job gui tin nhan thong bao het han thu viec
        /// </summary>
        public List<Tuple<string, string, int[]>> GetTrialStaffToDevContractDateNotifyJob(NotifyMethod notifyType)
        {

            List<Tuple<string, string, int[]>> notifyMsgList = new List<Tuple<string, string, int[]>>();

            //get danh sach can gui tin nhan sms 
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    var notifyList = this.GetTrialStaffToDevContractDateNotifyList(notifyType);

                    //get nhom theo tung ngay va tung phong
                    var grpList = notifyList.GroupBy(x => new { x.EventDate.Value.Date, x.SMSToNumber })
                                        .Select(grp => new {
                                            NotifyDate = grp.Key.Date,
                                            SMSToNumber = grp.Key.SMSToNumber,
                                            count = grp.Count(),
                                            items = grp.ToList()
                                        }).ToList();

                    //gui tin nhan cho tung nhom so dien thoai / ngay phong van
                    foreach (var item in grpList)
                    {
                        var smsContent = this.TrialStaffToDevContractDateNotifySmsContent(item.items);
                        var tuple = Tuple.Create(item.SMSToNumber, smsContent, item.items.Select(x => x.ID).ToArray());
                        notifyMsgList.Add(tuple);
                    }

                    //cap nhat sau khi da gui tin nhan 
                    //this.UpdateDevInterviewDateNotifyList(notifyType);

                    break;
                case NotifyMethod.EMAIL:

                    break;
            }

            return notifyMsgList;
        }

        /// <summary>
        /// Danh sach cac cuoc pv trong cung 1 ngay
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TrialStaffToDevContractDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            int i = 0;
            int MAX_ITEM_PER_PAGE = 15;
            bool isChanged = false;
            List<string> smsList = new List<string>();

            string breakLine = "\\n";
            string smsHeader = "Dept 1 xin tran trong thong bao toi ban @Candidator@ :" + breakLine;
            string smsEventDate = "Ke tu ngay @EventDate@ ban la nhan vien chinh thuc tai FJS." + breakLine;
            string smsOtherInfo = "Cac thu tuc can thiet thi cong ty se email toi ban." + breakLine + "Chuc ban luon hoan thanh tot cac cong viec.";
            string smsContent = "";
            string smsFooter = "Than chao";

            foreach (JobScheduler item in list)
            {
                if (item.SMSNotifyCount > 0) isChanged = true;

                if (i % MAX_ITEM_PER_PAGE == 0)
                {
                    if (i > 0)
                    {
                        smsList.Add(smsContent);
    
                    }

                    smsContent = smsHeader;

                    smsContent += smsEventDate;

                }

                smsContent += smsOtherInfo;

                smsContent += smsFooter;
                //thay the param
                smsContent = smsContent.Replace("@Candidator@", item.EventUser).Replace("@EventDate@", item.EventDate.Value.Date.ToString("yyyy-MM-dd"));

                if (isChanged)
                {
                    smsContent = smsContent.Replace("@Change@", "(thay doi)");
                }
                else
                {
                    smsContent = smsContent.Replace("@Change@", "");
                }

                i++;
            }
            //add to list
            smsList.Add(smsContent);

            return smsContent;

        }

        /// <summary>
        /// lay danh sach cac thong tin het han thu viec
        /// </summary>
        /// 
        private string GetTrialStaffToDevContractDateNotifySql(NotifyMethod notifyType)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
                              FROM [dbo].[JobSchedulers] Job 
                              INNER JOIN EMPS EMP ON (Job.TableKey = EMP.ID AND Job.EventDate = EMP.ContractDate)
                              WHERE 
                                    Job.JobType = 4
	                            AND Job.JobStatus IN(0,2) --job chua thuc thi / thuc thi khong thanh cong
	                            AND Job.EventDate = CONVERT(DATE, GETDATE()) --ngay dien ra su kien
	                            AND ISNULL(Job.IsChanged,1) = 1  --co thay doi
                                AND ISNULL(SMSNotifyCount,0) = 0 --chua gui lan nao
                                AND SMSToNumber IS NOT NULL
                                AND EMP.ContractDate >= CONVERT(DATE, GETDATE()) --da ky HD
                                AND EMP.JobLeaveDate IS NULL
                            ORDER BY 
	                            Job.EventDate
                            ";

            return sql;

        }

        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private IEnumerable<JobScheduler> GetTrialStaffToDevContractDateNotifyList(NotifyMethod notifyType)
        {

            string sql = this.GetTrialStaffToDevContractDateNotifySql(notifyType);

            var data = this._factory.Init().Database.SqlQuery<JobScheduler>(sql);

            return data;

        }

        public void UpdateTrialStaffToDevContractDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            string sql = @"";
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    sql += @" UPDATE  [dbo].[JobSchedulers]  SET 
                                JobStatus = " + jobStatus + @"
                            ,   ActualRunJobDate = GETDATE()
                            ,   SMSNotifyCount = ISNULL(SMSNotifyCount,0) + 1
                            ,   IsChanged = 0 
                            ,   UpdatedDate = GETDATE()
                            WHERE ID IN(" + string.Join(",", jobId) + ")";

                    break;

                case NotifyMethod.EMAIL:

                    break;
            }


            this._factory.Init().Database.ExecuteSqlCommand(sql);

        }

        #endregion
        

        #region"TrialStaffStartTrialDateNotify--Gui tin nhan thong bao nhan vien chuan bi vao thu viec"
        /// <summary>
        /// Thuc thi job gui tin nhan thong bao het han thu viec
        /// </summary>
        public List<Tuple<string, string, int[]>> GetTrialStaffStartTrialDateNotifyJob(NotifyMethod notifyType)
        {

            List<Tuple<string, string, int[]>> notifyMsgList = new List<Tuple<string, string, int[]>>();

            //get danh sach can gui tin nhan sms 
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    var notifyList = this.GetTrialStaffStartTrialDateNotifyList(notifyType);

                    //get nhom theo tung ngay va tung phong
                    var grpList = notifyList.GroupBy(x => new { x.EventDate.Value.Date, x.SMSToNumber })
                                        .Select(grp => new {
                                            NotifyDate = grp.Key.Date,
                                            SMSToNumber = grp.Key.SMSToNumber,
                                            count = grp.Count(),
                                            items = grp.ToList()
                                        }).ToList();

                    //gui tin nhan cho tung nhom so dien thoai / ngay phong van
                    foreach (var item in grpList)
                    {
                        var smsContent = this.TrialStaffStartTrialDateNotifySmsContent(item.items);
                        var tuple = Tuple.Create(item.SMSToNumber, smsContent, item.items.Select(x => x.ID).ToArray());
                        notifyMsgList.Add(tuple);
                    }

                    //cap nhat sau khi da gui tin nhan 
                    //this.UpdateDevInterviewDateNotifyList(notifyType);

                    break;
                case NotifyMethod.EMAIL:

                    break;
            }

            return notifyMsgList;
        }

        /// <summary>
        /// Danh sach cac cuoc pv trong cung 1 ngay
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TrialStaffStartTrialDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            int i = 0;
            int MAX_ITEM_PER_PAGE = 15;
            bool isChanged = false;
            List<string> smsList = new List<string>();

            string breakLine = "\\n";
            string smsHeader = "T/b nhan su sap vao thu viec" + breakLine;
            string smsEventDate = "Ke tu ngay @EventDate@ nhan vien ";
            string smsOtherInfo = "@Candidator@ se vao thu viec tai team." + breakLine;
            string smsContent = "";
            string smsFooter = "Hay chuan bi cac cong viec de viec nhan thu viec duoc thuan loi.";

            foreach (JobScheduler item in list)
            {
                if (item.SMSNotifyCount > 0) isChanged = true;

                if (i % MAX_ITEM_PER_PAGE == 0)
                {
                    if (i > 0)
                    {
                        smsList.Add(smsContent);

                    }

                    smsContent = smsHeader;

                    smsContent += smsEventDate;

                }

                smsContent += smsOtherInfo;

                smsContent += smsFooter;

                //thay the param
                smsContent = smsContent.Replace("@Candidator@", item.EventUser).Replace("@EventDate@", item.EventDate.Value.Date.ToString("yyyy-MM-dd"));

                if (isChanged)
                {
                    smsContent = smsContent.Replace("@Change@", "(thay doi)");
                }
                else
                {
                    smsContent = smsContent.Replace("@Change@", "");
                }

                i++;
            }
            //add to list
            smsList.Add(smsContent);

            return smsContent;

        }

        /// <summary>
        /// lay danh sach cac thong tin het han thu viec
        /// </summary>
        /// 
        private string GetTrialStaffStartTrialDateNotifySql(NotifyMethod notifyType)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
                              FROM [dbo].[JobSchedulers] Job 
                              LEFT OUTER JOIN EMPS EMP ON (Job.TableKey = EMP.ID AND Job.EventDate = EMP.StartTrialDate)
                              WHERE 
                                    Job.JobType = 2
	                            AND Job.JobStatus IN(0,2) --job chua thuc thi / thuc thi khong thanh cong
	                            AND Job.EventDate = CONVERT(DATE, GETDATE() + 1) --truoc ngay dien ra su kien 1 ngay
	                            AND ISNULL(Job.IsChanged,1) = 1  --co thay doi
                                AND ISNULL(SMSNotifyCount,0) = 0 --chua gui lan nao
                                AND SMSToNumber IS NOT NULL
                                AND EMP.StartTrialDate >= CONVERT(DATE, GETDATE()) 
                                AND EMP.JobLeaveDate IS NULL
                               ORDER BY 
	                            Job.EventDate
                            ";

            return sql;

        }

        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private IEnumerable<JobScheduler> GetTrialStaffStartTrialDateNotifyList(NotifyMethod notifyType)
        {

            string sql = this.GetTrialStaffStartTrialDateNotifySql(notifyType);

            var data = this._factory.Init().Database.SqlQuery<JobScheduler>(sql);

            return data;

        }

        public void UpdateTrialStaffStartTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            string sql = @"";
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    sql += @" UPDATE  [dbo].[JobSchedulers]  SET 
                                JobStatus = " + jobStatus + @"
                            ,   ActualRunJobDate = GETDATE()
                            ,   SMSNotifyCount = ISNULL(SMSNotifyCount,0) + 1
                            ,   IsChanged = 0 
                            ,   UpdatedDate = GETDATE()
                            WHERE ID IN(" + string.Join(",", jobId) + ")";

                    break;

                case NotifyMethod.EMAIL:

                    break;
            }


            this._factory.Init().Database.ExecuteSqlCommand(sql);

        }

        #endregion

       
        #region"DevJobLeavedDateNotify--Gui tin nhan thong bao nhan vien SAP NGHI VIEC"
        /// <summary>
        /// Thuc thi job gui tin nhan thong bao het han thu viec
        /// </summary>
        public List<Tuple<string, string, int[]>> GetDevJobLeavedDateNotifyJob(NotifyMethod notifyType)
        {

            List<Tuple<string, string, int[]>> notifyMsgList = new List<Tuple<string, string, int[]>>();

            //get danh sach can gui tin nhan sms 
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    var notifyList = this.GetDevJobLeavedDateNotifyList(notifyType);

                    //get nhom theo tung ngay va tung phong
                    var grpList = notifyList.GroupBy(x => new { x.EventDate.Value.Date, x.SMSToNumber })
                                        .Select(grp => new {
                                            NotifyDate = grp.Key.Date,
                                            SMSToNumber = grp.Key.SMSToNumber,
                                            count = grp.Count(),
                                            items = grp.ToList()
                                        }).ToList();

                    //gui tin nhan cho tung nhom so dien thoai / ngay phong van
                    foreach (var item in grpList)
                    {
                        var smsContent = this.DevJobLeavedDateNotifySmsContent(item.items);
                        var tuple = Tuple.Create(item.SMSToNumber, smsContent, item.items.Select(x => x.ID).ToArray());
                        notifyMsgList.Add(tuple);
                    }
                    
                    break;
                case NotifyMethod.EMAIL:

                    break;
            }

            return notifyMsgList;
        }

        /// <summary>
        /// Danh sach cac cuoc pv trong cung 1 ngay
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string DevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            int i = 0;
            int MAX_ITEM_PER_PAGE = 15;
            bool isChanged = false;
            List<string> smsList = new List<string>();

            string breakLine = "\\n";
            string smsHeader = "T/b nhan su sap nghi viec" + breakLine;
            string smsEventDate = "Ke tu ngay @EventDate@ nhan vien ";
            string smsOtherInfo = "@Candidator@ se nghi viec tai cong ty." + breakLine;
            string smsContent = "";
            string smsFooter = "Nho confirm va lien lac cong ty ve nghi viec nay som de cong ty tien hanh cac thu tuc.";

            foreach (JobScheduler item in list)
            {
                if (item.SMSNotifyCount > 0) isChanged = true;

                if (i % MAX_ITEM_PER_PAGE == 0)
                {
                    if (i > 0)
                    {
                        smsList.Add(smsContent);

                    }

                    smsContent = smsHeader;

                    smsContent += smsEventDate;

                }

                smsContent += smsOtherInfo;

                smsContent += smsFooter;

                //thay the param
                smsContent = smsContent.Replace("@Candidator@", item.EventUser).Replace("@EventDate@", item.EventDate.Value.Date.ToString("yyyy-MM-dd"));

                if (isChanged)
                {
                    smsContent = smsContent.Replace("@Change@", "(thay doi)");
                }
                else
                {
                    smsContent = smsContent.Replace("@Change@", "");
                }

                i++;
            }
            //add to list
            smsList.Add(smsContent);

            return smsContent;

        }

        /// <summary>
        /// lay danh sach cac thong tin het han thu viec
        /// </summary>
        /// 
        private string GetDevJobLeavedDateNotifySql(NotifyMethod notifyType)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
                              FROM [dbo].[JobSchedulers] Job 
                              LEFT OUTER JOIN EMPS EMP ON (Job.TableKey = EMP.ID AND Job.EventDate = EMP.JobLeaveDate)
                              WHERE 
                                    Job.JobType = 5 --nhan vien sap nghi viec
	                            AND Job.JobStatus IN(0,2) --job chua thuc thi / thuc thi khong thanh cong
	                            AND Job.EventDate = CONVERT(DATE, GETDATE() + 3) --truoc ngay dien ra su kien 3 ngay
	                            AND ISNULL(Job.IsChanged,1) = 1  --co thay doi
                                AND ISNULL(SMSNotifyCount,0) = 0 --chua gui lan nao
                                AND SMSToNumber IS NOT NULL
                                AND EMP.JobLeaveDate >= CONVERT(DATE,GETDATE()) 
                            ORDER BY 
	                            Job.EventDate
                            ";

            return sql;

        }

        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private IEnumerable<JobScheduler> GetDevJobLeavedDateNotifyList(NotifyMethod notifyType)
        {

            string sql = this.GetDevJobLeavedDateNotifySql(notifyType);

            var data = this._factory.Init().Database.SqlQuery<JobScheduler>(sql);

            return data;

        }

        public void UpdateDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            string sql = @"";
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    sql += @" UPDATE  [dbo].[JobSchedulers]  SET 
                                JobStatus = " + jobStatus + @"
                            ,   ActualRunJobDate = GETDATE()
                            ,   SMSNotifyCount = ISNULL(SMSNotifyCount,0) + 1
                            ,   IsChanged = 0 
                            ,   UpdatedDate = GETDATE()
                            WHERE ID IN(" + string.Join(",", jobId) + ")";

                    break;

                case NotifyMethod.EMAIL:

                    break;
            }


            this._factory.Init().Database.ExecuteSqlCommand(sql);

        }

        #endregion

        #region"ThankDevJobLeavedDateNotify--Gui tin nhan thong bao cam on cong hien cua nhan vien NGHI VIEC"
        /// <summary>
        /// Thuc thi job gui tin nhan cam on nhan vien nghi viec da cong hien cong ty
        /// </summary>
        public List<Tuple<string, string, int[]>> GetThankDevJobLeavedDateNotifyJob(NotifyMethod notifyType)
        {

            List<Tuple<string, string, int[]>> notifyMsgList = new List<Tuple<string, string, int[]>>();

            //get danh sach can gui tin nhan sms 
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    var notifyList = this.GetThankDevJobLeavedDateNotifyList(notifyType);

                    //get nhom theo tung ngay va tung phong
                    var grpList = notifyList.GroupBy(x => new { x.EventDate.Value.Date, x.SMSToNumber })
                                        .Select(grp => new {
                                            NotifyDate = grp.Key.Date,
                                            SMSToNumber = grp.Key.SMSToNumber,
                                            count = grp.Count(),
                                            items = grp.ToList()
                                        }).ToList();

                    //gui tin nhan cho tung nhom so dien thoai / ngay phong van
                    foreach (var item in grpList)
                    {
                        var smsContent = this.ThankDevJobLeavedDateNotifySmsContent(item.items);
                        var tuple = Tuple.Create(item.SMSToNumber, smsContent, item.items.Select(x => x.ID).ToArray());
                        notifyMsgList.Add(tuple);
                    }

                    break;
                case NotifyMethod.EMAIL:

                    break;
            }

            return notifyMsgList;
        }

        /// <summary>
        /// Danh sach cac cuoc pv trong cung 1 ngay
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string ThankDevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            int i = 0;
            int MAX_ITEM_PER_PAGE = 15;
            bool isChanged = false;
            List<string> smsList = new List<string>();

            string breakLine = "\\n";
            string smsHeader = "FJS DEPT1" + breakLine;
            string smsEventDate = "";
            string smsOtherInfo = "Cong ty tran trong cam on nhung dong gop cua @Candidator@ trong thoi gian lam viec tai cong ty." + breakLine;
            string smsContent = "";
            string smsFooter = "Chuc ban va gia dinh nhieu suc khoe va thanh cong.";

            foreach (JobScheduler item in list)
            {
                if (item.SMSNotifyCount > 0) isChanged = true;

                if (i % MAX_ITEM_PER_PAGE == 0)
                {
                    if (i > 0)
                    {
                        smsList.Add(smsContent);

                    }

                    smsContent = smsHeader;

                    smsContent += smsEventDate;

                }

                smsContent += smsOtherInfo;

                smsContent += smsFooter;

                //thay the param
                smsContent = smsContent.Replace("@Candidator@", item.EventUser).Replace("@EventDate@", item.EventDate.Value.Date.ToString("yyyy-MM-dd"));

                if (isChanged)
                {
                    smsContent = smsContent.Replace("@Change@", "(thay doi)");
                }
                else
                {
                    smsContent = smsContent.Replace("@Change@", "");
                }

                i++;
            }
            //add to list
            smsList.Add(smsContent);

            return smsContent;

        }

        /// <summary>
        /// lay danh sach cac thong tin het han thu viec
        /// </summary>
        /// 
        private string GetThankDevJobLeavedDateNotifySql(NotifyMethod notifyType)
        {

            string sql = @" SELECT Job.[ID]
                                  ,Job.[JobType]
                                  ,Job.[Name]
                                  ,Job.[ShortName]
                                  ,Job.[TableNameRelation]
                                  ,Job.[TableKey]
                                  ,Job.[TableKeyID]
                                  ,Job.[ScheduleRunJobDate]
                                  ,Job.[JobContent]
                                  ,Job.[FromEmail]
                                  ,Job.[ToNotiEmailList]
                                  ,Job.[CcNotiEmailList]
                                  ,Job.[BccNotiEmailList]
                                  ,Job.[SMSFromNumber]
                                  ,Job.[SMSToNumber]
                                  ,Job.[SMSContent]
                                  ,Job.[JobStatus]
                                  ,Job.[ActualRunJobDate]
                                  ,Job.[TemplateID]
                                  ,Job.[AttachmementID]
                                  ,Job.[RowVersion]
                                  ,Job.[DisplayOrder]
                                  ,Job.[AccountData]
                                  ,Job.[Note]
                                  ,Job.[AccessDataLevel]
                                  ,Job.[CreatedDate]
                                  ,Job.[CreatedBy]
                                  ,Job.[UpdatedDate]
                                  ,Job.[UpdatedBy]
                                  ,Job.[MetaKeyword]
                                  ,Job.[MetaDescription]
                                  ,Job.[Status]
                                  ,Job.[DataStatus]
                                  ,Job.[UserAgent]
                                  ,Job.[UserHostAddress]
                                  ,Job.[UserHostName]
                                  ,Job.[RequestDate]
                                  ,Job.[RequestBy]
                                  ,Job.[ApprovedDate]
                                  ,Job.[ApprovedBy]
                                  ,Job.[ApprovedStatus]
                                  ,Job.[EventDate]
                                  ,Job.[IsChanged]
                                  ,Job.[SMSNotifyRemider]
                                  ,Job.[EmailNotifyRemider]
                                  ,Job.[SMSNotifyCount]
                                  ,Job.[EmailNotifyCount]
                                  ,Job.[TemplateText]
                                  ,Job.[LocationEvent]
                                  ,Job.[EventUser]
                              FROM [dbo].[JobSchedulers] Job 
                              LEFT OUTER JOIN EMPS EMP ON (Job.TableKey = EMP.ID AND Job.EventDate = EMP.JobLeaveDate)
                              WHERE 
                                    Job.JobType = 6 --cam on nhan vien sap nghi viec vi da cong hien
	                            AND Job.JobStatus IN(0,2) --job chua thuc thi / thuc thi khong thanh cong
	                            AND Job.EventDate = CONVERT(DATE, GETDATE() + 3) --truoc ngay dien ra su kien 3 ngay
	                            AND ISNULL(Job.IsChanged,1) = 1  --co thay doi
                                AND ISNULL(SMSNotifyCount,0) = 0 --chua gui lan nao
                                AND SMSToNumber IS NOT NULL
                                AND EMP.JobLeaveDate >= CONVERT(DATE,GETDATE()) 
                            ORDER BY 
	                            Job.EventDate
                            ";

            return sql;

        }

        /// <summary>
        /// lay danh sach cac thong tin phong van da co ngay phong van va phong phong van 
        /// </summary>
        /// 
        private IEnumerable<JobScheduler> GetThankDevJobLeavedDateNotifyList(NotifyMethod notifyType)
        {

            string sql = this.GetThankDevJobLeavedDateNotifySql(notifyType);

            var data = this._factory.Init().Database.SqlQuery<JobScheduler>(sql);

            return data;

        }

        public void UpdateThankDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            string sql = @"";
            switch (notifyType)
            {
                case NotifyMethod.SMS:
                    sql += @" UPDATE  [dbo].[JobSchedulers]  SET 
                                JobStatus = " + jobStatus + @"
                            ,   ActualRunJobDate = GETDATE()
                            ,   SMSNotifyCount = ISNULL(SMSNotifyCount,0) + 1
                            ,   IsChanged = 0 
                            ,   UpdatedDate = GETDATE()
                            WHERE ID IN(" + string.Join(",", jobId) + ")";

                    break;

                case NotifyMethod.EMAIL:

                    break;
            }


            this._factory.Init().Database.ExecuteSqlCommand(sql);

        }

        #endregion

    }
}
