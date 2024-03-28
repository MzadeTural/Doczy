using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace Doczy.DataAccess.Repositories.Implementations.Base
{
    public class IdentityRepository<T> : IIdentityRepository<T> where T : BaseAppUser
    {
        private readonly DoczyContext _context;

        public IdentityRepository(DoczyContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

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
        public async Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>?[] includes)
          => await GetQuery(includes).FirstOrDefaultAsync(expression);
        public async Task<T> GetByIdAsync(Guid id)     
            => await Table.FindAsync(id);

        public async Task<T> GetUserByEmailOrPhoneNumberAsync(string emailOrPhoneNumber)
       => await Table.FirstOrDefaultAsync(u => u.Email == emailOrPhoneNumber || u.PhoneNumber == emailOrPhoneNumber);
        

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        public bool Update(T user)
        {
            var data = Table.Update(user);

            return data.State == EntityState.Modified;
        }


        public async Task<List<T>> GetUsersWithExpiredOTPAsync()
        {
            var currentTime = DateTime.UtcNow;
            return await Table
                .Where(u => u.OTP != null && u.OTPExpiryDate < currentTime)
                .ToListAsync();
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

        public IQueryable<T> GetAll(bool tracking = true, params Expression<Func<T, object>>?[] includes)
        {
            var query = GetQuery(includes);
            query = !tracking ? query.AsNoTracking() : query;
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
      

        public async Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression, params string[] includes)
         => await GetQueryy(includes).FirstOrDefaultAsync(expression);
    }
}
