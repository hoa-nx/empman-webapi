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
using EmpMan.Common.ViewModels.Models.Revenue;
using System.Globalization;
using System.Collections;
using EmpMan.Common.ViewModels;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using Z.EntityFramework.Plus;
using System.Threading.Tasks;
using EmpMan.Common;
using System.IO;
using System.Web;
using System.Configuration;
using EmpMan.Common.Enums;
using Mapster;
using EmpMan.Common.ViewModels.Models.Common;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/revenue")]
    [Authorize]
    public class RevenueController : ApiControllerBase
    {
        private IRevenueService _dataService;

        public RevenueController(IErrorService errorService, IRevenueService dataService) :
            base(errorService)
        {
            this._dataService = dataService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll().ToList();

                //var listDataVm = Mapper.Map<List<RevenueViewModel>>(listData);
                var listDataVm = listData.Adapt<List<RevenueViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }


        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                //_dataService.ProxyCreationEnabled(false);
                var model = _dataService.GetAll(keyword , null);
                //new string[]{ "ProjectDetail", "EstimateType","Customer","PM","PL"}
                //var model = _dataService.Search(keyword,page, pageSize,"",out totalRow, new string[] { "ProjectDetail", "EstimateType", "Customer", "PM", "PL" });

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //_dataService.DetachedEntity();

                //var responseData = Mapper.Map<List<Revenue>, List<RevenueViewModel>>(query);
                var responseData = query.Adapt<List<Revenue>, List<RevenueViewModel>>();

                //_dataService.ProxyCreationEnabled(true);
                var paginationSet = new PaginationSet<RevenueViewModel>()
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


        [Route("getallautocompletedata")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetAllAutoCompleteData(HttpRequestMessage request, int customerID)
        {
            return CreateHttpResponse(request, () =>
            {
                string sql = " SELECT DISTINCT 1 as ID , OrderNo  Name FROM REVENUES WHERE CustomerID= " + customerID;
                sql += " UNION ALL ";
                sql += " SELECT DISTINCT 2 as ID , ProjectContent Name FROM REVENUES WHERE CustomerID = " + customerID ;

                var kekka = _dataService.GetDbContext().Database.SqlQuery<CodeNameViewModel>(sql).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, kekka);

                return response;
            });
        }



        [Route("getallpagingmasterdata")]
        [HttpPost]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetAllPagingRelationData(HttpRequestMessage request, SearchItemViewModel searchModel)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                int page = searchModel.Page.Value;
                int pageSize = searchModel.PageSize.Value;
                //SearchItemViewModel searchModel = JsonConvert.DeserializeObject<SearchItemViewModel>(bodyData);

                //var model = _dataService.Search(searchModel.Keyword, page, pageSize, "", out totalRow, new string[]{"EstimateType", "Customer","PM", "PL" });

                if ( (searchModel.DateTimeItems == null || searchModel.DateTimeItems.Count() == 0)  
                    &&  (searchModel.NumberItems == null || searchModel.NumberItems.Count() == 0))
                {
                    searchModel.Keyword = "<<$@^*&^%$#@!*~>>??";

                }

                //var model = _dataService.GetAll(searchModel.Keyword, new string[] { "EstimateType", "Customer", "PM"});
                var model = _dataService.GetAll(searchModel.Keyword, null);
                if (searchModel.DateTimeItems!=null && searchModel.DateTimeItems.Count()>0)
                {
                //    //Expression<Func<Revenue, bool>> revenueYearMonth = c => c.ReportYearMonth == DateTime.Now;

                    model = model.Where(p => ( searchModel.DateTimeItems.Contains(  p.ReportYearMonth.Value ) ));

                }

                if(searchModel.NumberItems !=null && searchModel.NumberItems.Count() > 0)
                {
                    //filter theo ma khach hang
                    model = model.Where(p => (searchModel.NumberItems.Contains(p.CustomerID)));
                }

                if(searchModel.BoolItems !=null && searchModel.BoolItems.Count() > 0)
                {
                    bool nextMonthInclude = searchModel.BoolItems[0].Value;

                    //filter theo doanh so con lai thang sau
                    if (nextMonthInclude)
                    {
                        model = model.Where(p => (p.NextMonthMM.Value != 0));
                    }
                    //chi get so lieu onsite dai han
                    if (searchModel.BoolItems.Count > 1)
                    {
                        bool onsiteOnly = searchModel.BoolItems[1].Value;
                        model = model.Where(p => (p.InMonthOnsiteMM.Value != 0));
                    }
                    
                }

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CustomerID).OrderByDescending(x => x.OrderNo).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                
                //var responseData = Mapper.Map<List<Revenue>, List<RevenueViewModel>>(query);
                var responseData = query.Adapt<List<Revenue>, List<RevenueViewModel>>();

                var paginationSet = new PaginationSet<RevenueViewModel>()
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
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetAllGroupByYearMonth(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _dataService.GetAll();

                totalRow = model.Count();
                var groupData = model.GroupBy(p => p.ReportYearMonth,
                                          (key, elements) =>
                                          new
                                          {
                                              id = key,
                                              name = "" + key.Value.Year + "/" + key.Value.Month
                                          }).OrderByDescending(p=>p.id)
                                          .ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, groupData);
                return response;
            });
        }

        [Route("gettotalzenmonth")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetTotalZenMonth(HttpRequestMessage request , DateTime startDate)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var model = _dataService.GetAll();

                totalRow = model.Count();
                //moi khach hang se tinh theo No nhan dat hang rieng
                var summaryData = model.Where(x=> x.ReportYearMonth < startDate).GroupBy(x => new { x.OrderNo , x.CustomerID, x.CustomerUnitPriceID })
                                        .Select(c => new
                                        {
                                            OrderNo = c.Key.OrderNo,
                                            CustomerID = c.Key.CustomerID,
                                            CustomerUnitPriceID = c.Key.CustomerUnitPriceID,
                                            ZenTotalMM = c.Sum(p => p.InMonthSumMM),
                                            ZenTotalUSD = c.Sum(p => p.InMonthToUsd)

                                        }).ToList();


                var response = request.CreateResponse(HttpStatusCode.OK, summaryData);
                return response;
            });
        }

        [Route("getlisttreeview")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetAllTreeView(HttpRequestMessage request , string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var modelRevenue = _dataService.GetAll(keyword, null);

                IEnumerable<RevenueViewModel> modelVm = Mapper.Map<IEnumerable<Revenue>, IEnumerable<RevenueViewModel>>(modelRevenue);
                //var parents = from p in modelVm
                //              group p.ReportYearMonth.Value.Year by p.ReportYearMonth.Value.Year into g
                //              select g;

                var parents = modelVm.GroupBy(p => p.ReportYearMonth.Value.Year,
                                          (key, elements) =>
                                          new 
                                          {
                                              YearValue = key,
                                              Items = elements.ToList()
                                          })
                                          .ToList();

                List<LeoTreeViewModel> treeParent = new List<LeoTreeViewModel>();
                List<LeoTreeViewModel> treeChild;
                //loop over group 
                foreach (var parent in parents)
                {
                    LeoTreeViewModel treeModel = new LeoTreeViewModel();

                    treeModel.text = "Năm " + parent.YearValue;
                    treeModel.value = parent.YearValue.ToString();

                    treeChild = new List<LeoTreeViewModel>();

                    foreach (var child in parent.Items)
                    {
                        LeoTreeViewModel tree = new LeoTreeViewModel();
                        
                        tree.text = "Tháng " + child.ReportYearMonth.Value.Month;
                        tree.value = child.ReportYearMonth.Value.ToString();
                        treeChild.Add(tree);
                        //tree.Value = child
                    }
                    treeModel.children = treeChild;

                    treeParent.Add(treeModel);

                }

                response = request.CreateResponse(HttpStatusCode.OK, treeParent);

                return response;
            });
        }

        [Route("detailbymultijoken")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_LIST)]
        public HttpResponseMessage GetByMultiJoken(HttpRequestMessage request, DateTime ReportYearMonth  , int CustomerID , int CustomerUnitPriceID, int EstimateTypeID, string OrderNo)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetAll(null, new string[] { "Customer" });

                if (ReportYearMonth != null )
                {
                    model = model.Where(p => (p.ReportYearMonth.Value == ReportYearMonth));
                }
                if (OrderNo != null || !string.IsNullOrEmpty(OrderNo))
                {
                    model = model.Where(p => (p.OrderNo.ToLower() == OrderNo.ToLower()));
                }
                
                model = model.Where(p => (p.CustomerID.Value == CustomerID 
                                        && p.CustomerUnitPriceID.Value == CustomerUnitPriceID 
                                        && p.EstimateTypeMasterDetailID.Value == EstimateTypeID
                                        )
                                    );


                var responseData = Mapper.Map<List<Revenue>, List< RevenueViewModel>>(model.ToList());

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }



        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_EDIT)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id, new string[] {"Customer"});

                //var responseData = Mapper.Map<Revenue, RevenueViewModel>(model);
                var responseData = model.Adapt<Revenue, RevenueViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("new/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.REVENUE_EDIT)]
        public HttpResponseMessage GetRecordInit(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var newRecord = new RevenueViewModel();
                DateTime reportYM ;
                var ok = DateTime.TryParse(DateTime.Now.ToString("yyyy/MM/01"), out reportYM);

                newRecord.ReportYearMonth = reportYM;
                newRecord.ReportTitle = "Báo cáo doanh số";
                

                var response = request.CreateResponse(HttpStatusCode.OK, newRecord);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.REVENUE_EDIT)]
        public HttpResponseMessage Create(HttpRequestMessage request, RevenueViewModel dataVm)
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
                    Revenue newData = new Revenue();

                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    

                    newData.UpdateRevenue(dataVm);

                    var data = _dataService.Add(newData);
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.REVENUE_EDIT)]
        public HttpResponseMessage Update(HttpRequestMessage request, RevenueViewModel dataVm)
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
                    var dataFromDb = _dataService.GetById(dataVm.ID, null);

                    dataFromDb.UpdateRevenue(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<Revenue, RevenueViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("approveddata")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.REVENUE_EDIT)]
        public HttpResponseMessage UpdateApprovedInformation(HttpRequestMessage request, SearchItemViewModel approvedModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                
                if(approvedModel !=null && approvedModel.NumberItems.Count > 0)
                {
                    //xu ly cap nhat approved cac data 
                    //approvedModel.NumberItems.ForEach(x => {
                    //    string sql = "UPDATE REVENUE SET [ApprovedBy]=" + User.Identity.Name + " , [ApprovedDate] = " + DateTime.Now + " , [ApprovedStatus]=1 WHERE ID =" + x ;
                    //});


                    using (var context = _dataService.GetDbContext())
                    {
                        if (approvedModel.IsApproved.Value)
                        {
                            //approved
                            context.Revenues.Where(item => approvedModel.NumberItems.Contains(item.ID))
                                            .Update(i => new Revenue { ApprovedBy = User.Identity.Name, ApprovedDate = DateTime.Now, ApprovedStatus =(int) ApprovedStatusEnum.Approved });
                        }
                        else
                        {
                            //cancel approved
                            context.Revenues.Where(item => approvedModel.NumberItems.Contains(item.ID))
                                            .Update(i => new Revenue { ApprovedBy = null, ApprovedDate = null, ApprovedStatus = null });
                        }

                        
                    }


                }

                response = request.CreateResponse(HttpStatusCode.OK);
               
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.REVENUE_EDIT)]
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

                    var responseData = Mapper.Map<Revenue, RevenueViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.REVENUE_EDIT)]
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

        [HttpGet]
        [Route("tableExportXls")]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.REVENUE_EDIT)]
        public async Task<HttpResponseMessage> TableExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Doanh so " + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
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