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
    public interface IMasterDetailService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="masterDetail">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        MasterDetail Add(MasterDetail masterDetail);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="masterDetail">Record đối tượng</param>
        void Update(MasterDetail masterDetail);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="masterDetail">Record đối tượng</param>
        void Delete(MasterDetail masterDetail);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        MasterDetail Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        MasterDetail DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<MasterDetail> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<MasterDetail> GetAll(string keyword);
        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// 
        /// </summary>
        /// <param name="filterMasterID">Phan loai</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IEnumerable<MasterDetail> GetAll(int? filterMasterID, string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<MasterDetail> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<MasterDetail> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        MasterDetail GetById(int id);

        /// <summary>
        /// Lấy record đối tượng dựa vào key
        /// </summary>
        /// <param name="masterID">Id cha</param>
        /// <param name="masterDetailID">Id con</param>
        /// <returns></returns>
        MasterDetail GetByKey(int masterID, int masterDetailID);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
    }

    public class MasterDetailService : IMasterDetailService
    {
        private IMasterDetailRepository _masterDetailRepository;
        private IUnitOfWork _unitOfWork;

        public MasterDetailService(IMasterDetailRepository masterDetailRepository, IUnitOfWork unitOfWork)
        {
            this._masterDetailRepository = masterDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="masterDetail">Thông tin table đối tượng</param>
        /// <returns></returns>
        public MasterDetail Add(MasterDetail masterDetail)
        {
            return _masterDetailRepository.Add(masterDetail);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="masterDetail">Record đối tượng</param>
        public void Update(MasterDetail masterDetail)
        {
            _masterDetailRepository.Update(masterDetail);
        }

        public void Delete(MasterDetail masterDetail)
        {
            _masterDetailRepository.Delete(masterDetail);
        }
        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public MasterDetail Delete(int id)
        {
            //tu id , get ra key 
            var keyData = _masterDetailRepository.GetMulti(x => x.ID == id).First();

            return _masterDetailRepository.Delete(keyData);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public MasterDetail DeleteLogic(int id)
        {
            var dataFromDb = _masterDetailRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _masterDetailRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<MasterDetail> GetAll()
        {
            return _masterDetailRepository.GetAllNoTracking();
        }

        public IEnumerable<MasterDetail> GetAll(int? filterMasterID, string keyword)
        {
            var query = _masterDetailRepository.GetAll(new string[] { "Master" }) ;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));

            if (filterMasterID.HasValue)
                query = query.Where(x => x.MasterID == filterMasterID.Value);

            return query;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<MasterDetail> GetAll(string keyword)
        {
            var query = _masterDetailRepository.GetAllNoTracking();
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
        public IEnumerable<MasterDetail> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _masterDetailRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
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
        public IEnumerable<MasterDetail> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _masterDetailRepository.GetMulti(x => x.Name.Contains(keyword));

            switch (sort)
            {
                case "name":
                    query = query.OrderByDescending(x => x.Name);
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
        public MasterDetail GetById(int id)
        {
            return _masterDetailRepository.GetSingleById(id);
        }
        /// <summary>
        /// Lấy record đối tượng dựa vào key
        /// </summary>
        /// <param name="masterID">Id cha</param>
        /// <param name="masterDetailID">Id con</param>
        /// <returns></returns>
        public MasterDetail GetByKey(int masterID, int masterDetailID)
        {
            var query = _masterDetailRepository.GetMulti(x => (x.MasterID == masterID && x.MasterDetailCode == masterDetailID)).First();
            return query;
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