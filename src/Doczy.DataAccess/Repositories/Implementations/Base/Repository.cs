using Doczy.Core.Entities.Common;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Doczy.DataAccess.Repositories.Implementations.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly DoczyContext _context;

        public Repository(DoczyContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> CreateAsync(T entity)
        {
            var data = await Table.AddAsync(entity);

            return data.State == EntityState.Added;
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> expression, bool tracking = true, params Expression<Func<T, object>>?[] includes)
        {
            var query = GetQuery(includes).Where(expression);
            query = !tracking ? query.AsNoTracking() : query;
            return query;
        }

        public IQueryable<T> FindAllPaginate(Expression<Func<T, bool>> expression, int pageIndex, int pageSize, bool tracking = true, params Expression<Func<T, object>>?[] includes)
        {
            var query = GetQuery(includes).Where(expression);
            query = !tracking ? query.AsNoTracking() : query;
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }



        public IQueryable<T> GetAll(bool tracking = true, params Expression<Func<T, object>>[] includes)
            => !tracking ? GetQuery(includes).AsNoTracking() : GetQuery(includes);

        public async Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression)
            => await Table.Where(expression).FirstOrDefaultAsync();

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
          => await GetQuery(includes).AnyAsync(expression);

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public bool SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            return true;
        }


        public bool Update(T entity)
        {
            var data = Table.Update(entity);

            return data.State == EntityState.Modified;
        }



        private IQueryable<T> GetQuery(params Expression<Func<T, object>>[] includes)
        {
            var query = Table.AsQueryable();
            if (includes is not null && includes?.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }


    }
}
