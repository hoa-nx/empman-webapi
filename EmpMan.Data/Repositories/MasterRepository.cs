using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{

    public interface IMasterRepository : IRepository<Master>
    {

    }

    public class MasterRepository : RepositoryBase<Master>, IMasterRepository
    {
        public MasterRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }

}
