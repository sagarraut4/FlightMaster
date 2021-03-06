using FlightInventory.DataAccess.Entities.Common;
using FlightInventory.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlightInventory.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly FlightInventoryDbContext _context;

        protected IQueryable<T> Query => _context.Set<T>();

        public Repository(FlightInventoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            return await Query.CountAsync();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await Query.SingleOrDefaultAsync(e => e.Id == id) != null;
        }

        public async Task<T> FindAsync(int id)
        {
            return await Query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<IEnumerable<T>> FindRangeAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync(true);
        }

        public async Task InsertAsync(IEnumerable<T> entities)
        {
            await _context.AddAsync(entities);
            await _context.SaveChangesAsync(true);
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await Query.ToListAsync();
        }

        public async Task<IEnumerable<T>> ToListAsync(int pageNum, int quantity, string orderColumn, bool ascendent)
        {
            if (!ascendent)
            {
                return await Query
                .OrderByDescending(e => typeof(T).GetProperty((orderColumn)).GetValue(e))
                .Skip((pageNum - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
            }
            return await Query
            .OrderBy(e => typeof(T).GetProperty((orderColumn)).GetValue(e))
            .Skip((pageNum - 1) * quantity)
            .Take(quantity)
            .ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> exp)
        {
            return await Query.SingleOrDefaultAsync(exp);
        }
    }
}
