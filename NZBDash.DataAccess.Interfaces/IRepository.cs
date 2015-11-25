using System.Collections.Generic;
using System.Threading.Tasks;

using NZBDash.Common.Models.Data;

namespace NZBDash.DataAccess.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        T Find(int id);
        Task<T> FindAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Insert(T entity);
        IEnumerable<T> Insert(IEnumerable<T> entity);
        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entity);
        Task<T> InsertAsync(T entity);
        int Remove(T entity);
        Task<int> RemoveAsync(T entity);
        Task<int> ModifyAsync(T entity);
        int Modify(T entity);
    }
}