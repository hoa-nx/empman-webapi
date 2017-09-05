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
using EmpMan.Web.Models.Emp;
using System.Threading.Tasks;
using EmpMan.Common.Exceptions;
using EmpMan.Common.ViewModels;
using System.Text;
using System.Web;
using System.IO;
using OfficeOpenXml;
using EmpMan.Common;
using EmpMan.Common.Enums;
using System.Web.Http.ModelBinding;
using EmpMan.Web.Infrastructure.CustomAttribute;
using Mapster;
using System.Web.Script.Serialization;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/emp")]
    [Authorize]
    public class EmpController : ApiControllerBase
    {
        private IEmpService _dataService;
        private ISystemConfigService _systemConfigService;

        public EmpController(IErrorService errorService, IEmpService dataService , ISystemConfigService systemConfigService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._systemConfigService = systemConfigService;
        }

        [Route("getall")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                /** sử dụng cache */
                //var listDataVm = MemoryCacher.GetValue("emps");
                //if (listDataVm == null)
                //{
                //    var listData = _dataService.GetAll();

                //    listDataVm = Mapper.Map<List<EmpViewModel>>(listData);

                //    MemoryCacher.Add("emps", listDataVm, DateTimeOffset.UtcNow.AddHours(1));
                //}

                //HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<EmpViewModel>>(listData);
                var listDataVm = listData.Adapt<List<EmpViewModel>>();
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }

        [Route("getallfromview")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAllFromViewEmp(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                /** sử dụng cache */
                //var listDataVm = MemoryCacher.GetValue("emps");
                //if (listDataVm == null)
                //{
                //    var listData = _dataService.GetAll();

                //    listDataVm = Mapper.Map<List<EmpViewModel>>(listData);

                //    MemoryCacher.Add("emps", listDataVm, DateTimeOffset.UtcNow.AddHours(1));
                //}

                //HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                string filterSqlString = _systemConfigService.getEmpSqlFilter(User.Identity.Name, true);
                string orderBySqlString = _systemConfigService.getEmpSqlOrderBy(User.Identity.Name, true, "", false);

                string sql = " SELECT * FROM ViewEmp " + filterSqlString + orderBySqlString;

                var listData = _dataService.GetDbContext().Database.SqlQuery<EmpViewModel>(sql);

                //var listDataVm = Mapper.Map<List<EmpViewModel>>(listData);
                var listDataVm = listData.Adapt<List<EmpViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword , new string[] { "Dept", "Team", "Position" } );

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<Emp>, List<EmpViewModel>>(query);
                var responseData = query.Adapt<List<Emp>, List<EmpViewModel>>();

                var paginationSet = new PaginationSet<EmpViewModel>()
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
        [Route("getallpagingfromview")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAllFromViewEmp(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                int? topRecordSelect = null;
                 
                //string addWhereSql = "";

                //if (!string.IsNullOrEmpty(keyword))
                //    addWhereSql = " AND (COALESCE(FullName,'') + COALESCE(Name,'') + COALESCE(AccountName,' ') + COALESCE(TaxCode,' ') + COALESCE(Note,' ') + ISNULL(DeptName,'') + COALESCE(TeamName,'') + COALESCE(PositionName,'') + COALESCE (EmpTypeName,'')) like N'%" + keyword + "%'";

                //string filterSqlString = _systemConfigService.getEmpSqlFilter(User.Identity.Name, true, addWhereSql);
                //string orderBySqlString = _systemConfigService.getEmpSqlOrderBy(User.Identity.Name, true, "", false);
                ////get data 
                //var model = _systemConfigService.GetByAccount(User.Identity.Name);
                //int month = 4;
                //int year = 2017;
                //string processingDateFrom = year + "/01/01";
                //string processingDateTo = year + "/12/31";

                //if (model != null && model.ExpMonth.HasValue)
                //{
                //    month = model.ExpMonth.Value;
                //}
                //if (model != null && model.ProcessingYear.HasValue)
                //{
                //    year = model.ProcessingYear.Value.Year;
                //}

                //string sql = @" SELECT *
                //                --dang lam viec cho toi hien tai 	  
                //                , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND (JobLeaveDate IS NULL OR JobLeaveDate > CONVERT(DATE,GETDATE())) THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedCount
                //                --LTV chinh thuc da nghi viec 
                //                , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND (JobLeaveDate IS NOT NULL AND JobLeaveDate < CONVERT(DATE,GETDATE()) ) THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedJobLeavedCount
                //                --LTV thu viec nhung khong vao chinh thuc
                //                , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NULL AND JobLeaveDate IS NOT NULL AND StartTrialDate IS NOT NULL THEN 1 ELSE 0 END) OVER (PARTITION BY null) TrialJobLeavedCount
                //             , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NULL AND JobLeaveDate IS NULL AND (StartWorkingDate IS NOT NULL OR StartTrialDate IS NOT NULL) THEN 1 ELSE 0 END) OVER (PARTITION BY null) TrialCount
                //             , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND JobLeaveDate IS NULL AND ContractDate >= DATEADD(month,-" + month + @", CONVERT(DATE,GETDATE()))  THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedLTNMonthCount
                //             , SUM( case when EmpTypeMasterDetailID IN(3,4) THEN 1 ELSE 0 END) OVER (PARTITION BY null) OtherCount
                //             , SUM( case when EmpTypeMasterDetailID IN(2) THEN 1 ELSE 0 END) OVER (PARTITION BY null) TransCount
                //                --nhan vien nghi viec trong nam tai chinh da setting 
                //                , SUM( case when EmpTypeMasterDetailID IN(1) 
                //                            AND ContractDate IS NOT NULL 
                //                            AND (JobLeaveDate  BETWEEN  '" + processingDateFrom + @"' AND CONVERT(DATE,GETDATE())) THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedJobLeavedInProcessingYearCount

                //                --nhan vien thu viec da khong vao chinh thuc trong nam ( bao gom ca nhan vien start thu viec trong nam truoc )
                //                , SUM( case when EmpTypeMasterDetailID IN(1) 
                //                            AND ContractDate IS NULL 
                //                            AND ( StartTrialDate IS NOT NULL  OR StartWorkingDate IS NOT NULL)
                //                            AND (JobLeaveDate  BETWEEN  '" + processingDateFrom + @"' AND '" + processingDateTo + @"') THEN 1 ELSE 0 END) OVER (PARTITION BY null) TrialJobLeavedInProcessingYearCount

                //             , 0 OnsiteCount 
                //                , " + month + @"    ExpMonth
                //                , " + year + @"    ProcessingYear
                //                FROM ViewEmp " + filterSqlString + orderBySqlString;

                //get sql
                if(page==1 && pageSize ==1)
                {
                    //chi can lay 1 record la du 
                    topRecordSelect = 1;
                }
                string sql = GetAllFromViewEmpSql(keyword, topRecordSelect);

                var listData = _dataService.GetDbContext().Database.SqlQuery<EmpViewModel>(sql);

                totalRow = listData.Count();

                var responseData = listData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<EmpViewModel>>(listData);

                var paginationSet = new PaginationSet<EmpViewModel>()
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

        [Route("getlistbycommongroup")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAllFromViewEmpByCommonGroup(HttpRequestMessage request, string group)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                string addWhereSql = "";
                //phan sql dieu kien loc du lieu chung 
                string filterSqlString = _systemConfigService.getEmpSqlFilter(User.Identity.Name, true, addWhereSql);
                //phan sql dung de sort du lieu chung
                string orderBySqlString = _systemConfigService.getEmpSqlOrderBy(User.Identity.Name, true, "", false);
                //lay cac thong tin setting cua he thong
                var model = _systemConfigService.GetByAccount(User.Identity.Name);
                //so thang de loai tru nhan vien khong tinh vao hieu suat hoat dong
                int month = 4;
                //nam xu ly
                int year = 2017;

                DateTime processingDateFrom = new DateTime(year, 1, 1);
                DateTime processingDateTo = new DateTime(year, 12, 31);

                string processingDateStrFrom = year + "/01/01";
                string processingDateStrTo = year + "/12/31";

                if (model != null && model.ExpMonth.HasValue)
                {
                    month = model.ExpMonth.Value;
                }
                if (model != null && model.ProcessingYear.HasValue)
                {
                    year = model.ProcessingYear.Value.Year;
                }
                //cau sql chinh de get data
                string sql = @" SELECT 
                                    ID
                                    ,FULLNAME
                                    ,GENDER
                                    ,PHONENUMBER1
                                    ,WORKINGEMAIL
                                    ,ACCOUNTNAME
                                    ,AVATAR
                                    ,COMPANYNAME
                                    ,DEPTNAME
                                    ,TEAMNAME
                                    ,POSITIONNAME
                                    ,CONTRACTDATE
                                    ,JOBLEAVEDATE
                                    ,AGEFULL
                                    ,KeikenFromStartWorkingMonths
                                    ,KeikenFromContractMonths
                                    ,EmpTypeName
                                    ,JapaneseLevelName
                                    ,BusinessAllowanceLevelName
                                    ,BseAllowanceLevelName
                                    ,ContractTypeName
                                    ,CollectName
                                    ,Case When (KeikenFromContractMonths/12) Between 0 And 0.9999 Then N'Nhỏ hơn 1 năm'
                                          When (KeikenFromContractMonths/12) Between 1 And 1.9999 Then N'1 ～ 2 năm' 
                                          When (KeikenFromContractMonths/12) Between 2 And 2.9999 Then N'2 ～ 3 năm' 
                                          When (KeikenFromContractMonths/12) Between 3 And 3.9999 Then N'3 ～ 4 năm' 
                                          When (KeikenFromContractMonths/12) Between 4 And 4.9999 Then N'4 ～ 5 năm' 
                                          When (KeikenFromContractMonths/12) Between 5 And 7.9999 Then N'5 ～ 8 năm'
                                          When (KeikenFromContractMonths/12) > 7.9999 Then N'Trên 8 năm'
                                    End   KeikenFromContractYearBunrui
                                    ,COUNT(*) OVER ()   TotalRecords
                                    , " + month + @"    ExpMonth
                                    , " + year + @"    ProcessingYear
                                FROM ViewEmp " + filterSqlString + orderBySqlString;

                var listData = _dataService.GetDbContext().Database.SqlQuery<EmpViewModel>(sql);

                totalRow = listData.Count();

                var grpData = listData.Where(p => p.FullName == "#NULL#").GroupBy(u => u.CompanyName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                switch (group.ToLower())
                {
                    case CommonConstants.EXP_GROUP_DEPT:
                        grpData = listData.GroupBy(u => u.DeptName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                    case CommonConstants.EXP_GROUP_TEAM:
                        grpData = listData.GroupBy(u => u.TeamName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;

                    case CommonConstants.EXP_GROUP_POSITION:
                        grpData = listData.GroupBy(u => u.PositionName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;

                    case CommonConstants.EXP_GROUP_GENDER:
                        grpData = listData.GroupBy(u => u.Gender)
                                           .Select(grp => new
                                           {
                                               title = (grp.Key == true) ? "Nam" : "Nữ",
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                    case CommonConstants.EXP_GROUP_JAPANESE_LEVEL:
                        grpData = listData.GroupBy(u => u.JapaneseLevelName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                    case CommonConstants.EXP_GROUP_BUSSINESS_ALLOWANCE:
                        grpData = listData.GroupBy(u => u.BusinessAllowanceName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                    case CommonConstants.EXP_GROUP_BSE:
                        grpData = listData.GroupBy(u => u.BseAllowanceLevelName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;

                    case CommonConstants.EXP_GROUP_EMP_TYPE:
                        grpData = listData.GroupBy(u => u.EmpTypeName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                    //theo ten truong DH
                    case CommonConstants.EXP_GROUP_COLLECT_NAME:
                        grpData = listData.GroupBy(u => u.CollectName)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Chưa setting" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;

                    //tham nien theo ngay hop dong
                    case CommonConstants.EXP_GROUP_KEIKEN:
                        grpData = listData.GroupBy(u => u.KeikenFromContractYearBunrui)
                                           .Select(grp => new
                                           {
                                               title = string.IsNullOrEmpty(grp.Key) ? "Không xác định" : grp.Key,
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listData.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;

                    //Lay danh sach nhan vien hien tai dang lam viec
                    case CommonConstants.EXP_GROUP_CONTRACTED_COUNT:
                        var listDataContracted = getEmpListFromTopMenu(group);
                        grpData = listDataContracted.GroupBy(u => u.ContractedCount)
                                           .Select(grp => new
                                           {
                                               title = "LTV chính thức đang làm việc",
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listDataContracted.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;

                    //Lay danh sach nhan vien LTV nghỉ việc trong nam
                    case CommonConstants.EXP_GROUP_CONTRACTED_JOB_LEAVED_IN_PROCESSING_YEAR_COUNT:
                        var listDataContractedJobLeavedInYear = getEmpListFromTopMenu(group);
                        grpData = listDataContractedJobLeavedInYear.GroupBy(u => u.ContractedCount)
                                           .Select(grp => new
                                           {
                                               title = "LTV chính thức nghỉ việc trong năm",
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listDataContractedJobLeavedInYear.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                    //Lay danh sach nhan vien LTV thử việc không vào chính thức trong năm
                    case CommonConstants.EXP_GROUP_TRIAL_JOB_LEAVED_IN_PROCESSING_YEAR_COUNT:
                        var listDataTrialJobLeavedInYear = getEmpListFromTopMenu(group);
                        grpData = listDataTrialJobLeavedInYear.GroupBy(u => u.ContractedCount)
                                           .Select(grp => new
                                           {
                                               title = "LTV thử việc không nhận chính thức trong năm",
                                               count = grp.Count(),
                                               percent = listData.Count() == 0 ? (float)0 : (float)grp.Count() / (float)listDataTrialJobLeavedInYear.Count(),
                                               items = grp.ToList()
                                           }).ToList();
                        break;
                        //nhan vien nghi viec theo tung nam

                        //nhan vien nghi viec theo tung nam tung thang

                        //nhan vien thu viec theo tung nam 

                        //nha vien thu viec theo tung nam tung thang

                        //nhan vien thu viec nhan chinh thuc theo tung nam

                        //nhan vien thu viec khong nhan chinh thuc ( 2 ly do : do khong nhan hay la ung vien tu xin nghi)

                }

                var response = request.CreateResponse(HttpStatusCode.OK, grpData);
                return response;
            });
        }

        private IEnumerable<EmpViewModel> getEmpListFromTopMenu(string group)
        {
            string filterSql = "";
            switch (group.ToLower())
            {
                case CommonConstants.EXP_GROUP_CONTRACTED_COUNT:
                    filterSql += " AND EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND (JobLeaveDate IS NULL OR JobLeaveDate > CONVERT(DATE,GETDATE())) ";
                    break;

                case CommonConstants.EXP_GROUP_TRIAL_COUNT:
                    filterSql += " AND EmpTypeMasterDetailID IN(1) AND ContractDate IS NULL AND JobLeaveDate IS NULL AND (StartWorkingDate IS NOT NULL OR StartTrialDate IS NOT NULL) ";
                    break;

                case CommonConstants.EXP_GROUP_TRIAL_JOB_LEAVED_IN_PROCESSING_YEAR_COUNT:
                    filterSql += " AND EmpTypeMasterDetailID IN(1) AND ContractDate IS NULL AND(StartTrialDate IS NOT NULL  OR StartWorkingDate IS NOT NULL) AND(JobLeaveDate  BETWEEN  '@processingDateFrom@' AND '@processingDateTo@') ";
                    break;

                case CommonConstants.EXP_GROUP_CONTRACTED_JOB_LEAVED_IN_PROCESSING_YEAR_COUNT:
                    filterSql += " AND EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND(JobLeaveDate  BETWEEN  '@processingDateFrom@' AND CONVERT(DATE, GETDATE())) ";
                    break;

            }
            string sql = GetAllFromViewEmpSql(null,null, filterSql);

            var listData = _dataService.GetDbContext().Database.SqlQuery<EmpViewModel>(sql);

            return listData;
        }

        [Route("getexpandablebyteam")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_EXPANDABLE)]
        public HttpResponseMessage GetExpandableEmpListByTeam(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine(" SELECT ");
                sql.AppendLine("    EMP.ID ");
                sql.AppendLine(" ,	EMP.FULLNAME ");
                sql.AppendLine(" ,	EMP.NAME ");
                sql.AppendLine(" ,	EMP.PHONENUMBER1 TELEPHONE ");
                sql.AppendLine(" ,  EMP.WORKINGEMAIL ");
                sql.AppendLine(" ,	EMP.AVATAR ");
                sql.AppendLine(" ,	EMP.CURRENTCOMPANYID COMPANYID ");
                sql.AppendLine(" ,  COM.NAME COMPANYNAME ");
                sql.AppendLine(" ,  EMP.CURRENTDEPTID DEPTID ");
                sql.AppendLine(" ,  DEPT.NAME DEPTNAME ");
                sql.AppendLine(" ,  EMP.CURRENTTEAMID TEAMID ");
                sql.AppendLine(" ,  TEAM.NAME TEAMNAME ");
                sql.AppendLine(" ,  EMP.CURRENTPOSITIONID POSITIONID ");
                sql.AppendLine(" ,  0 KEIKENMONTH ");
                sql.AppendLine(" ,	NULL KEIKENYEAR ");
                sql.AppendLine(" ,	'' JAPANESELEVEL ");

                sql.AppendLine(" FROM EMPS EMP ");
                sql.AppendLine(" LEFT OUTER JOIN COMPANYS COM ON EMP.CURRENTCOMPANYID = COM.ID ");
                sql.AppendLine(" LEFT OUTER JOIN DEPTS DEPT ON EMP.CURRENTDEPTID = DEPT.ID ");
                sql.AppendLine(" LEFT OUTER JOIN TEAMS TEAM ON EMP.CURRENTTEAMID = TEAM.ID ");
                sql.AppendLine(" LEFT OUTER JOIN POSITIONS POS ON EMP.CURRENTPOSITIONID = POS.ID ");

                var kekka = _dataService.GetDbContext().Database.SqlQuery<EmpExpandableViewModel>(sql.ToString()).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, kekka);

                return response;
            });
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                var responseData = Mapper.Map<Emp, EmpViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getallautocompletedata")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAllAutoCompleteData(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                string sql = @" SELECT DISTINCT 1 as ID , TrialResult  Name FROM EMPS WHERE  TrialResult  IS NOT NULL
                                UNION ALL
                                SELECT DISTINCT 2 as ID , JobLeaveReason Name FROM EMPS WHERE JobLeaveReason IS NOT NULL";

                var kekka = _dataService.GetDbContext().Database.SqlQuery<CodeNameViewModel>(sql).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, kekka);

                return response;
            });
        }
        [Route("getallcodenamemodel")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage GetAllCodeNameModel(HttpRequestMessage request , int? posFrom , int?posTo , [ModelBinder(typeof(CommaDelimitedCollectionModelBinder))]IEnumerable<string> posGrp)
        {
            return CreateHttpResponse(request, () =>
            {
                string filterSqlString = _systemConfigService.getEmpSqlFilter(User.Identity.Name, false,"");
                string orderBySqlString = _systemConfigService.getEmpSqlOrderBy(User.Identity.Name, true, "", false);

                string sql = @" SELECT EMP.ID , EMP.FULLNAME NAME FROM EMPS EMP LEFT OUTER JOIN POSITIONS POS ON EMP.CurrentPositionID = POS.ID WHERE 1=1 ";
                if(posFrom.HasValue)
                {
                    sql += " AND EMP.CurrentPositionID >=" + posFrom;
                }
                if (posTo.HasValue)
                {
                    sql += " AND EMP.CurrentPositionID <=" + posTo;
                }

                if (posGrp!=null && posGrp.Count()>0)
                {
                    sql += " AND POS.PositionGroupMasterDetailID IN(" + string.Join(",", posGrp.ToArray()) + ")";
                }
                sql += filterSqlString;

                sql += orderBySqlString;

                //sql += " ORDER BY NAME ";

                var kekka = _dataService.GetDbContext().Database.SqlQuery<CodeNameViewModel>(sql).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, kekka);

                return response;
            });
        }


        [Route("add")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage Create(HttpRequestMessage request, EmpViewModel dataVm)
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
                    Emp newData = new Emp();

                    newData.No = 0; //Trường hợp sử dụng cho card item nên phải chuyển thành 0

                    /** trường hợp tạo mới thì cho Id bằng max(No tự sinh) +1 **/
                    if (dataVm.ID<=0)
                    {
                        dataVm.ID = _dataService.GetMaxID() + 1 ;
                    }
                    /** cập nhật các thông tin chung **/
                    dataVm.CreatedDate = DateTime.Now;
                    dataVm.CreatedBy = User.Identity.Name;

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    //Update lại Flag quản lý hiển thị avatar
                    if(string.IsNullOrEmpty(dataVm.Avatar))
                    {
                        dataVm.ShowAvatar = false;
                    }
                    else
                    {
                        dataVm.ShowAvatar = true;
                    }
                    //cập nhật trạng thái của nghỉ việc nếu đã có trị ngày nghỉ việc.

                    if(dataVm.JobLeaveDate.HasValue)
                    {
                        dataVm.IsJobLeave = true;
                    }
                    else
                    {
                        dataVm.IsJobLeave = false;
                    }

                    if (!string.IsNullOrEmpty(dataVm.AccountName))
                    {
                        dataVm.WorkingEmail = dataVm.AccountName + "@fujinet.net"; //xu ly tam thoi
                    }
                    //cap nhat cac master kbn
                    //UpdateMasterId(ref dataVm);

                    //update model
                    newData.UpdateEmp(dataVm);
                                        
                    var data = _dataService.Add(newData);

                    if (!string.IsNullOrEmpty(dataVm.AccountName)) { 
                        //tao user mac dinh 
                        bool kekka = CreateUserDefault(newData);
                    }
                    _dataService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.Created, data);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.EMP_CARD)]
        public HttpResponseMessage Update(HttpRequestMessage request, EmpViewModel dataVm)
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

                    //cập nhật trạng thái của nghỉ việc nếu đã có trị ngày nghỉ việc.
                    if (dataVm.JobLeaveDate.HasValue)
                    {
                        dataVm.IsJobLeave = true;
                    }
                    else
                    {
                        dataVm.IsJobLeave = false;
                    }
                    //cập nhật email công việc
                    if (!string.IsNullOrEmpty(dataVm.AccountName))
                    {
                        dataVm.WorkingEmail = dataVm.AccountName + "@fujinet.net"; //xu ly tam thoi
                    }

                    dataVm.UpdatedDate = DateTime.Now;
                    dataVm.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataVm.AccountData = User.Identity.GetApplicationUser().Email;
                    //Update lại Flag quản lý hiển thị avatar
                    if (string.IsNullOrEmpty(dataVm.Avatar))
                    {
                        dataVm.ShowAvatar = false;
                    }
                    else
                    {
                        dataVm.ShowAvatar = true;
                    }
                    //cap nhat cac master kbn
                    //UpdateMasterId(ref dataVm);

                    //update model
                    dataFromDb.UpdateEmp(dataVm);
                    //update to DB
                    _dataService.Update(dataFromDb);

                    if (!string.IsNullOrEmpty(dataVm.AccountName))
                    {
                        //tao user mac dinh 
                        bool kekka = CreateUserDefault(dataFromDb);
                    }

                    //commit data
                    _dataService.SaveChanges();

                    //return value
                    var responseData = Mapper.Map<Emp, EmpViewModel>(dataFromDb);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.EMP_CARD)]
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

                    var oldDataFromDb = _dataService.Delete(id);
                    _dataService.SaveChanges();

                    var responseData = Mapper.Map<Emp, EmpViewModel>(oldDataFromDb);

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        /// <summary>
        /// Tạo user tương ứng với staff
        /// </summary>
        /// <param name="accountName">Tên account sẽ được tạo</param>
        /// <returns></returns>
        private bool CreateUserDefault(Emp emp )
        {
            bool returnValue = false;
            //kiem tra xem co ton tai chua , neu chua ton tai thi tao , ton tai roi thi khong lam gi ca
            var appUser = AppUserManager.FindByIdAsync(emp.AccountName.ToLower());
            

            if (appUser.Result == null )
            {
                var newAppUser = new AppUser();

                newAppUser.UserName = emp.AccountName.ToLower();
                newAppUser.FullName = emp.FullName;
                newAppUser.CompanyID = emp.CurrentCompanyID;
                newAppUser.DeptID = emp.CurrentDeptID;
                newAppUser.TeamID = emp.CurrentTeamID;
                newAppUser.Avatar = emp.Avatar;
                newAppUser.EmailConfirmed = true;
                newAppUser.Email = emp.WorkingEmail;
                newAppUser.Gender = emp.Gender;
                
                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = AppUserManager.CreateAsync(newAppUser, "Abc12345");
                    if (result.Result.Succeeded)
                    {
                        returnValue = true;
                    }
                    else
                    {
                        returnValue = false;
                    }
                }
                catch (NameDuplicatedException dex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                //update user 

            }
            return returnValue;
        }


        [Route("import")]
        [HttpPost]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.EMP_CARD)]
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
            //do stuff with files if you wish
            //if (result.FormData["param"] == null)
            //{
            //    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chi dinh param");
            //}

            //Upload files
            int addedCount = 0;
            
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
                var listEmps = this.ReadEmpFromExcel(fullPath);
                if (listEmps.Count > 0)
                {
                    foreach (var emp in listEmps)
                    {
                        //tao user
                        _dataService.Add(emp);
                        //tao user mac dinh 
                        if (!string.IsNullOrEmpty(emp.AccountName))
                        {
                            bool kekka = CreateUserDefault(emp);
                        }

                        addedCount++;
                    }
                    _dataService.SaveChanges();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã import " + addedCount + " dòng thành công.");
        }

        private List<Emp> ReadEmpFromExcel(string fullPath)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Emp> listEmp = new List<Emp>();
                EmpViewModel empViewModel;
                Emp emp;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    empViewModel = new EmpViewModel();
                    emp = new Emp();

                    empViewModel.ID = workSheet.Cells[i, (int)EmpImportColumnEnum.ID].Value.ToString().ToInt(0);
                    empViewModel.FullName = workSheet.Cells[ i, (int)EmpImportColumnEnum.FullName].Text.ToString();
                    empViewModel.Name = workSheet.Cells[i, (int)EmpImportColumnEnum.Name].Value.ToString();
                    empViewModel.Furigana = workSheet.Cells[i, (int)EmpImportColumnEnum.Furigana].Text.ToString();
                    int gender = workSheet.Cells[i, (int)EmpImportColumnEnum.Gender].Value.ToString().ToInt(0);
                    if(gender == 1)
                    {
                        empViewModel.Gender = true;
                    }
                    else
                    {
                        empViewModel.Gender = false;
                    }

                    empViewModel.IdentNo = workSheet.Cells[i, (int)EmpImportColumnEnum.IdentNo].Text.ToString();

                    empViewModel.IdentIssueDate =  workSheet.Cells[i, (int)EmpImportColumnEnum.IdentIssueDate].Value.ToDateTime();
                    empViewModel.IdentIssuePlace = workSheet.Cells[i, (int)EmpImportColumnEnum.IdentIssuePlace].Text.ToString();
                    empViewModel.TaxCode = workSheet.Cells[i, (int)EmpImportColumnEnum.TaxCode].Text.ToString();
                    empViewModel.TaxCodeIssueDate = workSheet.Cells[i, (int)EmpImportColumnEnum.TaxCodeIssueDate].Value.ToDateTime();

                    empViewModel.ExtLinkNo = workSheet.Cells[i, (int)EmpImportColumnEnum.ExtLinkNo].Text.ToString();
                    empViewModel.TrainingProfileNo = workSheet.Cells[i, (int)EmpImportColumnEnum.TrainingProfileNo].Text.ToString();
                    empViewModel.BornPlace = workSheet.Cells[i, (int)EmpImportColumnEnum.BornPlace].Text.ToString();
                    empViewModel.Avatar = workSheet.Cells[i, (int)EmpImportColumnEnum.Avatar].Text.ToString();
                    empViewModel.ShowAvatar = true;

                    empViewModel.WorkingEmail = workSheet.Cells[i, (int)EmpImportColumnEnum.WorkingEmail].Text.ToString();
                    empViewModel.PersonalEmail = workSheet.Cells[i, (int)EmpImportColumnEnum.PersonalEmail].Text.ToString();
                    empViewModel.BirthDay = workSheet.Cells[i, (int)EmpImportColumnEnum.BirthDay].Value.ToDateTime();

                    empViewModel.AccountName = workSheet.Cells[i, (int)EmpImportColumnEnum.AccountName].Text.ToString();
                    empViewModel.PhoneNumber1 = workSheet.Cells[i, (int)EmpImportColumnEnum.PhoneNumber1].Text.ToString();
                    empViewModel.PhoneNumber2 = workSheet.Cells[i, (int)EmpImportColumnEnum.PhoneNumber2].Text.ToString();
                    empViewModel.PhoneNumber3 = workSheet.Cells[i, (int)EmpImportColumnEnum.PhoneNumber3].Text.ToString();
                    empViewModel.Address1 = workSheet.Cells[i, (int)EmpImportColumnEnum.Address1].Text.ToString();
                    empViewModel.Address2 = workSheet.Cells[i, (int)EmpImportColumnEnum.Address2].Text.ToString();

                    empViewModel.CurrentCompanyID = workSheet.Cells[i, (int)EmpImportColumnEnum.CurrentCompanyID].Value.ToInt(0);
                    empViewModel.CurrentDeptID = workSheet.Cells[i, (int)EmpImportColumnEnum.CurrentDeptID].Value.ToInt(0);
                    empViewModel.CurrentTeamID = workSheet.Cells[i, (int)EmpImportColumnEnum.CurrentTeamID].Value.ToInt(0);
                    empViewModel.CurrentPositionID = workSheet.Cells[i, (int)EmpImportColumnEnum.CurrentPositionID].Value.ToInt(0);

                    empViewModel.StartIntershipDate = workSheet.Cells[i, (int)EmpImportColumnEnum.StartIntershipDate].Value.ToDateTime();
                    empViewModel.EndIntershipDate = workSheet.Cells[i, (int)EmpImportColumnEnum.EndIntershipDate].Value.ToDateTime();
                    empViewModel.StartWorkingDate = workSheet.Cells[i, (int)EmpImportColumnEnum.StartWorkingDate].Value.ToDateTime();
                    empViewModel.StartLearningDate = workSheet.Cells[i, (int)EmpImportColumnEnum.StartLearningDate].Value.ToDateTime();
                    empViewModel.EndLearningDate = workSheet.Cells[i, (int)EmpImportColumnEnum.EndLearningDate].Value.ToDateTime();
                    empViewModel.StartTrialDate = workSheet.Cells[i, (int)EmpImportColumnEnum.StartTrialDate].Value.ToDateTime();
                    empViewModel.EndTrialDate = workSheet.Cells[i, (int)EmpImportColumnEnum.EndTrialDate].Value.ToDateTime();
                    empViewModel.ContractDate = workSheet.Cells[i, (int)EmpImportColumnEnum.ContractDate].Value.ToDateTime();

                    empViewModel.ContractTypeMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.ContractTypeMasterID].Value.ToInt(0);
                    empViewModel.ContractTypeMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.ContractTypeMasterDetailID].Value.ToInt(0);

                    empViewModel.JobLeaveRequestDate = workSheet.Cells[i, (int)EmpImportColumnEnum.JobLeaveRequestDate].Value.ToDateTime();
                    empViewModel.JobLeaveDate = workSheet.Cells[i, (int)EmpImportColumnEnum.JobLeaveDate].Value.ToDateTime();
                    if (empViewModel.JobLeaveDate.HasValue)
                    {
                        empViewModel.IsJobLeave = true;
                    }else
                    {
                        empViewModel.IsJobLeave = false;
                    }

                    empViewModel.JobLeaveReason = workSheet.Cells[i, (int)EmpImportColumnEnum.JobLeaveReason].Text.ToString();
                    empViewModel.GoogleId = workSheet.Cells[i, (int)EmpImportColumnEnum.GoogleId].Text.ToString();
                    empViewModel.MarriedDate = workSheet.Cells[i, (int)EmpImportColumnEnum.MarriedDate].Value.ToDateTime();
                    empViewModel.ExperienceBeforeContent = workSheet.Cells[i, (int)EmpImportColumnEnum.ExperienceBeforeContent].Text.ToString();
                    if(workSheet.Cells[i, (int)EmpImportColumnEnum.ExperienceBeforeConvert].Value != null)
                    {
                        empViewModel.ExperienceBeforeConvert = workSheet.Cells[i, (int)EmpImportColumnEnum.ExperienceBeforeConvert].Value.ToDecimal(0);
                    }
                    
                    if(workSheet.Cells[i, (int)EmpImportColumnEnum.ExperienceConvert].Value != null)
                    {
                        empViewModel.ExperienceConvert = workSheet.Cells[i, (int)EmpImportColumnEnum.ExperienceConvert].Value.ToDecimal(0);
                    }                    

                    empViewModel.EmpTypeMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.EmpTypeMasterID].Value.ToInt(0);
                    empViewModel.EmpTypeMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.EmpTypeMasterDetailID].Value.ToInt(0);

                    empViewModel.JapaneseLevelMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.JapaneseLevelMasterID].Value.ToInt(0);
                    empViewModel.JapaneseLevelMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.JapaneseLevelMasterDetailID].Value.ToInt(0);

                    empViewModel.BusinessAllowanceLevelMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.BusinessAllowanceLevelMasterID].Value.ToInt(0);
                    empViewModel.BusinessAllowanceLevelMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.BusinessAllowanceLevelMasterDetailID].Value.ToInt(0);

                    empViewModel.RoomWithInternetAllowanceLevelMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.RoomWithInternetAllowanceLevelMasterID].Value.ToInt(0);
                    empViewModel.RoomWithInternetAllowanceLevelMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.RoomWithInternetAllowanceLevelMasterDetailID].Value.ToInt(0);

                    empViewModel.RoomNoInternetAllowanceLevelMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.RoomNoInternetAllowanceLevelMasterID].Value.ToInt(0);
                    empViewModel.RoomNoInternetAllowanceLevelMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.RoomNoInternetAllowanceLevelMasterDetailID].Value.ToInt(0);

                    empViewModel.BseAllowanceLevelMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.BseAllowanceLevelMasterID].Value.ToInt(0);
                    empViewModel.BseAllowanceLevelMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.BseAllowanceLevelMasterDetailID].Value.ToInt(0);

                    empViewModel.CollectMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.CollectMasterID].Value.ToInt(0);
                    empViewModel.CollectMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.CollectMasterDetailID].Value.ToInt(0);

                    empViewModel.EducationLevelMasterID = workSheet.Cells[i, (int)EmpImportColumnEnum.EducationLevelMasterID].Value.ToInt(0);
                    empViewModel.EducationLevelMasterDetailID = workSheet.Cells[i, (int)EmpImportColumnEnum.EducationLevelMasterDetailID].Value.ToInt(0);

                    empViewModel.Temperament = workSheet.Cells[i, (int)EmpImportColumnEnum.Temperament].Text.ToString();
                    empViewModel.Introductor = workSheet.Cells[i, (int)EmpImportColumnEnum.Introductor].Text.ToString();
                    empViewModel.BloodGroup = workSheet.Cells[i, (int)EmpImportColumnEnum.BloodGroup].Text.ToString();
                    empViewModel.Hobby = workSheet.Cells[i, (int)EmpImportColumnEnum.Hobby].Text.ToString();
                    empViewModel.Objective = workSheet.Cells[i, (int)EmpImportColumnEnum.Objective].Text.ToString();
                    empViewModel.Note = workSheet.Cells[i, (int)EmpImportColumnEnum.Note].Text.ToString();

                    empViewModel.AccountData = workSheet.Cells[i, (int)EmpImportColumnEnum.AccountData].Text.ToString();
                    empViewModel.DataStatus = 1;
                    empViewModel.Status = true;

                    emp.UpdateEmp(empViewModel);
                    listEmp.Add(emp);
                }
                return listEmp;
            }
        }

        [HttpGet]
        [Route("allEmpExportXls")]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_CARD)]
        public async Task<HttpResponseMessage> ExportXls(HttpRequestMessage request, string filter = null)
        {
            string fileName = string.Concat("Danh sach nhan vien " + DateTime.Now.ToString("yyyyMMddhhmmsss") + ".xlsx");
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

                await ReportHelper.GenerateXls(data, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private string GetAllEmpSqlForImportColumn()
        {
            string sql = @"SELECT No,
                                    ID,
                                    FullName,
                                    Name,
                                    Furigana,
                                    Gender,
                                    IdentNo,
                                    IdentIssueDate,
                                    IdentIssuePlace,
                                    TaxCode,
                                    TaxCodeIssueDate,
                                    ExtLinkNo,
                                    TrainingProfileNo,
                                    BornPlace,
                                    Avatar,
                                    ShowAvatar,
                                    WorkingEmail,
                                    PersonalEmail,
                                    BirthDay,
                                    AccountName,
                                    PhoneNumber1,
                                    PhoneNumber2,
                                    PhoneNumber3,
                                    Address1,
                                    Address2,
                                    CurrentCompanyID,
                                    CurrentDeptID,
                                    CurrentTeamID,
                                    CurrentPositionID,
                                    StartIntershipDate,
                                    EndIntershipDate,
                                    StartWorkingDate,
                                    StartLearningDate,
                                    EndLearningDate,
                                    StartTrialDate,
                                    EndTrialDate,
                                    ContractDate,
                                    ContractTypeMasterID,
                                    ContractTypeMasterDetailID,
                                    JobLeaveRequestDate,
                                    JobLeaveDate,
                                    IsJobLeave,
                                    JobLeaveReason,
                                    GoogleId,
                                    MarriedDate,
                                    ExperienceBeforeContent,
                                    ExperienceBeforeConvert,
                                    ExperienceConvert,
                                    EmpTypeMasterID,
                                    EmpTypeMasterDetailID,
                                    IsBSE,
                                    JapaneseLevelMasterID,
                                    JapaneseLevelMasterDetailID,
                                    BusinessAllowanceLevelMasterID,
                                    BusinessAllowanceLevelMasterDetailID,
                                    RoomWithInternetAllowanceLevelMasterID,
                                    RoomWithInternetAllowanceLevelMasterDetailID,
                                    RoomNoInternetAllowanceLevelMasterID,
                                    RoomNoInternetAllowanceLevelMasterDetailID,
                                    BseAllowanceLevelMasterID,
                                    BseAllowanceLevelMasterDetailID,
                                    CollectMasterID,
                                    CollectMasterDetailID,
                                    EducationLevelMasterID,
                                    EducationLevelMasterDetailID,
                                    Temperament,
                                    Introductor,
                                    BloodGroup,
                                    Hobby,
                                    Objective,
                                    DisplayOrder,
                                    AccountData,
                                    Note,
                                    DeleteFlag,
                                    DataStatus
                            FROM EMPS ";
            return sql;

        }
        //[Permission(Action = "Read", Function = "USER")]
        private string GetAllFromViewEmpSql(string keyword , int? topRecordSelect = null , string addFilterSql =null)
        {
            string addWhereSql = "";
            string sqlTopSelect = "";

            if (topRecordSelect.HasValue)
            {
                sqlTopSelect = " TOP(" + topRecordSelect.Value + ")";
            }

            if (!string.IsNullOrEmpty(keyword))
                addWhereSql = " AND (COALESCE(FullName,'') + COALESCE(Name,'') + COALESCE(AccountName,' ') + COALESCE(TaxCode,' ') + COALESCE(Note,' ') + ISNULL(DeptName,'') + COALESCE(TeamName,'') + COALESCE(PositionName,'') + COALESCE (EmpTypeName,'')) like N'%" + keyword + "%' ";

            if (!string.IsNullOrEmpty(keyword))
                addWhereSql += " " +  addFilterSql;

            string filterSqlString = _systemConfigService.getEmpSqlFilter(User.Identity.Name, true, addWhereSql);
            string orderBySqlString = _systemConfigService.getEmpSqlOrderBy(User.Identity.Name, true, "", false);
            //get data 
            var model = _systemConfigService.GetByAccount(User.Identity.Name);
            int month = 4;
            int year = 2017;

            if (model.EmpFilterDataValue != null)
            {
                var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(model.EmpFilterDataValue);
                if (empFilterViewModel.systemValue.ExpMonth.HasValue)
                {
                    month = empFilterViewModel.systemValue.ExpMonth.Value;
                }
                if (empFilterViewModel.systemValue.ProcessingYear.HasValue)
                {
                    year = empFilterViewModel.systemValue.ProcessingYear.Value.Year;
                }
            }
            
            string processingDateFrom = year + "/01/01";
            string processingDateTo = year + "/12/31";

            if (model != null && model.ExpMonth.HasValue)
            {
                month = model.ExpMonth.Value;
            }
            if (model != null && model.ProcessingYear.HasValue)
            {
                year = model.ProcessingYear.Value.Year;
            }

            string sql = @" SELECT @TOP_RECORD@ *
                                --dang lam viec cho toi hien tai 	  
                                , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND (JobLeaveDate IS NULL OR JobLeaveDate > CONVERT(DATE,GETDATE())) THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedCount
                                --LTV chinh thuc da nghi viec 
                                , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND (JobLeaveDate IS NOT NULL AND JobLeaveDate < CONVERT(DATE,GETDATE()) ) THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedJobLeavedCount
                                --LTV thu viec nhung khong vao chinh thuc
                                , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NULL AND JobLeaveDate IS NOT NULL AND StartTrialDate IS NOT NULL THEN 1 ELSE 0 END) OVER (PARTITION BY null) TrialJobLeavedCount
	                            , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NULL AND JobLeaveDate IS NULL AND (StartWorkingDate IS NOT NULL OR StartTrialDate IS NOT NULL) THEN 1 ELSE 0 END) OVER (PARTITION BY null) TrialCount
	                            , SUM( case when EmpTypeMasterDetailID IN(1) AND ContractDate IS NOT NULL AND JobLeaveDate IS NULL AND ContractDate >= DATEADD(month,-" + month + @", CONVERT(DATE,GETDATE()))  THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedLTNMonthCount
	                            , SUM( case when EmpTypeMasterDetailID IN(3,4) THEN 1 ELSE 0 END) OVER (PARTITION BY null) OtherCount
	                            , SUM( case when EmpTypeMasterDetailID IN(2) THEN 1 ELSE 0 END) OVER (PARTITION BY null) TransCount
                                --nhan vien nghi viec trong nam tai chinh da setting 
                                , SUM( case when EmpTypeMasterDetailID IN(1) 
                                            AND ContractDate IS NOT NULL 
                                            AND (JobLeaveDate  BETWEEN  '" + processingDateFrom + @"' AND CONVERT(DATE,GETDATE())) THEN 1 ELSE 0 END) OVER (PARTITION BY null) ContractedJobLeavedInProcessingYearCount

                                --nhan vien thu viec da khong vao chinh thuc trong nam ( bao gom ca nhan vien start thu viec trong nam truoc )
                                , SUM( case when EmpTypeMasterDetailID IN(1) 
                                            AND ContractDate IS NULL 
                                            AND ( StartTrialDate IS NOT NULL  OR StartWorkingDate IS NOT NULL)
                                            AND (JobLeaveDate  BETWEEN  '" + processingDateFrom + @"' AND '" + processingDateTo + @"') THEN 1 ELSE 0 END) OVER (PARTITION BY null) TrialJobLeavedInProcessingYearCount

	                            , 0 OnsiteCount 
                                , COUNT(*) OVER ()      TotalRecords
                                , " + month + @"        ExpMonth
                                , " + year + @"         ProcessingYear
                                FROM ViewEmp " + filterSqlString + orderBySqlString;

            sql = sql.Replace("@TOP_RECORD@", sqlTopSelect);
            sql = sql.Replace("@processingDateFrom@", processingDateFrom);
            sql = sql.Replace("@processingDateTo@", processingDateTo);

            return sql;

        }

        private void UpdateMasterId(ref EmpViewModel dataVm)
        {
            dataVm.ContractTypeMasterID = (int)MasterKbnEnum.ContractType;
            dataVm.EmpTypeMasterID = (int)MasterKbnEnum.EmpType;
            dataVm.BseAllowanceLevelMasterID = (int)MasterKbnEnum.BseAllowanceLevel;
            dataVm.CollectMasterID = (int)MasterKbnEnum.ContractType;
            dataVm.EducationLevelMasterID = (int)MasterKbnEnum.EducationLevel;
            dataVm.JapaneseLevelMasterID = (int)MasterKbnEnum.JapaneseLevel;
            dataVm.BusinessAllowanceLevelMasterID = (int)MasterKbnEnum.BusinessAllowanceLevel;
            dataVm.RoomWithInternetAllowanceLevelMasterID = (int)MasterKbnEnum.RoomWithInternetAllowanceLevel;

        }

    }
}