using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using System.Data.Entity;
using EmpMan.Data;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface IRevenueService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="revenue">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        Revenue Add(Revenue revenue);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="revenue">Record đối tượng</param>
        void Update(Revenue revenue);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Revenue Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Revenue DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<Revenue> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<Revenue> GetAll(string keyword, string[] includes);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<Revenue> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<Revenue> Search(string keyword, int page, int pageSize, string sort, out int totalRow , string[] includes);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Revenue GetById(int id , string[] includes);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();

        void DetachedEntity(Revenue revenue);

        void ProxyCreationEnabled(bool isProxyCreationEnabled);

        IEnumerable<Revenue> ExcuteSql(string sql);

        EmpManDbContext GetDbContext();
    }

    public class RevenueService : IRevenueService
    {
        private IRevenueRepository _revenueRepository;
        private IUnitOfWork _unitOfWork;

        public RevenueService(IRevenueRepository revenueRepository, IUnitOfWork unitOfWork)
        {
            this._revenueRepository = revenueRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="revenue">Thông tin table đối tượng</param>
        /// <returns></returns>
        public Revenue Add(Revenue revenue)
        {
            return _revenueRepository.Add(revenue);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="revenue">Record đối tượng</param>
        public void Update(Revenue revenue)
        {
            _revenueRepository.Update(revenue);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Revenue Delete(int id)
        {
            return _revenueRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Revenue DeleteLogic(int id)
        {
            var dataFromDb = _revenueRepository.GetSingleById(id);
            dataFromDb.Status = true;
            _revenueRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<Revenue> GetAll()
        {
            return _revenueRepository.GetAllNoTracking();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<Revenue> GetAll(string keyword, string[] includes)
        {
            var query = _revenueRepository.GetMultiNoTracking( x => x.Status, includes);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (("" + x.CustomerName + x.ProjectName + x.ReportTitle + x.ProjectContent + x.Note).ToLower().Contains(keyword.ToLower())));


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
        public IEnumerable<Revenue> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            var query = _revenueRepository.GetMultiPagingNoTracking(x => (x.Status), out totalRow, page, pageSize);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (("" + x.CustomerName + x.ProjectName + x.ReportTitle + x.ProjectContent + x.Note).ToLower().Contains(keyword.ToLower())));

            return query;
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
        public IEnumerable<Revenue> Search(string keyword, int page, int pageSize, string sort, out int totalRow, string[] includes)
        {
            var query = _revenueRepository.GetMulti(x => (x.Status), includes);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (("" + x.CustomerName + x.ProjectName + x.ReportTitle + x.ProjectContent + x.Note).ToLower().Contains(keyword.ToLower())));

            switch (sort)
            {
                case "name":
                    //query = query.OrderByDescending(x => x.Name);
                    break;

                case "code":
                    //query = query.OrderByDescending(x => x.Code);
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
        public Revenue GetById(int id, string[] includes)
        {
            //return _revenueRepository.GetSingleById(id);
            return _revenueRepository.GetSingleByCondition(p => p.ID == id, includes);
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void DetachedEntity(Revenue revenue)
        {
            //_context.Entry(entity).State = EntityState.Detached;
            this._unitOfWork.DbContext.Entry(revenue).State = System.Data.Entity.EntityState.Detached;
        }

        public void ProxyCreationEnabled(bool isProxyCreationEnabled)
        {
            this._unitOfWork.DbContext.Configuration.ProxyCreationEnabled = isProxyCreationEnabled;
        }

        public IEnumerable<Revenue> ExcuteSql(string sql)
        {
            return this._unitOfWork.DbContext.Database.SqlQuery<Revenue>(sql);
            
        }

        public EmpManDbContext GetDbContext()
        {
            return this._unitOfWork.DbContext;
        }
    }
}