﻿using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IEmpSupportRepository : IRepository<EmpSupport>
    {

    }

    public class EmpSupportRepository : RepositoryBase<EmpSupport>, IEmpSupportRepository
    {
        public EmpSupportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
