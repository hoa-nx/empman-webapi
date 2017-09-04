namespace EmpMan.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private EmpManDbContext dbContext;

        public EmpManDbContext Init()
        {
            return dbContext ?? (dbContext = new EmpManDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}