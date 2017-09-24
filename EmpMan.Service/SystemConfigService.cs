using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using EmpMan.Data;
using EmpMan.Common.ViewModels;
using System.Web.Script.Serialization;
using EmpMan.Common;
using System;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface ISystemConfigService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="systemConfig">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        SystemConfig Add(SystemConfig systemConfig);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="systemConfig">Record đối tượng</param>
        void Update(SystemConfig systemConfig);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        SystemConfig Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        SystemConfig DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<SystemConfig> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<SystemConfig> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<SystemConfig> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<SystemConfig> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        SystemConfig GetById(int id);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="account">Tai khoan</param>
        /// <returns></returns>
        SystemConfig GetByAccount(string account);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
        IEnumerable<SystemConfig> ExcuteSql(string sql);

        EmpManDbContext GetDbContext();

        EmpFilterViewModel GetEmpFilterConfig(int code);
        /// <summary>
        /// Ley ve chuoi sql chua dieu kien filter nhan vien
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isWhereTrueStringAppend"></param>
        /// <param name="otherWhere"></param>
        /// <returns></returns>
        string getEmpSqlFilter(string account, bool isWhereTrueStringAppend, string otherWhere = "");
        /// <summary>
        /// Cau SQL dung de ORDER BY
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isAsc"></param>
        /// <param name="appendOrderByStringNoComma"></param>
        /// <param name="isFirstInsert"></param>
        /// <returns></returns>
        string getEmpSqlOrderBy(string account, bool isAsc, string appendOrderByStringNoComma, bool isFirstInsert);
    }

    public class SystemConfigService : ISystemConfigService
    {
        private ISystemConfigRepository _systemConfigRepository;
        private IUnitOfWork _unitOfWork;

        public SystemConfigService(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            this._systemConfigRepository = systemConfigRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="systemConfig">Thông tin table đối tượng</param>
        /// <returns></returns>
        public SystemConfig Add(SystemConfig systemConfig)
        {
            return _systemConfigRepository.Add(systemConfig);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="systemConfig">Record đối tượng</param>
        public void Update(SystemConfig systemConfig)
        {
            _systemConfigRepository.Update(systemConfig);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public SystemConfig Delete(int id)
        {
            return _systemConfigRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public SystemConfig DeleteLogic(int id)
        {
            var dataFromDb = _systemConfigRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _systemConfigRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<SystemConfig> GetAll()
        {
            return _systemConfigRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<SystemConfig> GetAll(string keyword)
        {
            var query = _systemConfigRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)|| x.ShortName.Contains(keyword));


            return query;
        }

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        public IEnumerable<SystemConfig> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _systemConfigRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
        }

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        public IEnumerable<SystemConfig> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _systemConfigRepository.GetMulti(x =>  x.Name.Contains(keyword));

            switch (sort)
            {
                case "name":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "code":
                    query = query.OrderByDescending(x => x.Code);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public SystemConfig GetById(int id)
        {
            return _systemConfigRepository.GetSingleById(id);
        }

        public SystemConfig GetByAccount(string account)
        {
            return _systemConfigRepository.GetAll().Where(x => (x.Code.ToLower() == account.ToLower() )).FirstOrDefault();
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<SystemConfig> ExcuteSql(string sql)
        {
            return this._unitOfWork.DbContext.Database.SqlQuery<SystemConfig>(sql);

        }

        public EmpManDbContext GetDbContext()
        {
            return this._unitOfWork.DbContext;
        }

        public EmpFilterViewModel GetEmpFilterConfig(int code)
        {
            SystemConfig config = GetById(code);

            string filterString = config.EmpFilterDataValue;

            var obj = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(filterString);

            return new EmpFilterViewModel();
        }

        public string getEmpSqlFilter(string account , bool isWhereTrueStringAppend , string otherWhere ="")
        {
            string sql = "";

            //get data 
            var model = GetByAccount(account);

            var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(model.EmpFilterDataValue);
            int month = 4;
            int year = DateTime.Now.Year;

            if (model.EmpFilterDataValue != null)
            {
                if (empFilterViewModel.systemValue.ExpMonth.HasValue)
                {
                    month = empFilterViewModel.systemValue.ExpMonth.Value;
                }
                if (empFilterViewModel.systemValue.ProcessingYear.HasValue)
                {
                    year = empFilterViewModel.systemValue.ProcessingYear.Value.ToLocalTime().Year;
                }
            }

            string processingDateFrom = year + "/01/01";
            string processingDateTo = year + "/12/31";

            if (isWhereTrueStringAppend)
            {
                sql = " WHERE 1 =1 ";
            }
            //Truong hop co thoi diem thong ke , thay the cac GETDATE() thanh ngay mong muon thong ke 
            bool chkGetDataToDate = false;
            string getDataToDateFrom = "GETDATE()";
            string getDataToDateTo = "GETDATE()";
            if (empFilterViewModel != null)
            {
                if (empFilterViewModel.chkGetDataToDate.HasValue)
                {
                    if (empFilterViewModel.chkGetDataToDate.Value)
                    {
                        //co setting tri 
                        chkGetDataToDate = true;
                        if (empFilterViewModel.getDataToDateTo.HasValue)
                        {
                            getDataToDateTo = "'" +  empFilterViewModel.getDataToDateTo.Value.ToLocalTime().ToString("yyyy/MM/dd") + "'";
                        }
                    }
                }
            }

            //phong ban 
            if (empFilterViewModel != null)
            {
                //cac thong tin chung 
                switch (empFilterViewModel.selectDataTypes)
                {
                    case "1" :
                        //neu la nghi viec ( nhung nhan vien nghi viec nhung co ngay nghi viec <= ngay hien tai hoac nho hon ngay chi dinh tren man hinh
                        //sql += " AND " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " <= CONVERT(DATE,GETDATE())";
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " <= CONVERT(DATE,@GET_DATA_TO_DATE_TO@)";
                        break;

                    case "2":
                        //lay tat ca nhân viên đang làm việc trong năm tài chính trở đi 
                        sql += " AND (" + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " IS NULL OR " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " >= CONVERT(DATE,'" + processingDateFrom + "' ))";

                        break;

                    case "3":
                        //dang lam viec
                        //sql += " AND (" + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " IS NULL OR " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " >= CONVERT(DATE,GETDATE()))";
                        sql += " AND (" + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " IS NULL OR " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " >= CONVERT(DATE,@GET_DATA_TO_DATE_TO@))";
                        break;

                    case "4":
                        //nghi viec sap toi ( nhung nhan vien nghi viec nhung co ngay nghi viec > ngay hien tai
                        //sql += " AND " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " >= CONVERT(DATE,GETDATE())";
                        //GET_DATA_TO_DATE_TO
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE + " >= CONVERT(DATE,@GET_DATA_TO_DATE_TO@)";
                        break;

                    case "99":
                        //nghi viec sap toi ( nhung nhan vien nghi viec nhung co ngay nghi viec > ngay hien tai
                        sql += " ";
                        break;
                }
                //Ngày vào công ty 
                //neu co check thi moi thuc thi filter
                //if (empFilterViewModel.chkStartWorkingDate) {
                    //tam thoi loai bo dieu kien nay
                    //sql += getDateSql(empFilterViewModel.startWorkingDateFrom, empFilterViewModel.startWorkingDateTo, CommonConstants.SEARCH_COL_NAME_START_WORKING_DATE);
                //}
                //Ngày vào thử việc
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkTrialDate)
                {
                    if (chkGetDataToDate && empFilterViewModel.getDataToDateTo.HasValue && empFilterViewModel.getDataToDateTo.Value < empFilterViewModel.trialDateTo.Value)
                    {
                        sql += getDateSql(empFilterViewModel.trialDateFrom, empFilterViewModel.getDataToDateTo, CommonConstants.SEARCH_COL_NAME_START_TRIAL_DATE);
                    }
                    else
                    {
                        sql += getDateSql(empFilterViewModel.trialDateFrom, empFilterViewModel.trialDateTo, CommonConstants.SEARCH_COL_NAME_START_TRIAL_DATE);
                    }
                    
                }
                //Ngày vào ky HD
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkContractDate)
                {
                    if (chkGetDataToDate && empFilterViewModel.getDataToDateTo.HasValue && empFilterViewModel.getDataToDateTo.Value > empFilterViewModel.contractDateTo.Value)
                    {
                        sql += getDateSql(empFilterViewModel.contractDateFrom, empFilterViewModel.getDataToDateTo, CommonConstants.SEARCH_COL_NAME_CONTRACT_DATE);
                    }
                    else{
                        sql += getDateSql(empFilterViewModel.contractDateFrom, empFilterViewModel.contractDateTo, CommonConstants.SEARCH_COL_NAME_CONTRACT_DATE);
                    }
                        
                }

                //Ngày nghỉ việc
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkJobLeaveDate)
                {
                    if (chkGetDataToDate && empFilterViewModel.getDataToDateTo.HasValue && empFilterViewModel.getDataToDateTo.Value > empFilterViewModel.jobLeaveDateTo.Value)
                    {
                        sql += getDateSql(empFilterViewModel.jobLeaveDateFrom, empFilterViewModel.getDataToDateTo, CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE);
                    }else
                    {
                        sql += getDateSql(empFilterViewModel.jobLeaveDateFrom, empFilterViewModel.jobLeaveDateTo, CommonConstants.SEARCH_COL_NAME_JOB_LEAVE_DATE);
                    }
                }

                //PHONG BAN
                if (empFilterViewModel.chkDept)
                {
                    if (empFilterViewModel.selectDepts !=null && empFilterViewModel.selectDepts.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectDepts);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_DEPTID + " IN (" + result + ")";
                    }
                }
                //TEAM
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkTeam)
                {
                    if (empFilterViewModel.selectTeams!=null && empFilterViewModel.selectTeams.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectTeams);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_TEAMID + " IN (" + result + ")";
                    }
                }

                //POSITION 
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkPosition)
                {
                    if (empFilterViewModel.selectPositions!=null && empFilterViewModel.selectPositions.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectPositions);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_POSITIONID + " IN (" + result + ")";
                    }
                }

                //LOAI CONG VIEC
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkEmpType)
                {
                    if (empFilterViewModel.selectEmpTypes !=null && empFilterViewModel.selectEmpTypes.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectEmpTypes);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_EMP_TYPE + " IN (" + result + ")";
                    }
                }

                //TIENG NHAT
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkJapaneseLevel)
                {
                    if (empFilterViewModel.selectJapaneseLevels!=null && empFilterViewModel.selectJapaneseLevels.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectJapaneseLevels);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_JAPANESE_LEVEL + " IN (" + result + ")";
                    }
                }

                //PC NGHIEP VU
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkBussinessAllowanceLevel)
                {
                    if (empFilterViewModel.selectBussinessAllowanceLevels !=null && empFilterViewModel.selectBussinessAllowanceLevels.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectBussinessAllowanceLevels);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_BUSSINESS_ALLOWANCE_LEVEL + " IN (" + result + ")";
                    }
                }

                //BSE
                //neu co check thi moi thuc thi filter
                if (empFilterViewModel.chkBseLevel)
                {
                    if (empFilterViewModel.selectBseLevels !=null && empFilterViewModel.selectBseLevels.Count() > 0)
                    {
                        var result = string.Join(",", empFilterViewModel.selectBseLevels);
                        sql += " AND " + CommonConstants.SEARCH_COL_NAME_BSE_LEVEL + " IN (" + result + ")";
                    }
                }

                //THONG TIN KHAC 
                //Chi hoc viec 
                if (empFilterViewModel.chkLearning)
                {
                    sql += " AND " + CommonConstants.SEARCH_COL_NAME_START_LEARNING_DATE + " IS NOT NULL ";
                }

                //Chi nhan vien thu viec
                if (empFilterViewModel.chkLearning)
                {
                    sql += " AND " + CommonConstants.SEARCH_COL_NAME_START_TRIAL_DATE + " IS NOT NULL ";
                }

                //Chi nhan vien co qui doi kinh nghiem
                if (empFilterViewModel.chkLearning)
                {
                    sql += " AND ISNULL(" + CommonConstants.SEARCH_COL_NAME_EXPERIENCE_CONVERT + ",0)  <> 0 ";
                }


            }


            //check otherWhere
            if (otherWhere.Length > 0)
            {
                sql += " " + otherWhere;
            }

            //thay the parameter 

            sql = sql.Replace("@GET_DATA_TO_DATE_TO@", getDataToDateTo);

            return sql;

        }

        public string getEmpSqlOrderBy(string account , bool isAsc  ,string appendOrderByStringNoComma , bool isFirstInsert )
        {
            string orderBy = "";
            string orderString = " ASC ";

            if (!isAsc)
                orderString = " DES";

            //get data 
            var model = GetByAccount(account);

            var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(model.EmpFilterDataValue);
            if (empFilterViewModel != null)
            {
                string[] orderByConfig = empFilterViewModel.sort;

                if (orderByConfig == null) return orderBy;

                if(orderByConfig.Count() > 0)
                {
                    //string[] order = Array.ConvertAll(s.Split(','), string.Parse);
                    foreach(string item in orderByConfig)
                    {
                        //get column name base on value
                        switch ( item)
                        {
                            case CommonConstants.SORT_NAME_ID:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_ID + orderString;
                                break;

                            case CommonConstants.SORT_NAME_FULLNAME:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_FULLNAME + orderString;
                                break;

                            case CommonConstants.SORT_NAME_NAME:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_NAME + orderString;
                                break;

                            case CommonConstants.SORT_NAME_BIRTHDAY:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_BIRTHDAY + orderString;
                                break;

                            case CommonConstants.SORT_NAME_START_WORKING_DATE:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_START_WORKING_DATE+ orderString;
                                break;

                            case CommonConstants.SORT_NAME_CONTRACT_DATE:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_CONTRACT_DATE  + orderString;
                                break;

                            case CommonConstants.SORT_NAME_DEPT:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_DEPTID + orderString;
                                break;

                            case CommonConstants.SORT_NAME_TEAM:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_TEAMID + orderString;
                                break;

                            case CommonConstants.SORT_NAME_POSITION:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_POSITIONID + orderString;
                                break;

                            case CommonConstants.SORT_NAME_TRANING_DATE:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_START_TRIAL_DATE + orderString;
                                break;
                            case CommonConstants.SORT_NAME_GENDER:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_GENDER + orderString;
                                break;

                            case CommonConstants.SORT_NAME_ACCOUNT:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_ACCOUNT_NAME + orderString;
                                break;

                            case CommonConstants.SORT_NAME_EMPTYPE:
                                orderBy += "," + CommonConstants.SEARCH_COL_NAME_EMP_TYPE + orderString;
                                break;

                        } //switch
                    }//loop

                    if(orderBy.Trim().Length > 0)
                    {
                        //loai bo dau , o dau 
                        orderBy = orderBy.TrimStart().Substring(1, orderBy.Length-1);
                    }

                    if(appendOrderByStringNoComma.Trim().Length > 0)
                    {
                        if (isFirstInsert)
                        {
                            //chen vao dau
                            orderBy = appendOrderByStringNoComma + "," + orderBy;
                        }else
                        {
                            //chen vao cuoi 
                            orderBy += "," + appendOrderByStringNoComma;
                        }
                    }
                    orderBy = " ORDER BY " + orderBy;
                }
            }
                

            return  orderBy;
        }

        private string getDateSql(DateTime? dateFrom , DateTime? dateTo , string colName )
        {
            string sql = "";

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                //ca start va from deu co tri
                sql += " AND " + colName + " BETWEEN CONVERT(DATE,'" + dateFrom.Value.ToLocalTime() + "') AND CONVERT(DATE,'" + dateTo.Value.ToLocalTime() + "')";

            }
            else if (dateFrom.HasValue && dateTo == null)
            {
                //neu chi co date start
                sql += " AND " + colName + " >= CONVERT(DATE,'" + dateFrom.Value.ToLocalTime() + "')";
            }
            else if (dateFrom == null && dateTo.HasValue)
            {
                //neu chi co date end
                sql += " AND " + colName + " <= CONVERT(DATE,'" + dateTo.Value.ToLocalTime() + "')";
            }
            return sql;

        }



    }
}