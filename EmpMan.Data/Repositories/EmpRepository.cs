using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpRepository : IRepository<Emp>
    {

    }

    public class EmpRepository : RepositoryBase<Emp>, IEmpRepository
    {
        public EmpRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
