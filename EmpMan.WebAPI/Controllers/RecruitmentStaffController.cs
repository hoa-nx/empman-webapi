using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmpMan.Model.Models;
using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using EmpMan.Web.Infrastructure.Extensions;
using EmpMan.Web.Models;
using EmpMan.Web.Providers;
using System.Linq;
using System;
using EmpMan.Web.Models.Master;
using System.Web.Script.Serialization;
using EmpMan.Web.Models.Emp;
using OfficeOpenXml;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using EmpMan.Common.Enums;
using EmpMan.Common;
using EmpMan.Common.ViewModels;
using EmpMan.Web.Models.File;
using Mapster;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/recruitmentstaff")]
    [Authorize]
    public class RecruitmentStaffController : ApiControllerBase
    {
        private IRecruitmentStaffService _dataService;
        private IFileStorageService _dataFileService;
        private IEmpService _dataEmpService;

        public RecruitmentStaffController(IErrorService errorService, IRecruitmentStaffService dataService , IFileStorageService dataFileService , IEmpService dataEmpService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._dataFileService = dataFileService;
            this._dataEmpService = dataEmpService;

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
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NULL) THEN 20 
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
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate >  CONVERT(DATE,GETDATE()) THEN 70 
											--@@Đã vào thử việc nhưng chưa đăng ký nhân viên
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate <= CONVERT(DATE,GETDATE()) AND EMP.ID IS NULL THEN 80 
											--@@Đã vào thử việc và đã đăng ký nhân viên
											when REC.ID IS NOT NULL AND ( RES.InterviewDate IS NOT NULL AND DATEDIFF(MINUTE, RES.InterviewDate , GETDATE()) > 0) AND RTRIM(LTRIM(ISNULL(RES.InterviewResult,''))) !='' AND RES.TrialStartDate <= CONVERT(DATE,GETDATE()) AND EMP.ID IS NOT NULL THEN 90 
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
                    newData.CreatedDate = DateTime.Now;
                    newData.CreatedBy = User.Identity.Name;
                    
                    newData.UpdatedDate = DateTime.Now;
                    newData.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    newData.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateRecruitmentStaff(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

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

                    var responseData = Mapper.Map<RecruitmentStaff, RecruitmentStaffViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        private void UpdateRecruitmentInterviewRelationData(RecruitmentStaffViewModel dataVm)
        {
            if(dataVm!=null  && dataVm.InterviewDate.HasValue)
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
                        _dataService.Delete(item);
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


    }
}