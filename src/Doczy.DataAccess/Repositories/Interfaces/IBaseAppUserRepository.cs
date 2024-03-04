using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces.Base;

namespace Doczy.DataAccess.Repositories.Interfaces
{
    public interface IBaseAppUserRepository:IIdentityRepository<BaseAppUser>
    {
    }
}
