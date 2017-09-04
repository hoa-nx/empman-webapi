using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IRevenueRepository : IRepository<Revenue>
    {

    }

    public class RevenueRepository : RepositoryBase<Revenue>, IRevenueRepository
    {
        public RevenueRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
