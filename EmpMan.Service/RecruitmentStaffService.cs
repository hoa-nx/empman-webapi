using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using EmpMan.Data;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface IRecruitmentStaffService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="recruitmentStaff">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        RecruitmentStaff Add(RecruitmentStaff recruitmentStaff);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="recruitmentStaff">Record đối tượng</param>
        void Update(RecruitmentStaff recruitmentStaff);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        RecruitmentStaff Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        RecruitmentStaff DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> GetAll(string keyword);

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="recruitmentID">Mã lần tuyển dụng</param>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> GetAll(string recruitmentID , string keyword);

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="empID">Mã nhân viên phỏng vấn</param>
        /// <param name="recruitmentID">Mã lần tuyển dụng</param>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> GetAllByUser(int empID, string recruitmentID, string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy danh sách các bạn sắp phỏng vấn
        /// </summary>
        /// <returns></returns>
        IEnumerable<RecruitmentStaff> GetInterviewNotificationList(int beforeHours);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        RecruitmentStaff GetById(int id);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Get context
        /// </summary>
        /// <returns></returns>
        EmpManDbContext GetDbContext();
    }

    public class RecruitmentStaffService : IRecruitmentStaffService
    {
        private IRecruitmentStaffRepository _recruitmentStaffRepository;
        private IUnitOfWork _unitOfWork;

        public RecruitmentStaffService(IRecruitmentStaffRepository recruitmentStaffRepository, IUnitOfWork unitOfWork)
        {
            this._recruitmentStaffRepository = recruitmentStaffRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="recruitmentStaff">Thông tin table đối tượng</param>
        /// <returns></returns>
        public RecruitmentStaff Add(RecruitmentStaff recruitmentStaff)
        {
            return _recruitmentStaffRepository.Add(recruitmentStaff);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="recruitmentStaff">Record đối tượng</param>
        public void Update(RecruitmentStaff recruitmentStaff)
        {
            _recruitmentStaffRepository.Update(recruitmentStaff);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public RecruitmentStaff Delete(int id)
        {
            return _recruitmentStaffRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public RecruitmentStaff DeleteLogic(int id)
        {
            var dataFromDb = _recruitmentStaffRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _recruitmentStaffRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<RecruitmentStaff> GetAll()
        {
            return _recruitmentStaffRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<RecruitmentStaff> GetAll(string keyword)
        {
            var query = _recruitmentStaffRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)|| x.ShortName.Contains(keyword));


            return query;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="recruitmentID">Mã lần tuyển dụng</param>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<RecruitmentStaff> GetAll(string recruitmentID, string keyword)
        {
            var query = _recruitmentStaffRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword) || x.ShortName.Contains(keyword));

            if (!string.IsNullOrEmpty(recruitmentID))
                query = query.Where(x => x.RecruitmentID == recruitmentID);

            return query;
        }
        /// <summary>
        /// Lay cac thong tin theo tung user rieng biet
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="recruitmentID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<RecruitmentStaff> GetAllByUser(int empID, string recruitmentID, string keyword)
        {
            string sql = @"
                SELECT RES.[ID]
                      ,RES.[RecruitmentID]
                      ,RES.[RecruitmentStaffID]
                      ,RES.[Name]
                      ,RES.[ShortName]
                      ,RES.[RecruitmentTypeMasterID]
                      ,RES.[RecruitmentTypeMasterDetailID]
                      ,RES.[RequestInCompanyDate]
                      ,RES.[InterviewResult]
                      ,RES.[RequestInterviewDate]
                      ,RES.[InterViewTime]
                      ,RES.[ExamRound1]
                      ,RES.[ExamResult]
                      ,RES.[CompanyCvNo]
                      ,RES.[Pharse]
                      ,RES.[FullName]
                      ,RES.[BirthDay]
                      ,RES.[Gender]
                      ,RES.[National]
                      ,RES.[IdentNo]
                      ,RES.[PhoneNumber]
                      ,RES.[Email]
                      ,RES.[KiboSalary]
                      ,RES.[EducationLevel]
                      ,RES.[CollectName]
                      ,RES.[ProfessionalKbn]
                      ,RES.[EducationType]
                      ,RES.[Grade]
                      ,RES.[IsCertificated]
                      ,RES.[DebtSubjectCount]
                      ,RES.[DebtSubjectReason]
                      ,RES.[CertificatedDateTime]
                      ,RES.[OtherCertificated]
                      ,RES.[JapaneseLevel]
                      ,RES.[EnglishLevel]
                      ,RES.[OtherSkill]
                      ,RES.[MarriedStatus]
                      ,RES.[Objective]
                      ,RES.[CvNote]
                      ,RES.[Comment1]
                      ,RES.[Comment2]
                      ,RES.[CvCreateDate]
                      ,RES.[CvUpdateDate]
                      ,RES.[CvSendCount]
                      ,RES.[CvSendList]
                      ,RES.[StartWorkingDate]
                      ,RES.[AdddressPlace]
                      ,RES.[BornPlace]
                      ,RES.[Hobby]
                      ,RES.[IsTestRound1ByPass]
                      ,RES.[GradeTestRound1]
                      ,RES.[EngGradeTestRound1]
                      ,RES.[ProfessionalKbnGradeTestRound1]
                      ,RES.[GradeTestRound2]
                      ,RES.[CvStatus]
                      ,RES.[EmpType]
                      ,RES.[TrainingClassConditionTalkDate]
                      ,RES.[WorkingConditionTalkDate]
                      ,RES.[Avatar]
                      ,RES.[IsSendSMS]
                      ,RES.[SMSCount]
                      ,RES.[SMSContent]
                      ,RES.[IsTrainingIntroduction]
                      ,RES.[DeptReceived]
                      ,RES.[TeamReceived]
                      ,RES.[TrialStartDate]
                      ,RES.[SupportEmpID]
                      ,RES.[GhostPC]
                      ,RES.[ItMailNotificationDate]
                      ,RES.[ResourceDeptMailNotificationDate]
                      ,RES.[SystemEmpID]
                      ,RES.[RowVersion]
                      ,RES.[DisplayOrder]
                      ,RES.[AccountData]
                      ,RES.[Note]
                      ,RES.[AccessDataLevel]
                      ,RES.[CreatedDate]
                      ,RES.[CreatedBy]
                      ,RES.[UpdatedDate]
                      ,RES.[UpdatedBy]
                      ,RES.[MetaKeyword]
                      ,RES.[MetaDescription]
                      ,RES.[Status]
                      ,RES.[DataStatus]
                      ,RES.[UserAgent]
                      ,RES.[UserHostAddress]
                      ,RES.[UserHostName]
                      ,RES.[RequestDate]
                      ,RES.[RequestBy]
                      ,RES.[ApprovedDate]
                      ,RES.[ApprovedBy]
                      ,RES.[ApprovedStatus]
                      ,RES.[FileID]
                      ,RES.[InterviewRoom]
                      ,RES.[InterviewDate]
                      ,RES.[InterviewComment]
                      ,case when REI.[RecruitmentID] IS NOT NULL  THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END IsRegister
                      ,RES.[IsFinished]  
                      ,REI.Cnt   
                      ,REI.ApprovedStatus  ApprovedStatusInterview
                  FROM 
	                [RecruitmentStaffs] RES
	                LEFT OUTER JOIN (
	                SELECT DISTINCT
                      [RecruitmentID]
                      ,[RecruitmentStaffID]
                      ,[Name]
                      , COUNT(*) OVER ( PaRTITION BY [RecruitmentID] ,[RecruitmentStaffID] ) AS CNT
                      , ApprovedStatus
	                  FROM [RecruitmentInterviews] 
	                  WHERE ([RegInterviewEmpID] = " + empID + @" OR 0 = " + empID + @")
	                ) REI
	                ON (RES.[RecruitmentID] = REI.[RecruitmentID] AND RES.[RecruitmentStaffID] = REI.[RecruitmentStaffID])
                ";

            var query = _unitOfWork.DbContext.Database.SqlQuery<RecruitmentStaff>(sql);

            IEnumerable<RecruitmentStaff> responseData = query;

            if (!string.IsNullOrEmpty(keyword))
                responseData = responseData.Where(x => x.Name.Contains(keyword) || x.ShortName.Contains(keyword));

            if (!string.IsNullOrEmpty(recruitmentID))
                responseData = responseData.Where(x => x.RecruitmentID == recruitmentID);

            return responseData;
        }

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        public IEnumerable<RecruitmentStaff> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _recruitmentStaffRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
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
        public IEnumerable<RecruitmentStaff> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _recruitmentStaffRepository.GetMulti(x =>  x.Name.Contains(keyword));

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

        public IEnumerable<RecruitmentStaff> GetInterviewNotificationList(int beforeHours)
        {
            return null;
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public RecruitmentStaff GetById(int id)
        {
            return _recruitmentStaffRepository.GetSingleById(id);
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
    }
}