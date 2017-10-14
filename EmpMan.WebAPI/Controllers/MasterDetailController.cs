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
using EmpMan.Common.ViewModels.Models.Common;
using EmpMan.Common.ViewModels.Models.Master;
using System.Web.Script.Serialization;
using Mapster;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/masterdetail")]
    [Authorize]
    public class MasterDetailController : ApiControllerBase
    {
        private IMasterDetailService _dataService;

        public MasterDetailController(IErrorService errorService, IMasterDetailService dataService) :
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

                //var listDataVm = Mapper.Map<List<MasterDetailViewModel>>(listData);
                var listDataVm = listData.Adapt<List<MasterDetailViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int? filterMasterID, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(filterMasterID, keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<MasterDetail>, List<MasterDetailViewModel>>(query);
                var responseData = query.Adapt<List<MasterDetail>, List<MasterDetailViewModel>>();

                var paginationSet = new PaginationSet<MasterDetailViewModel>()
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
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<MasterDetail>, List<MasterDetailViewModel>>(query);
                var responseData = query.Adapt<List<MasterDetail>, List<MasterDetailViewModel>>();
                var paginationSet = new PaginationSet<MasterDetailViewModel>()
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

        [Route("getbykbn/{kbn:int}")]
        [HttpGet]
        public HttpResponseMessage GetDetailByKbn(HttpRequestMessage request , int kbn)
        {
            return CreateHttpResponse(request, () =>
            {
                var response = request.CreateResponse(HttpStatusCode.OK, GetMasterDetailByKbn(kbn));
                return response;
            });
        }

        private List<MasterDetailViewModel> GetMasterDetailByKbn(int selectedKbn)
        {
            List<MasterDetailViewModel> items = new List<MasterDetailViewModel>();

            //get from DB
            var allDatas = _dataService.GetAll().Where(x => x.MasterID == selectedKbn).ToList();

            //items = Mapper.Map<List<MasterDetail>, List<MasterDetailViewModel>>(allDatas);
            items = allDatas.Adapt<List<MasterDetail>, List<MasterDetailViewModel>>();
            return items;
        }


        [Route("detail/{id:int}")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<MasterDetail, MasterDetailViewModel>(model);
                var responseData = model.Adapt<MasterDetail, MasterDetailViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("detailpk")]
        [HttpGet]
        //[Permission(Action = "Read", Function = "USER")]
        public HttpResponseMessage GetByKey(HttpRequestMessage request, int masterID , int masterDetailID)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetByKey(masterID, masterDetailID);

                //var responseData = Mapper.Map<MasterDetail, MasterDetailViewModel>(model);
                var responseData = model.Adapt<MasterDetail, MasterDetailViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        //[Permission(Action = "Create", Function = "USER")]
        public HttpResponseMessage Create(HttpRequestMessage request, MasterDetailViewModel dataVm)
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
                    MasterDetail newData = new MasterDetail();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateMasterDetail(dataVm);

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
        public HttpResponseMessage Update(HttpRequestMessage request, MasterDetailViewModel dataVm)
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
                    var dataFromDb = _dataService.GetByKey(dataVm.MasterID, dataVm.MasterDetailCode);

                    dataFromDb.UpdateMasterDetail(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<MasterDetail, MasterDetailViewModel>(dataFromDb);
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

                    var responseData = Mapper.Map<MasterDetail, MasterDetailViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
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