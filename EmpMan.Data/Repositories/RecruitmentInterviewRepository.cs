using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IRecruitmentInterviewRepository : IRepository<RecruitmentInterview>
    {

    }

    public class RecruitmentInterviewRepository : RepositoryBase<RecruitmentInterview>, IRecruitmentInterviewRepository
    {
        public RecruitmentInterviewRepository(IDbFactory dbFactory ) : base(dbFactory)
        {
            
        }
    }
}
