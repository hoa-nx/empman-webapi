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
using EmpMan.Common.ViewModels.Models.Emp;
using Mapster;
using EmpMan.Common;
using System.Web.Script.Serialization;
using EmpMan.Common.ViewModels;

namespace EmpMan.Web.Controllers
{
    [RoutePrefix("api/empdetailwork")]
    [Authorize]
    public class EmpDetailWorkController : ApiControllerBase
    {
        private IEmpDetailWorkService _dataService;
        private IEmpService _empService;
        private ISystemConfigService _systemConfigService;

        public EmpDetailWorkController(IErrorService errorService, IEmpDetailWorkService dataService, IEmpService empService, ISystemConfigService systemConfigService) :
            base(errorService)
        {
            this._dataService = dataService;
            this._empService = empService;
            this._systemConfigService = systemConfigService;
        }

        [Route("getall")]
        [HttpGet]
        //[Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listData = _dataService.GetAll();

                //var listDataVm = Mapper.Map<List<EmpDetailWorkViewModel>>(listData);
                var listDataVm = listData.Adapt<List<EmpDetailWorkViewModel>>();

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listDataVm);

                return response;
            });
        }
        [Route("getallpaging")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _dataService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.StartDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<EmpDetailWork>, List<EmpDetailWorkViewModel>>(query);
                var responseData = query.Adapt<List<EmpDetailWork>, List<EmpDetailWorkViewModel>>();

                var paginationSet = new PaginationSet<EmpDetailWorkViewModel>()
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
        [Route("getallbyemp")]
        [HttpGet]
        //[Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetAllByEmpID(HttpRequestMessage request, int emp)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = GetAllByEmpUseSql(emp);

                var query = model.OrderByDescending(x => x.StartDate).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, query);
                return response;
            });
        }

        [Route("gettimelinebyemp")]
        [HttpGet]
        //[Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetTimelineByEmpID(HttpRequestMessage request, int emp)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = GetTimelineByEmpUseSql(emp);

                var query = model.OrderBy(x => x.StartDate).ToList();

                var response = request.CreateResponse(HttpStatusCode.OK, query);
                return response;
            });
        }

        [Route("getonistebyemp")]
        [HttpGet]
        //[Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetOnsiteDataByEmpID(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = GetOnisteDataByEmpUseSql(null);

                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getallpagingbyemp")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetAllByEmpID(HttpRequestMessage request, int emp, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                //var model = _dataService.GetAllByEmp(keyword,emp);
                var model = GetAllByEmpUseSql(emp);
                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.StartDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                //var responseData = Mapper.Map<List<EmpDetailWork>, List<EmpDetailWorkViewModel>>(query);
                //var responseData = query.Adapt<List<EmpDetailWork>, List<EmpDetailWorkViewModel>>();

                var paginationSet = new PaginationSet<EmpDetailWorkViewModel>()
                {
                    Items = query,
                    PageIndex = page,
                    TotalRows = totalRow,
                    PageSize = pageSize
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        public IEnumerable<EmpDetailWorkViewModel> GetAllByEmpUseSql(int emp)
        {
            string sql = @"SELECT                      
                             EMW.ID
                            ,EMW.EmpID
                            ,EMW.StartDate
                            ,EMW.EndDate
                            ,EMW.CompanyID
                            ,EMW.IsChangeCompanyID
                            ,COM.ShortName CompanyName
                            ,EMW.DeptID
                            ,EMW.IsChangeDeptID
                            ,DEP.ShortName DeptName
                            ,EMW.TeamID
                            ,EMW.IsChangeTeamID
                            ,TEA.ShortName TeamName
                            ,EMW.PositionID
                            ,EMW.IsChangePositionID
                            ,POS.ShortName PositionName
                            ,EMW.EmpTypeMasterID
                            ,EMW.EmpTypeMasterDetailID
                            ,EMW.IsChangeEmpType
                            ,ETP.ShortName EmpTypeName
                            ,EMW.JapaneseLevelMasterID
                            ,EMW.JapaneseLevelMasterDetailID
                            ,EMW.IsChangeJapaneseLevel
                            ,JAP.ShortName JapaneseLevelName
                            ,EMW.BusinessAllowanceLevelMasterID
                            ,EMW.BusinessAllowanceLevelMasterDetailID
                            ,EMW.IsChangeBusinessAllowanceLevel
                            ,ALO.ShortName BusinessAllowanceLevelName
                            ,EMW.RoomWithInternetAllowanceLevelMasterID
                            ,EMW.RoomWithInternetAllowanceLevelMasterDetailID
                            ,EMW.IsChangeRoomWithInternetAllowanceLevel
                            ,rwi.ShortName RoomWithInternetAllowanceLevelName
                            ,EMW.RoomNoInternetAllowanceLevelMasterID
                            ,EMW.RoomNoInternetAllowanceLevelMasterDetailID
                            ,EMW.IsChangeRoomNoInternetAllowanceLevel
                            ,rni.ShortName RoomNoInternetAllowanceLevelName
                            ,EMW.BseAllowanceLevelMasterID
                            ,EMW.BseAllowanceLevelMasterDetailID
                            ,EMW.IsChangeBseAllowanceLevel
                            ,bse.ShortName BseAllowanceLevelName
                            ,EMW.CollectMasterID
                            ,EMW.CollectMasterDetailID
                            ,EMW.IsChangeCollect
                            ,col.ShortName CollectName
                            ,EMW.EducationLevelMasterID
                            ,EMW.EducationLevelMasterDetailID
                            ,EMW.IsChangeEducationLevel
                            ,edu.ShortName EducationLevelName
                            ,EMW.ContractTypeMasterID
                            ,EMW.ContractTypeMasterDetailID
                            ,EMW.IsChangeContractType
                            ,con.ShortName ContractTypeName
                            ,EMW.SignDate
                            ,EMW.Result
                            ,EMW.Action
                            ,EMW.DisplayOrder
                            ,EMW.AccountData
                            ,EMW.Note
                            ,EMW.AccessDataLevel
                            ,EMW.CreatedDate
                            ,EMW.CreatedBy
                            ,EMW.UpdatedDate
                            ,EMW.UpdatedBy
                            ,EMW.MetaKeyword
                            ,EMW.MetaDescription
                            ,EMW.Status
                            ,EMW.DataStatus
                            ,EMW.UserAgent
                            ,EMW.UserHostAddress
                            ,EMW.UserHostName
                            ,EMW.RequestDate
                            ,EMW.RequestBy
                            ,EMW.ApprovedDate
                            ,EMW.ApprovedBy
                            ,EMW.ApprovedStatus
                            ,EMW.Company2ID
                            ,EMW.IsChangeCompany2ID
                            ,CO2.ShortName CompanyName2
                            ,EMW.Dept2ID
                            ,EMW.IsChangeDept2ID
                            ,DE2.ShortName DeptName2
                            ,EMW.Team2ID
                            ,EMW.IsChangeTeam2ID
                            ,TE2.ShortName TeamName2
                            ,EMW.Position2ID
                            ,EMW.IsChangePosition2ID
                            ,PO2.ShortName PositionName2
                            ,EMW.WorkEmpTypeMasterID
                            ,EMW.WorkEmpTypeMasterDetailID
                            ,EMW.IsChangeWorkEmpType
                            ,WET.ShortName WorkEmpTypeName
                            ,EMW.OnsiteCustomerID
                            ,EMW.IsChangeOnsiteCustomerID
                            ,CUS.ShortName OnsiteCustomerName

                            FROM EmpDetailWorks EMW

                            LEFT OUTER JOIN dbo.Companys AS com ON EMW.CompanyID = com.ID 
                            LEFT OUTER JOIN dbo.Depts AS dep ON EMW.DeptID = dep.ID 
                            LEFT OUTER JOIN dbo.Teams AS tea ON EMW.TeamID = tea.ID 
                            LEFT OUTER JOIN dbo.Positions AS pos ON EMW.PositionID = pos.ID 
                            LEFT OUTER JOIN dbo.Companys AS co2 ON EMW.Company2ID = co2.ID 
                            LEFT OUTER JOIN dbo.Depts AS de2 ON EMW.Dept2ID = de2.ID 
                            LEFT OUTER JOIN dbo.Teams AS te2 ON EMW.Team2ID = te2.ID 
                            LEFT OUTER JOIN dbo.Positions AS po2 ON EMW.Position2ID = po2.ID

                            LEFT OUTER JOIN dbo.MasterDetails AS etp ON EMW.EmpTypeMasterID = etp.MasterID AND EMW.EmpTypeMasterDetailID = etp.MasterDetailCode 
                            LEFT OUTER JOIN dbo.MasterDetails AS jap ON EMW.JapaneseLevelMasterID = jap.MasterID AND EMW.JapaneseLevelMasterDetailID = jap.MasterDetailCode 

                            LEFT OUTER JOIN dbo.MasterDetails AS alo ON EMW.BusinessAllowanceLevelMasterID = alo.MasterID AND EMW.BusinessAllowanceLevelMasterDetailID = alo.MasterDetailCode 
                            LEFT OUTER JOIN dbo.MasterDetails AS rwi ON EMW.RoomWithInternetAllowanceLevelMasterID = rwi.MasterID AND EMW.RoomWithInternetAllowanceLevelMasterDetailID = rwi.MasterDetailCode 
                            LEFT OUTER JOIN dbo.MasterDetails AS rni ON EMW.RoomNoInternetAllowanceLevelMasterID = rni.MasterID AND EMW.RoomNoInternetAllowanceLevelMasterDetailID = rni.MasterDetailCode 
                            LEFT OUTER JOIN dbo.MasterDetails AS bse ON EMW.BseAllowanceLevelMasterID = bse.MasterID AND EMW.BseAllowanceLevelMasterDetailID = bse.MasterDetailCode 
                            LEFT OUTER JOIN dbo.MasterDetails AS col ON EMW.CollectMasterID = col.MasterID AND EMW.CollectMasterDetailID = col.MasterDetailCode 
                            LEFT OUTER JOIN dbo.MasterDetails AS edu ON EMW.EducationLevelMasterID = edu.MasterID AND EMW.EducationLevelMasterDetailID = edu.MasterDetailCode
                            LEFT OUTER JOIN dbo.MasterDetails AS con ON EMW.ContractTypeMasterID = con.MasterID AND EMW.ContractTypeMasterDetailID = con.MasterDetailCode 

                            LEFT OUTER JOIN dbo.MasterDetails AS wet ON EMW.WorkEmpTypeMasterID = wet.MasterID AND EMW.WorkEmpTypeMasterDetailID = wet.MasterDetailCode 
                            LEFT OUTER JOIN dbo.Customers AS cus ON EMW.OnsiteCustomerID = cus.ID 
                            WHERE 1= 1 AND EMW.EmpID =@EmpID@ ";
                            
            //thay the parameter
            sql = sql.Replace("@EmpID@", emp.ToString());

            var query = _dataService.GetDbContext().Database.SqlQuery<EmpDetailWorkViewModel>(sql);
            
            return query;

        }

        public IEnumerable<TimelineViewModel> GetTimelineByEmpUseSql(int emp)
        {
            string sql = @"
                        SELECT 
                            DET.EmpID           ID
                            ,DET.FullName       Name
                            ,DET.StartDate      StartDate
                            ,DET.ChangeContent  Title
                            ,DET.Note           Description
                            ,DET.CssName        CssName
                            ,DET.Icon           Icon
                            ,DET.Avatar         Avatar
                            ,DET.CollectName    CollectName
                            ,DET.KeikenFromContractMonths   KeikenFromContractMonths
                            ,DET.Age                        Age
                        FROM
                            (SELECT                      
                                 EMW.ID
                                ,EMW.EmpID
                                ,EMW.StartDate
                                ,EMW.EndDate
                                /*
                                ,EMW.CompanyID
                                ,EMW.IsChangeCompanyID
                                ,COM.ShortName CompanyName
                                ,EMW.DeptID
                                ,EMW.IsChangeDeptID
                                ,DEP.ShortName DeptName
                                ,EMW.TeamID
                                ,EMW.IsChangeTeamID
                                ,TEA.ShortName TeamName
                                ,EMW.PositionID
                                ,EMW.IsChangePositionID
                                ,POS.ShortName PositionName
                                ,EMW.EmpTypeMasterID
                                ,EMW.EmpTypeMasterDetailID
                                ,EMW.IsChangeEmpType
                                ,ETP.ShortName EmpTypeName
                                ,EMW.JapaneseLevelMasterID
                                ,EMW.JapaneseLevelMasterDetailID
                                ,EMW.IsChangeJapaneseLevel
                                ,JAP.ShortName JapaneseLevelName
                                ,EMW.BusinessAllowanceLevelMasterID
                                ,EMW.BusinessAllowanceLevelMasterDetailID
                                ,EMW.IsChangeBusinessAllowanceLevel
                                ,ALO.ShortName BusinessAllowanceLevelName
                                ,EMW.RoomWithInternetAllowanceLevelMasterID
                                ,EMW.RoomWithInternetAllowanceLevelMasterDetailID
                                ,EMW.IsChangeRoomWithInternetAllowanceLevel
                                ,rwi.ShortName RoomWithInternetAllowanceLevelName
                                ,EMW.RoomNoInternetAllowanceLevelMasterID
                                ,EMW.RoomNoInternetAllowanceLevelMasterDetailID
                                ,EMW.IsChangeRoomNoInternetAllowanceLevel
                                ,rni.ShortName RoomNoInternetAllowanceLevelName
                                ,EMW.BseAllowanceLevelMasterID
                                ,EMW.BseAllowanceLevelMasterDetailID
                                ,EMW.IsChangeBseAllowanceLevel
                                ,bse.ShortName BseAllowanceLevelName
                                ,EMW.CollectMasterID
                                ,EMW.CollectMasterDetailID
                                ,EMW.IsChangeCollect
                                */
                                ,col.ShortName CollectName
                                /*
                                ,EMW.EducationLevelMasterID
                                ,EMW.EducationLevelMasterDetailID
                                ,EMW.IsChangeEducationLevel
                                ,edu.ShortName EducationLevelName
                                ,EMW.ContractTypeMasterID
                                ,EMW.ContractTypeMasterDetailID
                                ,EMW.IsChangeContractType
                                ,con.ShortName ContractTypeName
                                ,EMW.SignDate
                                ,EMW.Result
                                ,EMW.Action
                                ,EMW.DisplayOrder
                                ,EMW.AccountData
                                */
                                ,EMW.Note
                                /*
                                ,EMW.AccessDataLevel
                                ,EMW.CreatedDate
                                ,EMW.CreatedBy
                                ,EMW.UpdatedDate
                                ,EMW.UpdatedBy
                                ,EMW.MetaKeyword
                                ,EMW.MetaDescription
                                ,EMW.Status
                                ,EMW.DataStatus
                                ,EMW.UserAgent
                                ,EMW.UserHostAddress
                                ,EMW.UserHostName
                                ,EMW.RequestDate
                                ,EMW.RequestBy
                                ,EMW.ApprovedDate
                                ,EMW.ApprovedBy
                                ,EMW.ApprovedStatus
                                ,EMW.Company2ID
                                ,EMW.IsChangeCompany2ID
                                ,CO2.ShortName CompanyName2
                                ,EMW.Dept2ID
                                ,EMW.IsChangeDept2ID
                                ,DE2.ShortName DeptName2
                                ,EMW.Team2ID
                                ,EMW.IsChangeTeam2ID
                                ,TE2.ShortName TeamName2
                                ,EMW.Position2ID
                                ,EMW.IsChangePosition2ID
                                ,PO2.ShortName PositionName2
                                ,EMW.WorkEmpTypeMasterID
                                ,EMW.WorkEmpTypeMasterDetailID
                                ,EMW.IsChangeWorkEmpType
                                ,WET.ShortName WorkEmpTypeName
                                ,EMW.OnsiteCustomerID
                                ,EMW.IsChangeOnsiteCustomerID
                                ,CUS.ShortName OnsiteCustomerName
                                */
                                ,IIF(EMW.IsChangeCompanyID = 1 ,  com.ShortName,'') 
							        + IIF(EMW.IsChangeDeptID = 1 ,  dep.ShortName,'') 
							        + IIF(EMW.IsChangeTeamID = 1 ,  tea.ShortName,'') 
							        + IIF(EMW.IsChangePositionID = 1 ,  pos.ShortName,'') 
							        + IIF(EMW.IsChangeCompany2ID = 1 ,  co2.ShortName,'') 
							        + IIF(EMW.IsChangeDept2ID = 1 ,  de2.ShortName,'') 
							        + IIF(EMW.IsChangeTeam2ID = 1 ,  te2.ShortName,'') 
							        + IIF(EMW.IsChangePosition2ID = 1 ,  po2.ShortName,'') 
							        + IIF(EMW.IsChangeWorkEmpType = 1 ,  wet.ShortName,'') 
							        + IIF(EMW.IsChangeEmpType = 1 ,  etp.ShortName,'') 
							        + IIF(EMW.IsChangeJapaneseLevel = 1 ,  jap.ShortName,'') 
							        + IIF(EMW.IsChangeBusinessAllowanceLevel = 1 ,  alo.ShortName,'') 
							        + IIF(EMW.IsChangeRoomWithInternetAllowanceLevel = 1 ,  rwi.ShortName,'') 
							        + IIF(EMW.IsChangeRoomNoInternetAllowanceLevel = 1 ,  rni.ShortName,'') 
							        + IIF(EMW.IsChangeBseAllowanceLevel = 1 ,  bse.ShortName,'') 
							        + IIF(EMW.IsChangeCollect = 1 ,  col.ShortName,'') 
							        + IIF(EMW.IsChangeEducationLevel = 1 ,  edu.ShortName,'') 
							        + IIF(EMW.IsChangeContractType = 1 ,  con.ShortName,'') 
							        + IIF(EMW.IsChangeOnsiteCustomerID = 1 , N'Onsite tại KH ' + cus.ShortName,'') ChangeContent

							    , case 
								    when (EMW.IsChangeCompanyID = 1 
									    OR EMW.IsChangeDeptID = 1 
									    OR EMW.IsChangeTeamID = 1 
									    OR EMW.IsChangePositionID = 1 
									    OR EMW.IsChangeCompany2ID = 1  
									    OR EMW.IsChangeDept2ID = 1
									    OR EMW.IsChangeTeam2ID = 1 
									    OR EMW.IsChangePosition2ID = 1 ) then N'cd-primary' 
								    when ( EMW.IsChangeWorkEmpType = 1 OR EMW.IsChangeEmpType = 1 ) then N'cd-info' 
								    when (EMW.IsChangeJapaneseLevel = 1 
									    OR EMW.IsChangeBusinessAllowanceLevel = 1 
									    OR EMW.IsChangeRoomWithInternetAllowanceLevel = 1
									    OR EMW.IsChangeRoomNoInternetAllowanceLevel = 1
									    OR EMW.IsChangeBseAllowanceLevel = 1) then N'cd-success' 
								    when( EMW.IsChangeCollect = 1 
									    OR EMW.IsChangeEducationLevel = 1 ) then  N'cd-info' 
								    when (EMW.IsChangeContractType = 1 ) then N'cd-success' 
								    when (EMW.IsChangeOnsiteCustomerID = 1 ) then N'cd-success' 
								    else N'cd-info' end  CssName

							    , case 
								    when (EMW.IsChangeCompanyID = 1 
									    OR EMW.IsChangeDeptID = 1 
									    OR EMW.IsChangeTeamID = 1 
									    OR EMW.IsChangePositionID = 1 
									    OR EMW.IsChangeCompany2ID = 1  
									    OR EMW.IsChangeDept2ID = 1
									    OR EMW.IsChangeTeam2ID = 1 
									    OR EMW.IsChangePosition2ID = 1 ) then N'fa fa-sitemap' 
								    when ( EMW.IsChangeWorkEmpType = 1 OR EMW.IsChangeEmpType = 1 ) then N'fa fa-random' 
								    when (EMW.IsChangeJapaneseLevel = 1 
									    OR EMW.IsChangeBusinessAllowanceLevel = 1 
									    OR EMW.IsChangeRoomWithInternetAllowanceLevel = 1
									    OR EMW.IsChangeRoomNoInternetAllowanceLevel = 1
									    OR EMW.IsChangeBseAllowanceLevel = 1) then N'fa fa-usd' 
								    when( EMW.IsChangeCollect = 1 
									    OR EMW.IsChangeEducationLevel = 1 ) then  N'fa fa-graduation-cap' 
								    when (EMW.IsChangeContractType = 1 ) then N'fa fa-file-text' 
								    when (EMW.IsChangeOnsiteCustomerID = 1 ) then N'fa fa-plane' 
								    else N'fa fa-tag' end  Icon
                                ,emp.FullName       FullName
                                ,emp.Avatar       
                                ,ISNULL( DATEDIFF(m,[emp].Startworkingdate , CONVERT(DATE,GETDATE())),0)    KeikenFromStartWorkingMonths
	                            ,ISNULL(DATEDIFF(m,[emp].ContractDate ,CONVERT(DATE,GETDATE())),0)          KeikenFromContractMonths
	                            ,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE()))- case when DATEADD(year,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE())), emp.BirthDay)> CONVERT(DATE,GETDATE()) then 1 else 0 end   Age
	                            ,ISNULL(DATEDIFF(DD,[emp].BirthDay , CONVERT(DATE,GETDATE())),0) /362.25 AgeFull

                                FROM EmpDetailWorks EMW

                                LEFT OUTER JOIN dbo.Companys AS com ON EMW.CompanyID = com.ID 
                                LEFT OUTER JOIN dbo.Depts AS dep ON EMW.DeptID = dep.ID 
                                LEFT OUTER JOIN dbo.Teams AS tea ON EMW.TeamID = tea.ID 
                                LEFT OUTER JOIN dbo.Positions AS pos ON EMW.PositionID = pos.ID 
                                LEFT OUTER JOIN dbo.Companys AS co2 ON EMW.Company2ID = co2.ID 
                                LEFT OUTER JOIN dbo.Depts AS de2 ON EMW.Dept2ID = de2.ID 
                                LEFT OUTER JOIN dbo.Teams AS te2 ON EMW.Team2ID = te2.ID 
                                LEFT OUTER JOIN dbo.Positions AS po2 ON EMW.Position2ID = po2.ID

                                LEFT OUTER JOIN dbo.MasterDetails AS etp ON EMW.EmpTypeMasterID = etp.MasterID AND EMW.EmpTypeMasterDetailID = etp.MasterDetailCode 
                                LEFT OUTER JOIN dbo.MasterDetails AS jap ON EMW.JapaneseLevelMasterID = jap.MasterID AND EMW.JapaneseLevelMasterDetailID = jap.MasterDetailCode 

                                LEFT OUTER JOIN dbo.MasterDetails AS alo ON EMW.BusinessAllowanceLevelMasterID = alo.MasterID AND EMW.BusinessAllowanceLevelMasterDetailID = alo.MasterDetailCode 
                                LEFT OUTER JOIN dbo.MasterDetails AS rwi ON EMW.RoomWithInternetAllowanceLevelMasterID = rwi.MasterID AND EMW.RoomWithInternetAllowanceLevelMasterDetailID = rwi.MasterDetailCode 
                                LEFT OUTER JOIN dbo.MasterDetails AS rni ON EMW.RoomNoInternetAllowanceLevelMasterID = rni.MasterID AND EMW.RoomNoInternetAllowanceLevelMasterDetailID = rni.MasterDetailCode 
                                LEFT OUTER JOIN dbo.MasterDetails AS bse ON EMW.BseAllowanceLevelMasterID = bse.MasterID AND EMW.BseAllowanceLevelMasterDetailID = bse.MasterDetailCode 
                                LEFT OUTER JOIN dbo.MasterDetails AS col ON EMW.CollectMasterID = col.MasterID AND EMW.CollectMasterDetailID = col.MasterDetailCode 
                                LEFT OUTER JOIN dbo.MasterDetails AS edu ON EMW.EducationLevelMasterID = edu.MasterID AND EMW.EducationLevelMasterDetailID = edu.MasterDetailCode
                                LEFT OUTER JOIN dbo.MasterDetails AS con ON EMW.ContractTypeMasterID = con.MasterID AND EMW.ContractTypeMasterDetailID = con.MasterDetailCode 

                                LEFT OUTER JOIN dbo.MasterDetails AS wet ON EMW.WorkEmpTypeMasterID = wet.MasterID AND EMW.WorkEmpTypeMasterDetailID = wet.MasterDetailCode 
                                LEFT OUTER JOIN dbo.Customers AS cus ON EMW.OnsiteCustomerID = cus.ID 
                                LEFT OUTER JOIN dbo.Emps AS emp ON EMW.EmpID = emp.ID 
                                LEFT OUTER JOIN dbo.RecruitmentStaffs AS RES ON EMW.EmpID = RES.SystemEmpID 

                                WHERE 1= 1 AND EMW.EmpID =@EmpID@   
                            )DET 
                    UNION ALL
                        SELECT 
                            emp.ID                                                                  ID
                            ,emp.FullName + ' - ' +ISNULL(pos.Name,'')                              Name
                            ,ISNULL(emp.StartTrialDate,emp.StartWorkingDate)                        StartDate
                            ,N'Bắt đầu thử việc'                                                    Title
                            ,emp.Note                                                               Description
                            ,N'cd-pink'                                                             CssName
                            ,N'fa fa-calendar'                                                      Icon
                            ,emp.Avatar                                                             Avatar
                            ,COALESCE(col.ShortName,res.CollectName + ISNULL(res.Grade,''))         CollectName
	                        ,ISNULL(DATEDIFF(m,[emp].ContractDate ,CONVERT(DATE,GETDATE())),0)      KeikenFromContractMonths
	                        ,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE()))- case when DATEADD(year,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE())), emp.BirthDay)> CONVERT(DATE,GETDATE()) then 1 else 0 end   Age
                        FROM
                            dbo.Emps emp
                        LEFT OUTER JOIN dbo.RecruitmentStaffs AS RES ON emp.ID = RES.SystemEmpID 
                        LEFT OUTER JOIN dbo.Companys AS com ON emp.CurrentCompanyID = com.ID 
                        LEFT OUTER JOIN dbo.Depts AS dep ON emp.CurrentDeptID = dep.ID 
                        LEFT OUTER JOIN dbo.Teams AS tea ON emp.CurrentTeamID = tea.ID 
                        LEFT OUTER JOIN dbo.Positions AS pos ON emp.CurrentPositionID = pos.ID 
                        LEFT OUTER JOIN dbo.MasterDetails AS col ON emp.CollectMasterID = col.MasterID AND emp.CollectMasterDetailID = col.MasterDetailCode 

                        WHERE 
                            emp.ID  =@EmpID@  AND emp.StartTrialDate IS NOT NULL 
                    UNION ALL
                        SELECT 
                            emp.ID                                  Id
                            ,emp.FullName                           Name
                            ,emp.ContractDate                       StartDate
                            ,N'Hợp đồng chính thức'                 Title
                            ,emp.Note                               Description
                            ,N'cd-success'                          CssName
                            ,N'fa fa-handshake-o'                   Icon
                            ,emp.Avatar                             Avatar
                            ,COALESCE(col.ShortName,res.CollectName + ISNULL(res.Grade,'')) CollectName
	                        ,ISNULL(DATEDIFF(m,[emp].ContractDate ,CONVERT(DATE,GETDATE())),0)          KeikenFromContractMonths
	                        ,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE()))- case when DATEADD(year,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE())), emp.BirthDay)> CONVERT(DATE,GETDATE()) then 1 else 0 end   Age
                        FROM
                            dbo.Emps emp
                        LEFT OUTER JOIN dbo.RecruitmentStaffs AS RES ON emp.ID = RES.SystemEmpID 
                        LEFT OUTER JOIN dbo.MasterDetails AS col ON emp.CollectMasterID = col.MasterID AND emp.CollectMasterDetailID = col.MasterDetailCode 

                        WHERE 
                            emp.ID  =@EmpID@ AND emp.ContractDate IS NOT NULL

                    UNION ALL
                        SELECT 
                            emp.ID                                  Id
                            ,emp.FullName                           Name
                            ,emp.JobLeaveDate                       StartDate
                            ,N'Nghỉ việc'                           Title
                            ,emp.Note                               Description
                            ,N'cd-danger'                           CssName
                            ,N'fa fa-user-times'                    Icon
                            ,emp.Avatar                             Avatar
                            ,COALESCE(col.ShortName,res.CollectName + ISNULL(res.Grade,'')) CollectName
	                        ,ISNULL(DATEDIFF(m,[emp].ContractDate ,CONVERT(DATE,GETDATE())),0)          KeikenFromContractMonths
	                        ,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE()))- case when DATEADD(year,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE())), emp.BirthDay)> CONVERT(DATE,GETDATE()) then 1 else 0 end   Age
                        FROM
                            dbo.Emps emp
                        LEFT OUTER JOIN dbo.RecruitmentStaffs AS RES ON emp.ID = RES.SystemEmpID 
                        LEFT OUTER JOIN dbo.MasterDetails AS col ON emp.CollectMasterID = col.MasterID AND emp.CollectMasterDetailID = col.MasterDetailCode 

                        WHERE 
                            emp.ID  =@EmpID@  AND emp.JobLeaveDate IS NOT NULL

                UNION ALL 
                --Lay danh sach cac ngay co ngay ket thuc nhu chuyen ve dept cu , onsite tro ve
                        SELECT                      
                            EMW.ID
                        ,EMW.EmpID
                        ,EMW.EndDate
                        ,EMW.EndDate
                        /*
                        ,EMW.CompanyID
                        ,EMW.IsChangeCompanyID
                        ,COM.ShortName CompanyName
                        ,EMW.DeptID
                        ,EMW.IsChangeDeptID
                        ,DEP.ShortName DeptName
                        ,EMW.TeamID
                        ,EMW.IsChangeTeamID
                        ,TEA.ShortName TeamName
                        ,EMW.PositionID
                        ,EMW.IsChangePositionID
                        ,POS.ShortName PositionName
                        ,EMW.EmpTypeMasterID
                        ,EMW.EmpTypeMasterDetailID
                        ,EMW.IsChangeEmpType
                        ,ETP.ShortName EmpTypeName
                        ,EMW.JapaneseLevelMasterID
                        ,EMW.JapaneseLevelMasterDetailID
                        ,EMW.IsChangeJapaneseLevel
                        ,JAP.ShortName JapaneseLevelName
                        ,EMW.BusinessAllowanceLevelMasterID
                        ,EMW.BusinessAllowanceLevelMasterDetailID
                        ,EMW.IsChangeBusinessAllowanceLevel
                        ,ALO.ShortName BusinessAllowanceLevelName
                        ,EMW.RoomWithInternetAllowanceLevelMasterID
                        ,EMW.RoomWithInternetAllowanceLevelMasterDetailID
                        ,EMW.IsChangeRoomWithInternetAllowanceLevel
                        ,rwi.ShortName RoomWithInternetAllowanceLevelName
                        ,EMW.RoomNoInternetAllowanceLevelMasterID
                        ,EMW.RoomNoInternetAllowanceLevelMasterDetailID
                        ,EMW.IsChangeRoomNoInternetAllowanceLevel
                        ,rni.ShortName RoomNoInternetAllowanceLevelName
                        ,EMW.BseAllowanceLevelMasterID
                        ,EMW.BseAllowanceLevelMasterDetailID
                        ,EMW.IsChangeBseAllowanceLevel
                        ,bse.ShortName BseAllowanceLevelName
                        ,EMW.CollectMasterID
                        ,EMW.CollectMasterDetailID
                        ,EMW.IsChangeCollect
                        */
                        ,col.ShortName CollectName
                        /*
                        ,EMW.EducationLevelMasterID
                        ,EMW.EducationLevelMasterDetailID
                        ,EMW.IsChangeEducationLevel
                        ,edu.ShortName EducationLevelName
                        ,EMW.ContractTypeMasterID
                        ,EMW.ContractTypeMasterDetailID
                        ,EMW.IsChangeContractType
                        ,con.ShortName ContractTypeName
                        ,EMW.SignDate
                        ,EMW.Result
                        ,EMW.Action
                        ,EMW.DisplayOrder
                        ,EMW.AccountData
                        */
                        ,EMW.Note
                        /*
                        ,EMW.AccessDataLevel
                        ,EMW.CreatedDate
                        ,EMW.CreatedBy
                        ,EMW.UpdatedDate
                        ,EMW.UpdatedBy
                        ,EMW.MetaKeyword
                        ,EMW.MetaDescription
                        ,EMW.Status
                        ,EMW.DataStatus
                        ,EMW.UserAgent
                        ,EMW.UserHostAddress
                        ,EMW.UserHostName
                        ,EMW.RequestDate
                        ,EMW.RequestBy
                        ,EMW.ApprovedDate
                        ,EMW.ApprovedBy
                        ,EMW.ApprovedStatus
                        ,EMW.Company2ID
                        ,EMW.IsChangeCompany2ID
                        ,CO2.ShortName CompanyName2
                        ,EMW.Dept2ID
                        ,EMW.IsChangeDept2ID
                        ,DE2.ShortName DeptName2
                        ,EMW.Team2ID
                        ,EMW.IsChangeTeam2ID
                        ,TE2.ShortName TeamName2
                        ,EMW.Position2ID
                        ,EMW.IsChangePosition2ID
                        ,PO2.ShortName PositionName2
                        ,EMW.WorkEmpTypeMasterID
                        ,EMW.WorkEmpTypeMasterDetailID
                        ,EMW.IsChangeWorkEmpType
                        ,WET.ShortName WorkEmpTypeName
                        ,EMW.OnsiteCustomerID
                        ,EMW.IsChangeOnsiteCustomerID
                        ,CUS.ShortName OnsiteCustomerName
                        */
                        ,IIF(EMW.IsChangeCompanyID = 1 ,  com.ShortName,'') 
	                        + IIF(EMW.IsChangeDeptID = 1 ,dep.ShortName,'') 
	                        + IIF(EMW.IsChangeTeamID = 1 ,  tea.ShortName,'') 
	                        + IIF(EMW.IsChangePositionID = 1 ,  pos.ShortName,'') 
	                        + IIF(EMW.IsChangeCompany2ID = 1 ,  co2.ShortName,'') 
	                        + IIF(EMW.IsChangeDept2ID = 1 ,  de2.ShortName,'') 
	                        + IIF(EMW.IsChangeTeam2ID = 1 ,  te2.ShortName,'') 
	                        + IIF(EMW.IsChangePosition2ID = 1 ,  po2.ShortName,'') 
	                        + IIF(EMW.IsChangeWorkEmpType = 1 ,  wet.ShortName,'') 
	                        + IIF(EMW.IsChangeEmpType = 1 ,  etp.ShortName,'') 
	                        + IIF(EMW.IsChangeJapaneseLevel = 1 ,  jap.ShortName,'') 
	                        + IIF(EMW.IsChangeBusinessAllowanceLevel = 1 ,  alo.ShortName,'') 
	                        + IIF(EMW.IsChangeRoomWithInternetAllowanceLevel = 1 ,  rwi.ShortName,'') 
	                        + IIF(EMW.IsChangeRoomNoInternetAllowanceLevel = 1 ,  rni.ShortName,'') 
	                        + IIF(EMW.IsChangeBseAllowanceLevel = 1 ,  bse.ShortName,'') 
	                        + IIF(EMW.IsChangeCollect = 1 ,  col.ShortName,'') 
	                        + IIF(EMW.IsChangeEducationLevel = 1 ,  edu.ShortName,'') 
	                        + IIF(EMW.IsChangeContractType = 1 ,  con.ShortName,'') 
	                        + IIF(EMW.IsChangeOnsiteCustomerID = 1 , N'Về nước sau khi onsite tại KH ' + ISNULL(cus.ShortName,''),'') ChangeContent
	
                        , case 
	                        when (EMW.IsChangeCompanyID = 1 
		                        OR EMW.IsChangeDeptID = 1 
		                        OR EMW.IsChangeTeamID = 1 
		                        OR EMW.IsChangePositionID = 1 
		                        OR EMW.IsChangeCompany2ID = 1  
		                        OR EMW.IsChangeDept2ID = 1
		                        OR EMW.IsChangeTeam2ID = 1 
		                        OR EMW.IsChangePosition2ID = 1 ) then N'cd-primary' 
	                        when ( EMW.IsChangeWorkEmpType = 1 OR EMW.IsChangeEmpType = 1 ) then N'cd-info' 
	                        when (EMW.IsChangeJapaneseLevel = 1 
		                        OR EMW.IsChangeBusinessAllowanceLevel = 1 
		                        OR EMW.IsChangeRoomWithInternetAllowanceLevel = 1
		                        OR EMW.IsChangeRoomNoInternetAllowanceLevel = 1
		                        OR EMW.IsChangeBseAllowanceLevel = 1) then N'cd-success' 
	                        when( EMW.IsChangeCollect = 1 
		                        OR EMW.IsChangeEducationLevel = 1 ) then  N'cd-info' 
	                        when (EMW.IsChangeContractType = 1 ) then N'cd-success' 
	                        when (EMW.IsChangeOnsiteCustomerID = 1 ) then N'cd-success' 
	                        else N'cd-info' end  CssName

                        , case 
	                        when (EMW.IsChangeCompanyID = 1 
		                        OR EMW.IsChangeDeptID = 1 
		                        OR EMW.IsChangeTeamID = 1 
		                        OR EMW.IsChangePositionID = 1 
		                        OR EMW.IsChangeCompany2ID = 1  
		                        OR EMW.IsChangeDept2ID = 1
		                        OR EMW.IsChangeTeam2ID = 1 
		                        OR EMW.IsChangePosition2ID = 1 ) then N'fa fa-sitemap' 
	                        when ( EMW.IsChangeWorkEmpType = 1 OR EMW.IsChangeEmpType = 1 ) then N'fa fa-random' 
	                        when (EMW.IsChangeJapaneseLevel = 1 
		                        OR EMW.IsChangeBusinessAllowanceLevel = 1 
		                        OR EMW.IsChangeRoomWithInternetAllowanceLevel = 1
		                        OR EMW.IsChangeRoomNoInternetAllowanceLevel = 1
		                        OR EMW.IsChangeBseAllowanceLevel = 1) then N'fa fa-usd' 
	                        when( EMW.IsChangeCollect = 1 
		                        OR EMW.IsChangeEducationLevel = 1 ) then  N'fa fa-graduation-cap' 
	                        when (EMW.IsChangeContractType = 1 ) then N'fa fa-file-text' 
	                        when (EMW.IsChangeOnsiteCustomerID = 1 ) then N'fa fa-plane' 
	                        else N'fa fa-tag' end  Icon
                        ,emp.FullName       FullName
                        ,emp.Avatar       
                        ,ISNULL( DATEDIFF(m,[emp].Startworkingdate , CONVERT(DATE,GETDATE())),0)    KeikenFromStartWorkingMonths
                        ,ISNULL(DATEDIFF(m,[emp].ContractDate ,CONVERT(DATE,GETDATE())),0)          KeikenFromContractMonths
                        ,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE()))- case when DATEADD(year,DATEDIFF(year, [emp].BirthDay, CONVERT(DATE,GETDATE())), emp.BirthDay)> CONVERT(DATE,GETDATE()) then 1 else 0 end   Age
                        ,ISNULL(DATEDIFF(DD,[emp].BirthDay , CONVERT(DATE,GETDATE())),0) /362.25 AgeFull

                        FROM EmpDetailWorks EMW
                        LEFT OUTER JOIN dbo.Emps AS emp ON EMW.EmpID = emp.ID 
                        LEFT OUTER JOIN dbo.Companys AS com ON emp.CurrentCompanyID = com.ID 
                        LEFT OUTER JOIN dbo.Depts AS dep ON emp.CurrentDeptID = dep.ID 
                        LEFT OUTER JOIN dbo.Teams AS tea ON emp.CurrentTeamID = tea.ID 
                        LEFT OUTER JOIN dbo.Positions AS pos ON emp.CurrentPositionID= pos.ID 
                        LEFT OUTER JOIN dbo.Companys AS co2 ON EMW.Company2ID = co2.ID 
                        LEFT OUTER JOIN dbo.Depts AS de2 ON EMW.Dept2ID = de2.ID 
                        LEFT OUTER JOIN dbo.Teams AS te2 ON EMW.Team2ID = te2.ID 
                        LEFT OUTER JOIN dbo.Positions AS po2 ON EMW.Position2ID = po2.ID

                        LEFT OUTER JOIN dbo.MasterDetails AS etp ON EMW.EmpTypeMasterID = etp.MasterID AND EMW.EmpTypeMasterDetailID = etp.MasterDetailCode 
                        LEFT OUTER JOIN dbo.MasterDetails AS jap ON EMW.JapaneseLevelMasterID = jap.MasterID AND EMW.JapaneseLevelMasterDetailID = jap.MasterDetailCode 

                        LEFT OUTER JOIN dbo.MasterDetails AS alo ON EMW.BusinessAllowanceLevelMasterID = alo.MasterID AND EMW.BusinessAllowanceLevelMasterDetailID = alo.MasterDetailCode 
                        LEFT OUTER JOIN dbo.MasterDetails AS rwi ON EMW.RoomWithInternetAllowanceLevelMasterID = rwi.MasterID AND EMW.RoomWithInternetAllowanceLevelMasterDetailID = rwi.MasterDetailCode 
                        LEFT OUTER JOIN dbo.MasterDetails AS rni ON EMW.RoomNoInternetAllowanceLevelMasterID = rni.MasterID AND EMW.RoomNoInternetAllowanceLevelMasterDetailID = rni.MasterDetailCode 
                        LEFT OUTER JOIN dbo.MasterDetails AS bse ON EMW.BseAllowanceLevelMasterID = bse.MasterID AND EMW.BseAllowanceLevelMasterDetailID = bse.MasterDetailCode 
                        LEFT OUTER JOIN dbo.MasterDetails AS col ON EMW.CollectMasterID = col.MasterID AND EMW.CollectMasterDetailID = col.MasterDetailCode 
                        LEFT OUTER JOIN dbo.MasterDetails AS edu ON EMW.EducationLevelMasterID = edu.MasterID AND EMW.EducationLevelMasterDetailID = edu.MasterDetailCode
                        LEFT OUTER JOIN dbo.MasterDetails AS con ON EMW.ContractTypeMasterID = con.MasterID AND EMW.ContractTypeMasterDetailID = con.MasterDetailCode 

                        LEFT OUTER JOIN dbo.MasterDetails AS wet ON EMW.WorkEmpTypeMasterID = wet.MasterID AND EMW.WorkEmpTypeMasterDetailID = wet.MasterDetailCode 
                        LEFT OUTER JOIN dbo.Customers AS cus ON EMW.OnsiteCustomerID = cus.ID 
                        LEFT OUTER JOIN dbo.RecruitmentStaffs AS RES ON EMW.EmpID = RES.SystemEmpID 

                        WHERE 1= 1 AND EMW.EmpID = @EmpID@ 
                        AND EMW.WorkEmpTypeMasterID IS NOT NULL 
                        AND EMW.EndDate IS NOT NULL
                        AND 1 = CASE WHEN EMW.WorkEmpTypeMasterDetailID =1 AND  EMW.DeptID <> Emp.CurrentDeptID THEN  1 --dept khac sang ho tro
		                        WHEN EMW.WorkEmpTypeMasterDetailID =2 AND  EMW.DeptID <> Emp.CurrentDeptID  THEN  1 --sang dept khac
		                        WHEN EMW.WorkEmpTypeMasterDetailID =3  THEN  1 --onsite
		                        WHEN EMW.WorkEmpTypeMasterDetailID =4  THEN  1 --nghi giua chung
		                        ELSE 0 END

                ";

            //thay the parameter
            sql = sql.Replace("@EmpID@", emp.ToString());

            var query = _dataService.GetDbContext().Database.SqlQuery<TimelineViewModel>(sql);

            return query;

        }

        private IEnumerable<EmpViewModel> GetOnisteDataByEmpUseSql(int? emp)
        {
            DateTime startDate= DateTime.Now;
            DateTime endDate = DateTime.Now;

            //get data cau hinh cua  use dang login
            var model =_systemConfigService.GetByAccount(User.Identity.Name.ToLower());

            string filterSqlString = _systemConfigService.getEmpSqlFilter(User.Identity.Name, false);
            string orderBySqlString = _systemConfigService.getEmpSqlOrderBy(User.Identity.Name, true, "", false);

            if (model != null)
            {
                //doc thong tin setting 
                var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(model.EmpFilterDataValue);

                if (model.EmpFilterDataValue != null)
                {
                    if (empFilterViewModel.chkGetDataToDate.Value)
                    {
                        if (empFilterViewModel.getDataToDateTo.HasValue)
                        {
                            startDate = empFilterViewModel.getDataToDateTo.Value;
                            endDate = empFilterViewModel.getDataToDateTo.Value;
                        }
                    }
                }
            }

            string sql = @"SELECT ID , FullName  FROM[dbo].[GetOnisteEmpListAtDate] (@CompanyID,@DeptID,NULL,@startDate,@endDate) WHERE 1=1 @EmpID@ " + filterSqlString;

            sql = sql.Replace("@CompanyID", User.Identity.GetApplicationUser().CompanyID.ToString());
            sql = sql.Replace("@DeptID", User.Identity.GetApplicationUser().DeptID.ToString());
            sql = sql.Replace("@TeamID", "NULL");
            sql = sql.Replace("@startDate", "'" + startDate.ToString("yyyy/MM/dd") + "'");
            sql = sql.Replace("@endDate", "'" + endDate.ToString("yyyy/MM/dd") + "'");

            if (emp.HasValue) { 
                sql = sql.Replace("@EmpID@", " AND ID = " + emp.Value.ToString());
            }else
            {
                sql = sql.Replace("@EmpID@", "");
            }
            var query = _dataService.GetDbContext().Database.SqlQuery<EmpViewModel>(sql);
            return query;
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        [Permission(Action = FunctionActions.READ, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _dataService.GetById(id);

                //var responseData = Mapper.Map<EmpDetailWork, EmpDetailWorkViewModel>(model);
                var responseData = model.Adapt<EmpDetailWork, EmpDetailWorkViewModel>();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("add")]
        [HttpPut]
        [Permission(Action = FunctionActions.CREATE, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage Create(HttpRequestMessage request, EmpDetailWorkViewModel dataVm)
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
                    if (dataVm.ListEmpID.Count > 0)
                    {
                        for(int i =0; i< dataVm.ListEmpID.Count; i++)
                        {
                            EmpDetailWork newData = new EmpDetailWork();

                            dataVm.EmpID = dataVm.ListEmpID[i].Value;

                            /** cập nhật các thông tin chung **/
                            dataVm.CreatedDate = DateTime.Now;
                            dataVm.CreatedBy = User.Identity.Name;

                            dataVm.UpdatedDate = DateTime.Now;
                            dataVm.UpdatedBy = User.Identity.Name;
                            //Người sở hữu dữ liệu
                            dataVm.AccountData = User.Identity.GetApplicationUser().Email;

                            newData.UpdateEmpDetailWork(dataVm);

                            //cap nhat lai du lieu cho nhan vien ( bang emps)
                            this.UpdateEmpData(ref newData);

                            //truong hop khong tao data chi tiet 
                            if (dataVm.IsDetailWorkCreateData.HasValue && dataVm.IsDetailWorkCreateData.Value)
                            {
                                var data = _dataService.Add(newData);
                                _dataService.SaveChanges();
                            }

                        }
                    }
                    
                    //tra ve so dong duoc tao moi
                    response = request.CreateResponse(HttpStatusCode.Created, dataVm.ListEmpID.Count);
                }
                return response;
            });
        }

        /// <summary>
        /// Cap nhat du lieu tu bang work cho nhan vien
        /// </summary>
        /// <param name="detailWork"></param>
        private void UpdateEmpData(ref EmpDetailWork detailWork)
        {
            this.ResetIsChangeFlag(ref detailWork);

            Emp emp = _empService.GetById(detailWork.EmpID);
            if (detailWork.CompanyID != null && emp.CurrentCompanyID != detailWork.CompanyID)
            {
                emp.CurrentCompanyID = detailWork.CompanyID;
                detailWork.IsChangeCompanyID = 1;
            }
            if (detailWork.DeptID != null && emp.CurrentDeptID != detailWork.DeptID)
            {
                emp.CurrentDeptID = detailWork.DeptID;
                detailWork.IsChangeDeptID = 1;
            }

            if (detailWork.TeamID != null && emp.CurrentTeamID != detailWork.TeamID)
            {
                emp.CurrentTeamID = detailWork.TeamID;
                detailWork.IsChangeTeamID = 1;
            }

            if (detailWork.PositionID != null && emp.CurrentPositionID != detailWork.PositionID)
            {
                emp.CurrentPositionID = detailWork.PositionID;
                detailWork.IsChangePositionID = 1;
            }

            if (detailWork.EmpTypeMasterID != null && detailWork.EmpTypeMasterDetailID != null)
            {
                emp.EmpTypeMasterID = detailWork.EmpTypeMasterID;
                emp.EmpTypeMasterDetailID = detailWork.EmpTypeMasterDetailID;
                detailWork.IsChangeEmpType = 1;
            }

            if (detailWork.JapaneseLevelMasterID != null && detailWork.JapaneseLevelMasterDetailID != null)
            {
                emp.JapaneseLevelMasterID = detailWork.JapaneseLevelMasterID;
                emp.JapaneseLevelMasterDetailID = detailWork.JapaneseLevelMasterDetailID;
                detailWork.IsChangeJapaneseLevel = 1;
            }

            if (detailWork.BusinessAllowanceLevelMasterID != null && detailWork.BusinessAllowanceLevelMasterDetailID != null)
            {
                emp.BusinessAllowanceLevelMasterID = detailWork.BusinessAllowanceLevelMasterID;
                emp.BusinessAllowanceLevelMasterDetailID = detailWork.BusinessAllowanceLevelMasterDetailID;
                detailWork.IsChangeBusinessAllowanceLevel = 1;
            }

            if (detailWork.RoomWithInternetAllowanceLevelMasterID != null && detailWork.RoomWithInternetAllowanceLevelMasterDetailID != null)
            {
                emp.RoomWithInternetAllowanceLevelMasterID = detailWork.RoomWithInternetAllowanceLevelMasterID;
                emp.RoomWithInternetAllowanceLevelMasterDetailID = detailWork.RoomWithInternetAllowanceLevelMasterDetailID;
                detailWork.IsChangeRoomWithInternetAllowanceLevel = 1;
            }

            if (detailWork.RoomNoInternetAllowanceLevelMasterID != null && detailWork.RoomNoInternetAllowanceLevelMasterDetailID != null)
            {
                emp.RoomNoInternetAllowanceLevelMasterID = detailWork.RoomNoInternetAllowanceLevelMasterID;
                emp.RoomNoInternetAllowanceLevelMasterDetailID = detailWork.RoomNoInternetAllowanceLevelMasterDetailID;
                detailWork.IsChangeRoomNoInternetAllowanceLevel = 1;
            }

            if (detailWork.BseAllowanceLevelMasterID != null && detailWork.BseAllowanceLevelMasterDetailID != null)
            {
                emp.BseAllowanceLevelMasterID = detailWork.BseAllowanceLevelMasterID;
                emp.BseAllowanceLevelMasterDetailID = detailWork.BseAllowanceLevelMasterDetailID;
                detailWork.IsChangeBseAllowanceLevel = 1;
            }
            if (detailWork.CollectMasterID != null && detailWork.CollectMasterDetailID != null)
            {
                emp.CollectMasterID = detailWork.CollectMasterID;
                emp.CollectMasterDetailID = detailWork.CollectMasterDetailID;
                detailWork.IsChangeCollect = 1;
            }

            if (detailWork.EducationLevelMasterID != null && detailWork.EducationLevelMasterDetailID != null)
            {
                emp.EducationLevelMasterID = detailWork.EducationLevelMasterID;
                emp.EducationLevelMasterDetailID = detailWork.EducationLevelMasterDetailID;
                detailWork.IsChangeEducationLevel = 1;
            }

            if (detailWork.ContractTypeMasterID != null && detailWork.ContractTypeMasterDetailID != null)
            {
                emp.ContractTypeMasterID = detailWork.ContractTypeMasterID;
                emp.ContractTypeMasterDetailID = detailWork.ContractTypeMasterDetailID;
                detailWork.IsChangeContractType = 1;
            }
            //onsite 
            if (detailWork.OnsiteCustomerID != null && detailWork.WorkEmpTypeMasterDetailID != null && detailWork.WorkEmpTypeMasterDetailID ==3) {
                detailWork.IsChangeOnsiteCustomerID = 1;
            }

            emp.UpdatedDate = DateTime.Now;
            emp.UpdatedBy = User.Identity.Name;
            //cap nhat data
            _empService.Update(emp);
            _empService.SaveChanges();


        }

        private void ResetIsChangeFlag(ref EmpDetailWork detailWork)
        {
            
            detailWork.IsChangeCompanyID = 0;
            
            detailWork.IsChangeDeptID = 0;
            
            detailWork.IsChangeTeamID = 0;
            
            detailWork.IsChangePositionID = 0;
            
            detailWork.IsChangeEmpType = 0;
            
            detailWork.IsChangeJapaneseLevel = 0;
            
            detailWork.IsChangeBusinessAllowanceLevel = 0;
            
            detailWork.IsChangeRoomWithInternetAllowanceLevel = 0;

            detailWork.IsChangeRoomNoInternetAllowanceLevel = 0;
            
            detailWork.IsChangeBseAllowanceLevel = 0;
            
            detailWork.IsChangeCollect = 0;
            
            detailWork.IsChangeEducationLevel = 0;
            
            detailWork.IsChangeContractType = 0;
            
            detailWork.IsChangeOnsiteCustomerID = 0;
            
        }

        [Route("update")]
        [HttpPut]
        [Permission(Action = FunctionActions.UPDATE, Function = FunctionConstants.EMP_WORK)]
        public HttpResponseMessage Update(HttpRequestMessage request, EmpDetailWorkViewModel dataVm)
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

                    dataFromDb.UpdateEmpDetailWork(dataVm);

                    dataFromDb.UpdatedDate = DateTime.Now;
                    dataFromDb.UpdatedBy = User.Identity.Name;
                    //Người sở hữu dữ liệu
                    dataFromDb.AccountData = User.Identity.GetApplicationUser().Email;

                    _dataService.Update(dataFromDb);
                    _dataService.SaveChanges();

                    //var responseData = Mapper.Map<EmpDetailWork, EmpDetailWorkViewModel>(dataFromDb);
                    var responseData = dataFromDb.Adapt<EmpDetailWork, EmpDetailWorkViewModel>();

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.EMP_WORK)]
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

                    //var responseData = Mapper.Map<EmpDetailWork, EmpDetailWorkViewModel>(oldDataFromDb);
                    var responseData = oldDataFromDb.Adapt<EmpDetailWork, EmpDetailWorkViewModel>();

                    response = request.CreateResponse(HttpStatusCode.Created, responseData);

                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Permission(Action = FunctionActions.DELETE, Function = FunctionConstants.EMP_WORK)]
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