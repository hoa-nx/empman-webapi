using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IProjectDetailResourceRepository : IRepository<ProjectDetailResource>
    {

    }

    public class ProjectDetailResourceRepository : RepositoryBase<ProjectDetailResource>, IProjectDetailResourceRepository
    {
        public ProjectDetailResourceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
