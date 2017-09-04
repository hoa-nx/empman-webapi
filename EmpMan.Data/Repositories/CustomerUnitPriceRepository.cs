using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface ICustomerUnitPriceRepository : IRepository<CustomerUnitPrice>
    {

    }

    public class CustomerUnitPriceRepository : RepositoryBase<CustomerUnitPrice>, ICustomerUnitPriceRepository
    {
        public CustomerUnitPriceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
