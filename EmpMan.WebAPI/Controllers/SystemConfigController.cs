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
using EmpMan.Web.Models.Schedule;
using EmpMan.Data;
using EmpMan.Common;
using EmpMan.Common.ViewModels;
using System.Web.Script.Serialization;
using Mapster;
using System.Text;
using System.Configuration;
using EmpMan.Web.Infrastructure.SmsHelper;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/setting")]
    [Authorize]
    public class SystemConfigController : ApiControllerBase
    {
        private ISystemConfigService _dataService;

        public SystemConfigController(IErrorService errorService, ISystemConfigService dataService) :
            base(errorService)
        {
            this._dataService = dataService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<SystemConfigViewModel>>(listData);
                var listDataVm = listData.Adapt<List<SystemConfigViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page - 1 * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<SystemConfig>, List<SystemConfigViewModel>>(query);
                var responseData = query.Adapt<List<SystemConfig>, List<SystemConfigViewModel>>();
                var paginationSet = new PaginationSet<SystemConfigViewModel>()
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
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<SystemConfig, SystemConfigViewModel>(model);
                var responseData = model.Adapt<SystemConfig, SystemConfigViewModel>();
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("detailsystemconfig/{id}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage GetByAccountAndId(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetByAccount(id);

                //var responseData = Mapper.Map<SystemConfig, SystemConfigViewModel>(model);
                var responseData = model.Adapt<SystemConfig, SystemConfigViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("detailsearchfilterbyuser")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage GetSearchFilterByAccount(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var response = request.CreateResponse(HttpStatusCode.OK , new EmpFilterViewModel());
                //tao moi neu chua ton tai 
                CreateSystemConfigByAccount(_dataService.GetDbContext());

                var model = _dataService.GetByAccount(User.Identity.Name);

                if (model.EmpFilterDataValue != null)
                {
                    var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(model.EmpFilterDataValue);

                    //ma hoa du lieu
                    string data1 = StringCipher.Decrypt(empFilterViewModel.systemValue.SidT, CommonConstants.SecKeyString);
                    empFilterViewModel.systemValue.SidT = data1;
                    string data2 = StringCipher.Decrypt(empFilterViewModel.systemValue.TokT, CommonConstants.SecKeyString);
                    empFilterViewModel.systemValue.TokT = data2;

                    response = request.CreateResponse(HttpStatusCode.OK, empFilterViewModel);
                }
                
                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage Create(HttpRequestMessage request, SystemConfigViewModel dataVm)
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
                    SystemConfig newData = new SystemConfig();

                    /** cập nhật các thông tin chung **/
                    newData.CreatedDate = DateTime.Now;
                    newData.CreatedBy = User.Identity.Name;

                    newData.UpdatedDate = DateTime.Now;
                    newData.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    newData.AccountData = User.Identity.GetApplicationUser().Email;


                    newData.UpdateSystemConfig(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage Update(HttpRequestMessage request, SystemConfigViewModel dataVm)
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
                    //tao moi neu chua ton tai 
                    CreateSystemConfigByAccount(_dataService.GetDbContext());

                    var dataFromDb = _dataService.GetByAccount(User.Identity.Name); 

                    dataFromDb.UpdateSystemConfig(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;
                    //ma hoa du lieu
                    //string data1 = StringCipher.Decrypt(dataFromDb.Data1 , CommonConstants.SecKeyString);
                    //dataFromDb.Data1 = data1;
                    //string data2 = StringCipher.Decrypt(dataFromDb.Data2, CommonConstants.SecKeyString);
                    //dataFromDb.Data2 = data2;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<SystemConfig, SystemConfigViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("updateempfilter")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.SYSTEM_CONFIG)]
        public HttpResponseMessage UpdateEmpFilterDataValue(HttpRequestMessage request, EmpFilterViewModel dataVm)
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
                    //test sms 
                    //SmsHelper.SendeSms("", "0967808590", "test");
                    //String[] phones = new String[] { "0000" };
                    //String rmsg = SmsHelper.sendSpeedSMS(phones, "***", 6, "");

                    //tao moi neu chua ton tai 
                    CreateSystemConfigByAccount(_dataService.GetDbContext());
                    //get date theo account logon
                    var dataFromDb = _dataService.GetByAccount(User.Identity.Name); 
                    
                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;
                    //chuyen doi thanh JSON string 
                    if (dataVm != null) {

                        if (dataVm.systemValue != null)
                        {
                            //thong tin de gui sms
                            if (dataVm.systemValue.SidT != null && dataVm.systemValue.TokT != null)
                            {
                                var jsonSystemValue = new JavaScriptSerializer().Serialize(dataVm.systemValue);
                                var objSystemValue = new JavaScriptSerializer().Deserialize<SystemValueViewModel>(jsonSystemValue);
                                //ma hoa du lieu
                                string data1 = StringCipher.Encrypt(objSystemValue.SidT, CommonConstants.SecKeyString);
                                objSystemValue.SidT = data1;
                                string data2 = StringCipher.Encrypt(objSystemValue.TokT, CommonConstants.SecKeyString);
                                objSystemValue.TokT = data2;

                                dataFromDb.SystemValue = new JavaScriptSerializer().Serialize(objSystemValue);
                                dataVm.systemValue = objSystemValue;
                            }
                        }

                        var json = new JavaScriptSerializer().Serialize(dataVm);

                        //var obj = new JavaScriptSerializer().Deserialize<SystemConfigViewModel>(json);

                        dataFromDb.EmpFilterDataValue = json;

                        if (dataVm.sort != null && dataVm.sort.Length > 0)
                        {
                            dataFromDb.EmpOrderBy = dataVm.sort.ToString();
                            //string[] order = Array.ConvertAll(s.Split(','), string.Parse);
                        }
                        
                    }
                    
                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<SystemConfig, SystemConfigViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }


        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.SYSTEM_CONFIG)]
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

                    var responseData = Mapper.Map<SystemConfig, SystemConfigViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        private void CreateSystemConfigByAccount(EmpManDbContext context)
        {
            bool isSonzai = true;
            var dataFromDb = _dataService.GetByAccount(User.Identity.Name);
            if (dataFromDb == null) isSonzai = false;

            if (!isSonzai)
            {

                //reset identity column 
                //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT (SystemConfigs, RESEED, 0)");
                //get max ID 
                int id = context.SystemConfigs.Max(p => p.ID) + 1 ;
                

                List<SystemConfig> listData = new List<SystemConfig>()
                {
                    new SystemConfig() {ID =id , Code = User.Identity.Name ,Name = "Cấu hình hệ thống",ShortName="Cấu hình hệ thống",Status = true,CreatedDate = DateTime.Now,CreatedBy = CommonConstants.AdminUser,AccountData = "xuanhoa97@gmail.com"}

                };
                context.SystemConfigs.AddRange(listData);

                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    //Write to file
                    System.IO.File.AppendAllLines(@"c:\temp\errors.txt", outputLines);
                    throw;

                    // Showing it on screen
                    throw new Exception(string.Join(",", outputLines.ToArray()));

                }
            }
        }


    }
}