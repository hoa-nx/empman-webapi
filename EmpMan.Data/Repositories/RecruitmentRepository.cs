using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IRecruitmentRepository : IRepository<Recruitment>
    {

    }

    public class RecruitmentRepository : RepositoryBase<Recruitment>, IRecruitmentRepository
    {
        public RecruitmentRepository(IDbFactory dbFactory ) : base(dbFactory)
        {
            
        }
    }
}
