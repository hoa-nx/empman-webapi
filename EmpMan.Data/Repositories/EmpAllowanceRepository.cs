using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpAllowanceRepository : IRepository<EmpAllowance>
    {

    }

    public class EmpAllowanceRepository : RepositoryBase<EmpAllowance>, IEmpAllowanceRepository
    {
        public EmpAllowanceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
