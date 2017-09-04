using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IRecruitmentStaffRepository : IRepository<RecruitmentStaff>
    {

    }

    public class RecruitmentStaffRepository : RepositoryBase<RecruitmentStaff>, IRecruitmentStaffRepository
    {
        public RecruitmentStaffRepository(IDbFactory dbFactory ) : base(dbFactory)
        {
            
        }
    }
}
