using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using EmpMan.Data;
using EmpMan.Common.Enums;
using System;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface IJobSchedulerService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="jobScheduler">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        JobScheduler Add(JobScheduler jobScheduler);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="jobScheduler">Record đối tượng</param>
        void Update(JobScheduler jobScheduler);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        JobScheduler Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        JobScheduler DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<JobScheduler> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<JobScheduler> GetAll(string keyword, string[] includes = null);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<JobScheduler> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<JobScheduler> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        JobScheduler GetById(int id);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        JobScheduler GetByKey(string id);

        /// <summary>
        /// Lấy record đối tượng dựa vào các thông tin như tên table , key của table
        /// </summary>
        /// <param name="jobType">Loai job</param>
        /// <param name="tableNameRelation">Ten bang </param>
        /// <param name="tableKey">Item key cua bang tableNameRelation</param>
        /// <param name="tableKeyID">ID/No cua table doi tuong</param>
        /// <param name="id">ID cua du lieu</param>
        /// <returns></returns>
        IEnumerable<JobScheduler> GetByTableKey(string jobType, string tableNameRelation, string tableKey, string tableKeyID, int? id = null);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Get context
        /// </summary>
        /// <returns></returns>
        EmpManDbContext GetDbContext();

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

        //thong bao het han thu viec
        //TrialStaffEndTrialDateNotify

        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetTrialStaffEndTrialDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateTrialStaffEndTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string TrialStaffEndTrialDateNotifySmsContent(IEnumerable<JobSchedulerViewModel> list);

        //NHAN CHINH THUC TrialStaffToDevContractDateNotify
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetTrialStaffToDevContractDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateTrialStaffToDevContractDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string TrialStaffToDevContractDateNotifySmsContent(IEnumerable<JobScheduler> list);

        //
        //NHAN VIEN SAP VAO THU VIEC TrialStaffStartTrialDateNotify
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetTrialStaffStartTrialDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateTrialStaffStartTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string TrialStaffStartTrialDateNotifySmsContent(IEnumerable<JobScheduler> list);

        //
        //NHAN VIEN SAP NGHI VIEC DevJobLeavedDateNotify
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetDevJobLeavedDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string DevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list);


        //CAM ON CONG HIEN CUA NHAN VIEN SAP NGHI VIEC ThankDevJobLeavedDateNotify
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        List<Tuple<string, string, int[]>> GetThankDevJobLeavedDateNotifyJob(NotifyMethod notifyType);

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        void UpdateThankDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult);

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        string ThankDevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list);

    }

    public class JobSchedulerService : IJobSchedulerService
    {
        private IJobSchedulerRepository _jobSchedulerRepository;
        private IUnitOfWork _unitOfWork;

        public JobSchedulerService(IJobSchedulerRepository jobSchedulereRepository, IUnitOfWork unitOfWork)
        {
            this._jobSchedulerRepository = jobSchedulereRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="jobScheduler">Thông tin table đối tượng</param>
        /// <returns></returns>
        public JobScheduler Add(JobScheduler jobScheduler)
        {
            return _jobSchedulerRepository.Add(jobScheduler);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="jobScheduler">Record đối tượng</param>
        public void Update(JobScheduler jobScheduler)
        {
            _jobSchedulerRepository.Update(jobScheduler);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public JobScheduler Delete(int id)
        {
            return _jobSchedulerRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public JobScheduler DeleteLogic(int id)
        {
            var dataFromDb = _jobSchedulerRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _jobSchedulerRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<JobScheduler> GetAll()
        {
            return _jobSchedulerRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<JobScheduler> GetAll(string keyword, string[] includes = null)
        {
            var query = _jobSchedulerRepository.GetAll(includes);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)|| x.ShortName.Contains(keyword));


            return query;
        }

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        public IEnumerable<JobScheduler> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _jobSchedulerRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
        }

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        public IEnumerable<JobScheduler> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _jobSchedulerRepository.GetMulti(x =>  x.Name.Contains(keyword));

            switch (sort)
            {
                case "name":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "id":
                    query = query.OrderByDescending(x => x.ID);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public JobScheduler GetById(int id)
        {
            return _jobSchedulerRepository.GetSingleById(id);
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public JobScheduler GetByKey(string id)
        {
            return _jobSchedulerRepository.GetSingleByKey(new object[] { id });
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào các thông tin như tên table , key của table
        /// </summary>
        /// <param name="tableNameRelation">Ten bang </param>
        /// <param name="tableKey">Item key cua bang tableNameRelation</param>
        /// <param name="tableKeyID">ID/No cua table doi tuong</param>
        /// <returns></returns>
        public IEnumerable<JobScheduler> GetByTableKey(string jobType , string tableNameRelation, string tableKey, string tableKeyID , int? id=null)
        {
            string sql = "SELECT * FROM JobSchedulers WHERE 1 =1 ";

            if (id != null && id.HasValue ) {
                sql += " AND ID =" + id + "";
            }

            if (!string.IsNullOrEmpty(jobType))
            {
                sql += " AND UPPER(JobType) ='" + jobType.Trim().ToUpper() + "'";
            }

            if (!string.IsNullOrEmpty(tableNameRelation))
            {
                sql += " AND UPPER(TableNameRelation) ='" + tableNameRelation.Trim().ToUpper() + "'" ;
            }

            if (!string.IsNullOrEmpty(tableKey))
            {
                sql += " AND UPPER(TableKey) ='" + tableKey.Trim().ToUpper() + "'";
            }

            if (!string.IsNullOrEmpty(tableKeyID))
            {
                sql += " AND TableKeyID ='" + tableKeyID.Trim().ToUpper() + "'";
            }


            return GetDbContext().Database.SqlQuery<JobScheduler>(sql);
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Get db context
        /// </summary>
        /// <returns></returns>
        public EmpManDbContext GetDbContext()
        {
            return this._unitOfWork.DbContext;
        }


        #region"DevInterviewDateNotify"
        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public List<Tuple<string, string, int[]>> GetDevInterviewDateNotifyJob(NotifyMethod notifyType)
        {
           return this._jobSchedulerRepository.GetDevInterviewDateNotifyJob(notifyType);
        }

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        public void UpdateDevInterviewDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            this._jobSchedulerRepository.UpdateDevInterviewDateNotifyList(notifyType, jobId, jobStatus, sendSmsResult);
        }

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string DevInterviewDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            return this._jobSchedulerRepository.DevInterviewDateNotifySmsContent(list);
        }

        #endregion

        #region"Xu ly lien quan hen job het han thu viec"
        //TrialStaffEndTrialDateNotify

        /// <summary>
        /// Lay danh sach can gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public List<Tuple<string, string, int[]>> GetTrialStaffEndTrialDateNotifyJob(NotifyMethod notifyType)
        {
            return this._jobSchedulerRepository.GetTrialStaffEndTrialDateNotifyJob(notifyType);
        }

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        public void UpdateTrialStaffEndTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            this._jobSchedulerRepository.UpdateTrialStaffEndTrialDateNotifyList(notifyType, jobId, jobStatus, sendSmsResult);
        }

        /// <summary>
        /// Lay noi dung sms
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TrialStaffEndTrialDateNotifySmsContent(IEnumerable<JobSchedulerViewModel> list)
        {
            return this._jobSchedulerRepository.TrialStaffEndTrialDateNotifySmsContent(list);
        }

        #endregion

        #region"Nhan chinh thuc nhan vien"
        
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public List<Tuple<string, string, int[]>> GetTrialStaffToDevContractDateNotifyJob(NotifyMethod notifyType)
        {
            return this._jobSchedulerRepository.GetTrialStaffToDevContractDateNotifyJob(notifyType);
        }

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        public void UpdateTrialStaffToDevContractDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            this._jobSchedulerRepository.UpdateTrialStaffToDevContractDateNotifyList(notifyType, jobId, jobStatus, sendSmsResult);
        }

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TrialStaffToDevContractDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            return this._jobSchedulerRepository.TrialStaffToDevContractDateNotifySmsContent(list);
        }
        #endregion

        #region"TrialStaffStartTrialDateNotify sap vao thu viec"
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public List<Tuple<string, string, int[]>> GetTrialStaffStartTrialDateNotifyJob(NotifyMethod notifyType)
        {
            return this._jobSchedulerRepository.GetTrialStaffStartTrialDateNotifyJob(notifyType);
        }

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        public void UpdateTrialStaffStartTrialDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            this._jobSchedulerRepository.UpdateTrialStaffStartTrialDateNotifyList(notifyType, jobId, jobStatus, sendSmsResult);
        }

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string TrialStaffStartTrialDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            return this._jobSchedulerRepository.TrialStaffStartTrialDateNotifySmsContent(list);
        }
        #endregion


        #region"DevJobLeavedDateNotify sap vao NGHI viec"
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public List<Tuple<string, string, int[]>> GetDevJobLeavedDateNotifyJob(NotifyMethod notifyType)
        {
            return this._jobSchedulerRepository.GetDevJobLeavedDateNotifyJob(notifyType);
        }

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        public void UpdateDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            this._jobSchedulerRepository.UpdateDevJobLeavedDateNotifyList(notifyType, jobId, jobStatus, sendSmsResult);
        }

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string DevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            return this._jobSchedulerRepository.DevJobLeavedDateNotifySmsContent(list);
        }
        #endregion

        #region"ThankDevJobLeavedDateNotify Cam on cong hien cua nhan vien nghi viec"
        /// <summary>
        /// Lay danh sach can gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <returns></returns>
        public List<Tuple<string, string, int[]>> GetThankDevJobLeavedDateNotifyJob(NotifyMethod notifyType)
        {
            return this._jobSchedulerRepository.GetThankDevJobLeavedDateNotifyJob(notifyType);
        }

        /// <summary>
        /// Cap nhat thong tin sau khi da gui tin nhan het han thu viec
        /// </summary>
        /// <param name="notifyType"></param>
        /// <param name="jobId"></param>
        /// <param name="jobStatus"></param>
        /// <param name="sendSmsResult"></param>
        public void UpdateThankDevJobLeavedDateNotifyList(NotifyMethod notifyType, int[] jobId, int jobStatus, string sendSmsResult)
        {
            this._jobSchedulerRepository.UpdateThankDevJobLeavedDateNotifyList(notifyType, jobId, jobStatus, sendSmsResult);
        }

        /// <summary>
        /// Lay noi dung sms het han thuc viec
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string ThankDevJobLeavedDateNotifySmsContent(IEnumerable<JobScheduler> list)
        {
            return this._jobSchedulerRepository.ThankDevJobLeavedDateNotifySmsContent(list);
        }
        #endregion
    }
}