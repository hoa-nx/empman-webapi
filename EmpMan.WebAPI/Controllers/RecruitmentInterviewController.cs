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
using EmpMan.Common.ViewModels;
using EmpMan.Common.Enums;
using EmpMan.Common;
using Mapster;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/recruitmentinterview")]
    [Authorize]
    public class RecruitmentInterviewController : ApiControllerBase
    {
        private IRecruitmentInterviewService _dataService;
        private IJobSchedulerService _dataJobSchedulerService;
        public RecruitmentInterviewController(IErrorService errorService, IRecruitmentInterviewService dataService , IJobSchedulerService dataJobSchedulerService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._dataJobSchedulerService = dataJobSchedulerService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<RecruitmentInterviewViewModel>>(listData);
                var listDataVm = listData.Adapt<List<RecruitmentInterviewViewModel>>();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallinterviewerlist")]
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, SearchItemViewModel searchParam)
        {
            return CreateHttpResponse(request, () =>
            {
                string sql = "";

                string recruitmentWhere = "";

                
                var response = request.CreateResponse(HttpStatusCode.BadRequest, "Không get được data");
                if (searchParam != null)
                {
                    string[] recruitmentID = searchParam.StringItems.ToArray();

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

                    sql = @"SELECT DISTINCT
	                              REI.RegInterviewEmpID      ID
	                            , EMP.FullName               Name
                            FROM
	                            RecruitmentInterviews REI 
	                            left outer join Emps EMP  ON REI.RegInterviewEmpID = EMp.ID 
                            WHERE REI.RecruitmentID IN ( " + recruitmentWhere + @")
                            ORDER BY 
	                            EMP.FullName";
                    var model = _dataService.GetDbContext().Database.SqlQuery<CodeNameViewModel>(sql).AsEnumerable();


                    response = request.CreateResponse(HttpStatusCode.OK, model);
                }

                return response;
            });
        }

        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page - 1 * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<RecruitmentInterview>, List<RecruitmentInterviewViewModel>>(query);
                var responseData = query.Adapt<List<RecruitmentInterview>, List<RecruitmentInterviewViewModel>>();

                var paginationSet = new PaginationSet<RecruitmentInterviewViewModel>()
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

        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<RecruitmentInterview, RecruitmentInterviewViewModel>(model);
                var responseData = model.Adapt<RecruitmentInterview, RecruitmentInterviewViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("detailbyrecruitmentstaff")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetByRecruitmentStaff(HttpRequestMessage request, string recruitmentID , string recruitmentStaffID, int regInterviewEmpID)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetSingleByCondition(x => (x.RecruitmentID == recruitmentID && x.RecruitmentStaffID == recruitmentStaffID && x.RegInterviewEmpID == regInterviewEmpID));

                //var responseData = Mapper.Map<RecruitmentInterview, RecruitmentInterviewViewModel>(model);
                var responseData = model.Adapt<RecruitmentInterview, RecruitmentInterviewViewModel>();
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getinterviewstaffbyrecruitmentstaff")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetInterviewStaffByRecruitmentStaff(HttpRequestMessage request, string recruitmentID, string recruitmentStaffID)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetMultiByCondition(x => (x.RecruitmentID == recruitmentID && x.RecruitmentStaffID == recruitmentStaffID ), new string[] { "RegInterviewEmp" , "RecruitmentStaff" });

                //var responseData = Mapper.Map<List<RecruitmentInterviewViewModel>>(model);
                var responseData = model.Adapt<List<RecruitmentInterviewViewModel>>();
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getinterviewstaffbyrecruitmentstaffforjob")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage GetInterviewStaffByRecruitmentStaffForJob(HttpRequestMessage request, string recruitmentID, string recruitmentStaffID)
        {
            return CreateHttpResponse(request, () =>
            {
                //string sql = @" SELECT 
                //                    REI.* 
                //                ,   EMP.WorkingEmail 
                //                ,   EMP.PhoneNumber1 
                //                ,   EMP.FullName Label 
                //                FROM [dbo].[RecruitmentInterviews] REI 
                //                LEFT OUTER JOIN EMPS EMP ON REI.RegInterviewEmpID = EMP.ID 
                //                WHERE UPPER(REI.RecruitmentID)='" + recruitmentID.ToUpper() + "' AND UPPER(REI.RecruitmentStaffID) ='" + recruitmentStaffID.ToUpper() +"'";

                //var responseData = _dataService.GetDbContext().Database.SqlQuery<RecruitmentInterviewViewModel>(sql).ToList();

                var responseData = _dataService.GetInterviewStaffByRecruitmentStaffForJob(recruitmentID, recruitmentStaffID);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage Create(HttpRequestMessage request, RecruitmentInterviewViewModel dataVm)
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
                    RecruitmentInterview newData = new RecruitmentInterview();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateRecruitmentInterview(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    //dang ky job
                    this.registerAlertJob("RecruitmentStaffs", data.RecruitmentStaffID, data.ID.ToString(), data, false);

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("addmulti")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage AddMulti(HttpRequestMessage request, RecruitmentInterviewViewModel[] dataVm)
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
                    int count = 0;
                    
                    foreach (var item in dataVm)
                    {
                        RecruitmentInterview newData = new RecruitmentInterview();

                        /** cập nhật các thông tin chung **/
                        newData.CreatedDate = DateTime.Now;
                        newData.CreatedBy = User.Identity.Name;

                        newData.UpdatedDate = DateTime.Now;
                        newData.UpdatedBy = User.Identity.Name;
                        //Người sở hữu dữ liệu
                        newData.AccountData = User.Identity.GetApplicationUser().Email;


                        newData.UpdateRecruitmentInterview(item);

                        var data = _dataService.Add(newData);

                        //dang ky job
                        this.registerAlertJob("RecruitmentStaffs", data.RecruitmentStaffID, data.ID.ToString(), data, false);

                        count++;
                    }

                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, count);
                }

                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage Update(HttpRequestMessage request, RecruitmentInterviewViewModel dataVm)
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

                    dataFromDb.UpdateRecruitmentInterview(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<RecruitmentInterview, RecruitmentInterviewViewModel>(dataFromDb);

                    //dang ky job
                    this.registerAlertJob("RecruitmentStaffs", dataFromDb.RecruitmentStaffID, dataFromDb.ID.ToString(), dataFromDb, false);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("approveddatastatus")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
        public HttpResponseMessage UpdateApprovedStatus(HttpRequestMessage request, SearchItemViewModel approvedModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (approvedModel != null && approvedModel.NumberItems!=null && approvedModel.NumberItems.Count()>0)
                {
                    _dataService.ChangeApprovedStatusMulti(item => approvedModel.NumberItems.Contains(item.ID), User.Identity.Name, approvedModel.ApprovedStatus);
                    _dataService.SaveChanges();
                }

                response = request.CreateResponse(HttpStatusCode.OK, "");

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
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

                    var responseData = Mapper.Map<RecruitmentInterview, RecruitmentInterviewViewModel>(oldDataFromDb);

                    //delete thong tin job da dang ky
                    this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(),"RecruitmentStaffs", oldDataFromDb.RecruitmentStaffID,oldDataFromDb.RegInterviewEmpID.ToString());

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.RECRUITMENT_INTERVIEW)]
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

                        //delete thong tin job da dang ky
                        this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(),"RecruitmentStaffs", oldDataFromDb.RecruitmentStaffID, oldDataFromDb.RegInterviewEmpID.ToString());
                    }

                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listData.Count);
                }

                return response;
            });
        }

        /// <summary>
        /// Dang ky job / Xoa job
        /// </summary>
        /// <param name="table">Ten bang</param>
        /// <param name="tableKey">Khoa cua du lieu</param>
        /// <param name="tableKeyId">Khoa </param>
        /// <param name="dataRecruitmentInterview">data phong van</param>
        private void registerAlertJob(string table, string tableKey, string tableKeyId, RecruitmentInterview dataRecruitmentInterview, bool isDeleteAction)
        {
            //ney nhu data phong van khong co nguoi dang ky thi khong thuc hien tiep theo
            if (!dataRecruitmentInterview.ScheduleInterviewDate.HasValue || string.IsNullOrEmpty(dataRecruitmentInterview.ScheduleInterviewRoom))
            {
                //xoa toan bo job neu nhu chua co thong tin phong van cua ung vien
                this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(), table, tableKey, null);
            }
            //lay danh sach dang ky phong van
            var recInterviewStaffList = this._dataService.GetInterviewStaffByRecruitmentStaffForJob(dataRecruitmentInterview.RecruitmentID, dataRecruitmentInterview.RecruitmentStaffID);
            foreach (var item in recInterviewStaffList)
            {
                tableKeyId = item.RegInterviewEmpID.ToString(); //ma NV dang ky phong van 

                if (isDeleteAction)
                {
                    //delete job ( XOA TOAN BO NHUNG NGUOI DANG KY PV UNG VIEN)
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

                    }//if
                    else
                    {
                        //delete
                        this.deleteAlertJob(((int)JobTypeEnum.DevInterviewDateNotify).ToString(), table, tableKey, tableKeyId);

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
        private void deleteAlertJob(string jobType ,string table, string tableKey, string tableKeyId)
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