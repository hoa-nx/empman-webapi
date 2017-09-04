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
    public interface IRecruitmentService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="recruitment">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        Recruitment Add(Recruitment recruitment);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="recruitment">Record đối tượng</param>
        void Update(Recruitment recruitment);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Recruitment Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Recruitment DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<Recruitment> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<Recruitment> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<Recruitment> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<Recruitment> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Recruitment GetById(int id);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        Recruitment GetByKey(string id);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Get context
        /// </summary>
        /// <returns></returns>
        EmpManDbContext GetDbContext();
    }

    public class RecruitmentService : IRecruitmentService
    {
        private IRecruitmentRepository _recruitmentRepository;
        private IUnitOfWork _unitOfWork;

        public RecruitmentService(IRecruitmentRepository recruitmentRepository, IUnitOfWork unitOfWork)
        {
            this._recruitmentRepository = recruitmentRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="recruitment">Thông tin table đối tượng</param>
        /// <returns></returns>
        public Recruitment Add(Recruitment recruitment)
        {
            return _recruitmentRepository.Add(recruitment);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="recruitment">Record đối tượng</param>
        public void Update(Recruitment recruitment)
        {
            _recruitmentRepository.Update(recruitment);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Recruitment Delete(int id)
        {
            return _recruitmentRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Recruitment DeleteLogic(int id)
        {
            var dataFromDb = _recruitmentRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _recruitmentRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<Recruitment> GetAll()
        {
            return _recruitmentRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<Recruitment> GetAll(string keyword)
        {
            var query = _recruitmentRepository.GetAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => (x.Name + x.Note + x.ID ).ToLower().Contains(keyword.ToLower()));
            
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
        public IEnumerable<Recruitment> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _recruitmentRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
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
        public IEnumerable<Recruitment> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _recruitmentRepository.GetMulti(x =>  x.Name.Contains(keyword));

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
        public Recruitment GetById(int id)
        {
            return _recruitmentRepository.GetSingleById(id);
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public Recruitment GetByKey(string id)
        {
            return _recruitmentRepository.GetSingleByKey(new object[] { id });
        }

        /// <summary>
        /// Cập nhật vào DB
        /// </summary>
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Get db context
        /// </summary>
        /// <returns></returns>
        public EmpManDbContext GetDbContext()
        {
            return this._unitOfWork.DbContext;
        }

    }
}