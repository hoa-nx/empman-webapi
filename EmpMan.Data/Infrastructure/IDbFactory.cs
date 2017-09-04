using System;

namespace EmpMan.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        EmpManDbContext Init();
    }
}