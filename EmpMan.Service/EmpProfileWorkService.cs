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
    public interface IEmpProfileWorkService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="empProfileWork">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        EmpProfileWork Add(EmpProfileWork empProfileWork);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="empProfileWork">Record đối tượng</param>
        void Update(EmpProfileWork empProfileWork);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpProfileWork Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpProfileWork DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmpProfileWork> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<EmpProfileWork> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<EmpProfileWork> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<EmpProfileWork> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpProfileWork GetById(int id);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
    }

    public class EmpProfileWorkService : IEmpProfileWorkService
    {
        private IEmpProfileWorkRepository _empProfileWorkRepository;
        private IUnitOfWork _unitOfWork;

        public EmpProfileWorkService(IEmpProfileWorkRepository empProfileWorkRepository, IUnitOfWork unitOfWork)
        {
            this._empProfileWorkRepository = empProfileWorkRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="empProfileWork">Thông tin table đối tượng</param>
        /// <returns></returns>
        public EmpProfileWork Add(EmpProfileWork empProfileWork)
        {
            return _empProfileWorkRepository.Add(empProfileWork);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="empProfileWork">Record đối tượng</param>
        public void Update(EmpProfileWork empProfileWork)
        {
            _empProfileWorkRepository.Update(empProfileWork);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public EmpProfileWork Delete(int id)
        {
            return _empProfileWorkRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public EmpProfileWork DeleteLogic(int id)
        {
            var dataFromDb = _empProfileWorkRepository.GetSingleById(id);

            dataFromDb.Status= true;
            _empProfileWorkRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<EmpProfileWork> GetAll()
        {
            return _empProfileWorkRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<EmpProfileWork> GetAll(string keyword)
        {
            var query = _empProfileWorkRepository.GetAll();
            //if (!string.IsNullOrEmpty(keyword))
            //    query = query.Where(x => x.Name.Contains(keyword)|| x.ShortName.Contains(keyword));


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
        public IEnumerable<EmpProfileWork> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _empProfileWorkRepository.GetMultiPaging(x => (x.Status ), out totalRow, page, pageSize);
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
        public IEnumerable<EmpProfileWork> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _empProfileWorkRepository.GetMulti(x => x.Status );

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
        public EmpProfileWork GetById(int id)
        {
            return _empProfileWorkRepository.GetSingleById(id);
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