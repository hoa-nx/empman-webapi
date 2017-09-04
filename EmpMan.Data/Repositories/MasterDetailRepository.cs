using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{

    public interface IMasterDetailRepository : IRepository<MasterDetail>
    {

    }

    public class MasterDetailRepository : RepositoryBase<MasterDetail>, IMasterDetailRepository
    {
        public MasterDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
