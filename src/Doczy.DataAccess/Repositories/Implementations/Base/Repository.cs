using Doczy.Core.Entities.Common;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
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

        public bool Delete(T entity)
          => Table.Remove(entity).State == EntityState.Deleted;
        public async Task<T> GetByIdAsync(Guid id)
           => await Table.FindAsync(id);
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

        public async Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>?[] includes)
            => await GetQuery(includes).FirstOrDefaultAsync(expression);

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

        public bool DeleteRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
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
        public IQueryable<T> GetQueryy(params string[] includes)
        {
            var query = Table.AsQueryable();

            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
        public Task<T> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetSingleStringIncludeAysnc(Expression<Func<T, bool>> expression, params string?[] stringIncludes)
         => await GetQueryy(stringIncludes).FirstOrDefaultAsync(expression);
    }
}
