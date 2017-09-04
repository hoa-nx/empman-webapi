using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IProjectDetailRepository : IRepository<ProjectDetail>
    {

    }

    public class ProjectDetailRepository : RepositoryBase<ProjectDetail>, IProjectDetailRepository
    {
        public ProjectDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
