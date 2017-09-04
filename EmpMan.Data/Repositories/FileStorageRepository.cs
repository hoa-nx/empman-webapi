using EmpMan.Data.Infrastructure;
using EmpMan.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Data.Repositories
{
    public interface IFileStorageRepository : IRepository<FileStorage>
    {
        IEnumerable<FileStorage> AddFileStorage(FileStorage fileStorage);

        IEnumerable<FileStorage> GetAllFiles();

        FileStorage GetFileById(int id);
    }

    public class FileStorageRepository : RepositoryBase<FileStorage>, IFileStorageRepository
    {
        public FileStorageRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<FileStorage> AddFileStorage(FileStorage fileStorage)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileStorage> GetAllFiles()
        {
            throw new NotImplementedException();
        }

        public FileStorage GetFileById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
