using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using EmpMan.Data;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface IEmpService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="emp">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        Emp Add(Emp emp);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="emp">Record đối tượng</param>
        void Update(Emp emp);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Emp Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Emp DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<Emp> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<Emp> GetAll(string keyword, string[] includes = null);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<Emp> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<Emp> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Emp GetById(int id);

        /// <summary>
        /// Lấy record đối tượng dựa vào account
        /// </summary>
        /// <param name="account">account đối tượng</param>
        /// <returns></returns>
        Emp GetByAccount(string account);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Get số No lớn nhất hiện tại
        /// </summary>
        /// <returns></returns>
        int GetMaxID();

        EmpManDbContext GetDbContext();

    }

    public class EmpService : IEmpService
    {
        private IEmpRepository _empRepository;
        private IUnitOfWork _unitOfWork;

        public EmpService(IEmpRepository empRepository, IUnitOfWork unitOfWork)
        {
            this._empRepository = empRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="emp">Thông tin table đối tượng</param>
        /// <returns></returns>
        public Emp Add(Emp emp)
        {
            return _empRepository.Add(emp);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="emp">Record đối tượng</param>
        public void Update(Emp emp)
        {
            _empRepository.Update(emp);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Emp Delete(int id)
        {
            return _empRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Emp DeleteLogic(int id)
        {
            var dataFromDb = _empRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _empRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<Emp> GetAll()
        {
            return _empRepository.GetAllNoTracking();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<Emp> GetAll(string keyword, string[] includes = null)
        {
            var query = _empRepository.GetAllNoTracking(includes);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (x.Name + x.FullName + x.Note + x.AccountName + x.TaxCode ).ToLower().Contains(keyword.ToLower()));


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
        public IEnumerable<Emp> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _empRepository.GetMultiPagingNoTracking(x => (x.Name + x.FullName + x.Note + x.AccountName + x.TaxCode).ToLower().Contains(keyword.ToLower()), out totalRow, page, pageSize);
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
        public IEnumerable<Emp> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            keyword = keyword.ToLower();
            var query = _empRepository.GetMultiNoTracking(x => (x.Name + x.FullName + x.Note + x.AccountName + x.TaxCode).ToLower().Contains(keyword.ToLower()));

            switch (sort)
            {
                case "name":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "fullname":
                    query = query.OrderByDescending(x => x.FullName);
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
        public Emp GetById(int id)
        {
            return _empRepository.GetSingleById(id);
        }

        public Emp GetByAccount(string account)
        {
            return _empRepository.GetSingleByCondition(x => x.AccountName.ToLower() == account.ToLower());
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();

        }

        public int GetMaxID()
        {
            return _unitOfWork.DbContext.Emps.Max(p => p.ID);
        }

        public EmpManDbContext GetDbContext()
        {
            return this._unitOfWork.DbContext;
        }

    }
}