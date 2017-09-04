using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpProfileTechRepository : IRepository<EmpProfileTech>
    {

    }

    public class EmpProfileTechRepository : RepositoryBase<EmpProfileTech>, IEmpProfileTechRepository
    {
        public EmpProfileTechRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
