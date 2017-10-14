using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpMan.Model.Models;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using EmpMan.Web.Infrastructure.Extensions;

using EmpMan.Web.Providers;
using System.Linq;
using System;
using EmpMan.Common.ViewModels.Models.Master;
using System.Web.Script.Serialization;
using EmpMan.Common.ViewModels.Models.Emp;
using OfficeOpenXml;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using EmpMan.Common.Enums;
using EmpMan.Common;
using EmpMan.Common.ViewModels;
using EmpMan.Common.ViewModels.Models.File;
using Mapster;
using EmpMan.Common.ViewModels.Models.Common;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/recruitmentstaff")]
    [Authorize]
    public class RecruitmentStaffController : ApiControllerBase
    {
        private IRecruitmentStaffService _dataService;
        private IRecruitmentInterviewService _dataRecruitmentInterviewService;
        private IFileStorageService _dataFileService;
        private IEmpService _dataEmpService;
        private IJobSchedulerService _dataJobSchedulerService;
        private IErrorService _errorService;

        public RecruitmentStaffController(IErrorService errorService, IRecruitmentStaffService dataService , IFileStorageService dataFileService , IEmpService dataEmpService , IRecruitmentInterviewService dataRecruitmentInterviewService, IJobSchedulerService dataJobSchedulerService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._dataFileService = dataFileService;
            this._dataEmpService = dataEmpService;
            this._dataRecruitmentInterviewService = dataRecruitmentInterviewService;
            this._errorService = errorService;
            this._dataJobSchedulerService = dataJobSchedulerService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<RecruitmentStaffViewModel>>(listData);
                var listDataVm = listData.Adapt<List<RecruitmentStaffViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page - 1 * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<RecruitmentStaff>, List<RecruitmentStaffViewModel>>(query);
                var responseData = query.Adapt<List<RecruitmentStaff>, List<RecruitmentStaffViewModel>>();

                var paginationSet = new PaginationSet<RecruitmentStaffViewModel>()
                {
                    Items = responseData,
                    PageIndex = page,
                    TotalRows = totalRow,
                    PageSize = pageSize
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string[] filterRecruitmentID, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                //var model = _dataService.GetAllByUser((int)User.Identity.GetApplicationUser().EmpID, filterRecruitmentID, keyword);
                //call local function 

                var model = GetByAllUser((int)User.Identity.GetApplicationUser().EmpID, filterRecruitmentID, keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<RecruitmentStaff>, List<RecruitmentStaffViewModel>>(query);

                var paginationSet = new PaginationSet<RecruitmentStaffViewModel>()
                {
                    Items = query,
                    PageIndex = page,
                    TotalRows = totalRow,
                    PageSize = pageSize
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallpaging")]
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, SearchItemViewModel searchParam)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                int page=0;
                int pageSize=0;

                var response = request.CreateResponse(HttpStatusCode.BadRequest, "Không get được data");
                 if (searchParam != null)
                {
                    page = searchParam.Page.Value;
                    pageSize = searchParam.PageSize.Value;

                    var model = GetByAllUser((int)User.Identity.GetApplicationUser().EmpID, searchParam.StringItems.ToArray(), searchParam.Keyword);

                    totalRow = model.Count();
                    var query = model.OrderByDescending(x => x.RecruitmentID).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    //get attach file 
                    query  = this.AddAttachtment(query);

                    var paginationSet = new PaginationSet<RecruitmentStaffViewModel>()
                    {
                        Items = query,
                        PageIndex = page,
                        TotalRows = totalRow,
                        PageSize = pageSize
                    };
                    response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                }
                
                return response;
            });
        }

        private List<RecruitmentStaffViewModel> AddAttachtment(List<RecruitmentStaffViewModel> list)
        {
            var listReturn = new List<RecruitmentStaffViewModel>(list);
            foreach (RecruitmentStaffViewModel item in listReturn)
            {
                //get thong tin file dinh kem cua ung vien tuong ung
                var fileList = _dataFileService.GetAllByKey("RecruitmentStaffs", item.RecruitmentStaffID).ToList();
                //var fileResultModel = Mapper.Map<List<FileResultViewModel>>(fileList);
                var fileResultModel = fileList.Adapt<List<FileResultViewModel>>();
                item.AttachmentFileLists = fileResultModel;
            }
            return listReturn;
        }

        private IEnumerable<RecruitmentStaffViewModel> GetByAllUser(int empId , string[] recruitmentID , string keyword)
        {

            string recruitmentWhere = "";
            if (recruitmentID != null && recruitmentID.Count() > 0)
            {
                for (int i = 0; i < recruitmentID.Length; i++)
                {
                    if (i == 0)
                    {
                        recruitmentWhere = "'" + recruitmentID[i] + "'";
                    }
                    else
                    {
                        recruitmentWhere += ",'" + recruitmentID[i] + "'";
                    }
                }
            }

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
                      ,CurrentStatus = CASE  
											--@@chờ phỏng vấn ( còn cho phép đăng ký)
											when  DATEDIFF(MINUTE, REC.AnsRecruitDeptDeadlineDate , GETDATE()) < 0 THEN 0 
											--@@không đăng ký phỏng vấn sau khi đã hết hạn ( trong table interview không có data tương ứng với ứng viên này) 
											when DATEDIFF(MINUTE, REC.AnsRecruitDeptDeadlineDate , GETDATE()) > 0 AND REI.[RecruitmentStaffID] IS NULL THEN 10 
											--@@chưa có lịch phỏng vấn 
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NULL AND TrialStartDate IS NULL ) THEN 20 
											--@@có đăng ký phỏng vấn nhưng chưa tiến hành phỏng vấn (~ chờ phỏng vấn 30)
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) < 0) THEN 30 
											--@@Chờ kết quả phỏng vấn
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) ='' THEN 40
											--@@Phỏng vấn NG 
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) IN(N'Không đạt',N'Không tới',N'Đã tìm được việc') THEN 41 
											--@@Chờ nói chuyện DKLC ( phỏng vấn xong và kết quả là ok)
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND (RES.WorkingConditionTalkDate IS NULL OR (RES.WorkingConditionTalkDate IS NOT NULL AND DATEDIFF(MINUTE, RES.WorkingConditionTalkDate, GETDATE()) < 0) )THEN 50 
											--@@Chờ feedback sau khi đã nói chuyện DKLV 
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.WorkingConditionTalkDate IS NOT NULL AND RES.TrialStartDate IS NULL THEN 60 
											--@@ Đang chờ vào thử việc( co ngay thu viec >= ngay hien tai )
											when REC.ID IS NOT NULL AND  (( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) OR  TrialStartDate IS NOT NULL)  AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate >  CONVERT(DATE,GETDATE()) THEN 70 
											--@@Đã vào thử việc nhưng chưa đăng ký nhân viên
											when REC.ID IS NOT NULL AND  (( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) OR  TrialStartDate IS NOT NULL)  AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate <= CONVERT(DATE,GETDATE()) AND EMP.ID IS NULL THEN 80 
											--@@Đã vào thử việc và đã đăng ký nhân viên
											when REC.ID IS NOT NULL AND  (( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) OR  TrialStartDate IS NOT NULL)  AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate <= CONVERT(DATE,GETDATE()) AND EMP.ID IS NOT NULL THEN 90 
											ELSE
											NULL
						END 

                  FROM 
	                [RecruitmentStaffs] RES
                    LEFT OUTER JOIN [Recruitments] REC ON RES.RecruitmentID = REC.ID
					LEFT OUTER JOIN [Emps] EMP ON RES.SystemEmpID = EMP.ID 
	                LEFT OUTER JOIN (
	                SELECT DISTINCT
                      [RecruitmentID]
                      ,[RecruitmentStaffID]
                      ,[Name]
                      , COUNT(*) OVER ( PaRTITION BY [RecruitmentID] ,[RecruitmentStaffID] ) AS CNT
                      , ISNULL(ApprovedStatus,0) ApprovedStatus
	                  FROM [RecruitmentInterviews] 
	                  WHERE ([RegInterviewEmpID] = " + empId + @" OR 0 = " + empId + @")
	                ) REI
	                ON (RES.[RecruitmentID] = REI.[RecruitmentID] AND RES.[RecruitmentStaffID] = REI.[RecruitmentStaffID])
                    WHERE 1=1 
                    ";

            //if (recruitmentWhere.Trim().Length > 0)
            //{
            //    sql += " AND RES.RecruitmentID IN (" + recruitmentWhere + ")";
            //}
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    sql += " AND LOWER(RES.RecruitmentID) + LOWER(ISNULL(RES.Name,'')) + LOWER(ISNULL(RES.RecruitmentStaffID,''))  LIKE ('" + keyword.ToLower() + "')";
            //}
                            

            var query =_dataService.GetDbContext().Database.SqlQuery<RecruitmentStaffViewModel>(sql).AsEnumerable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (x.Name + x.Note + x.RecruitmentID + x.CollectName + x.InterviewResult).ToLower().Contains(keyword.ToLower()));

            if (recruitmentID != null && recruitmentID.Count()>0)
                query = query.Where(x => recruitmentID.Contains(x.RecruitmentID));
            
            return query;
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<RecruitmentStaff, RecruitmentStaffViewModel>(model);
                var responseData = model.Adapt<RecruitmentStaff, RecruitmentStaffViewModel>();
                   
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                
                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage Create(HttpRequestMessage request, RecruitmentStaffViewModel dataVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    RecruitmentStaff newData = new RecruitmentStaff();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    
                    newData.UpdateRecruitmentStaff(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    //dang ky job
                    this.registerAlertJob("RecruitmentStaffs", data.RecruitmentStaffID, data.ID.ToString(), data,false);

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage Update(HttpRequestMessage request, RecruitmentStaffViewModel dataVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dataFromDb = _dataService.GetById(dataVm.ID);

                    dataFromDb.UpdateRecruitmentStaff(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);

                    //cap nhat thong tin ngay gio phong van 
                    this.UpdateRecruitmentInterviewRelationData(dataVm);

                    //commit data
                    _dataService.SaveChanges();

                    //dang ky job
                    this.registerAlertJob("RecruitmentStaffs", dataFromDb.RecruitmentStaffID, dataFromDb.ID.ToString(), dataFromDb, false);

                    var responseData = Mapper.Map<RecruitmentStaff, RecruitmentStaffViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        private void UpdateRecruitmentInterviewRelationData(RecruitmentStaffViewModel dataVm)
        {
            string interviewDate = "NULL";
            string interviewRoom = "NULL";

            if(dataVm != null && dataVm.InterviewDate.HasValue)
            {
                interviewDate = "'" +  dataVm.InterviewDate.Value.ToString() + "'" ;
            }
            if (dataVm != null && !string.IsNullOrEmpty(dataVm.InterviewRoom))
            {
                interviewRoom = "N'" + dataVm.InterviewRoom +"'";
            }
            string sql = @" UPDATE RecruitmentInterviews SET ScheduleInterviewDate=@interviewDate@
                            ,   ActualInterviewDate = @interviewDate@
                            ,   ScheduleInterviewRoom = @interviewRoom@
                            ,   ActualInterviewRoom = @interviewRoom@
                            WHERE 
                                RecruitmentID = N'" + dataVm.RecruitmentID + @"' AND RecruitmentStaffID = N'" + dataVm.RecruitmentStaffID + @"'"
                            ;
            sql = sql.Replace("@interviewDate@", interviewDate);
            sql = sql.Replace("@interviewRoom@", interviewRoom);

            _dataService.GetDbContext().Database.ExecuteSqlCommand(sql);

            /*
            if (dataVm!=null  && dataVm.InterviewDate.HasValue)
            {
                string sql = @" UPDATE RecruitmentInterviews SET ScheduleInterviewDate='" + dataVm.InterviewDate + @"'
                ,ActualInterviewDate = '" + dataVm.InterviewDate + @"'
                ,ScheduleInterviewRoom = N'" + dataVm.InterviewRoom + @"'
                ,ActualInterviewRoom = N'" + dataVm.InterviewRoom + @"'
                WHERE 
                    RecruitmentID = N'" + dataVm.RecruitmentID + @"' AND RecruitmentStaffID = N'" + dataVm.RecruitmentStaffID + @"'"
                ;

                _dataService.GetDbContext().Database.ExecuteSqlCommand(sql);
            }
            */

        }
        
        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    response = request.CreateResponse(HttpStatusCode.OK);

                    var oldDataFromDb = _dataService.Delete(id);
                    _dataService.SaveChanges();
                    //XOA CAC JOB DA DANG KY 
                    this.registerAlertJob("RecruitmentStaffs", oldDataFromDb.RecruitmentStaffID, oldDataFromDb.ID.ToString(), oldDataFromDb, true);

                    var responseData = Mapper.Map<RecruitmentStaff, RecruitmentStaffViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedItems)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listData = new JavaScriptSerializer().Deserialize<List<int>>(checkedItems);
                    foreach (var item in listData)
                    {
                        var oldDataFromDb = _dataService.Delete(item);

                        //XOA CAC JOB DA DANG KY 
                        this.registerAlertJob("RecruitmentStaffs", oldDataFromDb.RecruitmentStaffID, oldDataFromDb.ID.ToString(), oldDataFromDb, true);

                    }

                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listData.Count);
                }

                return response;
            });
        }

        [Route("import")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public async Task<HttpResponseMessage> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được máy chủ hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            //Mã lần tuyển dụng
            string recruitmentID = "";
            //Loại tuyển dụng ( LTV chính thức , học việc, tổng vụ ..)
            int recruitmentType = 0;

            if (result.FormData["recruitmentID"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn mã tuyển dụng.");
            }else
            {
                recruitmentID = result.FormData["recruitmentID"].ToString();
            }

            if (result.FormData["recruitmentType"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn loại hồ sơ để import.");
            }

            //Upload files
            int addedCount = 0;
            int.TryParse(result.FormData["recruitmentType"], out recruitmentType);

            

            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                //insert to DB
                var listRecruitmentStaffs = this.ReadEmpFromExcel(fullPath, recruitmentID , recruitmentType);
                if (listRecruitmentStaffs.Count > 0)
                {
                    foreach (var data in listRecruitmentStaffs)
                    {
                        //tao user
                        _dataService.Add(data);
                        
                        addedCount++;
                    }
                    _dataService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã import " + addedCount + " dòng thành công.");
        }

        /// <summary>
        /// Import thong tin nhanv ien tham gia khoa huan luyen
        /// </summary>
        /// <returns></returns>
        [Route("importtrainer")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public async Task<HttpResponseMessage> ImportTrainer()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được máy chủ hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            //Mã lần tuyển dụng
            string recruitmentID = "";
            //Loại tuyen dung huan luyen
            int recruitmentType = 1;

            if (result.FormData["recruitmentID"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn mã tuyển dụng.");
            }
            else
            {
                recruitmentID = result.FormData["recruitmentID"].ToString();
            }

            if (result.FormData["recruitmentType"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn loại hồ sơ để import.");
            }

            //Upload files
            int addedCount = 0;
            int.TryParse(result.FormData["recruitmentType"], out recruitmentType);



            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                //insert to DB
                var listRecruitmentStaffs = this.ReadEmpTrainerFromExcel(fullPath, recruitmentID, recruitmentType);
                if (listRecruitmentStaffs.Count > 0)
                {
                    foreach (var data in listRecruitmentStaffs)
                    {
                        //tao user
                        _dataService.Add(data);

                        addedCount++;
                    }
                    _dataService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã import " + addedCount + " dòng thành công.");
        }

        private List<RecruitmentStaff> ReadEmpFromExcel(string fullPath , string recruitmentID , int recruitmentType)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<RecruitmentStaff> listRecruitmentStaff = new List<RecruitmentStaff>();
                RecruitmentStaffViewModel recruitmentStaffViewModel;
                RecruitmentStaff recruitmentStaff;
                bool skipRow = false;
                for (int i = workSheet.Dimension.Start.Row + 4; i <= workSheet.Dimension.End.Row; i++)
                {
                    skipRow = false;
                    //check xem co doc dong khong phai doi tuong
                    string examRound1 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.ExamRound1].Text.ToString();
                    string examResult = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.ExamResult].Text.ToString();
                    DateTime? requestInterviewDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.RequestInterviewDate].Value.ToDateTime();

                    if (string.IsNullOrEmpty(examRound1.Trim()) || string.IsNullOrEmpty(examResult.Trim()) || examResult.Trim().ToLower()== "Loại".ToLower())
                    {
                        //bo qua qua khong doc dong nay 
                        skipRow = true;
                    }
                    if (skipRow == false)
                    {
                        recruitmentStaffViewModel = new RecruitmentStaffViewModel();
                        recruitmentStaff = new RecruitmentStaff();

                        recruitmentStaffViewModel.RecruitmentID = recruitmentID;
                        recruitmentStaffViewModel.RecruitmentStaffID = recruitmentID + "_" + workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CompanyCvNo].Value.ToInt(0);
                        recruitmentStaffViewModel.Name = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.FullName].Text.ToString();
                        recruitmentStaffViewModel.ShortName = recruitmentStaffViewModel.ShortName;
                        recruitmentStaffViewModel.RecruitmentTypeMasterID = (int)MasterKbnEnum.RecruitmentType;
                        recruitmentStaffViewModel.RecruitmentTypeMasterDetailID = recruitmentType;

                        recruitmentStaffViewModel.RequestInCompanyDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.RequestInCompanyDate].Value.ToDateTime();
                        recruitmentStaffViewModel.InterviewResult = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.InterviewResult].Text.ToString();
                        recruitmentStaffViewModel.RequestInterviewDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.RequestInterviewDate].Value.ToDateTime();
                        recruitmentStaffViewModel.InterViewTime = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.InterViewTime].Value.ToDateTime();
                        recruitmentStaffViewModel.ExamRound1 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.ExamRound1].Text.ToString();
                        recruitmentStaffViewModel.ExamResult = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.ExamResult].Text.ToString();
                        recruitmentStaffViewModel.CompanyCvNo = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CompanyCvNo].Value.ToInt(0);
                        recruitmentStaffViewModel.Pharse = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Pharse].Value.ToInt(0);
                        recruitmentStaffViewModel.FullName = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.FullName].Text.ToString();
                        recruitmentStaffViewModel.BirthDay = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.BirthDay].Value.ToDateTime();

                        int gender = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Gender].Value.ToString().ToInt(0);
                        if (gender == 1)
                        {
                            recruitmentStaffViewModel.Gender = true;
                        }
                        else
                        {
                            recruitmentStaffViewModel.Gender = false;
                        }


                        recruitmentStaffViewModel.National = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.National].Text.ToString();
                        recruitmentStaffViewModel.IdentNo = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.IdentNo].Text.ToString();
                        recruitmentStaffViewModel.PhoneNumber = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.PhoneNumber].Text.ToString();
                        recruitmentStaffViewModel.Email = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Email].Text.ToString();
                        recruitmentStaffViewModel.KiboSalary = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.KiboSalary].Value.ToDecimal(0);
                        recruitmentStaffViewModel.EducationLevel = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.EducationLevel].Text.ToString();
                        recruitmentStaffViewModel.CollectName = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CollectName].Text.ToString();
                        recruitmentStaffViewModel.ProfessionalKbn = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.ProfessionalKbn].Text.ToString();
                        recruitmentStaffViewModel.EducationType = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.EducationType].Text.ToString();
                        recruitmentStaffViewModel.Grade = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Grade].Text.ToString();
                        int certificated = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.IsCertificated].Text.ToString().ToInt(0);
                        if (certificated == 1)
                        {
                            recruitmentStaffViewModel.IsCertificated = true;
                        }
                        else
                        {
                            recruitmentStaffViewModel.IsCertificated = false;
                        }

                        recruitmentStaffViewModel.DebtSubjectCount = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.DebtSubjectCount].Text.ToInt(0);
                        recruitmentStaffViewModel.DebtSubjectReason = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.DebtSubjectReason].Text.ToString();
                        recruitmentStaffViewModel.CertificatedDateTime = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CertificatedDateTime].Text.ToString();
                        recruitmentStaffViewModel.OtherCertificated = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.OtherCertificated].Text.ToString();
                        recruitmentStaffViewModel.JapaneseLevel = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.JapaneseLevel].Text.ToString();
                        recruitmentStaffViewModel.EnglishLevel = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.EnglishLevel].Text.ToString();
                        recruitmentStaffViewModel.OtherSkill = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.OtherSkill].Text.ToString();
                        recruitmentStaffViewModel.MarriedStatus = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.MarriedStatus].Text.ToString();
                        recruitmentStaffViewModel.Objective = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Objective].Text.ToString();
                        recruitmentStaffViewModel.CvNote = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CvNote].Text.ToString();
                        recruitmentStaffViewModel.Comment1 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Comment1].Text.ToString();
                        recruitmentStaffViewModel.Comment2 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Comment2].Text.ToString();
                        recruitmentStaffViewModel.CvCreateDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CvCreateDate].Value.ToDateTime();
                        recruitmentStaffViewModel.CvUpdateDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CvUpdateDate].Value.ToDateTime();
                        recruitmentStaffViewModel.CvSendCount = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CvSendCount].Value.ToInt(0);
                        recruitmentStaffViewModel.CvSendList = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CvSendList].Text.ToString();
                        recruitmentStaffViewModel.StartWorkingDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.StartWorkingDate].Value.ToDateTime();
                        recruitmentStaffViewModel.AdddressPlace = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.AdddressPlace].Text.ToString();
                        recruitmentStaffViewModel.BornPlace = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.BornPlace].Text.ToString();
                        recruitmentStaffViewModel.Hobby = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.Hobby].Text.ToString();
                        recruitmentStaffViewModel.IsTestRound1ByPass = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.IsTestRound1ByPass].Text.ToString();
                        recruitmentStaffViewModel.GradeTestRound1 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.GradeTestRound1].Value.ToDecimal(0);
                        recruitmentStaffViewModel.EngGradeTestRound1 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.EngGradeTestRound1].Value.ToDecimal(0);
                        recruitmentStaffViewModel.ProfessionalKbnGradeTestRound1 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.ProfessionalKbnGradeTestRound1].Value.ToDecimal(0);
                        recruitmentStaffViewModel.GradeTestRound2 = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.GradeTestRound2].Value.ToDecimal(0);
                        recruitmentStaffViewModel.CvStatus = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.CvStatus].Text.ToString();
                        recruitmentStaffViewModel.EmpType = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.EmpType].Text.ToString();
                        recruitmentStaffViewModel.TrainingClassConditionTalkDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.TrainingClassConditionTalkDate].Value.ToDateTime();
                        recruitmentStaffViewModel.WorkingConditionTalkDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.WorkingConditionTalkDate].Value.ToDateTime();

                        recruitmentStaffViewModel.AccountData = User.Identity.GetApplicationUser().Email;

                        recruitmentStaffViewModel.DataStatus = (int)DataStatusEnum.REC_INTERVIEW_UNRIGISTER;
                        recruitmentStaffViewModel.Status = true;

                        recruitmentStaff.UpdateRecruitmentStaff(recruitmentStaffViewModel);
                        listRecruitmentStaff.Add(recruitmentStaff);
                    }
                    
                }
                return listRecruitmentStaff;
            }
        }

        /// <summary>
        /// Nhap thong tin huan luyen tu file excel 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="recruitmentID"></param>
        /// <param name="recruitmentType"></param>
        /// <returns></returns>
        private List<RecruitmentStaff> ReadEmpTrainerFromExcel(string fullPath, string recruitmentID, int recruitmentType)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<RecruitmentStaff> listRecruitmentStaff = new List<RecruitmentStaff>();
                RecruitmentStaffViewModel recruitmentStaffViewModel;
                RecruitmentStaff recruitmentStaff;
                bool skipRow = false;

                for (int i =10; i <= workSheet.Dimension.End.Row; i++)
                {
                    skipRow = false;
                    //check xem co doc dong khong phai doi tuong
                    string name = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.FullName].Text.ToString();
                    int no = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.No].Value.ToInt(0);

                    //DateTime? requestInterviewDate = workSheet.Cells[i, (int)RecruitmentStaffImportColumnEnum.RequestInterviewDate].Value.ToDateTime();

                    if (string.IsNullOrEmpty(name.Trim()) || no == 0 )
                    {
                    //bo qua qua khong doc dong nay 
                        skipRow = true;
                    }
                    if (skipRow == false)
                    {
                        recruitmentStaffViewModel = new RecruitmentStaffViewModel();
                        recruitmentStaff = new RecruitmentStaff();

                        recruitmentStaffViewModel.RecruitmentID = recruitmentID;
                        recruitmentStaffViewModel.RecruitmentStaffID = recruitmentID + "_" + workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.No].Value.ToInt(i-10);
                        recruitmentStaffViewModel.Name = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.FullName].Text.ToString();
                        recruitmentStaffViewModel.ShortName = recruitmentStaffViewModel.Name;
                        recruitmentStaffViewModel.FullName = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.FullName].Text.ToString();
                        recruitmentStaffViewModel.BirthDay = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.BirthDay].Value.ToDateTime();

                        int gender = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.Gender].Value.ToString().ToInt(0);
                        if (gender == 1)
                        {
                            recruitmentStaffViewModel.Gender = true;
                        }
                        else
                        {
                            recruitmentStaffViewModel.Gender = false;
                        }

                        recruitmentStaffViewModel.RecruitmentTypeMasterID = (int)MasterKbnEnum.RecruitmentType;
                        recruitmentStaffViewModel.RecruitmentTypeMasterDetailID = recruitmentType;

                        recruitmentStaffViewModel.ProfessionalKbn = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.ProgrammingLanguage].Text.ToString();
                        recruitmentStaffViewModel.EducationLevel = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.EducationLevel].Text.ToString();
                        recruitmentStaffViewModel.CollectName = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.CollectName].Text.ToString();
                        recruitmentStaffViewModel.EducationType = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.EducationType].Text.ToString();
                        recruitmentStaffViewModel.Grade = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.Grade].Text.ToString();
                                                
                        recruitmentStaffViewModel.Comment1 = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.Comment1].Text.ToString();
                        recruitmentStaffViewModel.Comment2 = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.Comment2].Text.ToString();
                        recruitmentStaffViewModel.GradeTestRound1 = workSheet.Cells[i, (int)RecruitmentStaffTrainerImportColumnEnum.GradeTestRound1].Value.ToDecimal(0);
                        
                        recruitmentStaffViewModel.AccountData = User.Identity.GetApplicationUser().Email;

                        recruitmentStaffViewModel.DataStatus = (int)DataStatusEnum.REC_INTERVIEW_UNRIGISTER;
                        recruitmentStaffViewModel.Status = true;

                        recruitmentStaff.UpdateRecruitmentStaff(recruitmentStaffViewModel);
                        listRecruitmentStaff.Add(recruitmentStaff);
                    }

                }
                return listRecruitmentStaff;
            }
        }


        [Route("getallautocompletedata")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetAllAutoCompleteData(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                string sql = " SELECT DISTINCT 1 as ID , GhostPC  Name FROM RecruitmentStaffs WHERE GhostPC IS NOT NULL";
                sql += " UNION ALL ";
                sql += " SELECT DISTINCT 2 as ID , InterviewRoom Name FROM RecruitmentStaffs WHERE InterviewRoom IS NOT NULL";

                var kekka = _dataService.GetDbContext().Database.SqlQuery<CodeNameViewModel>(sql).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, kekka);

                return response;
            });
        }
        /// <summary>
        /// Dang ky job
        /// </summary>
        /// <param name="table">Ten bang</param>
        /// <param name="tableKey">Khoa cua du lieu</param>
        /// <param name="tableKeyId">Khoa </param>
        /// <param name="dataRecruitmentStaff">data phong van</param>
        private  void registerAlertJob(string table, string tableKey, string tableKeyId, RecruitmentStaff dataRecruitmentStaff , bool isDeleteAction)
        {
            //ney nhu data phong van khong co nguoi dang ky thi xoa neu nhu da dang ky truoc do 
            if (!dataRecruitmentStaff.InterviewDate.HasValue || string.IsNullOrEmpty(dataRecruitmentStaff.InterviewRoom))
            {
                //xoa toan bo thong tin PV cua ung vien do
                this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(), table, tableKey, null);
            }

            //lay danh sach dang ky phong van
            var recInterviewStaffList = this._dataRecruitmentInterviewService.GetInterviewStaffByRecruitmentStaffForJob(dataRecruitmentStaff.RecruitmentID, dataRecruitmentStaff.RecruitmentStaffID);
            foreach (var item in recInterviewStaffList)
            {
                tableKeyId = item.RegInterviewEmpID.ToString(); //ma NV dang ky phong van 

                if (isDeleteAction)
                {
                    //delete job
                    this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(), table, tableKey, null);
                }
                else
                {
                    if (item.ScheduleInterviewDate.HasValue && (!string.IsNullOrEmpty(item.ScheduleInterviewRoom)))
                    {
                        JobSchedulerViewModel model = new JobSchedulerViewModel();
                        //loop qua tung nguoi dang ky job
                        //check xem da co ton tai hay chua ?
                        
                        var jobData = this._dataJobSchedulerService.GetByTableKey(((int)JobTypeEnum.DevInterviewDateNotify).ToString(),table, tableKey, tableKeyId).ToList().FirstOrDefault();
                        model = jobData.Adapt<JobScheduler, JobSchedulerViewModel>();

                        if (jobData != null && !string.IsNullOrEmpty(jobData.TableNameRelation))
                        {
                            //chua ton tai thi tao moi 
                            model.JobType = ((int)JobTypeEnum.DevInterviewDateNotify).ToString();
                            model.ScheduleRunJobDate = item.ScheduleInterviewDate;
                            model.EventDate = item.ScheduleInterviewDate;
                            model.EventUser = item.Name;
                            model.ToNotiEmailList = item.WorkingEmail;
                            model.SMSToNumber = item.PhoneNumber1;
                            model.LocationEvent = item.ScheduleInterviewRoom;
                        }
                        else
                        {
                            model = new JobSchedulerViewModel();
                            //chua ton tai thi tao moi 
                            model.JobType = ((int)JobTypeEnum.DevInterviewDateNotify).ToString();
                            model.Name = "Thông báo lịch phỏng vấn";
                            model.TableNameRelation = table;
                            model.TableKey = tableKey;
                            model.TableKeyID = tableKeyId;
                            model.ScheduleRunJobDate = item.ScheduleInterviewDate;
                            model.EventDate = item.ScheduleInterviewDate;
                            model.EventUser = item.Name;
                            model.FromEmail = "";
                            model.ToNotiEmailList = item.WorkingEmail;
                            model.CcNotiEmailList = "";
                            model.BccNotiEmailList = "";
                            model.SMSFromNumber = "";
                            model.SMSToNumber = item.PhoneNumber1;
                            //alertJob.SMSContent = "";
                            //alertJob.JobContent = "";.
                            model.JobStatus = 0;
                            //alertJob.ActualRunJobDate="";
                            model.TemplateID = (int)JobTypeEnum.DevInterviewDateNotify;
                            model.LocationEvent = item.ScheduleInterviewRoom;
                            model.Note = "Tạo tự động";

                        }
                        //goi api thuc thi cap nhat
                        var callApi = HttpClientHelper<JobSchedulerViewModel>.PostApiUseHttpClient("/api/jobscheduler/findregister", model);

                    }
                    else
                    {
                        //delete
                        this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(),table, tableKey, tableKeyId);

                    }//if

                }//if delete

            }//for
        }

        /// <summary>
        /// Dang ky alert sms job ve thong bao cho cap quan ly nhan vien sap nghi viec
        /// </summary>
        /// <param name="table">Ten bang</param>
        /// <param name="tableKey">Khoa cua du lieu</param>
        /// <param name="tableKeyId">Khoa </param>
        /// <param name="dataRecruitmentStaff">data phong van</param>
        private void registerAlertDevJobLeavedDateNotify(string table, string tableKey, string tableKeyId, Emp itemData, bool isDeleteAction)
        {
            //goi api thuc thi get data
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["staff"] = itemData.ID.ToString();
            query["includeManager"] = "1"; //lay ca thong tin cua manager
            query["otherStaff"] = "";

            string queryString = query.ToString();
            var notifyDataList = HttpClientHelper<EmpViewModel>.GetApiUseHttpClientRetList("/api/emp/getalertlistofstaff?" + queryString);

            foreach (var item in notifyDataList)
            {
                tableKeyId = item.ID.ToString(); //ma nhan vien

                if (isDeleteAction || !itemData.JobLeaveDate.HasValue)
                {
                    //delete job
                    this.deleteAlertJob(((int)JobTypeEnum.DevJobLeavedDateNotify).ToString(),table, tableKey, tableKeyId);
                }
                else
                {
                    //truong hop co ngay ket thuc trial va chua co ky HD va ngay ket thuc thu viec phai lon hon ngay hien tai
                    if (itemData.JobLeaveDate.HasValue && (itemData.JobLeaveDate.Value.Date >= DateTime.Now.Date) )
                    {
                        JobSchedulerViewModel model = new JobSchedulerViewModel();
                        //loop qua tung nguoi dang ky job
                        //check xem da co ton tai hay chua ?

                        var jobData = this._dataJobSchedulerService.GetByTableKey(((int)JobTypeEnum.DevJobLeavedDateNotify).ToString(),table, tableKey, tableKeyId).ToList().FirstOrDefault();
                        model = jobData.Adapt<JobScheduler, JobSchedulerViewModel>();

                        if (jobData != null && !string.IsNullOrEmpty(jobData.TableNameRelation))
                        {
                            //chua ton tai thi tao moi 
                            model.ScheduleRunJobDate = itemData.JobLeaveDate;//ngay nghi viec nhan vien
                            model.EventDate = itemData.JobLeaveDate;
                            model.ToNotiEmailList = item.WorkingEmail; //email cua nguoi se nhan tin nhan
                            model.SMSToNumber = item.PhoneNumber1;
                            model.LocationEvent = "";
                        }
                        else
                        {
                            model = new JobSchedulerViewModel();
                            //chua ton tai thi tao moi 
                            model.JobType = ((int)JobTypeEnum.DevJobLeavedDateNotify).ToString();
                            model.Name = "Thông báo sắp tới ngày nghỉ việc của NV";
                            model.TableNameRelation = table;
                            model.TableKey = tableKey;
                            model.TableKeyID = tableKeyId;
                            model.ScheduleRunJobDate = itemData.EndTrialDate;
                            model.EventDate = itemData.EndTrialDate;
                            model.EventUser = itemData.FullName;
                            model.FromEmail = "";
                            model.ToNotiEmailList = item.WorkingEmail;
                            model.CcNotiEmailList = "";
                            model.BccNotiEmailList = "";
                            model.SMSFromNumber = "";
                            model.SMSToNumber = item.PhoneNumber1;
                            //model.SMSContent = "";
                            //model.JobContent = "";.
                            model.JobStatus = 0;
                            //model.ActualRunJobDate="";
                            model.TemplateID = (int)JobTypeEnum.DevJobLeavedDateNotify;
                            model.LocationEvent = "";
                            model.Note = "Tạo tự động";

                        }

                        var callApi = HttpClientHelper<JobSchedulerViewModel>.PostApiUseHttpClient("/api/jobscheduler/findregister", model);

                    }
                    else
                    {
                        //delete neu nhu khong con setting ngay nghi viec
                        this.deleteAlertJob(((int)JobTypeEnum.DevJobLeavedDateNotify).ToString(),table, tableKey, tableKeyId);

                    }//if

                }//if delete

            }//for
        }
        /// <summary>
        /// Xoa thong tin job
        /// </summary>
        /// <param name="table">ten table</param>
        /// <param name="tableKey">ID cua nguoi duoc phong van</param>
        /// <param name="tableKeyId">ID cua nguoi dang phong van</param>
        private void deleteAlertJob(string jobType , string table, string tableKey, string tableKeyId)
        {
            //get thong tin job
            var jobData = this._dataJobSchedulerService.GetByTableKey(jobType,table, tableKey, tableKeyId).ToList().FirstOrDefault();
            if (jobData != null)
            {
                this._dataJobSchedulerService.Delete(jobData.ID);
                this._dataJobSchedulerService.SaveChanges();
            }
        }

    }
}