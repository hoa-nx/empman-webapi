using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using System.Linq.Expressions;
using System;
using EmpMan.Common.Enums;
using EmpMan.Data;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface IRecruitmentInterviewService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="recruitmentInterview">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        RecruitmentInterview Add(RecruitmentInterview recruitmentInterview);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="recruitmentInterview">Record đối tượng</param>
        void Update(RecruitmentInterview recruitmentInterview);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        RecruitmentInterview Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        RecruitmentInterview DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<RecruitmentInterview> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<RecruitmentInterview> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<RecruitmentInterview> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<RecruitmentInterview> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        RecruitmentInterview GetById(int id);
        /// <summary>
        /// Get dua vao dieu kien dua vao
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        RecruitmentInterview GetSingleByCondition(Expression<Func<RecruitmentInterview, bool>> expression, string[] includes = null);

        /// <summary>
        /// Get dua vao dieu kien dua vao
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IEnumerable<RecruitmentInterview> GetMultiByCondition(Expression<Func<RecruitmentInterview, bool>> expression, string[] includes = null);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Cập nhật trạng thái approved dữ liệu
        /// </summary>
        /// <param name="where"></param>
        /// <param name="userName"></param>
        /// <param name="approvedStatus"></param>
        void ChangeApprovedStatusMulti(Expression<Func<RecruitmentInterview, bool>> where, string userName, ApprovedStatusEnum approvedStatus);

        /// <summary>
        /// Get context
        /// </summary>
        /// <returns></returns>
        EmpManDbContext GetDbContext();
    }

    public class RecruitmentInterviewService : IRecruitmentInterviewService
    {
        private IRecruitmentInterviewRepository _recruitmentInterviewRepository;
        private IUnitOfWork _unitOfWork;

        public RecruitmentInterviewService(IRecruitmentInterviewRepository recruitmentInterviewRepository, IUnitOfWork unitOfWork)
        {
            this._recruitmentInterviewRepository = recruitmentInterviewRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="recruitmentInterview">Thông tin table đối tượng</param>
        /// <returns></returns>
        public RecruitmentInterview Add(RecruitmentInterview recruitmentInterview)
        {
            return _recruitmentInterviewRepository.Add(recruitmentInterview);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="recruitmentInterview">Record đối tượng</param>
        public void Update(RecruitmentInterview recruitmentInterview)
        {
            _recruitmentInterviewRepository.Update(recruitmentInterview);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public RecruitmentInterview Delete(int id)
        {
            return _recruitmentInterviewRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public RecruitmentInterview DeleteLogic(int id)
        {
            var dataFromDb = _recruitmentInterviewRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _recruitmentInterviewRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<RecruitmentInterview> GetAll()
        {
            return _recruitmentInterviewRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<RecruitmentInterview> GetAll(string keyword)
        {
            var query = _recruitmentInterviewRepository.GetAll();
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
        public IEnumerable<RecruitmentInterview> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _recruitmentInterviewRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) || x.ShortName.Contains(keyword))), out totalRow, page, pageSize);
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
        public IEnumerable<RecruitmentInterview> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _recruitmentInterviewRepository.GetMulti(x =>  x.Name.Contains(keyword));

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
        public RecruitmentInterview GetById(int id)
        {
            return _recruitmentInterviewRepository.GetSingleById(id);
        }

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public RecruitmentInterview GetSingleByCondition(Expression<Func<RecruitmentInterview, bool>> expression, string[] includes = null)
        {
            return _recruitmentInterviewRepository.GetSingleByCondition(expression, includes);
        }

        /// <summary>
        /// Get dua vao dieu kien dua vao
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<RecruitmentInterview> GetMultiByCondition(Expression<Func<RecruitmentInterview, bool>> expression, string[] includes = null)
        {
            return _recruitmentInterviewRepository.GetMulti(expression, includes);
        }

        /// <summary>
        /// Cập nhật trạng thái approved dữ liệu
        /// </summary>
        /// <param name="where"></param>
        /// <param name="userName"></param>
        /// <param name="approvedStatus"></param>
        public void ChangeApprovedStatusMulti(Expression<Func<RecruitmentInterview, bool>> where, string userName, ApprovedStatusEnum approvedStatus)
        {
            _recruitmentInterviewRepository.ChangeApprovedStatusMulti(where, userName, approvedStatus);
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