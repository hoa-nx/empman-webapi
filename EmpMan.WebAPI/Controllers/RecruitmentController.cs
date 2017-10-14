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
using EmpMan.Common;
using Mapster;
using EmpMan.Common.ViewModels;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/recruitment")]
    [Authorize]
    public class RecruitmentController : ApiControllerBase
    {
        private IRecruitmentService _dataService;

        public RecruitmentController(IErrorService errorService, IRecruitmentService dataService) :
            base(errorService)
        {
            this._dataService = dataService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll().OrderByDescending(x => x.ID);

                //var listDataVm = Mapper.Map<List<RecruitmentViewModel>>(listData);
                var listDataVm = listData.Adapt<List<RecruitmentViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.ID).Skip(page - 1 * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<Recruitment>, List<RecruitmentViewModel>>(query);
                var responseData = query.Adapt<List<Recruitment>, List<RecruitmentViewModel>>();

                var paginationSet = new PaginationSet<RecruitmentViewModel>()
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
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT_STAFF)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, SearchItemViewModel searchParam)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                int page = 0;
                int pageSize = 0;

                var response = request.CreateResponse(HttpStatusCode.BadRequest, "Không get được data");
                if (searchParam != null)
                {
                    page = searchParam.Page.Value;
                    pageSize = searchParam.PageSize.Value;

                    var model = GetRecruimentByType(searchParam.NumberItems.ToArray(), searchParam.Keyword);

                    totalRow = model.Count();

                    var query = model.OrderByDescending(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    var paginationSet = new PaginationSet<RecruitmentViewModel>()
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

        private IEnumerable<RecruitmentViewModel> GetRecruimentByType(int?[] recruitmentTypeID, string keyword)
        {

            string sql = @"
                /****** Script for SelectTopNRows command from SSMS  ******/
                SELECT REC.[ID]
                      ,REC.[No]
                      ,REC.[Name]
                      ,REC.[ShortName]
                      ,REC.[RecruitmentTypeMasterID]
                      ,REC.[RecruitmentTypeMasterDetailID]
                      ,REC.[CvCompanyFolderPath]
                      ,REC.[CvDeptFolderPath]
                      ,REC.[CvCount]
                      ,REC.[SendMailFromEmpID]
                      ,REC.[SendMailToEmpID]
                      ,REC.[AnsRecruitDeptDeadlineDate]
                      ,REC.[AnsLocalDeadlineDate]
                      ,REC.[IsNotification]
                      ,REC.[ExpireDate]
                      ,REC.[Content]
                      ,REC.[IsFinished]
                      ,REC.[FileID]
                      ,REC.[RowVersion]
                      ,REC.[DisplayOrder]
                      ,REC.[AccountData]
                      ,REC.[Note]
                      ,REC.[AccessDataLevel]
                      ,REC.[CreatedDate]
                      ,REC.[CreatedBy]
                      ,REC.[UpdatedDate]
                      ,REC.[UpdatedBy]
                      ,REC.[MetaKeyword]
                      ,REC.[MetaDescription]
                      ,REC.[Status]
                      ,REC.[DataStatus]
                      ,REC.[UserAgent]
                      ,REC.[UserHostAddress]
                      ,REC.[UserHostName]
                      ,REC.[RequestDate]
                      ,REC.[RequestBy]
                      ,REC.[ApprovedDate]
                      ,REC.[ApprovedBy]
                      ,REC.[ApprovedStatus]
	                  ,MDE.[Name] RecruitmentTypeName
                      ,RES.ReceivedStaffCount
                  FROM [dbo].[Recruitments] REC
                  LEFT OUTER JOIN MasterDetails MDE ON REC.RecruitmentTypeMasterID = MDE.MasterID AND REC.RecruitmentTypeMasterDetailID = MDE.MasterDetailCode
                  LEFT OUTER JOIN (SELECT RES.RecruitmentID , COUNT(*) ReceivedStaffCount FROM RecruitmentStaffs RES WHERE RES.DeptReceived IN(@DeptReceived@) AND RES.SystemEmpID >0  GROUP BY RES.RecruitmentID)  RES ON REC.ID = RES.RecruitmentID  
                    
                  ";

            //thay thep dept ID
            sql = sql.Replace("@DeptReceived@",  User.Identity.GetApplicationUser().DeptID.ToString());

            var query = _dataService.GetDbContext().Database.SqlQuery<RecruitmentViewModel>(sql).AsEnumerable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (x.Name + x.Note + x.ID + x.CvDeptFolderPath + x.CvCompanyFolderPath).ToLower().Contains(keyword.ToLower()));

            if (recruitmentTypeID != null && recruitmentTypeID.Count() > 0)
                query = query.Where(x => recruitmentTypeID.Contains(x.RecruitmentTypeMasterDetailID));

            return query;
        }
        

        [Route("detail/{id}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.RECRUITMENT)]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetByKey(id);

                //var responseData = Mapper.Map<Recruitment, RecruitmentViewModel>(model);
                var responseData = model.Adapt<Recruitment, RecruitmentViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.RECRUITMENT)]
        public HttpResponseMessage Create(HttpRequestMessage request, RecruitmentViewModel dataVm)
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
                    Recruitment newData = new Recruitment();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateRecruitment(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.RECRUITMENT)]
        public HttpResponseMessage Update(HttpRequestMessage request, RecruitmentViewModel dataVm)
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
                    var dataFromDb = _dataService.GetByKey(dataVm.ID);

                    dataFromDb.UpdateRecruitment(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<Recruitment, RecruitmentViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.RECRUITMENT)]
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

                    var responseData = Mapper.Map<Recruitment, RecruitmentViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.RECRUITMENT)]
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

    }
}