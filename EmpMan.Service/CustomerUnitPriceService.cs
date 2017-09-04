using System.Collections.Generic;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;
using System.Linq;
using System;

namespace EmpMan.Service
{
    /// <summary>
    /// Giao diện thực hiện các tác vụ liên quan đến DB
    /// </summary>
    public interface ICustomerUnitPriceService
    {
        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="customerUnitPrice">Thông tin table đối tượng</param>
        /// <returns>Đối tượng đã được add vào</returns>
        CustomerUnitPrice Add(CustomerUnitPrice customerUnitPrice);

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="customerUnitPrice">Record đối tượng</param>
        void Update(CustomerUnitPrice customerUnitPrice);

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        CustomerUnitPrice Delete(int id);

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        CustomerUnitPrice DeleteLogic(int id);

        /// <summary>
        /// Lấy toàn bộ record của table
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerUnitPrice> GetAll();

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        IEnumerable<CustomerUnitPrice> GetAll(string keyword);

        /// <summary>
        /// Lấy danh sách có phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="page">Trang</param>
        /// <param name="pageSize">Số Record của 1 page</param>
        /// <param name="totalRow">Tổng số record get được</param>
        /// <returns></returns>
        IEnumerable<CustomerUnitPrice> GetAllPaging(string keyword, int page, int pageSize, out int totalRow);

        /// <summary>
        /// Tìm kiếm danh sách record đối tượng thỏa mãn điều kiện keyword
        /// </summary>
        /// <param name="keyword">từ khóa tìm kiếm</param>
        /// <param name="page">trang đối tượng</param>
        /// <param name="pageSize">số record trên 1 page</param>
        /// <param name="sort">trình tự sort</param>
        /// <param name="totalRow">số dòng đối tượng get được</param>
        /// <returns></returns>
        IEnumerable<CustomerUnitPrice> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        /// <summary>
        /// Lấy record đối tượng dựa vào Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        CustomerUnitPrice GetById(int id);

        CustomerUnitPrice GetUnitPriceByCustomer(int customerID,DateTime startDate);

        IEnumerable<CustomerUnitPrice> GetMultiUnitPriceByCustomer(int customerID, DateTime startDate);

        /// <summary>
        /// Lưu các thay đổi vào DB
        /// </summary>
        void SaveChanges();
    }

    public class CustomerUnitPriceService : ICustomerUnitPriceService
    {
        private ICustomerUnitPriceRepository _customerUnitPriceRepository;
        private IUnitOfWork _unitOfWork;

        public CustomerUnitPriceService(ICustomerUnitPriceRepository customerUnitPriceRepository, IUnitOfWork unitOfWork)
        {
            this._customerUnitPriceRepository = customerUnitPriceRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Thêm item vào table đối tượng
        /// </summary>
        /// <param name="customerUnitPrice">Thông tin table đối tượng</param>
        /// <returns></returns>
        public CustomerUnitPrice Add(CustomerUnitPrice customerUnitPrice)
        {
            return _customerUnitPriceRepository.Add(customerUnitPrice);
        }

        /// <summary>
        /// Cập nhật record đối tượng
        /// </summary>
        /// <param name="customerUnitPrice">Record đối tượng</param>
        public void Update(CustomerUnitPrice customerUnitPrice)
        {
            _customerUnitPriceRepository.Update(customerUnitPrice);
        }

        /// <summary>
        /// Xóa vật lý record đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public CustomerUnitPrice Delete(int id)
        {
            return _customerUnitPriceRepository.Delete(id);
        }

        /// <summary>
        /// Xóa logic record (thiết lập flag delete thành true)
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns></returns>
        public CustomerUnitPrice DeleteLogic(int id)
        {
            var dataFromDb = _customerUnitPriceRepository.GetSingleById(id);
            dataFromDb.Status= true;
            _customerUnitPriceRepository.Update(dataFromDb);
            return dataFromDb;
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<CustomerUnitPrice> GetAll()
        {
            return _customerUnitPriceRepository.GetAll();
        }

        /// <summary>
        /// Lấy các record của table dựa vào keyword
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IEnumerable<CustomerUnitPrice> GetAll(string keyword)
        {
            var query = _customerUnitPriceRepository.GetAllNoTracking();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));


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
        public IEnumerable<CustomerUnitPrice> GetAllPaging(string keyword, int page, int pageSize, out int totalRow)
        {
            return _customerUnitPriceRepository.GetMultiPaging(x => (x.Status && (x.Name.Contains(keyword) )), out totalRow, page, pageSize);
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
        public IEnumerable<CustomerUnitPrice> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _customerUnitPriceRepository.GetMulti(x =>x.Name.Contains(keyword));

            switch (sort)
            {
                case "name":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "id":
                    query = query.OrderByDescending(x => x.ID);
                    break;

                default:
                    query = query.OrderByDescending(x => x.StartDate);
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
        public CustomerUnitPrice GetById(int id)
        {
            return _customerUnitPriceRepository.GetSingleById(id);
        }

        /// <summary>
        /// Lấy thông tin đơn giá dựa vào ngày của khách hàng
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <param name="date">Ngày tháng năm tính đơn giá</param>
        /// <returns></returns>
        public CustomerUnitPrice GetUnitPriceByCustomer(int customerID, DateTime startDate)
        {
            var query = _customerUnitPriceRepository.GetMulti(x=> x.CustomerID == customerID).Where(p=>p.StartDate<= startDate).OrderBy(p=> p.StartDate).First();
            return query;
        }

        /// <summary>
        /// Lấy toàn bộ đơn giá theo khách hàng ( 1 khách hàng có thể có nhiều loại đơn giá)
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <param name="startDate">Ngày áp dụng đơn giá</param>
        /// <returns></returns>
        public IEnumerable<CustomerUnitPrice> GetMultiUnitPriceByCustomer(int customerID, DateTime startDate)
        {
            var query = _customerUnitPriceRepository.GetMulti(x => x.CustomerID == customerID).Where(p => p.StartDate <= startDate).OrderBy(p => p.StartDate);
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