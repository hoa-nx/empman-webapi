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
using System.Threading.Tasks;
using EmpMan.Common;
using System.Web;
using System.IO;
using System.Configuration;
using Mapster;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/customerunitprice")]
    [Authorize]
    public class CustomerUnitPriceUnitPriceController : ApiControllerBase
    {
        private ICustomerUnitPriceService _dataService;

        public CustomerUnitPriceUnitPriceController(IErrorService errorService, ICustomerUnitPriceService dataService) :
            base(errorService)
        {
            this._dataService = dataService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll().OrderByDescending(p => p.StartDate);

                //var listDataVm = Mapper.Map<List<CustomerUnitPriceViewModel>>(listData);
                var listDataVm = listData.Adapt<List<CustomerUnitPriceViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<CustomerUnitPrice>, List<CustomerUnitPriceViewModel>>(query);
                var responseData = query.Adapt<List<CustomerUnitPrice>, List<CustomerUnitPriceViewModel>>();
                var paginationSet = new PaginationSet<CustomerUnitPriceViewModel>()
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

        [Route("getbycustomer")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage GetUnitPriceByCustomerUnitPrice(HttpRequestMessage request , int customerID , DateTime startDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetUnitPriceByCustomer(customerID, startDate );

                //var listDataVm = Mapper.Map<CustomerUnitPriceViewModel>(listData);
                var listDataVm = listData.Adapt<CustomerUnitPriceViewModel>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }

        [Route("getmultibycustomer")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage GetMultiUnitPriceByCustomerUnitPrice(HttpRequestMessage request, int customerID, DateTime startDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetMultiUnitPriceByCustomer(customerID, startDate).ToList();

                //var responseData = Mapper.Map<List<CustomerUnitPrice>, List<CustomerUnitPriceViewModel>>(listData);
                var responseData = listData.Adapt<List<CustomerUnitPrice>, List<CustomerUnitPriceViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<CustomerUnitPrice, CustomerUnitPriceViewModel>(model);
                var responseData = model.Adapt<CustomerUnitPrice, CustomerUnitPriceViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage Create(HttpRequestMessage request, CustomerUnitPriceViewModel dataVm)
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
                    CustomerUnitPrice newData = new CustomerUnitPrice();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateCustomerUnitPrice(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public HttpResponseMessage Update(HttpRequestMessage request, CustomerUnitPriceViewModel dataVm)
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

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;

                    dataFromDb.UpdateCustomerUnitPrice(dataVm);

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<CustomerUnitPrice, CustomerUnitPriceViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
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

                    var responseData = Mapper.Map<CustomerUnitPrice, CustomerUnitPriceViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [HttpGet]
        [Route("tableExportXls")]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.CUSTOMER_UNITPRICE)]
        public async Task<HttpResponseMessage> TableExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Danh sach don gia khach hang" + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var data = _dataService.GetAll(filter).ToList();

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