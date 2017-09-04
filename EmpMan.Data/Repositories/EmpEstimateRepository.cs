using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpEstimateRepository : IRepository<EmpEstimate>
    {

    }

    public class EmpEstimateRepository : RepositoryBase<EmpEstimate>, IEmpEstimateRepository
    {
        public EmpEstimateRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
