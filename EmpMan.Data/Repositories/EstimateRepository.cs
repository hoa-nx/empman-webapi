using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEstimateRepository : IRepository<Estimate>
    {

    }

    public class EstimateRepository : RepositoryBase<Estimate>, IEstimateRepository
    {
        public EstimateRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
