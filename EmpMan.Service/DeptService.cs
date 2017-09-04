using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface IDeptService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="dept">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        Dept Add(Dept dept);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="dept">Record đối tượng</param>
        void Update(Dept dept);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Dept Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Dept DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<Dept> GetAll();

        /// <summary>
        /// Lay danh sach phong ban tung cong ty
        /// </summary>
        /// <param name="filterCompanyID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IEnumerable<Dept> GetAll(int? filterCompanyID,string keyword);

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<Dept> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<Dept> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<Dept> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Dept GetById(int id);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
    }

    public class DeptService : IDeptService
    {
        private IDeptRepository _deptRepository;
        private IUnitOfWork _unitOfWork;

        public DeptService(IDeptRepository deptRepository, IUnitOfWork unitOfWork)
        {
            this._deptRepository = deptRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="dept">Thông tin table đối tượng</param>
        /// <returns></returns>
        public Dept Add(Dept dept)
        {
            return _deptRepository.Add(dept);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="dept">Record đối tượng</param>
        public void Update(Dept dept)
        {
            _deptRepository.Update(dept);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Dept Delete(int id)
        {
            return _deptRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Dept DeleteLogic(int id)
        {
            var dataFromDb = _deptRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _deptRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<Dept> GetAll()
        {
            return _deptRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<Dept> GetAll(string keyword)
        {
            var query = _deptRepository.GetAllNoTracking();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)|| x.ShortName.Contains(keyword));


            return query;
        }

        public IEnumerable<Dept> GetAll(int? filterCompanyID, string keyword)
        {
            var query = _deptRepository.GetAll(new string[] { "TopManager", "ViceManager1", "Manager1" });
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));

            if (filterCompanyID.HasValue)
                query = query.Where(x => x.CompanyID == filterCompanyID.Value );

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
        public IEnumerable<Dept> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _deptRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
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
        public IEnumerable<Dept> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _deptRepository.GetMulti(x => x.Name.Contains(keyword));

            switch (sort)
            {
                case "name":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "id":
                    query = query.OrderByDescending(x => x.ID);
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
        public Dept GetById(int id)
        {
            return _deptRepository.GetSingleById(id);
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

    }
}