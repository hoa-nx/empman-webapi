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
using EmpMan.Common.ViewModels.Models.Project;
using EmpMan.Common.ViewModels;
using EmpMan.Common;
using Mapster;
using EmpMan.Web.Common.ViewModels.Project;
using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/estimate")]
    [Authorize]
    public class EstimateController : ApiControllerBase
    {
        private IEstimateService _dataService;
        private IFileStorageService _dataFileService;

        public EstimateController(IErrorService errorService, IEstimateService dataService, IFileStorageService dataFileService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._dataFileService = dataFileService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.ESTIMATE)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<EstimateViewModel>>(listData);
                var listDataVm = listData.Adapt<List<EstimateViewModel>>();

                //get attach file 
                listDataVm = this.AddAttachtment(listDataVm);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.ESTIMATE)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<Estimate>, List<EstimateViewModel>>(query);
                var responseData = query.Adapt<List<Estimate>, List<EstimateViewModel>>();
                //get attach file 
                responseData = this.AddAttachtment(responseData);

                var paginationSet = new PaginationSet<EstimateViewModel>()
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
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.ESTIMATE)]
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

                    var model = _dataService.GetAll(searchParam.Keyword , new string[] {"Customer" });

                    totalRow = model.Count();

                    var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    //var responseData = Mapper.Map<List<Estimate>, List<EstimateViewModel>>(query);
                    var responseData = query.Adapt<List<Estimate>, List<EstimateViewModel>>();

                    //get attach file 
                    responseData = this.AddAttachtment(responseData);

                    var paginationSet = new PaginationSet<EstimateViewModel>()
                    {
                        Items = responseData,
                        PageIndex = page,
                        TotalRows = totalRow,
                        PageSize = pageSize
                    };

                    response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                }

                return response;
            });
        }

        [Route("detail/{id}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.ESTIMATE)]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetByKey(id);

                //var responseData = Mapper.Map<Estimate, EstimateViewModel>(model);
                var responseData = model.Adapt<Estimate, EstimateViewModel>();

                //if (responseData.OtherSofts != null && responseData.OtherSofts.Length>0)
                //{
                //    responseData.OtherSofts = string.Join(",", new JavaScriptSerializer().Deserialize<string[]>(responseData.OtherSofts));
                //}

                //chuyen doi string thanh List<string> 
                if (responseData.OS != null && responseData.OS.Trim().Length>0)
                {
                    responseData.OSLists = StringHelper.SeperatedStringToList(responseData.OS, ',');
                }
                if (responseData.Language != null && responseData.Language.Trim().Length > 0)
                {
                    responseData.LanguageLists = StringHelper.SeperatedStringToList(responseData.Language, ',');
                }
                if (responseData.OtherSofts != null && responseData.OtherSofts.Trim().Length > 0)
                {
                    responseData.OtherSoftLists = StringHelper.SeperatedStringToList(responseData.OtherSofts, ',');
                }

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.ESTIMATE)]
        public HttpResponseMessage Create(HttpRequestMessage request, EstimateViewModel dataVm)
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
                    Estimate newData = new Estimate();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;

                    //chuyen doi List<string> thanh string 
                    //OS 
                    if (dataVm.OSLists != null && dataVm.OSLists.Count > 0)
                    {
                        dataVm.OS = StringHelper.ListToStringSeperated(dataVm.OSLists, ",");
                    }
                    else
                    {
                        dataVm.OS = null;
                    }
                    //Language
                    if (dataVm.LanguageLists != null && dataVm.LanguageLists.Count > 0)
                    {
                        dataVm.Language = StringHelper.ListToStringSeperated(dataVm.LanguageLists, ",");
                    }
                    else
                    {
                        dataVm.Language = null;
                    }
                    //other
                    if (dataVm.OtherSoftLists != null && dataVm.OtherSoftLists.Count > 0)
                    {
                        dataVm.OtherSofts = StringHelper.ListToStringSeperated(dataVm.OtherSoftLists, ",");
                    }
                    else
                    {
                        dataVm.OtherSofts = null;
                    }

                    newData.UpdateEstimate(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<Estimate, EstimateViewModel>(data);
                    //chuyen doi string thanh List<string> 
                    if (responseData.OS != null && responseData.OS.Trim().Length > 0)
                    {
                        responseData.OSLists = StringHelper.SeperatedStringToList(responseData.OS, ',');
                    }
                    if (responseData.Language != null && responseData.Language.Trim().Length > 0)
                    {
                        responseData.LanguageLists = StringHelper.SeperatedStringToList(responseData.Language, ',');
                    }
                    if (responseData.OtherSofts != null && responseData.OtherSofts.Trim().Length > 0)
                    {
                        responseData.OtherSoftLists = StringHelper.SeperatedStringToList(responseData.OtherSofts, ',');
                    }

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.ESTIMATE)]
        public HttpResponseMessage Update(HttpRequestMessage request, EstimateViewModel dataVm)
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

                    //chuyen doi List<string> thanh string 
                    //OS 
                    if (dataVm.OSLists != null && dataVm.OSLists.Count > 0)
                    {
                        dataVm.OS = StringHelper.ListToStringSeperated(dataVm.OSLists, ",");
                    }else
                    {
                        dataVm.OS = null;
                    }
                    //Language
                    if (dataVm.LanguageLists != null && dataVm.LanguageLists.Count > 0)
                    {
                        dataVm.Language = StringHelper.ListToStringSeperated(dataVm.LanguageLists, ",");
                    }
                    else
                    {
                        dataVm.Language = null;
                    }
                    //other
                    if (dataVm.OtherSoftLists != null && dataVm.OtherSoftLists.Count > 0)
                    {
                        dataVm.OtherSofts = StringHelper.ListToStringSeperated(dataVm.OtherSoftLists, ",");
                    }
                    else
                    {
                        dataVm.OtherSofts = null;
                    }


                    dataFromDb.UpdateEstimate(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;
                    
                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<Estimate, EstimateViewModel>(dataFromDb);

                    //chuyen doi string thanh List<string> 
                    if (responseData.OS != null && responseData.OS.Trim().Length > 0)
                    {
                        responseData.OSLists = StringHelper.SeperatedStringToList(responseData.OS, ',');
                    }
                    if (responseData.Language != null && responseData.Language.Trim().Length > 0)
                    {
                        responseData.LanguageLists = StringHelper.SeperatedStringToList(responseData.Language, ',');
                    }
                    if (responseData.OtherSofts != null && responseData.OtherSofts.Trim().Length > 0)
                    {
                        responseData.OtherSoftLists = StringHelper.SeperatedStringToList(responseData.OtherSofts, ',');
                    }

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        
        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.ESTIMATE)]
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

                    var responseData = Mapper.Map<Estimate, EstimateViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.ESTIMATE)]
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

        private List<EstimateViewModel> AddAttachtment(List<EstimateViewModel> list)
        {
            var listReturn = new List<EstimateViewModel>(list);
            foreach (EstimateViewModel item in listReturn)
            {
                //get thong tin file dinh kem cua ung vien tuong ung
                var fileList = _dataFileService.GetAllByKey("Estimates", item.ID).ToList();
                //var fileResultModel = Mapper.Map<List<FileResultViewModel>>(fileList);
                var fileResultModel = fileList.Adapt<List<FileResultViewModel>>();
                item.AttachmentFileLists = fileResultModel;
            }
            return listReturn;
        }

    }
}