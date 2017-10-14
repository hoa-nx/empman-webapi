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
using EmpMan.Common.ViewModels.Models.Project;
using EmpMan.Common.ViewModels.Models.Master;
using EmpMan.Common.ViewModels.Models.Revenue;
using EmpMan.Common.ViewModels;
using System.Threading.Tasks;
using EmpMan.Common;
using System.Web;
using System.IO;
using System.Configuration;
using Mapster;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/target")]
    [Authorize]
    public class TargetController : ApiControllerBase
    {
        private ITargetService _dataService;

        public TargetController(IErrorService errorService, ITargetService dataService) :
            base(errorService)
        {
            this._dataService = dataService;
        }

        [Route("getall")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<TargetViewModel>>(listData);
                var listDataVm = listData.Adapt<List<TargetViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword, null);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<Target>, List<TargetViewModel>>(query);
                var responseData = query.Adapt<List<Target>, List<TargetViewModel>>();

                var paginationSet = new PaginationSet<TargetViewModel>()
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
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<Target, TargetViewModel>(model);
                var responseData = model.Adapt<Target, TargetViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("new/{id:int}")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetRecordInit(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var newRecord = new TargetViewModel();
                DateTime reportYM;
                var ok = DateTime.TryParse(DateTime.Now.ToString("yyyy/MM/01"), out reportYM);

                newRecord.YearMonth = reportYM;
                newRecord.Name = "Mục tiêu " + newRecord.YearMonth;


                var response = request.CreateResponse(HttpStatusCode.OK, newRecord);

                return response;
            });
        }

        [Route("getallpagingmasterdata")]
        [HttpPost]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetAllPagingRelationData(HttpRequestMessage request, SearchItemViewModel searchModel)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                int page = searchModel.Page.Value;
                int pageSize = searchModel.PageSize.Value;

                var model = _dataService.GetAll(searchModel.Keyword, new string[] {"Company", "Dept","Team" });

                if (searchModel.DateTimeItems != null && searchModel.DateTimeItems.Count() > 0)
                {
                    //    //Expression<Func<Revenue, bool>> revenueYearMonth = c => c.ReportYearMonth == DateTime.Now;

                    model = model.Where(p => (searchModel.DateTimeItems.Contains(p.YearMonth)));

                }

                if (searchModel.NumberItems != null && searchModel.NumberItems.Count() > 0)
                {
                    //filter theo ma phong ban
                    model = model.Where(p => (searchModel.NumberItems.Contains(p.DeptID)));
                }

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.YearMonth).OrderByDescending(x => x.DeptID).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                
                var responseData = Mapper.Map<List<Target>, List<TargetViewModel>>(query);

                //cap nhat cac tri khong get tu bang chinh
                foreach (TargetViewModel item in responseData)
                {
                    if(item.Company !=null)
                        item.CompanyName = item.Company.Name;

                    if (item.Dept != null)
                        item.DeptName = item.Dept.Name;

                    if (item.Team != null)
                        item.TeamName = item.Team.Name;
                }

                var paginationSet = new PaginationSet<TargetViewModel>()
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

        [Route("getalldatabyyearmonth")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetAllGroupByYearMonth(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _dataService.GetAll();

                totalRow = model.Count();
                var groupData = model.GroupBy(p => p.YearMonth,
                                          (key, elements) =>
                                          new
                                          {
                                              id = key,
                                              name = "" + key.Year + "/" + key.Month
                                          })
                                          .ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, groupData);
                return response;
            });
        }


        [Route("add")]
        [HttpPost]
        //[Permission(Action = "Create", Function = "USER")]
        public HttpResponseMessage Create(HttpRequestMessage request, TargetViewModel dataVm)
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
                    Target newData = new Target();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateTarget(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        //[Permission(Action = "Update", Function = "USER")]
        public HttpResponseMessage Update(HttpRequestMessage request, TargetViewModel dataVm)
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

                    dataFromDb.UpdateTarget(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<Target, TargetViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        //[Permission(Action = "Delete", Function = "USER")]
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

                    var responseData = Mapper.Map<Target, TargetViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [HttpGet]
        [Route("tableExportXls")]
        public async Task<HttpResponseMessage> TableExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Muc tieu phong ban " + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _dataService.GetAll(filter,null).ToList();

                //get sql
                //string sql = GetAllFromViewEmpSql(filter);
                //var listData = _dataService.GetDbContext().Database.SqlQuery<EmpViewModel>(sql).ToList();

                string pw = ConfigurationManager.AppSettings["Password"].ToString();

                await ReportHelper.GenerateXls(data, fullPath, pw);

                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}