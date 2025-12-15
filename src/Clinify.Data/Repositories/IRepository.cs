using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Data.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        T? Get(Guid id);
        List<T> GetAll();
        void Update(T entity);
        void Delete(Guid id);
        void Delete(T entity);
        bool Exits(Guid id);
        int Save();
    }
}
