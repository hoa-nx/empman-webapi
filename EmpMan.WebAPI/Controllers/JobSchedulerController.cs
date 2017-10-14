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
using System.Web.Script.Serialization;
using EmpMan.Common.ViewModels;
using EmpMan.Common;
using Mapster;
using EmpMan.Common.Enums;
using EmpMan.Common.ViewModels.Models;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/jobscheduler")]
    [Authorize]
    public class JobSchedulerController : ApiControllerBase
    {
        private IJobSchedulerService _dataService;
        private IFileStorageService _dataFileService;

        public JobSchedulerController(IErrorService errorService, IJobSchedulerService dataService, IFileStorageService dataFileService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._dataFileService = dataFileService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<JobSchedulerViewModel>>(listData);
                var listDataVm = listData.Adapt<List<JobSchedulerViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<JobScheduler>, List<JobSchedulerViewModel>>(query);
                var responseData = query.Adapt<List<JobScheduler>, List<JobSchedulerViewModel>>();
                
                var paginationSet = new PaginationSet<JobSchedulerViewModel>()
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
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
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

                    var model = _dataService.GetAll(searchParam.Keyword );

                    totalRow = model.Count();

                    var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    //var responseData = Mapper.Map<List<JobScheduler>, List<JobSchedulerViewModel>>(query);
                    var responseData = query.Adapt<List<JobScheduler>, List<JobSchedulerViewModel>>();

                    var paginationSet = new PaginationSet<JobSchedulerViewModel>()
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

        [Route("runjob")]
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage RunJob(HttpRequestMessage request, SearchItemViewModel searchParam)
        {
            return CreateHttpResponse(request, () =>
            {
                var response = request.CreateResponse(HttpStatusCode.BadRequest, "Không get được data");
                if (searchParam != null)
                {
                    string ids = string.Join(",", searchParam.NumberItems.Select(n => n.ToString()).ToArray());

                    string sql = " SELECT job.* FROM JobSchedulers job WHERE ID IN (" + ids + ") ORDER BY job.JobType, job.EventDate";

                    var query = _dataService.GetDbContext().Database.SqlQuery<JobSchedulerViewModel>(sql);

                    response = request.CreateResponse(HttpStatusCode.OK, "OK");
                }

                return response;
            });
        }

        [Route("detail/{id}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<JobScheduler, JobSchedulerViewModel>(model);
                var responseData = model.Adapt<JobScheduler, JobSchedulerViewModel>();

 
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        /// <summary>
        /// Tim va get theo table va key
        /// </summary>
        /// <param name="request"></param>
        /// <param name="table"></param>
        /// <param name="tableKey"></param>
        /// <param name="tableKeyId"></param>
        /// <returns></returns>
        [Route("detailbytablekey")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage GetByTableKey(HttpRequestMessage request, string jobType, string table , string tableKey , string tableKeyId)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetByTableKey(jobType,table, tableKey , tableKeyId).ToList().FirstOrDefault();

                //var responseData = Mapper.Map<JobScheduler, JobSchedulerViewModel>(model);
                var responseData = model.Adapt<JobScheduler, JobSchedulerViewModel>();


                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage Create(HttpRequestMessage request, JobSchedulerViewModel dataVm)
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
                    JobScheduler newData = new JobScheduler();
                    //tao moi thi cho la co thay doi 
                    dataVm.IsChanged = true;
                    dataVm.JobStatus = 0;
                    dataVm.SMSNotifyCount = 0;
                    dataVm.EmailNotifyCount = 0;
                    dataVm.SMSNotifyRemider = 0;
                    dataVm.EmailNotifyRemider = 0;
                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;

                    
                    newData.UpdateJobScheduler(dataVm);
                    
                    //update sms content
                    switch (dataVm.JobType.ToInt(0))
                    {
                        case (int)JobTypeEnum.DevInterviewDateNotify:
                            List<JobScheduler> list = new List<JobScheduler>();
                            list.Add(newData);
                            newData.SMSContent = _dataService.DevInterviewDateNotifySmsContent(list);
                            break;

                        case (int)JobTypeEnum.TrialStaffEndTrialDateNotify:
                            //thong bao het han thu viec
                            List<JobSchedulerViewModel> listTrialEndDate = new List<JobSchedulerViewModel>();
                            listTrialEndDate.Add(newData.Adapt<JobScheduler, JobSchedulerViewModel>());
                            newData.SMSContent = _dataService.TrialStaffEndTrialDateNotifySmsContent(listTrialEndDate);
                            break;

                        case (int)JobTypeEnum.TrialStaffToDevContractDateNotify:
                            //thong bao nhan chinh thuc
                            List<JobScheduler> listContractDate = new List<JobScheduler>();
                            listContractDate.Add(newData);
                            newData.SMSContent = _dataService.TrialStaffToDevContractDateNotifySmsContent(listContractDate);
                            break;

                        case (int)JobTypeEnum.DevJobLeavedDateNotify:
                            //thong bao nhan vien sap nghi viec
                            List<JobScheduler> listDevJobLeavedDateNotify = new List<JobScheduler>();
                            listDevJobLeavedDateNotify.Add(newData);
                            newData.SMSContent = _dataService.DevJobLeavedDateNotifySmsContent(listDevJobLeavedDateNotify);
                            break;

                        case (int)JobTypeEnum.ThankDevJobLeavedDateNotify:
                            //cam on dong gop cua NV nghi viec
                            List<JobScheduler> listThankDevJobLeavedDateNotify = new List<JobScheduler>();
                            listThankDevJobLeavedDateNotify.Add(newData);
                            newData.SMSContent = _dataService.DevJobLeavedDateNotifySmsContent(listThankDevJobLeavedDateNotify);
                            break;



                    }
                    

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<JobScheduler, JobSchedulerViewModel>(data);
                    
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage Update(HttpRequestMessage request, JobSchedulerViewModel dataVm)
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

                    //cap nhat trang thai cua du lieu
                    dataVm.IsChanged = this.IsChangedInfo(JobTypeEnum.DevInterviewDateNotify, dataFromDb, dataVm);
                    if (dataVm.IsChanged.Value)
                    {
                        //Neu co thay doi thi can phai thuc thi lai job
                        dataVm.JobStatus = 0;
                    }

                    dataFromDb.UpdateJobScheduler(dataVm);
                    
                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    //update sms content
                    switch (dataFromDb.JobType.ToInt(0))
                    {
                        case (int)JobTypeEnum.DevInterviewDateNotify:
                            //thong bao phong van
                            List<JobScheduler> list = new List<JobScheduler>();
                            list.Add(dataFromDb);
                            dataFromDb.SMSContent = _dataService.DevInterviewDateNotifySmsContent(list);
                            break;

                        case (int)JobTypeEnum.TrialStaffEndTrialDateNotify:
                            //thong bao het han thu viec
                            List<JobSchedulerViewModel> listTrialEndDate = new List<JobSchedulerViewModel>();
                            listTrialEndDate.Add(dataFromDb.Adapt<JobScheduler, JobSchedulerViewModel>());
                            dataFromDb.SMSContent = _dataService.TrialStaffEndTrialDateNotifySmsContent(listTrialEndDate);
                            break;

                        case (int)JobTypeEnum.TrialStaffToDevContractDateNotify:
                            //thong bao nhan chinh thuc
                            List<JobScheduler> listContractDate = new List<JobScheduler>();
                            listContractDate.Add(dataFromDb);
                            dataFromDb.SMSContent = _dataService.TrialStaffToDevContractDateNotifySmsContent(listContractDate);
                            break;

                        case (int)JobTypeEnum.DevJobLeavedDateNotify:
                            //thong bao nhan vien sap nghi viec
                            List<JobScheduler> listDevJobLeavedDateNotify = new List<JobScheduler>();
                            listDevJobLeavedDateNotify.Add(dataFromDb);
                            dataFromDb.SMSContent = _dataService.DevJobLeavedDateNotifySmsContent(listDevJobLeavedDateNotify);
                            break;

                        case (int)JobTypeEnum.ThankDevJobLeavedDateNotify:
                            //cam on dong gop cua NV nghi viec
                            List<JobScheduler> listThankDevJobLeavedDateNotify = new List<JobScheduler>();
                            listThankDevJobLeavedDateNotify.Add(dataFromDb);
                            dataFromDb.SMSContent = _dataService.DevJobLeavedDateNotifySmsContent(listThankDevJobLeavedDateNotify);
                            break;

                    }

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<JobScheduler, JobSchedulerViewModel>(dataFromDb);
                    
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        private bool IsChangedInfo(JobTypeEnum jobType , JobScheduler currentData, JobSchedulerViewModel newData)
        {
            //lay trang thai du lieu hien tai
            bool isChange = currentData.IsChanged.Value;

            switch ( jobType)
            {
                case JobTypeEnum.DevInterviewDateNotify:
                    //so sanh giua tri cu va moi cua du lieu job de biet co thay doi hay khong ( phai gui lai thong bao)
                    if(newData.ScheduleRunJobDate.HasValue && newData.ScheduleRunJobDate.Value > DateTime.Now && (currentData.ScheduleRunJobDate != newData.ScheduleRunJobDate))
                    {
                        isChange = true;
                    }

                    if ( newData.EventDate.HasValue && newData.EventDate.Value > DateTime.Now &&(currentData.EventDate != newData.EventDate))
                    {
                        isChange = true;
                    }

                    if (!string.IsNullOrEmpty(currentData.LocationEvent) && (currentData.LocationEvent != newData.LocationEvent))
                    {
                        isChange = true;
                    }

                    break;

                default:
                    break;
            }

            return isChange;
        }

        /// <summary>
        /// Tim va dang ky job neu chua ton tai.Neu ton tai thi cap nhat
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dataVm"></param>
        /// <returns></returns>
        [Route("findregister")]
        [HttpPost]
        [Permission(Action=FunctionActions.CREATE , Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage RegisterJob(HttpRequestMessage request, JobSchedulerViewModel dataVm)
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
                    
                    var dataFromDb = _dataService.GetByTableKey (dataVm.JobType, dataVm.TableNameRelation , dataVm.TableKey , dataVm.TableKeyID, dataVm.ID).ToList();
                    
                    if (dataFromDb.Count() > 0)
                    {
                        
                        //goi ham so cap nhat 
                        this.Update(request, dataVm);
                    }
                    else
                    {
                        //tao moi neu chua co
                        this.Create(request, dataVm);
                    }

                    response = request.CreateResponse(HttpStatusCode.Created, dataVm); //dataVm : KHONG phai la du lieu sau khi cap nhat
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.JOB_SCHEDULER)]
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

                    var responseData = Mapper.Map<JobScheduler, JobSchedulerViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        /// <summary>
        /// Xoa theo table va key
        /// </summary>
        /// <param name="request"></param>
        /// <param name="table"></param>
        /// <param name="tableKey"></param>
        /// <param name="tableKeyId"></param>
        /// <returns></returns>
        [Route("deletebytablekey")]
        [HttpDelete]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage DeleteByTableKey(HttpRequestMessage request, string jobType, string table, string tableKey, string tableKeyId)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetByTableKey(jobType,table, tableKey, tableKeyId).ToList().FirstOrDefault();

                //var responseData = Mapper.Map<JobScheduler, JobSchedulerViewModel>(model);
                //var responseData = model.Adapt<JobScheduler, JobSchedulerViewModel>();
                if(model!=null && model.ID > 0)
                {
                    this.Delete(request, model.ID);
                }

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.JOB_SCHEDULER)]
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

        /// <summary>
        /// Lay danh sach cac job can phai gui thong bao.
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("getdevinterviewdatenotify")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.JOB_SCHEDULER)]
        public HttpResponseMessage GetDevInterviewDateNotifys(HttpRequestMessage request)
        {
            /*
             * Dieu kien get data:
             * Tu data dang ky tai job , tien hanh get lai record doi tuong tai bang thong tin phong van
             * Lay ra dot phong van tuong ung .
             * Truong hop :
             *  #Toan bo nguoi dang ky da co lich pv 
             *  va chua ket thuc dot pv 
             *  va ngay pv > ngay hien tai 
             *  
             *  thi se gui sms thong bao 1 luot
             */

            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<JobSchedulerViewModel>>(listData);
                var listDataVm = listData.Adapt<List<JobSchedulerViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }


    }
}