using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(long id);
        Task<long> CreateAsync(T entity);
        void Update(T entity);
        void Delete(long id);
        void SaveChange();
        IEnumerable<T> GetAll();
    }
}
