namespace EmpMan.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        EmpManDbContext DbContext { get;}
        void Commit();
    }
}