using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IDeptRepository : IRepository<Dept>
    {

    }

    public class DeptRepository : RepositoryBase<Dept>, IDeptRepository
    {
        public DeptRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
