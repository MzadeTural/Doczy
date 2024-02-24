using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;

namespace Doczy.DataAccess.Repositories.Interfaces.Base
{
    public interface IIdentityRepository<T> where T : BaseAppUser
    {
        Task<int> SaveAsync();
        Task<T> GetByIdAsync(Guid id);

    }
}
