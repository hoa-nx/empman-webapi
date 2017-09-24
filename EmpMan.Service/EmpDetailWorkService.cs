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
    public interface IEmpDetailWorkService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="empDetailWork">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        EmpDetailWork Add(EmpDetailWork empDetailWork);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="empDetailWork">Record đối tượng</param>
        void Update(EmpDetailWork empDetailWork);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpDetailWork Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpDetailWork DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmpDetailWork> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<EmpDetailWork> GetAll(string keyword);

        /// <summary>
        /// Lấy các record của table dựa vào keyword va emp id
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="emp">ma nhan vien</param>
        /// <returns></returns>
        IEnumerable<EmpDetailWork> GetAllByEmp(string keyword, int emp);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<EmpDetailWork> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<EmpDetailWork> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpDetailWork GetById(int id);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();

        EmpManDbContext GetDbContext();

    }

    public class EmpDetailWorkService : IEmpDetailWorkService
    {
        private IEmpDetailWorkRepository _empDetailWorkRepository;
        private IUnitOfWork _unitOfWork;

        public EmpDetailWorkService(IEmpDetailWorkRepository empDetailWorkRepository, IUnitOfWork unitOfWork)
        {
            this._empDetailWorkRepository = empDetailWorkRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="empDetailWork">Thông tin table đối tượng</param>
        /// <returns></returns>
        public EmpDetailWork Add(EmpDetailWork empDetailWork)
        {
            return _empDetailWorkRepository.Add(empDetailWork);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="empDetailWork">Record đối tượng</param>
        public void Update(EmpDetailWork empDetailWork)
        {
            _empDetailWorkRepository.Update(empDetailWork);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public EmpDetailWork Delete(int id)
        {
            return _empDetailWorkRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public EmpDetailWork DeleteLogic(int id)
        {
            var dataFromDb = _empDetailWorkRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _empDetailWorkRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<EmpDetailWork> GetAll()
        {
            return _empDetailWorkRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<EmpDetailWork> GetAll(string keyword)
        {
            var query = _empDetailWorkRepository.GetAll();
            //if (!string.IsNullOrEmpty(keyword))
            //    query = query.Where(x => x.Name.Contains(keyword)|| x.ShortName.Contains(keyword));


            return query;
        }

        public IEnumerable<EmpDetailWork> GetAllByEmp(string keyword, int emp)
        {
            var query = _empDetailWorkRepository.GetMultiNoTracking( x => x.EmpID == emp);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Note.Contains(keyword.ToUpper()));

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
        public IEnumerable<EmpDetailWork> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _empDetailWorkRepository.GetMultiPaging(x => (x.Status ), out totalRow, page, pageSize);
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
        public IEnumerable<EmpDetailWork> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _empDetailWorkRepository.GetMulti(x => x.Status );

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
        public EmpDetailWork GetById(int id)
        {
            return _empDetailWorkRepository.GetSingleById(id);
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public EmpManDbContext GetDbContext()
        {
            return this._unitOfWork.DbContext;
        }

    }
}