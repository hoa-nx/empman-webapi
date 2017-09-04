using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{

    public interface ITeamRepository : IRepository<Team>
    {

    }

    public class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        public TeamRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
