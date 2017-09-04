using Microsoft.AspNet.Identity.EntityFramework;
using EmpMan.Data;
using EmpMan.Model.Models;

namespace EmpMan.Identity
{
    public class ApplicationUserStore : UserStore<AppUser>
    {
        public ApplicationUserStore(EmpManDbContext context)
            : base(context)
        {
        }
    }
}