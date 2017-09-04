using EmpMan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmpMan.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        T Delete(int id);

        //Delete multi records
        void DeleteMulti(Expression<Func<T, bool>> where);

        // Get an entity by int id
        T GetSingleById(int id);

        // Get an entity by key value
        T GetSingleByKey(object[] id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        /// <summary>
        /// Thay doi trang thai approved cua du lieu
        /// </summary>
        /// <param name="where"></param>
        void ChangeApprovedStatusMulti(Expression<Func<T, bool>> where , string userName , ApprovedStatusEnum approvedStatus);

        /// <summary>
        /// Thay doi trang thai approved cua du lieu dua vao ID cua table
        /// </summary>
        /// <param name="where"></param>
        void ChangeApprovedStatusById(int id , string userName, ApprovedStatusEnum approvedStatus);

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetAllNoTracking(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiNoTracking(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        IEnumerable<T> GetMultiPagingNoTracking(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}