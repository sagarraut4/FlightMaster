using FlightInventory.DataAccess.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlightInventory.DataAccess.Repository.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> ToListAsync();
        Task<T> FindAsync(int id);
        Task<IEnumerable<T>> FindRangeAsync(IEnumerable<int> ids);
        Task DeleteAsync(T entity);
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<T>> ToListAsync(int pageNum, int quantity, string orderColumn, bool ascendent);
        Task<int> CountAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> exp);
    }
}
