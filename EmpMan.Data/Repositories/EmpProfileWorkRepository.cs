using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpProfileWorkRepository : IRepository<EmpProfileWork>
    {

    }

    public class EmpProfileWorkRepository : RepositoryBase<EmpProfileWork>, IEmpProfileWorkRepository
    {
        public EmpProfileWorkRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
