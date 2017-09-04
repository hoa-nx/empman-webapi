﻿using System.Collections.Generic;
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
    public interface IFileStorageService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="fileStorage">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        FileStorage Add(FileStorage fileStorage);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="fileStorage">Record đối tượng</param>
        void Update(FileStorage fileStorage);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        FileStorage Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        FileStorage DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<FileStorage> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<FileStorage> GetAll(string keyword);

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<FileStorage> GetAllByKey(string table , string key);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<FileStorage> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<FileStorage> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        FileStorage GetById(int id);

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

    public class FileStorageService : IFileStorageService
    {
        private IFileStorageRepository _fileStorageRepository;
        private IUnitOfWork _unitOfWork;

        public FileStorageService(IFileStorageRepository fileStorageRepository, IUnitOfWork unitOfWork)
        {
            this._fileStorageRepository = fileStorageRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="fileStorage">Thông tin table đối tượng</param>
        /// <returns></returns>
        public FileStorage Add(FileStorage fileStorage)
        {
            return _fileStorageRepository.Add(fileStorage);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="fileStorage">Record đối tượng</param>
        public void Update(FileStorage fileStorage)
        {
            _fileStorageRepository.Update(fileStorage);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public FileStorage Delete(int id)
        {
            return _fileStorageRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public FileStorage DeleteLogic(int id)
        {
            var dataFromDb = _fileStorageRepository.GetSingleById(id);

            dataFromDb.Status= true;
            _fileStorageRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<FileStorage> GetAll()
        {
            return _fileStorageRepository.GetAll();
        }

        public IEnumerable<FileStorage> GetAllByKey(string table, string key)
        {
            return _fileStorageRepository.GetAll().Where(p => p.RelatedTable.ToLower() == table.ToLower() && p.RelatedKey.ToLower() == key.ToLower()) ;
        }
        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<FileStorage> GetAll(string keyword)
        {
            var query = _fileStorageRepository.GetAll();
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
        public IEnumerable<FileStorage> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _fileStorageRepository.GetMultiPaging(x => (x.Status), out totalRow, page, pageSize);
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
        public IEnumerable<FileStorage> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _fileStorageRepository.GetMulti(x => x.Status );

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
        public FileStorage GetById(int id)
        {
            return _fileStorageRepository.GetSingleById(id);
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