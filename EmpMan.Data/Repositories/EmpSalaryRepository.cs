using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpSalaryRepository : IRepository<EmpSalary>
    {

    }

    public class EmpSalaryRepository : RepositoryBase<EmpSalary>, IEmpSalaryRepository
    {
        public EmpSalaryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
