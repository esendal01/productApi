using System.Collections.Generic;
using System.Threading.Tasks;

namespace product.Api.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}