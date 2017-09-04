using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IExchangeRateRepository : IRepository<ExchangeRate>
    {

    }

    public class ExchangeRateRepository : RepositoryBase<ExchangeRate>,  IExchangeRateRepository
    {
        public ExchangeRateRepository (IDbFactory dbFactory ) : base(dbFactory)
        {
            
        }
    }
}
