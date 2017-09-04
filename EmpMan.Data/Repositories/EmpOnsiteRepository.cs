using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpOnsiteRepository : IRepository<EmpOnsite>
    {

    }

    public class EmpOnsiteRepository : RepositoryBase<EmpOnsite>, IEmpOnsiteRepository
    {
        public EmpOnsiteRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
