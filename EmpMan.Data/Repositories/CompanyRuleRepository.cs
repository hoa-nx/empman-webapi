using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface ICompanyRuleRepository : IRepository<CompanyRule>
    {

    }

    public class CompanyRuleRepository : RepositoryBase<CompanyRule>, ICompanyRuleRepository
    {
        public CompanyRuleRepository(IDbFactory dbFactory ) : base(dbFactory)
        {
            
        }
    }
}
