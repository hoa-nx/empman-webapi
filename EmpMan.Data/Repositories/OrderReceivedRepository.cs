using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IOrderReceivedRepository : IRepository<OrderReceived>
    {

    }

    public class OrderReceivedRepository : RepositoryBase<OrderReceived>, IOrderReceivedRepository
    {
        public OrderReceivedRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
