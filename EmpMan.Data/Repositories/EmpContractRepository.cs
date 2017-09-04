using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpContractRepository : IRepository<EmpContract>
    {

    }

    public class EmpContractRepository : RepositoryBase<EmpContract>, IEmpContractRepository
    {
        public EmpContractRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
