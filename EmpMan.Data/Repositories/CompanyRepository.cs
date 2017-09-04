using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {

    }

    public class CompanyRepository : RepositoryBase<Company>,  ICompanyRepository
    {
        public CompanyRepository (IDbFactory dbFactory ) : base(dbFactory)
        {
            
        }
    }
}
