using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpDetailWorkRepository : IRepository<EmpDetailWork>
    {

    }

    public class EmpDetailWorkRepository : RepositoryBase<EmpDetailWork>, IEmpDetailWorkRepository
    {
        public EmpDetailWorkRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
