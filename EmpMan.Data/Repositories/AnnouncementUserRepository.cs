using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;

namespace EmpMan.Data.Repositories
{
    public interface IAnnouncementUserRepository : IRepository<AnnouncementUser>
    {
    }

    public class AnnouncementUserRepository : RepositoryBase<AnnouncementUser>, IAnnouncementUserRepository
    {
        public AnnouncementUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}