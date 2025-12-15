using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Data.Repositories
{
    public interface IRepositoryAsync<T>
    {
        Task AddAsync(T entity);
        Task<T?> GetAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
        Task<int> SaveAsync();
    }
}
