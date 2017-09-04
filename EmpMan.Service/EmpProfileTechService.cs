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
    public interface IEmpProfileTechService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="empProfileTech">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        EmpProfileTech Add(EmpProfileTech empProfileTech);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="empProfileTech">Record đối tượng</param>
        void Update(EmpProfileTech empProfileTech);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpProfileTech Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpProfileTech DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmpProfileTech> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<EmpProfileTech> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<EmpProfileTech> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<EmpProfileTech> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        EmpProfileTech GetById(int id);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
    }

    public class EmpProfileTechService : IEmpProfileTechService
    {
        private IEmpProfileTechRepository _empProfileTechRepository;
        private IUnitOfWork _unitOfWork;

        public EmpProfileTechService(IEmpProfileTechRepository empProfileTechRepository, IUnitOfWork unitOfWork)
        {
            this._empProfileTechRepository = empProfileTechRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="empProfileTech">Thông tin table đối tượng</param>
        /// <returns></returns>
        public EmpProfileTech Add(EmpProfileTech empProfileTech)
        {
            return _empProfileTechRepository.Add(empProfileTech);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="empProfileTech">Record đối tượng</param>
        public void Update(EmpProfileTech empProfileTech)
        {
            _empProfileTechRepository.Update(empProfileTech);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public EmpProfileTech Delete(int id)
        {
            return _empProfileTechRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public EmpProfileTech DeleteLogic(int id)
        {
            var dataFromDb = _empProfileTechRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _empProfileTechRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<EmpProfileTech> GetAll()
        {
            return _empProfileTechRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<EmpProfileTech> GetAll(string keyword)
        {
            var query = _empProfileTechRepository.GetAll();
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
        public IEnumerable<EmpProfileTech> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _empProfileTechRepository.GetMultiPaging(x => (x.Status ), out totalRow, page, pageSize);
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
        public IEnumerable<EmpProfileTech> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _empProfileTechRepository.GetMulti(x => x.Status );

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
        public EmpProfileTech GetById(int id)
        {
            return _empProfileTechRepository.GetSingleById(id);
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