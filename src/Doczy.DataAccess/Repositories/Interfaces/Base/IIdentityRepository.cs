using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Doczy.DataAccess.Repositories.Interfaces.Base
{
    public interface IIdentityRepository<T> where T : BaseAppUser
    {
        Task<int> SaveAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetUserByEmailOrPhoneNumberAsync(string emailOrPhoneNumber);
        bool Update(T user);
        Task<List<T>> GetUsersWithExpiredOTPAsync();
        IQueryable<T> FindAll(Expression<Func<T, bool>> expression, bool tracking = true, params Expression<Func<T, object>>?[] includes);

    }
}
