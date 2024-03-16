using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Doczy.DataAccess.Repositories.Interfaces.Base
{
    public interface IIdentityRepository<T> where T : BaseAppUser
    {
        Task<int> SaveAsync();
        IQueryable<T> GetAll(bool tracking = true, params Expression<Func<T, object>>?[] includes);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetUserByEmailOrPhoneNumberAsync(string emailOrPhoneNumber);
        Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression, params string[] includes);
        Task<T> GetSingleAysnc(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>?[] includes);
        bool Update(T user);
        Task<List<T>> GetUsersWithExpiredOTPAsync();
        IQueryable<T> FindAll(Expression<Func<T, bool>> expression, bool tracking = true, params Expression<Func<T, object>>?[] includes);

    }
}
